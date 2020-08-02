using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class CallExpressServiceTools
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        private CallExpressServiceTools()
        {
        }

        // Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
        public static CallExpressServiceTools getInstance()
        {
            return CallExpressServiceTools.Nested.tools;
        }

        // Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
        public string callSfExpressServiceByCSIM(string reqURL, string reqXML, string clientCode, string checkword)
        {
            string verifyCode = VerifyCodeUtil.md5EncryptAndBase64(reqXML + checkword);
            return CallExpressServiceTools.querySFAPIservice(reqURL, reqXML, verifyCode);
        }

        // Token: 0x06000004 RID: 4 RVA: 0x000020A4 File Offset: 0x000002A4
        public static string querySFAPIservice(string url, string xml, string verifyCode)
        {
            HttpClientUtil httpClientUtil = new HttpClientUtil();
            bool flag = url == null;
            if (flag)
            {
                url = "http://bsp-oisp.sf-express.com/bsp-oisp/sfexpressService";
            }
            try
            {
                return httpClientUtil.postSFAPI(url, xml, verifyCode);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return null;
        }

        // Token: 0x04000001 RID: 1
        private static readonly object Padlock = new object();

        // Token: 0x02000005 RID: 5
        private class Nested
        {
            // Token: 0x0600000D RID: 13 RVA: 0x00002327 File Offset: 0x00000527
            static Nested()
            {
                CallExpressServiceTools.Nested.tools = new CallExpressServiceTools();
            }

            // Token: 0x04000002 RID: 2
            internal static CallExpressServiceTools tools = null;
        }

        public string callSfExpressServiceByJSon(string reqURL,string timeStamp, string serviceCode, string msgData, string clientCode, string checkword)
        {
            string verifyCode = VerifyCodeUtil.getMsgDigest(msgData,timeStamp,checkword);
            List<KeyValuePair<string, string>> paras = new List<KeyValuePair<string, string>>();
            paras.Add(new KeyValuePair<string, string>("partnerID", clientCode));
            paras.Add(new KeyValuePair<string, string>("requestID", Guid.NewGuid().ToString("N")));
            paras.Add(new KeyValuePair<string, string>("serviceCode", serviceCode));
            paras.Add(new KeyValuePair<string, string>("timestamp", timeStamp));
            paras.Add(new KeyValuePair<string, string>("msgData", msgData));
            paras.Add(new KeyValuePair<string, string>("msgDigest", verifyCode));
            return CallExpressServiceTools.querySFAPIserviceByJSon(reqURL, paras);
        }
        public static string querySFAPIserviceByJSon(string url, List<KeyValuePair<string, string>> datas)
        {
            HttpClientUtil httpClientUtil = new HttpClientUtil();
            try
            {
                return httpClientUtil.postSFAPI(url, datas);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return null;
        }

        /*
         示例仅供参考,详情请下载对应的SDK
参考链接:http://qiao.sf-express.com/pages/helpCenter/download.html

示例代码:
String CALL_URL_BOX ="http://sfapi-sbox.sf-express.com/sfexpressService";

CallExpressServiceTools client=CallExpressServiceTools.getInstance();
Map<String, String> params = new HashMap<String, String>();

params.put("partnerID", CLIENT_CODE);  // 顾客编码 ，对应丰桥上获取的clientCode

params.put("requestID", UUID.randomUUID().toString().replace("-", ""));

params.put("serviceCode",testService.getCode());// 接口服务码

params.put("timestamp", timeStamp);
    
params.put("msgData", msgData);
      
params.put("msgDigest", client.getMsgDigest(msgData,timeStamp,CHECK_WORD));//数据签名
        
String result = HttpClientUtil.post(CALL_URL_BOX, params);
b

         */
    }
}
