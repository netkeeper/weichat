<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="_Default, App_Web_gwpghh3p" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="style/weui.css"/>
    <link rel="stylesheet" href="style/example.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- welcome -->
    <div class="welcome">
        <h3 class="w3ls-title">Welcome</h3>
        <p class="w3title-text">欢迎来到信达思微网站，我们网站为您提供全面的数据查询，如需查询数据请点击相关菜单。如果您遇到问题请及时联络我们的客户人员或在网站提交您的反馈，谢谢！</p>
        <div class="welcome-info" style="padding-top:1em;padding-bottom:1em;">
                <%--<div class="bd">
                    <div class="weui_grids">
                        <a href="DataQuery.aspx" class="weui_grid box1">
                            <div class="weui_grid_icon">
                                <i class="icon icon_button"></i>
                            </div>
                            <p class="weui_grid_label">
                                Button
                            </p>
                        </a>
                        <a href="#/cell" class="weui_grid box2">
                            <div class="weui_grid_icon">
                                <i class="icon icon_cell"></i>
                            </div>
                            <p class="weui_grid_label">
                                Cell
                            </p>
                        </a>
                        <a href="#/toast" class="weui_grid box3">
                            <div class="weui_grid_icon">
                                <i class="icon icon_toast"></i>
                            </div>
                            <p class="weui_grid_label">
                                Toast
                            </p>
                        </a>
                        <a href="#/dialog" class="weui_grid box4">
                            <div class="weui_grid_icon">
                                <i class="icon icon_dialog"></i>
                            </div>
                            <p class="weui_grid_label">
                                Dialog
                            </p>
                        </a>
                        <a href="#/progress" class="weui_grid box5">
                            <div class="weui_grid_icon">
                                <i class="icon icon_progress"></i>
                            </div>
                            <p class="weui_grid_label">
                                Progress
                            </p>
                        </a>
                        <a href="#/msg" class="weui_grid box6">
                            <div class="weui_grid_icon">
                                <i class="icon icon_msg"></i>
                            </div>
                            <p class="weui_grid_label">
                                Msg
                            </p>
                        </a>
                        <a href="#/article" class="weui_grid box7">
                            <div class="weui_grid_icon">
                                <i class="icon icon_article"></i>
                            </div>
                            <p class="weui_grid_label">
                                Article
                            </p>
                        </a>
                        <a href="#/actionsheet" class="weui_grid box8">
                            <div class="weui_grid_icon">
                                <i class="icon icon_actionSheet"></i>
                            </div>
                            <p class="weui_grid_label">
                                ActionSheet
                            </p>
                        </a>
                        <a href="#/icons" class="weui_grid box9">
                            <div class="weui_grid_icon">
                                <i class="icon icon_icons"></i>
                            </div>
                            <p class="weui_grid_label">
                                Icons
                            </p>
                        </a>
                    </div>
                </div>--%>

                        <div id="content">
                            <div id="home" class="section">
                                <div class="home_box left">
                                    <div class="row1 box box1">
                                        <div class="box_with_padding">
                                            <a href="DataQuery.aspx"><img alt="" src="images/H_NewIdea.png" /><h2 style="font-size:3vw;">Wearer</h2></a>
                                           
                                        </div>
                                    </div>
                                    <div class="row1 box2">
                                        <div class="box_with_padding">
                                            <a href="DataQuery.aspx"><img alt="" src="images/H_ProjectList.png"/><h2 style="font-size:3vw;">Uniform</h2></a>
                                            
                                        </div>
                                    </div>
                                    <div class="row1 box3">
                                        <div class="box_with_padding">
                                            <a href="DataQuery.aspx"><img alt="" src="images/H_ProjectReport.png"/><h2 style="font-size:3vw;">SRS</h2></a>
                                           
                                        </div>
                                    </div>
                                    <div class="row1 box5">
                                        <div class="box_with_padding">
                                            <a href="#contact"><img alt="" src="images/H_Contact.png"/><h2 style="font-size:3vw;">Contact</h2></a>
                                           
                                        </div>
                                    </div>
                                </div>
                                <div class="home_box right">
                                    <div class="row1 box box8">
                                        <div class="box_with_padding">
                                            <a href="DataQuery.aspx"><img alt="" src="images/H_IdeaList.png"/><h2 style="font-size:3vw;">Inventory</h2></a>
                                            
                                        </div>
                                    </div>
                                    <div class="row1 box6">
                                        <div class="box_with_padding">
                                            <a href="DataQuery.aspx"><img alt="" src="images/H_ProjectEvaluation.png"/><h2 style="font-size:3vw;">Invoice</h2></a>
                                            
                                        </div>
                                    </div>
                                    <div class="row1 box7">
                                        <div class="box_with_padding">
                                            <a href="DataQuery.aspx"><img alt="" src="images/H_Management.png" /><h2 style="font-size:3vw;">Others</h2></a>
                                            
                                        </div>
                                    </div>
                                    <div class="row1 box5">
                                        <div class="box_with_padding">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- END of Home -->
                        </div>
        </div>
    </div>
    <!-- //welcome -->
</asp:Content>

