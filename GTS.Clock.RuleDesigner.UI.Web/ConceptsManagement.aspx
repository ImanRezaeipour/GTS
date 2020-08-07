<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConceptsManagement.aspx.cs"
    Inherits="GTS.Clock.RuleDesigner.UI.Web.ConceptsManagement" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/colorPickerStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <%--<script type="text/javascript" src="JS/json.js"></script>--%>
    <form id="ConceptsManagement" runat="server" meta:resourcekey="ConceptsManagement">
        <table id="tblConcepts_ConceptsForm" style="width: 97%; height: 400px; font-family: Arial; font-size: small; text-align: right;" class="BoxStyle">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbConcepts" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbConcepts" runat="server" ClientSideCommand="tlbConceptNew_TlbConcepts_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                            ImageWidth="16px" ConceptType="Command" meta:resourcekey="tlbConceptNew_TlbConcepts"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbConcepts" runat="server" ClientSideCommand="tlbConceptEdit_TlbConcepts_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                            ImageWidth="16px" ConceptType="Command" meta:resourcekey="tlbConceptEdit_TlbConcepts"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbConcepts" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbConceptDelete_TlbConcepts_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ConceptType="Command"
                                            meta:resourcekey="tlbConceptDelete" TextImageSpacing="5" />
                                        <%-- <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbConcepts" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                            ConceptType="Command" meta:resourcekey="tlbConceptHelp" TextImageSpacing="5"
                                            ClientSideCommand="tlbConceptHelp_TlbConcepts_onClick();" />--%>
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbConcepts" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbConceptSave_TlbConcepts_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px" ConceptType="Command"
                                            meta:resourcekey="tlbConceptSave" TextImageSpacing="5" Enabled="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbTlbConcepts" runat="server" ClientSideCommand="tlbConceptCancel_TlbTlbConcepts_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                            ImageWidth="16px" ConceptType="Command" meta:resourcekey="tlbConceptCancel" TextImageSpacing="5"
                                            Enabled="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDefine_TlbTlbConcepts" runat="server" ClientSideCommand="tlbConceptDefine_TlbTlbConcepts_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="view_detailed_silver.png"
                                            ImageWidth="16px" ConceptType="Command" meta:resourcekey="tlbConceptDefine" TextImageSpacing="5"
                                            Enabled="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbConcept" runat="server"
                                            ClientSideCommand="tlbItemFormReconstruction_TlbConcept_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbConcept" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbConcepts" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbConceptExit_TlbConcepts_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ConceptType="Command"
                                            meta:resourcekey="tlbConceptExit" TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="ActionMode_Concepts" class="ToolbarMode"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td class="WhiteLabel">
                                <asp:Label ID="lblConceptName" runat="server" meta:resourcekey="lblConceptName"></asp:Label>
                            </td>
                            <td>
                                <input id="txtCnptName" type="text" runat="server" style="width: 200px;" class="TextBoxes" disabled="disabled"
                                    onclick="this.select();" onfocus="this.select();" />
                            </td>
                            <td class="WhiteLabel">
                                <asp:Label ID="lblConceptCode" runat="server" meta:resourcekey="lblConceptCode"></asp:Label>
                            </td>
                            <td>
                                <input id="txtCnptCode" type="text" runat="server" style="width: 200px;" class="TextBoxes" disabled="disabled"
                                    onclick="this.select();" onfocus="this.select();" />
                            </td>
                            <td class="WhiteLabel">
                                <asp:Label ID="lblReservedField" runat="server" meta:resourcekey="lblReservedField"></asp:Label>
                            </td>
                            <td>
                                <input id="txtCnpKeyColumnName" type="text" runat="server" style="width: 200px;" class="TextBoxes"
                                    onclick="this.select();" onfocus="this.select();" disabled="disabled" />
                            </td>
                            <td class="WhiteLabel">
                                <asp:Label ID="lblConceptColor" runat="server" meta:resourcekey="lblConceptColor"></asp:Label>
                            </td>
                            <td>
                                <div>
                                    <div>
                                        <span class="lbl" style="line-height: 24px;"></span>
                                        <a id="a_ColorPicker" href="javascript:void(0);" class="choose">
                                            <span id="clr_ColorPicker"></span>
                                            <span class="lbl">...</span>
                                        </a>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblConceptType" runat="server" meta:resourcekey="lblConceptType"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbPeriodicTypeField_Concepts" OnCallback="CallBack_cmbPeriodicTypeField_Concepts_onCallBack"
                                                Height="26">
                                                <Content>
                                                    <ComponentArt:ComboBox ID="cmbPeriodicTypeField_Concepts" runat="server" AutoComplete="true"
                                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                        ItemCssClass="comboitem" ItemHoverCssClass="comboConceptHover" SelectedItemCssClass="comboItemHover"
                                                        Style="width: 200px" TextBoxCssClass="comboTextBox" Enabled="false">
                                                        <ClientEvents>
                                                            <Expand EventHandler="cmbPeriodicTypeField_Concepts_onExpand" />
                                                            <Change EventHandler="cmbPeriodicTypeField_Concepts_onChange" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_TypeFields" />
                                                </Content>
                                                <ClientEvents>
                                                    <BeforeCallback EventHandler="cmbPeriodicTypeField_Concepts_onBeforeCallback" />
                                                    <CallbackComplete EventHandler="cmbPeriodicTypeField_Concepts_onCallbackComplete" />
                                                    <CallbackError EventHandler="cmbPeriodicTypeField_Concepts_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:Label ID="lblConceptMatt" runat="server" meta:resourcekey="lblConceptMatt"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbTypeField_Concepts" OnCallback="CallBack_cmbTypeField_Concepts_onCallBack"
                                                Height="26">
                                                <Content>
                                                    <ComponentArt:ComboBox ID="cmbTypeField_Concepts" runat="server" AutoComplete="true"
                                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                        ItemCssClass="comboitem" ItemHoverCssClass="comboConceptHover" SelectedItemCssClass="comboItemHover"
                                                        Style="width: 200px" TextBoxCssClass="comboTextBox" Enabled="False">
                                                        <ClientEvents>
                                                            <Expand EventHandler="cmbTypeField_Concepts_onExpand" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_MattFields" />
                                                </Content>
                                                <ClientEvents>
                                                    <BeforeCallback EventHandler="cmbTypeField_Concepts_onBeforeCallback" />
                                                    <CallbackComplete EventHandler="cmbTypeField_Concepts_onCallbackComplete" />
                                                    <CallbackError EventHandler="cmbTypeField_Concepts_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:Label ID="lblExecuteTime" runat="server" meta:resourcekey="lblExecuteTime"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <ComponentArt:CallBack runat="server" ID="CallBack_ExecuteTimeField_Concepts" OnCallback="CallBack_ExecuteTimeField_Concepts_onCallBack"
                                                Height="26">
                                                <Content>
                                                    <ComponentArt:ComboBox ID="cmbCallSituationTypeField_Concepts" runat="server" AutoComplete="true"
                                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                        ItemCssClass="comboitem" ItemHoverCssClass="comboConceptHover" SelectedItemCssClass="comboItemHover"
                                                        Style="width: 200px" TextBoxCssClass="comboTextBox" Enabled="False">
                                                        <ClientEvents>
                                                            <Expand EventHandler="cmbCallSituationTypeField_Concepts_onExpand" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_ExecuteFields" />
                                                </Content>
                                                <ClientEvents>
                                                    <BeforeCallback EventHandler="cmbCallSituationTypeField_Concepts_onBeforeCallback" />
                                                    <CallbackComplete EventHandler="cmbCallSituationTypeField_Concepts_onCallbackComplete" />
                                                    <CallbackError EventHandler="cmbCallSituationTypeField_Concepts_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:Label ID="lblStorageMethod" runat="server" meta:resourcekey="lblStorageMethod"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbPersistSituationTypeField_Concepts" OnCallback="CallBack_cmbPersistSituationTypeField_Concepts_onCallBack"
                                                Height="26">
                                                <Content>
                                                    <ComponentArt:ComboBox ID="cmbPersistSituationTypeField_Concepts" runat="server" AutoComplete="true"
                                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                        ItemCssClass="comboitem" ItemHoverCssClass="comboConceptHover" SelectedItemCssClass="comboItemHover"
                                                        Style="width: 200px" TextBoxCssClass="comboTextBox" Enabled="False">
                                                        <ClientEvents>
                                                            <Expand EventHandler="cmbPersistSituationTypeField_Concepts_onExpand" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_StorageMethodField" />
                                                </Content>
                                                <ClientEvents>
                                                    <BeforeCallback EventHandler="cmbPersistSituationTypeField_Concepts_onBeforeCallback" />
                                                    <CallbackComplete EventHandler="cmbPersistSituationTypeField_Concepts_onCallbackComplete" />
                                                    <CallbackError EventHandler="cmbPersistSituationTypeField_Concepts_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblConceptCustomeCategoryCode" runat="server" meta:resourcekey="lblConceptCustomeCategoryCode"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbConceptCustomeCategoryCodeField_Concepts" OnCallback="CallBack_cmbConceptCustomeCategoryCodeField_Concepts_onCallBack"
                                                Height="26">
                                                <Content>
                                                    <ComponentArt:ComboBox ID="cmbConceptCustomeCategoryCodeField_Concepts" runat="server" AutoComplete="true"
                                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                        ItemCssClass="comboitem" ItemHoverCssClass="comboConceptHover" SelectedItemCssClass="comboItemHover"
                                                        Style="width: 200px" TextBoxCssClass="comboTextBox" Enabled="false">
                                                        <ClientEvents>
                                                            <Expand EventHandler="cmbConceptCustomeCategoryCodeField_Concepts_onExpand" />
                                                        </ClientEvents>
                                                    </ComponentArt:ComboBox>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_ConceptCustomeCategoryCodeFields" />
                                                </Content>
                                                <ClientEvents>
                                                    <BeforeCallback EventHandler="cmbConceptCustomeCategoryCodeField_Concepts_onBeforeCallback" />
                                                    <CallbackComplete EventHandler="cmbConceptCustomeCategoryCodeField_Concepts_onCallbackComplete" />
                                                    <CallbackError EventHandler="cmbConceptCustomeCategoryCodeField_Concepts_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table style="width: 100%;">

                        <tr>
                            <td style="width: 100%">
                                <table width="60%">
                                    <tr>
                                        <td style="width: 50%;">
                                            <table style="width: 100%;" class="BoxStyle">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblQuickSerch_Concept" runat="server" meta:resourcekey="lblQuickSerch_Concept"
                                                            Text=": جستجوی سریع" CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtSerchTerm_Concept" />
                                                                </td>
                                                                <td style="width: 5%">
                                                                    <ComponentArt:ToolBar ID="tlbItemQuickSearch" runat="server" CssClass="toolbar"
                                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                        UseFadeEffect="false">
                                                                        <Items>
                                                                            <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbConceptQuickSearch" runat="server"
                                                                                ClientSideCommand="tlbItemSearch_TlbConceptQuickSearch_onClick();" DropDownImageHeight="16px"
                                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px"
                                                                                ItemType="Command" meta:resourcekey="tlbItemSearch_TlbConceptQuickSearch" TextImageSpacing="5" />
                                                                        </Items>
                                                                    </ComponentArt:ToolBar>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 50%;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 20%" valign="middle">
                                                                    <div id="Div1" runat="server" class="DropDownHeader" meta:resourcekey="AlignObj" style="width: 100%">
                                                                        <img id="imgbox_SearchByConcept_Concepts" runat="server" alt=""
                                                                            src="~/Images/Ghadir/arrowDown.jpg" />
                                                                        <span id="header_SearchByConceptBox_Concepts" class="WhiteLabel">انتخاب مفهوم روزانه</span>
                                                                    </div>
                                                                    <div id="box_SearchByConcept_Concepts" class="dhtmlgoodies_contentBox" style="width: 36.5%; top: 100px; left: 265px; right: 0">
                                                                        <div id="subbox_SearchByConcept_Concepts" class="dhtmlgoodies_content">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <table style="width: 100%;">
                                                                                            <tr>
                                                                                                <td style="width: 90%">
                                                                                                    <table style="width: 100%;">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblConcept_Concepts" runat="server" CssClass="ContentLabel"
                                                                                                                    meta:resourcekey="lblConcept_Concepts" Text=": مفاهیم روزانه"></asp:Label>
                                                                                                            </td>
                                                                                                            <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj">
                                                                                                                <ComponentArt:ToolBar ID="TlbPaging_ConceptSearch_Concepts" runat="server"
                                                                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                                                    UseFadeEffect="false" Style="direction: ltr">
                                                                                                                    <Items>
                                                                                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_ConceptSearch_Concepts"
                                                                                                                            runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_ConceptSearch_Concepts_onClick();"
                                                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_ConceptSearch_Concepts"
                                                                                                                            TextImageSpacing="5" />
                                                                                                                        <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_ConceptSearch_Concepts"
                                                                                                                            runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_ConceptSearch_Concepts_onClick();"
                                                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                            ImageUrl="first.png" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_ConceptSearch_Concepts"
                                                                                                                            TextImageSpacing="5" />
                                                                                                                        <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_ConceptSearch_Concepts"
                                                                                                                            runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_ConceptSearch_Concepts_onClick();"
                                                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                            ImageUrl="Before.png" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_ConceptSearch_Concepts"
                                                                                                                            TextImageSpacing="5" />
                                                                                                                        <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_ConceptSearch_Concepts"
                                                                                                                            runat="server" ClientSideCommand="tlbItemNext_TlbPaging_ConceptSearch_Concepts_onClick();"
                                                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                            ImageUrl="Next.png" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_ConceptSearch_Concepts"
                                                                                                                            TextImageSpacing="5" />
                                                                                                                        <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_ConceptSearch_Concepts"
                                                                                                                            runat="server" ClientSideCommand="tlbItemLast_TlbPaging_ConceptSearch_Concepts_onClick();"
                                                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                                            ImageUrl="last.png" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_ConceptSearch_Concepts"
                                                                                                                            TextImageSpacing="5" />
                                                                                                                    </Items>
                                                                                                                </ComponentArt:ToolBar>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                                <td style="width: 10%">&nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 90%">
                                                                                                    <ComponentArt:CallBack ID="CallBack_cmbConcept_Concepts" runat="server"
                                                                                                        Height="26" OnCallback="CallBack_cmbConcept_Concepts_onCallBack">
                                                                                                        <Content>
                                                                                                            <ComponentArt:ComboBox ID="cmbConcept_Concepts" runat="server" AutoComplete="true"
                                                                                                                AutoHighlight="false" CssClass="comboBox" DataFields="BarCode" DataTextField="Name"
                                                                                                                DropDownCssClass="comboDropDown" DropDownWidth="390" DropDownHeight="250" DropDownPageSize="8"
                                                                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                                                FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbConcept_Concepts"
                                                                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client"
                                                                                                                SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                                                                <ClientTemplates>
                                                                                                                    <ComponentArt:ClientTemplate ID="ItemTemplate_cmbConcept_Concepts">
                                                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                            <tr class="dataRow">
                                                                                                                                <td class="dataCell" style="width: 40%">## DataItem.getProperty('Name') ##
                                                                                                                                </td>
                                                                                                                                <td class="dataCell" style="width: 30%">## DataItem.getProperty('Code') ##
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </ComponentArt:ClientTemplate>
                                                                                                                </ClientTemplates>
                                                                                                                <DropDownHeader>
                                                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="390">
                                                                                                                        <tr class="headingRow">
                                                                                                                            <td id="clmnName_cmbConcept_Concepts" class="headingCell" style="width: 40%; text-align: center">Name
                                                                                                                            </td>
                                                                                                                            <td id="clmnCode_cmbConcept_Concepts" class="headingCell" style="width: 30%; text-align: center">Code
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </DropDownHeader>
                                                                                                                <ClientEvents>
                                                                                                                    <Expand EventHandler="cmbConcept_Concepts_onExpand" />
                                                                                                                </ClientEvents>
                                                                                                            </ComponentArt:ComboBox>
                                                                                                            <asp:HiddenField ID="hfErrorHiddenField_cmbConcept_Concepts" runat="server" />
                                                                                                            <asp:HiddenField ID="hfConceptPageCount_Concepts" runat="server" />
                                                                                                            <asp:HiddenField ID="hfConceptCount_Concepts" runat="server" />
                                                                                                        </Content>
                                                                                                        <ClientEvents>
                                                                                                            <BeforeCallback EventHandler="CallBack_cmbConcept_Concepts_onBeforeCallback" />
                                                                                                            <CallbackComplete EventHandler="CallBack_cmbConcept_Concepts_onCallBackComplete" />
                                                                                                            <CallbackError EventHandler="CallBack_cmbConcept_Concepts_onCallbackError" />
                                                                                                        </ClientEvents>
                                                                                                    </ComponentArt:CallBack>
                                                                                                </td>
                                                                                                <td style="width: 10%">&nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 90%">
                                                                                                    <input id="txtConceptSearch_Concepts" runat="server" class="TextBoxes"
                                                                                                        style="width: 95%" type="text" />
                                                                                                </td>
                                                                                                <td style="width: 10%">
                                                                                                    <ComponentArt:ToolBar ID="TlbSearchConcept_Concepts" runat="server"
                                                                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                                        <Items>
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchConcept_Concepts"
                                                                                                                runat="server" ClientSideCommand="tlbItemSearch_TlbSearchConcept_Concepts_onClick();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchConcept_Concepts"
                                                                                                                TextImageSpacing="5" />
                                                                                                        </Items>
                                                                                                    </ComponentArt:ToolBar>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </div>
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
                        </tr>

                        <tr>
                            <td colspan="2">
                                <table style="width: 100%;" class="BoxStyle">
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td id="" class="HeaderLabel" style="width: 95%">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td id="header_ConceptsBox_Concepts" class="HeaderLabel WhiteLabel" style="width: 50%;">مفاهيم
                                                                </td>
                                                                <td id="loadingPanel_GridConcepts_Concepts" class="HeaderLabel" style="width: 45%"></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <ComponentArt:CallBack ID="CallBack_GridConcepts_Concept" OnCallback="CallBack_GridConcepts_Concept_OnCallBack"
                                                runat="server">
                                                <Content>
                                                    <ComponentArt:DataGrid ID="GridConcepts_Concepts" runat="server" AllowHorizontalScrolling="true"
                                                        CssClass="Grid" EnableViewState="true" ShowFooter="false" FillContainer="true"
                                                        FooterCssClass="GridFooter" Height="150" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                                        PageSize="10" RunningMode="Client" AllowMultipleSelect="false" AllowColumnResizing="false"
                                                        ScrollBar="Off" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                        ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                        ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                        ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="100%">
                                                        <Levels>
                                                            <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                                AllowSorting="false" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                                SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                                DataKeyField="ID" SortImageWidth="9" HoverRowCssClass="HoverRow">
                                                                <Columns>
                                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                                    <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending" meta:resourcekey="GridColumnConceptName" HeadingTextCssClass="HeadingText" />
                                                                    <%--<ComponentArt:GridColumn Align="Center" DataField="Script" DefaultSortDirection="Descending"  meta:resourcekey="GridColumnConceptScript" HeadingTextCssClass="HeadingText" />--%>
                                                                    <ComponentArt:GridColumn DataField="IdentifierCode" Align="Center" DefaultSortDirection="Descending" meta:resourcekey="GridColumnConceptIdentifierCode" HeadingTextCssClass="HeadingText" />
                                                                    <ComponentArt:GridColumn DataField="KeyColumnName" Align="Center" DefaultSortDirection="Descending" meta:resourcekey="GridColumnConceptColumnName" HeadingTextCssClass="HeadingText" />
                                                                    <ComponentArt:GridColumn DataField="Color" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnColor_GridConcepts_Concepts" DefaultSortDirection="Descending" meta:resourcekey="GridColumnConceptColor" HeadingTextCssClass="HeadingText" />
                                                                    <ComponentArt:GridColumn DataField="PeriodicTypeTitle" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnPeriodicTypeTitle_GridConcepts_Concepts" DefaultSortDirection="Descending" meta:resourcekey="GridColumnConceptPeriodicType" HeadingTextCssClass="HeadingText" />
                                                                    <ComponentArt:GridColumn DataField="PeriodicType" Visible="false" />
                                                                    <ComponentArt:GridColumn DataField="TypeTitle" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnTypeTitle_GridConcepts_Concepts" DefaultSortDirection="Descending" meta:resourcekey="GridColumnConceptType" HeadingTextCssClass="HeadingText" />
                                                                    <ComponentArt:GridColumn DataField="Type" Visible="false" />
                                                                    <ComponentArt:GridColumn DataField="CalcSituationTypeTitle" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnCalSituationTypeTitle_GridConcepts_Concepts" DefaultSortDirection="Descending" meta:resourcekey="GridColumnConceptCalcSituationType" HeadingTextCssClass="HeadingText" />
                                                                    <ComponentArt:GridColumn DataField="CalcSituationType" Visible="false" />
                                                                    <ComponentArt:GridColumn DataField="PersistSituationTypeTitle" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnPersistSituationTypeTitle_GridConcepts_Concepts" DefaultSortDirection="Descending" meta:resourcekey="GridColumnConceptPersistSituationType" HeadingTextCssClass="HeadingText" />
                                                                    <ComponentArt:GridColumn DataField="PersistSituationType" Visible="false" />
                                                                    <ComponentArt:GridColumn DataField="CustomCategoryCode" Align="Center" DataCellClientTemplateId="DataCellClientTemplate_clmnCustomeCategoryCodeTitle_GridConcepts_Concepts" DefaultSortDirection="Descending" meta:resourcekey="GridColumnConceptCustomeCategoryCodeTitle" HeadingTextCssClass="HeadingText" />
                                                                    <%--<ComponentArt:GridColumn DataField="Description" Align="Center" DefaultSortDirection="Descending"  meta:resourcekey="GridColumnConceptDescription" HeadingTextCssClass="HeadingText" />--%>
                                                                    <ComponentArt:GridColumn DataField="UserDefined" Visible="false" />
                                                                    <ComponentArt:GridColumn DataField="Script" Visible="false" />
                                                                    <ComponentArt:GridColumn DataField="CSharpCode" Visible="false" />
                                                                    <ComponentArt:GridColumn DataField="JsonObject" Visible="false" />
                                                                </Columns>
                                                            </ComponentArt:GridLevel>
                                                        </Levels>
                                                        <ClientTemplates>
                                                            <ComponentArt:ClientTemplate runat="server" ID="DataCellClientTemplate_clmnPeriodicTypeTitle_GridConcepts_Concepts">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td align="center">##GetPeriodicTypeTitle_Concept(DataItem.GetMember('PeriodicType').Value)##
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ComponentArt:ClientTemplate>
                                                            <ComponentArt:ClientTemplate runat="server" ID="DataCellClientTemplate_clmnTypeTitle_GridConcepts_Concepts">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td align="center">##GetTypeTitle_Concept(DataItem.GetMember('Type').Value)##
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ComponentArt:ClientTemplate>
                                                            <ComponentArt:ClientTemplate runat="server" ID="DataCellClientTemplate_clmnCalSituationTypeTitle_GridConcepts_Concepts">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td align="center">##GetCalSituationTypeTitle_Concept(DataItem.GetMember('CalcSituationType').Value)##
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ComponentArt:ClientTemplate>
                                                            <ComponentArt:ClientTemplate runat="server" ID="DataCellClientTemplate_clmnPersistSituationTypeTitle_GridConcepts_Concepts">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td align="center">##GetPersistSituationTypeTitle_Concept(DataItem.GetMember('PersistSituationType').Value)##
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ComponentArt:ClientTemplate>
                                                            <ComponentArt:ClientTemplate runat="server" ID="DataCellClientTemplate_clmnCustomeCategoryCodeTitle_GridConcepts_Concepts">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td align="center">##GetCustomeCategoryCodeTitle_Concept(DataItem.GetMember('CustomCategoryCode').Value)##
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ComponentArt:ClientTemplate>
                                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnColor_GridConcepts_Concepts">
                                                                <table style="width: 100%; cursor: crosshair; background-color: ##DataItem.GetMember('Color').Value##;">
                                                                    <tr>
                                                                        <td style="height: 100%;" align="center">رنگ<!--##DataItem.GetMember('Color').Value##--></td>
                                                                    </tr>
                                                                </table>
                                                            </ComponentArt:ClientTemplate>
                                                        </ClientTemplates>
                                                        <ClientEvents>
                                                            <ItemSelect EventHandler="GridConcepts_Concepts_onItemSelect" />
                                                            <Load EventHandler="GridConcepts_Concepts_onLoad" />
                                                        </ClientEvents>
                                                    </ComponentArt:DataGrid>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_Concepts" />
                                                    <asp:HiddenField runat="server" ID="hfConceptsCount_Concepts" />
                                                    <asp:HiddenField runat="server" ID="hfConceptsPageCount_Concepts" />
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="CallBack_GridConcepts_Concept_OnCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_GridConcepts_Concept_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td id="Td1" runat="server" meta:resourcekey="AlignObj" style="width: 75%;">
                                                        <ComponentArt:ToolBar runat="server" ID="TlbPaging_GridConcepts_Concepts" CssClass="toolbar"
                                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                            Style="direction: ltr" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridConcepts_Concepts"
                                                                    runat="server" ClientSideCommand="tlbConceptRefresh_TlbPaging_GridConcepts_Concepts_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                    ImageUrl="refresh.png" ImageWidth="16px" ConceptType="Command" meta:resourcekey="tlbConceptRefresh_TlbPaging_GridConcepts_Concepts"
                                                                    TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridConcepts_Concepts" runat="server"
                                                                    ClientSideCommand="tlbConceptFirst_TlbPaging_GridConcepts_Concepts_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                    ImageUrl="first.png" ImageWidth="16px" ConceptType="Command" meta:resourcekey="tlbConceptFirst_TlbPaging_GridConcepts_Concepts"
                                                                    TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridConcepts_Concepts" runat="server"
                                                                    ClientSideCommand="tlbConceptBefore_TlbPaging_GridConcepts_Concepts_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                    ImageUrl="Before.png" ImageWidth="16px" ConceptType="Command" meta:resourcekey="tlbConceptBefore_TlbPaging_GridConcepts_Concepts"
                                                                    TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridConcepts_Concepts" runat="server"
                                                                    ClientSideCommand="tlbConceptNext_TlbPaging_GridConcepts_Concepts_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                    ImageUrl="Next.png" ImageWidth="16px" ConceptType="Command" meta:resourcekey="tlbConceptNext_TlbPaging_GridConcepts_Concepts"
                                                                    TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridConcepts_Concepts" runat="server"
                                                                    ClientSideCommand="tlbConceptLast_TlbPaging_GridConcepts_Concepts_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                    ImageUrl="last.png" ImageWidth="16px" ConceptType="Command" meta:resourcekey="tlbConceptLast_TlbPaging_GridConcepts_Concepts"
                                                                    TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                            </Items>
                                                        </ComponentArt:ToolBar>
                                                    </td>
                                                    <td id="footer_GridConcepts_Concepts" class="WhiteLabel" style="width: 25%"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <ComponentArt:Dialog ID="DialogColors" runat="server" AllowDrag="true" AllowResize="false"
            Alignment="MiddleCentre" Width="325" Height="363" ContentCssClass="dlg">
            <Content>
                <div class="ttl" onmousedown="DialogColors.StartDrag(event);">
                    <a class="close" href="javascript:void(0);" onclick="DialogColors.close();this.blur();return false;">
                        <span>X</span></a>
                </div>
                <div class="con">
                    <ComponentArt:MultiPage ID="DialogPages" runat="server">
                        <ComponentArt:PageView ID="GridPage">
                            <div class="pg">
                                <ComponentArt:ColorPicker ID="ColorGrid" GridColumns="12" Mode="Grid" CssClass="grid"
                                    ColorCssClass="swatch" ColorHoverCssClass="swatch-h" ColorActiveCssClass="swatch-a"
                                    ColorGridCssClass="swatches" GridCellSpacing="1" runat="server">
                                    <ClientEvents>
                                        <ColorChanged EventHandler="color_changed" />
                                    </ClientEvents>
                                    <Colors>
                                        <ComponentArt:ColorPickerColor Hex="#ff0000" />
                                        <ComponentArt:ColorPickerColor Hex="#ff8f00" />
                                        <ComponentArt:ColorPickerColor Hex="#ffff00" />
                                        <ComponentArt:ColorPickerColor Hex="#c5ff00" />
                                        <ComponentArt:ColorPickerColor Hex="#00ff00" />
                                        <ComponentArt:ColorPickerColor Hex="#00ff98" />
                                        <ComponentArt:ColorPickerColor Hex="#00ffff" />
                                        <ComponentArt:ColorPickerColor Hex="#00c1ff" />
                                        <ComponentArt:ColorPickerColor Hex="#0000ff" />
                                        <ComponentArt:ColorPickerColor Hex="#9400ff" />
                                        <ComponentArt:ColorPickerColor Hex="#ff00ff" />
                                        <ComponentArt:ColorPickerColor Hex="#ff0058" />
                                        <ComponentArt:ColorPickerColor Hex="#e51919" />
                                        <ComponentArt:ColorPickerColor Hex="#e58b19" />
                                        <ComponentArt:ColorPickerColor Hex="#e4e519" />
                                        <ComponentArt:ColorPickerColor Hex="#b6e519" />
                                        <ComponentArt:ColorPickerColor Hex="#19e519" />
                                        <ComponentArt:ColorPickerColor Hex="#19e592" />
                                        <ComponentArt:ColorPickerColor Hex="#19e4e5" />
                                        <ComponentArt:ColorPickerColor Hex="#19b3e5" />
                                        <ComponentArt:ColorPickerColor Hex="#1919e5" />
                                        <ComponentArt:ColorPickerColor Hex="#8f19e5" />
                                        <ComponentArt:ColorPickerColor Hex="#e519e4" />
                                        <ComponentArt:ColorPickerColor Hex="#e5195f" />
                                        <ComponentArt:ColorPickerColor Hex="#a33636" />
                                        <ComponentArt:ColorPickerColor Hex="#a37336" />
                                        <ComponentArt:ColorPickerColor Hex="#a3a336" />
                                        <ComponentArt:ColorPickerColor Hex="#8aa336" />
                                        <ComponentArt:ColorPickerColor Hex="#36a336" />
                                        <ComponentArt:ColorPickerColor Hex="#36a376" />
                                        <ComponentArt:ColorPickerColor Hex="#36a3a3" />
                                        <ComponentArt:ColorPickerColor Hex="#3688a3" />
                                        <ComponentArt:ColorPickerColor Hex="#3636a3" />
                                        <ComponentArt:ColorPickerColor Hex="#7536a3" />
                                        <ComponentArt:ColorPickerColor Hex="#a336a3" />
                                        <ComponentArt:ColorPickerColor Hex="#a3365c" />
                                        <ComponentArt:ColorPickerColor Hex="#602020" />
                                        <ComponentArt:ColorPickerColor Hex="#604420" />
                                        <ComponentArt:ColorPickerColor Hex="#606020" />
                                        <ComponentArt:ColorPickerColor Hex="#516020" />
                                        <ComponentArt:ColorPickerColor Hex="#206020" />
                                        <ComponentArt:ColorPickerColor Hex="#206046" />
                                        <ComponentArt:ColorPickerColor Hex="#206060" />
                                        <ComponentArt:ColorPickerColor Hex="#205060" />
                                        <ComponentArt:ColorPickerColor Hex="#202060" />
                                        <ComponentArt:ColorPickerColor Hex="#452060" />
                                        <ComponentArt:ColorPickerColor Hex="#602060" />
                                        <ComponentArt:ColorPickerColor Hex="#602036" />
                                        <ComponentArt:ColorPickerColor Hex="#ff9999" />
                                        <ComponentArt:ColorPickerColor Hex="#ffd299" />
                                        <ComponentArt:ColorPickerColor Hex="#ffff99" />
                                        <ComponentArt:ColorPickerColor Hex="#e8ff99" />
                                        <ComponentArt:ColorPickerColor Hex="#99ff99" />
                                        <ComponentArt:ColorPickerColor Hex="#99ffd6" />
                                        <ComponentArt:ColorPickerColor Hex="#99ffff" />
                                        <ComponentArt:ColorPickerColor Hex="#99e6ff" />
                                        <ComponentArt:ColorPickerColor Hex="#9999ff" />
                                        <ComponentArt:ColorPickerColor Hex="#d499ff" />
                                        <ComponentArt:ColorPickerColor Hex="#ff99ff" />
                                        <ComponentArt:ColorPickerColor Hex="#ff99bc" />
                                        <ComponentArt:ColorPickerColor Hex="#ffe0e0" />
                                        <ComponentArt:ColorPickerColor Hex="#fff1e0" />
                                        <ComponentArt:ColorPickerColor Hex="#ffffe0" />
                                        <ComponentArt:ColorPickerColor Hex="#f8ffe0" />
                                        <ComponentArt:ColorPickerColor Hex="#e0ffe0" />
                                        <ComponentArt:ColorPickerColor Hex="#e0fff3" />
                                        <ComponentArt:ColorPickerColor Hex="#e0ffff" />
                                        <ComponentArt:ColorPickerColor Hex="#e0f7ff" />
                                        <ComponentArt:ColorPickerColor Hex="#e0e0ff" />
                                        <ComponentArt:ColorPickerColor Hex="#f2e0ff" />
                                        <ComponentArt:ColorPickerColor Hex="#ffe0ff" />
                                        <ComponentArt:ColorPickerColor Hex="#ffe0eb" />
                                        <ComponentArt:ColorPickerColor Hex="#ffffff" />
                                        <ComponentArt:ColorPickerColor Hex="#e2e2e2" />
                                        <ComponentArt:ColorPickerColor Hex="#d7d7d7" />
                                        <ComponentArt:ColorPickerColor Hex="#cdcdcd" />
                                        <ComponentArt:ColorPickerColor Hex="#b7b7b7" />
                                        <ComponentArt:ColorPickerColor Hex="#898989" />
                                        <ComponentArt:ColorPickerColor Hex="#707070" />
                                        <ComponentArt:ColorPickerColor Hex="#555555" />
                                        <ComponentArt:ColorPickerColor Hex="#464646" />
                                        <ComponentArt:ColorPickerColor Hex="#252525" />
                                        <ComponentArt:ColorPickerColor Hex="#111111" />
                                        <ComponentArt:ColorPickerColor Hex="#000000" />
                                    </Colors>
                                </ComponentArt:ColorPicker>
                            </div>
                        </ComponentArt:PageView>
                    </ComponentArt:MultiPage>
                </div>
                <div class="ftr">
                    <div id="chip">
                    </div>
                    <div id="hex">
                    </div>
                </div>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogColors_OnShow" />
                <OnClose EventHandler="DialogColors_OnClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
            runat="server" Width="280px">
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
        <iframe id="hidden_IFrame_Concepts" runat="server" style="width: 0px; height: 0px"></iframe>
        <!-- Titles -->
        <asp:HiddenField runat="server" ID="hfConceptCodeTitle" meta:resourcekey="hfConceptCodeTitle" />
        <!-- /Titles -->
        <!-- State -->
        <asp:HiddenField runat="server" ID="hfStateView_Concepts" meta:resourcekey="hfView_Concepts" />
        <asp:HiddenField runat="server" ID="hfStateAdd_Concepts" meta:resourcekey="hfAdd_Concepts" />
        <asp:HiddenField runat="server" ID="hfStateEdit_Concepts" meta:resourcekey="hfEdit_Concepts" />
        <asp:HiddenField runat="server" ID="hfStateDelete_Concepts" meta:resourcekey="hfDelete_Concepts" />
        <asp:HiddenField runat="server" ID="hfStateErrorType_Concepts" meta:resourcekey="hfErrorType_Concepts" />
        <asp:HiddenField runat="server" ID="hfStateConnectionError_Concepts" meta:resourcekey="hfConnectionError_Concepts" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_Concepts" meta:resourcekey="hfDeleteMessage_Concepts" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_Concepts" meta:resourcekey="hfCloseMessage_Concepts" />

        <!-- /State -->
        <asp:HiddenField runat="server" ID="hfConceptsPageSize_Concepts" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridConcepts_Concepts" meta:resourcekey="hfloadingPanel_GridConcepts_Concepts" />
        <!-- Enums ErrorHiddenField_TypeFields -->
        <asp:HiddenField runat="server" ID="hfJsonEnum_PeriodicType" />
        <asp:HiddenField runat="server" ID="hfJsonEnum_Type" />
        <asp:HiddenField runat="server" ID="hfJsonEnum_CalSituationType" />
        <asp:HiddenField runat="server" ID="hfJsonEnum_PersistSituationType" />
        <asp:HiddenField runat="server" ID="hfJsonEnum_CustomeCategoryCode" />

        <asp:HiddenField runat="server" ID="hfclmnName_cmbConcept_Concepts" meta:resourcekey="hfclmnName_cmbConcept_Concepts" />
        <asp:HiddenField runat="server" ID="hfclmnCode_cmbConcept_Concepts" meta:resourcekey="hfclmnCode_cmbConcept_Concepts" />
    </form>
</body>
</html>
