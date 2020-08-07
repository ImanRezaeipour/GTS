<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="TestWebApplication.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:LoginName ID="LoginName1" runat="server" />
        <asp:LoginStatus ID="LoginStatus1" runat="server" />
    </div>
    <table>
        <tr>
            <td>
                <a href="ExecuteAll.aspx">محاسبه کلی</a>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="userSettingSaveBtn" runat="server" Text="Save USer Setting" OnClick="userSettingSaveBtn_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Get USer Setting" Style="height: 26px"
                    OnClick="Button1_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button2" runat="server" Text="Get Person Info" Style="height: 26px"
                    OnClick="Button2_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button3" runat="server" Text="Get Person Report" Style="height: 26px"
                    OnClick="Button3_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button4" runat="server" Text="Save Remain Budget" Style="height: 26px"
                    OnClick="Button4_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button5" runat="server" Text="Calendar Repeat Period" Style="height: 26px"
                    OnClick="Button5_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button6" runat="server" Text="Grid Settings" Style="height: 26px"
                    OnClick="Button6_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button7" runat="server" Text="Save Grid Settings" Style="height: 26px"
                    OnClick="Button7_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button8" runat="server" Text="Calculate!" Style="height: 26px" OnClick="Button8_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="barcodeTB" runat="server"></asp:TextBox>
                <asp:Button ID="Button9" runat="server" Text="Calculate!" Style="height: 26px" OnClick="Button9_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button10" runat="server" Text="Assgn Date Range" Style="height: 26px"
                    OnClick="Button10_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button11" runat="server" Text="Manager Under Managment" Style="height: 26px"
                    OnClick="Button11_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button12" runat="server" Text="Leave Remain" Style="height: 26px"
                    OnClick="Button12_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button13" runat="server" Text="Domain USers" Style="height: 26px"
                    OnClick="Button13_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button14" runat="server" Text="مانده مرخصی" Style="height: 26px"
                    OnClick="Button14_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button15" runat="server" Text="جستجوی گزارش کارکرد مدیریتی" Style="height: 26px"
                    OnClick="Button15_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button16" runat="server" Text="خلاصه کارتابل" Style="height: 26px"
                    OnClick="Button16_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button19" runat="server" Text="تاریخچه کارتابل" Style="height: 26px"
                    OnClick="Button19_Click1" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button17" runat="server" Text="درج درخواست بوسیله اپراتور" Style="height: 26px"
                    OnClick="Button17_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button18" runat="server" Text="بیشینه کارتابل" Style="height: 26px"
                    OnClick="Button18_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button20" runat="server" Text="ِData Access" Style="height: 26px"
                    OnClick="Button20_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button21" runat="server" Text="ِکپی قانون" Style="height: 26px" OnClick="Button21_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button22" runat="server" Text="ِکارتابل" Style="height: 26px" OnClick="Button22_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button23" runat="server" Text="Person By Page" Style="height: 26px"
                    OnClick="Button23_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button24" runat="server" Text="Kartal Search" Style="height: 26px"
                    OnClick="Button24_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button25" runat="server" Text="گزارش کارکرد مدیریتی جستجو" Style="height: 26px"
                    OnClick="Button25_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button26" runat="server" Text="درخواست اپراتور" Style="height: 26px"
                    OnClick="Button26_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button27" runat="server" Text="انتساب قوانین" Style="height: 26px"
                    OnClick="Button27_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button28" runat="server" Text="ثبت درخواست" Style="height: 26px"
                    OnClick="Button28_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button29" runat="server" Text="مراحل" Style="height: 26px" OnClick="Button29_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button30" runat="server" Text="بخش ها" Style="height: 26px" OnClick="Button30_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button31" runat="server" Text="دخواست تایید شده" Style="height: 26px"
                    OnClick="Button31_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button32" runat="server" Text="جستجوی جریان کاری" Style="height: 26px"
                    OnClick="Button32_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button33" runat="server" Text="بروزرسانی محدوده محاسبات" Style="height: 26px"
                    OnClick="Button32_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button34" runat="server" Text="Advance search" Style="height: 26px"
                    OnClick="Button34_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button35" runat="server" Text="جستجوی مدیریتی" Style="height: 26px"
                    OnClick="Button35_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button36" runat="server" Text="License Test" Style="height: 26px"
                    OnClick="Button36_Click" /><asp:TextBox ID="resultTB" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button37" runat="server" Text="گانت چارت" Style="height: 26px" OnClick="Button37_Click" /><asp:TextBox
                    ID="guntTB" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button38" runat="server" Text="ثبت دستوری" Style="height: 26px" OnClick="Button38_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button39" runat="server" Text="رزرو فیلد" Style="height: 26px" OnClick="Button39_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button40" runat="server" Text="گانت چارت" Style="height: 26px" OnClick="Button40_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button41" runat="server" Text="کاربر" Style="height: 26px" OnClick="Button41_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button42" runat="server" Text="کارکرد ماهانه - واسط کاربر" Style="height: 26px"
                    OnClick="Button42_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button43" runat="server" Text="insert rule" Style="height: 26px"
                    OnClick="Button43_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button44" runat="server" Text="انتقال مانده مرخصی" Style="height: 26px"
                    OnClick="Button44_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button45" runat="server" Text="Update person" Style="height: 26px"
                    OnClick="Button45_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button46" runat="server" Text="Delete Person" Style="height: 26px"
                    OnClick="Button46_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button47" runat="server" Text="دسترسی پیشکارت نقش" Style="height: 26px"
                    OnClick="Button47_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button48" runat="server" Text="درخت تحت مدیریت" Style="height: 26px"
                    OnClick="Button48_Click" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Button ID="Button49" runat="server" Text="تغییر مشخصات سازمانی - گروه کاری" Style="height: 26px"
                    OnClick="Button49_Click" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Button ID="Button50" runat="server" Text="ویرایش جانشین" 
                    Style="height: 26px" onclick="Button50_Click"
                    />
            </td>
        </tr>
          <tr>
            <td>
                <asp:Button ID="Button51" runat="server" Text="پیام خصوصی" 
                    Style="height: 26px" onclick="Button51_Click"
                    />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
