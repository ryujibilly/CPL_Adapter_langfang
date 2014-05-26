using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPLAdapter
{
    class LineProtocol
    {
        public byte[] GetBytesForDisplay(string strDataTag, string strData)
        {
            //0x20	0x49	0x4E	0x43	0x3D
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
                DataList.Add(0x32);
                DataList.Add(0x2A);
                DataList.Add(0x20);
                foreach (char chr in toolface)
                {
                    int temp1 = Convert.ToInt32(chr);
                    DataList.Add(BitConverter.GetBytes(temp1)[0]);
                }
                for (int i = 0; i < 5 - len; i++)
                {
                    DataList.Add(0x20);
                }
                DataList.Add(0x3a);
                DataList.Add(0x32);
                DataList.Add(0x30);
                DataList.Add(0x0a);              //end
            }
            else if (strDataTag == "17")                                      // gaobian toolface
            {
                string toolface = strData.Substring(4);
                int len = toolface.Length;
                DataList.Add(0x20);             // G?: 
                DataList.Add(0x47);
                DataList.Add(0x32);
                DataList.Add(0x3a);
                DataList.Add(0x20);
                foreach (char chr in toolface)
                {
                    int temp1 = Convert.ToInt32(chr);
                    DataList.Add(BitConverter.GetBytes(temp1)[0]);
                }
                for (int i = 0; i < 5 - len; i++)
                {
                    DataList.Add(0x20);
                }
                DataList.Add(0x3a);
                DataList.Add(0x32);
                DataList.Add(0x30);  
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
            //同步头三字节
            listtemp.Add(0xFF);
            listtemp.Add(0xf0);
            listtemp.Add(0x12);
            //地址0一字节
            listtemp.Add(0x00);

            //数据长度两字节
            listtemp.Add(0x00);
            listtemp.Add(0x0D);

            //数据14字节
            listtemp.AddRange(list);
            return ArrToByte(listtemp);
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
