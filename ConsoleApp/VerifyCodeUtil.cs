using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleApp
{
    internal class VerifyCodeUtil
    {
        // Token: 0x06000008 RID: 8 RVA: 0x00002234 File Offset: 0x00000434
        public static string loadFile(string fileName)
        {
            string result;
            try
            {
                StreamReader streamReader = new StreamReader(fileName, Encoding.Default);
                StringBuilder stringBuilder = new StringBuilder();
                string value;
                while ((value = streamReader.ReadLine()) != null)
                {
                    stringBuilder.Append(value);
                }
                result = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw new IOException(ex.StackTrace);
            }
            return result;
        }

        // Token: 0x06000009 RID: 9 RVA: 0x0000229C File Offset: 0x0000049C
        public static string md5EncryptAndBase64(string str)
        {
            return VerifyCodeUtil.encodeBase64(VerifyCodeUtil.md5Encrypt(str));
        }

        // Token: 0x0600000A RID: 10 RVA: 0x000022BC File Offset: 0x000004BC
        private static byte[] md5Encrypt(string encryptStr)
        {
            byte[] result;
            try
            {
                MD5 md = new MD5CryptoServiceProvider();
                result = md.ComputeHash(Encoding.Default.GetBytes(encryptStr));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
            return result;
        }

        // Token: 0x0600000B RID: 11 RVA: 0x00002304 File Offset: 0x00000504
        private static string encodeBase64(byte[] b)
        {
            return Convert.ToBase64String(b);
        }

        public static string getMsgDigest(string msgData, String timeStamp, String md5Key)
        {
            //return VerifyCodeUtil.encodeBase64(VerifyCodeUtil.md5Encrypt(str));
            String msgDigest = VerifyCodeUtil.md5EncryptAndBase64(UrlEncode(msgData + timeStamp + md5Key, Encoding.UTF8));

            return msgDigest;
        }

        private static string UrlEncode(string str,Encoding encoding)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in str)
            {
                string t = HttpUtility.UrlEncode(c.ToString(), encoding);
                if (t.Length > 1)
                {
                    builder.Append(t.ToUpper());
                }
                else
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }
    }
}
