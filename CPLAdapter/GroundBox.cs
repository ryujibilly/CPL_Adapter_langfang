using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO.Ports;
using System.Diagnostics;
using System.Collections;
using System.Linq;

namespace CPLAdapter
{
    public delegate void DataRecvCallback(byte[] datalist); 

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
            BoxTimer.Start(100, true);
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
        private byte[] GetLatestData = new byte[] { 0xaa, 0x55, 0x01, 0x00, 0x05, 0x00, 0x02, 0x88, 0x01};
        /// <summary>
        /// 定时器回调函数
        /// </summary>
        private void Callback(uint uTimerID, uint uMsg, UIntPtr dwUser, UIntPtr dw1, UIntPtr dw2)
        {
            //Callback from the MMTimer API that fires the Timer event. Note we are in a different thread here
            TimerCount++;
            if (TimerCount == 5)       //read liya
            {
                SendByte(GetLatestData);
                TimerCount = 0;
            }
            else  //send dev azi toolface
            {
                lock (objDisplay)
                {
                    if (Displaydata.Count != 0)
                    {
                        SendByte(Displaydata[0]);
                        Displaydata.RemoveAt(0);
                    }
                }
            }
        }
        
        private List<byte[]> Displaydata = new List<byte[]>();

        private object objDisplay = new object();
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
                byte[] changeMode = new byte[] { 0xaa, 0x55, 0x01, 0x00, 0x01, 0x00, 0x00, 0x48, 0x01 };
                SendByte(changeMode);
                Thread.Sleep(200);
                Pressuredatatemp.Add(0x53);
                Pressuredatatemp.Add(0x53); 
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
        /// liya data
        /// </summary>
        private List<byte> Pressuredatatemp = new List<byte>();
        /// <summary>
        /// receive data
        /// </summary>
        private void Communication_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(10);
                                
            byte[] receive = new byte[Communication.BytesToRead];
            Communication.Read(receive, 0, receive.Length);
           
            if (receive.Length < 259)
            {
                return;
            }
            //print(receive);
            if (CheckData(receive))
            {
                byte[] tempNO = new byte[10];
                byte[] temp = new byte[2];

                uint t = 0;
                int i = 6;
                while (true)
                {
                    for (int k=0; i <= receive.Length - 10 && k<25; i += 10,k++)
                    {
                        Array.Copy(receive, i, tempNO, 0, 10);
                        Array.Copy(tempNO, 2, temp, 0, 2);

                        Pressuredatatemp.Add(temp[0]);
                        Pressuredatatemp.Add(temp[1]);

                        //System.Diagnostics.Trace.Write(string.Format("{0:X2} {0:X2} ", temp[0], temp[1]));
                        //t++;
                        //if (t % 16 == 0)
                        //{
                        //    System.Diagnostics.Trace.Write("\n");
                        //}

                        if (Pressuredatatemp.Count >= 802)                 //802个字节为一组进行回调
                        {
                            GetData(Pressuredatatemp.ToArray());
                            Pressuredatatemp.Clear();
                            Pressuredatatemp.Add(0x53);
                            Pressuredatatemp.Add(0x53);                   //"ss"
                        }
                    }
                    //System.Diagnostics.Trace.Write("\n");
                    //System.Diagnostics.Trace.Write("\n");
                    i += 9;
                    if (i > receive.Length - 10)
                    {
                        break;
                    }
                }
            }
        }

        private byte[] Standard = new byte[] { 0x55, 0xEE, 0x00, 0x01, 0X85, 0xFA };
        /// <summary>
        /// check liya data receive
        /// </summary>
        private bool CheckData(byte[] Receive)
        {
            if (Receive.Length < Standard.Length)
            {
                return false;
            }

            for (int i = 0; i < Standard.Length; i++)
            {
                if (Receive[i] != Standard[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
