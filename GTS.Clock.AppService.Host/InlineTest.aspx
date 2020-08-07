<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InlineTest.aspx.cs" Inherits="GTS.Clock.AppService.Host.InlineTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    The Date is now <%= DateTime.Now.ToShortDateString() %>
    </div>
    <br /><br />
    <asp:Button ID="btn" runat="server" Text="Bind Now" onclick="btn_Click" />
    <br /><br />
    <asp:GridView ID="myGrid" runat="server" AutoGenerateColumns="false">
    <Columns>
    <asp:BoundField DataField="Name" HeaderText="Shift Name" />
    <asp:BoundField DataField="Color" HeaderText="Shift Color" />
    <asp:BoundField DataField="ShiftTypeTitle"   HeaderText="Shift Type" />
    </Columns>
    </asp:GridView>
   
    </form>
</body>
</html>
