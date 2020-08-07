<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageViewer.aspx.cs" Inherits="ImageViewer" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="ImageViewerForm" runat="server" meta:resourcekey="ImageViewerForm">
    <asp:Image ID="ImageViewerControl" runat="server" Height="121px" Width="98px" ImageUrl="~/ClientAttachements/user.png"/>
    </form>
</body>
</html>
