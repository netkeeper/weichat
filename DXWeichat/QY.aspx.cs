using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using DBAccess;

public partial class QY : System.Web.UI.Page
{
    //以下是连接必要信息，到企业号中寻找
    string sToken = "d113ee6b121874b6ff4835cd64e20add";
    string sCorpID = "wx382b77b20a9e8f31";
    string sEncodingAESKey = "Q7MFILWtcIwxBzc7yoWsjs59pfgSYIsSI2W4oYDIk8z";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Log.log("This is a test for data query - " + GetData("200009105"));
        Valid();
    }

    private void Valid()  //用于申请“成为开发者”时向微信发送验证信息。
    {
        Log.log(Request.QueryString.ToString());

        if (Request.HttpMethod == "GET")
        {
            /*
             ------------使用示例一：验证回调URL---------------
             *企业开启回调模式时，企业号会向验证url发送一个get请求 
             假设点击验证时，企业收到类似请求：
             * GET /cgi-bin/wxpush?msg_signature=5c45ff5e21c57e6ad56bac8758b79b1d9ac89fd3&timestamp=1409659589&nonce=263014780&echostr=P9nAzCzyDtyTWESHep1vC5X9xho%2FqYX3Zpb4yKa9SKld1DsH3Iyt3tP3zNdtp%2B4RPcs8TgAE7OaBO%2BFZXvnaqQ%3D%3D 
             * HTTP/1.1 Host: qy.weixin.qq.com

             * 接收到该请求时，企业应			1.解析出Get请求的参数，包括消息体签名(msg_signature)，时间戳(timestamp)，随机数字串(nonce)以及公众平台推送过来的随机加密字符串(echostr),
             这一步注意作URL解码。
             2.验证消息体签名的正确性 
             3.解密出echostr原文，将原文当作Get请求的response，返回给公众平台
             第2，3步可以用公众平台提供的库函数VerifyURL来实现。
             */

            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(sToken, sEncodingAESKey, sCorpID);
            string sVerifyMsgSig = Request["msg_signature"];
            Log.log(sVerifyMsgSig);
            string sVerifyTimeStamp = Request["timestamp"];
            Log.log(sVerifyTimeStamp);
            string sVerifyNonce = Request["nonce"];
            Log.log(sVerifyNonce);
            string sVerifyEchoStr = Request["echostr"];
            Log.log(sVerifyEchoStr);


            //string sVerifyMsgSig = "5c45ff5e21c57e6ad56bac8758b79b1d9ac89fd3";
            //string sVerifyTimeStamp = "1409659589";
            //string sVerifyNonce = "263014780";
            //string sVerifyEchoStr = "P9nAzCzyDtyTWESHep1vC5X9xho/qYX3Zpb4yKa9SKld1DsH3Iyt3tP3zNdtp+4RPcs8TgAE7OaBO+FZXvnaqQ==";

            int ret = 0;
            string sEchoStr = "";
            ret = wxcpt.VerifyURL(sVerifyMsgSig, sVerifyTimeStamp, sVerifyNonce, sVerifyEchoStr, ref sEchoStr);
            if (ret != 0)
            {
                Log.log("ERR: VerifyURL fail, ret: " + ret);
                return;
            }
            Log.log("Return String is:" + sEchoStr);
            //ret==0表示验证成功，sEchoStr参数表示明文，用户需要将sEchoStr作为get请求的返回参数，返回给企业号。
            // HttpUtils.SetResponse(sEchoStr);
            Response.Output.Write(sEchoStr);
        }
        else if (Request.HttpMethod == "POST")
        {
            /*
			------------使用示例二：对用户回复的消息解密---------------
			用户回复消息或者点击事件响应时，企业会收到回调消息，此消息是经过公众平台加密之后的密文以post形式发送给企业，密文格式请参考官方文档
			假设企业收到公众平台的回调消息如下：
			POST /cgi-bin/wxpush? msg_signature=477715d11cdb4164915debcba66cb864d751f3e6&timestamp=1409659813&nonce=1372623149 HTTP/1.1
			Host: qy.weixin.qq.com
			Content-Length: 613
			<xml>			<ToUserName><![CDATA[wx5823bf96d3bd56c7]]></ToUserName><Encrypt><![CDATA[RypEvHKD8QQKFhvQ6QleEB4J58tiPdvo+rtK1I9qca6aM/wvqnLSV5zEPeusUiX5L5X/0lWfrf0QADHHhGd3QczcdCUpj911L3vg3W/sYYvuJTs3TUUkSUXxaccAS0qhxchrRYt66wiSpGLYL42aM6A8dTT+6k4aSknmPj48kzJs8qLjvd4Xgpue06DOdnLxAUHzM6+kDZ+HMZfJYuR+LtwGc2hgf5gsijff0ekUNXZiqATP7PF5mZxZ3Izoun1s4zG4LUMnvw2r+KqCKIw+3IQH03v+BCA9nMELNqbSf6tiWSrXJB3LAVGUcallcrw8V2t9EL4EhzJWrQUax5wLVMNS0+rUPA3k22Ncx4XXZS9o0MBH27Bo6BpNelZpS+/uh9KsNlY6bHCmJU9p8g7m3fVKn28H3KDYA5Pl/T8Z1ptDAVe0lXdQ2YoyyH2uyPIGHBZZIs2pDBS8R07+qN+E7Q==]]></Encrypt>
			<AgentID><![CDATA[218]]></AgentID>
			</xml>

			企业收到post请求之后应该			1.解析出url上的参数，包括消息体签名(msg_signature)，时间戳(timestamp)以及随机数字串(nonce)
			2.验证消息体签名的正确性。
			3.将post请求的数据进行xml解析，并将<Encrypt>标签的内容进行解密，解密出来的明文即是用户回复消息的明文，明文格式请参考官方文档
			第2，3步可以用公众平台提供的库函数DecryptMsg来实现。
			*/
            //string sReqMsgSig = "477715d11cdb4164915debcba66cb864d751f3e6";
            //string sReqTimeStamp = "1409659813";
            //string sReqNonce = "1372623149";
            //string sReqData = "<xml><ToUserName><![CDATA[wx5823bf96d3bd56c7]]></ToUserName><Encrypt><![CDATA[RypEvHKD8QQKFhvQ6QleEB4J58tiPdvo+rtK1I9qca6aM/wvqnLSV5zEPeusUiX5L5X/0lWfrf0QADHHhGd3QczcdCUpj911L3vg3W/sYYvuJTs3TUUkSUXxaccAS0qhxchrRYt66wiSpGLYL42aM6A8dTT+6k4aSknmPj48kzJs8qLjvd4Xgpue06DOdnLxAUHzM6+kDZ+HMZfJYuR+LtwGc2hgf5gsijff0ekUNXZiqATP7PF5mZxZ3Izoun1s4zG4LUMnvw2r+KqCKIw+3IQH03v+BCA9nMELNqbSf6tiWSrXJB3LAVGUcallcrw8V2t9EL4EhzJWrQUax5wLVMNS0+rUPA3k22Ncx4XXZS9o0MBH27Bo6BpNelZpS+/uh9KsNlY6bHCmJU9p8g7m3fVKn28H3KDYA5Pl/T8Z1ptDAVe0lXdQ2YoyyH2uyPIGHBZZIs2pDBS8R07+qN+E7Q==]]></Encrypt><AgentID><![CDATA[218]]></AgentID></xml>";
            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(sToken, sEncodingAESKey, sCorpID);
            string sReqMsgSig = Request["msg_signature"];
            string sReqTimeStamp = Request["timestamp"];
            string sReqNonce = Request["nonce"];
            // Post请求的密文数据
            var inputStream = Request.InputStream;
            var strLen = Convert.ToInt32(inputStream.Length);
            var strArr = new byte[strLen];
            inputStream.Read(strArr, 0, strLen);
            string sReqData = Encoding.UTF8.GetString(strArr);

            int ret = 0;
            string sMsg = "";  // 解析之后的明文
            ret = wxcpt.DecryptMsg(sReqMsgSig, sReqTimeStamp, sReqNonce, sReqData, ref sMsg);
            if (ret != 0)
            {
                Log.log("ERR: Decrypt Fail, ret: " + ret);
                return;
            }
            // ret==0表示解密成功，sMsg表示解密之后的明文xml串
            // TODO: 对明文的处理
            // For example:
            Log.log(sMsg);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sMsg);
            XmlNode root = doc.FirstChild;
            string content = root["Content"].InnerText;
            Response.Output.Write(content);
            Log.log(content);
            //System.Console.WriteLine(content);
            // ...
            // ...

            /*
                ------------使用示例三：企业回复用户消息的加密---------------
                企业被动回复用户的消息也需要进行加密，并且拼接成密文格式的xml串。
                假设企业需要回复用户的明文如下：
                <xml>
                <ToUserName><![CDATA[mycreate]]></ToUserName>
                <FromUserName><![CDATA[wx5823bf96d3bd56c7]]></FromUserName>
                <CreateTime>1348831860</CreateTime>
                <MsgType><![CDATA[text]]></MsgType>
                <Content><![CDATA[this is a test]]></Content>
                <MsgId>1234567890123456</MsgId>
                <AgentID>128</AgentID>
                </xml>

                为了将此段明文回复给用户，企业应：			
                1.自己生成时间时间戳(timestamp),随机数字串(nonce)以便生成消息体签名，也可以直接用从公众平台的post url上解析出的对应值。
                2.将明文加密得到密文。	
                3.用密文，步骤1生成的timestamp,nonce和企业在公众平台设定的token生成消息体签名。			
                4.将密文，消息体签名，时间戳，随机数字串拼接成xml格式的字符串，发送给企业。
                以上2，3，4步可以用公众平台提供的库函数EncryptMsg来实现。
                */
            // 需要发送的明文
            string sData = GetData(content);
            Log.log(sData);
            string sRespData = "<xml><ToUserName><![CDATA[mycreate]]></ToUserName><FromUserName><![CDATA["+ sCorpID + "]]></FromUserName><CreateTime>1348831860</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + sData + "]]></Content><MsgId>1234567890123456</MsgId><AgentID>2</AgentID></xml>";
            string sEncryptMsg = ""; //xml格式的密文
            ret = wxcpt.EncryptMsg(sRespData, sReqTimeStamp, sReqNonce, ref sEncryptMsg);
            if (ret != 0)
            {
                Log.log("ERR: EncryptMsg Fail, ret: " + ret);
                return;
            }
            // TODO:
            // 加密成功，企业需要将加密之后的sEncryptMsg返回
            // HttpUtils.SetResponse(sEncryptMsg);
            Response.Output.Write(sEncryptMsg);
        }
        Response.End();
    }

    private string GetData(string sID)
    {
        string sValue = "";
        DBHelper db = new DBHelper();
        try
        {
            sValue = db.GetSingle(String.Format(@"SELECT b.FULLNAME FROM customer a, wearer b, WEAREREMPLOYMENT c, UniqueItemNoNPool d, UNIQUEITEM e, STAY_DESC f, STATUS_DESC g WHERE a.CUSTOMER_ID = b.CUSTOMER_ID  AND b.WEARER_ID = c.WEARER_ID  AND c.WEAREREMPLOYMENT_ID = d.WEAREREMPLOYMENT_ID   AND d.UNIQUEITEM_ID = e.UNIQUEITEM_ID  AND e.STAY_ID = f.STAY_ID  AND e.STATUS_ID = g.STATUS_ID  AND e.STATUS_ID = 26  AND f.LANGUAGE_ID = 1  AND g.LANGUAGE_ID = 1  AND b.DATEINACTIVE > sysdate and e.PRIMARYID='{0}'", sID)).ToString();
        }
        catch
        {
            return "数据未查到，请重新输入。";
        }

        return sValue;
    }

    
}