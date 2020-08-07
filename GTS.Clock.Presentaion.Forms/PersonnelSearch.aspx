<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.PersonnelSearch"
    CodeFile="PersonnelSearch.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="PersonnelSearchForm" runat="server" meta:resourcekey="PersonnelSearchForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small" class="BoxStyle">
        <tr>
            <td style="width: 50%">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbPersonnelSearch" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbPersonnelSearch" runat="server" ClientSideCommand="tlbItemSave_TlbPersonnelSearch_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbPersonnelSearch"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbPersonnelSearch" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbPersonnelSearch_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbPersonnelSearch"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPersonnelSearch" runat="server" ClientSideCommand="tlbItemExit_TlbPersonnelSearch_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbPersonnelSearch"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 3%">
                            <input id="rdbAllPersonnel_PersonnelSearch" type="radio"  name="PersonnelActiveState" />
                        </td>
                        <td style="width: 30%">
                            <asp:Label ID="lblAllPersonnel_PersonnelSearch" runat="server" meta:resourcekey="lblAllPersonnel_PersonnelSearch"
                                Text="کل پرسنل" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td style="width: 3%">
                            <input id="rdbActive_PersonnelSearch" type="radio" checked="checked" name="PersonnelActiveState" />
                        </td>
                        <td style="width: 30%">
                            <asp:Label ID="lblActive_PersonnelSearch" runat="server" meta:resourcekey="lblActive_PersonnelSearch"
                                Text="فعال" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td style="width: 3%">
                            <input id="rdbDeactive_PersonnelSearch" type="radio" name="PersonnelActiveState" />
                        </td>
                        <td style="width: 30%">
                            <asp:Label ID="lblDeactive_PersonnelSearch" runat="server" meta:resourcekey="lblDeactive_PersonnelSearch"
                                Text="غیر فعال" CssClass="WhiteLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="lblSex_PersonnelSearch" runat="server" meta:resourcekey="lblSex_PersonnelSearch"
                                Text="جنسیت :" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbSex_PersonnelSearch" OnCallback="CallBack_cmbSex_PersonnelSearch_onCallBack"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbSex_PersonnelSearch" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                    DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                    TextBoxCssClass="comboTextBox" Style="width: 100%" TextBoxEnabled="true">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbSex_PersonnelSearch_onExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Sex_PersonnelSearch" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbSex_PersonnelSearch_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbSex_PersonnelSearch_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbSex_PersonnelSearch_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbClear_Sex_PersonnelSearch" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_Sex_PersonnelSearch" runat="server"
                                                    ClientSideCommand="tlbItemClear_TlbClear_Sex_PersonnelSearch_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_Sex_PersonnelSearch"
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
            <td>
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 30%">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFatherName_PersonnelSearch" runat="server"
                                meta:resourcekey="lblFatherName_PersonnelSearch" Text="نام پدر :" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td>
                            &nbsp;<input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtFatherName_PersonnelSearch"
                                onselect="this.select();" onfocus="this.select();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="lblMarriageState_PersonnelSearch" runat="server" meta:resourcekey="lblMarriageState_PersonnelSearch"
                                Text="وضعیت تاهل :" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbMarriageState_PersonnelSearch"
                                            OnCallback="CallBack_cmbMarriageState_PersonnelSearch_onCallBack" Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbMarriageState_PersonnelSearch" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                    DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                    TextBoxCssClass="comboTextBox" Width="100%" TextBoxEnabled="true">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbMarriageState_PersonnelSearch_onExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_MarriageState_PersonnelSearch" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbMarriageState_PersonnelSearch_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbMarriageState_PersonnelSearch_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbMarriageState_PersonnelSearch_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbClear_MarriageState_PersonnelSearch" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_MarriageState_PersonnelSearch"
                                                    runat="server" ClientSideCommand="tlbItemClear_TlbClear_MarriageState_PersonnelSearch_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_MarriageState_PersonnelSearch"
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
            <td>
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 30%">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMilitaryState_PersonnelSearch" runat="server"
                                meta:resourcekey="lblMilitaryState_PersonnelSearch" Text="وضعیت نظام وظیفه :"
                                CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbMilitaryState_PersonnelSearch"
                                            OnCallback="CallBack_cmbMilitaryState_PersonnelSearch_onCallBack" Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbMilitaryState_PersonnelSearch" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                    DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                    TextBoxCssClass="comboTextBox" Style="width: 100%" DropDownHeight="170" TextBoxEnabled="true">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbMilitaryState_PersonnelSearch_onExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_MilitaryState_PersonnelSearch" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbMilitaryState_PersonnelSearch_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbMilitaryState_PersonnelSearch_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbMilitaryState_PersonnelSearch_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbClear_MilitaryState_PersonnelSearch" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_MilitaryState_PersonnelSearch"
                                                    runat="server" ClientSideCommand="tlbItemClear_TlbClear_MilitaryState_PersonnelSearch_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_MilitaryState_PersonnelSearch"
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
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="lblEducation_PersonnelSearch" runat="server" CssClass="WhiteLabel"
                                meta:resourcekey="lblEducation_PersonnelSearch" Text="تحصیلات :"></asp:Label>
                        </td>
                        <td>
                            &nbsp;<input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtEducation_PersonnelSearch"
                                onselect="this.select();" onfocus="this.select();" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 30%">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBirthLocation_PersonnelSearch" runat="server"
                                meta:resourcekey="lblBirthLocation_PersonnelSearch" Text="محل تولد :" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td>
                            &nbsp;<input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtBirthLocation_PersonnelSearch"
                                onselect="this.select();" onfocus="this.select();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="lblCardNumber_PersonnelSearch" runat="server" meta:resourcekey="lblCardID_PersonnelSearch"
                                Text="شماره کارت :" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td>
                            &nbsp;<input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtCardNumber_PersonnelSearch"
                                onselect="this.select();" onfocus="this.select();" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 30%">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEmployNumber_PersonnelSearch" runat="server"
                                meta:resourcekey="lblEmployNumber_PersonnelSearch" Text="شماره استخدامی :" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td>
                            &nbsp;<input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtEmployNumber_PersonnelSearch"
                                onselect="this.select();" onfocus="this.select();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="lblDepartment_PersonnelSearch" runat="server" meta:resourcekey="lblDepartment_PersonnelSearch"
                                Text="بخش :" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 50%">
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbDepartment_PersonnelSearch"
                                            OnCallback="CallBack_cmbDepartment_PersonnelSearch_onCallBack" Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbDepartment_PersonnelSearch" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                    DropDownHeight="190" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                    TextBoxCssClass="comboTextBox" Width="100%" ExpandDirection="Down" TextBoxEnabled="true">
                                                    <DropDownContent>
                                                        <ComponentArt:TreeView ID="trvDepartment_PersonnelSearch" runat="server" CollapseImageUrl="images/TreeView/exp.gif"
                                                            CssClass="TreeView" DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                            EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                            ExpandImageUrl="images/TreeView/col.gif" Height="98%" HoverNodeCssClass="HoverTreeNode"
                                                            ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                                            LineImageWidth="19" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17"
                                                            NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode" ShowLines="true"
                                                            Width="100%" meta:resourcekey="trvDepartment_PersonnelSearch">
                                                            <ClientEvents>
                                                                <NodeSelect EventHandler="trvDepartment_PersonnelSearch_onNodeSelect" />
                                                            </ClientEvents>
                                                        </ComponentArt:TreeView>
                                                    </DropDownContent>
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbDepartment_PersonnelSearch_onExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Department_PersonnelSearch" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbDepartment_PersonnelSearch_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbDepartment_PersonnelSearch_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbDepartment_PersonnelSearch_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbClear_Department_PersonnelSearch" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_Department_PersonnelSearch" runat="server"
                                                    ClientSideCommand="tlbItemClear_TlbClear_Department_PersonnelSearch_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_Department_PersonnelSearch"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbRefresh_cmbDepartment_PersonnelSearch" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbDepartment_PersonnelSearch"
                                                    runat="server" ClientSideCommand="Refresh_cmbDepartment_PersonnelSearch();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbDepartment_PersonnelSearch"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td style="width: 15%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <input id="chbSubDepartment_PersonnelSearch" type="checkbox" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSubDepartment_PersonnelSearch" class="WhiteLabel" runat="server"
                                                        Text="زیربخش" meta:resourcekey="lblSubDepartment_PersonnelSearch"></asp:Label>
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
            <td>
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 30%">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblOrganizationPost_PersonnelSearch"
                                runat="server" CssClass="WhiteLabel" meta:resourcekey="lblOrganizationPost_PersonnelSearch"
                                Text="پست سازمانی :"></asp:Label>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbOrganizationPost_PersonnelSearch"
                                            OnCallback="CallBack_cmbOrganizationPost_PersonnelSearch_onCallBack" Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbOrganizationPost_PersonnelSearch" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                    DropDownHeight="190" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                    TextBoxCssClass="comboTextBox" Width="100%" ExpandDirection="Down" TextBoxEnabled="true">
                                                    <DropDownContent>
                                                        <ComponentArt:TreeView ID="trvOrganizationPost_PersonnelSearch" runat="server" CollapseImageUrl="images/TreeView/exp.gif"
                                                            CssClass="TreeView" DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                            EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                            ExpandImageUrl="images/TreeView/col.gif" Height="98%" HoverNodeCssClass="HoverTreeNode"
                                                            ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                                            LineImageWidth="19" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17"
                                                            NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode" ShowLines="true"
                                                            Width="100%" meta:resourcekey="trvOrganizationPost_PersonnelSearch">
                                                            <ClientEvents>
                                                                <NodeSelect EventHandler="trvOrganizationPost_PersonnelSearch_onNodeSelect" />
                                                                <CallbackComplete EventHandler="trvOrganizationPost_PersonnelSearch_onCallbackComplete" />
                                                                <NodeBeforeExpand EventHandler="trvOrganizationPost_PersonnelSearch_onNodeBeforeExpand" />
                                                            </ClientEvents>
                                                        </ComponentArt:TreeView>
                                                    </DropDownContent>
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbOrganizationPost_PersonnelSearch_onExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_OrganizationPost_PersonnelSearch" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_cmbOrganizationPost_PersonnelSearch_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbOrganizationPost_PersonnelSearch_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td style="width: 7%">
                                        <ComponentArt:ToolBar ID="TlbClear_OrganizationPost_PersonnelSearch" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_OrganizationPost_PersonnelSearch"
                                                    runat="server" ClientSideCommand="tlbItemClear_TlbClear_OrganizationPost_PersonnelSearch_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_OrganizationPost_PersonnelSearch"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td style="width: 7%">
                                        <ComponentArt:ToolBar ID="TlbRefresh_cmbOrganizationPost_PersonnelSearch" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbOrganizationPost_PersonnelSearch"
                                                    runat="server" ClientSideCommand="Refresh_cmbOrganizationPost_PersonnelSearch();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbOrganizationPost_PersonnelSearch"
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
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="lblEmployType_PersonnelSearch" runat="server" meta:resourcekey="lblEmployType_PersonnelSearch"
                                Text="نوع استخدام :" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbEmployType_PersonnelSearch"
                                            OnCallback="CallBack_cmbEmployType_PersonnelSearch_onCallBack" Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbEmployType_PersonnelSearch" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DataTextField="Name"
                                                    DataValueField="ID" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                    ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                    Width="100%" ExpandDirection="Up" TextBoxEnabled="true">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbEmployType_PersonnelSearch_onExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_EmployType_PersonnelSearch" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbEmployType_PersonnelSearch_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbEmployType_PersonnelSearch_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbEmployType_PersonnelSearch_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbClear_EmployDate_PersonnelSearch" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_EmployDate_PersonnelSearch" runat="server"
                                                    ClientSideCommand="tlbItemClear_TlbClear_EmployDate_PersonnelSearch_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_EmployDate_PersonnelSearch"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbRefresh_cmbEmployType_PersonnelSearch" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbEmployType_PersonnelSearch"
                                                    runat="server" ClientSideCommand="Refresh_cmbEmployType_PersonnelSearch();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbEmployType_PersonnelSearch"
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
            <td>
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 30%">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEmployDate_WorkGroups_PersonnelSearch"
                                runat="server" CssClass="WhiteLabel" meta:resourcekey="lblEmployDate_WorkGroups_PersonnelSearch"
                                Text="تاریخ استخدام :"></asp:Label>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 25%">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFromDate_EmployDate_PersonnelSearch"
                                                        runat="server" meta:resourcekey="lblFromDate_EmployDate_PersonnelSearch" Text="از تاریخ :"
                                                        CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                                <td style="width: 25%">
                                                    <input id="txtFromDate_EmployDate_PersonnelSearch" style="width: 97%" class="TextBoxes"
                                                        type="text" readonly="readonly" />
                                                </td>
                                                <td style="width: 25%">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblToDate_EmployDate_PersonnelSearch"
                                                        runat="server" CssClass="WhiteLabel" meta:resourcekey="lblToDate_EmployDate_PersonnelSearch"
                                                        Text="تا تاریخ :"></asp:Label>
                                                </td>
                                                <td style="width: 25%">
                                                    <input id="txtToDate_EmployDate_PersonnelSearch" style="width: 97%" class="TextBoxes"
                                                        type="text" readonly="readonly" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 7%">
                                        <ComponentArt:ToolBar ID="TlbSetDate_EmployDate_PersonnelSearch" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemSetDate_TlbSetDate_EmployDate_PersonnelSearch"
                                                    runat="server" ClientSideCommand="tlbItemSetDate_TlbSetDate_EmployDate_PersonnelSearch();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Calendar.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSetDate_TlbSetDate_EmployDate_PersonnelSearch"
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
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="lblControlStation_PersonnelSearch" runat="server" meta:resourcekey="lblControlStation_PersonnelSearch"
                                Text="ایستگاه کنترل :" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbControlStation_PersonnelSearch"
                                            OnCallback="CallBack_cmbControlStation_PersonnelSearch_onCallBack" Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbControlStation_PersonnelSearch" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                    DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                    TextBoxCssClass="comboTextBox" Width="100%" ExpandDirection="Up" DataTextField="Name"
                                                    DataValueField="ID" TextBoxEnabled="true">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbControlStation_PersonnelSearch_onExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_ControlStation_PersonnelSearch" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbControlStation_PersonnelSearch_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbControlStation_PersonnelSearch_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbControlStation_PersonnelSearch_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbClear_ControlStation_PersonnelSearch" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_ControlStation_PersonnelSearch"
                                                    runat="server" ClientSideCommand="tlbItemClear_TlbClear_ControlStation_PersonnelSearch_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_ControlStation_PersonnelSearch"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbRefresh_cmbControlStation_PersonnelSearch" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbControlStation_PersonnelSearch"
                                                    runat="server" ClientSideCommand="Refresh_cmbControlStation_PersonnelSearch();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbControlStation_PersonnelSearch"
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
            <td>
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 30%">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBirthDate_WorkGroups_PersonnelSearch"
                                runat="server" meta:resourcekey="lblBirthDate_WorkGroups_PersonnelSearch" Text="تاریخ تولد :"
                                CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 25%">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFromDate_BirthDate_PersonnelSearch"
                                                        runat="server" meta:resourcekey="lblFromDate_BirthDate_PersonnelSearch" Text="از تاریخ :"
                                                        CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                                <td style="width: 25%">
                                                    <input id="txtFromDate_BirthDate_PersonnelSearch" style="width: 97%" class="TextBoxes"
                                                        type="text" readonly="readonly" />
                                                </td>
                                                <td style="width: 25%">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblToDate_BirthDate_PersonnelSearch"
                                                        runat="server" CssClass="WhiteLabel" meta:resourcekey="lblToDate_BirthDate_PersonnelSearch"
                                                        Text="تا تاریخ :"></asp:Label>
                                                </td>
                                                <td style="width: 25%">
                                                    <input id="txtToDate_BirthDate_PersonnelSearch" style="width: 97%" class="TextBoxes"
                                                        type="text" readonly="readonly" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 7%">
                                        <ComponentArt:ToolBar ID="TlbSetDate_BirthDate_PersonnelSearch" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemSetDate_TlbSetDate_BirthDate_PersonnelSearch"
                                                    runat="server" ClientSideCommand="tlbItemSetDate_TlbSetDate_BirthDate_PersonnelSearch();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Calendar.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSetDate_TlbSetDate_BirthDate_PersonnelSearch"
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
            <td colspan="2">
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="lblWorkGroup_PersonnelSearch" runat="server" meta:resourcekey="lblWorkGroup_PersonnelSearch"
                                Text="گروه کاری :" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbWorkGroups_PersonnelSearch"
                                            OnCallback="CallBack_cmbWorkGroups_PersonnelSearch_onCallBack" Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbWorkGroups_PersonnelSearch" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                    DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                    TextBoxCssClass="comboTextBox" Width="100%" DataTextField="Name" DataValueField="ID"
                                                    ExpandDirection="Up" TextBoxEnabled="true">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbWorkGroups_PersonnelSearch_onExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_WorkGroups_PersonnelSearch" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbWorkGroups_PersonnelSearch_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbWorkGroups_PersonnelSearch_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbWorkGroups_PersonnelSearch_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbClear_WorkGroups_PersonnelSearch" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_WorkGroups_PersonnelSearch" runat="server"
                                                    ClientSideCommand="tlbItemClear_TlbClear_WorkGroups_PersonnelSearch_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_WorkGroups_PersonnelSearch"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbRefresh_cmbWorkGroups_PersonnelSearch" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbWorkGroups_PersonnelSearch"
                                                    runat="server" ClientSideCommand="Refresh_cmbWorkGroups_PersonnelSearch();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbWorkGroups_PersonnelSearch"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 31%">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFromDate_WorkGroups_PersonnelSearch"
                                                        runat="server" meta:resourcekey="lblFromDate_WorkGroups_PersonnelSearch" Text="از تاریخ :"
                                                        CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;<input id="txtFromDate_WorkGroups_PersonnelSearch" style="width: 98%" class="TextBoxes"
                                                        type="text" readonly="readonly" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 4%">
                                        <ComponentArt:ToolBar ID="TlbSetDate_WorkGroups_PersonnelSearch" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemSetDate_TlbSetDate_WorkGroups_PersonnelSearch"
                                                    runat="server" ClientSideCommand="tlbItemSetDate_TlbSetDate_WorkGroups_PersonnelSearch();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Calendar.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSetDate_TlbSetDate_WorkGroups_PersonnelSearch"
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
            <td colspan="2">
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="lblRuleGroup_PersonnelSearch" runat="server" meta:resourcekey="lblRuleGroup_PersonnelSearch"
                                Text="گروه قانون :" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbRuleGroups_PersonnelSearch"
                                            OnCallback="CallBack_cmbRuleGroups_PersonnelSearch_onCallBack" Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbRuleGroups_PersonnelSearch" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                    DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                    TextBoxCssClass="comboTextBox" Width="100%" DataTextField="Name" DataValueField="ID"
                                                    ExpandDirection="Up" TextBoxEnabled="true">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbRuleGroups_PersonnelSearch_onExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_RuleGroups_PersonnelSearch" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbRuleGroups_PersonnelSearch_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbRuleGroups_PersonnelSearch_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbRuleGroups_PersonnelSearch_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbClear_RuleGroups_PersonnelSearch" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_RuleGroups_PersonnelSearch" runat="server"
                                                    ClientSideCommand="tlbItemClear_TlbClear_RuleGroups_PersonnelSearch_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_RuleGroups_PersonnelSearch"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbRefresh_cmbRuleGroups_PersonnelSearch" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbRuleGroups_PersonnelSearch"
                                                    runat="server" ClientSideCommand="Refresh_cmbRuleGroups_PersonnelSearch();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbRuleGroups_PersonnelSearch"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 31%">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFromDate_RuleGroups_PersonnelSearch"
                                                        runat="server" meta:resourcekey="lblFromDate_RuleGroups_PersonnelSearch" Text="از تاریخ :"
                                                        CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp<input id="txtFromDate_RuleGroups_PersonnelSearch" style="width: 93%" class="TextBoxes"
                                                        type="text" readonly="readonly" />
                                                </td>
                                                <td style="width: 31%">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblToDate_RuleGroups_PersonnelSearch"
                                                        runat="server" CssClass="WhiteLabel" meta:resourcekey="lblToDate_RuleGroups_PersonnelSearch"
                                                        Text="تا تاریخ :"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;<input id="txtToDate_RuleGroups_PersonnelSearch" style="width: 93%" class="TextBoxes"
                                                        type="text" readonly="readonly" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 4%">
                                        <ComponentArt:ToolBar ID="TlbSetDate_RuleGroups_PersonnelSearch" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemSetDate_TlbSetDate_RuleGroups_PersonnelSearch"
                                                    runat="server" ClientSideCommand="tlbItemSetDate_TlbSetDate_RuleGroups_PersonnelSearch();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Calendar.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSetDate_TlbSetDate_RuleGroups_PersonnelSearch"
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
            <td colspan="2">
                <table style="width: 100%; border: 1px outset black">
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="lblCalculationRange_PersonnelSearch" runat="server" meta:resourcekey="lblCalculationRange_PersonnelSearch"
                                Text="محدوده محاسبات :" CssClass="WhiteLabel"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbCalculationRange_PersonnelSearch"
                                            OnCallback="CallBack_cmbCalculationRange_PersonnelSearch_onCallBack" Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbCalculationRange_PersonnelSearch" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                    DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                    TextBoxCssClass="comboTextBox" Width="100%" ExpandDirection="Down" DataTextField="Name"
                                                    DataValueField="ID" TextBoxEnabled="true">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbCalculationRange_PersonnelSearch_onExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_CalculationRange_PersonnelSearch" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbCalculationRange_PersonnelSearch_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbCalculationRange_PersonnelSearch_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbCalculationRange_PersonnelSearch_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbClear_CalculationRange_PersonnelSearch" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_CalculationRange_PersonnelSearch"
                                                    runat="server" ClientSideCommand="tlbItemClear_TlbClear_CalculationRange_PersonnelSearch_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_CalculationRange_PersonnelSearch"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbRefresh_cmbCalculationRange_PersonnelSearch" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbCalculationRange_PersonnelSearch"
                                                    runat="server" ClientSideCommand="Refresh_cmbCalculationRange_PersonnelSearch();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbCalculationRange_PersonnelSearch"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <table style="width: 100%; border: 1px outset black">
                                            <tr>
                                                <td style="width: 31%">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFromDate_CalculationDateRange_PersonnelSearch"
                                                        runat="server" meta:resourcekey="lblFromDate_CalculationDateRange_PersonnelSearch"
                                                        Text="از تاریخ :" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;<input id="txtFromDate_CalculationDateRange_PersonnelSearch" style="width: 98%"
                                                        class="TextBoxes" type="text" readonly="readonly" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 4%">
                                        <ComponentArt:ToolBar ID="TlbSetDate_CalculationRange_PersonnelSearch" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemSetDate_TlbSetDate_CalculationRange_PersonnelSearch"
                                                    runat="server" ClientSideCommand="tlbItemSetDate_TlbSetDate_CalculationRange_PersonnelSearch();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Calendar.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSetDate_TlbSetDate_CalculationRange_PersonnelSearch"
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
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogDualCalendars"
        runat="server" Width="650px">
        <Content>
            <table style="width: 100%; font-family: Arial; font-size: small" class="BodyStyle">
                <tr>
                    <td colspan="4">
                        <ComponentArt:ToolBar ID="TlbDualCalendars_PersonnelSearch" runat="server" CssClass="toolbar"
                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                            UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbDualCalendars_PersonnelSearch" runat="server"
                                    ClientSideCommand="tlbItemSave_TlbDualCalendars_PersonnelSearch_onClick();" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="save.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbDualCalendars_PersonnelSearch"
                                    TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbDualCalendars_PersonnelSearch" runat="server"
                                    ClientSideCommand="tlbItemExit_TlbDualCalendars_PersonnelSearch_onClick();" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemExit_TlbDualCalendars_PersonnelSearch"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%">
                        <asp:Label runat="server" ID="lblFromDate_DualCalendars_PersonnelSearch" Text="از تاریخ :"
                            meta:resourcekey="lblFromDate_DualCalendars_PersonnelSearch" CssClass="WhiteLabel"></asp:Label>
                    </td>
                    <td style="width: 35%">
                        <table>
                            <tr>
                                <td id="Container_FromDateCalendars_DualCalendars_PersonnelSearch">
                                    <table runat="server" id="Container_pdpFromDate_DualCalendars_PersonnelSearch" visible="false"
                                        style="width: 100%">
                                        <tr>
                                            <td>
                                                <pcal:PersianDatePickup ID="pdpFromDate_DualCalendars_PersonnelSearch" Wrap="true"
                                                    runat="server" CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="Container_gdpFromDate_DualCalendars_PersonnelSearch" visible="false"
                                        style="width: 100%">
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalFromDate_DualCalendars_PersonnelSearch">
                                                    <tr>
                                                        <td onmouseup="btn_gdpFromDate_DualCalendars_PersonnelSearch_OnMouseUp(event)">
                                                            <ComponentArt:Calendar ID="gdpFromDate_DualCalendars_PersonnelSearch" runat="server"
                                                                ControlType="Picker" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                PickerFormat="Custom" SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                <ClientEvents>
                                                                    <SelectionChanged EventHandler="gdpFromDate_DualCalendars_PersonnelSearch_OnDateChange" />
                                                                </ClientEvents>
                                                            </ComponentArt:Calendar>
                                                        </td>
                                                        <td style="font-size: 10px;">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <img id="btn_gdpFromDate_DualCalendars_PersonnelSearch" alt="" class="calendar_button"
                                                                onclick="btn_gdpFromDate_DualCalendars_PersonnelSearch_OnClick(event)" onmouseup="btn_gdpFromDate_DualCalendars_PersonnelSearch_OnMouseUp(event)"
                                                                src="Images/Calendar/btn_calendar.gif" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <ComponentArt:Calendar ID="gCalFromDate_DualCalendars_PersonnelSearch" runat="server"
                                                    AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                    CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                    DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                    ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                    NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                    PopUpExpandControlId="btn_gdpFromDate_DualCalendars_PersonnelSearch" PrevImageUrl="cal_prevMonth.gif"
                                                    SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                    SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                    <ClientEvents>
                                                        <SelectionChanged EventHandler="gCalFromDate_DualCalendars_PersonnelSearch_OnChange" />
                                                        <Load EventHandler="gCalFromDate_DualCalendars_PersonnelSearch_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:Calendar>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top">
                                    <ComponentArt:ToolBar ID="TlbClear_FromDateCalendars_DualCalendars_PersonnelSearch"
                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_FromDateCalendars_DualCalendars_PersonnelSearch"
                                                runat="server" ClientSideCommand="tlbItemClear_TlbClear_FromDateCalendars_DualCalendars_PersonnelSearch_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_FromDateCalendars_DualCalendars_PersonnelSearch"
                                                TextImageSpacing="5" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 10%">
                        <asp:Label runat="server" ID="lblToDate_DualCalendars_PersonnelSearch" Text="تا تاریخ :"
                            meta:resourcekey="lblToDate_DualCalendars_PersonnelSearch" CssClass="WhiteLabel"></asp:Label>
                    </td>
                    <td style="width: 35%">
                        <table>
                            <tr>
                                <td id="Container_ToDateCalendars_DualCalendars_PersonnelSearch">
                                    <table runat="server" id="Container_pdpToDate_DualCalendars_PersonnelSearch" visible="false"
                                        style="width: 100%">
                                        <tr>
                                            <td>
                                                <pcal:PersianDatePickup ID="pdpToDate_DualCalendars_PersonnelSearch" runat="server"
                                                    CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="Container_gdpToDate_DualCalendars_PersonnelSearch" visible="false"
                                        style="width: 100%">
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalToDate_DualCalendars_PersonnelSearch">
                                                    <tr>
                                                        <td onmouseup="btn_gdpToDate_DualCalendars_PersonnelSearch_OnMouseUp(event)">
                                                            <ComponentArt:Calendar ID="gdpToDate_DualCalendars_PersonnelSearch" runat="server"
                                                                ControlType="Picker" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                PickerFormat="Custom" SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                <ClientEvents>
                                                                    <SelectionChanged EventHandler="gdpToDate_DualCalendars_PersonnelSearch_OnDateChange" />
                                                                </ClientEvents>
                                                            </ComponentArt:Calendar>
                                                        </td>
                                                        <td style="font-size: 10px;">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <img id="btn_gdpToDate_DualCalendars_PersonnelSearch" alt="" class="calendar_button"
                                                                onclick="btn_gdpToDate_DualCalendars_PersonnelSearch_OnClick(event)" onmouseup="btn_gdpToDate_DualCalendars_PersonnelSearch_OnMouseUp(event)"
                                                                src="Images/Calendar/btn_calendar.gif" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <ComponentArt:Calendar ID="gCalToDate_DualCalendars_PersonnelSearch" runat="server"
                                                    AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                    CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                    DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                    ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                    NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                    PopUpExpandControlId="btn_gdpToDate_DualCalendars_PersonnelSearch" PrevImageUrl="cal_prevMonth.gif"
                                                    SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                    SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                    <ClientEvents>
                                                        <SelectionChanged EventHandler="gCalToDate_DualCalendars_PersonnelSearch_OnChange" />
                                                        <Load EventHandler="gCalToDate_DualCalendars_PersonnelSearch_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:Calendar>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top">
                                    <ComponentArt:ToolBar ID="TlbClear_ToDateCalendars_DualCalendars_PersonnelSearch"
                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_ToDateCalendars_DualCalendars_PersonnelSearch"
                                                runat="server" ClientSideCommand="tlbItemClear_TlbClear_ToDateCalendars_DualCalendars_PersonnelSearch_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_ToDateCalendars_DualCalendars_PersonnelSearch"
                                                TextImageSpacing="5" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </Content>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogSingleCalendar"
        runat="server" Width="350px">
        <Content>
            <table style="width: 100%; font-family: Arial; font-size: small" class="BodyStyle">
                <tr>
                    <td colspan="2">
                        <ComponentArt:ToolBar ID="TlbSingleCalendar_PersonnelSearch" runat="server" CssClass="toolbar"
                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                            UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbSingleCalendar_PersonnelSearch" runat="server"
                                    ClientSideCommand="tlbItemSave_TlbSingleCalendar_PersonnelSearch_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbSingleCalendar_PersonnelSearch"
                                    TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbSingleCalendar_PersonnelSearch" runat="server"
                                    ClientSideCommand="tlbItemExit_TlbSingleCalendar_PersonnelSearch_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbSingleCalendar_PersonnelSearch"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        <asp:Label runat="server" ID="lblFromDate_SingleCalendar_PersonnelSearch" Text="از تاریخ :"
                            meta:resourcekey="lblFromDate_SingleCalendar_PersonnelSearch" CssClass="WhiteLabel"></asp:Label>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td id="Container_FromDateCalendars_SingleCalendar_PersonnelSearch">
                                    <table runat="server" id="Container_pdpFromDate_SingleCalendar_PersonnelSearch" visible="false"
                                        style="width: 100%">
                                        <tr>
                                            <td>
                                                <pcal:PersianDatePickup ID="pdpFromDate_SingleCalendar_PersonnelSearch" runat="server"
                                                    CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="Container_gdpFromDate_SingleCalendar_PersonnelSearch" visible="false"
                                        style="width: 100%">
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalFromDate_SingleCalendar_PersonnelSearch">
                                                    <tr>
                                                        <td onmouseup="btn_gdpFromDate_SingleCalendar_PersonnelSearch_OnMouseUp(event)">
                                                            <ComponentArt:Calendar ID="gdpFromDate_SingleCalendar_PersonnelSearch" runat="server"
                                                                ControlType="Picker" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                PickerFormat="Custom" SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                                <ClientEvents>
                                                                    <SelectionChanged EventHandler="gdpFromDate_SingleCalendar_PersonnelSearch_OnDateChange" />
                                                                </ClientEvents>
                                                            </ComponentArt:Calendar>
                                                        </td>
                                                        <td style="font-size: 10px;">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <img id="btn_gdpFromDate_SingleCalendar_PersonnelSearch" alt="" class="calendar_button"
                                                                onclick="btn_gdpFromDate_SingleCalendar_PersonnelSearch_OnClick(event)" onmouseup="btn_gdpFromDate_SingleCalendar_PersonnelSearch_OnMouseUp(event)"
                                                                src="Images/Calendar/btn_calendar.gif" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <ComponentArt:Calendar ID="gCalFromDate_SingleCalendar_PersonnelSearch" runat="server"
                                                    AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                    CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                    DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                    ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                    NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                    PopUpExpandControlId="btn_gdpFromDate_SingleCalendar_PersonnelSearch" PrevImageUrl="cal_prevMonth.gif"
                                                    SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                    SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                    <ClientEvents>
                                                        <SelectionChanged EventHandler="gCalFromDate_SingleCalendar_PersonnelSearch_OnChange" />
                                                        <Load EventHandler="gCalFromDate_SingleCalendar_PersonnelSearch_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:Calendar>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top">
                                    <ComponentArt:ToolBar ID="TlbClear_FromDateCalendars_SingleCalendar_PersonnelSearch"
                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_FromDateCalendars_SingleCalendar_PersonnelSearch"
                                                runat="server" ClientSideCommand="tlbItemClear_TlbClear_FromDateCalendars_SingleCalendar_PersonnelSearch_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_FromDateCalendars_SingleCalendar_PersonnelSearch"
                                                TextImageSpacing="5" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </Content>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
        runat="server" Width="320px">
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
    <asp:HiddenField runat="server" ID="hfTitle_DialogPersonnelSearch" meta:resourcekey="hfTitle_DialogPersonnelSearch" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_PersonnelSearch" meta:resourcekey="hfCloseMessage_PersonnelSearch" />
    <asp:HiddenField runat="server" ID="hfErrorType_PersonnelSearch" meta:resourcekey="hfErrorType_PersonnelSearch" />
    <asp:HiddenField runat="server" ID="hfConnectionError_PersonnelSearch" meta:resourcekey="hfConnectionError_PersonnelSearch" />
    <asp:HiddenField runat="server" ID="hfcmbAlarm_PersonnelSearch" meta:resourcekey="hfcmbAlarm_PersonnelSearch" />
    <asp:HiddenField runat="server" ID="hfSexList_PersonnelSearch" />
    <asp:HiddenField runat="server" ID="hfMarriageStateList_PersonnelSearch" />
    </form>
</body>
</html>
