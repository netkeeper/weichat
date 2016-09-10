using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class DataQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //GetData("1001177562");
        if (!IsPostBack)
        {

        }
    }

    private void GetData(string sID)
    {
        string sValue = "";
        DBHelper db = new DBHelper();
        try
        {
            DataSet dt = db.Query(String.Format(@"SELECT a.CUSTOMER_ID CUSTOMER,a.NAME,b.FULLNAME FROM customer a, wearer b, WEAREREMPLOYMENT c, UniqueItemNoNPool d, UNIQUEITEM e, STAY_DESC f, STATUS_DESC g WHERE a.CUSTOMER_ID = b.CUSTOMER_ID  AND b.WEARER_ID = c.WEARER_ID  AND c.WEAREREMPLOYMENT_ID = d.WEAREREMPLOYMENT_ID   AND d.UNIQUEITEM_ID = e.UNIQUEITEM_ID  AND e.STAY_ID = f.STAY_ID  AND e.STATUS_ID = g.STATUS_ID  AND e.STATUS_ID = 26  AND f.LANGUAGE_ID = 1  AND g.LANGUAGE_ID = 1  AND b.DATEINACTIVE > sysdate and e.PRIMARYID='{0}'", sID));
            ASPxCardView1.DataSource = dt;
            ASPxCardView1.DataBind();
        }
        catch
        {
            sValue = "数据未查到，请重新输入。";
        }
    }

    private void showdata(string sUniformID)
    {
        string sVaule = "";
        ABSQuery.RSSoapClient GetData = new ABSQuery.RSSoapClient();
        sVaule = GetData.QueryWearerInventoryByID(sUniformID).ToString();

        DataTable dt = new DataTable();
        DataColumn dcName = new DataColumn("Wearer Number");
        DataColumn dcValue = new DataColumn("Wearer Name");
        DataColumn dcLocker = new DataColumn("Bank&Locker ID");
        dt.Columns.Add(dcName);
        dt.Columns.Add(dcValue);
        dt.Columns.Add(dcLocker);

        DataTable dt2 = new DataTable();
        DataColumn dcProduct = new DataColumn("Product ID");
        DataColumn dcBarcode = new DataColumn("BarCode ID");
        DataColumn dcSize = new DataColumn("Size ID");
        dt2.Columns.Add(dcProduct);
        dt2.Columns.Add(dcBarcode);
        dt2.Columns.Add(dcSize);

        JArray ja = (JArray)JsonConvert.DeserializeObject(sVaule);
        for (int i = 0; i < ja.Count; i++)
        {

            //TextBox1.Text = "customer_id - " + ja[i]["customer_id"].ToString() + Environment.NewLine;
            //TextBox1.Text += "customernumber - " + ja[i]["customernumber"].ToString() + Environment.NewLine;
            //TextBox1.Text += "customername - " + ja[i]["customername"].ToString() + Environment.NewLine;
            //TextBox1.Text += "wearer_id - " + ja[i]["wearer_id"].ToString() + Environment.NewLine;
            //TextBox1.Text += "wearernumber - " + ja[i]["wearernumber"].ToString() + Environment.NewLine;
            //TextBox1.Text += "fullname - " + ja[i]["fullname"].ToString() + Environment.NewLine;

            //TextBox1.Text += ja[i]["Employment"].ToString() + Environment.NewLine;
            //TextBox1.Text += "************************************************************" + Environment.NewLine;
            JArray ja2 = (JArray)JsonConvert.DeserializeObject(ja[i]["Employment"].ToString());
            for (int i2 = 0; i2 < ja2.Count; i2++)
            {
                //TextBox1.Text += "WearerEmployment_Id - " + ja2[i2]["WearerEmployment_Id"].ToString() + Environment.NewLine;
                //TextBox1.Text += "Wearer_Id - " + ja2[i2]["Wearer_Id"].ToString() + Environment.NewLine;
                //TextBox1.Text += "SequenceNumber - " + ja2[i2]["SequenceNumber"].ToString() + Environment.NewLine;
                //TextBox1.Text += "WearerFunction_Id - " + ja2[i2]["WearerFunction_Id"].ToString() + Environment.NewLine;
                //TextBox1.Text += "Flag_Id - " + ja2[i2]["Flag_Id"].ToString() + Environment.NewLine;
                //TextBox1.Text += "DeliveryPoint_Id - " + ja2[i2]["DeliveryPoint_Id"].ToString() + Environment.NewLine;
                //TextBox1.Text += "Locker_id - " + ja2[i2]["Locker_id"].ToString() + Environment.NewLine;
                DataRow dr = dt.NewRow();
                dr[0] = ja[i]["wearernumber"].ToString();
                dr[1] = ja[i]["fullname"].ToString();
                dr[2] = ja2[i2]["Locker_id"].ToString();
                dt.Rows.Add(dr);

               // TextBox1.Text += "DispenseCredit - " + ja2[i2]["DispenseCredit"].ToString() + Environment.NewLine;
                //TextBox1.Text += "SystemUser_Id - " + ja2[i2]["SystemUser_Id"].ToString() + Environment.NewLine;
                //TextBox1.Text += "Timestamp - " + ja2[i2]["Timestamp"].ToString() + Environment.NewLine;
                //TextBox1.Text += "FlagStartDate - " + ja2[i2]["FlagStartDate"].ToString() + Environment.NewLine;
                //TextBox1.Text += ja2[i2]["WearerInventory"].ToString() + Environment.NewLine;
                //TextBox1.Text += "************************************************************" + Environment.NewLine;
                JArray ja3 = (JArray)JsonConvert.DeserializeObject(ja2[i2]["WearerInventory"].ToString());
                for (int i3 = 0; i3 < ja3.Count; i3++)
                {
                    //TextBox1.Text += "WearerInventory_Id - " + ja3[i3]["WearerInventory_Id"].ToString() + Environment.NewLine;
                    //TextBox1.Text += "Product_Id - " + ja3[i3]["Product_Id"].ToString() + Environment.NewLine;
                    //TextBox1.Text += ja3[i3]["Items"].ToString() + Environment.NewLine;
                    JArray ja4 = (JArray)JsonConvert.DeserializeObject(ja3[i3]["Items"].ToString());
                    //TextBox1.Text += "+++++++++++++++++++++++++++++++++++++++++" + Environment.NewLine;
                    for (int i4 = 0; i4 < ja4.Count; i4++)
                    {
                        DataRow dr2 = dt2.NewRow();
                        dr2[0] = ja4[i4]["Product_Id"].ToString();

                        //TextBox1.Text += "Product_Id - " + ja4[i4]["Product_Id"].ToString() + Environment.NewLine;
                        //TextBox1.Text += "UniqueItem_Id - " + ja4[i4]["UniqueItem_Id"].ToString() + Environment.NewLine;
                        //TextBox1.Text += "PrimaryId - " + ja4[i4]["PrimaryId"].ToString() + Environment.NewLine;
                        dr2[1] = ja4[i4]["PrimaryId"].ToString();

                       // TextBox1.Text += "SecondaryId - " + ja4[i4]["SecondaryId"].ToString() + Environment.NewLine;
                        //TextBox1.Text += "SizeDefinition_Id - " + ja4[i4]["SizeDefinition_Id"].ToString() + Environment.NewLine;
                        dr2[2] = ja4[i4]["SizeDefinition_Id"].ToString();
                        dt2.Rows.Add(dr2);

                        //TextBox1.Text += "&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&" + Environment.NewLine;
                    }
                }
            }
        }
        ASPxCardView1.DataSource = dt;
        ASPxCardView1.DataBind();
        ASPxGridView1.DataSource = dt2;
        ASPxGridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string sUniformID = Request.Form["InputUSN"];
        //ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + sUniformID + "');", true);
        //GetData(sUniformID);
        //showdata(sUniformID);
        Response.Redirect("Result.aspx?ID=" + sUniformID.Trim());
    }
}