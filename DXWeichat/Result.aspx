<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Result.aspx.cs" Inherits="Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="style/weui.css" />
    <link rel="stylesheet" href="style/example.css" />
    <script>
        function HideDIV() {
            $("#dialog1").slideUp("normal");
            window.location.href = "Dataquery.aspx";
        }
        function ShowDIV() {
            $("#dialog1").slideDown("normal");
        }
        function HideloadingToast() {
            $("#loadingToast").slideUp("normal");
        }
        function ShowloadingToast() {
            $("#loadingToast").slideDown("normal");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="w3agile properties">
        <div class="properties-top">
            <h3 class="w3ls-title">查询结果</h3>
            <p class="w3title-text"></p>
        </div>
        <div class="welcome-info" style="padding-top: 1em; padding-bottom: 1em;">
            <div>
            </div>
            <div>
                <div class="form-group">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Result Information</h3>
                        </div>
                        <div class="panel-body">
                            <dx:ASPxCardView ID="ASPxCardView1" runat="server" Theme="DevEx" Width="100%">
                            <Settings LayoutMode="Flow" />
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                        </dx:ASPxCardView>
                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" Styles-Header-HorizontalAlign="Center" SettingsPager-Mode="ShowAllRecords"  Width="100%" Settings-ShowFooter="True"></dx:ASPxGridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class=" bg-info">
            </div>
        </div>
    </div>

    <div class="weui_dialog_alert" id="dialog1" style="display: none;">
        <div class="weui_mask"></div>
        <div class="weui_dialog">
            <div class="weui_dialog_hd"><strong class="weui_dialog_title">查询结果</strong></div>
            <div class="weui_dialog_bd">
                数据未查到，请确认输入的ID是否正确有效！
            </div>
            <div class="weui_dialog_ft">
                <a href="javascript:;" onclick="HideDIV();" class="weui_btn_dialog primary">确定</a>
            </div>
        </div>
    </div>
</asp:Content>

