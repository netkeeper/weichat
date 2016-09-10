<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="DataQuery, App_Web_dataquery.aspx.cdcab7d2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="style/weui.css" />
    <link rel="stylesheet" href="style/example.css" />
    <script>
        function HideDIV() {
            $("#dialog2").slideUp("normal");
        }
        function ShowDIV() {
            $("#dialog2").slideDown("normal");
        }
        function HideloadingToast() {
            $("#loadingToast").slideUp("normal");
        }
        function ShowloadingToast() {
            $("#loadingToast").slideDown("normal");
        }
        function ButtonClick() {
            //alert(1);
            document.getElementById("<%=Submit.ClientID %>").click();
            //ShowDIV();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="w3agile properties">
        <div class="properties-top">
            <h3 class="w3ls-title">ABS数据查询</h3>
            <p class="w3title-text">目前仅支持基于Uniform序列号查询Wearer信息，请在下面的输入框输入Uniform序列号。Uniform序列号为10位纯数字。</p>
        </div>
        <div class="welcome-info" style="padding-top: 1em; padding-bottom: 1em;">
            <div>
            </div>
            <div>
                <div class="form-group">
                    <label for="InputUSN">Uniform Serial Number</label>
                    <input type="number" class="form-control" name="InputUSN" id="InputUSN" placeholder="Uniform Serial Number" required="required" />
                </div>
                <button type="button" onclick="ButtonClick();" class="btn btn-primary btn-lg btn-block">提交查询</button>
            </div>
            <div class=" bg-info">
            </div>
        </div>
    </div>
    <div class="weui_dialog_alert" id="dialog2" style="display: none;">
        <div class="weui_mask"></div>
        <div class="weui_dialog">
            <div class="weui_dialog_hd"><strong class="weui_dialog_title">查询结果</strong></div>
            <div class="weui_dialog_bd">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <dx:ASPxCardView ID="ASPxCardView1" runat="server" Theme="DevEx" Width="100%">
                            <Settings LayoutMode="Flow" />
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                        </dx:ASPxCardView>
                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" SettingsPager-Mode="ShowAllRecords"></dx:ASPxGridView>
                        <asp:LinkButton ID="Submit" runat="server" Text="" OnClick="Button1_Click"></asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="weui_dialog_ft">
                <a href="javascript:;" onclick="HideDIV();" class="weui_btn_dialog primary">确定</a>
            </div>
        </div>
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3"
        DisplayAfter="100" DynamicLayout="true">
        <ProgressTemplate>
            <!-- loading toast -->
            <div id="loadingToast" class="weui_loading_toast">
                <div class="weui_mask_transparent"></div>
                <div class="weui_toast">
                    <div class="weui_loading">
                        <div class="weui_loading_leaf weui_loading_leaf_0"></div>
                        <div class="weui_loading_leaf weui_loading_leaf_1"></div>
                        <div class="weui_loading_leaf weui_loading_leaf_2"></div>
                        <div class="weui_loading_leaf weui_loading_leaf_3"></div>
                        <div class="weui_loading_leaf weui_loading_leaf_4"></div>
                        <div class="weui_loading_leaf weui_loading_leaf_5"></div>
                        <div class="weui_loading_leaf weui_loading_leaf_6"></div>
                        <div class="weui_loading_leaf weui_loading_leaf_7"></div>
                        <div class="weui_loading_leaf weui_loading_leaf_8"></div>
                        <div class="weui_loading_leaf weui_loading_leaf_9"></div>
                        <div class="weui_loading_leaf weui_loading_leaf_10"></div>
                        <div class="weui_loading_leaf weui_loading_leaf_11"></div>
                    </div>
                    <p class="weui_toast_content">数据加载中</p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

