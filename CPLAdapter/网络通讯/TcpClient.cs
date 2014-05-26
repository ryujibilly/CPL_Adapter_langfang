using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CPLAdapter
{
    class TcpClient
    {
        private IGroundBox IBox = null;
        /// <summary>
        /// 
        /// </summary>
        private UdpClient CMSUdpClient = null;
        /// <summary>
        /// 客户端tcp套接字
        /// </summary>
        private System.Net.Sockets.TcpClient SockClient = null;
        /// <summary>
        /// 服务器端口号
        /// </summary>
        private int ServerPort = 0;
        /// <summary>
        /// 服务器IP
        /// </summary>
        private string ServerHostName = null;
        /// <summary>
        /// 客户端线程
        /// </summary>
        private Thread ClientThread = null;
        /// <summary>
        /// 是否继续循环
        /// </summary>
        private bool IsContinue = true;
        /// <summary>
        /// 构造函数
        /// </summary>
        ///   <param name="strHostName">服务器IP或域名</param>
        /// <param name="svrPort">服务器的端口号</param>
        public TcpClient(string strCPLHostName, int svrCPLPort, string strCMSHostName, int srvCMSPort, IGroundBox iBox)
        {
            this.ServerHostName = strCPLHostName;
            this.ServerPort = svrCPLPort;
            this.IBox = iBox;
            CMSUdpClient = new UdpClient(strCMSHostName, srvCMSPort);
            ClientThread = new Thread(new ThreadStart(RcvThreadFunc));

        }
        /// <summary>
        /// 开始启动客户端接收
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            IsContinue = true;
            ClientThread.Start();
            return true;
        }
        /// <summary>
        /// 停止接收
        /// </summary>
        public void Stop()
        {
            IsContinue = false;
            //关闭套接字
            try
            {
                if (SockClient != null)
                {
                    SockClient.Close();
                }
            }
            catch { }

            try
            {

                if (CMSUdpClient != null)
                {
                    CMSUdpClient.Close();
                }
            }
            catch { }
            //关闭线程
            try
            {
                if (ClientThread != null)
                {
                    ClientThread.Abort();
                }
            }
            catch { }
        }
        /// <summary>
        /// 线程函数
        /// </summary>
        void RcvThreadFunc()
        {
            System.Diagnostics.Trace.WriteLine("TcpClient 线程启动！");
            //数据接收缓冲区 默认大小2k
            byte[] bytesRecv=new byte[2048];
            while (IsContinue)
            {
                //打开TCP并连接
                SockClient = new System.Net.Sockets.TcpClient();
                bool bIsConnect = false;
                try
                {
                    SockClient.Connect(ServerHostName, ServerPort);
                    bIsConnect = true;
                }
                catch 
                {
                    bIsConnect = false;
                }
                if (!bIsConnect)
                {
                    System.Diagnostics.Trace.WriteLine("TcpClient 无法连接CPL服务器！");
                    SockClient.Close();
                    SockClient = null;
                    Thread.Sleep(500);
                    continue;
                }

                //循环读取数据
                NetworkStream ns = SockClient.GetStream();
                while (IsContinue)                
                {
                    int rcvLen = 0;
                    try
                    {
                        rcvLen = ns.Read(bytesRecv, 0, bytesRecv.Length);
                        if (rcvLen <= 0)
                        {
                            ns.Close();
                            SockClient.Close();
                            break;
                        }
                    }
                    catch 
                    {
                        ns.Close();
                        SockClient.Close();
                        break;
                    }
                   
                    string strWIts=Encoding.ASCII.GetString(bytesRecv, 0, rcvLen);
                    System.Diagnostics.Trace.WriteLine(strWIts);

                    //收到数据后发送给GroundBox
                    if (IBox != null)
                    {
                        IBox.SaveData(strWIts);
                    }

                    //发送给CMS
                    CMSUdpClient.Send(bytesRecv, rcvLen);
                }
            }
        }
    }
}