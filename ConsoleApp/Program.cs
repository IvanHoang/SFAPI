using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main0(string[] args)
        {
            //将callSfCsimExpressRequest文件下报文放于{program_}/bin/Debug/netcoreapp2.1 文件夹下

            //String path = "1.order.txt";//下订单
            String path = "2.order_query.txt";//查订单
            //string path = "3.order_confirm.txt";// 订单取消
            //String path = "4.order_filter.txt";//订单筛选
            //String path = "5.route_queryByMailNo.txt";//路由查询-通过运单号
            //String path = "5.route_queryByOrderNo.txt";//路由查询-通过订单号
            //String path = "7.orderZD.txt"; //子单号申请
            String reqXml = Read(path);

            //String reqURL = "https://bsp-oisp.sf-express.com/bsp-oisp/sfexpressService";
            string reqURL = "https://sfapi-sbox.sf-express.com/std/service";
            String clientCode = "YCwS0WdQ";//此处替换为您在丰桥平台获取的顾客编码
            String checkword = "14WkevNcMHtXrwo3f322lwrz7RfWc7nX";//此处替换为您在丰桥平台获取的校验码            
            String myReqXML = reqXml.Replace("SLKJ2019", clientCode);
            Console.WriteLine("请求报文：" + myReqXML);

            CallExpressServiceTools tools = CallExpressServiceTools.getInstance();
            String respXml = tools.callSfExpressServiceByCSIM(reqURL, myReqXML, clientCode, checkword);

            if (respXml != null)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("返回报文: " + respXml);
                Console.WriteLine("--------------------------------------");
                Console.ReadKey(true);
            }

        }
        static void Main(string[] args)
        {
            //String reqURL = "https://bsp-oisp.sf-express.com/bsp-oisp/sfexpressService";
            string reqURL = "https://sfapi-sbox.sf-express.com/std/service";
            String clientCode = "YCwS0WdQ";//此处替换为您在丰桥平台获取的顾客编码
            String checkword = "14WkevNcMHtXrwo3f322lwrz7RfWc7nX";//此处替换为您在丰桥平台获取的校验码            
            string respXml = Order_Search(reqURL, "EXP_RECE_SEARCH_ORDER_RESP", clientCode, checkword);

            /*
             * EXP_RECE_CREATE_ORDER; //下订单
             EXP_RECE_SEARCH_ORDER_RESP; //查订单
             EXP_RECE_UPDATE_ORDER;//订单取消
             EXP_RECE_FILTER_ORDER_BSP;//订单筛选
             EXP_RECE_SEARCH_ROUTES;//查路由
             EXP_RECE_GET_SUB_MAILNO;//子单号
             EXP_RECE_QUERY_SFWAYBILL;//查运费
             */

            if (respXml != null)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("返回报文: " + respXml);
                Console.WriteLine("--------------------------------------");
                Console.ReadKey(true);
            }

        }

        private static string Order_Search(string reqURL,string EspServiceCode, string clientCode, string checkword)
        {
            String msgData_查 = "{\"orderId\":\"TF2020010100001\",\"searchType\":\"1\",\"language\":\"zh-cn\"}";
            Console.WriteLine("请求报文：" + msgData_查);

            CallExpressServiceTools tools = CallExpressServiceTools.getInstance();
            var timeStamp = DateTime.Now.Ticks.ToString();
            String respXml = tools.callSfExpressServiceByJSon(reqURL, timeStamp,
                EspServiceCode, msgData_查, clientCode, checkword);
            return respXml;
        }

        public static String Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);

            StringBuilder builder = new StringBuilder();
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                builder.Append(line);
            }
            return builder.ToString();
        }
    }
}
