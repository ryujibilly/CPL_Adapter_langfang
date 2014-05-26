using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace CPLAdapter
{
    class ConfigInfo
    {
        /// <summary>
        /// 本机UDP接收深度端口号
        /// </summary>
        public int LocalDeptPort=9001;
        /// <summary>
        /// 本机接收CPL命令端口号
        /// </summary>
        public int LocalCmdRecvPort=9002;
        /// <summary>
        /// CMS端wits接收IP
        /// </summary>
        public string CMSWitsRecvIP="192.168.1.230";
        /// <summary>
        /// CMS端wits接收端口号
        /// </summary>
        public int CMSWitsRecvPort=9003;
        /// <summary>
        /// CPL端WITS服务端口号
        /// </summary>
        public int CPLWitsPort=9004;
        /// <summary>
        /// CPL端WITS服务IP
        /// </summary>
        public string CPLWitsIP="192.168.1.231";
        /// <summary>
        /// 地面箱串口号
        /// </summary>
        public string ComPortNum = "COM2";
        /// <summary>
        /// 串口通讯波特率
        /// </summary>
        public int BaudRate = 460800;
        /// <summary>
        /// 司显连接方式 1.有线 0 无线
        /// </summary>
        public int DisplayMode = 0;
        /// <summary>
        /// 司显节点设备序列号
        /// </summary>
        public string DeviceSN = "00000000";
        /// <summary>
        /// 司显节点网络密钥
        /// </summary>
        public string NetKey = "00000000";
        /// <summary>
        /// 配置信息是否有效
        /// </summary>
        public bool IsValue = false;

    }
    class Config
    {
        /// <summary>
        /// 全局配置信息
        /// </summary>
        public static ConfigInfo CfgInfo = new ConfigInfo();

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <returns></returns>
        public static bool SaveConfig()
        {
            bool bIsSave = false;
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);
                //LocalDeptPort
                XmlElement eleLocalDeptPort = doc.CreateElement("LocalDeptPort");
                XmlText txtLocalDeptPort = doc.CreateTextNode(CfgInfo.LocalDeptPort.ToString());
                //LocalCmdRecvPort
                XmlElement eleLocalCmdRecvPort = doc.CreateElement("LocalCmdRecvPort");
                XmlText txtLocalCmdRecvPort = doc.CreateTextNode(CfgInfo.LocalCmdRecvPort.ToString());
                //CMSWitsRecvIP
                XmlElement eleCMSWitsRecvIP = doc.CreateElement("CMSWitsRecvIP");
                XmlText txtCMSWitsRecvIP = doc.CreateTextNode(CfgInfo.CMSWitsRecvIP);
                //CMSWitsRecvPort
                XmlElement eleCMSWitsRecvPort = doc.CreateElement("CMSWitsRecvPort");
                XmlText txtCMSWitsRecvPort = doc.CreateTextNode(CfgInfo.CMSWitsRecvPort.ToString());
                //CPLWitsPort
                XmlElement eleCPLWitsPort = doc.CreateElement("CPLWitsPort");
                XmlText txtCPLWitsPort = doc.CreateTextNode(CfgInfo.CPLWitsPort.ToString());
                //CPLWitsIP
                XmlElement eleCPLWitsIP = doc.CreateElement("CPLWitsIP");
                XmlText txtCPLWitsIP = doc.CreateTextNode(CfgInfo.CPLWitsIP.ToString());
                //ComPortNum
                XmlElement eleComPortNum = doc.CreateElement("ComPortNum");
                XmlText txtComPortNum = doc.CreateTextNode(CfgInfo.ComPortNum);
                //BaudRate
                XmlElement eleBaudRate = doc.CreateElement("BaudRate");
                XmlText txtBaudRate = doc.CreateTextNode(CfgInfo.BaudRate.ToString());
                //DisplayMode
                XmlElement eleDisplayMode = doc.CreateElement("DisplayMode");
                XmlText txtDisplayMode = doc.CreateTextNode(CfgInfo.DisplayMode.ToString());

                XmlNode newElem = doc.CreateNode("element", "config", "");
                newElem.AppendChild(eleLocalDeptPort);
                newElem.LastChild.AppendChild(txtLocalDeptPort);

                newElem.AppendChild(eleLocalCmdRecvPort);
                newElem.LastChild.AppendChild(txtLocalCmdRecvPort);

                newElem.AppendChild(eleCMSWitsRecvIP);
                newElem.LastChild.AppendChild(txtCMSWitsRecvIP);

                newElem.AppendChild(eleLocalDeptPort);
                newElem.LastChild.AppendChild(txtLocalDeptPort);

                newElem.AppendChild(eleCMSWitsRecvPort);
                newElem.LastChild.AppendChild(txtCMSWitsRecvPort);

                newElem.AppendChild(eleCPLWitsPort);
                newElem.LastChild.AppendChild(txtCPLWitsPort);

                newElem.AppendChild(eleCPLWitsIP);
                newElem.LastChild.AppendChild(txtCPLWitsIP);

                newElem.AppendChild(eleComPortNum);
                newElem.LastChild.AppendChild(txtComPortNum);

                newElem.AppendChild(eleBaudRate);
                newElem.LastChild.AppendChild(txtBaudRate);

                newElem.AppendChild(eleDisplayMode);
                newElem.LastChild.AppendChild(txtDisplayMode);

                XmlElement root = doc.CreateElement("config");
                root.AppendChild(newElem);
                doc.AppendChild(root);
                doc.Save("config.xml");
                
                bIsSave = true;
            }
            catch 
            {
                bIsSave = false;
            }
            return bIsSave;
        }
        
        /// <summary>
        /// 获取配置信息 
        /// </summary>
        /// <returns></returns>
        public static bool GetConfig()
        {
            bool bIsGet = false;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("config.xml");

                CfgInfo.LocalDeptPort=int.Parse(doc.SelectSingleNode("//LocalDeptPort").InnerText);
                CfgInfo.LocalCmdRecvPort = int.Parse(doc.SelectSingleNode("//LocalCmdRecvPort").InnerText);
                CfgInfo.CMSWitsRecvIP = doc.SelectSingleNode("//CMSWitsRecvIP").InnerText;
                CfgInfo.CMSWitsRecvPort = int.Parse(doc.SelectSingleNode("//CMSWitsRecvPort").InnerText);
                CfgInfo.CPLWitsPort = int.Parse(doc.SelectSingleNode("//CPLWitsPort").InnerText);
                CfgInfo.CPLWitsIP = doc.SelectSingleNode("//CPLWitsIP").InnerText;
                CfgInfo.ComPortNum = doc.SelectSingleNode("//ComPortNum").InnerText;
                CfgInfo.BaudRate = int.Parse(doc.SelectSingleNode("//BaudRate").InnerText);
                CfgInfo.DisplayMode = int.Parse(doc.SelectSingleNode("//DisplayMode").InnerText);


                XmlDocument xmldoc = new XmlDocument();
                if (File.Exists("NodeSettings.xml"))
                {
                    xmldoc.Load("NodeSettings.xml");
                    XmlNodeList xmlnode = xmldoc.SelectSingleNode("Settings").ChildNodes;
                    foreach (XmlElement element in xmlnode)
                    {
                        if (element.Attributes["purpose"].Value == "driller")
                        {
                            //司显属性值取得
                            CfgInfo.DeviceSN = element.Attributes["deviceSN"].Value;
                            CfgInfo.NetKey = element.Attributes["netKey"].Value;
                            break;
                        }
                    }
                }

                bIsGet = true;
            }
            catch
            {
                bIsGet = false;
            }
            return bIsGet;   
        }
        
    }
}