<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainPage.aspx.cs" Inherits="GTS.Clock.Presentaion.WebForms.MainPage" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="FlashControl" Namespace="Bewise.Web.UI.WebControls" TagPrefix="Bewise" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/navStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dockMenu.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Images/Ghadir/favicon.ico" rel="Shortcut Icon" />
    <title></title>
</head>
<body class="MainBodyStyle">
    <script type="text/javascript" src="JS/jquery.js"></script>
    <script type="text/javascript" src="JS/imgscale.jquery.min.js"></script>

    <form id="MainForm" runat="server" meta:resourcekey="MainForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 100%; height: 100%; font-size: small; font-family: Tahoma;"
            id="tblMaster_MainForm">
            <tr runat="server" style="height: 80px;">
                <td style="height: 100%" valign="top" colspan="3">
                    <table style="width: 100%; height: 80px">
                        <tr style="height: 95%">
                            <td colspan="3">
                                <%--<Bewise:FlashControl ID="HeaderFlashControl" runat="server" Menu="false" Scale="Exactfit"
                                    WMode="Transparent" BrowserDetection="False" Height="80px" Width="100%" Quality="High"
                                    Loop="true" Play="false" />--%>
                                <img runat="server" id="imgHeaderLogo" alt="" style="width:100%; height:80px;"/>
                            </td>
                        </tr>
                        <tr style="height: 5%">
                            <td>
                                <table style="width: 100%" class="MainHeaderStyle">
                                    <tr>
                                        <td style="width: 21%">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 5%">
                                                        <asp:ImageButton ID="ImgbtnPersian" runat="server" OnClientClick="imgbtnPersian_onClick();"
                                                            OnClick="imgbtnPersian_onClick" PostBackUrl="~/MainPage.aspx" ImageUrl="~/Images/TopToolBar/Iran_flag.png" />
                                                    </td>
                                                    <td style="width: 5%">
                                                        <asp:ImageButton ID="ImgbtnEnglish" runat="server" OnClientClick="ImgbtnEnglish_onClick();"
                                                            OnClick="ImgbtnEnglish_onClick" PostBackUrl="~/MainPage.aspx" ImageUrl="~/Images/TopToolBar/uk-flag.png" />
                                                    </td>
                                                    <td style="width: 90%"></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 77%">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 50%">
                                                        <asp:Label ID="lblCurrentDate" runat="server" CssClass="HeaderLabel"></asp:Label>
                                                    </td>
                                                    <td style="width: 50%" align="center">
                                                        <asp:Label ID="lblCurrentUser" runat="server" CssClass="WelcomeLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj" style="width: 2%">
                                            <asp:ImageButton ID="imgbtnLogOut" runat="server" OnClientClick="imgbtnLogOut_onClick();"
                                                OnClick="imgbtnLogOut_onClick" PostBackUrl="~/MainPage.aspx" ImageUrl="~/Images/TopToolBar/exit.png" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div class="bottomDockMenu" id="bottomDockMenu">
                        <div class="bottomDockMenu-container">
                            <a id="dmItemWelcome_DockMenu" class="bottomDockMenu-item" href="#" onclick="DockMenuItem_onClick('dmItemWelcome_DockMenu')">
                                <span></span>
                                <img id="qlItemHome" src="Images/DockMenu/home.png" alt="" /></a><a id="dmItemWorkFlowsView_DockMenu"
                                    class="bottomDockMenu-item" href="#" onclick="DockMenuItem_onClick('dmItemWorkFlowsView_DockMenu')">
                                    <span></span>
                                    <img id="qlItemWorkFlowsView" src="images/DockMenu/email.png" alt="" /></a>
                            <a id="dmItemTrafficsControl_DockMenu" class="bottomDockMenu-item" href="#" onclick="DockMenuItem_onClick('dmItemTrafficsControl_DockMenu')">
                                <span></span>
                                <img id="qlItemTrafficsControl" src="images/DockMenu/portfolio.png" alt="" /></a><a
                                    id="dmItemRegisteredRequests_DockMenu" class="bottomDockMenu-item" href="#" onclick="DockMenuItem_onClick('dmItemRegisteredRequests_DockMenu')">
                                    <span></span>
                                    <img id="qlItemRegisteredRequests" src="images/DockMenu/video.png" alt="" /></a>
                            <a id="dmItemSurveyedRequests_DockMenu" class="bottomDockMenu-item" href="#" onclick="DockMenuItem_onClick('dmItemSurveyedRequests_DockMenu')">
                                <span></span>
                                <img id="qlItemSurveyedRequests" src="images/DockMenu/music.png" alt="" /></a>
                            <a id="dmItemKartable_DockMenu" class="bottomDockMenu-item" href="#" onclick="DockMenuItem_onClick('dmItemKartable_DockMenu')">
                                <span></span>
                                <img id="qlItemKartable" src="images/DockMenu/history.png" alt="" /></a><a id="dmItemReportsIntroduction_DockMenu"
                                    class="bottomDockMenu-item" href="#" onclick="DockMenuItem_onClick('dmItemReportsIntroduction_DockMenu')">
                                    <span></span>
                                    <img id="qlItemReportsIntroduction" src="images/DockMenu/calendar.png" alt="" /></a>
                            <a id="dmItemPersonnelIntroduction_DockMenu" class="bottomDockMenu-item" href="#"
                                onclick="DockMenuItem_onClick('dmItemPersonnelIntroduction_DockMenu')"><span></span>
                                <img id="qlItemPersonnelIntroduction" src="images/DockMenu/link.png" alt="" /></a> <a id="dmItemManagerMasterMonthlyOperationReport_DockMenu"
                                    class="bottomDockMenu-item" href="#" onclick="DockMenuItem_onClick('dmItemManagerMasterMonthlyOperationReport_DockMenu')">
                                    <span></span>
                                    <img id="qlItemManagerMasterMonthlyOperationReport" src="images/DockMenu/rss.png" alt="" /></a>
                            <a id="dmItemPersonnelMasterMonthlyOperationReport_DockMenu" class="bottomDockMenu-item"
                                href="#" onclick="DockMenuItem_onClick('dmItemPersonnelMasterMonthlyOperationReport_DockMenu')">
                                <span></span>
                                <img id="qlItemPersonnelMasterMonthlyOperationReport" src="images/DockMenu/rss2.png" alt="" /></a>
                        </div>
                    </div>
                </td>
            </tr>
            <tr runat="server" id="NavBarMain_tr" style="height: 370px;">
                <td style="width: 21%; height: 100%;" valign="top">
                    <div id="NavBarMain_div">
                        <ComponentArt:NavBar ID="NavBarMain" Width="95%" Height="100%" CssClass="NavBar"
                            FillContainer="false" DefaultItemLookId="TopItemLook" ExpandSinglePath="true"
                            ImagesBaseUrl="images/NavBar" DefaultSelectedItemLookId="Level2SelectedItemLook"
                            runat="server" DefaultItemTextAlign="Right" meta:resourcekey="NavBarMain">
                            <ItemLooks>
                                <ComponentArt:ItemLook LookId="TopItemLook" RightIconUrl="arrow.gif" ExpandedRightIconUrl="arrow_expanded.gif"
                                    LabelPaddingLeft="15" meta:resourcekey="TopItemLook" />
                                <ComponentArt:ItemLook LookId="Level2ItemLook" LabelPaddingLeft="15" meta:resourcekey="Level2ItemLook" />
                                <ComponentArt:ItemLook LookId="Level2SelectedItemLook" LabelPaddingLeft="15" meta:resourcekey="Level2SelectedItemLook" />
                            </ItemLooks>
                            <Items>
                                <ComponentArt:NavBarItem Text="تعاریف پایه" DefaultSubItemLookId="Level2ItemLook"
                                    Expanded="false" ID="nvbItemBasicDefinitions_NavBarMain" SelectedLookId="TopItemLook"
                                    SubGroupCssClass="Level2Group" meta:resourcekey="nvbItemsBasicDefinitions_NavBarMain">
                                    <ComponentArt:NavBarItem Text="معرفی پرسنل" meta:resourcekey="nvbItemPersonnelIntroduction_NavBarMain"
                                        ID="nvbItemPersonnelIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی انواع استخدام" meta:resourcekey="nvbItemEmployTypesIntroduction_NavBarMain"
                                        ID="nvbItemEmployTypesIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی شرکت ها" ID="nvbItemCorporationsIntroduction_NavBarMain"
                                        meta:resourcekey="nvbItemCorporationsIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی بخش ها" ID="nvbItemDepartmentsIntroduction_NavBarMain"
                                        meta:resourcekey="nvbItemDepartmentsIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی پست های سازمانی" meta:resourcekey="nvbItemPostsIntroduction_NavBarMain"
                                        ID="nvbItemPostsIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی گروه قوانین" meta:resourcekey="nvbItemRulesGroupIntroduction_NavBarMain"
                                        ID="nvbItemRulesGroupIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی گروه های کاری" meta:resourcekey="nvbItemWorkGroupsIntroduction_NavBarMain"
                                        ID="nvbItemWorkGroupsIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی شیفت ها" meta:resourcekey="nvbItemShiftIntroduction_NavBarMain"
                                        ID="nvbItemShiftIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی نوبت کاری" meta:resourcekey="nvbItemWorkHeatIntroduction_NavBarMain"
                                        ID="nvbItemWorkHeatIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی پیش کارت" meta:resourcekey="nvbItemPreCardIntroduction_NavBarMain"
                                        ID="nvbItemPreCardIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی پزشک" meta:resourcekey="nvbItemPhysicianIntroduction_NavBarMain"
                                        ID="nvbItemPhysicianIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی بیماری" meta:resourcekey="nvbItemIllnessIntroduction_NavBarMain"
                                        ID="nvbItemIllnessIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی محل های ماموریت" meta:resourcekey="nvbItemMissionLocationsIntroduction_NavBarMain"
                                        ID="nvbItemMissionLocationsIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی ایستگاه کنترل" meta:resourcekey="nvbItemControlStationIntroduction_NavBarMain"
                                        ID="nvbItemControlStationIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی دستگاه" meta:resourcekey="nvbItemMachineIntroduction_NavBarMain"
                                        ID="nvbItemMachineIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی تعطیلات سالیانه" ID="nvbItemYearlyHolidaysIntroduction_NavBarMain"
                                        meta:resourcekey="nvbItemYearlyHolidaysIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                </ComponentArt:NavBarItem>
                                <ComponentArt:NavBarItem Text="عملیات مجوز" DefaultSubItemLookId="Level2ItemLook" 
                                    Expanded="true" ID="nvbItemJustificationOperation_NavBarMain" SelectedLookId="TopItemLook"
                                    SubGroupCssClass="Level2Group" meta:resourcekey="nvbItemJustificationOperation_NavBarMain">
                                    <ComponentArt:NavBarItem Text="درخواست های ثبت شده" meta:resourcekey="nvbItemRegisteredRequests_NavBarMain"
                                        ID="nvbItemRegisteredRequests_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="کارتابل" meta:resourcekey="nvbItemKartable_NavBarMain" Value="UsingBySubstitiute"
                                        ID="nvbItemKartable_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="تاریخچه کارتابل" meta:resourcekey="nvbItemSurveyedRequests_NavBarMain" Value="UsingBySubstitiute"
                                        ID="nvbItemSurveyedRequests_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="گزارش مدیریتی کارکرد ماهیانه" meta:resourcekey="nvbItemManagerMasterMonthlyOperationReport_NavBarMain" Value="UsingBySubstitiute"
                                        ID="nvbItemManagerMasterMonthlyOperationReport_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="گزارش کارکرد ماهیانه" meta:resourcekey="nvbItemPersonnelMasterMonthlyOperationReport_NavBarMain"
                                        ID="nvbItemPersonnelMasterMonthlyOperationReport_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی جریان های کاری" meta:resourcekey="nvbItemWorkFlowsView_NavBarMain"
                                        ID="nvbItemWorkFlowsView_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="مشاهده مدیران" meta:resourcekey="nvbItemMasterManagersIntroduction_NavBarMain"
                                        ID="nvbItemMasterManagersIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی جانشین" meta:resourcekey="nvbItemSubstituteIntroduction_NavBarMain"
                                        ID="nvbItemSubstituteIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                </ComponentArt:NavBarItem>
                                <ComponentArt:NavBarItem Text="عملیات حضور و غیاب" DefaultSubItemLookId="Level2ItemLook"
                                    Expanded="false" ID="nvbItemPresenceAndAbsenceOperation_NavBarMain" SelectedLookId="TopItemLook"
                                    SubGroupCssClass="Level2Group" meta:resourcekey="nvbItemPresenceAndAbsenceOperation_NavBarMain">
                                    <ComponentArt:NavBarItem Text="تعریف محدوده محاسبات" meta:resourcekey="nvbItemCalculationRangeDefinition_NavBarMain"
                                        ID="nvbItemCalculationRangeDefinition_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="تنظیمات واسط کاربری" meta:resourcekey="nvbItemUiValidation_NavBarMain"
                                        ID="nvbItemUiValidation_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی شیفت های استثناء" meta:resourcekey="nvbItemExceptionShiftsIntroduction_NavBarMain"
                                        ID="nvbItemExceptionShiftsIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="مانده مرخصی" meta:resourcekey="nvbItemMasterLeaveRemains_NavBarMain"
                                        ID="nvbItemMasterLeaveRemains_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="کنترل ترددها" meta:resourcekey="nvbItemTrafficsControl_NavBarMain"
                                        ID="nvbItemTrafficsControl_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="نگهبانی" meta:resourcekey="nvbItemSentry_NavBarMain"
                                        ID="nvbItemSentry_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                </ComponentArt:NavBarItem>
                                <ComponentArt:NavBarItem Text="عملیات جانبی" DefaultSubItemLookId="Level2ItemLook"
                                    Expanded="false" ID="nvbItemLateralOperations_NavBarMain" SelectedLookId="TopItemLook"
                                    SubGroupCssClass="Level2Group" meta:resourcekey="nvbItemLateralOperations_NavBarMain">
                                    <ComponentArt:NavBarItem Text="معرفی نقش ها" meta:resourcekey="nvbItemRolesIntroduction_NavBarMain"
                                        ID="nvbItemRolesIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی کاربران" meta:resourcekey="nvbItemUsersIntroduction_NavBarMain"
                                        ID="nvbItemUsersIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="تغییر کلمه عبور" meta:resourcekey="nvbItemPasswordChange_NavBarMain"
                                        ID="nvbItemPasswordChange_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="تغییر مشخصات سازمانی پرسنل" meta:resourcekey="nvbItemPersonnelOrganizationFeaturesChange_NavBarMain"
                                        ID="nvbItemPersonnelOrganizationFeaturesChange_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="مدیریت اخبار عمومی" meta:resourcekey="nvbItemMasterPublicNews_NavBarMain"
                                        ID="nvbItemMasterPublicNews_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="پیام های خصوصی" meta:resourcekey="nvbItemPrivateMessage_NavBarMain"
                                        ID="nvbItemPrivateMessage_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="انجام محاسبت" meta:resourcekey="nvbItemCalculations_NavBarMain"
                                        ID="nvbItemCalculations_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="تنظیمات کاربری شخصی" meta:resourcekey="nvbItemPersonalUserSettings_NavBarMain"
                                        ID="nvbItemPersonalUserSettings_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="تنظیمات کاربری مدیریتی" meta:resourcekey="nvbItemManagementUserSettings_NavBarMain"
                                        ID="nvbItemManagementUserSettings_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                </ComponentArt:NavBarItem>
                                <ComponentArt:NavBarItem Text="گزارش ها" DefaultSubItemLookId="Level2ItemLook" Expanded="false"
                                    ID="nvbItemReports_NavBarMain" SelectedLookId="TopItemLook" SubGroupCssClass="Level2Group"
                                    meta:resourcekey="nvbItemReports_NavBarMain">
                                    <ComponentArt:NavBarItem Text="معرفی گزارش ها" meta:resourcekey="nvbItemReportsIntroduction_NavBarMain"
                                        ID="nvbItemReportsIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="گزارش های سیستمی" meta:resourcekey="nvbItemSystemReports_NavBarMain"
                                        ID="nvbItemSystemReports_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                </ComponentArt:NavBarItem>
                            </Items>
                            <ClientEvents>
                                <ItemSelect EventHandler="NavBarMain_onItemSelect" />
                                <Load EventHandler="NavBarMain_onItemLoad" />
                            </ClientEvents>
                        </ComponentArt:NavBar>
                    </div>
                </td>
                <td style="width: 78%; height: 100%;" valign="top">
                    <div>
                        <ComponentArt:TabStrip ID="TabStripMenus" runat="server" DefaultGroupTabSpacing="1"
                            DefaultItemLookId="DefaultTabLook" DefaultSelectedItemLookId="SelectedTabLook"
                            ScrollingEnabled="true" ImagesBaseUrl="images/TabStrip" MultiPageId="MultiPageMenus"
                            ScrollLeftLookId="ScrollItem" ScrollRightLookId="ScrollItem" Width="100%">
                            <ItemLooks>
                                <ComponentArt:ItemLook LookId="DefaultTabLook" CssClass="DefaultTab" HoverCssClass="DefaultTabHover"
                                    LabelPaddingLeft="15" LabelPaddingRight="15" LabelPaddingTop="4" LabelPaddingBottom="4"
                                    LeftIconUrl="tab_left_icon.gif" RightIconUrl="tab_right_icon.gif" LeftIconWidth="13"
                                    LeftIconHeight="25" RightIconWidth="13" RightIconHeight="25" meta:resourcekey="DefaultTabLook" />
                                <ComponentArt:ItemLook LookId="SelectedTabLook" CssClass="SelectedTab" LabelPaddingLeft="15"
                                    LabelPaddingRight="15" LabelPaddingTop="4" LabelPaddingBottom="4" LeftIconUrl="selected_tab_left_icon.gif"
                                    RightIconUrl="selected_tab_right_icon.gif" LeftIconWidth="13" LeftIconHeight="25"
                                    RightIconWidth="13" RightIconHeight="25" meta:resourcekey="SelectedTabLook" />
                                <ComponentArt:ItemLook LookId="ScrollItem" CssClass="ScrollItem" HoverCssClass="ScrollItemHover"
                                    LabelPaddingLeft="5" LabelPaddingRight="5" LabelPaddingTop="0" LabelPaddingBottom="0" />
                            </ItemLooks>
                            <Tabs>
                                <ComponentArt:TabStripTab ID="tbWelcome_TabStripMenus" Text="شرکت طرح و پردازش غدیر"
                                    meta:resourcekey="tbWelcome_TabStripMenus">
                                </ComponentArt:TabStripTab>
                            </Tabs>
                            <ClientEvents>
                                <TabSelect EventHandler="TabStripMenus_onTabSelect" />
                            </ClientEvents>
                        </ComponentArt:TabStrip>
                    </div>
                    <ComponentArt:MultiPage ID="MultiPageMenus" runat="server" CssClass="MultiPage" SelectedIndex="0"
                        Width="100%">
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvWelcome">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvWelcome_iFrame" src="MainView.aspx?(new Date()).getDate()"
                                    class="pgvWelcome_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvShiftIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvShiftIntroduction_iFrame"
                                    src="about:blank" class="pgvShiftIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvDepartmentsIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvDepartmentsIntroduction_iFrame"
                                    src="about:blank" class="pgvDepartmentsIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvPostsIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvPostsIntroduction_iFrame"
                                    src="about:blank" class="pgvPostsIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvEmployTypesIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvEmployTypesIntroduction_iFrame"
                                    src="about:blank" class="pgvEmployTypesIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvMissionLocationsIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvMissionLocationsIntroduction_iFrame"
                                    src="about:blank" class="pgvMissionLocationsIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvExceptionShiftsIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvExceptionShiftsIntroduction_iFrame"
                                    src="about:blank" class="pgvExceptionShiftsIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvWorkGroupsIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvWorkGroupsIntroduction_iFrame"
                                    src="about:blank" class="pgvWorkGroupsIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvPersonnelIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvPersonnelIntroduction_iFrame"
                                    src="about:blank" class="pgvPersonnelIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvWorkHeatIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvWorkHeatIntroduction_iFrame"
                                    src="about:blank" class="pgvWorkHeatIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvControlStationIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvControlStationIntroduction_iFrame"
                                    src="about:blank" class="pgvControlStationIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvMachineIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvMachineIntroduction_iFrame"
                                    src="about:blank" class="pgvMachineIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvMasterLeaveRemains">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvMasterLeaveRemains_iFrame"
                                    src="about:blank" class="pgvMasterLeaveRemains_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvPersonnelOrganizationFeaturesChange">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvPersonnelOrganizationFeaturesChange_iFrame"
                                    src="about:blank" class="pgvPersonnelOrganizationFeaturesChange_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvUsersIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvUsersIntroduction_iFrame"
                                    src="about:blank" class="pgvUsersIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvPreCardIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvPreCardIntroduction_iFrame"
                                    src="about:blank" class="pgvPreCardIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvPasswordChange">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvPasswordChange_iFrame" src="about:blank"
                                    class="pgvPasswordChange_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvRolesIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvRolesIntroduction_iFrame"
                                    src="about:blank" class="pgvRolesIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvManagerMasterMonthlyOperationReport">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvManagerMasterMonthlyOperationReport_iFrame"
                                    src="about:blank" class="pgvManagerMasterMonthlyOperationReport_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvPersonnelMasterMonthlyOperationReport">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvPersonnelMasterMonthlyOperationReport_iFrame"
                                    src="about:blank" class="pgvPersonnelMasterMonthlyOperationReport_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvMasterManagersIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvMasterManagersIntroduction_iFrame"
                                    src="about:blank" class="pgvMasterManagersIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvWorkFlowsView">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvWorkFlowsView_iFrame" src="about:blank"
                                    class="pgvWorkFlowsView_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvSubstituteIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvSubstituteIntroduction_iFrame"
                                    src="about:blank" class="pgvSubstituteIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvPhysicianIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvPhysicianIntroduction_iFrame"
                                    src="about:blank" class="pgvPhysicianIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvIllnessIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvIllnessIntroduction_iFrame"
                                    src="about:blank" class="pgvIllnessIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvCalculationRangeDefinition">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvCalculationRangeDefinition_iFrame"
                                    src="about:blank" class="pgvCalculationRangeDefinition_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvYearlyHolidaysIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvYearlyHolidaysIntroduction_iFrame"
                                    src="about:blank" class="pgvYearlyHolidaysIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvRulesGroupIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvRulesGroupIntroduction_iFrame"
                                    src="about:blank" class="pgvRulesGroupIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvReportsIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvReportsIntroduction_iFrame"
                                    src="about:blank" class="pgvReportsIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvTrafficsControl">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvTrafficsControl_iFrame" src="about:blank"
                                    class="pgvTrafficsControl_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvMasterPublicNews">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvMasterPublicNews_iFrame" src="about:blank"
                                    class="pgvMasterPublicNews_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvPrivateMessage">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvPrivateMessage_iFrame" src="about:blank"
                                    class="pgvPrivateMessage_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvUiValidation">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvUiValidation_iFrame" src="about:blank"
                                    class="pgvUiValidation_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvCalculations">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvCalculations_iFrame" src="about:blank"
                                    class="pgvCalculations_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvCorporationsIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvCorporationsIntroduction_iFrame" src="about:blank"
                                    class="pgvCorporationsIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                    </ComponentArt:MultiPage>
                </td>
                <td style="width: 1%; height: 100%" valign="top"></td>
            </tr>
        </table>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogLeaveBudgetheader" FooterClientTemplateId="DialogLeaveBudgetfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogLeaveBudget"
            runat="server" PreloadContentUrl="false" ContentUrl="LeaveBudget.aspx" IFrameCssClass="LeaveBudget_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogLeaveBudgetheader">
                    <table style="width: 523px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogLeaveBudget.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogLeaveBudget_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogLeaveBudget" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogLeaveBudget" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogLeaveBudget_IFrame').src = 'WhitePage.aspx'; DialogLeaveBudget.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogLeaveBudget_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogLeaveBudgetfooter">
                    <table id="tbl_DialogLeaveBudgetfooter" style="width: 523px" cellpadding="0" cellspacing="0"
                        border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogLeaveBudget_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogLeaveBudget_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogLeaveBudget_onShow" />
                <OnClose EventHandler="DialogLeaveBudget_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogUserInterfaceAccessLevelsheader" FooterClientTemplateId="DialogUserInterfaceAccessLevelsfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogUserInterfaceAccessLevels"
            runat="server" PreloadContentUrl="false" IFrameCssClass="UserInterfaceAccessLevels_iFrame"
            ContentUrl="UserInterfaceAccessLevels.aspx">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogUserInterfaceAccessLevelsheader">
                    <table style="width: 603px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogUserInterfaceAccessLevels.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogUserInterfaceAccessLevels_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogUserInterfaceAccessLevels" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogUserInterfaceAccessLevels" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogUserInterfaceAccessLevels_IFrame').src='WhitePage.aspx'; DialogUserInterfaceAccessLevels.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogUserInterfaceAccessLevels_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogUserInterfaceAccessLevelsfooter">
                    <table id="tbl_DialogUserInterfaceAccessLevelsfooter" style="width: 603px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogUserInterfaceAccessLevels_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogUserInterfaceAccessLevels_downRightImage" style="display: block;"
                                    src="Images/Dialog/down_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogUserInterfaceAccessLevels_onShow" />
                <OnClose EventHandler="DialogUserInterfaceAccessLevels_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogPersonnelMainInformationheader" FooterClientTemplateId="DialogPersonnelMainInformationfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="Default" ID="DialogPersonnelMainInformation"
            runat="server" PreloadContentUrl="false" ContentUrl="PersonnelMainInformation.aspx"
            IFrameCssClass="PersonnelMainInformation_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogPersonnelMainInformationheader">
                    <table id="tbl_DialogPersonnelMainInformationheader" style="width: 993px" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogPersonnelMainInformation.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogPersonnelMainInformation_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogPersonnelMainInformation" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogPersonnelMainInformation" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogPersonnelMainInformation_IFrame').src = 'WhitePage.aspx' ;DialogPersonnelMainInformation.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogPersonnelMainInformation_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogPersonnelMainInformationfooter">
                    <table id="tbl_DialogPersonnelMainInformationfooter" style="width: 993px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogPersonnelMainInformation_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogPersonnelMainInformation_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogPersonnelMainInformation_onShow" />
                <OnClose EventHandler="DialogPersonnelMainInformation_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogPersonnelSearchheader" FooterClientTemplateId="DialogPersonnelSearchfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="Default" ID="DialogPersonnelSearch"
            PreloadContentUrl="false" ContentUrl="PersonnelSearch.aspx" IFrameCssClass="PersonnelSearch_iFrame"
            runat="server" Style="width: 50%;">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogPersonnelSearchheader">
                    <table id="tbl_DialogPersonnelSearchheader" style="width: 993px" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogPersonnelSearch.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogPersonnelSearch_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogPersonnelSearch" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogPersonnelSearch" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogPersonnelSearch_IFrame').contentWindow.CloseDialogPersonnelSearch();" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogPersonnelSearch_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogPersonnelSearchfooter">
                    <table id="tbl_DialogPersonnelSearchfooter" style="width: 993px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogPersonnelSearch_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogPersonnelSearch_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogPersonnelSearch_onShow" />
                <OnClose EventHandler="DialogPersonnelSearch_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogCalendarheader" FooterClientTemplateId="DialogCalendarfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogCalendar"
            runat="server" PreloadContentUrl="false" ContentUrl="Calendar.aspx" IFrameCssClass="Calendar_iFrame"
            Style="width: 50%;">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogCalendarheader">
                    <table style="width: 633px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogCalendar.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogCalendar_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogCalendar" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td valign="middle" id="CloseButton_DialogCalendar">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogCalendar_IFrame').src = 'WhitePage.aspx' ;DialogCalendar.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogCalendar_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogCalendarfooter">
                    <table id="tbl_DialogCalendarfooter" style="width: 633px" cellpadding="0" cellspacing="0"
                        border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogCalendar_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogCalendar_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogCalendar_onShow" />
                <OnClose EventHandler="DialogCalendar_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogExceptionShiftsheader" FooterClientTemplateId="DialogExceptionShiftsfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogExceptionShifts"
            PreloadContentUrl="false" ContentUrl="ExceptionShifts.aspx" IFrameCssClass="ExceptionShifts_tbDetails_iFrame"
            runat="server">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogExceptionShiftsheader">
                    <table style="width: 737px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogExceptionShifts.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogExceptionShifts_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogExceptionShifts" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogExceptionShifts" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogExceptionShifts_IFrame').src = 'WhitePage.aspx'; DialogExceptionShifts.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogExceptionShifts_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogExceptionShiftsfooter">
                    <table id="tbl_DialogExceptionShiftsfooter" style="width: 737px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogExceptionShifts_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogExceptionShifts_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogExceptionShifts_onShow" />
                <OnClose EventHandler="DialogExceptionShifts_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" Modal="true" AllowResize="false"
            runat="server" AllowDrag="false" Alignment="MiddleCentre" ID="DialogLoading">
            <Content>
                <table>
                    <tr>
                        <td>
                            <img runat="server" alt="" src="~/Images/Dialog/loading2.gif" />
                        </td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogLoading_onShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogMonthlyOperationGridSchemaheader" FooterClientTemplateId="DialogMonthlyOperationGridSchemafooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="Default" ID="DialogMonthlyOperationGridSchema"
            runat="server" PreloadContentUrl="false" ContentUrl="MonthlyOperationGridSchema.aspx"
            IFrameCssClass="MonthlyOperationGridSchema_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogMonthlyOperationGridSchemaheader">
                    <table id="tbl_DialogMonthlyOperationGridSchemaheader" style="width: 993px" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogMonthlyOperationGridSchema.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogMonthlyOperationGridSchema_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogMonthlyOperationGridSchema" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogMonthlyOperationGridSchema" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogMonthlyOperationGridSchema_IFrame').src = 'WhitePage.aspx'; DialogMonthlyOperationGridSchema.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogMonthlyOperationGridSchema_topRightImage" style="display: block;"
                                    src="Images/Dialog/top_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogMonthlyOperationGridSchemafooter">
                    <table id="tbl_DialogMonthlyOperationGridSchemafooter" style="width: 993px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogMonthlyOperationGridSchema_downLeftImage" style="display: block;"
                                    src="Images/Dialog/down_left.gif" alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogMonthlyOperationGridSchema_downRightImage" style="display: block;"
                                    src="Images/Dialog/down_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogMonthlyOperationGridSchema_onShow" />
                <OnClose EventHandler="DialogMonthlyOperationGridSchema_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogMonthlyOperationGanttChartSchemaheader" FooterClientTemplateId="DialogMonthlyOperationGanttChartSchemafooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="Default" ID="DialogMonthlyOperationGanttChartSchema"
            runat="server" PreloadContentUrl="false" ContentUrl="MonthlyOperationGanttChartSchema.aspx"
            IFrameCssClass="MonthlyOperationGanttChartSchema_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogMonthlyOperationGanttChartSchemaheader">
                    <table id="tbl_DialogMonthlyOperationGanttChartSchemaheader" style="width: 993px"
                        cellpadding="0" cellspacing="0" border="0" onmousedown="DialogMonthlyOperationGanttChartSchema.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogMonthlyOperationGanttChartSchema_topLeftImage" style="display: block;"
                                    src="Images/Dialog/top_left.gif" alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogMonthlyOperationGanttChartSchema" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogMonthlyOperationGanttChartSchema" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogMonthlyOperationGanttChartSchema_IFrame').src = 'WhitePage.aspx'; DialogMonthlyOperationGanttChartSchema.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogMonthlyOperationGanttChartSchema_topRightImage" style="display: block;"
                                    src="Images/Dialog/top_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogMonthlyOperationGanttChartSchemafooter">
                    <table id="tbl_DialogMonthlyOperationGanttChartSchemafooter" style="width: 993px"
                        cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogMonthlyOperationGanttChartSchema_downLeftImage" style="display: block;"
                                    src="Images/Dialog/down_left.gif" alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogMonthlyOperationGanttChartSchema_downRightImage" style="display: block;"
                                    src="Images/Dialog/down_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogMonthlyOperationGanttChartSchema_onShow" />
                <OnClose EventHandler="DialogMonthlyOperationGanttChartSchema_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogUnderManagementPersonnelheader" FooterClientTemplateId="DialogUnderManagementPersonnelfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogUnderManagementPersonnel"
            runat="server" PreloadContentUrl="false" ContentUrl="UnderManagementPersonnel.aspx"
            IFrameCssClass="UnderManagementPersonnel_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogUnderManagementPersonnelheader">
                    <table style="width: 903px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogUnderManagementPersonnel.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogUnderManagementPersonnel_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogUnderManagementPersonnel" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogUnderManagementPersonnel" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogUnderManagementPersonnel_IFrame').src = 'WhitePage.aspx', DialogUnderManagementPersonnel.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogUnderManagementPersonnel_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogUnderManagementPersonnelfooter">
                    <table id="tbl_DialogUnderManagementPersonnelfooter" style="width: 903px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogUnderManagementPersonnel_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogUnderManagementPersonnel_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogUnderManagementPersonnel_onShow" />
                <OnClose EventHandler="DialogUnderManagementPersonnel_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogKartableheader" FooterClientTemplateId="DialogKartablefooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="Default" ID="DialogKartable"
            runat="server" PreloadContentUrl="false" ContentUrl="about:blank" IFrameCssClass="Kartable_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogKartableheader">
                    <table id="tbl_DialogKartableheader" style="width: 993px" cellpadding="0" cellspacing="0"
                        border="0" onmousedown="DialogKartable.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogKartable_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogKartable" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogKartable" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogKartable_IFrame').src = 'WhitePage.aspx'; DialogKartable.Close('cancelled')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogKartable_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogKartablefooter">
                    <table id="tbl_DialogKartablefooter" style="width: 993px" cellpadding="0" cellspacing="0"
                        border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogKartable_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogKartable_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogKartable_onShow" />
                <OnClose EventHandler="DialogKartable_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogEndorsementFlowStateheader" FooterClientTemplateId="DialogEndorsementFlowStatefooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogEndorsementFlowState"
            runat="server" PreloadContentUrl="false" ContentUrl="EndorsementFlowState.aspx"
            IFrameCssClass="EndorsementFlowState_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogEndorsementFlowStateheader">
                    <table style="width: 603px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogEndorsementFlowState.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogEndorsementFlowState_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogEndorsementFlowState" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogEndorsementFlowState" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogEndorsementFlowState_IFrame').src = 'WhitePage.aspx'; DialogEndorsementFlowState.Close('cancelled')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogEndorsementFlowState_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogEndorsementFlowStatefooter">
                    <table id="tbl_DialogEndorsementFlowStatefooter" style="width: 603px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogEndorsementFlowState_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogEndorsementFlowState_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogEndorsementFlowState_onShow" />
                <OnClose EventHandler="DialogEndorsementFlowState_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogRegisteredRequestsheader" FooterClientTemplateId="DialogRegisteredRequestsfooter"
            Modal="true" AllowResize="false" AllowDrag="false" ID="DialogRegisteredRequests"
            Alignment="Default" runat="server" PreloadContentUrl="false" ContentUrl="RegisteredRequests.aspx"
            IFrameCssClass="RegisteredRequests_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogRegisteredRequestsheader">
                    <table id="tbl_DialogRegisteredRequestsheader" style="width: 978px" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogRegisteredRequests.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogRegisteredRequests_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogRegisteredRequests" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogRegisteredRequests" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogRegisteredRequests_IFrame').src = 'WhitePage.aspx'; DialogRegisteredRequests.Close('cancelled')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogRegisteredRequests_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogRegisteredRequestsfooter">
                    <table id="tbl_DialogRegisteredRequestsfooter" style="width: 978px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogRegisteredRequests_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogRegisteredRequests_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogRegisteredRequests_onShow" />
                <OnClose EventHandler="DialogRegisteredRequests_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogCalculationRange"
            HeaderClientTemplateId="DialogCalculationRangeheader" FooterClientTemplateId="DialogCalculationRangefooter"
            runat="server" PreloadContentUrl="false" ContentUrl="CalculationRange.aspx" IFrameCssClass="CalculationRange_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogCalculationRangeheader">
                    <table id="tbl_DialogCalculationRangeheader" style="width: 903px;" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogCalculationRange.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogCalculationRange_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogCalculationRange" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogCalculationRange" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogCalculationRange_IFrame').src = 'WhitePage.aspx'; DialogCalculationRange.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogCalculationRange_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogCalculationRangefooter">
                    <table id="tbl_DialogCalculationRangefooter" style="width: 903px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogCalculationRange_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogCalculationRange_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogCalculationRange_onShow" />
                <OnClose EventHandler="DialogCalculationRange_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogRulesGroupUpdate"
            HeaderClientTemplateId="DialogRulesGroupUpdateheader" FooterClientTemplateId="DialogRulesGroupUpdatefooter"
            runat="server" PreloadContentUrl="false" ContentUrl="OvertimeJustificationRequest.aspx"
            IFrameCssClass="RulesGroupUpdate_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogRulesGroupUpdateheader">
                    <table id="tbl_DialogRulesGroupUpdateheader" style="width: 993px;" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogRulesGroupUpdate.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogRulesGroupUpdate_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogRulesGroupUpdate" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogRulesGroupUpdate" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogRulesGroupUpdate_IFrame').src = 'WhitePage.aspx'; DialogRulesGroupUpdate.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogRulesGroupUpdate_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogRulesGroupUpdatefooter">
                    <table id="tbl_DialogRulesGroupUpdatefooter" style="width: 993px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogRulesGroupUpdate_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogRulesGroupUpdate_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogRulesGroupUpdate_onShow" />
                <OnClose EventHandler="DialogRulesGroupUpdate_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogReportParametersheader" FooterClientTemplateId="DialogReportParametersfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogReportParameters"
            runat="server" PreloadContentUrl="false" ContentUrl="ReportParameters.aspx" IFrameCssClass="ReportParameters_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogReportParametersheader">
                    <table id="tbl_DialogReportParametersheader" style="width: 993px" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogReportParameters.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogReportParameters_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogReportParameters" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogReportParameters" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogReportParameters_IFrame').src = 'WhitePage.aspx'; DialogReportParameters.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogReportParameters_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogReportParametersfooter">
                    <table id="tbl_DialogReportParametersfooter" style="width: 993px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogReportParameters_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogReportParameters_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogReportParameters_onShow" />
                <OnClose EventHandler="DialogReportParameters_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogSubstituteSettingsheader" FooterClientTemplateId="DialogSubstituteSettingsfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogSubstituteSettings"
            runat="server" PreloadContentUrl="false" ContentUrl="SubstituteSettings.aspx"
            IFrameCssClass="SubstituteSettings_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogSubstituteSettingsheader">
                    <table style="width: 753px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogSubstituteSettings.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogSubstituteSettings_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogSubstituteSettings" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogSubstituteSettings" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogSubstituteSettings_IFrame').src = 'WhitePage.aspx'; DialogSubstituteSettings.Close('cancelled')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogSubstituteSettings_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogSubstituteSettingsfooter">
                    <table id="tbl_DialogSubstituteSettingsfooter" style="width: 753px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogSubstituteSettings_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogSubstituteSettings_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogSubstituteSettings_onShow" />
                <OnClose EventHandler="DialogSubstituteSettings_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogOperatorsheader" FooterClientTemplateId="DialogOperatorsfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogOperators"
            runat="server" PreloadContentUrl="false" ContentUrl="Operators.aspx" IFrameCssClass="Operators_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogOperatorsheader">
                    <table id="tbl_DialogOperatorsheader" style="width: 803px" cellpadding="0" cellspacing="0"
                        border="0" onmousedown="DialogOperators.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogOperators_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogOperators" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogOperators" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogOperators_IFrame').src = 'WhitePage.aspx'; DialogOperators.Close('cancelled')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogOperators_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogOperatorsfooter">
                    <table id="tbl_DialogOperatorsfooter" style="width: 803px" cellpadding="0" cellspacing="0"
                        border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogOperators_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogOperators_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogOperators_onShow" />
                <OnClose EventHandler="DialogOperators_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogLeaveReserve"
            HeaderClientTemplateId="DialogLeaveReserveheader" FooterClientTemplateId="DialogLeaveReservefooter"
            runat="server" PreloadContentUrl="false" ContentUrl="LeaveReserve.aspx" IFrameCssClass="LeaveReserve_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogLeaveReserveheader">
                    <table id="tbl_DialogLeaveReserveheader" style="width: 803px;" cellpadding="0" cellspacing="0"
                        border="0" onmousedown="DialogLeaveReserve.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogLeaveReserve_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogLeaveReserve" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogLeaveReserve" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogLeaveReserve_IFrame').src = 'WhitePage.aspx'; DialogLeaveReserve.Close('cancelled')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogLeaveReserve_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogLeaveReservefooter">
                    <table id="tbl_DialogLeaveReservefooter" style="width: 803px" cellpadding="0" cellspacing="0"
                        border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogLeaveReserve_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogLeaveReserve_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogLeaveReserve_onShow" />
                <OnClose EventHandler="DialogLeaveReserve_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogMasterRulesViewheader" FooterClientTemplateId="DialogMasterRulesViewfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogMasterRulesView"
            runat="server" PreloadContentUrl="false" ContentUrl="MasterRulesView.aspx" IFrameCssClass="MasterRulesView_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogMasterRulesViewheader">
                    <table id="tbl_DialogMasterRulesViewheader" style="width: 723px" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogMasterRulesView.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogMasterRulesView_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogMasterRulesView" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogMasterRulesView" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogMasterRulesView_IFrame').src = 'WhitePage.aspx'; DialogMasterRulesView.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogMasterRulesView_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogMasterRulesViewfooter">
                    <table id="tbl_DialogMasterRulesViewfooter" style="width: 723px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogMasterRulesView_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogMasterRulesView_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogMasterRulesView_onShow" />
                <OnClose EventHandler="DialogMasterRulesView_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogMasterDataAccessLevelsheader" FooterClientTemplateId="DialogMasterDataAccessLevelsfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogMasterDataAccessLevels"
            runat="server" PreloadContentUrl="false" ContentUrl="MonthlyExceptionShifts.aspx" IFrameCssClass="MasterDataAccessLevels_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogMasterDataAccessLevelsheader">
                    <table id="tbl_DialogMasterDataAccessLevelsheader" style="width: 903px" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogMasterDataAccessLevels.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogMasterDataAccessLevels_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogMasterDataAccessLevels" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogMasterDataAccessLevels" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogMasterDataAccessLevels_IFrame').src = 'WhitePage.aspx'; DialogMasterDataAccessLevels.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogMasterDataAccessLevels_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogMasterDataAccessLevelsfooter">
                    <table id="tbl_DialogMasterDataAccessLevelsfooter" style="width: 903px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogMasterDataAccessLevels_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogMasterDataAccessLevels_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogMasterDataAccessLevels_onShow" />
                <OnClose EventHandler="DialogMasterDataAccessLevels_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogSendPrivateMessageheader" FooterClientTemplateId="DialogSendPrivateMessagefooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogSendPrivateMessage"
            runat="server" PreloadContentUrl="false" ContentUrl="SendPrivateMessage.aspx"
            IFrameCssClass="SendPrivateMessage_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogSendPrivateMessageheader">
                    <table style="width: 903px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogSendPrivateMessage.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogSendPrivateMessage_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogSendPrivateMessage" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogSendPrivateMessage" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogSendPrivateMessage_IFrame').src = 'WhitePage.aspx'; DialogSendPrivateMessage.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogSendPrivateMessage_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogSendPrivateMessagefooter">
                    <table id="tbl_DialogSendPrivateMessagefooter" style="width: 903px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogSendPrivateMessage_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogSendPrivateMessage_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogSendPrivateMessage_onShow" />
                <OnClose EventHandler="DialogSendPrivateMessage_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogUiValidationRulesheader" FooterClientTemplateId="DialogUiValidationRulesfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogUiValidationRules"
            runat="server" PreloadContentUrl="false" ContentUrl="UiValidationRules.aspx"
            IFrameCssClass="UiValidationRules_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogUiValidationRulesheader">
                    <table style="width: 903px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogUiValidationRules.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogUiValidationRules_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogUiValidationRules" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogUiValidationRules" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogUiValidationRules_IFrame').src = 'WhitePage.aspx'; DialogUiValidationRules.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogUiValidationRules_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogUiValidationRulesfooter">
                    <table id="tbl_DialogUiValidationRulesfooter" style="width: 903px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogUiValidationRules_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogUiValidationRules_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogUiValidationRules_onShow" />
                <OnClose EventHandler="DialogUiValidationRules_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogUserSettings"
            HeaderClientTemplateId="DialogUserSettingsheader" FooterClientTemplateId="DialogUserSettingsfooter"
            runat="server" PreloadContentUrl="false" ContentUrl="UserSettings.aspx" IFrameCssClass="UserSettings_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogUserSettingsheader">
                    <table id="tbl_DialogUserSettingsheader" style="width: 703px;" cellpadding="0" cellspacing="0"
                        border="0" onmousedown="DialogUserSettings.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogUserSettings_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogUserSettings" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogUserSettings" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogUserSettings_IFrame').src = 'WhitePage.aspx'; DialogUserSettings.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogUserSettings_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogUserSettingsfooter">
                    <table id="tbl_DialogUserSettingsfooter" style="width: 703px" cellpadding="0" cellspacing="0"
                        border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogUserSettings_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogUserSettings_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogUserSettings_onShow" />
                <OnClose EventHandler="DialogUserSettings_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogTrafficsTransfer"
            HeaderClientTemplateId="DialogTrafficsTransferheader" FooterClientTemplateId="DialogTrafficsTransferfooter"
            runat="server" PreloadContentUrl="false" ContentUrl="TrafficsTransfer.aspx" IFrameCssClass="TrafficsTransfer_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogTrafficsTransferheader">
                    <table id="tbl_DialogTrafficsTransferheader" style="width: 503px;" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogTrafficsTransfer.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogTrafficsTransfer_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogTrafficsTransfer" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogTrafficsTransfer" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogTrafficsTransfer_IFrame').src = 'WhitePage.aspx'; DialogTrafficsTransfer.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogTrafficsTransfer_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogTrafficsTransferfooter">
                    <table id="tbl_DialogTrafficsTransferfooter" style="width: 503px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogTrafficsTransfer_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogTrafficsTransfer_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogTrafficsTransfer_onShow" />
                <OnClose EventHandler="DialogTrafficsTransfer_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogSystemReportsheader" FooterClientTemplateId="DialogSystemReportsfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="Default" ID="DialogSystemReports"
            runat="server" PreloadContentUrl="false" ContentUrl="SystemReports.aspx"
            IFrameCssClass="SystemReports_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogSystemReportsheader">
                    <table id="tbl_DialogSystemReportsheader" style="width: 993px" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogSystemReports.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogSystemReports_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogSystemReports" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogSystemReports" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogSystemReports_IFrame').src = 'WhitePage.aspx'; DialogSystemReports.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogSystemReports_topRightImage" style="display: block;"
                                    src="Images/Dialog/top_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogSystemReportsfooter">
                    <table id="tbl_DialogSystemReportsfooter" style="width: 993px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogSystemReports_downLeftImage" style="display: block;"
                                    src="Images/Dialog/down_left.gif" alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogSystemReports_downRightImage" style="display: block;"
                                    src="Images/Dialog/down_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogSystemReports_onShow" />
                <OnClose EventHandler="DialogSystemReports_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogMonthlyExceptionShiftsheader" FooterClientTemplateId="DialogMonthlyExceptionShiftsfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="Default" ID="DialogMonthlyExceptionShifts"
            runat="server" PreloadContentUrl="false" ContentUrl="MonthlyExceptionShifts.aspx" IFrameCssClass="MonthlyExceptionShifts_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogMonthlyExceptionShiftsheader">
                    <table id="tbl_DialogMonthlyExceptionShiftsheader" style="width: 993px" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogMonthlyExceptionShifts.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogMonthlyExceptionShifts_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogMonthlyExceptionShifts" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogMonthlyExceptionShifts" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogMonthlyExceptionShifts_IFrame').src = 'WhitePage.aspx'; DialogMonthlyExceptionShifts.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogMonthlyExceptionShifts_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogMonthlyExceptionShiftsfooter">
                    <table id="tbl_DialogMonthlyExceptionShiftsfooter" style="width: 993px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogMonthlyExceptionShifts_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogMonthlyExceptionShifts_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogMonthlyExceptionShifts_onShow" />
                <OnClose EventHandler="DialogMonthlyExceptionShifts_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <asp:HiddenField runat="server" ID="hfdmItemPersonnelMasterMonthlyOperationReport_DockMenu"
            meta:resourcekey="hfdmItemPersonnelMasterMonthlyOperationReport_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemManagerMasterMonthlyOperationReport_DockMenu"
            meta:resourcekey="hfdmItemManagerMasterMonthlyOperationReport_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemPersonnelIntroduction_DockMenu" meta:resourcekey="hfdmItemPersonnelIntroduction_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemReportsIntroduction_DockMenu" meta:resourcekey="hfdmItemReportsIntroduction_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemKartable_DockMenu" meta:resourcekey="hfdmItemKartable_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemSurveyedRequests_DockMenu" meta:resourcekey="hfdmItemSurveyedRequests_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemRegisteredRequests_DockMenu" meta:resourcekey="hfdmItemRegisteredRequests_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemTrafficOperationByOperator_DockMenu"
            meta:resourcekey="hfdmItemTrafficOperationByOperator_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemWorkFlowsView_DockMenu" meta:resourcekey="hfdmItemWorkFlowsView_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemWelcome_DockMenu" meta:resourcekey="hfdmItemWelcome_DockMenu" />
        <asp:HiddenField runat="server" ID="hfErrorType_MainForm" meta:resourcekey="hfErrorType_MainForm" />
        <asp:HiddenField runat="server" ID="hfConnectionError_MainForm" meta:resourcekey="hfConnectionError_MainForm" />
        <asp:HiddenField runat="server" ID="hfCurrentUILangID" />
        <asp:HiddenField runat="server" ID="hfCurrentSysLangID" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemHome" meta:resourcekey="hfTitle_qlItemHome" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemWorkFlowsView" meta:resourcekey="hfTitle_qlItemWorkFlows" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemTrafficsControl" meta:resourcekey="hfTitle_qlItemTrafficsControl" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemRegisteredRequests" meta:resourcekey="hfTitle_qlItemRegisteredRequests" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemSurveyedRequests" meta:resourcekey="hfTitle_qlItemSurveyedRequests" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemKartable" meta:resourcekey="hfTitle_qlItemKartable" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemReportsIntroduction" meta:resourcekey="hfTitle_qlItemReportsIntroduction" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemPersonnelIntroduction" meta:resourcekey="hfTitle_qlItemPersonnelIntroduction" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemManagerMasterMonthlyOperationReport"
            meta:resourcekey="hfTitle_qlItemManagerMasterMonthlyOperationReport" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemPersonnelMasterMonthlyOperationReport" meta:resourcekey="hfTitle_qlItemPersonnelMasterMonthlyOperationReport" />
        <asp:HiddenField runat="server" ID="hfAccessNoAllowdNavBarItemsList" />
    </form>
</body>
</html>
