<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArchiveTest.aspx.cs" Inherits="TestWebApplication.ArchiveTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:LoginName ID="LoginName1" runat="server" />
            <asp:LoginStatus ID="LoginStatus1" runat="server" />
        </div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="button1" runat="server" Text="ExsitsArchive" OnClick="button1_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="barcodeTb" Text="Barcode" runat="server"></asp:TextBox>
                    <asp:Button ID="button2" runat="server" Text="Copy To Archive" OnClick="button2_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="button3" runat="server" Text="Show Archive" OnClick="button3_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="button4" runat="server" Text="Search" OnClick="button4_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="button5" runat="server" Text="Get Map Setting" OnClick="button5_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="button6" runat="server" Text="Set Map Setting" OnClick="button6_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="button7" runat="server" Text="Set Concept Value" 
                        onclick="button7_Click"  />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
