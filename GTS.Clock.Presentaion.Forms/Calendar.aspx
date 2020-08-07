<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.Calendar"
    CodeFile="Calendar.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="CSS/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="CSS/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="CSS/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="CSS/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
</head>
<body onkeydown="CalendarForm_onKeyDown(event);" style="background-color: #E6E6FA"
    onload="">
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="CalendarForm" runat="server" meta:resourcekey="CalendarForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table id="Mastertbl_Calendar" style="width: 100%; font-family: Arial; font-size: small;"
        class="borderStyle">
        <tr>
            <td colspan="5" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td width="50%">
                            <ComponentArt:ToolBar ID="TlbCalendar_DialogCalendar" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemRgister_DialogCalendar" runat="server" ClientSideCommand="tlbItemRgister_DialogCalendar_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRgister_DialogCalendar"
                                        TextImageSpacing="5" Visible="true" />
                                    <ComponentArt:ToolBarItem ID="tlbItemPeriodRepeat_DialogCalendar" runat="server"
                                        ClientSideCommand="tlbItemPeriodRepeat_DialogCalendar_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cycle.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemPeriodRepeat_DialogCalendar" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHoliday_DialogCalendar" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemHoliday_DialogCalendar_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="BallClockRed.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemHoliday_DialogCalendar" TextImageSpacing="5" Visible="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemNotHoliday_DialogCalendar" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemNotHoliday_DialogCalendar_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="BallClockWhite.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemNotHoliday_DialogCalendar" TextImageSpacing="5" Visible="false" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_tlbItemNotHoliday_DialogCalendar"
                                        runat="server" ClientSideCommand="tlbItemFormReconstruction_tlbItemNotHoliday_DialogCalendar_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_tlbItemNotHoliday_DialogCalendar"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_DialogCalendar" runat="server" ClientSideCommand="tlbItemExit_DialogCalendar_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_DialogCalendar"
                                        TextImageSpacing="5" Visible="true" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td width="10%">
                            <asp:Label runat="server" ID="lblTypes_DialogCalendar" meta:resourcekey="lblTypes_DialogCalendar"
                                Text=""></asp:Label>
                        </td>
                        <td width="40%">
                            <ComponentArt:ComboBox ID="cmbTypes_DialogCalendar" runat="server" AutoComplete="true"
                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                TextBoxEnabled="true" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover"
                                HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                SelectedItemCssClass="comboItemHover" ItemClientTemplateId="ItemClientTemplateId_cmbTypes_DialogCalendar"
                                Style="width: 100%" TextBoxCssClass="comboTextBox">
                                <ClientEvents>
                                    <Change EventHandler="cmbTypes_DialogCalendar_onChange" />
                                </ClientEvents>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="ItemClientTemplateId_cmbTypes_DialogCalendar">
                                        <table style="width: 100%; border: 1px outset black; -moz-border-radius: 5px;-webkit-border-radius: 5px;border-radius: 5px;-khtml-border-radius: 5px;">
                                            <tr>
                                                <td style="width: 10%; background-color: ##DataItem.get_value()##; border: 1px outset black; -moz-border-radius: 5px;-webkit-border-radius: 5px;border-radius: 5px;-khtml-border-radius: 5px;">
                                                </td>
                                                <td style="font-family: Arial; font-size: small; vertical-align: middle">
                                                    ## DataItem.get_text() ##
                                                </td>
                                            </tr>
                                        </table>
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                            </ComponentArt:ComboBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="12%" meta:resourcekey="AlignObj" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay11" runat="server" Text="شنبه" meta:resourcekey="lblDay1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay21" runat="server" Text="یکشنبه" meta:resourcekey="lblDay2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay31" runat="server" Text="دوشنبه" meta:resourcekey="lblDay3"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay41" runat="server" Text="سه شنبه" meta:resourcekey="lblDay4"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay51" runat="server" Text="چهارشنبه" meta:resourcekey="lblDay5"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay61" runat="server" Text="پنجشنبه" meta:resourcekey="lblDay6"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay71" runat="server" Text="جمعه" meta:resourcekey="lblDay7"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="22%">
                <table style="width: 97%;" class="borderStyle">
                    <tr>
                        <td colspan="6" class="MonthTitleStyle" align="center">
                            <asp:Label ID="lblMonth1" runat="server" Text="فروردین" meta:resourcekey="lblMonth1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal111">
                            <input id="txtcal111" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal111');"/>
                        </td>
                        <td id="tdcal121">
                            <input id="txtcal121" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal121');" />
                        </td>
                        <td id="tdcal131">
                            <input id="txtcal131" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal131');" />
                        </td>
                        <td id="tdcal141">
                            <input id="txtcal141" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal141');" />
                        </td>
                        <td id="tdcal151">
                            <input id="txtcal151" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal151');" />
                        </td>
                        <td id="tdcal161">
                            <input id="txtcal161" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal161');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal112">
                            <input id="txtcal112" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal112');" />
                        </td>
                        <td id="tdcal122">
                            <input id="txtcal122" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal122');" />
                        </td>
                        <td id="tdcal132">
                            <input id="txtcal132" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal132');" />
                        </td>
                        <td id="tdcal142">
                            <input id="txtcal142" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal142');" />
                        </td>
                        <td id="tdcal152">
                            <input id="txtcal152" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal152');" />
                        </td>
                        <td id="tdcal162">
                            <input id="txtcal162" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal162');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal113">
                            <input id="txtcal113" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal113');" />
                        </td>
                        <td id="tdcal123">
                            <input id="txtcal123" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal123');" />
                        </td>
                        <td id="tdcal133">
                            <input id="txtcal133" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal133');" />
                        </td>
                        <td id="tdcal143">
                            <input id="txtcal143" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal143');" />
                        </td>
                        <td id="tdcal153">
                            <input id="txtcal153" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal153');" />
                        </td>
                        <td id="tdcal163">
                            <input id="txtcal163" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal163');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal114">
                            <input id="txtcal114" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal114');" />
                        </td>
                        <td id="tdcal124">
                            <input id="txtcal124" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal124');" />
                        </td>
                        <td id="tdcal134">
                            <input id="txtcal134" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal134');" />
                        </td>
                        <td id="tdcal144">
                            <input id="txtcal144" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal144');" />
                        </td>
                        <td id="tdcal154">
                            <input id="txtcal154" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal154');" />
                        </td>
                        <td id="tdcal164">
                            <input id="txtcal164" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal164');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal115">
                            <input id="txtcal115" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal115');" />
                        </td>
                        <td id="tdcal125">
                            <input id="txtcal125" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal125');" />
                        </td>
                        <td id="tdcal135">
                            <input id="txtcal135" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal135');" />
                        </td>
                        <td id="tdcal145">
                            <input id="txtcal145" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal145');" />
                        </td>
                        <td id="tdcal155">
                            <input id="txtcal155" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal155');" />
                        </td>
                        <td id="tdcal165">
                            <input id="txtcal165" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal165');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal116">
                            <input id="txtcal116" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal116');" />
                        </td>
                        <td id="tdcal126">
                            <input id="txtcal126" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal126');" />
                        </td>
                        <td id="tdcal136">
                            <input id="txtcal136" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal136');" />
                        </td>
                        <td id="tdcal146">
                            <input id="txtcal146" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal146');" />
                        </td>
                        <td id="tdcal156">
                            <input id="txtcal156" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal156');" />
                        </td>
                        <td id="tdcal166">
                            <input id="txtcal166" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal166');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal117">
                            <input id="txtcal117" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal117');" />
                        </td>
                        <td id="tdcal127">
                            <input id="txtcal127" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal127');" />
                        </td>
                        <td id="tdcal137">
                            <input id="txtcal137" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal137');" />
                        </td>
                        <td id="tdcal147">
                            <input id="txtcal147" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal147');" />
                        </td>
                        <td id="tdcal157">
                            <input id="txtcal157" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal157');" />
                        </td>
                        <td id="tdcal167">
                            <input id="txtcal167" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal167');" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="22%">
                <table style="width: 97%;" class="borderStyle">
                    <tr>
                        <td colspan="6" class="MonthTitleStyle" align="center">
                            <asp:Label ID="lblMonth4" runat="server" Text="تیر" meta:resourcekey="lblMonth4"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal411">
                            <input id="txtcal411" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal411');" />
                        </td>
                        <td id="tdcal421">
                            <input id="txtcal421" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal421');" />
                        </td>
                        <td id="tdcal431">
                            <input id="txtcal431" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal431');" />
                        </td>
                        <td id="tdcal441">
                            <input id="txtcal441" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal441');" />
                        </td>
                        <td id="tdcal451">
                            <input id="txtcal451" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal451');" />
                        </td>
                        <td id="tdcal461">
                            <input id="txtcal461" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal461');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal412">
                            <input id="txtcal412" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal412');" />
                        </td>
                        <td id="tdcal422">
                            <input id="txtcal422" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal422');" />
                        </td>
                        <td id="tdcal432">
                            <input id="txtcal432" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal432');" />
                        </td>
                        <td id="tdcal442">
                            <input id="txtcal442" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal442');" />
                        </td>
                        <td id="tdcal452">
                            <input id="txtcal452" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal452');" />
                        </td>
                        <td id="tdcal462">
                            <input id="txtcal462" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal462');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal413">
                            <input id="txtcal413" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal413');" />
                        </td>
                        <td id="tdcal423">
                            <input id="txtcal423" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal423');" />
                        </td>
                        <td id="tdcal433">
                            <input id="txtcal433" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal433');" />
                        </td>
                        <td id="tdcal443">
                            <input id="txtcal443" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal443');" />
                        </td>
                        <td id="tdcal453">
                            <input id="txtcal453" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal453');" />
                        </td>
                        <td id="tdcal463">
                            <input id="txtcal463" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal463');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal414">
                            <input id="txtcal414" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal414');" />
                        </td>
                        <td id="tdcal424">
                            <input id="txtcal424" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal424');" />
                        </td>
                        <td id="tdcal434">
                            <input id="txtcal434" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal434');" />
                        </td>
                        <td id="tdcal444">
                            <input id="txtcal444" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal444');" />
                        </td>
                        <td id="tdcal454">
                            <input id="txtcal454" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal454');" />
                        </td>
                        <td id="tdcal464">
                            <input id="txtcal464" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal464');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal415">
                            <input id="txtcal415" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal415');" />
                        </td>
                        <td id="tdcal425">
                            <input id="txtcal425" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal425');" />
                        </td>
                        <td id="tdcal435">
                            <input id="txtcal435" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal435');" />
                        </td>
                        <td id="tdcal445">
                            <input id="txtcal445" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal445');" />
                        </td>
                        <td id="tdcal455">
                            <input id="txtcal455" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal455');" />
                        </td>
                        <td id="tdcal465">
                            <input id="txtcal465" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal465');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal416">
                            <input id="txtcal416" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal416');" />
                        </td>
                        <td id="tdcal426">
                            <input id="txtcal426" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal426');" />
                        </td>
                        <td id="tdcal436">
                            <input id="txtcal436" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal436');" />
                        </td>
                        <td id="tdcal446">
                            <input id="txtcal446" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal446');" />
                        </td>
                        <td id="tdcal456">
                            <input id="txtcal456" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal456');" />
                        </td>
                        <td id="tdcal466">
                            <input id="txtcal466" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal466');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal417">
                            <input id="txtcal417" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal417');" />
                        </td>
                        <td id="tdcal427">
                            <input id="txtcal427" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal427');" />
                        </td>
                        <td id="tdcal437">
                            <input id="txtcal437" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal437');" />
                        </td>
                        <td id="tdcal447">
                            <input id="txtcal447" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal447');" />
                        </td>
                        <td id="tdcal457">
                            <input id="txtcal457" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal457');" />
                        </td>
                        <td id="tdcal467">
                            <input id="txtcal467" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal467');" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="22%">
                <table style="width: 97%;" class="borderStyle">
                    <tr>
                        <td colspan="6" class="MonthTitleStyle" align="center">
                            <asp:Label ID="lblMonth7" runat="server" Text="مهر" meta:resourcekey="lblMonth7"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal711">
                            <input id="txtcal711" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal711');" />
                        </td>
                        <td id="tdcal721">
                            <input id="txtcal721" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal721');" />
                        </td>
                        <td id="tdcal731">
                            <input id="txtcal731" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal731');" />
                        </td>
                        <td id="tdcal741">
                            <input id="txtcal741" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal741');" />
                        </td>
                        <td id="tdcal751">
                            <input id="txtcal751" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal751');" />
                        </td>
                        <td id="tdcal761">
                            <input id="txtcal761" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal761');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal712">
                            <input id="txtcal712" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal712');" />
                        </td>
                        <td id="tdcal722">
                            <input id="txtcal722" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal722');" />
                        </td>
                        <td id="tdcal732">
                            <input id="txtcal732" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal732');" />
                        </td>
                        <td id="tdcal742">
                            <input id="txtcal742" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal742');" />
                        </td>
                        <td id="tdcal752">
                            <input id="txtcal752" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal752');" />
                        </td>
                        <td id="tdcal762">
                            <input id="txtcal762" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal762');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal713">
                            <input id="txtcal713" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal713');" />
                        </td>
                        <td id="tdcal723">
                            <input id="txtcal723" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal723');" />
                        </td>
                        <td id="tdcal733">
                            <input id="txtcal733" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal733');" />
                        </td>
                        <td id="tdcal743">
                            <input id="txtcal743" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal743');" />
                        </td>
                        <td id="tdcal753">
                            <input id="txtcal753" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal753');" />
                        </td>
                        <td id="tdcal763">
                            <input id="txtcal763" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal763');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal714">
                            <input id="txtcal714" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal714');" />
                        </td>
                        <td id="tdcal724">
                            <input id="txtcal724" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal724');" />
                        </td>
                        <td id="tdcal734">
                            <input id="txtcal734" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal734');" />
                        </td>
                        <td id="tdcal744">
                            <input id="txtcal744" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal744');" />
                        </td>
                        <td id="tdcal754">
                            <input id="txtcal754" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal754');" />
                        </td>
                        <td id="tdcal764">
                            <input id="txtcal764" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal764');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal715">
                            <input id="txtcal715" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal715');" />
                        </td>
                        <td id="tdcal725">
                            <input id="txtcal725" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal725');" />
                        </td>
                        <td id="tdcal735">
                            <input id="txtcal735" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal735');" />
                        </td>
                        <td id="tdcal745">
                            <input id="txtcal745" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal745');" />
                        </td>
                        <td id="tdcal755">
                            <input id="txtcal755" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal755');" />
                        </td>
                        <td id="tdcal765">
                            <input id="txtcal765" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal765');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal716">
                            <input id="txtcal716" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal716');" />
                        </td>
                        <td id="tdcal726">
                            <input id="txtcal726" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal726');" />
                        </td>
                        <td id="tdcal736">
                            <input id="txtcal736" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal736');" />
                        </td>
                        <td id="tdcal746">
                            <input id="txtcal746" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal746');" />
                        </td>
                        <td id="tdcal756">
                            <input id="txtcal756" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal756');" />
                        </td>
                        <td id="tdcal766">
                            <input id="txtcal766" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal766');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal717">
                            <input id="txtcal717" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal717');" />
                        </td>
                        <td id="tdcal727">
                            <input id="txtcal727" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal727');" />
                        </td>
                        <td id="tdcal737">
                            <input id="txtcal737" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal737');" />
                        </td>
                        <td id="tdcal747">
                            <input id="txtcal747" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal747');" />
                        </td>
                        <td id="tdcal757">
                            <input id="txtcal757" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal757');" />
                        </td>
                        <td id="tdcal767">
                            <input id="txtcal767" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal767');" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="22%">
                <table style="width: 97%;" class="borderStyle">
                    <tr>
                        <td colspan="6" class="MonthTitleStyle" align="center">
                            <asp:Label ID="lblMonth10" runat="server" Text="دی" meta:resourcekey="lblMonth10"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1011">
                            <input id="txtcal1011" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1011');" />
                        </td>
                        <td id="tdcal1021">
                            <input id="txtcal1021" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1021');" />
                        </td>
                        <td id="tdcal1031">
                            <input id="txtcal1031" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1031');" />
                        </td>
                        <td id="tdcal1041">
                            <input id="txtcal1041" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1041');" />
                        </td>
                        <td id="tdcal1051">
                            <input id="txtcal1051" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1051');" />
                        </td>
                        <td id="tdcal1061">
                            <input id="txtcal1061" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1061');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1012">
                            <input id="txtcal1012" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1012');" />
                        </td>
                        <td id="tdcal1022">
                            <input id="txtcal1022" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1022');" />
                        </td>
                        <td id="tdcal1032">
                            <input id="txtcal1032" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1032');" />
                        </td>
                        <td id="tdcal1042">
                            <input id="txtcal1042" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1042');" />
                        </td>
                        <td id="tdcal1052">
                            <input id="txtcal1052" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1052');" />
                        </td>
                        <td id="tdcal1062">
                            <input id="txtcal1062" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1062');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1013">
                            <input id="txtcal1013" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1013');" />
                        </td>
                        <td id="tdcal1023">
                            <input id="txtcal1023" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1023');" />
                        </td>
                        <td id="tdcal1033">
                            <input id="txtcal1033" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1033');" />
                        </td>
                        <td id="tdcal1043">
                            <input id="txtcal1043" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1043');" />
                        </td>
                        <td id="tdcal1053">
                            <input id="txtcal1053" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1053');" />
                        </td>
                        <td id="tdcal1063">
                            <input id="txtcal1063" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1063');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1014">
                            <input id="txtcal1014" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1014');" />
                        </td>
                        <td id="tdcal1024">
                            <input id="txtcal1024" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1024');" />
                        </td>
                        <td id="tdcal1034">
                            <input id="txtcal1034" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1034');" />
                        </td>
                        <td id="tdcal1044">
                            <input id="txtcal1044" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1044');" />
                        </td>
                        <td id="tdcal1054">
                            <input id="txtcal1054" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1054');" />
                        </td>
                        <td id="tdcal1064">
                            <input id="txtcal1064" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1064');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1015">
                            <input id="txtcal1015" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1015');" />
                        </td>
                        <td id="tdcal1025">
                            <input id="txtcal1025" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1025');" />
                        </td>
                        <td id="tdcal1035">
                            <input id="txtcal1035" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1035');" />
                        </td>
                        <td id="tdcal1045">
                            <input id="txtcal1045" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1045');" />
                        </td>
                        <td id="tdcal1055">
                            <input id="txtcal1055" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1055');" />
                        </td>
                        <td id="tdcal1065">
                            <input id="txtcal1065" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1065');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1016">
                            <input id="txtcal1016" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1016');" />
                        </td>
                        <td id="tdcal1026">
                            <input id="txtcal1026" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1026');" />
                        </td>
                        <td id="tdcal1036">
                            <input id="txtcal1036" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1036');" />
                        </td>
                        <td id="tdcal1046">
                            <input id="txtcal1046" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1046');" />
                        </td>
                        <td id="tdcal1056">
                            <input id="txtcal1056" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1056');" />
                        </td>
                        <td id="tdcal1066">
                            <input id="txtcal1066" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1066');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1017">
                            <input id="txtcal1017" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1017');" />
                        </td>
                        <td id="tdcal1027">
                            <input id="txtcal1027" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1027');" />
                        </td>
                        <td id="tdcal1037">
                            <input id="txtcal1037" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1037');" />
                        </td>
                        <td id="tdcal1047">
                            <input id="txtcal1047" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1047');" />
                        </td>
                        <td id="tdcal1057">
                            <input id="txtcal1057" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1057');" />
                        </td>
                        <td id="tdcal1067">
                            <input id="txtcal1067" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1067');" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="12%" meta:resourcekey="AlignObj" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay12" runat="server" Text="شنبه" meta:resourcekey="lblDay1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay22" runat="server" Text="یکشنبه" meta:resourcekey="lblDay2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay32" runat="server" Text="دوشنبه" meta:resourcekey="lblDay3"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay42" runat="server" Text="سه شنبه" meta:resourcekey="lblDay4"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay52" runat="server" Text="چهارشنبه" meta:resourcekey="lblDay5"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay62" runat="server" Text="پنجشنبه" meta:resourcekey="lblDay6"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay72" runat="server" Text="جمعه" meta:resourcekey="lblDay7"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="22%">
                <table style="width: 97%;" class="borderStyle">
                    <tr>
                        <td colspan="6" class="MonthTitleStyle" align="center">
                            <asp:Label ID="lblMonth2" runat="server" Text="اردیبهشت" meta:resourcekey="lblMonth2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal211">
                            <input id="txtcal211" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal211');" />
                        </td>
                        <td id="tdcal221">
                            <input id="txtcal221" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal221');" />
                        </td>
                        <td id="tdcal231">
                            <input id="txtcal231" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal231');" />
                        </td>
                        <td id="tdcal241">
                            <input id="txtcal241" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal241');" />
                        </td>
                        <td id="tdcal251">
                            <input id="txtcal251" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal251');" />
                        </td>
                        <td id="tdcal261">
                            <input id="txtcal261" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal261');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal212">
                            <input id="txtcal212" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal212');" />
                        </td>
                        <td id="tdcal222">
                            <input id="txtcal222" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal222');" />
                        </td>
                        <td id="tdcal232">
                            <input id="txtcal232" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal232');" />
                        </td>
                        <td id="tdcal242">
                            <input id="txtcal242" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal242');" />
                        </td>
                        <td id="tdcal252">
                            <input id="txtcal252" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal252');" />
                        </td>
                        <td id="tdcal262">
                            <input id="txtcal262" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal262');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal213">
                            <input id="txtcal213" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal213');" />
                        </td>
                        <td id="tdcal223">
                            <input id="txtcal223" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal223');" />
                        </td>
                        <td id="tdcal233">
                            <input id="txtcal233" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal233');" />
                        </td>
                        <td id="tdcal243">
                            <input id="txtcal243" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal243');" />
                        </td>
                        <td id="tdcal253">
                            <input id="txtcal253" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal253');" />
                        </td>
                        <td id="tdcal263">
                            <input id="txtcal263" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal263');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal214">
                            <input id="txtcal214" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal214');" />
                        </td>
                        <td id="tdcal224">
                            <input id="txtcal224" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal224');" />
                        </td>
                        <td id="tdcal234">
                            <input id="txtcal234" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal234');" />
                        </td>
                        <td id="tdcal244">
                            <input id="txtcal244" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal244');" />
                        </td>
                        <td id="tdcal254">
                            <input id="txtcal254" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal254');" />
                        </td>
                        <td id="tdcal264">
                            <input id="txtcal264" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal264');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal215">
                            <input id="txtcal215" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal215');" />
                        </td>
                        <td id="tdcal225">
                            <input id="txtcal225" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal225');" />
                        </td>
                        <td id="tdcal235">
                            <input id="txtcal235" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal235');" />
                        </td>
                        <td id="tdcal245">
                            <input id="txtcal245" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal245');" />
                        </td>
                        <td id="tdcal255">
                            <input id="txtcal255" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal255');" />
                        </td>
                        <td id="tdcal265">
                            <input id="txtcal265" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal265');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal216">
                            <input id="txtcal216" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal216');" />
                        </td>
                        <td id="tdcal226">
                            <input id="txtcal226" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal226');" />
                        </td>
                        <td id="tdcal236">
                            <input id="txtcal236" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal236');" />
                        </td>
                        <td id="tdcal246">
                            <input id="txtcal246" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal246');" />
                        </td>
                        <td id="tdcal256">
                            <input id="txtcal256" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal256');" />
                        </td>
                        <td id="tdcal266">
                            <input id="txtcal266" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal266');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal217">
                            <input id="txtcal217" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal217');" />
                        </td>
                        <td id="tdcal227">
                            <input id="txtcal227" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal227');" />
                        </td>
                        <td id="tdcal237">
                            <input id="txtcal237" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal237');" />
                        </td>
                        <td id="tdcal247">
                            <input id="txtcal247" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal247');" />
                        </td>
                        <td id="tdcal257">
                            <input id="txtcal257" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal257');" />
                        </td>
                        <td id="tdcal267">
                            <input id="txtcal267" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal267');" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 97%;" class="borderStyle">
                    <tr>
                        <td colspan="6" class="MonthTitleStyle" align="center">
                            <asp:Label ID="lblMonth5" runat="server" Text="مرداد" meta:resourcekey="lblMonth5"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal511">
                            <input id="txtcal511" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal511');" />
                        </td>
                        <td id="tdcal521">
                            <input id="txtcal521" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal521');" />
                        </td>
                        <td id="tdcal531">
                            <input id="txtcal531" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal531');" />
                        </td>
                        <td id="tdcal541">
                            <input id="txtcal541" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal541');" />
                        </td>
                        <td id="tdcal551">
                            <input id="txtcal551" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal551');" />
                        </td>
                        <td id="tdcal561">
                            <input id="txtcal561" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal561');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal512">
                            <input id="txtcal512" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal512');" />
                        </td>
                        <td id="tdcal522">
                            <input id="txtcal522" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal522');" />
                        </td>
                        <td id="tdcal532">
                            <input id="txtcal532" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal532');" />
                        </td>
                        <td id="tdcal542">
                            <input id="txtcal542" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal542');" />
                        </td>
                        <td id="tdcal552">
                            <input id="txtcal552" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal552');" />
                        </td>
                        <td id="tdcal562">
                            <input id="txtcal562" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal562');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal513">
                            <input id="txtcal513" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal513');" />
                        </td>
                        <td id="tdcal523">
                            <input id="txtcal523" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal523');" />
                        </td>
                        <td id="tdcal533">
                            <input id="txtcal533" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal533');" />
                        </td>
                        <td id="tdcal543">
                            <input id="txtcal543" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal543');" />
                        </td>
                        <td id="tdcal553">
                            <input id="txtcal553" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal553');" />
                        </td>
                        <td id="tdcal563">
                            <input id="txtcal563" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal563');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal514">
                            <input id="txtcal514" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal514');" />
                        </td>
                        <td id="tdcal524">
                            <input id="txtcal524" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal524');" />
                        </td>
                        <td id="tdcal534">
                            <input id="txtcal534" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal534');" />
                        </td>
                        <td id="tdcal544">
                            <input id="txtcal544" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal544');" />
                        </td>
                        <td id="tdcal554">
                            <input id="txtcal554" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal554');" />
                        </td>
                        <td id="tdcal564">
                            <input id="txtcal564" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal564');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal515">
                            <input id="txtcal515" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal515');" />
                        </td>
                        <td id="tdcal525">
                            <input id="txtcal525" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal525');" />
                        </td>
                        <td id="tdcal535">
                            <input id="txtcal535" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal535');" />
                        </td>
                        <td id="tdcal545">
                            <input id="txtcal545" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal545');" />
                        </td>
                        <td id="tdcal555">
                            <input id="txtcal555" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal555');" />
                        </td>
                        <td id="tdcal565">
                            <input id="txtcal565" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal565');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal516">
                            <input id="txtcal516" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal516');" />
                        </td>
                        <td id="tdcal526">
                            <input id="txtcal526" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal526');" />
                        </td>
                        <td id="tdcal536">
                            <input id="txtcal536" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal536');" />
                        </td>
                        <td id="tdcal546">
                            <input id="txtcal546" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal546');" />
                        </td>
                        <td id="tdcal556">
                            <input id="txtcal556" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal556');" />
                        </td>
                        <td id="tdcal566">
                            <input id="txtcal566" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal566');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal517">
                            <input id="txtcal517" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal517');" />
                        </td>
                        <td id="tdcal527">
                            <input id="txtcal527" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal527');" />
                        </td>
                        <td id="tdcal537">
                            <input id="txtcal537" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal537');" />
                        </td>
                        <td id="tdcal547">
                            <input id="txtcal547" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal547');" />
                        </td>
                        <td id="tdcal557">
                            <input id="txtcal557" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal557');" />
                        </td>
                        <td id="tdcal567">
                            <input id="txtcal567" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal567');" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 97%;" class="borderStyle">
                    <tr>
                        <td colspan="6" class="MonthTitleStyle" align="center">
                            <asp:Label ID="lblMonth8" runat="server" Text="آبان" meta:resourcekey="lblMonth8"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal811">
                            <input id="txtcal811" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal811');" />
                        </td>
                        <td id="tdcal821">
                            <input id="txtcal821" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal821');" />
                        </td>
                        <td id="tdcal831">
                            <input id="txtcal831" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal831');" />
                        </td>
                        <td id="tdcal841">
                            <input id="txtcal841" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal841');" />
                        </td>
                        <td id="tdcal851">
                            <input id="txtcal851" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal851');" />
                        </td>
                        <td id="tdcal861">
                            <input id="txtcal861" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal861');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal812">
                            <input id="txtcal812" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal812');" />
                        </td>
                        <td id="tdcal822">
                            <input id="txtcal822" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal822');" />
                        </td>
                        <td id="tdcal832">
                            <input id="txtcal832" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal832');" />
                        </td>
                        <td id="tdcal842">
                            <input id="txtcal842" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal842');" />
                        </td>
                        <td id="tdcal852">
                            <input id="txtcal852" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal852');" />
                        </td>
                        <td id="tdcal862">
                            <input id="txtcal862" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal862');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal813">
                            <input id="txtcal813" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal813');" />
                        </td>
                        <td id="tdcal823">
                            <input id="txtcal823" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal823');" />
                        </td>
                        <td id="tdcal833">
                            <input id="txtcal833" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal833');" />
                        </td>
                        <td id="tdcal843">
                            <input id="txtcal843" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal843');" />
                        </td>
                        <td id="tdcal853">
                            <input id="txtcal853" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal853');" />
                        </td>
                        <td id="tdcal863">
                            <input id="txtcal863" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal863');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal814">
                            <input id="txtcal814" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal814');" />
                        </td>
                        <td id="tdcal824">
                            <input id="txtcal824" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal824');" />
                        </td>
                        <td id="tdcal834">
                            <input id="txtcal834" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal834');" />
                        </td>
                        <td id="tdcal844">
                            <input id="txtcal844" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal844');" />
                        </td>
                        <td id="tdcal854">
                            <input id="txtcal854" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal854');" />
                        </td>
                        <td id="tdcal864">
                            <input id="txtcal864" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal864');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal815">
                            <input id="txtcal815" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal815');" />
                        </td>
                        <td id="tdcal825">
                            <input id="txtcal825" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal825');" />
                        </td>
                        <td id="tdcal835">
                            <input id="txtcal835" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal835');" />
                        </td>
                        <td id="tdcal845">
                            <input id="txtcal845" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal845');" />
                        </td>
                        <td id="tdcal855">
                            <input id="txtcal855" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal855');" />
                        </td>
                        <td id="tdcal865">
                            <input id="txtcal865" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal865');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal816">
                            <input id="txtcal816" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal816');" />
                        </td>
                        <td id="tdcal826">
                            <input id="txtcal826" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal826');" />
                        </td>
                        <td id="tdcal836">
                            <input id="txtcal836" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal836');" />
                        </td>
                        <td id="tdcal846">
                            <input id="txtcal846" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal846');" />
                        </td>
                        <td id="tdcal856">
                            <input id="txtcal856" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal856');" />
                        </td>
                        <td id="tdcal866">
                            <input id="txtcal866" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal866');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal817">
                            <input id="txtcal817" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal817');" />
                        </td>
                        <td id="tdcal827">
                            <input id="txtcal827" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal827');" />
                        </td>
                        <td id="tdcal837">
                            <input id="txtcal837" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal837');" />
                        </td>
                        <td id="tdcal847">
                            <input id="txtcal847" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal847');" />
                        </td>
                        <td id="tdcal857">
                            <input id="txtcal857" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal857');" />
                        </td>
                        <td id="tdcal867">
                            <input id="txtcal867" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal867');" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="22%">
                <table style="width: 97%;" class="borderStyle">
                    <tr>
                        <td colspan="6" class="MonthTitleStyle" align="center">
                            <asp:Label ID="lblMonth11" runat="server" Text="بهمن" meta:resourcekey="lblMonth11"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1111">
                            <input id="txtcal1111" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1111');" />
                        </td>
                        <td id="tdcal1121">
                            <input id="txtcal1121" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1121');" />
                        </td>
                        <td id="tdcal1131">
                            <input id="txtcal1131" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1131');" />
                        </td>
                        <td id="tdcal1141">
                            <input id="txtcal1141" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1141');" />
                        </td>
                        <td id="tdcal1151">
                            <input id="txtcal1151" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1151');" />
                        </td>
                        <td id="tdcal1161">
                            <input id="txtcal1161" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1161');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1112">
                            <input id="txtcal1112" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1112');" />
                        </td>
                        <td id="tdcal1122">
                            <input id="txtcal1122" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1122');" />
                        </td>
                        <td id="tdcal1132">
                            <input id="txtcal1132" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1132');" />
                        </td>
                        <td id="tdcal1142">
                            <input id="txtcal1142" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1142');" />
                        </td>
                        <td id="tdcal1152">
                            <input id="txtcal1152" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1152');" />
                        </td>
                        <td id="tdcal1162">
                            <input id="txtcal1162" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1162');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1113">
                            <input id="txtcal1113" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1113');" />
                        </td>
                        <td id="tdcal1123">
                            <input id="txtcal1123" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1123');" />
                        </td>
                        <td id="tdcal1133">
                            <input id="txtcal1133" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1133');" />
                        </td>
                        <td id="tdcal1143">
                            <input id="txtcal1143" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1143');" />
                        </td>
                        <td id="tdcal1153">
                            <input id="txtcal1153" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1153');" />
                        </td>
                        <td id="tdcal1163">
                            <input id="txtcal1163" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1163');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1114">
                            <input id="txtcal1114" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1114');" />
                        </td>
                        <td id="tdcal1124">
                            <input id="txtcal1124" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1124');" />
                        </td>
                        <td id="tdcal1134">
                            <input id="txtcal1134" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1134');" />
                        </td>
                        <td id="tdcal1144">
                            <input id="txtcal1144" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1144');" />
                        </td>
                        <td id="tdcal1154">
                            <input id="txtcal1154" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1154');" />
                        </td>
                        <td id="tdcal1164">
                            <input id="txtcal1164" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1164');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1115">
                            <input id="txtcal1115" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1115');" />
                        </td>
                        <td id="tdcal1125">
                            <input id="txtcal1125" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1125');" />
                        </td>
                        <td id="tdcal1135">
                            <input id="txtcal1135" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1135');" />
                        </td>
                        <td id="tdcal1145">
                            <input id="txtcal1145" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1145');" />
                        </td>
                        <td id="tdcal1155">
                            <input id="txtcal1155" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1155');" />
                        </td>
                        <td id="tdcal1165">
                            <input id="txtcal1165" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1165');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1116">
                            <input id="txtcal1116" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1116');" />
                        </td>
                        <td id="tdcal1126">
                            <input id="txtcal1126" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1126');" />
                        </td>
                        <td id="tdcal1136">
                            <input id="txtcal1136" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1136');" />
                        </td>
                        <td id="tdcal1146">
                            <input id="txtcal1146" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1146');" />
                        </td>
                        <td id="tdcal1156">
                            <input id="txtcal1156" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1156');" />
                        </td>
                        <td id="tdcal1166">
                            <input id="txtcal1166" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1166');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1117">
                            <input id="txtcal1117" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1117');" />
                        </td>
                        <td id="tdcal1127">
                            <input id="txtcal1127" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1127');" />
                        </td>
                        <td id="tdcal1137">
                            <input id="txtcal1137" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1137');" />
                        </td>
                        <td id="tdcal1147">
                            <input id="txtcal1147" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1147');" />
                        </td>
                        <td id="tdcal1157">
                            <input id="txtcal1157" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1157');" />
                        </td>
                        <td id="tdcal1167">
                            <input id="txtcal1167" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1167');" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="Td2" width="12%" meta:resourcekey="AlignObj" runat="server">
                <table style="width: 97%;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay13" runat="server" Text="شنبه" meta:resourcekey="lblDay1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay23" runat="server" Text="یکشنبه" meta:resourcekey="lblDay2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay33" runat="server" Text="دوشنبه" meta:resourcekey="lblDay3"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay43" runat="server" Text="سه شنبه" meta:resourcekey="lblDay4"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay53" runat="server" Text="چهارشنبه" meta:resourcekey="lblDay5"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay63" runat="server" Text="پنجشنبه" meta:resourcekey="lblDay6"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="DayTitleStyle">
                            <asp:Label ID="lblDay73" runat="server" Text="جمعه" meta:resourcekey="lblDay7"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="22%">
                <table style="width: 97%;" class="borderStyle">
                    <tr>
                        <td colspan="6" class="MonthTitleStyle" align="center">
                            <asp:Label ID="lblMonth3" runat="server" Text="خرداد" meta:resourcekey="lblMonth3"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal311">
                            <input id="txtcal311" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal311');" />
                        </td>
                        <td id="tdcal321">
                            <input id="txtcal321" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal321');" />
                        </td>
                        <td id="tdcal331">
                            <input id="txtcal331" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal331');" />
                        </td>
                        <td id="tdcal341">
                            <input id="txtcal341" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal341');" />
                        </td>
                        <td id="tdcal351">
                            <input id="txtcal351" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal351');" />
                        </td>
                        <td id="tdcal361">
                            <input id="txtcal361" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal361');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal312">
                            <input id="txtcal312" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal312');" />
                        </td>
                        <td id="tdcal322">
                            <input id="txtcal322" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal322');" />
                        </td>
                        <td id="tdcal332">
                            <input id="txtcal332" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal332');" />
                        </td>
                        <td id="tdcal342">
                            <input id="txtcal342" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal342');" />
                        </td>
                        <td id="tdcal352">
                            <input id="txtcal352" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal352');" />
                        </td>
                        <td id="tdcal362">
                            <input id="txtcal362" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal362');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal313">
                            <input id="txtcal313" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal313');" />
                        </td>
                        <td id="tdcal323">
                            <input id="txtcal323" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal323');" />
                        </td>
                        <td id="tdcal333">
                            <input id="txtcal333" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal333');" />
                        </td>
                        <td id="tdcal343">
                            <input id="txtcal343" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal343');" />
                        </td>
                        <td id="tdcal353">
                            <input id="txtcal353" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal353');" />
                        </td>
                        <td id="tdcal363">
                            <input id="txtcal363" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal363');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal314">
                            <input id="txtcal314" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal314');" />
                        </td>
                        <td id="tdcal324">
                            <input id="txtcal324" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal324');" />
                        </td>
                        <td id="tdcal334">
                            <input id="txtcal334" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal334');" />
                        </td>
                        <td id="tdcal344">
                            <input id="txtcal344" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal344');" />
                        </td>
                        <td id="tdcal354">
                            <input id="txtcal354" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal354');" />
                        </td>
                        <td id="tdcal364">
                            <input id="txtcal364" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal364');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal315">
                            <input id="txtcal315" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal315');" />
                        </td>
                        <td id="tdcal325">
                            <input id="txtcal325" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal325');" />
                        </td>
                        <td id="tdcal335">
                            <input id="txtcal335" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal335');" />
                        </td>
                        <td id="tdcal345">
                            <input id="txtcal345" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal345');" />
                        </td>
                        <td id="tdcal355">
                            <input id="txtcal355" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal355');" />
                        </td>
                        <td id="tdcal365">
                            <input id="txtcal365" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal365');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal316">
                            <input id="txtcal316" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal316');" />
                        </td>
                        <td id="tdcal326">
                            <input id="txtcal326" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal326');" />
                        </td>
                        <td id="tdcal336">
                            <input id="txtcal336" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal336');" />
                        </td>
                        <td id="tdcal346">
                            <input id="txtcal346" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal346');" />
                        </td>
                        <td id="tdcal356">
                            <input id="txtcal356" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal356');" />
                        </td>
                        <td id="tdcal366">
                            <input id="txtcal366" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal366');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal317">
                            <input id="txtcal317" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal317');" />
                        </td>
                        <td id="tdcal327">
                            <input id="txtcal327" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal327');" />
                        </td>
                        <td id="tdcal337">
                            <input id="txtcal337" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal337');" />
                        </td>
                        <td id="tdcal347">
                            <input id="txtcal347" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal347');" />
                        </td>
                        <td id="tdcal357">
                            <input id="txtcal357" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal357');" />
                        </td>
                        <td id="tdcal367">
                            <input id="txtcal367" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal367');" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 97%;" class="borderStyle">
                    <tr>
                        <td colspan="6" class="MonthTitleStyle" align="center">
                            <asp:Label ID="lblMonth6" runat="server" Text="شهریور" meta:resourcekey="lblMonth6"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal611">
                            <input id="txtcal611" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal611');" />
                        </td>
                        <td id="tdcal621">
                            <input id="txtcal621" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal621');" />
                        </td>
                        <td id="tdcal631">
                            <input id="txtcal631" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal631');" />
                        </td>
                        <td id="tdcal641">
                            <input id="txtcal641" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal641');" />
                        </td>
                        <td id="tdcal651">
                            <input id="txtcal651" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal651');" />
                        </td>
                        <td id="tdcal661">
                            <input id="txtcal661" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal661');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal612">
                            <input id="txtcal612" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal612');" />
                        </td>
                        <td id="tdcal622">
                            <input id="txtcal622" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal622');" />
                        </td>
                        <td id="tdcal632">
                            <input id="txtcal632" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal632');" />
                        </td>
                        <td id="tdcal642">
                            <input id="txtcal642" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal642');" />
                        </td>
                        <td id="tdcal652">
                            <input id="txtcal652" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal652');" />
                        </td>
                        <td id="tdcal662">
                            <input id="txtcal662" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal662');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal613">
                            <input id="txtcal613" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal613');" />
                        </td>
                        <td id="tdcal623">
                            <input id="txtcal623" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal623');" />
                        </td>
                        <td id="tdcal633">
                            <input id="txtcal633" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal633');" />
                        </td>
                        <td id="tdcal643">
                            <input id="txtcal643" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal643');" />
                        </td>
                        <td id="tdcal653">
                            <input id="txtcal653" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal653');" />
                        </td>
                        <td id="tdcal663">
                            <input id="txtcal663" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal663');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal614">
                            <input id="txtcal614" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal614');" />
                        </td>
                        <td id="tdcal624">
                            <input id="txtcal624" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal624');" />
                        </td>
                        <td id="tdcal634">
                            <input id="txtcal634" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal634');" />
                        </td>
                        <td id="tdcal644">
                            <input id="txtcal644" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal644');" />
                        </td>
                        <td id="tdcal654">
                            <input id="txtcal654" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal654');" />
                        </td>
                        <td id="tdcal664">
                            <input id="txtcal664" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal664');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal615">
                            <input id="txtcal615" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal615');" />
                        </td>
                        <td id="tdcal625">
                            <input id="txtcal625" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal625');" />
                        </td>
                        <td id="tdcal635">
                            <input id="txtcal635" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal635');" />
                        </td>
                        <td id="tdcal645">
                            <input id="txtcal645" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal645');" />
                        </td>
                        <td id="tdcal655">
                            <input id="txtcal655" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal655');" />
                        </td>
                        <td id="tdcal665">
                            <input id="txtcal665" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal665');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal616">
                            <input id="txtcal616" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal616');" />
                        </td>
                        <td id="tdcal626">
                            <input id="txtcal626" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal626');" />
                        </td>
                        <td id="tdcal636">
                            <input id="txtcal636" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal636');" />
                        </td>
                        <td id="tdcal646">
                            <input id="txtcal646" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal646');" />
                        </td>
                        <td id="tdcal656">
                            <input id="txtcal656" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal656');" />
                        </td>
                        <td id="tdcal666">
                            <input id="txtcal666" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal666');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal617">
                            <input id="txtcal617" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal617');" />
                        </td>
                        <td id="tdcal627">
                            <input id="txtcal627" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal627');" />
                        </td>
                        <td id="tdcal637">
                            <input id="txtcal637" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal637');" />
                        </td>
                        <td id="tdcal647">
                            <input id="txtcal647" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal647');" />
                        </td>
                        <td id="tdcal657">
                            <input id="txtcal657" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal657');" />
                        </td>
                        <td id="tdcal667">
                            <input id="txtcal667" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal667');" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 97%;" class="borderStyle">
                    <tr>
                        <td colspan="6" class="MonthTitleStyle" align="center">
                            <asp:Label ID="lblMonth9" runat="server" Text="آذر" meta:resourcekey="lblMonth9"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal911">
                            <input id="txtcal911" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal911');" />
                        </td>
                        <td id="tdcal921">
                            <input id="txtcal921" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal921');" />
                        </td>
                        <td id="tdcal931">
                            <input id="txtcal931" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal931');" />
                        </td>
                        <td id="tdcal941">
                            <input id="txtcal941" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal941');" />
                        </td>
                        <td id="tdcal951">
                            <input id="txtcal951" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal951');" />
                        </td>
                        <td id="tdcal961">
                            <input id="txtcal961" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal961');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal912">
                            <input id="txtcal912" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal912');" />
                        </td>
                        <td id="tdcal922">
                            <input id="txtcal922" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal922');" />
                        </td>
                        <td id="tdcal932">
                            <input id="txtcal932" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal932');" />
                        </td>
                        <td id="tdcal942">
                            <input id="txtcal942" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal942');" />
                        </td>
                        <td id="tdcal952">
                            <input id="txtcal952" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal952');" />
                        </td>
                        <td id="tdcal962">
                            <input id="txtcal962" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal962');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal913">
                            <input id="txtcal913" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal913');" />
                        </td>
                        <td id="tdcal923">
                            <input id="txtcal923" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal923');" />
                        </td>
                        <td id="tdcal933">
                            <input id="txtcal933" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal933');" />
                        </td>
                        <td id="tdcal943">
                            <input id="txtcal943" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal943');" />
                        </td>
                        <td id="tdcal953">
                            <input id="txtcal953" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal953');" />
                        </td>
                        <td id="tdcal963">
                            <input id="txtcal963" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal963');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal914">
                            <input id="txtcal914" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal914');" />
                        </td>
                        <td id="tdcal924">
                            <input id="txtcal924" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal924');" />
                        </td>
                        <td id="tdcal934">
                            <input id="txtcal934" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal934');" />
                        </td>
                        <td id="tdcal944">
                            <input id="txtcal944" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal944');" />
                        </td>
                        <td id="tdcal954">
                            <input id="txtcal954" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal954');" />
                        </td>
                        <td id="tdcal964">
                            <input id="txtcal964" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal964');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal915">
                            <input id="txtcal915" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal915');" />
                        </td>
                        <td id="tdcal925">
                            <input id="txtcal925" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal925');" />
                        </td>
                        <td id="tdcal935">
                            <input id="txtcal935" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal935');" />
                        </td>
                        <td id="tdcal945">
                            <input id="txtcal945" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal945');" />
                        </td>
                        <td id="tdcal955">
                            <input id="txtcal955" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal955');" />
                        </td>
                        <td id="tdcal965">
                            <input id="txtcal965" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal965');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal916">
                            <input id="txtcal916" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal916');" />
                        </td>
                        <td id="tdcal926">
                            <input id="txtcal926" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal926');" />
                        </td>
                        <td id="tdcal936">
                            <input id="txtcal936" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal936');" />
                        </td>
                        <td id="tdcal946">
                            <input id="txtcal946" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal946');" />
                        </td>
                        <td id="tdcal956">
                            <input id="txtcal956" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal956');" />
                        </td>
                        <td id="tdcal966">
                            <input id="txtcal966" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal966');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal917">
                            <input id="txtcal917" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal917');" />
                        </td>
                        <td id="tdcal927">
                            <input id="txtcal927" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal927');" />
                        </td>
                        <td id="tdcal937">
                            <input id="txtcal937" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal937');" />
                        </td>
                        <td id="tdcal947">
                            <input id="txtcal947" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal947');" />
                        </td>
                        <td id="tdcal957">
                            <input id="txtcal957" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal957');" />
                        </td>
                        <td id="tdcal967">
                            <input id="txtcal967" type="text" class="CalendarTextBoxes" readonly="readonly" onblur="txtCalInDialogCalendar_onBlur();"
                                runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()" onfocus="txtCalInDialogCalendar_onFocus('txtcal967');" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="22%">
                <table style="width: 97%;" class="borderStyle">
                    <tr>
                        <td colspan="6" class="MonthTitleStyle" align="center">
                            <asp:Label ID="lblMonth12" runat="server" Text="اسفند" meta:resourcekey="lblMonth12"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1211">
                            <input id="txtcal1211" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1211');" />
                        </td>
                        <td id="tdcal1221">
                            <input id="txtcal1221" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1221');" />
                        </td>
                        <td id="tdcal1231">
                            <input id="txtcal1231" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1231');" />
                        </td>
                        <td id="tdcal1241">
                            <input id="txtcal1241" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1241');" />
                        </td>
                        <td id="tdcal1251">
                            <input id="txtcal1251" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1251');" />
                        </td>
                        <td id="tdcal1261">
                            <input id="txtcal1261" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1261');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1212">
                            <input id="txtcal1212" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1212');" />
                        </td>
                        <td id="tdcal1222">
                            <input id="txtcal1222" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1222');" />
                        </td>
                        <td id="tdcal1232">
                            <input id="txtcal1232" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1232');" />
                        </td>
                        <td id="tdcal1242">
                            <input id="txtcal1242" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1242');" />
                        </td>
                        <td id="tdcal1252">
                            <input id="txtcal1252" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1252');" />
                        </td>
                        <td id="tdcal1262">
                            <input id="txtcal1262" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1262');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1213">
                            <input id="txtcal1213" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1213');" />
                        </td>
                        <td id="tdcal1223">
                            <input id="txtcal1223" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1223');" />
                        </td>
                        <td id="tdcal1233">
                            <input id="txtcal1233" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1233');" />
                        </td>
                        <td id="tdcal1243">
                            <input id="txtcal1243" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1243');" />
                        </td>
                        <td id="tdcal1253">
                            <input id="txtcal1253" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1253');" />
                        </td>
                        <td id="tdcal1263">
                            <input id="txtcal1263" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1263');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1214">
                            <input id="txtcal1214" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1214');" />
                        </td>
                        <td id="tdcal1224">
                            <input id="txtcal1224" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1224');" />
                        </td>
                        <td id="tdcal1234">
                            <input id="txtcal1234" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1234');" />
                        </td>
                        <td id="tdcal1244">
                            <input id="txtcal1244" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1244');" />
                        </td>
                        <td id="tdcal1254">
                            <input id="txtcal1254" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1254');" />
                        </td>
                        <td id="tdcal1264">
                            <input id="txtcal1264" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1264');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1215">
                            <input id="txtcal1215" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1215');" />
                        </td>
                        <td id="tdcal1225">
                            <input id="txtcal1225" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1225');" />
                        </td>
                        <td id="tdcal1235">
                            <input id="txtcal1235" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1235');" />
                        </td>
                        <td id="tdcal1245">
                            <input id="txtcal1245" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1245');" />
                        </td>
                        <td id="tdcal1255">
                            <input id="txtcal1255" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1255');" />
                        </td>
                        <td id="tdcal1265">
                            <input id="txtcal1265" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1265');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1216">
                            <input id="txtcal1216" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1216');" />
                        </td>
                        <td id="tdcal1226">
                            <input id="txtcal1226" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1226');" />
                        </td>
                        <td id="tdcal1236">
                            <input id="txtcal1236" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1236');" />
                        </td>
                        <td id="tdcal1246">
                            <input id="txtcal1246" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1246');" />
                        </td>
                        <td id="tdcal1256">
                            <input id="txtcal1256" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1256');" />
                        </td>
                        <td id="tdcal1266">
                            <input id="txtcal1266" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1266');" />
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcal1217">
                            <input id="txtcal1217" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1217');" />
                        </td>
                        <td id="tdcal1227">
                            <input id="txtcal1227" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1227');" />
                        </td>
                        <td id="tdcal1237">
                            <input id="txtcal1237" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1237');" />
                        </td>
                        <td id="tdcal1247">
                            <input id="txtcal1247" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1247');" />
                        </td>
                        <td id="tdcal1257">
                            <input id="txtcal1257" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1257');" />
                        </td>
                        <td id="tdcal1267">
                            <input id="txtcal1267" type="text" class="CalendarTextBoxes" readonly="readonly"
                                onblur="txtCalInDialogCalendar_onBlur();" runat="server" ondblclick="txtCalInDialogCalendar_ondblclick()"
                                onfocus="txtCalInDialogCalendar_onFocus('txtcal1267');" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" runat="server" meta:resourcekey="AlignObj">
                <table style="width: 100%;">
                    <tr align="center">
                        <td style="width: 20%">
                            <asp:Label ID="lblTypesInCalendar_Calendar" runat="server" Text=""></asp:Label>
                        </td>
                        <td id="tdShift0backgroundColor" style="width: 6%; cursor: pointer" class="BoxStyle">
                        </td>
                        <td id="tdShift1backgroundColor" style="width: 6%; cursor: pointer" class="BoxStyle">
                        </td>
                        <td id="tdShift2backgroundColor" style="width: 6%; cursor: pointer" class="BoxStyle">
                        </td>
                        <td id="tdShift3backgroundColor" style="width: 6%; cursor: pointer" class="BoxStyle">
                        </td>
                        <td id="tdShift4backgroundColor" style="width: 6%; cursor: pointer" class="BoxStyle">
                        </td>
                        <td id="tdShift5backgroundColor" style="width: 6%; cursor: pointer" class="BoxStyle">
                        </td>
                        <td id="tdShift6backgroundColor" style="width: 6%; cursor: pointer" class="BoxStyle">
                        </td>
                        <td id="tdShift7backgroundColor" style="width: 6%; cursor: pointer" class="BoxStyle">
                        </td>
                        <td id="tdShift8backgroundColor" style="width: 6%; cursor: pointer" class="BoxStyle">
                        </td>
                        <td id="tdShift9backgroundColor" style="width: 6%; cursor: pointer" class="BoxStyle">
                        </td>
                    </tr>
                </table>
            </td>
            <td colspan="2" align="center">
                <asp:Label ID="lblNextBeforeMonth_Calendar" runat="server" Text="ماه قبل/بعد" meta:resourcekey="lblNextBeforeMonth_Calendar"></asp:Label>
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        HeaderClientTemplateId="DialogPeriodRepeatheader" FooterClientTemplateId="DialogPeriodRepeatfooter"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogPeriodRepeat"
        runat="server" PreloadContentUrl="false" ContentUrl="PeriodRepeat.aspx"
        IFrameCssClass="PeriodRepeat_iFrame">
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="DialogPeriodRepeatheader">
                <table id="tbl_DialogPeriodRepeatheader" style="width: 353px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogPeriodRepeat.StartDrag(event);">
                    <tr>
                        <td width="6">
                            <img id="DialogPeriodRepeat_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="Title_DialogPeriodRepeat" valign="bottom" style="color: White; font-size: 13px;
                                        font-family: Arial; font-weight: bold">
                                    </td>
                                    <td id="CloseButton_DialogPeriodRepeat" valign="middle">
                                        <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogPeriodRepeat_IFrame').src = 'WhitePage.aspx'; DialogPeriodRepeat.Close('cancelled');" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="6">
                            <img id="DialogPeriodRepeat_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="DialogPeriodRepeatfooter">
                <table id="tbl_DialogPeriodRepeatfooter" style="width: 353px" cellpadding="0"
                    cellspacing="0" border="0">
                    <tr>
                        <td width="6">
                            <img id="DialogPeriodRepeat_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                alt="" />
                        </td>
                        <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat;
                            padding: 3px">
                        </td>
                        <td width="6">
                            <img id="DialogPeriodRepeat_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                alt="" />
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ClientEvents>
            <OnShow EventHandler="DialogPeriodRepeat_onShow" />
            <OnClose EventHandler="DialogPeriodRepeat_onClose" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
        runat="server" Width="280px">
        <Content>
            <table id="tblConfirm_DialogConfirm" style="width: 100%;" class="ConfirmStyle">
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
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCancel"
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
                        <img id="Img1" runat="server" alt="" src="~/Images/Dialog/Waiting.gif"  />
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogWaiting_onShow" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <asp:HiddenField runat="server" ID="hfBaseLists_Calendar" />
    <asp:HiddenField runat="server" ID="ErrorHiddenField_BasePanel_Calendar" />
    <asp:HiddenField runat="server" ID="hfCalData_Calendar" />
    <asp:HiddenField runat="server" ID="hfCalDataView_Calendar" />
    <asp:HiddenField runat="server" ID="ErrorHiddenField_CalData_Calendar" />
    <asp:HiddenField runat="server" ID="hfCalAxises_Calendar" />
    <asp:HiddenField runat="server" ID="ErrorHiddenField_CalAxises_Calendar" />
    <asp:HiddenField runat="server" ID="hfTitle_DialogCalendar" meta:resourcekey="hfTitle_DialogCalendar" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_Calendar" meta:resourcekey="hfCloseMessage_Calendar" />
    </form>
</body>
</html>
