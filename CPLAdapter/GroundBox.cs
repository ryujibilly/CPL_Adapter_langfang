using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO.Ports;
using System.Diagnostics;
using System.Collections;
using System.Linq;
using System.IO;

namespace CPLAdapter
{
    public delegate void DataRecvCallback(byte[] datalist,int dataType);

    public class GroundBox:IGroundBox
    {
        private SerialPort Communication =null; 
        private MMTimer BoxTimer = null;
        private DataRecvCallback GetData;
        private string BoxCom;
        private int BoxBaudRate;
        private int TimerCount = 0;
        private WirelessProtocol WireProt = null;
        private LineProtocol LineProt = null;
        private FileStream fs;
        private StreamWriter sw;
        private byte[] FragHead = new byte[4];
        private List<byte> Pressuredatatemp = new List<byte>();
        private List<byte> Rawdata = new List<byte>();
        private List<byte> Pressure_data = new List<byte>();
        private List<byte[]> Displaydata = new List<byte[]>();
        private List<byte> Depth_data = new List<byte>();
        private List<byte> Depthdatatemp = new List<byte>();
        private object objDisplay = new object();
        private static object QueueObj = new object();
        private byte[] START = new byte[] { 0xcc, 0xcc, 0xcc, 0xcc, 0xa2 };
        public enum DecodeMethod { origin, non_direct, filter };
        public void BeginWork()
        {
            ClearQueue();
            CommonData.ClearQueue();
            Communication.Write(START, 0, 5);
        }

        public void ClearQueue()
        {
            lock (QueueObj)
            {
                if (Pressure_data.Count > 0)
                {
                    Pressure_data.Clear();
                }
                if (Rawdata.Count > 0)
                {
                    Rawdata.Clear();
                }
                if (Pressuredatatemp.Count > 0)
                {
                    Pressuredatatemp.Clear();
                }
                if(Depth_data.Count>0)
                {
                    Depth_data.Clear();
                }
                if(Depthdatatemp.Count>0)
                {
                    Depthdatatemp.Clear();
                }
            }
        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="dataRecv"></param>
        /// <param name="com"></param>
        /// <param name="baudRate"></param>
        public GroundBox(DataRecvCallback dataRecv,string com,int baudRate,string strSNno,string strKeyno)
        {
            Communication = new SerialPort();
            BoxTimer = new MMTimer(Callback);
            WireProt = new WirelessProtocol(strSNno,strKeyno);
            LineProt = new LineProtocol();
            GetData = dataRecv;
            BoxCom = com;
            BoxBaudRate = baudRate;
        }
        /// <summary>
        /// start
        /// </summary>
        public bool Start()
        {
            if (!OpenCom(BoxCom, BoxBaudRate))
            {
                return false;
            }
            BoxTimer.Start(100, false);//BoxTimer.Start(100, true);   向前端箱要数  亮
            return true;
        }
        /// <summary>
        /// stop
        /// </summary>
        public void Stop()
        {
            try
            {
                if (Communication.IsOpen)
                {
                    Communication.Close();
                }
                BoxTimer.Stop();
            }
            catch { }
        }
        /// <summary>
        /// 获取最新4组 每组25个压力数组
        /// </summary>
       // private byte[] GetLatestData = new byte[] { 0xaa, 0x55, 0x01, 0x00, 0x05, 0x00, 0x02, 0x88, 0x01};
        //private byte[] GetLatestData = new byte[] { 0xcc, 0xcc, 0xcc, 0xcc, 0xa2 };//亮
        /// <summary>
        /// 定时器回调函数
        /// </summary>
        private void Callback(uint uTimerID, uint uMsg, UIntPtr dwUser, UIntPtr dw1, UIntPtr dw2)
        {
            //Callback from the MMTimer API that fires the Timer event. Note we are in a different thread here
            //TimerCount++;
            //if (TimerCount == 5)       //read liya  
            //{
            //    SendByte(GetLatestData);
            //    TimerCount = 0;
            //}
            //else  //send dev azi toolface
            //{
            //    lock (objDisplay)
            //    {
            //        if (Displaydata.Count != 0)
            //        {
            //            SendByte(Displaydata[0]);
            //            Displaydata.RemoveAt(0);
            //        }
            //    }
            //}
            SendByte(START);//亮
        }
        

        /// <summary>
        /// savedata
        /// </summary>
        public void SaveData(string dataBuffer)
        {
            if ((!dataBuffer.StartsWith("&&\r\n07")) || (!dataBuffer.EndsWith("!!\r\n")))
            {
                return;
            }
            string[] buffer = dataBuffer.Replace("\r\n", "@").Split('@');
            foreach (string str in buffer)
            {
                if (str.Length < 2)
                {
                    continue;
                }
                string pageNo = str.Substring(0, 2);
                if (pageNo != "07")
                {
                    continue;
                }
                string location = str.Substring(2, 2);
                if (location != "13" && location != "14" && location != "16" && location != "17")
                {
                    continue;
                }
                byte[] bytesRet = null;
                if(Config.CfgInfo.DisplayMode==1)
                {
                    //有线
                    bytesRet=LineProt.GetBytesForDisplay(location, str);
                }
                else
                {
                    //无线
                    bytesRet = WireProt.GetBytesForDisplay(location, str);
                }
                lock (objDisplay)
                {
                    Displaydata.Add(bytesRet);
                }
            }
        }
       
        /// <summary>
        /// open com
        /// </summary>
        private bool OpenCom(string com, int baudrate)
        {
            bool bRet = false;
            try
            {
                Communication.PortName = com;
                Communication.BaudRate = baudrate;
                Communication.RtsEnable = true;
                Communication.DataBits = 8;
                Communication.Parity = Parity.None;
                Communication.StopBits = StopBits.One;
                Communication.ReceivedBytesThreshold = 259;
                Communication.DataReceived += new SerialDataReceivedEventHandler(Communication_DataReceived);//数据接收的事件
                Communication.Open();
               // byte[] changeMode = new byte[] { 0xaa, 0x55, 0x01, 0x00, 0x01, 0x00, 0x00, 0x48, 0x01 };
               // SendByte(changeMode);
                //Thread.Sleep(200);
                //Pressuredatatemp.Add(0x53);
                //Pressuredatatemp.Add(0x53); 
                bRet = true;
            }
            catch (Exception Erro)
            {
                bRet = false;
                System.Diagnostics.Debug.WriteLine(Erro.Message);
            }
            return bRet;
        }
        /// <summary>
        /// 往串口写数据
        /// </summary>
        private void SendByte(byte[] ByteList)
        {
            if (Communication.IsOpen)
            {
                Communication.Write(ByteList, 0, ByteList.Length);
            }
            else
            {
                Debug.WriteLine("The port is not opened");
            }
        }
       
        private void print(byte[] receive)
        {
            //System.Diagnostics.Debug.WriteLine(receive.Length + "->" + DateTime.Now.ToString());
            //return;
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
        /// receive data
        /// </summary>
        private void Communication_DataReceived(object sender, SerialDataReceivedEventArgs e)//串口数据接收   亮
        {
            Thread.Sleep(10);                 
            byte[] receive = new byte[Communication.BytesToRead];
            Communication.Read(receive, 0, receive.Length);
            Rawdata.AddRange(receive);
            int mark = CheckData(Rawdata.ToArray());
            if (mark > 0)
            {
                Rawdata.RemoveRange(0, mark);
                Trace.WriteLine(">>>>>>>>>> mark1="+mark+"<<<<<<<<<<<<<");
            }
            if(Rawdata.Count>=6112&&mark<0)
            {
                Rawdata.RemoveRange(0,6108);
            }
            if (Rawdata.Count < 6112)
            {
                return;
            }
            if (mark==0)
            {
                while (true)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        for (int m = 0; m < 100 ; m++)
                        {

                            Pressure_data.Add(Rawdata[j * 610 + m * 6 + 6 + 0]);//origin
                            Pressure_data.Add(Rawdata[j * 610 + m * 6 + 6 + 1]);
                            //Pressure_data.Add(Rawdata[j * 610 + m * 6 + 6 + 2]);//filter
                            //Pressure_data.Add(Rawdata[j * 610 + m * 6 + 6 + 3]);
                            //Pressure_data.Add(Rawdata[j * 610 + m * 6 + 6 + 4]);//nodirect
                            //Pressure_data.Add(Rawdata[j * 610 + m * 6 + 6 + 5]);  
                        }
                    }
                    Depth_data.Add(Rawdata[6 + 610 * 9 + 600 + 0]);//HookLoad
                    Depth_data.Add(Rawdata[6 + 610 * 9 + 600 + 1]);
                    Depth_data.Add(Rawdata[6 + 610 * 9 + 600 + 4]);//BPI
                    Depth_data.Add(Rawdata[6 + 610 * 9 + 600 + 5]);
                    Rawdata.RemoveRange(0,6112);
                    mark = CheckData(Rawdata.ToArray());
                    Trace.WriteLine("\r\n");
                    Trace.WriteLine(">>>>>>>>>> mark2=" + mark + "<<<<<<<<<<<<<");

                    if(Depth_data.Count>=4)// 8 bytes as a row to callback
                    {
                        Depthdatatemp.Add(Depth_data[0]);
                        Depthdatatemp.Add(Depth_data[1]);
                        Depthdatatemp.Add(Depth_data[2]);
                        Depthdatatemp.Add(Depth_data[3]);
                        Depth_data.RemoveRange(0, 4);
                        GetData(Depthdatatemp.ToArray(),2);
                        Depthdatatemp.Clear();
                        Depthdatatemp.Add((byte)'D');//"DD"
                        Depthdatatemp.Add((byte)'D');
                    }
                    if (Pressure_data.Count >= 4000)                 //802个字节为一组进行回调
                    {   
                        for(int n=0;n<400;n++)
                        {
                            Pressuredatatemp.Add(Pressure_data[10 * n ]);
                            Pressuredatatemp.Add(Pressure_data[10 * n + 1]);
                        }
                        Pressure_data.RemoveRange(0, 4000);
                        GetData(Pressuredatatemp.ToArray(),1);
                        Pressuredatatemp.Clear();
                        Pressuredatatemp.Add(0x53);//"SS"
                        Pressuredatatemp.Add(0x53);
                    }
                    if (Rawdata.Count< 6112)
                    {
                        break;
                    }

                    //i += 9;
                    //if (i > receive.Length - 10)
                    //{
                    //    break;
                    //}
                        //for (int k = 0; i <= receive.Length - 10 && k < 25; i += 10, k++)
                        //{
                        //    Array.Copy(receive, i, tempNO, 0, 10);
                        //    Array.Copy(tempNO, 2, temp, 0, 2);

                        //    Pressuredatatemp.Add(temp[0]);
                        //    Pressuredatatemp.Add(temp[1]);
                        //    //System.Diagnostics.Trace.Write(string.Format("{0:X2} {0:X2} ", temp[0], temp[1]));
                        //    //t++;
                        //    //if (t % 16 == 0)
                        //    //{
                        //    //    System.Diagnostics.Trace.Write("\n");
                        //    //}
                        //    if (Pressuredatatemp.Count >= 802)                 //802个字节为一组进行回调
                        //    {
                        //        GetData(Pressuredatatemp.ToArray());
                        //        Pressuredatatemp.Clear();
                        //        Pressuredatatemp.Add(0x53);
                        //        Pressuredatatemp.Add(0x53);                   //"ss"
                        //    }
                        //}
                    //System.Diagnostics.Trace.Write("\n");
                    //System.Diagnostics.Trace.Write("\n");
                    //i += 9;
                    //if (i > receive.Length - 10)
                    //{
                    //    break;
                    //}
                }
            }
            else
            {
                ;//找不到帧头时应错位继续寻找  亮
            }
        }

        //private byte[] Standard = new byte[] { 0x55, 0xEE, 0x00, 0x01, 0X85, 0xFA };
        private byte[] Standard = new byte[] { 0x55, 0x55, 0x55, 0x55};//亮
        /// <summary>
        /// check liya data receive
        /// </summary>
        private int CheckData(byte[] Receive)//亮   找数据帧头
        {
            if (Receive.Length < Standard.Length)
            {
                return -1;
            }

            for (int i = 0; i < Receive.Length - Standard.Length; i++)
            {
                if (Receive[i].Equals(0x55) && Receive[i + 1].Equals(0x55) && Receive[i + 2].Equals(0x55) && Receive[i + 3].Equals(0x55))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
