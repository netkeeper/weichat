using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string corpid = "wx382b77b20a9e8f31";
        string corpsecret = "6dtG0rnGtCa0TQfpJjMECTG7bm6pVtFPs5K10B_eSSMqA3jj-ly2dyGO-l7qyqjS";
       // string token = WXApi.GetToken(corpid, corpsecret);
       //Log.log(token);

        //string sURL = "https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token=" + token;
        //string sData = "{\"name\": \"广州研发\",\"parentid\": 1}";
        //Log.log(sData);
        //Log.log(WXApi.CreateUpdate(sURL, sData));
    }
}