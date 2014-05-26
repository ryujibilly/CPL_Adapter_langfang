using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPLAdapter
{
    class Adapter
    {
        private GroundBox gBox = null;
        UdpServer udpServer = null;
        TcpServer tcpServer = null;
        TcpClient tcpClient = null;
        public Adapter()
        {
            gBox = new GroundBox(DataRecvCallback, Config.CfgInfo.ComPortNum,Config.CfgInfo.BaudRate, Config.CfgInfo.DeviceSN, Config.CfgInfo.NetKey);
            //gBox.SaveData("&&\r\n071421.2\r\n!!\r\n");

            //byte[] b = new byte[802];
            //b[0] = (byte)'S';
            //b[1] = (byte)'S';
            //for (int i = 2; i <= 801; i++)
            //{
            //    b[i] = (byte)i;
            //}

            //for (int i = 0; i < 100; i++)
            //{
            //    CommonData.SaveQueueItem(b);
            //}
           
            udpServer = new UdpServer(Config.CfgInfo.LocalDeptPort);
            tcpServer = new TcpServer(Config.CfgInfo.LocalCmdRecvPort);
            tcpClient = new TcpClient(Config.CfgInfo.CPLWitsIP, Config.CfgInfo.CPLWitsPort, Config.CfgInfo.CMSWitsRecvIP, Config.CfgInfo.CMSWitsRecvPort,gBox);
        }
        private void DataRecvCallback(byte[] datalist)
        {
            //System.Diagnostics.Debug.WriteLine(datalist.Length + "->" + DateTime.Now.ToString());
            //return;
            CommonData.SaveQueueItem(datalist);
            return;

            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString());
            for (int i = 0; i < datalist.Length; i++)
            {

                System.Diagnostics.Debug.Write(string.Format("{0:X} ", datalist[i]));
                if (i % 16 == 0)
                {
                    System.Diagnostics.Debug.Write("\n");
                }
            }
            System.Diagnostics.Debug.Write("\n\n\n\n");
        }
        public string Start()
        {
            if (!gBox.Start())
            {
                return "打开串口出错!";
            }
            if (!udpServer.Start())
            {
                gBox.Stop();
                return "打开从CMS接收数据监听端口出错!";
            }
            if (!tcpServer.Start())
            {
                gBox.Stop();
                udpServer.Stop();
                return "打开命令接收数据出错!";
            }
            if (!tcpClient.Start())
            {
                gBox.Stop();
                udpServer.Stop();
                tcpServer.Stop();
                return "打开从CPL接收数据的wits线程出错!";
            }
            return null;
        }
        public void Stop()
        {
            gBox.Stop();
            udpServer.Stop();
            tcpServer.Stop();
            tcpClient.Stop();
        }
    }
}
