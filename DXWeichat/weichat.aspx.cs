using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    private readonly string Token = "d113ee6b121874b6ff4835cd64e20add";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Valid();
        Auth();
    }

    private void Valid()  //用于申请“成为开发者”时向微信发送验证信息。
    {
        string echoStr = Request.QueryString["echoStr"].ToString();
        if (CheckSignature1())
        {
            if (!string.IsNullOrEmpty(echoStr))
            {
                Response.Write(echoStr);
                Response.End();
            }
        }
    }

    /// <summary>
    /// 验证微信签名
    /// </summary>
    /// <returns></returns>
    private bool CheckSignature1()
    {
        string signature = Request.QueryString["signature"].ToString();
        string timestamp = Request.QueryString["timestamp"].ToString();
        string nonce = Request.QueryString["nonce"].ToString();
        string[] ArrTmp = { Token, timestamp, nonce };
        Array.Sort(ArrTmp);//字典排序
        string tmpStr = string.Join("", ArrTmp);
        tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");//对该字符串进行sha1加密
        tmpStr = tmpStr.ToLower();

        //开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        //开发者通过检验signature对请求进行校验，若确认此次GET请求来自微信服务器，请原样返回echostr参数内容，则接入生效，否则接入失败
        if (tmpStr == signature)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Auth()
    {
        string signature = Request["signature"];
        string timestamp = Request["timestamp"];
        string nonce = Request["nonce"];
        string echoStr = Request["echoStr"];

        if(Request.HttpMethod=="GET")
        {
            if(Check(signature,timestamp,nonce,Token))
            {
                WriteConten(echoStr);
            }
            else
            {
                Response.Write("failed:" + signature + "," + GetSignature(timestamp, nonce, Token) + Environment.NewLine + "If you look this sentence that mean is current addresss can be a Weichat background URL, please keep the Token is same Weichat setting.");
            }
            Response.End();
        }
    }

    private void WriteConten(string str)
    {
        Response.Output.Write(str);
    }

    public  bool Check(string signature, string timestamp, string nonce, string token)
    {
        return signature == GetSignature(timestamp, nonce, token);
    }

    public string GetSignature(string timestamp, string nonce, string token)
    {
        token = token ?? Token;
        string[] arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
        string arrString = string.Join("", arr);
        System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
        byte[] sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
        StringBuilder enText = new StringBuilder();
        foreach (var b in sha1Arr)
        {
            enText.AppendFormat("{0:x2}", b);
        }
        return enText.ToString();
    }
}