<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserSettings.aspx.cs" Inherits="UserSettings" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/toolbar.css" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="UserSettingsForm" runat="server" meta:resourcekey="UserSettingsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table id="Mastertbl_UserSettings" class="BoxStyle" style="width: 100%; font-family: Arial;
        font-size: small">
        <tr>
            <td>
                <ComponentArt:ToolBar ID="TlbUserSettings" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemPersonnelSettingsView_TlbUserSettings" runat="server"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemPersonnelSettingsView_TlbUserSettings"
                            ClientSideCommand="tlbItemPersonnelSettingsView_TlbUserSettings_onClick();" TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbUserSettings" runat="server" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemHelp_TlbUserSettings" ClientSideCommand="tlbItemHelp_TlbUserSettings_onClick();"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbUserSettings" runat="server"
                            ClientSideCommand="tlbItemFormReconstruction_TlbUserSettings_onClick();" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbUserSettings"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemExit_TTlbUserSettings" runat="server" ClientSideCommand="tlbItemExit_TlbUserSettings_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbUserSettings"
                            TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
        </tr>
        <tr>
            <td>
                <li></li>
                <table runat="server" id="Container_PersonnelSelect_UserSettings" class="BoxStyle"
                    style="width: 100%;">
                    <tr>
                        <td id="headerPersonnelSelect_UserSettings" class="HeaderLabel">
                            انتخاب پرسنل
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 90%;" class="BoxStyle">
                                <tr>
                                    <td colspan="2">
                                        <table style="width: 50%;">
                                            <tr>
                                                <td style="width: 50%">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 5%">
                                                                <input id="RdbSinglePersonel_UserSettings" type="radio" value="V1" checked="checked"
                                                                    onclick="ChangePersonnelCountState_UserSettings('Single');" name="PersonnelCountState" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblOnePersonel_UserSettings" CssClass="WhiteLabel" runat="server"
                                                                    Text="انفرادی" meta:resourcekey="lblOnePersonel_UserSettings">
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 5%">
                                                                <input id="RdbGroupPersonel_UserSettings" type="radio" value="V1" onclick="ChangePersonnelCountState_UserSettings('Group');"
                                                                    name="PersonnelCountState" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblGroupPersonel_UserSettings" runat="server" CssClass="WhiteLabel"
                                                                    Text="گروهی" meta:resourcekey="lblGroupPersonel_UserSettings">
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 78%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPersonnel_UserSettings" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblPersonnel_UserSettings"
                                                        Text=": پرسنل"></asp:Label>
                                                </td>
                                                <td id="Td4" runat="server" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_UserSettings" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                        Style="direction: ltr;" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_UserSettings"
                                                                runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_UserSettings_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_UserSettings"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_UserSettings"
                                                                runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_UserSettings_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="first.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_UserSettings"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_UserSettings"
                                                                runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_UserSettings_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Before.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_UserSettings"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_UserSettings"
                                                                runat="server" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_UserSettings_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Next.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_UserSettings"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_UserSettings"
                                                                runat="server" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_UserSettings_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="last.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_UserSettings"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td id="headerPersonnelCount_UserSettings" style="width: 22%">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack ID="CallBack_cmbPersonnel_UserSettings" runat="server" OnCallback="CallBack_cmbPersonnel_UserSettings_Callback"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbPersonnel_UserSettings" runat="server" AutoComplete="true"
                                                    AutoHighlight="false" CssClass="comboBox" DataFields="BarCode" DataTextField="Name"
                                                    DropDownCssClass="comboDropDown" DropDownHeight="250" DropDownPageSize="8" DropDownWidth="400"
                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_UserSettings"
                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client" TextBoxEnabled="true"
                                                    SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                    <ClientTemplates>
                                                        <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_UserSettings">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr class="dataRow">
                                                                    <td class="dataCell" style="width: 40%">
                                                                        ## DataItem.getProperty('Text') ##
                                                                    </td>
                                                                    <td class="dataCell" style="width: 30%">
                                                                        ## DataItem.getProperty('BarCode') ##
                                                                    </td>
                                                                    <td class="dataCell" style="width: 30%">
                                                                        ## DataItem.getProperty('CardNum') ##
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ComponentArt:ClientTemplate>
                                                    </ClientTemplates>
                                                    <DropDownHeader>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="400">
                                                            <tr class="headingRow">
                                                                <td id="clmnName_cmbPersonnel_UserSettings" class="headingCell" style="width: 40%;
                                                                    text-align: center">
                                                                    Name And Family
                                                                </td>
                                                                <td id="clmnBarCode_cmbPersonnel_UserSettings" class="headingCell" style="width: 30%;
                                                                    text-align: center">
                                                                    BarCode
                                                                </td>
                                                                <td id="clmnCardNum_cmbPersonnel_UserSettings" class="headingCell" style="width: 30%;
                                                                    text-align: center">
                                                                    CardNum
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </DropDownHeader>
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbPersonnel_UserSettings_onExpand" />
                                                        <Change EventHandler="cmbPersonnel_UserSettings_onChange" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField ID="ErrorHiddenField_Personnel_UserSettings" runat="server" />
                                                <asp:HiddenField ID="hfPersonnelPageCount_UserSettings" runat="server" />
                                                <asp:HiddenField ID="hfPersonnelCount_UserSettings" runat="server" />
                                                <asp:HiddenField runat="server" ID="hfPersonnelSelectedCount_UserSettings" Value="0" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbPersonnel_UserSettings_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbPersonnel_UserSettings_onCallBackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbPersonnel_UserSettings_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input id="txtPersonnelSearch_UserSettings" runat="server" class="TextBoxes" onclick="this.select();"
                                            onfocus="this.select();" style="width: 98%" type="text" />
                                    </td>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbSearchPersonnel_UserSettings" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_UserSettings" runat="server"
                                                    ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_UserSettings_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_UserSettings"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSelectedPersonnelCount_UserSettings" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbAdvancedSearch_UserSettings" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_UserSettings"
                                                    runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_UserSettings_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_UserSettings"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table runat="server" id="Container_SkinsSettings_UserSettings" style="width: 100%;"
                    class="BoxStyle">
                    <tr>
                        <td id="headerSkinsSettings_UserSettings" colspan="2" class="HeaderLabel">
                            تنظیمات پوسته نرم افزار
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 50%;">
                                <tr>
                                    <td style="width: 25%">
                                        <asp:Label ID="lblSkins_SkinsSettings_UserSettings" runat="server" CssClass="WhiteLabel"
                                            Text="پوسته ها :" meta:resourcekey="lblSkins_SkinsSettings_UserSettings"></asp:Label>
                                    </td>
                                    <td>
                                        <ComponentArt:ComboBox ID="cmbSkins_SkinsSettings_UserSettings" runat="server" AutoComplete="true"
                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                            DropDownHeight="200" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropImageUrl="Images/ComboBox/ddn.png" ExpandDirection="Down" FocusedCssClass="comboBoxHover"
                                            HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                            SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true"
                                            Width="100%" ItemClientTemplateId="ItemClientTemplateId_cmbSkins_SkinsSettings_UserSettings">
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="ItemClientTemplateId_cmbSkins_SkinsSettings_UserSettings">
                                                    <table style="width: 100%; height: 40px; border: 1px outset black">
                                                        <tr>
                                                            <td style="width: 60%; border: 1px outset black; background-image: url('Skins/##DataItem.get_value()##/Images/Ghadir/bg-body.jpg');
                                                                -moz-border-radius: 10px; -webkit-border-radius: 10px; border-radius: 10px; -khtml-border-radius: 10px;">
                                                            </td>
                                                            <td align="center">
                                                                ##DataItem.get_text()##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                        </ComponentArt:ComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 10%" align="center">
                            <ComponentArt:ToolBar ID="TlbSkinsSettings" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbSkinsSettings" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                        ClientSideCommand="tlbItemSave_TlbSkinsSettings_onClick();" ItemType="Command"
                                        meta:resourcekey="tlbItemSave_TlbSkinsSettings" TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td id="headerEmailSMSSettings_UserSettings" class="HeaderLabel">
                            تنظیمات ارسال نامه الکترونیکی و پیام کوتاه
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;" class="BoxStyle">
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 3%">
                                                                            <input id="chbVerifySendEmail_EmailSMSSettings_UserSettings" type="checkbox" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblVerifySendEmail_EmailSMSSettings_UserSettings" runat="server" Text="ارسال نامه الکترونیکی"
                                                                                class="WhiteLabel" meta:resourcekey="lblVerifySendEmail_EmailSMSSettings_UserSettings"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 3%" align="center">
                                                                            <input id="rdbSendEmail_DayEmail_EmailSMSSettings_UserSettings" type="radio" name="EmailSettings"
                                                                                checked="checked" />
                                                                        </td>
                                                                        <td style="width: 30%" align="center">
                                                                            <asp:Label ID="lblSendEmail_DayEmail_EmailSMSSettings_UserSettings" runat="server"
                                                                                Text="ارسال نامه الکترونیکی هر" CssClass="WhiteLabel" meta:resourcekey="lblSendEmail_DayEmail_EmailSMSSettings_UserSettings"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                            <ComponentArt:ComboBox ID="cmbDay_DayEmail_EmailSMSSettings_UserSettings" runat="server"
                                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                DropDownCssClass="comboDropDown" DropDownHeight="100" DropDownResizingMode="Corner"
                                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                TextBoxCssClass="comboTextBox" Width="100%">
                                                                            </ComponentArt:ComboBox>
                                                                        </td>
                                                                        <td style="width: 20%" align="center">
                                                                            <asp:Label ID="lblDay_DayEmail_EmailSMSSettings_UserSettings" class="WhiteLabel"
                                                                                runat="server" Text="روز یکبار در ساعت" meta:resourcekey="lblDay_DayEmail_EmailSMSSettings_UserSettings"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                            <ComponentArt:ComboBox ID="cmbHour_DayEmail_EmailSMSSettings_UserSettings" runat="server"
                                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                DropDownCssClass="comboDropDown" DropDownHeight="100" DropDownResizingMode="Corner"
                                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                TextBoxCssClass="comboTextBox" Width="100%">
                                                                            </ComponentArt:ComboBox>
                                                                        </td>
                                                                        <td style="width: 5%" align="center">
                                                                            <asp:Label ID="lblAnd_DayEmail_EmailSMSSettings_UserSettings" runat="server" class="WhiteLabel"
                                                                                Text="و" meta:resourcekey="lblAnd_DayEmail_EmailSMSSettings_UserSettings"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                            <ComponentArt:ComboBox ID="cmbMinute_DayEmail_EmailSMSSettings_UserSettings" runat="server"
                                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                DropDownCssClass="comboDropDown" DropDownHeight="100" DropDownResizingMode="Corner"
                                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                TextBoxCssClass="comboTextBox" Width="100%">
                                                                            </ComponentArt:ComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblMinute_DayEmail_EmailSMSSettings_UserSettings" runat="server" class="WhiteLabel"
                                                                                Text="دقیقه" meta:resourcekey="lblMinute_EmailSMSSettings_UserSettings"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 3%" align="center">
                                                                            <input id="rdbSendEmail_HourEmail_EmailSMSSettings_UserSettings" type="radio" name="EmailSettings" />
                                                                        </td>
                                                                        <td style="width: 30%" align="center">
                                                                            <asp:Label ID="lblSendEmail_HourEmail_EmailSMSSettings_UserSettings" runat="server"
                                                                                Text="ارسال نامه الکترونیکی هر" CssClass="WhiteLabel" meta:resourcekey="lblSendEmail_HourEmail_EmailSMSSettings_UserSettings"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                            <ComponentArt:ComboBox ID="cmbHour_HourEmail_EmailSMSSettings_UserSettings" runat="server"
                                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                DropDownCssClass="comboDropDown" DropDownHeight="100" DropDownResizingMode="Corner"
                                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                TextBoxCssClass="comboTextBox" Width="100%">
                                                                            </ComponentArt:ComboBox>
                                                                        </td>
                                                                        <td style="width: 20%" align="center">
                                                                            <asp:Label ID="lblHour_HourEmail_EmailSMSSettings_UserSettings" class="WhiteLabel"
                                                                                runat="server" Text="ساعت و" meta:resourcekey="lblHour_HourEmail_EmailSMSSettings_UserSettings"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                            <ComponentArt:ComboBox ID="cmbMinute_HourEmail_EmailSMSSettings_UserSettings" runat="server"
                                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                DropDownCssClass="comboDropDown" DropDownHeight="100" DropDownResizingMode="Corner"
                                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                TextBoxCssClass="comboTextBox" Width="100%">
                                                                            </ComponentArt:ComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblMinute_Email_EmailSMSSettings_UserSettings0" runat="server" class="WhiteLabel"
                                                                                Text="دقیقه" meta:resourcekey="lblMinute_EmailSMSSettings_UserSettings"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table class="BoxStyle" style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 3%">
                                                                            <input id="chbVerifySendSMS_EmailSMSSettings_UserSettings" type="checkbox" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblVerifySendSMS_EmailSMSSettings_UserSettings" runat="server" Text="ارسال پیام کوتاه"
                                                                                class="WhiteLabel" meta:resourcekey="lblVerifySendSMS_EmailSMSSettings_UserSettings"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 3%" align="center">
                                                                            <input id="rdbSendSMS_DaySMS_EmailSMSSettings_UserSettings" type="radio" name="SMSSettings"
                                                                                checked="checked" />
                                                                        </td>
                                                                        <td style="width: 30%" align="center">
                                                                            <asp:Label ID="lblSendSMS_DaySMS_EmailSMSSettings_UserSettings" runat="server" CssClass="WhiteLabel"
                                                                                meta:resourcekey="lblSendSMS_HourSMS_EmailSMSSettings_UserSettings" Text="ارسال پیام کوتاه هر"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                            <ComponentArt:ComboBox ID="cmbDay_DaySMS_EmailSMSSettings_UserSettings" runat="server"
                                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                DropDownCssClass="comboDropDown" DropDownHeight="100" DropDownResizingMode="Corner"
                                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                TextBoxCssClass="comboTextBox" Width="100%">
                                                                            </ComponentArt:ComboBox>
                                                                        </td>
                                                                        <td style="width: 20%" align="center">
                                                                            <asp:Label ID="lblDay_DaySMS_EmailSMSSettings_UserSettings" runat="server" class="WhiteLabel"
                                                                                Text="روز یکبار در ساعت" meta:resourcekey="lblDay_DaySMS_EmailSMSSettings_UserSettings"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                            <ComponentArt:ComboBox ID="cmbHour_DaySMS_EmailSMSSettings_UserSettings" runat="server"
                                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                DropDownCssClass="comboDropDown" DropDownHeight="100" DropDownResizingMode="Corner"
                                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                TextBoxCssClass="comboTextBox" Width="100%">
                                                                            </ComponentArt:ComboBox>
                                                                        </td>
                                                                        <td style="width: 5%" align="center">
                                                                            <asp:Label ID="lblAnd_DaySMS_EmailSMSSettings_UserSettings" runat="server" class="WhiteLabel"
                                                                                Text="و" meta:resourcekey="lblAnd_DaySMS_EmailSMSSettings_UserSettings"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                            <ComponentArt:ComboBox ID="cmbMinute_DaySMS_EmailSMSSettings_UserSettings" runat="server"
                                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                DropDownCssClass="comboDropDown" DropDownHeight="100" DropDownResizingMode="Corner"
                                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                TextBoxCssClass="comboTextBox" Width="100%">
                                                                            </ComponentArt:ComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblMinute_DaySMS_EmailSMSSettings_UserSettings" runat="server" Text="دقیقه"
                                                                                class="WhiteLabel" meta:resourcekey="lblMinute_DaySMS_EmailSMSSettings_UserSettings"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 3%" align="center">
                                                                            <input id="rdbSendSMS_HourSMS_EmailSMSSettings_UserSettings" type="radio" name="SMSSettings" />
                                                                        </td>
                                                                        <td style="width: 30%" align="center">
                                                                            <asp:Label ID="lblSendSMS_HourSMS_EmailSMSSettings_UserSettings" runat="server" Text="ارسال نامه الکترونیکی هر"
                                                                                CssClass="WhiteLabel" meta:resourcekey="lblSendSMS_HourSMS_EmailSMSSettings_UserSettings"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                            <ComponentArt:ComboBox ID="cmbHour_HourSMS_EmailSMSSettings_UserSettings" runat="server"
                                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                DropDownCssClass="comboDropDown" DropDownHeight="100" DropDownResizingMode="Corner"
                                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                TextBoxCssClass="comboTextBox" Width="100%">
                                                                            </ComponentArt:ComboBox>
                                                                        </td>
                                                                        <td style="width: 20%" align="center">
                                                                            <asp:Label ID="lblHour_HourSMS_EmailSMSSettings_UserSettings" class="WhiteLabel"
                                                                                runat="server" Text="ساعت و" meta:resourcekey="lblHour_HourSMS_EmailSMSSettings_UserSettings"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                            <ComponentArt:ComboBox ID="cmbMinute_HourSMS_EmailSMSSettings_UserSettings" runat="server"
                                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                DropDownCssClass="comboDropDown" DropDownHeight="100" DropDownResizingMode="Corner"
                                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                TextBoxCssClass="comboTextBox" Width="100%">
                                                                            </ComponentArt:ComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblMinute_HourSMS_EmailSMSSettings_UserSettings" runat="server" class="WhiteLabel"
                                                                                Text="دقیقه" meta:resourcekey="lblMinute_HourSMS_EmailSMSSettings_UserSettings"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 10%" align="center" valign="middle">
                                        <ComponentArt:ToolBar ID="TlbEmailSMSSettings" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbEmailSMSSettings" runat="server" ClientSideCommand="tlbItemSave_TlbEmailSMSSettings_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbEmailSMSSettings"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
        runat="server" Width="300px">
        <Content>
            <table id="tblConfirm_DialogConfirm" style="width: 100%;" class="ConfirmStyle">
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
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCancelConfirm"
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
    <asp:HiddenField runat="server" ID="hfPersonalTitle_DialogUserSettings" meta:resourcekey="hfPersonalTitle_DialogUserSettings" />
    <asp:HiddenField runat="server" ID="hfManagementTitle_DialogUserSettings" meta:resourcekey="hfManagementTitle_DialogUserSettings" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_UserSettings" meta:resourcekey="hfCloseMessage_UserSettings" />
    <asp:HiddenField runat="server" ID="hfheaderSkinsSettings_UserSettings" meta:resourcekey="hfheaderSkinsSettings_UserSettings" />
    <asp:HiddenField runat="server" ID="hfheaderEmailSMSSettings_UserSettings" meta:resourcekey="hfheaderEmailSMSSettings_UserSettings" />
    <asp:HiddenField runat="server" ID="hfheaderPersonnelSelect_UserSettings" meta:resourcekey="hfheaderPersonnelSelect_UserSettings" />
    <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_UserSettings" meta:resourcekey="hfclmnName_cmbPersonnel_UserSettings" />
    <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_UserSettings" meta:resourcekey="hfclmnBarCode_cmbPersonnel_UserSettings" />
    <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_UserSettings" meta:resourcekey="hfclmnCardNum_cmbPersonnel_UserSettings" />
    <asp:HiddenField runat="server" ID="hfheaderPersonnelCount_UserSettings" meta:resourcekey="hfheaderPersonnelCount_UserSettings" />
    <asp:HiddenField runat="server" ID="hfConnectionError_UserSettings" meta:resourcekey="hfConnectionError_UserSettings" />
    <asp:HiddenField runat="server" ID="hfErrorType_UserSettings" meta:resourcekey="hfErrorType_UserSettings" />
    <asp:HiddenField runat="server" ID="hfPersonnelPageSize_UserSettings" />
    <asp:HiddenField runat="server" ID="hfSettingsBatchList_UserSettings" />
    <asp:HiddenField runat="server" ID="hfEmailSMSSettingsDefaultTime_UserSettings" />
    </form>
</body>
</html>
