<%@ page language="C#" autoeventwireup="true" inherits="Default3, App_Web_coayutys" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox1" TextMode="MultiLine" Width="100%" Height="50px" runat="server">

        </asp:TextBox><asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

        <dx:ASPxCardView ID="ASPxCardView1" runat="server" Theme="DevEx" Width="100%">
                            <Settings LayoutMode="Flow" />
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                        </dx:ASPxCardView>

    </div>
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" SettingsPager-Mode="ShowAllRecords"></dx:ASPxGridView>

    </form>
</body>
</html>
