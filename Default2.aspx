<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="Default2, App_Web_aojh2q1j" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <dx:ASPxCardView ID="ASPxCardView1" runat="server" Theme="DevEx" Width="100%">
                    <Settings LayoutMode="Flow" />
                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                </dx:ASPxCardView>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
</asp:Content>

