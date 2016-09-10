using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// CheckSignature 的摘要说明
/// </summary>
public class CheckSignature
{
    public const string Token = "d113ee6b121874b6ff4835cd64e20add";
    public CheckSignature()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static bool Check(string signature, string timestamp, string nonce, string token)
    {
        return signature == GetSignature(timestamp, nonce, token);
    }

    public static string GetSignature(string timestamp, string nonce, string token = Token)
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