using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;

namespace GLAS_Adapter
{
    //public delegate void AdpDispose(Adapter adp);
    public class Adapter:Component
    {
        public Form1 Owner { get; set; }//Adapter's Owner-"Form1 Object"
        public GroundBox gBox { get; set; }
        public UdpServer udpServer { get; set; }
        public UdpClient udpClient { get; set; }
        public TcpServer tcpServer { get; set; }
        public TcpClient tcpClient { get; set; }
        private DepType TypeofDepth { get; set; }
        private Random rand { get; set; }

        private static readonly string strSndTemplate = "&&\r\n0121{0}\r\n!!\r\n";//WITS's SPP Headbytes
        public Adapter(string port,DepType dt)
        {
            gBox = new GroundBox(DataRecvCallback,port,Config.CfgInfo.BaudRate, Config.CfgInfo.DeviceSN, Config.CfgInfo.NetKey);
            TypeofDepth = dt;
            udpServer = new UdpServer(Config.CfgInfo.LocalDeptPort,dt);
            tcpServer = new TcpServer(Config.CfgInfo.LocalCmdRecvPort,dt);
            udpClient = new UdpClient(Config.CfgInfo.CMSWitsRecvIP, Config.CfgInfo.CMSWitsRecvPort);
            tcpServer.Owner = this;
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
        /// <summary>
        /// 数据接收回调函数
        /// </summary>
        /// <param name="datalist">数据数组</param>
        /// <param name="dataType">数据种类:1:spp;2:depth.</param>
        private void DataRecvCallback(byte[] datalist,int dataType)
        {
            if (dataType == 1)
            { 
                CommonData.SaveSppQueue(datalist);
                //int i=rand.Next(1,398);
                double Origin=(datalist[102]+datalist[103]*256)/2.4;
                double ElectValue=(Origin-10485)*16/41943+4.0;
                double StandpipePressure=(ElectValue-4)*42/16*1000;
                byte[] sndBytes=ASCIIEncoding.ASCII.GetBytes(string.Format(strSndTemplate,StandpipePressure.ToString("0.00")));
                udpClient.Send(sndBytes,sndBytes.Length);
                Trace.WriteLine(">>>>"+DateTime.Now.ToLongTimeString()+"<<<<");
                Trace.WriteLine("\r\n 原始值:"+Origin.ToString()+"\t\t 电流值:"+ElectValue.ToString()+"\t\t 立管压力:"+StandpipePressure.ToString());
            }
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
