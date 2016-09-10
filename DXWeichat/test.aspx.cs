using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private string GetAccessTaken()
    {
        string sCorpID = "wx382b77b20a9e8f31";
        string sSecret = "6dtG0rnGtCa0TQfpJjMECTG7bm6pVtFPs5K10B_eSSMqA3jj-ly2dyGO-l7qyqjS";
        string sTaken = WXApi.GetToken(sCorpID, sSecret);
        return sTaken;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox1.Text = GetAccessTaken();
        TextBox2.Text = WXApi.GetDPList(TextBox1.Text);
    }
}