using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GLAS_Adapter
{
    public sealed class Funcs
    {
        public static readonly Funcs _funcs = new Funcs();
        public void print(byte[] receive)
        {
            int c = 0;
            for (int i = 0; i < receive.Length; i++)
            {
                System.Diagnostics.Trace.Write(string.Format("{0:X2} ", receive[i]));
                c++;
                if (c == 20)
                {
                    System.Diagnostics.Trace.Write("\n");
                    c = 0;
                }
            }
            System.Diagnostics.Trace.Write("\r\n");
        }
        /// <summary>
        /// HexString 转 byte[]
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public byte[] HexStringToByteArray(String s)
        {
            try
            {
                if (s != null)
                {
                    char[] seperater = { ' ' };
                    String[] str = s.Split(seperater, StringSplitOptions.RemoveEmptyEntries);
                    byte[] b = new byte[str.LongLength];
                    for (long i = 0; i < str.LongLength; i++)
                    {
                        b[i] = byte.Parse(str[i], System.Globalization.NumberStyles.AllowHexSpecifier);
                    }
                    return b;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// byte[] 转 HexString
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string ByteArrayToHexString(byte[] data)
        {
            try
            {
                StringBuilder sb = new StringBuilder(data.Length * 3);
                foreach (byte b in data)
                    sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
                return sb.ToString().ToUpper();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
