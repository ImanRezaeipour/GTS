<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.LeaveBudget"
    CodeFile="LeaveBudget.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/numericUpDown.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="LeaveBudgetForm" runat="server" meta:resourcekey="LeaveBudgetForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="font-family: Arial; font-size: small; width: 100%" class="BoxStyle">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbLeaveBudget_LeaveBudget" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemEndorsement_TlbLeaveBudget_LeaveBudget" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEndorsement_TlbLeaveBudget_LeaveBudget"
                                        TextImageSpacing="5" ClientSideCommand="tlbItemEndorsement_TlbLeaveBudget_LeaveBudget_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbLeaveBudget_LeaveBudget"
                                        runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbLeaveBudget_LeaveBudget_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbLeaveBudget_LeaveBudget"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbLeaveBudget_LeaveBudget" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ClientSideCommand="tlbItemExit_TlbLeaveBudget_LeaveBudget_onClick();"
                                        ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbLeaveBudget_LeaveBudget"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_LeaveBudget" style="width: 15%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 40%;">
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="lblYear_LeaveBudget" runat="server" meta:resourcekey="lblYear_LeaveBudget"
                                Text=": سال" class="WhiteLabel"></asp:Label>
                        </td>
                        <td>
                            <ComponentArt:ComboBox ID="cmbYear_LeaveBudget" runat="server" AutoComplete="true"
                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                TextBoxCssClass="comboTextBox" Style="width: 98%;" DropDownHeight="280">
                                <ClientEvents>
                                    <Change EventHandler="cmbYear_LeaveBudget_onChange" />
                                </ClientEvents>
                            </ComponentArt:ComboBox>
                        </td>
                        <td style="width: 5%">
                            <ComponentArt:ToolBar ID="TlbView_LeaveBudget" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemView_TlbView_LeaveBudget" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="NormalView.png" ClientSideCommand="tlbItemView_TlbView_LeaveBudget_onClick();"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemView_TlbView_LeaveBudget"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 5%">
                                        <input id="rdbYearBudget_LeaveBudget" type="radio" name="Budget" checked="checked"
                                            onclick="rdbYearBudget_LeaveBudget_onClick();" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblYearBudget_LeaveBudget" runat="server" Text="بودجه بندی سال" meta:resourcekey="lblYearBudget_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table style="width: 70%;">
                                <tr>
                                    <td style="width: 30%">
                                        <input id="txtYearBudgetDay_LeaveBudget" type="text" class="TextBoxes" style="width: 80%;
                                            text-align: center" onchange="DayBox_LeaveBudget_onChange('txtYearBudgetDay_LeaveBudget')"
                                            onfocus="this.select()" onclick="this.select()" />
                                    </td>
                                    <td style="width: 20%" align="center">
                                        <asp:Label ID="lblYearBudgetDay_LeaveBudget" runat="server" Text="روز و" meta:resourcekey="lblYearBudgetDay_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <MKB:TimeSelector ID="TimeSelector_Hour_LeaveBudget" runat="server" DisplaySeconds="true"
                                            MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                        </MKB:TimeSelector>
                                    </td>
                                    <td style="width: 20%" align="center">
                                        <asp:Label ID="lblYearBudgetHour_LeaveBudget" runat="server" Text="ساعت" meta:resourcekey="lblYearBudgetHour_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 5%">
                                        <input id="rdbSpecialCase_LeaveBudget" type="radio" name="Budget" onclick="rdbSpecialCase_LeaveBudget_onClick();" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSpecialCase_LeaveBudget" runat="server" Text="حالت خاص" meta:resourcekey="lblSpecialCase_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table style="width: 70%; border-top: outset 1px black; border-bottom: outset 1px black;
                                border-left: outset 1px black; border-right: outset 1px black;">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblMonth1_LeaveBudget" runat="server" meta:resourcekey="lblMonth1_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblMonth2_LeaveBudget" runat="server" meta:resourcekey="lblMonth2_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblMonth3_LeaveBudget" runat="server" meta:resourcekey="lblMonth3_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblHeaderDay_LeaveBudget" runat="server" meta:resourcekey="lblHeaderDay_LeaveBudget"
                                            Text="روز"></asp:Label>
                                    </td>
                                    <td rowspan="2">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 90%; text-align: center" class="TextBoxes"
                                                        id="txtMonth1_LeaveBudget" onchange="DayBox_LeaveBudget_onChange('txtMonth1_LeaveBudget')"
                                                        onfocus="this.select()" onclick="this.select()" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <MKB:TimeSelector ID="TimeSelector_Month1_LeaveBudget" runat="server" DisplaySeconds="true"
                                                        MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;">
                                                    </MKB:TimeSelector>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td rowspan="2">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 90%; text-align: center" class="TextBoxes"
                                                        id="txtMonth2_LeaveBudget" onchange="DayBox_LeaveBudget_onChange('txtMonth2_LeaveBudget')"
                                                        onfocus="this.select()" onclick="this.select()" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <MKB:TimeSelector ID="TimeSelector_Month2_LeaveBudget" runat="server" DisplaySeconds="true"
                                                        MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                                    </MKB:TimeSelector>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td rowspan="2">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 90%; text-align: center" class="TextBoxes"
                                                        id="txtMonth3_LeaveBudget" onchange="DayBox_LeaveBudget_onChange('txtMonth3_LeaveBudget')"
                                                        onfocus="this.select()" onclick="this.select()" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <MKB:TimeSelector ID="TimeSelector_Month3_LeaveBudget" runat="server" DisplaySeconds="true"
                                                        MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                                    </MKB:TimeSelector>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblHeaderHour_LeaveBudget" runat="server" meta:resourcekey="lblHeaderHour_LeaveBudget"
                                            Text="ساعت" class="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblMonth4_LeaveBudget" runat="server" meta:resourcekey="lblMonth4_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblMonth5_LeaveBudget" runat="server" meta:resourcekey="lblMonth5_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblMonth6_LeaveBudget" runat="server" meta:resourcekey="lblMonth6_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblHeaderDay_LeaveBudget0" runat="server" meta:resourcekey="lblHeaderDay_LeaveBudget"
                                            Text="روز" class="WhiteLabel"></asp:Label>
                                    </td>
                                    <td rowspan="2">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 90%; text-align: center" class="TextBoxes"
                                                        id="txtMonth4_LeaveBudget" onchange="DayBox_LeaveBudget_onChange('txtMonth4_LeaveBudget')"
                                                        onfocus="this.select()" onclick="this.select()" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <MKB:TimeSelector ID="TimeSelector_Month4_LeaveBudget" runat="server" DisplaySeconds="true"
                                                        MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                                    </MKB:TimeSelector>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td rowspan="2">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 90%; text-align: center" class="TextBoxes"
                                                        id="txtMonth5_LeaveBudget" onchange="DayBox_LeaveBudget_onChange('txtMonth5_LeaveBudget')"
                                                        onfocus="this.select()" onclick="this.select()" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <MKB:TimeSelector ID="TimeSelector_Month5_LeaveBudget" runat="server" DisplaySeconds="true"
                                                        MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                                    </MKB:TimeSelector>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td rowspan="2">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 90%; text-align: center" class="TextBoxes"
                                                        id="txtMonth6_LeaveBudget" onchange="DayBox_LeaveBudget_onChange('txtMonth6_LeaveBudget')"
                                                        onfocus="this.select()" onclick="this.select()" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <MKB:TimeSelector ID="TimeSelector_Month6_LeaveBudget" runat="server" DisplaySeconds="true"
                                                        MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                                    </MKB:TimeSelector>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblHeaderHour_LeaveBudget0" runat="server" meta:resourcekey="lblHeaderHour_LeaveBudget"
                                            Text="ساعت" class="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblMonth7_LeaveBudget" runat="server" meta:resourcekey="lblMonth7_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblMonth8_LeaveBudget" runat="server" meta:resourcekey="lblMonth8_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblMonth9_LeaveBudget" runat="server" meta:resourcekey="lblMonth9_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblHeaderDay_LeaveBudget1" runat="server" meta:resourcekey="lblHeaderDay_LeaveBudget"
                                            Text="روز" class="WhiteLabel"></asp:Label>
                                    </td>
                                    <td rowspan="2">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 90%; text-align: center" class="TextBoxes"
                                                        id="txtMonth7_LeaveBudget" onchange="DayBox_LeaveBudget_onChange('txtMonth7_LeaveBudget')"
                                                        onfocus="this.select()" onclick="this.select()" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <MKB:TimeSelector ID="TimeSelector_Month7_LeaveBudget" runat="server" DisplaySeconds="true"
                                                        MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                                    </MKB:TimeSelector>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td rowspan="2">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 90%; text-align: center" class="TextBoxes"
                                                        id="txtMonth8_LeaveBudget" onchange="DayBox_LeaveBudget_onChange('txtMonth8_LeaveBudget')"
                                                        onfocus="this.select()" onclick="this.select()" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <MKB:TimeSelector ID="TimeSelector_Month8_LeaveBudget" runat="server" DisplaySeconds="true"
                                                        MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                                    </MKB:TimeSelector>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td rowspan="2">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 90%; text-align: center" class="TextBoxes"
                                                        id="txtMonth9_LeaveBudget" onchange="DayBox_LeaveBudget_onChange('txtMonth9_LeaveBudget')"
                                                        onfocus="this.select()" onclick="this.select()" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <MKB:TimeSelector ID="TimeSelector_Month9_LeaveBudget" runat="server" DisplaySeconds="true"
                                                        MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                                    </MKB:TimeSelector>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblHeaderHour_LeaveBudget1" runat="server" meta:resourcekey="lblHeaderHour_LeaveBudget"
                                            Text="ساعت" class="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblMonth10_LeaveBudget" runat="server" meta:resourcekey="lblMonth10_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblMonth11_LeaveBudget" runat="server" meta:resourcekey="lblMonth11_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblMonth12_LeaveBudget" runat="server" meta:resourcekey="lblMonth12_LeaveBudget"
                                            class="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblHeaderDay_LeaveBudget2" runat="server" meta:resourcekey="lblHeaderDay_LeaveBudget"
                                            Text="روز" class="WhiteLabel"></asp:Label>
                                    </td>
                                    <td rowspan="2">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 90%; text-align: center" class="TextBoxes"
                                                        id="txtMonth10_LeaveBudget" onchange="DayBox_LeaveBudget_onChange('txtMonth10_LeaveBudget')"
                                                        onfocus="this.select()" onclick="this.select()" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <MKB:TimeSelector ID="TimeSelector_Month10_LeaveBudget" runat="server" DisplaySeconds="true"
                                                        MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                                    </MKB:TimeSelector>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td rowspan="2">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 90%; text-align: center" class="TextBoxes"
                                                        id="txtMonth11_LeaveBudget" onchange="DayBox_LeaveBudget_onChange('txtMonth11_LeaveBudget')"
                                                        onfocus="this.select()" onclick="this.select()" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <MKB:TimeSelector ID="TimeSelector_Month11_LeaveBudget" runat="server" DisplaySeconds="true"
                                                        MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                                    </MKB:TimeSelector>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td rowspan="2">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 90%; text-align: center" class="TextBoxes"
                                                        id="txtMonth12_LeaveBudget" onchange="DayBox_LeaveBudget_onChange('txtMonth12_LeaveBudget')"
                                                        onfocus="this.select()" onclick="this.select()" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <MKB:TimeSelector ID="TimeSelector_Month12_LeaveBudget" runat="server" DisplaySeconds="true"
                                                        MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr">
                                                    </MKB:TimeSelector>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblHeaderHour_LeaveBudget2" runat="server" meta:resourcekey="lblHeaderHour_LeaveBudget"
                                            Text="ساعت" class="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td id="Td8" runat="server" meta:resourcekey="LableAlign" style="width: 12%">
                            <asp:Label ID="lblDescription_LeaveBudget" runat="server" meta:resourcekey="lblDescription_LeaveBudget"
                                Text=": توضیحات" class="WhiteLabel"></asp:Label>
                        </td>
                        <td style="width: 93%">
                            <textarea cols="20" rows="3" class="TextBoxes" id="txtDescription_LeaveBudget" style="width: 99%;
                                height: 45px"></textarea>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
        runat="server" Width="300px">
        <Content>
            <table style="width: 100%;" class="ConfirmStyle">
                <tr align="center">
                    <td colspan="2">
                        <asp:Label ID="lblConfirm" runat="server" CssClass="WhiteLabel"></asp:Label>
                    </td>
                </tr>
                <tr align="center">
                    <td style="width: 50%">
                        <ComponentArt:ToolBar ID="TlbOkConfirm" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                            ItemSpacing="1px" UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemOk_TlbOkConfirm" runat="server" ClientSideCommand="tlbItemOk_TlbOkConfirm_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemOk_TlbOkConfirm"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                    <td>
                        <ComponentArt:ToolBar ID="TlbCancelConfirm" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                            ItemSpacing="1px" UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbCancelConfirm" runat="server" ClientSideCommand="tlbItemCancel_TlbCancelConfirm_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCancelConfirm"
                                    TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
            </table>
        </Content>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" Modal="true" AllowResize="false"
        runat="server" AllowDrag="false" Alignment="MiddleCentre" ID="DialogWaiting">
        <Content>
            <table>
                <tr>
                    <td>
                        <img id="Img1" runat="server" alt="" src="~/Images/Dialog/Waiting.gif" />
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogWaiting_onShow" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <asp:HiddenField runat="server" ID="Title_DialogLeaveBudget" meta:resourcekey="Title_DialogLeaveBudget" />
    <asp:HiddenField runat="server" ID="hfBudgetAxises_LeaveBudget" />
    <asp:HiddenField runat="server" ID="ErrorHiddenField_BudgetAxises_LeaveBudget" />
    <asp:HiddenField runat="server" ID="hfCurrentYear_LeaveBudget" />
    <asp:HiddenField runat="server" ID="hfLeaveBudget_LeaveBudget" />
    <asp:HiddenField runat="server" ID="ErrorHiddenField_LeaveBudget" />
    <asp:HiddenField runat="server" ID="hfErrorType_LeaveBudget" meta:resourcekey="hfErrorType_LeaveBudget" />
    <asp:HiddenField runat="server" ID="hfConnectionError_LeaveBudget" meta:resourcekey="hfConnectionError_LeaveBudget" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_LeaveBudget" meta:resourcekey="hfCloseMessage_LeaveBudget" />
    </form>
</body>
</html>
