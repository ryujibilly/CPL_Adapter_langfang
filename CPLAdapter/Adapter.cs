using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GLAS_Adapter
{
    //public delegate void AdpDispose(Adapter adp);
    public class Adapter:Component
    {
        public Form1 Owner { get; set; }//Adapter's Owner-"Form1 Object"
        public GroundBox gBox { get; set; }
        public UdpServer udpServer { get; set; }
        public TcpServer tcpServer { get; set; }
        public TcpClient tcpClient { get; set; }
        private DepType TypeofDepth { get; set; }
        public Adapter(string port,DepType dt)
        {
            gBox = new GroundBox(DataRecvCallback,port,Config.CfgInfo.BaudRate, Config.CfgInfo.DeviceSN, Config.CfgInfo.NetKey);
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
            TypeofDepth = dt;
            udpServer = new UdpServer(Config.CfgInfo.LocalDeptPort);
            tcpServer = new TcpServer(Config.CfgInfo.LocalCmdRecvPort,dt);
            tcpServer.Owner = this;
        }
        public void AdpDispose()
        {
            try
            {
                if (this.Owner.adp != null)
                {
                    //this.Owner.adp.Stop();
                    this.Owner.adpRun = false;
                    this.Owner.adp.Dispose();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
        public void BeginWork()
        {
            this.gBox.BeginWork();
        }
        private void DataRecvCallback(byte[] datalist,int dataType)//1:spp;2:depth.
        {
            if (dataType == 1)
                CommonData.SaveSppQueue(datalist);
            if (dataType == 2)
                CommonData.SaveDepthQueue(datalist);
            return;
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
            return null;
        }
        public void Stop()
        {
            gBox.Stop();
            udpServer.Stop();
            tcpServer.Stop();
        }
    }
}
