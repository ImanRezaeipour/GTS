<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/rotator.css" runat="server" type="text/css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="ProductsForm" runat="server" meta:resourcekey="ProductsForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; height: 100px">
        <tr>
            <td valign="middle">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 75px">
                    <tr>
                        <td style="width: 70%" valign="middle" align="center">
                            <ComponentArt:Rotator ID="ProductsRotator_Products" runat="server" CssClass="Rotator"
                                Height="90" HideEffect="None" HideEffectDuration="600" PauseOnMouseOver="true"
                                RotationType="SlideShow" ShowEffect="GradientWipe" ShowEffectDuration="1000"
                                SlidePause="4000" Width="250" XmlContentFile="XML/products.xml">
                                <SlideTemplate>
                                    <table border="0" cellpadding="0" cellspacing="2" width="240">
                                        <tr>
                                            <td align="center">
                                                <span class="ProductTitle">
                                                    <%# DataBinder.Eval(Container.DataItem, "Title") %></span><br />
                                                <span class="ProductText">
                                                    <%# DataBinder.Eval(Container.DataItem, "Text") %></span><br />
                                                <span class="ProductPrice">
                                                    <%# DataBinder.Eval(Container.DataItem, "Price") %></span><br />
                                            </td>
                                            <td style="width: 100px;">
                                                <img alt="" height="65" src='Images/Rotator/<%# DataBinder.Eval(Container.DataItem, "Image") %>'
                                                    style="border: none;" width="70" />
                                            </td>
                                        </tr>
                                    </table>
                                </SlideTemplate>
                            </ComponentArt:Rotator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hfheader_Products" meta:resourcekey="hfheader_Products"/>
    </form>
</body>
</html>
