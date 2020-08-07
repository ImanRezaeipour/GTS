<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocalDateTime.aspx.cs" Inherits="LocalDateTime" %>

<%@ Register Assembly="FlashControl" Namespace="Bewise.Web.UI.WebControls" TagPrefix="Bewise" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/clock.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>    
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <script type="text/javascript" src="JS/jqueryRotate.js"></script>



    <form id="LocalDateTimeForm" runat="server" meta:resourcekey="LocalDateTimeForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <div id="divOverlay_LocalDateTime" style="visibility: visible; position: absolute; left: 0px; top: 0px; width: 100%; height: 100%; text-align: center; z-index: 1000; background-image: url('Images/Ghadir/Transparent.png');">
        </div>
        <table style="width: 100%;">
            <tr>
                <td valign="middle"  dir="ltr">
                    <%--<Bewise:FlashControl ID="HeaderFlashControl" runat="server" MovieUrl="~/swf/clock13.swf"
                        Menu="false" Scale="Exactfit" WMode="Transparent" BrowserDetection="true" Height="157px"
                        Width="157" Quality="High" Loop="true" Enabled="false" />--%>
                    <div id="clockHolder">
                        <div class="rotatingWrapper">
                            <img id="hour" alt="" src="Images/Clock/hour.png" />
                        </div>
                        <div class="rotatingWrapper">
                            <img id="min" alt="" src="Images/Clock/minute.png" />
                        </div>
                        <div class="rotatingWrapper">
                            <img id="sec" alt="" src="Images/Clock/second.png" />
                        </div>
                        <img id="clock" alt="" src="Images/Clock/clock.png" />
                    </div>
                </td>
                <td valign="middle" style="width: 50%" align="center">
                    <asp:Label ID="lblCurrentDateTime_LocalDateTime" runat="server" Text="" CssClass="HeaderLabel"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="hfheader_LocalDateTime" meta:resourcekey="hfheader_LocalDateTime" />
        <asp:HiddenField runat="server" ID="hfServerTime_LocalTime"/>
    </form>
</body>
</html>
