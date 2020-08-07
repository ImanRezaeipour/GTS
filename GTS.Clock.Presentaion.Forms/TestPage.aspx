<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestPage.aspx.cs" Inherits="TestPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnInsertCorporation" runat="server" 
            Text="Insert Corporation" onclick="btnInsertCorporation_Click" />
        <asp:Button ID="btnCorporationUserDataAccess" runat="server" 
        onclick="btnCorporationUserDataAccess_Click" 
        Text="Insert Corporation User Data Access" Width="234px" />
        <asp:Button ID="btnGetAllUserDataAccessLevels" runat="server" 
            Text="Get All User Data Access Levels" 
            onclick="btnGetAllUserDataAccessLevels_Click" />
    </div>    
    </form>
</body>
</html>
