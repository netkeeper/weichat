using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBAccess;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int interval = 1471849055;
        TimeSpan timeSpan = new TimeSpan(0, 0, interval);
        DateTime baseTime = DateTime.Now - timeSpan;
        Log.log(baseTime.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string sValue = "";
        DBHelper db = new DBHelper();
        try
        {
            DataSet dt = db.Query(String.Format(@"SELECT a.CUSTOMER_ID CUSTOMER,a.NAME,b.FULLNAME FROM customer a, wearer b, WEAREREMPLOYMENT c, UniqueItemNoNPool d, UNIQUEITEM e, STAY_DESC f, STATUS_DESC g WHERE a.CUSTOMER_ID = b.CUSTOMER_ID  AND b.WEARER_ID = c.WEARER_ID  AND c.WEAREREMPLOYMENT_ID = d.WEAREREMPLOYMENT_ID   AND d.UNIQUEITEM_ID = e.UNIQUEITEM_ID  AND e.STAY_ID = f.STAY_ID  AND e.STATUS_ID = g.STATUS_ID  AND e.STATUS_ID = 26  AND f.LANGUAGE_ID = 1  AND g.LANGUAGE_ID = 1  AND b.DATEINACTIVE > sysdate and e.PRIMARYID='{0}'", "1001177562"));
            ASPxCardView1.DataSource = dt;
            ASPxCardView1.DataBind();
        }
        catch
        {
            sValue = "数据未查到，请重新输入。";
        }
    }
}