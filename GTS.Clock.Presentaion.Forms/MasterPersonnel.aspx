<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterPersonnel.aspx.cs"
    Inherits="GTS.Clock.Presentaion.WebForms.MasterPersonnel" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="MasterPersonnelMainInformationForm" runat="server" meta:resourcekey="MasterPersonnelMainInformationForm"
    onsubmit="return false;">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; height: 400px; font-family: Arial; font-size: small">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbPersonnel" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbPersonnel" runat="server" ClientSideCommand="tlbItemNew_TlbPersonnel_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbPersonnel"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbPersonnel" runat="server" ClientSideCommand="tlbItemEdit_TlbPersonnel_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbPersonnel"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbPersonnel" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemDelete_TlbPersonnel_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemDelete_TlbPersonnel" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbPersonnel" runat="server" ClientSideCommand="tlbItemSearch_TlbPersonnel_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbPersonnel"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbPersonnel" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbPersonnel" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbPersonnel_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbPersonnel" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbPersonnel_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbPersonnel"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPersonnel" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbPersonnel" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemExit_TlbPersonnel_onClick();" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_Personnel" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 45%;" class="BoxStyle">
                    <tr>
                        <td>
                            &nbsp;
                            <asp:Label ID="lblQuickSerch_Personnel" runat="server" meta:resourcekey="lblQuickSerch_Personnel"
                                Text=": جستجوی سریع" CssClass="WhiteLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 80%">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtSerchTerm_Personnel"
                                            onfocus="this.select();" onclick="this.select();" />
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbPersonnelQuickSearch" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbPersonnelQuickSearch" runat="server"
                                                    ClientSideCommand="tlbItemSearch_TlbPersonnelQuickSearch_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemSearch_TlbPersonnelQuickSearch" TextImageSpacing="5" />
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
            <td height="50%">
                <table style="width: 95%;" class="BoxStyle">
                    <tr>
                        <td style="color: White; width: 100%">
                            <table style="width: 100%">
                                <tr>
                                    <td id="header_Personnel_Personnel" class="HeaderLabel" style="width: 50%;">
                                        Personnel
                                    </td>
                                    <td id="loadingPanel_GridPersonnel_Personnel" class="HeaderLabel" style="width: 45%">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <ComponentArt:CallBack ID="CallBack_GridPersonnel_Personnel" runat="server" OnCallback="CallBack_GridPersonnel_Personnel_onCallBack">
                                <Content>
                                    <ComponentArt:DataGrid ID="GridPersonnel_Personnel" runat="server" AllowHorizontalScrolling="true"
                                        CssClass="Grid" EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter"
                                        ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                        PageSize="15" RunningMode="Client" SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false"
                                        ShowFooter="false" AllowColumnResizing="false" ScrollBar="Off" ScrollTopBottomImagesEnabled="true"
                                        ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                        ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                        ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="600px">
                                        <Levels>
                                            <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                SortImageWidth="9">
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Active" HeadingText="فعال" Width="50"
                                                        ColumnType="CheckBox" HeadingTextCssClass="HeadingText" meta:resourcekey="Active_GridPersonnel_Personnel" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="PersonCode" DefaultSortDirection="Descending"
                                                        HeadingText="شماره پرسنلی" HeadingTextCssClass="HeadingText" meta:resourcekey="PersonnelNumber_GridPersonnel_Personnel"
                                                        Width="125" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="FirstName" DefaultSortDirection="Descending"
                                                        HeadingText="نام" HeadingTextCssClass="HeadingText" meta:resourcekey="Name_GridPersonnel_Personnel"
                                                        Width="175" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="LastName" DefaultSortDirection="Descending"
                                                        HeadingText="نام خانوادگی" HeadingTextCssClass="HeadingText" meta:resourcekey="Family_GridPersonnel_Personnel"
                                                        Width="175" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="Department.Name" DefaultSortDirection="Descending"
                                                        HeadingText="بخش" HeadingTextCssClass="HeadingText" meta:resourcekey="Department_GridPersonnel_Personnel"
                                                        Width="175" />
                                                    <ComponentArt:GridColumn DataField="Department.ID" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="CardNum" DefaultSortDirection="Descending"
                                                        HeadingText="شماره کارت" HeadingTextCssClass="HeadingText" meta:resourcekey="CardNumber_GridPersonnel_Personnel"
                                                        Width="175" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="OrganizationUnit.Name" DefaultSortDirection="Descending"
                                                        HeadingText="پست سازمانی" HeadingTextCssClass="HeadingText" meta:resourcekey="OrganizationPost_GridPersonnel_Personnel"
                                                        Width="175" />
                                                    <ComponentArt:GridColumn DataField="OrganizationUnit.ID" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="OrganizationUnit.CustomCode" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="OrganizationUnit.ParentPath" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="OrganizationUnit.ParentID" Visible="false" />
                                                    <ComponentArt:GridColumn Align="Center" DataField="CurrentActiveWorkGroup" DefaultSortDirection="Descending"
                                                        HeadingText="گروه کاری" HeadingTextCssClass="HeadingText" meta:resourcekey="WorkGroup_GridPersonnel_Personnel"
                                                        Width="175" />
                                                    <ComponentArt:GridColumn DataField="Sex" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="SexTitle" Visible="false" DataCellClientTemplateId="DataCellClientTemplate_clmnSexTitle_GridPersonnel_Personnel" />
                                                    <ComponentArt:GridColumn DataField="PersonDetail.FatherName" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonDetail.MeliCode" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonDetail.MilitaryStatus" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonDetail.MilitaryStatusTitle" Visible="false"
                                                        DataCellClientTemplateId="DataCellClientTemplate_clmnMilitaryStatusTitle_GridPersonnel_Personnel" />
                                                    <ComponentArt:GridColumn DataField="PersonDetail.BirthCertificate" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonDetail.PlaceIssued" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="Education" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="MaritalStatus" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="MaritalStatusTitle" Visible="false" DataCellClientTemplateId="DataCellClientTemplate_clmnMaritalStatusTitle_GridPersonnel_Personnel" />
                                                    <ComponentArt:GridColumn DataField="PersonDetail.Status" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonDetail.Tel" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonDetail.MobileNumber" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonDetail.Address" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonDetail.EmailAddress" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonDetail.BirthPlace" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonDetail.UIBirthDate" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="CurrentActiveRuleGroup" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="CurrentActiveDateRangeGroup" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="EmploymentNum" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.ControlStation.ID" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.ControlStation.Name" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="EmploymentType.ID" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="EmploymentType.Name" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="UIEmploymentDate" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="UIEndEmploymentDate" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.UIValidationGroup.ID" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.UIValidationGroup.Name" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonDetail.Grade" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonDetail.Image" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R1" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R2" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R3" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R4" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R5" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R6" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R7" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R8" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R9" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R10" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R11" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R12" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R13" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R14" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R15" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R16" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R16Text" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R17" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R17Text" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R18" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R18Text" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R19" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R19Text" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R20" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="PersonTASpec.R20Text" Visible="false" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientTemplates>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnSexTitle_GridPersonnel_Personnel">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center">
                                                            ##GetSexTitle_Personnel(DataItem.GetMember('Sex').Value)##
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnMilitaryStatusTitle_GridPersonnel_Personnel">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center">
                                                            ##GetMilitaryStatusTitle_Personnel(DataItem.GetMember('MilitaryStatus').Value)##
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnMaritalStatusTitle_GridPersonnel_Personnel">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center">
                                                            ##GetMaritalStatusTitle_Personnel(DataItem.GetMember('MaritalStatus').Value)##
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                        </ClientTemplates>
                                        <ClientEvents>
                                            <Load EventHandler="GridPersonnel_Personnel_onLoad" />                                            
                                        </ClientEvents>
                                    </ComponentArt:DataGrid>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_Personnel" />
                                    <asp:HiddenField runat="server" ID="hfPersonnelCount_Personnel" />
                                    <asp:HiddenField runat="server" ID="hfPersonnelPageCount_Personnel" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_GridPersonnel_Personnel_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_GridPersonnel_Personnel_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 75%;" runat="server" meta:resourcekey="AlignObj">
                                        <ComponentArt:ToolBar ID="TlbPaging_GridPersonnel_Personnel" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            Style="direction: ltr" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridPersonnel_Personnel" runat="server"
                                                    ClientSideCommand="tlbItemRefresh_TlbPaging_GridPersonnel_Personnel_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridPersonnel_Personnel"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridPersonnel_Personnel" runat="server"
                                                    ClientSideCommand="tlbItemFirst_TlbPaging_GridPersonnel_Personnel_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="first.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridPersonnel_Personnel"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridPersonnel_Personnel" runat="server"
                                                    ClientSideCommand="tlbItemBefore_TlbPaging_GridPersonnel_Personnel_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridPersonnel_Personnel"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridPersonnel_Personnel" runat="server"
                                                    ClientSideCommand="tlbItemNext_TlbPaging_GridPersonnel_Personnel_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="Next.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridPersonnel_Personnel"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridPersonnel_Personnel" runat="server"
                                                    ClientSideCommand="tlbItemLast_TlbPaging_GridPersonnel_Personnel_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                    ImageUrl="last.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridPersonnel_Personnel"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="footer_GridPersonnel_Personnel" style="width: 25%" class="WhiteLabel">
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" meta:resourcekey="AlignObj" colspan="2">
                                        <table style="width: 30%;">
                                            <tr>
                                                <td id="Td6" runat="server" meta:resourcekey="LableAlign">
                                                    <asp:Label ID="lblPersonnelCount_Personnel" runat="server" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblPersonnelCount_Personnel" Text=": تعداد پرسنل"></asp:Label>
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
    </table>
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
    <asp:HiddenField runat="server" ID="hfSexList_Personnel" />
    <asp:HiddenField runat="server" ID="hfMilitaryStatusList_Personnel" />
    <asp:HiddenField runat="server" ID="hfMaritalStatusList_Personnel" />
    <asp:HiddenField runat="server" ID="hfPersonnelPageSize_Personnel" />
    <asp:HiddenField runat="server" ID="hfheader_Personnel_Personnel" meta:resourcekey="hfheader_Personnel_Personnel" />
    <asp:HiddenField runat="server" ID="hfView_Personnel" meta:resourcekey="hfView_Personnel" />
    <asp:HiddenField runat="server" ID="hfDelete_Personnel" meta:resourcekey="hfDelete_Personnel" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_Personnel" meta:resourcekey="hfDeleteMessage_Personnel" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_Personnel" meta:resourcekey="hfCloseMessage_Personnel" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridPersonnel_Personnel" meta:resourcekey="hfloadingPanel_GridPersonnel_Personnel" />
    <asp:HiddenField runat="server" ID="hfBaseDateString_Personnel" />
    <asp:HiddenField runat="server" ID="hffooter_GridPersonnel_Personnel" meta:resourcekey="hffooter_GridPersonnel_Personnel" />
    <asp:HiddenField runat="server" ID="hfErrorType_Personnel" meta:resourcekey="hfErrorType_Personnel" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Personnel" meta:resourcekey="hfConnectionError_Personnel" />
    </form>
</body>
</html>
