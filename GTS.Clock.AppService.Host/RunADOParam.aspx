<%@ Page Language="C#" AutoEventWireup="true" Inherits="RunADOParam" Codebehind="RunADOParam.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="grid1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="MonthValue_ScndCnpId" HeaderText="MonthValue_ScndCnpId"
                    SortExpression="MonthValue_ScndCnpId" />
                <asp:BoundField DataField="MonthValue_Value" HeaderText="MonthValue_Value" ReadOnly="True"
                    SortExpression="MonthValue_Value" />
                <asp:BoundField DataField="MonthValue_FromDate" HeaderText="MonthValue_FromDate"
                    SortExpression="MonthValue_FromDate" />
                <asp:BoundField DataField="MonthValue_ToDate" HeaderText="MonthValue_ToDate" SortExpression="MonthValue_ToDate" />
                <asp:BoundField DataField="MonthValue_Type" HeaderText="MonthValue_Type" SortExpression="MonthValue_Type" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ghadircopyConnectionString %>"
            SelectCommand="SELECT ScndCnpValue.ScndCnpValue_SecondaryConceptId AS MonthValue_ScndCnpId, SUM(ScndCnpValue.ScndCnpValue_Value) AS MonthValue_Value, CalcDateRange.CalculationDateRange_FromDate AS MonthValue_FromDate, CalcDateRange.CalculationDateRange_ToDate AS MonthValue_ToDate, CalcDateRange.CalculationDateRange_Type AS MonthValue_Type FROM (SELECT ScndCnpValue_ID, ScndCnpValue_SecondaryConceptId, ScndCnpValue_Index, ScndCnpValue_PersonId, ScndCnpValue_Value, ScndCnpValue_IsValid, ScndCnpValue_FromDate, ScndCnpValue_ToDate, ScndCnpValue_Type FROM TA_SecondaryConceptValue WHERE (NOT (ScndCnpValue_FromDate BETWEEN @p0 AND @p1)) AND (NOT (ScndCnpValue_ToDate BETWEEN @p2 AND @p3)) AND (NOT (ScndCnpValue_FromDate &lt; @p4)) OR (NOT (ScndCnpValue_FromDate BETWEEN @p0 AND @p1)) AND (NOT (ScndCnpValue_ToDate BETWEEN @p2 AND @p3)) AND (NOT (ScndCnpValue_ToDate &gt;= @p5))) AS ScndCnpValue INNER JOIN (SELECT CalculationDateRange_ID, CalculationDateRange_SecondaryConceptId, CalculationDateRange_PersonId, CalculationDateRange_FromDate, CalculationDateRange_ToDate, CalculationDateRange_Type FROM TA_CalculationDateRange WHERE (CalculationDateRange_PersonId = @p6)) AS CalcDateRange ON ScndCnpValue.ScndCnpValue_PersonId = CalcDateRange.CalculationDateRange_PersonId AND ScndCnpValue.ScndCnpValue_SecondaryConceptId = CalcDateRange.CalculationDateRange_SecondaryConceptId WHERE (ScndCnpValue.ScndCnpValue_FromDate BETWEEN CalcDateRange.CalculationDateRange_FromDate AND CalcDateRange.CalculationDateRange_ToDate) AND (ScndCnpValue.ScndCnpValue_ToDate BETWEEN CalcDateRange.CalculationDateRange_FromDate AND CalcDateRange.CalculationDateRange_ToDate) GROUP BY ScndCnpValue.ScndCnpValue_PersonId, ScndCnpValue.ScndCnpValue_SecondaryConceptId, CalcDateRange.CalculationDateRange_FromDate, CalcDateRange.CalculationDateRange_ToDate, CalcDateRange.CalculationDateRange_Type">
            <SelectParameters>
                <asp:ControlParameter ControlID="p0" Name="p0" PropertyName="Text" />
                <asp:ControlParameter ControlID="p1" Name="p1" PropertyName="Text" />
                <asp:ControlParameter ControlID="p0" Name="p2" PropertyName="Text" />
                <asp:ControlParameter ControlID="p1" Name="p3" PropertyName="Text" />
                <asp:ControlParameter ControlID="p0" Name="p4" PropertyName="Text" />
                <asp:ControlParameter ControlID="p1" Name="p5" PropertyName="Text" />
                <asp:ControlParameter ControlID="p2" Name="p6" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    <br />
    <table>
        <tr>
            <asp:TextBox ID="p0" runat="server"></asp:TextBox><td>
            </td>
            <td>
                from p0:
            </td>
        </tr>
        </tr>
        <tr>
            <asp:TextBox ID="p1" runat="server"></asp:TextBox><td>
            </td>
            <td>
                from p0:
            </td>
        </tr>
            <tr>
            <asp:TextBox ID="p2"  runat="server">103</asp:TextBox><td>
            </td>
            <td>
                from p6:
            </td>
        </tr>
    </table>
   
    </form>
</body>
</html>
