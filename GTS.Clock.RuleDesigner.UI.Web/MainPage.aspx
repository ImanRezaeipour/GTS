<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="GTS.Clock.RuleDesigner.UI.Web.MainPage" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%--<%@ Register Assembly="FlashControl" Namespace="Bewise.Web.UI.WebControls" TagPrefix="Bewise" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link id="Link1" href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link2" href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link3" href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link4" href="css/navStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link5" href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link6" href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link7" href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link8" href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link9" href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link10" href="css/dockMenu.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link11" href="css/menuStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link12" href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body class="MainBodyStyle">
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="MainForm" runat="server" meta:resourcekey="MainForm">
        <asp:ScriptManager ID="ScriptManager_MainForm" runat="server"></asp:ScriptManager>
        <table style="width: 100%; height: 100%; font-size: small; font-family: Tahoma;"
            id="tblMaster_MainForm">
            <tr id="Tr1" runat="server" style="height: 80px;">
                <td style="height: 100%" valign="top" colspan="3">
                    <table style="width: 100%; height: 80px">
                        <tr style="height: 95%">
                            <td colspan="3">
                                <%--<Bewise:FlashControl ID="HeaderFlashControl" runat="server" Menu="false" Scale="Exactfit"
                                    WMode="Transparent" BrowserDetection="False" Height="80px" Width="100%" Quality="High"
                                    Loop="true" Play="false" />--%>
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
                                                        <asp:ImageButton ID="ImgbtnPersian" runat="server" OnClick="imgPersian_onClick" PostBackUrl="~/MainPage.aspx"
                                                            ImageUrl="~/Images/TopToolBar/Iran_flag.png" />
                                                    </td>
                                                    <td style="width: 5%">
                                                        <asp:ImageButton ID="ImgbtnEnglish" runat="server" OnClick="ImgbtnEnglish_onClick"
                                                            PostBackUrl="~/MainPage.aspx" ImageUrl="~/Images/TopToolBar/uk-flag.png" />
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
                                            <ComponentArt:ToolBar ID="tlbLogout" runat="server" DefaultItemActiveCssClass="itemActive"
                                                AutoPostBackOnSelect="true" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly" DefaultItemTextImageSpacing="0"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemLogout_tlbLogout" runat="server" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Exit.png" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItemLogout_tlbLogout" TextImageSpacing="5"
                                                        AutoPostBackOnSelect="True" />
                                                </Items>
                                            </ComponentArt:ToolBar>
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
                                    <img id="qlItemWorkFlows" src="images/DockMenu/email.png" alt="" /></a>
                            <a id="dmItemTrafficsControl_DockMenu" class="bottomDockMenu-item" href="#" onclick="DockMenuItem_onClick('dmItemTrafficsControl_DockMenu')">
                                <span></span>
                                <img id="qlItemTrafficsControl" src="images/DockMenu/portfolio.png" alt="" /></a><a
                                    id="dmItemRegisteredRequests_DockMenu" class="bottomDockMenu-item" href="#" onclick="DockMenuItem_onClick('dmItemRegisteredRequests_DockMenu')">
                                    <span></span>
                                    <img id="qlItemRegisteredRequests" src="images/DockMenu/video.png" alt="" /></a>
                            <a id="dmItemSurveyedRequests_DockMenu" class="bottomDockMenu-item" href="#" onclick="DockMenuItem_onClick('dmItemSurveyedRequests_DockMenu')">
                                <span></span>
                                <img id="qlItemSurveyedRequests" src="images/DockMenu/music.png" alt="" /></a>
                            <a id="dmItemCartable_DockMenu" class="bottomDockMenu-item" href="#" onclick="DockMenuItem_onClick('dmItemCartable_DockMenu')">
                                <span></span>
                                <img id="qlItemCartable" src="images/DockMenu/history.png" alt="" /></a> <a id="dmItemReportsIntroduction_DockMenu"
                                    class="bottomDockMenu-item" href="#" onclick="DockMenuItem_onClick('dmItemReportsIntroduction_DockMenu')">
                                    <span></span>
                                    <img id="qlItemReports" src="images/DockMenu/calendar.png" alt="" /></a>
                            <a id="dmItemPersonnelIntroduction_DockMenu" class="bottomDockMenu-item" href="#"
                                onclick="DockMenuItem_onClick('dmItemPersonnelIntroduction_DockMenu')"><span></span>
                                <img id="qlItemPersonnel" src="images/DockMenu/link.png" alt="" /></a> <a id="dmItemManagerMasterMonthlyOperationReport_DockMenu"
                                    class="bottomDockMenu-item" href="#" onclick="DockMenuItem_onClick('dmItemManagerMasterMonthlyOperationReport_DockMenu')">
                                    <span></span>
                                    <img id="qlItemManagersMonthlyOperationReport" src="images/DockMenu/rss.png" alt="" /></a>
                            <a id="dmItemPersonnelMasterMonthlyOperationReport_DockMenu" class="bottomDockMenu-item"
                                href="#" onclick="DockMenuItem_onClick('dmItemPersonnelMasterMonthlyOperationReport_DockMenu')">
                                <span></span>
                                <img id="qlItemMonthlyOperationReport" src="images/DockMenu/rss2.png" alt="" /></a>
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
                                    Expanded="true" ID="nvbItemBasicDefinitions_NavBarMain" SelectedLookId="TopItemLook"
                                    SubGroupCssClass="Level2Group" meta:resourcekey="nvbItemsBasicDefinitions_NavBarMain">
                                    <ComponentArt:NavBarItem Text="معرفی پست های سازمانی" meta:resourcekey="nvbItemPostsIntroduction_NavBarMain"
                                        ID="nvbItemPostsIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="پرسنل" meta:resourcekey="nvbItemPersonnelIntroduction_NavBarMain"
                                        ID="nvbItemPersonnelIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                </ComponentArt:NavBarItem>
                                <ComponentArt:NavBarItem Text="عملیات جانبی" DefaultSubItemLookId="Level2ItemLook"
                                    Expanded="false" ID="nvbItemLateralOperations_NavBarMain" SelectedLookId="TopItemLook"
                                    SubGroupCssClass="Level2Group" meta:resourcekey="nvbItemLateralOperations_NavBarMain">
                                    <ComponentArt:NavBarItem Text="معرفی کاربران" meta:resourcekey="nvbItemUsersIntroduction_NavBarMain"
                                        ID="nvbItemUsersIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="گزارش کارکرد ماهیانه" meta:resourcekey="nvbItemPersonnelMasterMonthlyOperationReport_NavBarMain"
                                        ID="nvbItemPersonnelMasterMonthlyOperationReport_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="مدیریت مفاهیم و قوانین" meta:resourcekey="nvbItemConceptRuleMasterOperation_NavBarMain"
                                        ID="nvbItemConceptRuleMasterOperation_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="مدیريت مولفه هاي ساخت مفاهيم و قوانين" meta:resourcekey="nvbItemExpressionsOperation_NavBarMain"
                                        ID="nvbItemExpressionsOperation_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                    <ComponentArt:NavBarItem Text="معرفی شیفت های استثناء" meta:resourcekey="nvbItemExceptionShiftsIntroduction_NavBarMain"
                                        ID="nvbItemExceptionShiftsIntroduction_NavBarMain">
                                    </ComponentArt:NavBarItem>
                                </ComponentArt:NavBarItem>
                            </Items>
                            <ClientEvents>
                                <ItemSelect EventHandler="NavBarMain_onItemSelect" />
                            </ClientEvents>
                        </ComponentArt:NavBar>
                    </div>
                </td>
                <td style="width: 78%; height: 100%;" valign="top">
                    <div class="TabStripContainer">
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
                                <iframe allowtransparency="true" runat="server" id="pgvWelcome_iFrame" src="about:blank"
                                    class="pgvWelcome_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvPostsIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvPostsIntroduction_iFrame"
                                    src="about:blank" class="pgvPostsIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvUsersIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvUsersIntroduction_iFrame"
                                    src="about:blank" class="pgvUsersIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvPersonnelIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvPersonnelIntroduction_iFrame"
                                    src="about:blank" class="pgvPersonnelIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvPersonnelMasterMonthlyOperationReport">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvPersonnelMasterMonthlyOperationReport_iFrame"
                                    src="about:blank" class="pgvPersonnelMasterMonthlyOperationReport_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvConceptRuleMasterOperation">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvConceptRuleMasterOperation_iFrame"
                                    src="about:blank" class="pgvConceptRuleMasterOperation_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvExceptionShiftsIntroduction">
                            <div style="width: 100%; margin: 0 auto;">
                                <iframe allowtransparency="true" runat="server" id="pgvExceptionShiftsIntroduction_iFrame"
                                    src="about:blank" class="pgvExceptionShiftsIntroduction_iFrame" frameborder="0"></iframe>
                            </div>
                        </ComponentArt:PageView>
                    </ComponentArt:MultiPage>
                </td>
                <td style="width: 1%; height: 100%" valign="top"></td>
            </tr>
        </table>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" Modal="false" AllowResize="false"
            runat="server" AllowDrag="false" Alignment="MiddleCentre" ID="DialogLoading">
            <Content>
                <img id="Img1" runat="server" alt="" src="~/Images/Dialog/loading2.gif" onclick="DialogLoading.Close();" />
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogLoding_onShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="Default" ID="DialogConceptsManagement"
            HeaderClientTemplateId="DialogConceptsManagementheader" FooterClientTemplateId="DialogConceptsManagementfooter"
            runat="server" PreloadContentUrl="false" ContentUrl="ConceptsManagement.aspx" IFrameCssClass="ConceptsManagement_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogConceptsManagementheader">
                    <table id="tbl_DialogConceptsManagementheader" style="width: 503px;" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogConceptsManagement.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogConceptsManagement_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogConceptsManagement" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogConceptsManagement" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogConceptsManagement_IFrame').src = 'WhitePage.aspx'; DialogConceptsManagement.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogConceptsManagement_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogConceptsManagementfooter">
                    <table id="tbl_DialogConceptsManagementfooter" style="width: 503px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogConceptsManagement_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogConceptsManagement_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogConceptsManagement_onShow" />
                <OnClose EventHandler="DialogConceptsManagement_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="Default" ID="DialogRulesManagement"
            HeaderClientTemplateId="DialogRulesManagementheader" FooterClientTemplateId="DialogRulesManagementfooter"
            runat="server" PreloadContentUrl="false" ContentUrl="RulesManagement.aspx" IFrameCssClass="RulesManagement_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogRulesManagementheader">
                    <table id="tbl_DialogRulesManagementheader" style="width: 503px;" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogRulesManagement.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogRulesManagement_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogRulesManagement" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogRulesManagement" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogRulesManagement_IFrame').src = 'WhitePage.aspx'; DialogRulesManagement.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogRulesManagement_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogRulesManagementfooter">
                    <table id="tbl_DialogRulesManagementfooter" style="width: 503px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogRulesManagement_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogRulesManagement_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogRulesManagement_onShow" />
                <OnClose EventHandler="DialogRulesManagement_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="True" AllowResize="false" AllowDrag="false" Alignment="Default" ID="DialogConceptRuleEditor"
            HeaderClientTemplateId="DialogConceptRuleEditorheader" FooterClientTemplateId="DialogConceptRuleEditorfooter"
            runat="server" PreloadContentUrl="false" ContentUrl="RulesManagement.aspx" IFrameCssClass="RulesManagement_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogConceptRuleEditorheader">
                    <table id="tbl_DialogConceptRuleEditorheader" style="width: 503px;" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogConceptRuleEditor.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogConceptRuleEditor_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogConceptRuleEditor" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogConceptRuleEditor" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogConceptRuleEditor_IFrame').src = 'WhitePage.aspx'; DialogConceptRuleEditor.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogConceptRuleEditor_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogConceptRuleEditorfooter">
                    <table id="tbl_DialogConceptRuleEditorfooter" style="width: 503px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogConceptRuleEditor_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogConceptRuleEditor_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogConceptRuleEditor_onShow" />
                <OnClose EventHandler="DialogConceptRuleEditor_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="True" AllowResize="false" AllowDrag="false" Alignment="Default" ID="DialogExpressionsManagement"
            HeaderClientTemplateId="DialogExpressionsManagementheader" FooterClientTemplateId="DialogExpressionsManagementfooter"
            runat="server" PreloadContentUrl="false" ContentUrl="ExpressionsManagement.aspx" IFrameCssClass="ExpressionsManagement_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogExpressionsManagementheader">
                    <table id="tbl_DialogExpressionsManagementheader" style="width: 503px;" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogExpressionsManagement.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogExpressionsManagement_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogExpressionsManagement" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogExpressionsManagement" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogExpressionsManagement_IFrame').src = 'WhitePage.aspx'; DialogExpressionsManagement.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogExpressionsManagement_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogExpressionsManagementfooter">
                    <table id="tbl_DialogExpressionsManagementfooter" style="width: 503px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogExpressionsManagement_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogExpressionsManagement_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogExpressionsManagement_onShow" />
                <OnClose EventHandler="DialogExpressionsManagement_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <asp:HiddenField runat="server" ID="hfdmItemPersonnelMasterMonthlyOperationReport_DockMenu" meta:resourcekey="hfdmItemPersonnelMasterMonthlyOperationReport_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemManagerMasterMonthlyOperationReport_DockMenu" meta:resourcekey="hfdmItemManagerMasterMonthlyOperationReport_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemPersonnelIntroduction_DockMenu" meta:resourcekey="hfdmItemPersonnelIntroduction_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemReportsIntroduction_DockMenu" meta:resourcekey="hfdmItemReportsIntroduction_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemCartable_DockMenu" meta:resourcekey="hfdmItemCartable_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemSurveyedRequests_DockMenu" meta:resourcekey="hfdmItemSurveyedRequests_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemRegisteredRequests_DockMenu" meta:resourcekey="hfdmItemRegisteredRequests_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemTrafficOperationByOperator_DockMenu" meta:resourcekey="hfdmItemTrafficOperationByOperator_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemWorkFlowsView_DockMenu" meta:resourcekey="hfdmItemWorkFlowsView_DockMenu" />
        <asp:HiddenField runat="server" ID="hfdmItemWelcome_DockMenu" meta:resourcekey="hfdmItemWelcome_DockMenu" />
        <%--        <asp:HiddenField runat="server" ID="hfErrorType_MainForm" meta:resourcekey="hfErrorType_MainForm" />
        <asp:HiddenField runat="server" ID="hfConnectionError_MainForm" meta:resourcekey="hfConnectionError_MainForm" />--%>
        <asp:HiddenField runat="server" ID="hfCurrentUILangID" />
        <asp:HiddenField runat="server" ID="hfCurrentSysLangID" />
        <%--        <asp:HiddenField runat="server" ID="hfTitle_qlItemHome" meta:resourcekey="hfTitle_qlItemHome" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemWorkFlows" meta:resourcekey="hfTitle_qlItemWorkFlows" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemTrafficsControl" meta:resourcekey="hfTitle_qlItemTrafficsControl" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemRegisteredRequests" meta:resourcekey="hfTitle_qlItemRegisteredRequests" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemSurveyedRequests" meta:resourcekey="hfTitle_qlItemSurveyedRequests" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemCartable" meta:resourcekey="hfTitle_qlItemCartable" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemReports" meta:resourcekey="hfTitle_qlItemReports" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemPersonnel" meta:resourcekey="hfTitle_qlItemPersonnel" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemManagersMonthlyOperationReport" meta:resourcekey="hfTitle_qlItemManagersMonthlyOperationReport" />
        <asp:HiddenField runat="server" ID="hfTitle_qlItemMonthlyOperationReport" meta:resourcekey="hfTitle_qlItemMonthlyOperationReport" />--%>
    </form>
</body>
</html>
