using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Xml;

namespace ECommerce.Controllers
{
    public class WeChatController : Controller
    {
        private static readonly string token = "weixintest";

        [HttpGet]
        public ActionResult Bot(string signature, string timestamp, string nonce, string echostr)
        {
            string result = "verify failed";

            if (CheckSignature(signature, timestamp, nonce))
            {
                result = echostr;
            }

            return Content(result);
        }

        [HttpPost]
        public ActionResult Bot()
        {
            Wxmessage wx = GetWxMessage();

            string result = SendTextMessage(wx, "你好");

            return this.Content(result);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        private static bool CheckSignature(string signature, string timestamp, string nonce)
        {
            string[] arr = { token, timestamp, nonce };

            // 将token、timestamp、nonce三个参数进行字典序排序
            Array.Sort<string>(arr);

            StringBuilder content = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                content.Append(arr[i]);
            }

            string tmpStr = SHA1_Encrypt(content.ToString());

            // 将sha1加密后的字符串可与signature对比，标识该请求来源于微信  
            return string.Compare(tmpStr, signature, StringComparison.CurrentCultureIgnoreCase) == 0;
        }


        /// <summary>
        /// 使用缺省密钥给字符串加密
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        private static string SHA1_Encrypt(string sourceString)
        {
            byte[] strRes = Encoding.Default.GetBytes(sourceString);
            HashAlgorithm iSha = new SHA1CryptoServiceProvider();
            strRes = iSha.ComputeHash(strRes);

            StringBuilder enText = new StringBuilder();
            foreach (byte iByte in strRes)
            {
                enText.AppendFormat("{0:x2}", iByte);
            }

            return enText.ToString();
        }

        /// <summary>
        /// 发送文字消息
        /// </summary>
        /// <param name="wx">获取的收发者信息</param>
        /// <param name="content">笑话内容</param>
        /// <returns></returns>
        private string SendTextMessage(Wxmessage wx, string content)
        {
            string res = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[text]]></MsgType> <Content><![CDATA[{3}]]></Content> <FuncFlag>0</FuncFlag></xml> ",
                wx.FromUserName, wx.ToUserName, DateTime.Now, content);
            return res;
        }

        /// <summary>
        /// 获取请求过来的微信信息
        /// </summary>
        /// <returns></returns>
        private Wxmessage GetWxMessage()
        {
            Wxmessage wx = new Wxmessage();
            using (StreamReader str = new StreamReader(Request.InputStream, System.Text.Encoding.UTF8))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(str);
                wx.ToUserName = xml.SelectSingleNode("xml").SelectSingleNode("ToUserName").InnerText;
                wx.FromUserName = xml.SelectSingleNode("xml").SelectSingleNode("FromUserName").InnerText;
                wx.MsgType = xml.SelectSingleNode("xml").SelectSingleNode("MsgType").InnerText;
                if (wx.MsgType.Trim() == "text")
                {
                    wx.Content = xml.SelectSingleNode("xml").SelectSingleNode("Content").InnerText;
                }
                if (wx.MsgType.Trim() == "event")
                {
                    wx.EventName = xml.SelectSingleNode("xml").SelectSingleNode("Event").InnerText;
                }
            }

            return wx;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="content"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private string GetSubString(string content, int length)
        {
            if (content.Length >= length)
            {
                return content.Substring(0, length);
            }
            else
            {
                return content;
            }
        }

        // 自定义一个微信消息实体类
        class Wxmessage
        {
            public string FromUserName { get; set; }
            public string ToUserName { get; set; }
            public string MsgType { get; set; }
            public string EventName { get; set; }
            public string Content { get; set; }
        }
    }
}
