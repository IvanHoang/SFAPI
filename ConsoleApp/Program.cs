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
            /*查询订单*/
            string respXml = Order_Search(reqURL, "EXP_RECE_SEARCH_ORDER_RESP", clientCode, checkword);
            /*下订单*/
            //string respXml = Order_Create(reqURL, "EXP_RECE_CREATE_ORDER", clientCode, checkword);
            /*订单取消*/
            //string respXml = Order_Cancel(reqURL, "EXP_RECE_UPDATE_ORDER", clientCode, checkword);
            /*订单筛选*/
            //string respXml = Order_Select(reqURL, "EXP_RECE_FILTER_ORDER_BSP", clientCode, checkword);
            /*查路由*/
            //string respXml = Order_Routes(reqURL, "EXP_RECE_SEARCH_ROUTES", clientCode, checkword);
            /*子单号*/
            //string respXml = Order_Suborder(reqURL, "EXP_RECE_GET_SUB_MAILNO", clientCode, checkword);
            /*查运费*/
            //string respXml = Order_Cost(reqURL, "EXP_RECE_QUERY_SFWAYBILL", clientCode, checkword);
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
        //订单搜索
        private static string Order_Search(string reqURL,string EspServiceCode, string clientCode, string checkword)
        {
            String msgData_Search = "{\"orderId\":\"TF2020010100001\",\"searchType\":\"1\",\"language\":\"zh-cn\"}";
            Console.WriteLine("请求报文：" + msgData_Search);
            CallExpressServiceTools tools = CallExpressServiceTools.getInstance();
            var timeStamp = DateTime.Now.Ticks.ToString();
            String respXml = tools.callSfExpressServiceByJSon(reqURL, timeStamp,
                EspServiceCode, msgData_Search, clientCode, checkword);
            return respXml;
        }
        //订单创建                                                        
        private static string Order_Create(string reqURL, string EspServiceCode, string clientCode, string checkword)
        {
            string msgData_Create= "{\"cargoDetails\":[{\"count\":2.365,\"unit\":\"个\",\"weight\":6.1,\"amount\":100.5111,\"currency\":\"HKD\",\"name\":\"护肤品1\",\"sourceArea\":\"CHN\"}],\"contactInfoList\":[{\"address\":\"广东省深圳市南山区软件产业基地11栋\",\"contact\":\"小曾\",\"contactType\":1,\"country\":\"CN\",\"postCode\":\"580058\",\"tel\":\"4006789888\"},{\"address\":\"广东省广州市白云区湖北大厦\",\"company\":\"顺丰速运\",\"contact\":\"小邱\",\"contactType\":2,\"country\":\"CN\",\"postCode\":\"580058\",\"tel\":\"18688806057\"}],\"language\":\"zh_CN\",\"orderId\":\"OrderNum20200612223\"}";
            Console.WriteLine("请求报文：" + msgData_Create);
            CallExpressServiceTools tools = CallExpressServiceTools.getInstance();
            var timeStamp = DateTime.Now.Ticks.ToString();
            string respXml = tools.callSfExpressServiceByJSon(reqURL,timeStamp,
                EspServiceCode, msgData_Create, clientCode, checkword);
            return respXml;
        }
        //订单取消
        private static string Order_Cancel(string reqURL, string EspServiceCode, string clientCode, string checkword)
        {
            string msgData_Cancel = "{\"dealType\":2,\"orderId\":\"BZL51054473992769999\",\"waybillNoInfoList\":[{\"waybillNo\":\"SF2000090670189\",\"waybillType\":1}]}";
            Console.WriteLine("请求报文：" + msgData_Cancel);
            CallExpressServiceTools tools = CallExpressServiceTools.getInstance();
            var timeStamp = DateTime.Now.Ticks.ToString();
            string respXml = tools.callSfExpressServiceByJSon(reqURL, timeStamp,
                EspServiceCode, msgData_Cancel, clientCode, checkword);
            return respXml;
        }
        //订单筛选
        private static string Order_Select(string reqURL, string EspServiceCode, string clientCode, string checkword)
        {
            string msgData_Select = "{\"contactInfos\":[{\"address\":\"创业总部基地B07二楼\",\"city\":\"天津市\",\"contactType\":2,\"country\":\"中国\",\"county\":\"武清区\",\"province\":\"天津市\",\"tel\":\"19851401196\"},{\"address\":\"测试订单，请不要接单\",\"city\":\"成都市\",\"contactType\":1,\"country\":\"中国\",\"county\":\"锦江区\",\"province\":\"四川省\"}],\"filterType\":1}";
            Console.WriteLine("请求报文：" + msgData_Select);
            CallExpressServiceTools tools = CallExpressServiceTools.getInstance();
            var timeStamp = DateTime.Now.Ticks.ToString();
            string respXml = tools.callSfExpressServiceByJSon(reqURL, timeStamp,
                EspServiceCode, msgData_Select, clientCode, checkword);
            return respXml;
        }

        //查路由
        private static string Order_Routes(string reqURL, string EspServiceCode, string clientCode, string checkword)
        {
            string msgData_Routes = "{	\"language\":\"0\",	\"trackingType\":\"1\",	\"trackingNumber\":[\"444003077898\",\"441003077850\"],	\"methodType\":\"1\"}";
            Console.WriteLine("请求报文：" + msgData_Routes);
            CallExpressServiceTools tools = CallExpressServiceTools.getInstance();
            var timeStamp = DateTime.Now.Ticks.ToString();
            string respXml = tools.callSfExpressServiceByJSon(reqURL, timeStamp,
                EspServiceCode, msgData_Routes, clientCode, checkword);
            return respXml;
        }

        //查子单号
        private static string Order_Suborder(string reqURL, string EspServiceCode, string clientCode, string checkword)
        {
            string msgData_Suborder = "{	\"orderId\":\"F2_20200408122346\",	\"parcelQty\":2}";
            Console.WriteLine("请求报文：" + msgData_Suborder);
            CallExpressServiceTools tools = CallExpressServiceTools.getInstance();
            var timeStamp = DateTime.Now.Ticks.ToString();
            string respXml = tools.callSfExpressServiceByJSon(reqURL, timeStamp,
                EspServiceCode, msgData_Suborder, clientCode, checkword);
            return respXml;
        }

        //查运费
        private static string Order_Cost(string reqURL, string EspServiceCode, string clientCode, string checkword)
        {
            string msgData_Cost = "{\"type\":\"1\",\"waybillNo\":\"\",\"orderId\":\"LFCN0007556700\"}";
            Console.WriteLine("请求报文：" + msgData_Cost);
            CallExpressServiceTools tools = CallExpressServiceTools.getInstance();
            var timeStamp = DateTime.Now.Ticks.ToString();
            string respXml = tools.callSfExpressServiceByJSon(reqURL, timeStamp,
                EspServiceCode, msgData_Cost, clientCode, checkword);
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
