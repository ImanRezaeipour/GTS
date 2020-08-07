<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailSender.aspx.cs" Inherits="MailSender" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="Send" />        
    </div>
    </form>
</body>
</html>
