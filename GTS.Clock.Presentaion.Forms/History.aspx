<%@ Page Language="C#" AutoEventWireup="true" CodeFile="History.aspx.cs" Inherits="History" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
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

    <form id="HistoryForm" runat="server" meta:resourcekey="HistoryForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small;" class="BoxStyle">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%">
                            <asp:Label ID="lblRequestTopic_History" runat="server" Text=": عنوان درخواست" CssClass="WhiteLabel"
                                meta:resourcekey="lblRequestTopic_History" Style="font-weight: bold"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblRequestIssuer_History" runat="server" Text=": درخواست کننده" meta:resourcekey="lblRequestIssuer_History"
                                CssClass="HeaderLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblLastAudience_History" runat="server" CssClass="WhiteLabel" Text="آخرین بار"
                                                        meta:resourcekey="lblLastAudience_History" Style="font-weight: bold"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">
                                                    <asp:Label ID="lblFrom_History" runat="server" Text=": از" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblFrom_History"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTo_History" runat="server" Text=": تا" CssClass="WhiteLabel" meta:resourcekey="lblTo_History"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtFrom_History" type="text" style="width: 95%" class="TextBoxes" readonly="readonly" />
                                                </td>
                                                <td>
                                                    <input id="txtTo_History" type="text" style="width: 95%" class="TextBoxes" readonly="readonly" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblConsumed_History" runat="server" CssClass="WhiteLabel" Text="استفاده شده"
                                                        meta:resourcekey="lblConsumed_History" Style="font-weight: bold"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblInMonth_Consumed_History" runat="server" CssClass="WhiteLabel"
                                                        Text=": در ماه" meta:resourcekey="lblInMonth_Consumed_History"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblInYear_Consumed_History" runat="server" CssClass="WhiteLabel" Text=": در سال"
                                                        meta:resourcekey="lblInYear_Consumed_History"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">
                                                    <input id="txtInMonth_Consumed_History" type="text" readonly="readonly" class="TextBoxes"
                                                        style="width: 95%" />
                                                </td>
                                                <td>
                                                    <input id="txtInYear_Consumed_History" type="text" readonly="readonly" class="TextBoxes"
                                                        style="width: 95%" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Container_MeritLeaveRemain_History">
                                        <table id="MeritLeaveRemainBox_History" style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblMeritLeaveRemain_History" runat="server" Text="مانده مرخصی استحقاقی"
                                                        CssClass="WhiteLabel" meta:resourcekey="lblMeritLeaveRemain_History" Style="font-weight: bold"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">
                                                    <asp:Label ID="lblInMonth_MeritLeaveRemain_History" runat="server" CssClass="WhiteLabel"
                                                        Text=": در ماه" meta:resourcekey="lblInMonth_MeritLeaveRemain_History"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblInYear_MeritLeaveRemain_History" runat="server" CssClass="WhiteLabel"
                                                        Text=": در ماه" meta:resourcekey="lblInYear_MeritLeaveRemain_History"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtInMonth_MeritLeaveRemain_History" type="text" readonly="readonly" class="TextBoxes"
                                                        style="width: 95%" />
                                                </td>
                                                <td>
                                                    <input id="txtInYear_MeritLeaveRemain_History" type="text" readonly="readonly" class="TextBoxes"
                                                        style="width: 95%" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDescription_History" runat="server" CssClass="WhiteLabel" Text=": توضیحات"
                                            meta:resourcekey="lblDescription_History"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <textarea id="txtDescription_History" cols="20" name="S1" rows="12" style="width: 99%; height:190px;"
                                            readonly="readonly" class="TextBoxes"></textarea>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hfTitle_DialogHistory" meta:resourcekey="hfTitle_DialogHistory"/>
    <asp:HiddenField runat="server" ID="hfHistory_History"/>
    <asp:HiddenField runat="server" ID="ErrorHiddenField_History"/>
    </form>
</body>
</html>
