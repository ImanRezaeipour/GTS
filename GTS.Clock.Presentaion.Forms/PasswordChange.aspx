<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PasswordChange.aspx.cs" Inherits="GTS.Clock.Presentaion.WebForms.PasswordChange" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="FUA" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="PasswordChangeForm" runat="server" meta:resourcekey="PasswordChangeForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 50%; height: 200px; font-family: Arial; font-size: small">
        <tr>
            <td style="height: 20%">
                <ComponentArt:ToolBar ID="TlbPasswordChange" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemEndorsement_TlbPasswordChange" runat="server"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEndorsement_TlbPasswordChange"
                            TextImageSpacing="5" ClientSideCommand="tlbItemEndorsement_TlbPasswordChange_onClick();" />
                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbPasswordChange" runat="server" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemHelp_TlbPasswordChange" TextImageSpacing="5"
                            ClientSideCommand="tlbItemHelp_TlbPasswordChange_onClick();" />
                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbPasswordChange" runat="server"
                            ClientSideCommand="tlbItemFormReconstruction_TlbPasswordChange_onClick();" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbPasswordChange"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPasswordChange" runat="server" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemExit_TlbPasswordChange" TextImageSpacing="5"
                            ClientSideCommand="tlbItemExit_TlbPasswordChange_onClick();" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblUserName_PasswordChange" runat="server" meta:resourcekey="lblUserName_PasswordChange"
                    Text=": نام کاربری" CssClass="WhiteLabel"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <input type="text" runat="server" style="width: 80%;" class="TextBoxes" id="txtUserName_PasswordChange"
                    disabled="disabled" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCurrentPassword_PasswordChange" runat="server" meta:resourcekey="lblCurrentPassword_PasswordChange"
                    Text=": کلمه عبور جاری" CssClass="WhiteLabel"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <input type="password" runat="server" style="width: 80%;" class="TextBoxes" id="txtPassword_PasswordChange" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNewPassword_PasswordChange" runat="server" meta:resourcekey="lblNewPassword_PasswordChange"
                    Text=": کلمه عبور جدید" CssClass="WhiteLabel"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <input type="password" runat="server" style="width: 80%;" class="TextBoxes" id="txtNewPassword_PasswordChange" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNewPasswordRepeat_PasswordChange" runat="server" meta:resourcekey="lblNewPasswordRepeat_PasswordChange"
                    Text=": تکرار کلمه عبور جدید" CssClass="WhiteLabel"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <input type="password" runat="server" style="width: 80%;" class="TextBoxes" id="txtNewPasswordRepeat_PasswordChange" />
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
        runat="server" Width="320px">
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
    <asp:HiddenField runat="server" ID="hfCurrentUser_PasswordChange" />
    <asp:HiddenField runat="server" ID="hfChangeMessage_PasswordChange" meta:resourcekey="hfChangeMessage_PasswordChange" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_PasswordChange" meta:resourcekey="hfCloseMessage_PasswordChange" />
    <asp:HiddenField runat="server" ID="hfErrorType_PasswordChange" meta:resourcekey="hfErrorType_PasswordChange" />
    <asp:HiddenField runat="server" ID="hfConnectionError_PasswordChange" meta:resourcekey="hfConnectionError_PasswordChange" />
    </form>
</body>
</html>
