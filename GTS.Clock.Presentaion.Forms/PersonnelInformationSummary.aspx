<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonnelInformationSummary.aspx.cs"
    Inherits="PersonnelInformationSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="PersonnelInformationSummaryForm" runat="server" meta:resourcekey="PersonnelInformationSummaryForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%;">
        <tr>
            <td id="PersonnelInformationSummaryContainer_PersonnelInformationSummary" runat="server" valign="top">
            </td>
            <td style="width: 40%">
                <iframe id="CurrentPersonnelImage_PersonnelInformationSummary" runat="server" scrolling="yes"
                    style="width: 95%; overflow:hidden" allowtransparency="true" frameborder="0"></iframe>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hfheader_PersonnelInformationSummary" meta:resourcekey="hfheader_PersonnelInformationSummary"/>
    <asp:HiddenField runat="server" ID="ErrorHiddenField_PersonnelInformationSummary"/>
    <asp:HiddenField runat="server" ID="hfCurrentPersonnelImage_PersonnelInformationSummary"/>
    <%--<asp:HiddenField runat="server" ID="CurrentPersonnelID_PersonnelInformationSummary"/>--%>
    </form>
</body>
</html>
