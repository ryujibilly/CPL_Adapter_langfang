using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLAS_Adapter
{
    class WirelessProtocol
    {
        /// <summary>
        /// toolface no different for each time
        /// </summary>
        private int SignG = 2, SignM = 2;
        private string SNno;
        private string Keyno;

        public WirelessProtocol(string strNo,string strKey)
        {
            SNno = GetEightLenStr(strNo);
            Keyno = GetEightLenStr(strKey);
        }

        /// <summary>
        /// 确保字符串长度为8 不够前面加0 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string GetEightLenStr(string str)
        {
            int len = str.Length;
            if (len > 8)
            {
                return str.Substring(0, 8);
            }
            if (len < 8)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < (8 - len); i++)
                {
                    sb.Append('0');
                }
                sb.Append(str);
                return sb.ToString();
            }
            return str;
        }
        public byte[] GetBytesForDisplay(string strDataTag, string strData)
        {
            List<byte> DataList = new List<byte>();
            if (strDataTag == "13") //jingxie
            {
                string DEV = strData.Substring(4);
                int len = DEV.Length;
                DataList.Add(0x20);  // INC=  
                DataList.Add(0x49);
                DataList.Add(0x4e);
                DataList.Add(0x43);
                DataList.Add(0x3d);
                foreach (char chr in DEV)
                {
                    int temp1 = Convert.ToInt32(chr);
                    DataList.Add(BitConverter.GetBytes(temp1)[0]);
                }
                for (int i = 0; i < 8 - len; i++)
                {
                    DataList.Add(0x20);
                }
                DataList.Add(0x0a);             //end
            }
            else if (strDataTag == "14")//fangwei
            {
                string AZI = strData.Substring(4);
                int len = AZI.Length;
                DataList.Add(0x20);  // AZ =
                DataList.Add(0x41);
                DataList.Add(0x5a);
                DataList.Add(0x20);
                DataList.Add(0x3d);
                foreach (char chr in AZI)
                {
                    int temp1 = Convert.ToInt32(chr);
                    DataList.Add(BitConverter.GetBytes(temp1)[0]);
                }
                for (int i = 0; i < 8 - len; i++)
                {
                    DataList.Add(0x20);
                }
                DataList.Add(0x0a);             //end
            }
            else if (strDataTag == "16")                                      // cixing toolface          
            {
                string toolface = strData.Substring(4);
                int len = toolface.Length;
                DataList.Add(0x20);             // M?: 
                DataList.Add(0x4d);
                DataList.Add(BitConverter.GetBytes(SignM + 48)[0]);
                SignM++;                         //different for each time
                if (SignM == 10)
                {
                    SignM = 2;
                }
                DataList.Add(0x3a);
                DataList.Add(0x20);
                foreach (char chr in toolface)
                {
                    int temp1 = Convert.ToInt32(chr);
                    DataList.Add(BitConverter.GetBytes(temp1)[0]);
                }
                DataList.Add(0x52);             //R
                for (int i = 0; i < 7 - len; i++)
                {
                    DataList.Add(0x20);
                }
                DataList.Add(0x0a);             //end
            }
            else if (strDataTag == "17")                                      // gaobian toolface
            {
                string toolface = strData.Substring(4);
                int len = toolface.Length;
                DataList.Add(0x20);             // G?: 
                DataList.Add(0x47);
                DataList.Add(BitConverter.GetBytes(SignG + 48)[0]);
                SignG++;                         //different for each time
                if (SignG == 10)
                {
                    SignG = 2;
                }
                DataList.Add(0x3a);
                DataList.Add(0x20);
                foreach (char chr in toolface)
                {
                    int temp1 = Convert.ToInt32(chr);
                    DataList.Add(BitConverter.GetBytes(temp1)[0]);
                }
                DataList.Add(0x52);             //R
                for (int i = 0; i < 7 - len; i++)
                {
                    DataList.Add(0x20);
                }
                DataList.Add(0x0a);             //end
            }

            return GetDataList(DataList.ToArray());
        }
        /// <summary>
        /// make protocol
        /// </summary>
        private byte[] GetDataList(byte[] list)
        {
            //List<byte> listtemp = new List<byte>();
            ArrayList listtemp = new ArrayList();
            //命令头6字节
            listtemp.Add(0x7f);
            listtemp.Add(0xff);
            listtemp.Add(0xfe);
            listtemp.Add(0xfe);
            listtemp.Add(0xfd);
            listtemp.Add(0x5a);
            //协议长度1字节 恒为0x2a
            listtemp.Add(0x2a);
            //命令号1字节 恒为0xaa
            listtemp.Add(0xaa);
            //从机SN号8字节
            foreach (char chr in SNno)
            {
                int temp = Convert.ToInt32(chr);
                listtemp.Add(BitConverter.GetBytes(temp)[0]);
            }
            //网络密钥8字节
            foreach (char chr in Keyno)
            {
                int temp = Convert.ToInt32(chr);
                listtemp.Add(BitConverter.GetBytes(temp)[0]);
            }
            //数据包序号2字节
            listtemp.Add(0x18);    //serial number
            listtemp.Add(0x19);
            //数据14字节
            listtemp.AddRange(list);   //data
            //CRC两字节
            byte[] crc = crc16(listtemp);
            listtemp.Add(crc[1]);
            listtemp.Add(crc[0]);
            //listtemp.Add(0x20);    //signal strength
            return ArrToByte(listtemp);
        }
        ///CRC16校验算法,（低字节在前，高字节在后）ArrayList方法
        /// </summary>
        /// <param name="data">要校验的数组</param>
        /// <returns>返回校验结果，低字节在前，高字节在后</returns>
        private byte[] crc16(ArrayList data)
        {
            if (data.Count == 0)
                throw new Exception("调用CRC16校验算法,（低字节在前，高字节在后）时发生异常，异常信息：被校验的数组长度为0。");
            byte[] temdata = new byte[data.Count + 2];
            int xda, xdapoly;
            byte i, j, xdabit;
            xda = 0xFFFF;
            xdapoly = 0xA001;
            for (i = 0; i < data.Count; i++)
            {
                xda ^= Convert.ToInt32(data[i].ToString());
                for (j = 0; j < 8; j++)
                {
                    xdabit = (byte)(xda & 0x01);
                    xda >>= 1;
                    if (xdabit == 1)
                        xda ^= xdapoly;
                }
            }
            temdata = new byte[2] { (byte)(xda & 0xFF), (byte)(xda >> 8) };
            return temdata;
        }
        /// <summary>
        /// arraylist to byte[]
        /// </summary>
        private byte[] ArrToByte(ArrayList AList)
        {
            byte[] ByteList = new byte[AList.Count];
            for (int i = 0; i < AList.Count; i++)
            {
                ByteList[i] = Convert.ToByte(AList[i]);
            }
            return ByteList;
        }
    }
}
