using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class HttpClientUtil
    {
        // Token: 0x06000006 RID: 6 RVA: 0x00002110 File Offset: 0x00000310
        public string postSFAPI(string url, string xml, string verifyCode)
        {
            string result = "";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{0}={1}", "xml", xml);
            stringBuilder.Append("&");
            stringBuilder.AppendFormat("{0}={1}", "verifyCode", verifyCode);
            byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
            httpWebRequest.ContentLength = (long)bytes.Length;
            using (Stream requestStream = httpWebRequest.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
            }
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();
            using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        public string postSFAPI(string url,List<KeyValuePair<string,string>> datas)
        {
            string result = "";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            //StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.AppendFormat("{0}={1}", "xml", xml);
            //stringBuilder.Append("&");
            //stringBuilder.AppendFormat("{0}={1}", "verifyCode", verifyCode);

            var data = string.Join("&", datas.Select(t => $"{t.Key}={t.Value}"));
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            httpWebRequest.ContentLength = (long)bytes.Length;
            using (Stream requestStream = httpWebRequest.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
            }
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();
            using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
    }
}
