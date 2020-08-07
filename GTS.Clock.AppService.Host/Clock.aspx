<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Clock.aspx.cs" Inherits="GTS.Clock.AppService.Host.Clock" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GTS Calculation Test</title>
    <style type="text/css">
        .style1
        {
            width: 199px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" dir="rtl">
            <tr>
                <td width="50%" style="border: solid 1px black">
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="center">
                                <h2>
                                    آماده سازی:</h2>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                شماره پرسنلی:
                            </td>
                            <td>
                                <asp:TextBox ID="barcodeTB" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                تاریخ:
                            </td>
                            <td>
                                <asp:TextBox ID="dateTB" runat="server" Text="1388/08/01"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="calcReadyBtn" runat="server" Text="شروع" OnClick="calcReadyBtn_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="border: solid 1px black">
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="center">
                                <h2>
                                    محاسبه:</h2>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                شماره پرسنلی:
                            </td>
                            <td>
                                <asp:TextBox ID="barcode2TB" ReadOnly="true" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                بازه زمانی :
                            </td>
                            <td>
                                از
                                <asp:TextBox ID="fromTB" Width="70px" runat="server" Text="1388/08/01" ReadOnly="True"></asp:TextBox>
                                تا
                                <asp:TextBox ID="toDateTB" Width="70px" runat="server" Text="1388/09/01"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="Button1" runat="server" Text="شروع" OnClick="Button1_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="50%" style="border: solid 1px black">
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="center">
                                <h2>
                                    آماده سازی دسته جمعی:</h2>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                شمارهای پرسنلی منفصل با ویرگول:
                            </td>
                            <td>
                                <asp:TextBox ID="barcodesTB" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                تاریخ:
                            </td>
                            <td>
                                <asp:TextBox ID="datsTB" runat="server" Text="1388/08/01"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="Button2" runat="server" Text="شروع" OnClick="Button2_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="border: solid 1px black">
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="center">
                                <h2>
                                    محاسبه دسته جمعی:</h2>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                بازه زمانی :
                            </td>
                            <td>
                                از
                                <asp:TextBox ID="fromAllTB" Width="70px" runat="server" Text="1388/08/01" ReadOnly="True"></asp:TextBox>
                                تا
                                <asp:TextBox ID="toAllTB" Width="70px" runat="server" Text="1388/09/01"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" class="style1">
                                <br>
                                <br />
                                <asp:Button ID="Button3" runat="server" Text="شروع" OnClick="Button3_Click" />
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <h3>
                        <asp:Label ID="msgLbl" runat="server"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    <h2>
                        گزارش عمومی:</h2>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
