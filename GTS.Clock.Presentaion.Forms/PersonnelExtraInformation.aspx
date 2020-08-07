<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonnelExtraInformation.aspx.cs"
    Inherits="PersonnelExtraInformation" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="PersonnelExtraInformationForm" runat="server" meta:resourcekey="PersonnelExtraInformation">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="font-family: Arial; font-size: small; width: 100%; height: 490px;"
        class="BoxStyle">
        <tr style="height: 12%">
            <td colspan="3">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbPersonnelExtraInformation" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbPersonnelExtraInformation" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbPersonnelExtraInformation"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemSave_TlbPersonnelExtraInformation_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSettings_TlbPersonnelExtraInformation" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="regulation.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSettings_TlbPersonnelExtraInformation"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemSettings_TlbPersonnelExtraInformation_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbPersonnelExtraInformation" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbPersonnelExtraInformation"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbPersonnelExtraInformation_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbPersonnelExtraInformation"
                                        runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbPersonnelExtraInformation_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbPersonnelExtraInformation"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPersonnelExtraInformation" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbPersonnelExtraInformation"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemExit_TlbPersonnelExtraInformation_onClick();" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_PersonnelExtraInformation" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 11%">
            <td style="width: 33%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label runat="server" ID="lblTitle_R1_DialogPersonnelExtraInformation" CssClass="WhiteLabel"
                                Text="فیلد رزرو 1" meta:resourcekey="lblTitle_R1_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <input type="text" runat="server" style="width: 85%;" class="TextBoxes" id="txtValue_R1_DialogPersonnelExtraInformation" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 33%">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R2_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 2" meta:resourcekey="lblTitle_R2_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <input type="text" runat="server" style="width: 85%;" class="TextBoxes" id="txtValue_R2_DialogPersonnelExtraInformation" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 33%">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R3_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 3" meta:resourcekey="lblTitle_R3_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <input type="text" runat="server" style="width: 85%;" class="TextBoxes" id="txtValue_R3_DialogPersonnelExtraInformation" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 11%">
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R4_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 4" meta:resourcekey="lblTitle_R4_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <input type="text" runat="server" style="width: 85%;" class="TextBoxes" id="txtValue_R4_DialogPersonnelExtraInformation" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R5_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 5" meta:resourcekey="lblTitle_R5_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <input type="text" runat="server" style="width: 85%;" class="TextBoxes" id="txtValue_R5_DialogPersonnelExtraInformation" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R6_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 6" meta:resourcekey="lblTitle_R6_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <input type="text" runat="server" style="width: 85%;" class="TextBoxes" id="txtValue_R6_DialogPersonnelExtraInformation" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 11%">
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R7_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 7" meta:resourcekey="lblTitle_R7_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <input type="text" runat="server" style="width: 85%;" class="TextBoxes" id="txtValue_R7_DialogPersonnelExtraInformation" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R8_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 8" meta:resourcekey="lblTitle_R8_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <input type="text" runat="server" style="width: 85%;" class="TextBoxes" id="txtValue_R8_DialogPersonnelExtraInformation" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R9_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 9" meta:resourcekey="lblTitle_R9_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <input type="text" runat="server" style="width: 85%;" class="TextBoxes" id="txtValue_R9_DialogPersonnelExtraInformation" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 11%">
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R10_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 10" meta:resourcekey="lblTitle_R10_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <input type="text" runat="server" style="width: 85%;" class="TextBoxes" id="txtValue_R10_DialogPersonnelExtraInformation" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R11_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 11" meta:resourcekey="lblTitle_R11_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <input type="text" runat="server" style="width: 85%;" class="TextBoxes" id="txtValue_R11_DialogPersonnelExtraInformation" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R12_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 12" meta:resourcekey="lblTitle_R12_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <input type="text" runat="server" style="width: 85%;" class="TextBoxes" id="txtValue_R12_DialogPersonnelExtraInformation" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 11%">
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R13_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 13" meta:resourcekey="lblTitle_R13_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <input type="text" runat="server" style="width: 85%;" class="TextBoxes" id="txtValue_R13_DialogPersonnelExtraInformation" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R14_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 14" meta:resourcekey="lblTitle_R14_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <input type="text" runat="server" style="width: 85%;" class="TextBoxes" id="txtValue_R14_DialogPersonnelExtraInformation" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R15_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 15" meta:resourcekey="lblTitle_R15_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <input type="text" runat="server" style="width: 85%;" class="TextBoxes" id="txtValue_R15_DialogPersonnelExtraInformation" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 11%">
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R16_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 16" meta:resourcekey="lblTitle_R16_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbValue_R16_DialogPersonnelExtraInformation"
                                OnCallback="CallBack_cmbValue_R16_DialogPersonnelExtraInformation_onCallback"
                                Height="26">
                                <Content>
                                    <ComponentArt:ComboBox ID="cmbValue_R16_DialogPersonnelExtraInformation" runat="server"
                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                        DataTextField="ComboText" DataValueField="ComboValue" DropDownCssClass="comboDropDown"
                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                        TextBoxCssClass="comboTextBox" Width="88%" ExpandDirection="Up" TextBoxEnabled="true">
                                        <ClientEvents>
                                            <Expand EventHandler="cmbValue_R16_DialogPersonnelExtraInformation_onExpand" />
                                            <Collapse EventHandler="cmbValue_R16_DialogPersonnelExtraInformation_onCollapse" />
                                        </ClientEvents>
                                    </ComponentArt:ComboBox>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_R16_DialogPersonnelExtraInformation" />
                                </Content>
                                <ClientEvents>
                                    <BeforeCallback EventHandler="CallBack_cmbValue_R16_DialogPersonnelExtraInformation_onBeforeCallback" />
                                    <CallbackComplete EventHandler="CallBack_cmbValue_R16_DialogPersonnelExtraInformation_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_cmbValue_R16_DialogPersonnelExtraInformation_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R17_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 17" meta:resourcekey="lblTitle_R17_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbValue_R17_DialogPersonnelExtraInformation"
                                OnCallback="CallBack_cmbValue_R17_DialogPersonnelExtraInformation_onCallback"
                                Height="26">
                                <Content>
                                    <ComponentArt:ComboBox ID="cmbValue_R17_DialogPersonnelExtraInformation" runat="server"
                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                        DataTextField="ComboText" DataValueField="ComboValue" DropDownCssClass="comboDropDown"
                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                        TextBoxCssClass="comboTextBox" Width="88%" ExpandDirection="Up" TextBoxEnabled="true">
                                        <ClientEvents>
                                            <Expand EventHandler="cmbValue_R17_DialogPersonnelExtraInformation_onExpand" />
                                            <Collapse EventHandler="cmbValue_R17_DialogPersonnelExtraInformation_onCollapse" />
                                        </ClientEvents>
                                    </ComponentArt:ComboBox>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_R17_DialogPersonnelExtraInformation" />
                                </Content>
                                <ClientEvents>
                                    <BeforeCallback EventHandler="CallBack_cmbValue_R17_DialogPersonnelExtraInformation_onBeforeCallback" />
                                    <CallbackComplete EventHandler="CallBack_cmbValue_R17_DialogPersonnelExtraInformation_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_cmbValue_R17_DialogPersonnelExtraInformation_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R18_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 18" meta:resourcekey="lblTitle_R18_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbValue_R18_DialogPersonnelExtraInformation"
                                OnCallback="CallBack_cmbValue_R18_DialogPersonnelExtraInformation_onCallback">
                                <Content>
                                    <ComponentArt:ComboBox ID="cmbValue_R18_DialogPersonnelExtraInformation" runat="server"
                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                        DataTextField="ComboText" DataValueField="ComboValue" DropDownCssClass="comboDropDown"
                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                        TextBoxCssClass="comboTextBox" Width="88%" ExpandDirection="Up" TextBoxEnabled="true">
                                        <ClientEvents>
                                            <Expand EventHandler="cmbValue_R18_DialogPersonnelExtraInformation_onExpand" />
                                            <Collapse EventHandler="cmbValue_R18_DialogPersonnelExtraInformation_onCollapse" />
                                        </ClientEvents>
                                    </ComponentArt:ComboBox>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_R18_DialogPersonnelExtraInformation" />
                                </Content>
                                <ClientEvents>
                                    <BeforeCallback EventHandler="CallBack_cmbValue_R18_DialogPersonnelExtraInformation_onBeforeCallback" />
                                    <CallbackComplete EventHandler="CallBack_cmbValue_R18_DialogPersonnelExtraInformation_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_cmbValue_R18_DialogPersonnelExtraInformation_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 11%">
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R19_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 19" meta:resourcekey="lblTitle_R19_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbValue_R19_DialogPersonnelExtraInformation"
                                OnCallback="CallBack_cmbValue_R19_DialogPersonnelExtraInformation_onCallback"
                                Height="26">
                                <Content>
                                    <ComponentArt:ComboBox ID="cmbValue_R19_DialogPersonnelExtraInformation" runat="server"
                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                        DataTextField="ComboText" DataValueField="ComboValue" DropDownCssClass="comboDropDown"
                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                        TextBoxCssClass="comboTextBox" Width="88%" ExpandDirection="Up" TextBoxEnabled="true">
                                        <ClientEvents>
                                            <Expand EventHandler="cmbValue_R19_DialogPersonnelExtraInformation_onExpand" />
                                            <Collapse EventHandler="cmbValue_R19_DialogPersonnelExtraInformation_onCollapse" />
                                        </ClientEvents>
                                    </ComponentArt:ComboBox>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_R19_DialogPersonnelExtraInformation" />
                                </Content>
                                <ClientEvents>
                                    <BeforeCallback EventHandler="CallBack_cmbValue_R19_DialogPersonnelExtraInformation_onBeforeCallback" />
                                    <CallbackComplete EventHandler="CallBack_cmbValue_R19_DialogPersonnelExtraInformation_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_cmbValue_R19_DialogPersonnelExtraInformation_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 49%">
                            <asp:Label ID="lblTitle_R20_DialogPersonnelExtraInformation" runat="server" CssClass="WhiteLabel"
                                Text="فیلد رزرو 20" meta:resourcekey="lblTitle_R20_DialogPersonnelExtraInformation"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td style="width: 49%">
                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbValue_R20_DialogPersonnelExtraInformation"
                                OnCallback="CallBack_cmbValue_R20_DialogPersonnelExtraInformation_onCallback"
                                Height="26">
                                <Content>
                                    <ComponentArt:ComboBox ID="cmbValue_R20_DialogPersonnelExtraInformation" runat="server"
                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                        DataTextField="ComboText" DataValueField="ComboValue" DropDownCssClass="comboDropDown"
                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                        TextBoxCssClass="comboTextBox" Width="88%" ExpandDirection="Up" TextBoxEnabled="true">
                                        <ClientEvents>
                                            <Expand EventHandler="cmbValue_R20_DialogPersonnelExtraInformation_onExpand" />
                                            <Collapse EventHandler="cmbValue_R20_DialogPersonnelExtraInformation_onCollapse" />
                                        </ClientEvents>
                                    </ComponentArt:ComboBox>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_R20_DialogPersonnelExtraInformation" />
                                </Content>
                                <ClientEvents>
                                    <BeforeCallback EventHandler="CallBack_cmbValue_R20_DialogPersonnelExtraInformation_onBeforeCallback" />
                                    <CallbackComplete EventHandler="CallBack_cmbValue_R20_DialogPersonnelExtraInformation_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_cmbValue_R20_DialogPersonnelExtraInformation_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
        runat="server" Width="330px">
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
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogPersonnelExtraInformationSettings"
        HeaderClientTemplateId="DialogPersonnelExtraInformationSettingsheader" FooterClientTemplateId="DialogPersonnelExtraInformationSettingsfooter"
        runat="server" PreloadContentUrl="false" ContentUrl="PersonnelExtraInformationSettings.aspx"
        IFrameCssClass="PersonnelExtraInformationSettings_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogPersonnelExtraInformationSettingsheader">
                <table id="tbl_DialogPersonnelExtraInformationSettingsheader" style="width: 703px;"
                    cellpadding="0" cellspacing="0" border="0" onmousedown="DialogPersonnelExtraInformationSettings.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogPersonnelExtraInformationSettings_topLeftImage" style="display: block;"
                                src="Images/Dialog/top_left.gif" alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogPersonnelExtraInformationSettings" valign="bottom" style="color: White;
                                        font-size: 13px; font-family: Arial; font-weight: bold;">
                                    </td>
                                    <td id="CloseButton_DialogPersonnelExtraInformationSettings" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogPersonnelExtraInformationSettings_IFrame').contentWindow.CloseDialogPersonnelExtraInformationSettings();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogPersonnelExtraInformationSettings_topRightImage" style="display: block;"
                                src="Images/Dialog/top_right.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogPersonnelExtraInformationSettingsfooter">
                <table id="tbl_DialogPersonnelExtraInformationSettingsfooter" style="width: 703px"
                    cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogPersonnelExtraInformationSettings_downLeftImage" style="display: block;"
                                src="Images/Dialog/down_left.gif" alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogPersonnelExtraInformationSettings_downRightImage" style="display: block;"
                                src="Images/Dialog/down_right.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogPersonnelExtraInformationSettings_onShow" />
            <OnClose EventHandler="DialogPersonnelExtraInformationSettings_onClose" />
        </ClientEvents>
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
    <asp:HiddenField runat="server" ID="hfTitle_DialogPersonnelExtraInformation" meta:resourcekey="hfTitle_DialogPersonnelExtraInformation" />
    <asp:HiddenField runat="server" ID="hfAdd_DialogPersonnelExtraInformation" meta:resourcekey="hfAdd_DialogPersonnelExtraInformation" />
    <asp:HiddenField runat="server" ID="hfEdit_DialogPersonnelExtraInformation" meta:resourcekey="hfEdit_DialogPersonnelExtraInformation" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_DialogPersonnelExtraInformation"
        meta:resourcekey="hfDeleteMessage_DialogPersonnelExtraInformation" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_DialogPersonnelExtraInformation"
        meta:resourcekey="hfCloseMessage_DialogPersonnelExtraInformation" />
    <asp:HiddenField runat="server" ID="hfcmbAlarm_DialogPersonnelExtraInformation" meta:resourcekey="hfcmbAlarm_DialogPersonnelExtraInformation" />
    <asp:HiddenField runat="server" ID="hfLoadException_PersonnelExtraInformation" />
    </form>
</body>
</html>
