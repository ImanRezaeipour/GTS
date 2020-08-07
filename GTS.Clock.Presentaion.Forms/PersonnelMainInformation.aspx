<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.PersonnelMainInformation"
    CodeFile="PersonnelMainInformation.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="FUA" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/navStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/upload.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="PersonnelMainInformationForm" runat="server" meta:resourcekey="PersonnelMainInformationForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 99%; font-family: Arial; font-size: small;" class="BoxStyle">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 18%">
                                <ComponentArt:ToolBar ID="TlbPersonnelMainInformation" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbPersonnelMainInformation" runat="server"
                                            ClientSideCommand="tlbItemSave_TlbPersonnelMainInformation_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemSave_TlbPersonnelMainInformation"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbPersonnelMainInformation" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbPersonnelMainInformation"
                                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbPersonnelMainInformation_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExtraInformation_TlbPersonnelMainInformation"
                                            runat="server" ClientSideCommand="tlbItemExtraInformation_TlbPersonnelMainInformation_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="view_detailed.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExtraInformation_TlbPersonnelMainInformation"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbPersonnelMainInformation"
                                            runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbPersonnelMainInformation_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbPersonnelMainInformation"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPersonnelMainInformation" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbPersonnelMainInformation"
                                            ClientSideCommand="tlbItemExit_TlbPersonnelMainInformation_onClick();" TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td style="width: 65%">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 2%">
                                            <input id="chbActive_DialogPersonnelMainInformation" type="checkbox" checked="checked" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblActive_DialogPersonnelMainInformation" runat="server" Text="فعال"
                                                meta:resourcekey="lblActive_DialogPersonnelMainInformation" CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td id="ActionMode_DialogPersonnelMainInformation" class="ToolbarMode"></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 2%" rowspan="2">&nbsp; &nbsp; &nbsp;
                <ComponentArt:ToolBar ID="TlbVPersonnelMainInformation" runat="server" CssClass="verticaltoolbar"
                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                    DefaultItemTextImageSpacing="0" Orientation="Vertical" ImagesBaseUrl="images/ToolBar/"
                    ItemSpacing="1px" UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbVPersonnelMainInformation" runat="server"
                            ClientSideCommand="tlbItemSave_TlbPersonnelMainInformation_onClick();" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemSave_TlbVPersonnelMainInformation"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbVPersonnelMainInformation" runat="server"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbVPersonnelMainInformation"
                            ClientSideCommand="tlbItemExit_TlbPersonnelMainInformation_onClick();" TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbVPersonnelMainInformation" runat="server"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbVPersonnelMainInformation"
                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbVPersonnelMainInformation_onClick();" />
                        <ComponentArt:ToolBarItem ID="tlbItemExtraInformation_TlbVPersonnelMainInformation"
                            runat="server" ClientSideCommand="tlbItemExtraInformation_TlbPersonnelMainInformation_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="view_detailed.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExtraInformation_TlbVPersonnelMainInformation"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbVPersonnelMainInformation" runat="server"
                            ClientSideCommand="tlbItemRefresh_TlbVPersonnelMainInformation_onClick();" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbVPersonnelMainInformation"
                            TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; border-top: gray 1px double; border-right: gray 1px double; font-size: small; border-left: gray 1px double; border-bottom: gray 1px double;">
                        <tr>
                            <td id="Td1" runat="server" style="width: 12%">
                                <asp:Label ID="lblName_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblName_DialogPersonnelMainInformation"
                                    Text="نام :" CssClass="WhiteLabel" Style="color: #B22222"></asp:Label>
                            </td>
                            <td id="Td2" runat="server" meta:resourcekey="tdPair_DialogPersonnelMainInformation"
                                style="width: 37%">
                                <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtFirstName_DialogPersonnelMainInformation"
                                    onselect="this.select();" onfocus="this.select();" />
                            </td>
                            <td id="Td3" runat="server" style="width: 12%">
                                <asp:Label ID="lblFamily_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblFamily_DialogPersonnelMainInformation"
                                    Text="نام خانوادگی :" CssClass="WhiteLabel" Style="color: #B22222"></asp:Label>
                            </td>
                            <td id="Td4" runat="server" meta:resourcekey="tdPair_DialogPersonnelMainInformation"
                                style="width: 37%">
                                <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtLastName_DialogPersonnelMainInformation"
                                    onselect="this.select();" onfocus="this.select();" />
                            </td>
                        </tr>
                        <tr>
                            <td id="Td5" runat="server" style="width: 12%">
                                <asp:Label ID="lblSex_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblSex_DialogPersonnelMainInformation"
                                    Text="جنسیت :" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td id="test" runat="server" meta:resourcekey="tdPair_DialogPersonnelMainInformation"
                                style="width: 37%">
                                <ComponentArt:CallBack runat="server" ID="CallBack_cmbSex_DialogPersonnelMainInformation"
                                    Height="26" OnCallback="CallBack_cmbSex_DialogPersonnelMainInformation_onCallBack">
                                    <Content>
                                        <ComponentArt:ComboBox ID="cmbSex_DialogPersonnelMainInformation" runat="server"
                                            AutoComplete="false" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                            DropDownCssClass="comboDropDown" DropDownResizingMode="Off" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                            TextBoxCssClass="comboTextBox" Width="100%" TextBoxEnabled="true">
                                            <ClientEvents>
                                                <Expand EventHandler="cmbSex_DialogPersonnelMainInformation_onExpand" />
                                                <Collapse EventHandler="cmbSex_DialogPersonnelMainInformation_onCollapse" />
                                            </ClientEvents>
                                        </ComponentArt:ComboBox>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_Sex_DialogPersonnelMainInformation" />
                                    </Content>
                                    <ClientEvents>
                                        <BeforeCallback EventHandler="CallBack_cmbSex_DialogPersonnelMainInformation_onBeforeCallback" />
                                        <CallbackComplete EventHandler="CallBack_cmbSex_DialogPersonnelMainInformation_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_cmbSex_DialogPersonnelMainInformation_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                            <td id="Td6" runat="server" style="width: 12%">
                                <asp:Label ID="lblFatherName_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblFatherName_DialogPersonnelMainInformation"
                                    Text="نام پدر :" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td id="Td7" runat="server" meta:resourcekey="tdPair_DialogPersonnelMainInformation"
                                style="width: 37%">
                                <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtFatherName_DialogPersonnelMainInformation"
                                    onselect="this.select();" onfocus="this.select();" />
                            </td>
                        </tr>
                        <tr>
                            <td id="Td8" runat="server">
                                <asp:Label ID="lblNationalCode_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblNationalCode_DialogPersonnelMainInformation"
                                    Text="کد ملی :" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td>
                                <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtNationalCode_DialogPersonnelMainInformation"
                                    onselect="this.select();" onfocus="this.select();" />
                            </td>
                            <td id="Td9" runat="server">
                                <asp:Label ID="lblMilitaryState_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblMilitaryState_DialogPersonnelMainInformation"
                                    Text="وضعیت نظام وظیفه :" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td>
                                <ComponentArt:CallBack runat="server" ID="CallBack_cmbMilitaryState_DialogPersonnelMainInformation"
                                    OnCallback="CallBack_cmbMilitaryState_DialogPersonnelMainInformation_onCallBack"
                                    Height="26">
                                    <Content>
                                        <ComponentArt:ComboBox ID="cmbMilitaryState_DialogPersonnelMainInformation" runat="server"
                                            AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                            DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                            TextBoxCssClass="comboTextBox" Style="width: 100%" DropDownHeight="170" TextBoxEnabled="true">
                                            <ClientEvents>
                                                <Expand EventHandler="cmbMilitaryState_DialogPersonnelMainInformation_onExpand" />
                                                <Collapse EventHandler="cmbMilitaryState_DialogPersonnelMainInformation_onCollapse" />
                                            </ClientEvents>
                                        </ComponentArt:ComboBox>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_MilitaryState_DialogPersonnelMainInformation" />
                                    </Content>
                                    <ClientEvents>
                                        <BeforeCallback EventHandler="CallBack_cmbMilitaryState_DialogPersonnelMainInformation_onBeforeCallback" />
                                        <CallbackComplete EventHandler="CallBack_cmbMilitaryState_DialogPersonnelMainInformation_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_cmbMilitaryState_DialogPersonnelMainInformation_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                        <tr>
                            <td id="Td10" runat="server">
                                <asp:Label ID="lblIdentityCertificate_DialogPersonnelMainInformation" runat="server"
                                    meta:resourcekey="lblBirthCertificateID_DialogPersonnelMainInformation" Text="شماره شناسنامه :"
                                    CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td>
                                <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtIdentityCertificate_DialogPersonnelMainInformation"
                                    onselect="this.select();" onfocus="this.select();" />
                            </td>
                            <td id="Td11" runat="server">
                                <asp:Label ID="lblIssuanceLocation_DialogPersonnelMainInformation" runat="server"
                                    meta:resourcekey="lblIssuanceLocation_DialogPersonnelMainInformation" Text="محل صدور :"
                                    CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td>
                                <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtIssuanceLocation_DialogPersonnelMainInformation"
                                    onselect="this.select();" onfocus="this.select();" />
                            </td>
                        </tr>
                        <tr>
                            <td id="Td12" runat="server">
                                <asp:Label ID="lblEducation_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblEducation_DialogPersonnelMainInformation"
                                    Text="تحصیلات :" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td>
                                <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtEducation_DialogPersonnelMainInformation"
                                    onselect="this.select();" onfocus="this.select();" />
                            </td>
                            <td id="Td13" runat="server">
                                <asp:Label ID="lblMarriageState_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblMarriageState_DialogPersonnelMainInformation"
                                    Text="وضعیت تاهل :" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td>
                                <ComponentArt:CallBack runat="server" ID="CallBack_cmbMarriageState_DialogPersonnelMainInformation"
                                    OnCallback="CallBack_cmbMarriageState_DialogPersonnelMainInformation_onCallBack"
                                    Height="26">
                                    <Content>
                                        <ComponentArt:ComboBox ID="cmbMarriageState_DialogPersonnelMainInformation" runat="server"
                                            AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                            DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                            TextBoxCssClass="comboTextBox" Width="100%" TextBoxEnabled="true">
                                            <ClientEvents>
                                                <Expand EventHandler="cmbMarriageState_DialogPersonnelMainInformation_onExpand" />
                                                <Collapse EventHandler="cmbMarriageState_DialogPersonnelMainInformation_onCollapse" />
                                            </ClientEvents>
                                        </ComponentArt:ComboBox>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_MarriageState_DialogPersonnelMainInformation" />
                                    </Content>
                                    <ClientEvents>
                                        <BeforeCallback EventHandler="CallBack_cmbMarriageState_DialogPersonnelMainInformation_onBeforeCallback" />
                                        <CallbackComplete EventHandler="CallBack_cmbMarriageState_DialogPersonnelMainInformation_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_cmbMarriageState_DialogPersonnelMainInformation_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                        <tr>
                            <td id="Td14" runat="server">
                                <asp:Label ID="lblMobileNumber_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblMobileNumber_DialogPersonnelMainInformation"
                                    Text="تلفن همراه :" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td>
                                <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtMobileNumber_DialogPersonnelMainInformation"
                                    onselect="this.select();" onfocus="this.select();" />
                            </td>
                            <td id="Td15" runat="server">
                                <asp:Label ID="lblTel_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblTel_DialogPersonnelMainInformation"
                                    Text="تلفن ثابت :" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td>
                                <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtTel_DialogPersonnelMainInformation"
                                    onselect="this.select();" onfocus="this.select();" />
                            </td>
                        </tr>
                        <tr>
                            <td id="Td16" runat="server">
                                <asp:Label ID="lblAddress_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblAddress_DialogPersonnelMainInformation"
                                    Text="آدرس :" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td colspan="3">
                                <textarea cols="20" name="S1" rows="1" style="width: 99%;" class="TextBoxes" id="txtAddress_DialogPersonnelMainInformation"
                                    onselect="this.select();" onfocus="this.select();"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td id="Td17" runat="server">
                                <asp:Label ID="lblBirthDate_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblBirthDate_DialogPersonnelMainInformation"
                                    Text="تاریخ تولد :" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td id="Container_BirthDateCalendars_DialogPersonnelMainInformation">
                                            <table runat="server" id="Container_pdpBirthDate_DialogPersonnelMainInformation"
                                                visible="false" style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <pcal:PersianDatePickup ID="pdpBirthDate_DialogPersonnelMainInformation" runat="server"
                                                            CssClass="PersianDatePicker"></pcal:PersianDatePickup>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table runat="server" id="Container_gdpBirthDate_DialogPersonnelMainInformation"
                                                visible="false" style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalBirthDate_DialogPersonnelMainInformation">
                                                            <tr>
                                                                <td onmouseup="btn_gdpBirthDate_DialogPersonnelMainInformation_OnMouseUp(event)">
                                                                    <ComponentArt:Calendar ID="gdpBirthDate_DialogPersonnelMainInformation" runat="server"
                                                                        ControlType="Picker" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                        PickerFormat="Custom" SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gdpBirthDate_DialogPersonnelMainInformation_OnDateChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                                <td style="font-size: 10px;">&nbsp;
                                                                </td>
                                                                <td>
                                                                    <img id="btn_gdpBirthDate_DialogPersonnelMainInformation" alt="" class="calendar_button"
                                                                        onclick="btn_gdpBirthDate_DialogPersonnelMainInformation_OnClick(event)" onmouseup="btn_gdpBirthDate_DialogPersonnelMainInformation_OnMouseUp(event)"
                                                                        src="Images/Calendar/btn_calendar.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <ComponentArt:Calendar ID="gCalBirthDate_DialogPersonnelMainInformation" runat="server"
                                                            AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                            CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                            DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                            ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                            NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                            PopUpExpandControlId="btn_gdpBirthDate_DialogPersonnelMainInformation" PrevImageUrl="cal_prevMonth.gif"
                                                            SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                            SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                            <ClientEvents>
                                                                <SelectionChanged EventHandler="gCalBirthDate_DialogPersonnelMainInformation_OnChange" />
                                                                <Load EventHandler="gCalBirthDate_DialogPersonnelMainInformation_onLoad" />
                                                            </ClientEvents>
                                                        </ComponentArt:Calendar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <ComponentArt:ToolBar ID="TlbClear_BirthDateCalendars_DialogPersonnelMainInformation"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_BirthDateCalendars_DialogPersonnelMainInformation"
                                                        runat="server" ClientSideCommand="tlbItemClear_TlbClear_BirthDateCalendars_DialogPersonnelMainInformation_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_BirthDateCalendars_DialogPersonnelMainInformation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td runat="server">
                                <asp:Label ID="lblBirthLocation_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblBirthLocation_DialogPersonnelMainInformation"
                                    Text="محل تولد :" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td>
                                <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtBirthLocation_DialogPersonnelMainInformation"
                                    onselect="this.select();" onfocus="this.select();" />
                            </td>
                        </tr>
                        <tr>
                            <td runat="server">
                                <asp:Label ID="lblPersonnelNumber_DialogPersonnelMainInformation" runat="server"
                                    meta:resourcekey="lblPersonnelNumber_DialogPersonnelMainInformation" Text="شماره پرسنلی :"
                                    CssClass="WhiteLabel" Style="color: #B22222"></asp:Label>
                            </td>
                            <td>
                                <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtPersonnelNumber_DialogPersonnelMainInformation"
                                    onselect="this.select();" onfocus="this.select();" />
                            </td>
                            <td runat="server">
                                <asp:Label ID="lblCardNumber_DialogPersonnelMainInformation" runat="server" CssClass="WhiteLabel"
                                    meta:resourcekey="lblCardID_DialogPersonnelMainInformation" Text="شماره کارت :"></asp:Label>
                            </td>
                            <td>
                                <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtCardNumber_DialogPersonnelMainInformation"
                                    onselect="this.select();" onfocus="this.select();" />
                            </td>
                        </tr>
                        <tr>
                            <td runat="server">
                                <asp:Label ID="lblDepartment_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblDepartment_DialogPersonnelMainInformation"
                                    Text="بخش :" CssClass="WhiteLabel" Style="color: #B22222"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbDepartment_DialogPersonnelMainInformation"
                                                OnCallback="CallBack_cmbDepartment_DialogPersonnelMainInformation_onCallBack"
                                                Height="26">
                                                <Content>
                                                    <ComponentArt:ComboBox ID="cmbDepartment_DialogPersonnelMainInformation" runat="server"
                                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                        DropDownCssClass="comboDropDown" DropDownHeight="190" DropDownResizingMode="Corner"
                                                        DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                        ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                        Width="100%" ExpandDirection="Up" TextBoxEnabled="true">
                                                        <DropDownContent>
                                                            <ComponentArt:TreeView ID="trvDepartment_DialogPersonnelMainInformation" runat="server"
                                                                CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                                                DefaultImageWidth="16" DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15"
                                                                ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" Height="98%"
                                                                HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20"
                                                                LineImagesFolderUrl="Images/TreeView/LeftLines" LineImageWidth="19" NodeCssClass="TreeNode"
                                                                NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode"
                                                                ShowLines="true" Width="100%" meta:resourcekey="trvLineImages">
                                                                <ClientEvents>
                                                                    <NodeSelect EventHandler="trvDepartment_DialogPersonnelMainInformation_onNodeSelect" />
                                                                </ClientEvents>
                                                            </ComponentArt:TreeView>
                                                        </DropDownContent>
                                                        <ClientEvents>
                                                            <Expand EventHandler="cmbDepartment_DialogPersonnelMainInformation_onExpand" />
                                                            <Collapse EventHandler="cmbDepartment_DialogPersonnelMainInformation_onCollapse" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_Department_DialogPersonnelMainInformation" />
                                                </Content>
                                                <ClientEvents>
                                                    <BeforeCallback EventHandler="CallBack_cmbDepartment_DialogPersonnelMainInformation_onBeforeCallback" />
                                                    <CallbackComplete EventHandler="CallBack_cmbDepartment_DialogPersonnelMainInformation_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_cmbDepartment_DialogPersonnelMainInformation_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                        <td style="width: 5%">
                                            <ComponentArt:ToolBar ID="TlbRefresh_cmbDepartment_DialogPersonnelMainInformation"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbDepartment_DialogPersonnelMainInformation"
                                                        runat="server" ClientSideCommand="Refresh_cmbDepartment_DialogPersonnelMainInformation();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbDepartment_DialogPersonnelMainInformation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                        <td style="width: 5%">
                                            <ComponentArt:ToolBar ID="TlbParentDepartments_cmbDepartment_DialogPersonnelMainInformation"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemParentDepartments_TlbParentDepartments_cmbDepartment_DialogPersonnelMainInformation"
                                                        runat="server" ClientSideCommand="tlbItemParentDepartments_TlbParentDepartments_cmbDepartment_DialogPersonnelMainInformation_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemParentDepartments_TlbParentDepartments_cmbDepartment_DialogPersonnelMainInformation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td id="Td18" runat="server">
                                <asp:Label ID="lblOrganizationPost_DialogPersonnelMainInformation" runat="server"
                                    CssClass="WhiteLabel" meta:resourcekey="lblOrganizationPost_DialogPersonnelMainInformation"
                                    Text="پست سازمانی :"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbOrganizationPost_DialogPersonnelMainInformation"
                                                OnCallback="CallBack_cmbOrganizationPost_DialogPersonnelMainInformation_onCallBack"
                                                Height="26">
                                                <Content>
                                                    <ComponentArt:ComboBox ID="cmbOrganizationPost_DialogPersonnelMainInformation" runat="server"
                                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                        DropDownCssClass="comboDropDown" DropDownHeight="190" DropDownResizingMode="Corner"
                                                        DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                        ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                        Width="100%" ExpandDirection="Up" TextBoxEnabled="true">
                                                        <DropDownContent>
                                                            <ComponentArt:TreeView ID="trvOrganizationPost_DialogPersonnelMainInformation" runat="server"
                                                                CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                                                DefaultImageWidth="16" DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15"
                                                                ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" Height="98%"
                                                                HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20"
                                                                LineImagesFolderUrl="Images/TreeView/LeftLines" LineImageWidth="19" NodeCssClass="TreeNode"
                                                                NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode"
                                                                ShowLines="true" Width="100%" meta:resourcekey="trvLineImages">
                                                                <ClientEvents>
                                                                    <NodeSelect EventHandler="trvOrganizationPost_DialogPersonnelMainInformation_onNodeSelect" />
                                                                    <CallbackComplete EventHandler="trvOrganizationPost_DialogPersonnelMainInformation_onCallbackComplete" />
                                                                    <NodeBeforeExpand EventHandler="trvOrganizationPost_DialogPersonnelMainInformation_onNodeBeforeExpand" />
                                                                </ClientEvents>
                                                            </ComponentArt:TreeView>
                                                        </DropDownContent>
                                                        <ClientEvents>
                                                            <Expand EventHandler="cmbOrganizationPost_DialogPersonnelMainInformation_onExpand" />
                                                            <Collapse EventHandler="cmbOrganizationPost_DialogPersonnelMainInformation_onCollapse" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_OrganizationPost_DialogPersonnelMainInformation" />
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="CallBack_cmbOrganizationPost_DialogPersonnelMainInformation_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_cmbOrganizationPost_DialogPersonnelMainInformation_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                        <td style="width: 5%">
                                            <ComponentArt:ToolBar ID="TlbRefresh_cmbOrganizationPost_DialogPersonnelMainInformation"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbOrganizationPost_DialogPersonnelMainInformation"
                                                        runat="server" ClientSideCommand="Refresh_cmbOrganizationPost_DialogPersonnelMainInformation();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbOrganizationPost_DialogPersonnelMainInformation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                        <td style="width: 5%">
                                            <ComponentArt:ToolBar ID="TlbClear_cmbOrganizationPost_DialogPersonnelMainInformation"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_cmbOrganizationPost_DialogPersonnelMainInformation"
                                                        runat="server" ClientSideCommand="tlbItemClear_TlbClear_cmbOrganizationPost_DialogPersonnelMainInformation_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_cmbOrganizationPost_DialogPersonnelMainInformation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td runat="server">
                                <asp:Label ID="lblEmployType_DialogPersonnelMainInformation" runat="server" CssClass="WhiteLabel"
                                    meta:resourcekey="lblEmployType_DialogPersonnelMainInformation" Text="نوع استخدام :"
                                    Style="color: #B22222"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbEmployType_DialogPersonnelMainInformation"
                                                OnCallback="CallBack_cmbEmployType_DialogPersonnelMainInformation_onCallBack"
                                                Height="26">
                                                <Content>
                                                    <ComponentArt:ComboBox ID="cmbEmployType_DialogPersonnelMainInformation" runat="server"
                                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                        DataTextField="Name" DataValueField="ID" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                        DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                        ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                        Width="100%" ExpandDirection="Up" TextBoxEnabled="true">
                                                        <ClientEvents>
                                                            <Expand EventHandler="cmbEmployType_DialogPersonnelMainInformation_onExpand" />
                                                            <Collapse EventHandler="cmbEmployType_DialogPersonnelMainInformation_onCollapse" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_EmployType_DialogPersonnelMainInformation" />
                                                </Content>
                                                <ClientEvents>
                                                    <BeforeCallback EventHandler="CallBack_cmbEmployType_DialogPersonnelMainInformation_onBeforeCallback" />
                                                    <CallbackComplete EventHandler="CallBack_cmbEmployType_DialogPersonnelMainInformation_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_cmbEmployType_DialogPersonnelMainInformation_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                        <td style="width: 5%">
                                            <ComponentArt:ToolBar ID="TlbRefresh_cmbEmployType_DialogPersonnelMainInformation"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbEmployType_DialogPersonnelMainInformation"
                                                        runat="server" ClientSideCommand="Refresh_cmbEmployType_DialogPersonnelMainInformation();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbEmployType_DialogPersonnelMainInformation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td runat="server">
                                <asp:Label ID="lblControlStation_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblControlStation_DialogPersonnelMainInformation"
                                    Text="ایستگاه کنترل :" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbControlStation_DialogPersonnelMainInformation"
                                                OnCallback="CallBack_cmbControlStation_DialogPersonnelMainInformation_onCallBack"
                                                Height="26">
                                                <Content>
                                                    <ComponentArt:ComboBox ID="cmbControlStation_DialogPersonnelMainInformation" runat="server"
                                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                        DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                        TextBoxCssClass="comboTextBox" Width="100%" ExpandDirection="Up" DataTextField="Name"
                                                        DataValueField="ID" TextBoxEnabled="true">
                                                        <ClientEvents>
                                                            <Expand EventHandler="cmbControlStation_DialogPersonnelMainInformation_onExpand" />
                                                            <Collapse EventHandler="cmbControlStation_DialogPersonnelMainInformation_onCollapse" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_ControlStation_DialogPersonnelMainInformation" />
                                                </Content>
                                                <ClientEvents>
                                                    <BeforeCallback EventHandler="CallBack_cmbControlStation_DialogPersonnelMainInformation_onBeforeCallback" />
                                                    <CallbackComplete EventHandler="CallBack_cmbControlStation_DialogPersonnelMainInformation_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_cmbControlStation_DialogPersonnelMainInformation_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                        <td style="width: 5%">
                                            <ComponentArt:ToolBar ID="TlbRefresh_cmbControlStation_DialogPersonnelMainInformation"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbControlStation_DialogPersonnelMainInformation"
                                                        runat="server" ClientSideCommand="Refresh_cmbControlStation_DialogPersonnelMainInformation();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbControlStation_DialogPersonnelMainInformation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                        <td style="width: 5%">
                                            <ComponentArt:ToolBar ID="TlbClear_cmbControlStation_DialogPersonnelMainInformation"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_cmbControlStation_DialogPersonnelMainInformation"
                                                        runat="server" ClientSideCommand="tlbItemClear_TlbClear_cmbControlStation_DialogPersonnelMainInformation_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_cmbControlStation_DialogPersonnelMainInformation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td runat="server">
                                <asp:Label ID="lblEmployNumber_DialogPersonnelMainInformation" runat="server" CssClass="WhiteLabel"
                                    meta:resourcekey="lblEmployNumber_DialogPersonnelMainInformation" Text="شماره استخدامی :"></asp:Label>
                            </td>
                            <td>
                                <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtEmployNumber_DialogPersonnelMainInformation"
                                    onselect="this.select();" onfocus="this.select();" />
                            </td>
                            <td runat="server">
                                <asp:Label ID="lblUserInterfaceRuleGroup_DialogPersonnelMainInformation" runat="server"
                                    meta:resourcekey="lblUserInterfaceRuleGroup_DialogPersonnelMainInformation" Text="گروه قانون واسط کاربری :"
                                    CssClass="WhiteLabel" Style="color: #B22222"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation"
                                                OnCallback="CallBack_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_onCallBack"
                                                Height="26">
                                                <Content>
                                                    <ComponentArt:ComboBox ID="cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation"
                                                        runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                        DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                        TextBoxCssClass="comboTextBox" Width="100%" ExpandDirection="Up" DataTextField="Name"
                                                        DataValueField="ID" TextBoxEnabled="true">
                                                        <ClientEvents>
                                                            <Expand EventHandler="cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_onExpand" />
                                                            <Collapse EventHandler="cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_onCollapse" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_UserInterfaceRuleGroup_DialogPersonnelMainInformation" />
                                                </Content>
                                                <ClientEvents>
                                                    <BeforeCallback EventHandler="CallBack_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_onBeforeCallback" />
                                                    <CallbackComplete EventHandler="CallBack_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                        <td style="width: 5%">
                                            <ComponentArt:ToolBar ID="TlbRefresh_CallBack_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation"
                                                        runat="server" ClientSideCommand="Refresh_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td runat="server">
                                <asp:Label ID="lblEmployDate_WorkStart_DialogPersonnelMainInformation" runat="server"
                                    CssClass="WhiteLabel" meta:resourcekey="lblEmployDate_WorkStart_DialogPersonnelMainInformation"
                                    Text="تاریخ استخدام :"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td id="Container_EmployDateCalendars_WorkStart_DialogPersonnelMainInformation">
                                            <table runat="server" id="Container_pdpEmployDate_WorkStart_DialogPersonnelMainInformation"
                                                visible="false" style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <pcal:PersianDatePickup ID="pdpEmployDate_WorkStart_DialogPersonnelMainInformation"
                                                            runat="server" CssClass="PersianDatePicker"></pcal:PersianDatePickup>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table runat="server" id="Container_gdpEmployDate_WorkStart_DialogPersonnelMainInformation"
                                                visible="false" style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalEmployDate_WorkStart_DialogPersonnelMainInformation">
                                                            <tr>
                                                                <td onmouseup="btn_gdpEmployDate_WorkStart_DialogPersonnelMainInformation_OnMouseUp(event)">
                                                                    <ComponentArt:Calendar ID="gdpEmployDate_WorkStart_DialogPersonnelMainInformation"
                                                                        runat="server" ControlType="Picker" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                        PickerFormat="Custom" SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gdpEmployDate_WorkStart_DialogPersonnelMainInformation_OnDateChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                                <td style="font-size: 10px;">&nbsp;
                                                                </td>
                                                                <td>
                                                                    <img id="btn_gdpEmployDate_WorkStart_DialogPersonnelMainInformation" alt="" class="calendar_button"
                                                                        onclick="btn_gdpEmployDate_WorkStart_DialogPersonnelMainInformation_OnClick(event)"
                                                                        onmouseup="btn_gdpEmployDate_WorkStart_DialogPersonnelMainInformation_OnMouseUp(event)"
                                                                        src="Images/Calendar/btn_calendar.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <ComponentArt:Calendar ID="gCalEmployDate_WorkStart_DialogPersonnelMainInformation"
                                                            runat="server" AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                            CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                            DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                            ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                            NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                            PopUpExpandControlId="btn_gdpEmployDate_WorkStart_DialogPersonnelMainInformation"
                                                            PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                            SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                            <ClientEvents>
                                                                <SelectionChanged EventHandler="gCalEmployDate_WorkStart_DialogPersonnelMainInformation_OnChange" />
                                                                <Load EventHandler="gCalEmployDate_WorkStart_DialogPersonnelMainInformation_onLoad" />
                                                            </ClientEvents>
                                                        </ComponentArt:Calendar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <ComponentArt:ToolBar ID="TlbClear_EmployDateCalendars_DialogPersonnelMainInformation"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_EmployDateCalendars_DialogPersonnelMainInformation"
                                                        runat="server" ClientSideCommand="tlbItemClear_TlbClear_EmployDateCalendars_DialogPersonnelMainInformation_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_EmployDateCalendars_DialogPersonnelMainInformation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td runat="server">
                                <asp:Label ID="lblEmployEndDate_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblEmployEndDate_DialogPersonnelMainInformation"
                                    Text="تاریخ پایان استخدام :" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td id="Container_EmployEndDateCalendars_DialogPersonnelMainInformation">
                                            <table runat="server" id="Container_pdpEmployEndDate_DialogPersonnelMainInformation"
                                                visible="false" style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <pcal:PersianDatePickup ID="pdpEmployEndDate_DialogPersonnelMainInformation" runat="server"
                                                            CssClass="PersianDatePicker"></pcal:PersianDatePickup>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table runat="server" id="Container_gdpEmployEndDate_DialogPersonnelMainInformation"
                                                visible="false" style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalEmployEndDate_DialogPersonnelMainInformation">
                                                            <tr>
                                                                <td onmouseup="btn_gdpEmployEndDate_DialogPersonnelMainInformation_OnMouseUp(event)">
                                                                    <ComponentArt:Calendar ID="gdpEmployEndDate_DialogPersonnelMainInformation" runat="server"
                                                                        ControlType="Picker" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                        PickerFormat="Custom" SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gdpEmployEndDate_DialogPersonnelMainInformation_OnDateChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                                <td style="font-size: 10px;">&nbsp;
                                                                </td>
                                                                <td>
                                                                    <img id="btn_gdpEmployEndDate_DialogPersonnelMainInformation" alt="" class="calendar_button"
                                                                        onclick="btn_gdpEmployEndDate_DialogPersonnelMainInformation_OnClick(event)"
                                                                        onmouseup="btn_gdpEmployEndDate_DialogPersonnelMainInformation_OnMouseUp(event)"
                                                                        src="Images/Calendar/btn_calendar.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <ComponentArt:Calendar ID="gCalEmployEndDate_DialogPersonnelMainInformation" runat="server"
                                                            AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                            CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                            DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                            ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                            NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                            PopUpExpandControlId="btn_gdpEmployEndDate_DialogPersonnelMainInformation" PrevImageUrl="cal_prevMonth.gif"
                                                            SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                            SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                            <ClientEvents>
                                                                <SelectionChanged EventHandler="gCalEmployEndDate_DialogPersonnelMainInformation_OnChange" />
                                                                <Load EventHandler="gCalEmployEndDate_DialogPersonnelMainInformation_onLoad" />
                                                            </ClientEvents>
                                                        </ComponentArt:Calendar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <ComponentArt:ToolBar ID="TlbClear_EmployEndDateCalendars_DialogPersonnelMainInformation"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_EmployEndDateCalendars_DialogPersonnelMainInformation"
                                                        runat="server" ClientSideCommand="tlbItemClear_TlbClear_EmployEndDateCalendars_DialogPersonnelMainInformation_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_EmployEndDateCalendars_DialogPersonnelMainInformation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td runat="server">
                                <asp:Label ID="lblWorkGroup_DialogPersonnelMainInformation" runat="server" CssClass="WhiteLabel"
                                    meta:resourcekey="lblWorkGroup_DialogPersonnelMainInformation" Text="گروه کاری :"
                                    Style="color: #B22222"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtCurrentActiveWorkGroup_DialogPersonnelMainInformation"
                                                readonly="readonly" onselect="this.select();" onfocus="this.select();" />
                                        </td>
                                        <td style="width: 5%">
                                            <ComponentArt:ToolBar ID="TlbWorkGroupDefinition_DialogPersonnelMainInformation"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemWorkGroupDefinition_TlbWorkGroupDefinition_DialogPersonnelMainInformation"
                                                        runat="server" ClientSideCommand="tlbItemWorkGroupDefinition_TlbWorkGroupDefinition_DialogPersonnelMainInformation_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="BallClockAqua.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemWorkGroupDefinition_TlbWorkGroupDefinition_DialogPersonnelMainInformation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td runat="server">
                                <asp:Label ID="lblRuleGroup_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblRuleGroup_DialogPersonnelMainInformation"
                                    Text="گروه قانون :" CssClass="WhiteLabel" Style="color: #B22222"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtCurrentActiveRuleGroup_DialogPersonnelMainInformation"
                                                readonly="readonly" onselect="this.select();" onfocus="this.select();" />
                                        </td>
                                        <td style="width: 5%">
                                            <ComponentArt:ToolBar ID="TlbRuleGroupDefinition_DialogPersonnelMainInformation"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRuleGroupDefinition_TlbRuleGroupDefinition_DialogPersonnelMainInformation"
                                                        runat="server" ClientSideCommand="ShowDialogPersonnelRulesGroups();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="BallClockAmber.png" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItemRuleGroupDefinition_TlbRuleGroupDefinition_DialogPersonnelMainInformation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td runat="server">
                                <asp:Label ID="lblCalculationRangeGroup_DialogPersonnelMainInformation" runat="server"
                                    CssClass="WhiteLabel" meta:resourcekey="lblCalculationRangeGroup_DialogPersonnelMainInformation"
                                    Text="گروه محدوده محاسبات :" Style="color: #B22222"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtCurrentActiveCalculationRangeGroup_DialogPersonnelMainInformation"
                                                readonly="readonly" onselect="this.select();" onfocus="this.select();" />
                                        </td>
                                        <td style="width: 5%">
                                            <ComponentArt:ToolBar ID="TlbCalculationRangeGroupDefinition_DialogPersonnelMainInformation"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemCalculationRangeGroupDefinition_TlbCalculationRangeGroupDefinition_DialogPersonnelMainInformation"
                                                        runat="server" ClientSideCommand="tlbItemCalculationRangeGroupDefinition_TlbCalculationRangeGroupDefinition_DialogPersonnelMainInformation_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Verde.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCalculationRangeGroupDefinition_TlbCalculationRangeGroupDefinition_DialogPersonnelMainInformation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td runat="server">
                                <asp:Label ID="lblEmailAddress_DialogPersonnelMainInformation" runat="server" CssClass="WhiteLabel"
                                    meta:resourcekey="lblEmailAddress_DialogPersonnelMainInformation" Text="پست الکترونیکی :"></asp:Label>
                            </td>
                            <td>
                                <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtEmailAddress_DialogPersonnelMainInformation"
                                    onselect="this.select();" onfocus="this.select();" />
                            </td>
                        </tr>
                        <tr>
                            <td runat="server">
                                <asp:Label ID="lblGrade_DialogPersonnelMainInformation" runat="server"
                                    CssClass="WhiteLabel" meta:resourcekey="lblGrade_DialogPersonnelMainInformation"
                                    Text="رتبه :"></asp:Label>
                            </td>
                            <td>

                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <ComponentArt:CallBack ID="CallBack_cmbGrade_DialogPersonnelMainInformation" runat="server" Height="26" OnCallback="CallBack_cmbGrade_DialogPersonnelMainInformation_onCallBack">
                                                <Content>
                                                    <ComponentArt:ComboBox ID="cmbGrade_DialogPersonnelMainInformation" runat="server" ExpandDirection="Up" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" Width="100%" TextBoxEnabled="true">
                                                        <ClientEvents>
                                                            <Expand EventHandler="cmbGrade_DialogPersonnelMainInformation_onExpand" />
                                                            <Collapse EventHandler="cmbGrade_DialogPersonnelMainInformation_onCollapse" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                    <asp:HiddenField ID="ErrorHiddenField_Grade_DialogPersonnelMainInformation" runat="server" />
                                                </Content>
                                                <ClientEvents>
                                                    <BeforeCallback EventHandler="CallBack_cmbGrade_DialogPersonnelMainInformation_onBeforeCallback" />
                                                    <CallbackComplete EventHandler="CallBack_cmbGrade_DialogPersonnelMainInformation_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_cmbGrade_DialogPersonnelMainInformation_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                        <td style="width: 5%">
                                            <ComponentArt:ToolBar ID="TlbClear_cmbGrade_DialogPersonnelMainInformation"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_cmbGrade_DialogPersonnelMainInformation"
                                                        runat="server" ClientSideCommand="tlbItemClear_TlbClear_cmbGrade_DialogPersonnelMainInformation_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_cmbGrade_DialogPersonnelMainInformation"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                            <td runat="server">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td runat="server">
                                <asp:Label ID="lblImageSelect_DialogPersonnelMainInformation" runat="server" meta:resourcekey="lblImageSelect_DialogPersonnelMainInformation"
                                    Text="انتخاب تصویر :" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td>

                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 56%">
                                            <ComponentArt:CallBack ID="Callback_ImageUploader_DialogPersonnelMainInformation"
                                                runat="server" OnCallback="Callback_ImageUploader_DialogPersonnelMainInformation_onCallBack">
                                                <Content>
                                                    <cc1:FileUploaderAJAX ID="ImageUploader_DialogPersonnelMainInformation" runat="server"
                                                        MaxFiles="3" meta:resourcekey="ImageUploader_DialogPersonnelMainInformation"
                                                        showDeletedFilesOnPostBack="false" text_Add="" text_Delete="" text_X="" />
                                                    <%--<asp:HiddenField runat="server" ID="ErrorHiddenField_ImageUploader_DialogPersonnelMainInformation"/>--%>
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="Callback_ImageUploader_DialogPersonnelMainInformation_onCallBackComplete" />
                                                    <CallbackError EventHandler="Callback_ImageUploader_DialogPersonnelMainInformation_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                        <td style="width: 5%">
                                            <ComponentArt:ToolBar ID="TlbDeletePersonnelImage_DialogPersonnelMainInformation" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemDeletePersonnelImage_TlbDeletePersonnelImage_DialogPersonnelMainInformation" runat="server" ClientSideCommand="tlbItemDeletePersonnelImage_TlbDeletePersonnelImage_DialogPersonnelMainInformation_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDeletePersonnelImage_TlbDeletePersonnelImage_DialogPersonnelMainInformation" TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                        <td id="tdImageName_DialogPersonnelMainInformation"></td>
                                    </tr>
                                </table>

                            </td>
                            <td runat="server">&nbsp;</td>
                            <td>
                                <iframe id="PersonnelImage_DialogPersonnelMainInformation"
                                    runat="server" scrolling="yes" style="width: 30%; height: 480; overflow: scroll"
                                    allowtransparency="true" frameborder="0" name="I1"></iframe>
                            </td>
                        </tr>
                        </table>
                </td>
            </tr>
        </table>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogPersonnelExtraInformationheader" FooterClientTemplateId="DialogPersonnelExtraInformationfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogPersonnelExtraInformation"
            runat="server" PreloadContentUrl="false" ContentUrl="PersonnelExtraInformation.aspx"
            IFrameCssClass="PersonnelExtraInformation_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogPersonnelExtraInformationheader">
                    <table id="tbl_DialogPersonnelExtraInformationheader" style="width: 803px" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogPersonnelExtraInformation.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogPersonnelExtraInformation_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogPersonnelExtraInformation" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogPersonnelExtraInformation" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogPersonnelExtraInformation_IFrame').src = 'WhitePage.aspx'; DialogPersonnelExtraInformation.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogPersonnelExtraInformation_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogPersonnelExtraInformationfooter">
                    <table id="tbl_DialogPersonnelExtraInformationfooter" style="width: 803px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogPersonnelExtraInformation_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogPersonnelExtraInformation_downRightImage" style="display: block;"
                                    src="Images/Dialog/down_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogPersonnelExtraInformation_onShow" />
                <OnClose EventHandler="DialogPersonnelExtraInformation_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogPersonnelSingleDateFeatures"
            HeaderClientTemplateId="DialogPersonnelSingleDateFeaturesheader" FooterClientTemplateId="DialogPersonnelSingleDateFeaturesfooter"
            runat="server" PreloadContentUrl="false" ContentUrl="PersonnelSingleDateFeatures.aspx"
            IFrameCssClass="PersonnelSingleDateFeatures_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogPersonnelSingleDateFeaturesheader">
                    <table id="tbl_DialogPersonnelSingleDateFeaturesheader" style="width: 603px;" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogPersonnelSingleDateFeatures.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogPersonnelSingleDateFeatures_topLeftImage" style="display: block;"
                                    src="Images/Dialog/top_left.gif" alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogPersonnelSingleDateFeatures" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogPersonnelSingleDateFeatures" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogPersonnelSingleDateFeatures_IFrame').src = 'WhitePage.aspx'; DialogPersonnelSingleDateFeatures.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogPersonnelSingleDateFeatures_topRightImage" style="display: block;"
                                    src="Images/Dialog/top_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogPersonnelSingleDateFeaturesfooter">
                    <table id="tbl_DialogPersonnelSingleDateFeaturesfooter" style="width: 603px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogPersonnelSingleDateFeatures_downLeftImage" style="display: block;"
                                    src="Images/Dialog/down_left.gif" alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogPersonnelSingleDateFeatures_downRightImage" style="display: block;"
                                    src="Images/Dialog/down_right.gif" alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogPersonnelSingleDateFeatures_onShow" />
                <OnClose EventHandler="DialogPersonnelSingleDateFeatures_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogPersonnelRulesGroups"
            HeaderClientTemplateId="DialogPersonnelRulesGroupsheader" FooterClientTemplateId="DialogPersonnelRulesGroupsfooter"
            runat="server" PreloadContentUrl="false" ContentUrl="PersonnelRulesGroups.aspx"
            IFrameCssClass="PersonnelRulesGroups_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogPersonnelRulesGroupsheader">
                    <table id="tbl_DialogPersonnelRulesGroupsheader" style="width: 693px;" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogPersonnelRulesGroups.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogPersonnelRulesGroups_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogPersonnelRulesGroups" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogPersonnelRulesGroups" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogPersonnelRulesGroups_IFrame').src = 'WhitePage.aspx'; DialogPersonnelRulesGroups.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogPersonnelRulesGroups_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogPersonnelRulesGroupsfooter">
                    <table id="tbl_DialogPersonnelRulesGroupsfooter" style="width: 693px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogPersonnelRulesGroups_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogPersonnelRulesGroups_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogPersonnelRulesGroups_onShow" />
                <OnClose EventHandler="DialogPersonnelRulesGroups_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogParentDepartments"
            runat="server" Width="400px">
            <Content>
                <table id="tbl_DialogParentDepartments_PersonnelMainInformation" runat="server" class="BodyStyle"
                    style="width: 100%; font-family: Arial; font-size: small">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 98%">&nbsp;
                                    </td>
                                    <td>
                                        <ComponentArt:ToolBar ID="tlbExit_ParentDepartments_PersonnelMainInformation" runat="server"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemExit_tlbExit_ParentDepartments_PersonnelMainInformation"
                                                    runat="server" ClientSideCommand="tlbItemExit_tlbExit_ParentDepartments_PersonnelMainInformation_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="close-down.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_tlbExit_ParentDepartments_PersonnelMainInformation"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 50%">
                                        <asp:Label ID="lblParentDepartments_ParentDepartments_PersonnelMainInformation" runat="server"
                                            CssClass="WhiteLabel" meta:resourcekey="lblParentDepartments_ParentDepartments_PersonnelMainInformation"
                                            Text=": بخش های والد"></asp:Label>
                                    </td>
                                    <td id="loadingPanel_trvParentDepartments_PersonnelMainInformation" class="HeaderLabel"
                                        style="width: 45%"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <ComponentArt:CallBack runat="server" ID="CallBack_trvParentDepartments_PersonnelMainInformation"
                                OnCallback="CallBack_trvParentDepartments_PersonnelMainInformation_onCallBack">
                                <Content>
                                    <ComponentArt:TreeView ID="trvParentDepartments_PersonnelMainInformation" runat="server"
                                        CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                        DefaultImageWidth="16" DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15"
                                        ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" Height="200"
                                        HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20"
                                        LineImagesFolderUrl="Images/TreeView/LeftLines" LineImageWidth="19" NodeCssClass="TreeNode"
                                        NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode"
                                        ShowLines="true" Width="100%" meta:resourcekey="trvLineImages">
                                        <ClientEvents>
                                            <Load EventHandler="trvParentDepartments_PersonnelMainInformation_onLoad" />
                                        </ClientEvents>
                                    </ComponentArt:TreeView>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_ParentDepartments_PersonnelMainInformation" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_trvParentDepartments_PersonnelMainInformation_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_trvParentDepartments_PersonnelMainInformation_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </Content>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
            runat="server" Width="280px">
            <Content>
                <table style="width: 100%;" class="ConfirmStyle">
                    <tr align="center">
                        <td colspan="2">
                            <asp:Label ID="lblConfirm" runat="server" CssClass="WhiteLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td style="width: 50%">
                            <ComponentArt:ToolBar ID="TlbOkConfirm" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemOk_TlbOkConfirm" runat="server" ClientSideCommand="tlbItemOk_TlbOkConfirm_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemOk_TlbOkConfirm"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td>
                            <ComponentArt:ToolBar ID="TlbCancelConfirm" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbCancelConfirm" runat="server" ClientSideCommand="tlbItemCancel_TlbCancelConfirm_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCancel"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </Content>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" Modal="true" AllowResize="false"
            runat="server" AllowDrag="false" Alignment="MiddleCentre" ID="DialogWaiting">
            <Content>
                <table>
                    <tr>
                        <td>
                            <img id="Img1" runat="server" alt="" src="~/Images/Dialog/Waiting.gif" />
                        </td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogWaiting_onShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <asp:HiddenField runat="server" ID="hfTitle_DialogPersonnelMainInformation" meta:resourcekey="hfTitle_DialogPersonnelMainInformation" />
        <asp:HiddenField runat="server" ID="hfAdd_DialogPersonnelMainInformation" meta:resourcekey="hfAdd_DialogPersonnelMainInformation" />
        <asp:HiddenField runat="server" ID="hfEdit_DialogPersonnelMainInformation" meta:resourcekey="hfEdit_DialogPersonnelMainInformation" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_DialogPersonnelMainInformation"
            meta:resourcekey="hfCloseMessage_DialogPersonnelMainInformation" />
        <asp:HiddenField runat="server" ID="hfcmbAlarm_DialogPersonnelMainInformation" meta:resourcekey="hfcmbAlarm_DialogPersonnelMainInformation" />
        <asp:HiddenField runat="server" ID="hfSexList_DialogPersonnelMainInformation" />
        <asp:HiddenField runat="server" ID="hfMilitaryStateList_DialogPersonnelMainInformation" />
        <asp:HiddenField runat="server" ID="hfMarriageStateList_DialogPersonnelMainInformation" />
        <asp:HiddenField runat="server" ID="hfCurrentDate_PersonnelMainInformation" />
        <asp:HiddenField runat="server" ID="hfErrorType_PersonnelMainInformation" meta:resourcekey="hfErrorType_PersonnelMainInformation" />
        <asp:HiddenField runat="server" ID="hfConnectionError_PersonnelMainInformation" meta:resourcekey="hfConnectionError_PersonnelMainInformation" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_trvParentDepartments_PersonnelMainInformation"
            meta:resourcekey="hfloadingPanel_trvParentDepartments_PersonnelMainInformation" />
    </form>
</body>
</html>
