using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace GLAS_Adapter
{

    /// <summary>
    /// UDP服务,接收CMS发送过来的井深数据
    /// </summary>
    public class UdpServer
    {
        /// <summary>
        /// 本地监听端口号
        /// </summary>
        private int UdpPort = 8001;
        /// <summary>
        /// 服务器UDP套接字
        /// </summary>
        private Socket ServerSocket = null;
        /// <summary>
        /// 接收线程
        /// </summary>
        private Thread RecvThread = null;
        /// <summary>
        /// 神开2000深度信息字符数组
        /// </summary>
        private char[] SK2000_Chars = null;
        /// <summary>
        /// 神开2000深度信息字符队列
        /// </summary>
        private List<char> SK2000List = new List<char>();

        private char[] emp1255 = new char[1255];
        private DepType TypeDepth { get; set; }
        /// <summary>
        /// 是否继续循环
        /// </summary>
        //private bool IsContinue = true;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Port"></param>
        public UdpServer(int localUdpPort,DepType dt)
        {
            this.UdpPort = localUdpPort;
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            TypeDepth = dt;
            RecvThread = new Thread(new ThreadStart(this.Thread_Func));
        }

        /// <summary>
        /// 开始接收数据
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            bool bRet = false;
            try
            {
                IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, UdpPort);
                ServerSocket.Bind(ipEnd);
                RecvThread.Start();
                bRet = true;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("UdpServer::Start->"+ex.Message);
                bRet = false;
            }
            return bRet;
        }

        /// <summary>
        /// 停止接收数据
        /// </summary>
        public void Stop()
        {
            try
            {
                if (ServerSocket != null)
                {
                    ServerSocket.Close();
                }
            }
            catch { }
            try
            {
                if (RecvThread != null)
                {
                    RecvThread.Abort();
                }
            }
            catch{}   
        }

        private byte[] BytesRecv = new byte[10240];
        /// <summary>
        /// 在此线程函数中接收UDP数据
        /// </summary>
        void Thread_Func()
        {
            System.Diagnostics.Trace.WriteLine("UdpServer 接收线程启动!");
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sender);
            while (true)
            {
                try
                {
                    if(TypeDepth.Equals(DepType.CMS))//从CMS平台 UDP获得广播深度
                    {
                        int rcvLen = ServerSocket.ReceiveFrom(BytesRecv, ref Remote);
                        if (rcvLen <= 0)
                        {
                            Thread.Sleep(300);
                            continue;
                        }
                        string strWitsDepth = System.Text.Encoding.ASCII.GetString(BytesRecv, 0, rcvLen);
                        if (strWitsDepth.StartsWith("&&\r\n01"))
                        {
                            float fWellDepth = GetWellDepth(strWitsDepth);
                            System.Diagnostics.Trace.WriteLine("从CMS收到深度数据:" + fWellDepth);
                            //System.Diagnostics.Trace.WriteLine(strWitsDepth);
                            CommonData.WellDepth = fWellDepth;
                        }
                    }
                    if(TypeDepth.Equals(DepType.SK2000))//从SK2000平台 UDP获得广播深度
                    {
                        while (true)
                        {
                            int rcvLen = ServerSocket.ReceiveFrom(BytesRecv, ref Remote);
                            if (rcvLen <= 0)
                            {
                                Thread.Sleep(200);
                                continue;
                            }
                            SK2000_Chars = System.Text.Encoding.ASCII.GetChars(BytesRecv, 0, rcvLen);
                            SK2000List.AddRange(SK2000_Chars);
                            if (SK2000List.Count >= 1255 && SK2000List[4] == '/')
                                /*
                                 * SK2000帧格式截断
                                 */
                            {
                                char[] temp = emp1255;
                                for (int i = 0; i < 1255; i++)
                                {
                                    temp[i] = SK2000List[i];
                                }
                                SK2000List.RemoveRange(0, 1255);
                                float fWellDepth = GetWellDepth(temp);
                                System.Diagnostics.Trace.WriteLine("从SK2000收到深度数据:" + fWellDepth);
                                CommonData.WellDepth = fWellDepth;
                            }
                            else SK2000List.RemoveRange(0, 1);
                        }
                    }
                }
                catch(Exception e)
                {
                    System.Diagnostics.Trace.WriteLine("UdpServer::Thread_Func->"+e.Message);
                }
            }
        }
        /// <summary>
        /// 从WITS中获取深度
        /// </summary>
        /// <param name="strDepthWits"></param>
        /// <returns></returns>
        private float GetWellDepth(string strDepthWits)
        {
            string[] strSplits = strDepthWits.Split(new string[] {"\r\n"},StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < strSplits.Length-1; i++)
            {
                //1表第10项是井深
                if (strSplits[i].StartsWith("0110"))
                {
                    return float.Parse(strSplits[i].Substring(4));
                }
            }
            return 0.0f;
        }
        /// <summary>
        /// 从WITS中获取深度
        /// </summary>
        /// <param name="strDepthWits"></param>
        /// <returns></returns>
        private float GetWellDepth(char[] charDepthWits)
        {
            float pi = 3.14f;
            String tempS = null;
            //Char[] tempC = new Char[10];
            for (int i = 0; i < 10;i++)
            {
                tempS+= charDepthWits[825 + i];
            }
            //tempS = tempC;
            pi = float.Parse(tempS);
            return pi;
        }
    }
}
