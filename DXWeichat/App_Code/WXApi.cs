using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// WXApi 的摘要说明
/// </summary>
public class WXApi
{
    public WXApi()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public class HttpRequestUtil
    {
        #region 请求Url，不发送数据
        /// <summary>
        /// 请求Url，不发送数据
        /// </summary>
        public static string RequestUrl(string url)
        {
            return RequestUrl(url, "POST");
        }
        #endregion

        #region 请求Url，不发送数据
        /// <summary>
        /// 请求Url，不发送数据
        /// </summary>
        public static string RequestUrl(string url, string method)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = method;
            request.ContentType = "text/html";
            request.Headers.Add("charset", "utf-8");

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            return content;
        }
        #endregion
    }

    /// <summary>
    /// 获取Json字符串某节点的值
    /// </summary>
    private static string GetJsonValue(string jsonStr, string key)
    {
        string result = string.Empty;
        if (!string.IsNullOrEmpty(jsonStr))
        {
            key = "\"" + key.Trim('"') + "\"";
            int index = jsonStr.IndexOf(key) + key.Length + 1;
            if (index > key.Length + 1)
            {
                //先截逗号，若是最后一个，截“｝”号，取最小值
                int end = jsonStr.IndexOf(',', index);
                if (end == -1)
                {
                    end = jsonStr.IndexOf('}', index);
                }

                result = jsonStr.Substring(index, end - index);
                result = result.Trim(new char[] { '"', ' ', '\'' }); //过滤引号或空格
            }
        }
        return result;
    }

    #region 验证Token是否过期
    /// <summary>
    /// 验证Token是否过期
    /// </summary>
    public static bool TokenExpired(string access_token)
    {
        string jsonStr = HttpRequestUtil.RequestUrl(string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", access_token));
        if (GetJsonValue(jsonStr, "errcode") == "42001")
        {
            return true;
        }
        return false;
    }
    #endregion

    /// <summary>
    /// 获取Token
    /// </summary>
    public static string GetToken(string appid, string secret)
    {
        string strJson = HttpRequestUtil.RequestUrl(string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", appid, secret));
        return GetJsonValue(strJson, "access_token");
    }

    public static string GetDPList(string access_token)
    {
        string strJson = HttpRequestUtil.RequestUrl(string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={0}", access_token));
        return strJson;
    }

    public static string CreateUpdate(string posturl, string postData)
    {
        Stream outstream = null;
        Stream instream = null;
        StreamReader sr = null;
        HttpWebResponse response = null;
        HttpWebRequest request = null;
        Encoding encoding = Encoding.UTF8;
        byte[] data = encoding.GetBytes(postData);
        // 准备请求...
        try
        {
            // 设置参数
            request = WebRequest.Create(posturl) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            outstream = request.GetRequestStream();
            outstream.Write(data, 0, data.Length);
            outstream.Close();
            //发送请求并获取相应回应数据
            response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            instream = response.GetResponseStream();
            sr = new StreamReader(instream, encoding);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            string err = string.Empty;
            return content;
        }
        catch (Exception ex)
        {
            string err = ex.Message;
            return string.Empty;
        }
    }

}