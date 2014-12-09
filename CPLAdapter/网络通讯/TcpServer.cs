using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GLAS_Adapter
{
    /// <summary>
    /// TCP服务器,接收CPL连接
    /// </summary>
    public class TcpServer
    {
        private int TcpPort = 8002;
        private Socket ServerSocket = null;
        private Thread RecvThread = null;
        private Socket CurClientSocket = null;
        private object ClientSocketAsynObj = new object();
        private List<byte> DataTemp = new List<byte>();
        private List<byte> DepthTemp = new List<byte>();
        private byte[] Emp6 = new byte[6];
        private byte[] Emp802 = new byte[802];
        public Adapter Owner { get; set; }//tcpServer's Owner-"Adapter Object"
        //public GroundBox gBox { get; set; }
        private Object SentObject = new Object();
        /// <summary>
        /// 接收定时器
        /// </summary>
        private MMTimer RecvTimer1 = null;//Spp&CMS_Depth
        private MMTimer RecvTimer2 = null;//CPL_Depth
        //private MMTimer RecvTimer3 = null;//CMS-Depth

        private UInt16 RecvCount {get;set;}
        /// <summary>
        /// 是否开始测井
        /// </summary>
        private bool IsStartWell = false;
        /// <summary>
        /// 是否开始深度跟踪
        /// </summary>
        private bool IsStartDepth = false;
        /// <summary>
        /// 存放井深的字节数组
        /// </summary>
        private byte[] WellDeptBytes = new byte[6];
        private byte[] WellDeptBytesCMS=new byte[6];
        private byte[] TempBytes = null;
        private byte[] bytesSent=null;
        /// <summary>
        /// 是否继续线程循环
        /// </summary>
        private bool IsContinue = false;
        private DepType TypeDepth { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tcpPort">端口号</param>
        public TcpServer(int tcpPort,DepType dt)
        {
            this.TcpPort = tcpPort;
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            RecvThread = new Thread(new ThreadStart(this.RecvThreadFunc));
            RecvTimer1 = new MMTimer(this.SendTimerFunc);
            RecvTimer2 = new MMTimer(this.SendTimerFunc_CPL_Depth);
            TypeDepth = dt;
            WellDeptBytes = Emp6;
            Emp6[0] = Emp6[1] = 0x44;
            WellDeptBytesCMS[0] = WellDeptBytesCMS[1] = 0x45;
            Emp802[0] = Emp802[1] = 0x53;
        }
        /// <summary>
        /// tcp服务开始启动
        /// </summary>
        public bool Start()
        {
            bool bRet = false;
            IsContinue = true;
            try
            {
                IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, TcpPort);
                ServerSocket.Bind(ipEnd);
                ServerSocket.Listen(10);
                RecvThread.Start();
                RecvTimer1.Start(1000, true);
                RecvTimer2.Start(250, true);//Half period of circulation
                DepthTemp.Add(0x44);
                DepthTemp.Add(0x44);
                DataTemp.Add(0x53);
                DataTemp.Add(0x53);
                WellDeptBytesCMS[0] = 0x45;
                WellDeptBytesCMS[1] = 0x45;
                bRet = true;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("TcpServer::Start->" + ex.Message);
                bRet = false;
            }
            return bRet;
        }
        /// <summary>
        /// tcp服务开始关闭
        /// </summary>
        public void Stop()
        {
            IsContinue = false;
            try
            {
                if (ServerSocket != null)
                {
                    ServerSocket.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            try
            {

                if (RecvThread != null)
                {
                    RecvThread.Abort();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            try
            {
                if (RecvTimer1 != null)
                {
                    RecvTimer1.Stop();
                }
                if (RecvTimer2 != null)
                {
                    RecvTimer2.Stop();
                }
                //if(RecvTimer3!=null)
                //{
                //    RecvTimer3.Stop();
                //}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 在此线程函数中接收命令数据
        /// </summary>
        void RecvThreadFunc()
        {
            System.Diagnostics.Trace.WriteLine("TcpServer 线程启动！");
            byte[] bytesRecv = new byte[64];
            while (IsContinue)
            {
                try
                {
                    CurClientSocket = ServerSocket.Accept();
                    Trace.WriteLine(string.Format("客户端{0}已经连接！",((IPEndPoint)CurClientSocket.RemoteEndPoint).Address.ToString()));
                    CurClientSocket.Send(ASCIIEncoding.ASCII.GetBytes("Server OK,YOU CAN SEND COMMAND"));

                    while (IsContinue)//一个新的连接
                    {
                        int rcvLen = CurClientSocket.Receive(bytesRecv);
                        if (rcvLen <= 0)
                        {
                            lock (ClientSocketAsynObj)
                            {
                                CurClientSocket.Close();
                                CurClientSocket = null;
                                IsStartWell = IsStartDepth = false;
                            }
                            break;
                        }
                        //对比 如果是开始发送指令
                        if (rcvLen == 32 && bytesRecv[0] == 'C' && bytesRecv[1] == 'O' && bytesRecv[2] == 'M' && bytesRecv[3] == 'D'
                            && bytesRecv[4] == 0x20 && bytesRecv[5] == 0x20 && bytesRecv[7] == 0x30)
                        {
                            System.Diagnostics.Trace.WriteLine(string.Format("收到指令{0}", bytesRecv[6]));
                            lock (ClientSocketAsynObj)
                            {
                                //43 4F 4D 44 20 20 00 30 00 00 00 00 00 00 00 00 20 03 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
                                //开始测井0x3000
                                if (bytesRecv[6] == 0x00)
                                {
                                    IsStartWell = true;
                                    CommonData.ClearSpp();
                                    Owner.gBox.ClearQueue();
                                    DataTemp.Clear();
                                }//停止测井
                                else if (bytesRecv[6] == 0x01)
                                {
                                     IsStartWell = false;
                                }//开始深度跟踪
                                else if (bytesRecv[6] == 0x02)
                                {
                                    IsStartDepth = true;
                                    CommonData.ClearDep();
                                    Owner.gBox.ClearQueue();
                                    DepthTemp.Clear();
                                }
                                //停止深度跟踪
                                else if (bytesRecv[6] == 0x03)
                                {
                                    IsStartDepth = false;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine("TCPServer::RecvThreadFunc->" + e.Message);
                }
            }
        }
        /// <summary>
        /// 定时发送SPP函数
        /// </summary>
        /// <param name="uTimerID"></param>
        /// <param name="uMsg"></param>
        /// <param name="dwUser"></param>
        /// <param name="dw1"></param>
        /// <param name="dw2"></param>
        private void SendTimerFunc(uint uTimerID, uint uMsg, UIntPtr dwUser, UIntPtr dw1, UIntPtr dw2)
        {
            try
            {
                lock (ClientSocketAsynObj)
                {
                    RecvCount++;
                    //发送深度数据//c3 f5 48 40
                    //CommonData.WellDepth++;//????????
                    byte[] curWellDeptBytes = BitConverter.GetBytes(CommonData.WellDepth);
                    WellDeptBytesCMS[2] = curWellDeptBytes[0];
                    WellDeptBytesCMS[3] = curWellDeptBytes[1];
                    WellDeptBytesCMS[4] = curWellDeptBytes[2];
                    WellDeptBytesCMS[5] = curWellDeptBytes[3];
                    if (IsStartDepth && CurClientSocket != null && CurClientSocket.Connected && TypeDepth.Equals(DepType.CMS))
                    {
                        Trace.WriteLine(DateTime.Now.ToLongTimeString());
                        Funcs._funcs.print(WellDeptBytesCMS);
                        if (Sent(WellDeptBytesCMS) <= 0)
                        {
                            System.Diagnostics.Trace.WriteLine("下发深度数据失败！");
                        }
                    }
                }
                lock (ClientSocketAsynObj)
                {
                    if (IsStartWell && CurClientSocket != null && CurClientSocket.Connected)
                    {
                        byte[] curSppBytes = CommonData.GetQueueItem();
                        if (curSppBytes != null)
                        {
                            DataTemp.AddRange(curSppBytes);
                            while (DataTemp.Count >= 802)
                            {
                                TempBytes = Emp802;
                                if (DataTemp[800] == DataTemp[801] && DataTemp[800] == 0x53)
                                {
                                    TempBytes[0] = DataTemp[800];
                                    TempBytes[1] = DataTemp[801];
                                    for (int i = 0; i < 800; i++)
                                    {
                                        TempBytes[i + 2] = DataTemp[i];
                                    }
                                }
                                else if (DataTemp[0] == DataTemp[1] && DataTemp[0] == 0x53)
                                {
                                    for (int i = 0; i < 802; i++)
                                    {
                                        TempBytes[i] = DataTemp[i];
                                    }
                                }
                                DataTemp.Clear();
                                Trace.WriteLine(DateTime.Now.ToLongTimeString());
                                Funcs._funcs.print(TempBytes);
                                //lock (SentObject)
                                {
                                    if (Sent(TempBytes) <= 0)
                                    {
                                        System.Diagnostics.Trace.WriteLine("下发压力数据失败！");
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine("TCPServer::SendTimerFunc->" + e.Message);
            }
        }
        /// <summary>
        /// 定时发送CPL_BPI&GZ函数
        /// </summary>
        /// <param name="uTimerID"></param>
        /// <param name="uMsg"></param>
        /// <param name="dwUser"></param>
        /// <param name="dw1"></param>
        /// <param name="dw2"></param>
        private void SendTimerFunc_CPL_Depth(uint uTimerID, uint uMsg, UIntPtr dwUser, UIntPtr dw1, UIntPtr dw2)
        {
            try
            {
                lock (ClientSocketAsynObj)
                {
                    if (IsStartDepth && CurClientSocket != null && CurClientSocket.Connected&&TypeDepth.Equals(DepType.CPL))
                    {
                        byte[] curWellDeptBytes = CommonData.GetDepthItem();
                        int frag = 0;
                        if (curWellDeptBytes != null)
                        {
                            DepthTemp.AddRange(curWellDeptBytes);
                            while (DepthTemp.Count >= 6)
                            {
                                WellDeptBytes =Emp6;
                                if (DepthTemp[0] == DepthTemp[1] && DepthTemp[0] == 0x44)
                                {                        
                                    WellDeptBytes[0] = DepthTemp[0];
                                    WellDeptBytes[1] = DepthTemp[1];
                                    WellDeptBytes[2] = DepthTemp[4];
                                    WellDeptBytes[3] = DepthTemp[5];
                                    WellDeptBytes[4] = DepthTemp[2];
                                    WellDeptBytes[5] = DepthTemp[3];
                                    frag = 0;
                                }
                                else if (DepthTemp[2] == DepthTemp[3] && DepthTemp[2] == 0x44)
                                {
                                    WellDeptBytes[0] = DepthTemp[2];
                                    WellDeptBytes[1] = DepthTemp[3];
                                    WellDeptBytes[2] = DepthTemp[0];
                                    WellDeptBytes[3] = DepthTemp[1];
                                    WellDeptBytes[4] = DepthTemp[4];
                                    WellDeptBytes[5] = DepthTemp[5];
                                    frag = 2;
                                }
                                else if (DepthTemp[4] == DepthTemp[5] && DepthTemp[4] == 0x44)
                                {
                                    WellDeptBytes[0] = DepthTemp[4];
                                    WellDeptBytes[1] = DepthTemp[5];
                                    WellDeptBytes[2] = DepthTemp[2];
                                    WellDeptBytes[3] = DepthTemp[3];
                                    WellDeptBytes[4] = DepthTemp[0];
                                    WellDeptBytes[5] = DepthTemp[1];
                                    frag = 4;
                                }
                                if (DepthTemp.Count > 6)
                                    DepthTemp.RemoveRange(0, frag);
                                else if (DepthTemp.Count == 6)
                                    DepthTemp.RemoveRange(0, 6);
                                DepthTemp.Clear();
                                Trace.WriteLine(DateTime.Now.ToLongTimeString());
                                Funcs._funcs.print(WellDeptBytes);
                                //lock (SentObject)
                                {
                                    if (Sent(WellDeptBytes) <= 0)
                                    {
                                        System.Diagnostics.Trace.WriteLine("发送深度数据失败！");
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        ///// <summary>
        ///// 定时发送CMS_BPI&GZ函数
        ///// </summary>
        ///// <param name="uTimerID"></param>
        ///// <param name="uMsg"></param>
        ///// <param name="dwUser"></param>
        ///// <param name="dw1"></param>
        ///// <param name="dw2"></param>
        //private void SendTimerFunc_CMS_Depth(uint uTimerID, uint uMsg, UIntPtr dwUser, UIntPtr dw1, UIntPtr dw2)
        //{
        //    try
        //    {
        //        RecvCount++;
        //        byte[] curWellDeptBytes = BitConverter.GetBytes(CommonData.WellDepth);
        //        WellDeptBytesCMS[2] = curWellDeptBytes[0];
        //        WellDeptBytesCMS[3] = curWellDeptBytes[1];
        //        WellDeptBytesCMS[4] = curWellDeptBytes[2];
        //        WellDeptBytesCMS[5] = curWellDeptBytes[3];
        //        lock (ClientSocketAsynObj)
        //        {
        //            if (IsStartDepth && CurClientSocket != null && CurClientSocket.Connected && TypeDepth.Equals(DepType.CMS))
        //            {
        //                Trace.WriteLine(DateTime.Now.ToLongTimeString());
        //                Funcs._funcs.print(WellDeptBytesCMS);
        //                if (Sent(WellDeptBytesCMS) <= 0)
        //                {
        //                    System.Diagnostics.Trace.WriteLine("下发深度数据失败！");
        //                }
        //            }
        //        }
                    
                
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Trace.WriteLine(ex.Message);
        //    }
        //}
        public int Sent (byte[] tempBytes)
        {
            lock (SentObject)
            {
                bytesSent = tempBytes;
                return CurClientSocket.Send(bytesSent);
            }
        }
        private void print(byte[] receive)
        {
            System.Diagnostics.Debug.WriteLine(receive.Length + "->" + DateTime.Now.ToString());
            int c = 0;
            for (int i = 0; i < receive.Length; i++)
            {
                System.Diagnostics.Debug.Write(string.Format("{0:X2} ", receive[i]));
                c++;
                if (c == 16)
                {
                    System.Diagnostics.Debug.Write("\n");
                    c = 0;
                }
            }
            System.Diagnostics.Debug.Write("\n\n");
        }
        /// <summary>
        /// Depth Resource
        /// </summary>

        //public void ChangeToCPL()
        //{
        //    TypeDepth = DepType.CPL;
        //}
        //public void ChangeToCMS()
        //{
        //    TypeDepth = DepType.CMS;
        //}
        public bool OnTimer1()
        {
            try
            {
                RecvTimer1.Start(1000, true);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }
        public bool OnTimer2()
        {
            try
            {
                RecvTimer2.Start(250, true);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }
        //public bool OnTimer3()
        //{
        //    try
        //    {
        //        RecvTimer3.Start(1000, true);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Trace.WriteLine(ex.Message);
        //        return false;
        //    }
        //}
        public bool OffTimer1()
        {
            try
            {
                RecvTimer1.Stop();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }
        public bool OffTimer2()
        {
            try
            {
                RecvTimer2.Stop();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }
        //public bool OffTimer3()
        //{
        //    try
        //    {
        //        RecvTimer3.Stop();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Trace.WriteLine(ex.Message);
        //        return false;
        //    }
        //}
    }        
}
