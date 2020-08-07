<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calc.aspx.cs" %>

<%@ Import Namespace="GTS.Clock.Business.Engine" %>
<%@ Register Assembly="JQControls" Namespace="JQControls" TagPrefix="jq" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtPersonId.Text)) return;

        var engine = new Executer();
        engine.Execute("CalcASPX", (decimal)GTS.Clock.Infrastructure.Utility.Utility.ToInteger(txtPersonId.Text), (System.DateTime)JQDatePicker1.Date);
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <jq:JQLoader ID="JQLoader1" Theme="DarkHive" runat="server">
        </jq:JQLoader>
        <div>
            <dl>
                <dt>PersonId:</dt>
                <dd>
                    <asp:TextBox ID="txtPersonId" runat="server"></asp:TextBox>
                </dd>
                <dt>Date: </dt>
                <dd>
                    <jq:JQDatePicker ID="JQDatePicker1" Regional="fa" runat="server" DateFormat="YMD"
                        IEDateFormat="YMD" IsRTL="True">
                    </jq:JQDatePicker>
                </dd>
                <dd>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Calculate" /></dd>
            </dl>
        </div>
    </form>
</body>
</html>
