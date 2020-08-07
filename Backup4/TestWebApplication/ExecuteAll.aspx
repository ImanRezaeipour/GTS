<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExecuteAll.aspx.cs" Inherits="TestWebApplication.ExecuteAll" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 181px;
        }
        .style2
        {
            width: 276px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Timer runat="server" ID="UpdateTimer" Interval="5000" OnTick="UpdateTimer_Tick" />
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="style1">
                    ToDate
                    :</td>
                <td class="style2">
                    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999"
                        CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                        ForeColor="Black" Height="180px" Width="200px">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                    </asp:Calendar>
                </td>
                <td>
                    &nbsp;
                    <asp:Button ID="Button1" runat="server" Height="39px" onclick="Button1_Click" 
                        Text="Execute All" Width="160px" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Progress Number:
                </td>
                <td class="style2">
                    <asp:UpdatePanel runat="server" ID="TimedPanel" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="UpdateTimer" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Label ID="progresaLBL" runat="server"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;
                </td>
                <td class="style2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
