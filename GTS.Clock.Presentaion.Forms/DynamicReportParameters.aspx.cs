using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Model.RequestFlow;
using AspNetPersianDatePickup;
using System.Text;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class DynamicReportParameters : GTSBasePage
    {
        public StringGenerator StringBuilder
        {
            get
            {
                return new StringGenerator();
            }
        }

        public ExceptionHandler exceptionHandler
        {
            get
            {
                return new ExceptionHandler();
            }
        }

        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
            }
        }

        enum Scripts
        {
            DynamicReportParameters_onPageLoad
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            this.SetCurrentDate_ReportParameters();
            this.Test();
            this.WriteScript();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }

        private void SetCurrentDate_ReportParameters()
        {
            string strCurrentDate = string.Empty;
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "en-US":
                    strCurrentDate = DateTime.Now.ToShortDateString();
                    break;
                case "fa-IR":
                    strCurrentDate = this.LangProv.GetSysDateString(DateTime.Now);
                    break;
            }
            this.hfCurrentDate_ReportParameters.Value = strCurrentDate;
        }

        private void Test()
        {
            this.Form.Controls.Add(new LiteralControl("<table style='width: 100%;'><tr><td id='Container_WorkGroupCalendars_GroupFilter_ReportParameters'>"));
            PersianDatePickup pdpWorkGroup_GroupFilter_ReportParameters = new PersianDatePickup()
            {
                ID = "pdpWorkGroup_GroupFilter_ReportParameters",
                CssClass = "PersianDatePicker"
            };
            this.Form.Controls.Add(pdpWorkGroup_GroupFilter_ReportParameters);
            this.Form.Controls.Add(new LiteralControl("<table border='0' cellpadding='0' cellspacing='0' id='Container_gCalWorkGroup_GroupFilter_ReportParameters'><tr><td onmouseup='btn_gdpWorkGroup_GroupFilter_ReportParameters_OnMouseUp(event)'>"));
            ComponentArt.Web.UI.Calendar gdpWorkGroup_GroupFilter_ReportParameters = new ComponentArt.Web.UI.Calendar()
            {
                ID = "gdpWorkGroup_GroupFilter_ReportParameters",
                ControlType = ComponentArt.Web.UI.CalendarControlType.Picker,
                PickerCssClass = "picker",
                PickerCustomFormat = "yyyy/MM/dd",
                PickerFormat = ComponentArt.Web.UI.DateTimeFormatType.Custom,
                SelectedDate = new DateTime(2008, 1, 1),
                MaxDate = new DateTime(2122, 1, 1)
            };
            gdpWorkGroup_GroupFilter_ReportParameters.ClientEvents.SelectionChanged = new ComponentArt.Web.UI.ClientEvent("gdpWorkGroup_GroupFilter_ReportParameters_OnDateChange");
            this.Form.Controls.Add(gdpWorkGroup_GroupFilter_ReportParameters);
            this.Form.Controls.Add(new LiteralControl("</td><td style='font-size: 10px;'>&nbsp;</td><td><img id='btn_gdpWorkGroup_GroupFilter_ReportParameters' alt='' class='calendar_button' onclick='btn_gdpWorkGroup_GroupFilter_ReportParameters_OnClick(event)' onmouseup='btn_gdpWorkGroup_GroupFilter_ReportParameters_OnMouseUp(event)' src='Images/Calendar/btn_calendar.gif' /></td></tr></table>"));
            ComponentArt.Web.UI.Calendar gCalWorkGroup_GroupFilter_ReportParameters = new ComponentArt.Web.UI.Calendar()
            {
                ID = "gCalWorkGroup_GroupFilter_ReportParameters",
                AllowMonthSelection = false,
                AllowMultipleSelection = false,
                AllowWeekSelection = false,
                CalendarCssClass = "calendar",
                TitleCssClass = "title",
                ControlType = ComponentArt.Web.UI.CalendarControlType.Calendar,
                DayCssClass = "day",
                DayHeaderCssClass = "dayheader",
                DayHoverCssClass = "dayhover",
                DayNameFormat = DayNameFormat.FirstTwoLetters,
                ImagesBaseUrl = "Images/Calendar",
                MonthCssClass = "month",
                NextImageUrl = "cal_nextMonth.gif",
                NextPrevCssClass = "nextprev",
                OtherMonthDayCssClass = "othermonthday",
                PopUp = ComponentArt.Web.UI.CalendarPopUpType.Custom,
                PopUpExpandControlId = "btn_gdpWorkGroup_GroupFilter_ReportParameters",
                PrevImageUrl = "cal_prevMonth.gif",
                SelectedDate = new DateTime(2008, 1, 1),
                SelectedDayCssClass = "selectedday",
                SwapDuration = 300,
                SwapSlide = ComponentArt.Web.UI.SlideType.Linear,
                VisibleDate = new DateTime(2008, 1, 1),
                MaxDate = new DateTime(2122, 1, 1)
            };
            gCalWorkGroup_GroupFilter_ReportParameters.ClientEvents.SelectionChanged = new ComponentArt.Web.UI.ClientEvent("gCalWorkGroup_GroupFilter_ReportParameters_OnChange");
            gCalWorkGroup_GroupFilter_ReportParameters.ClientEvents.Load = new ComponentArt.Web.UI.ClientEvent("gCalWorkGroup_GroupFilter_ReportParameters_onLoad");
            this.Form.Controls.Add(gCalWorkGroup_GroupFilter_ReportParameters);
            this.Form.Controls.Add(new LiteralControl("</td></tr></table>"));
        }

        private void WriteScript()
        {
            string script = "function gdpWorkGroup_GroupFilter_ReportParameters_OnDateChange(sender, eventArgs) {\n" +
                             "var BirthDate = gdpWorkGroup_GroupFilter_ReportParameters.getSelectedDate();\n" +
                              "gCalWorkGroup_GroupFilter_ReportParameters.setSelectedDate(BirthDate);\n" +
                             "}\n" +

                          "\nfunction gCalWorkGroup_GroupFilter_ReportParameters_OnChange(sender, eventArgs) {\n" +
                              "var BirthDate = gCalWorkGroup_GroupFilter_ReportParameters.getSelectedDate();\n" +
                              "gdpWorkGroup_GroupFilter_ReportParameters.setSelectedDate(BirthDate);\n" +
                          "}\n" +

                            "\nfunction btn_gdpWorkGroup_GroupFilter_ReportParameters_OnClick(event) {\n" +
                            "if (gCalWorkGroup_GroupFilter_ReportParameters.get_popUpShowing()) {\n" +
                                "gCalWorkGroup_GroupFilter_ReportParameters.hide();\n" +
                            "}\n" +
                            "else {\n" +
                                    "gCalWorkGroup_GroupFilter_ReportParameters.setSelectedDate(gdpWorkGroup_GroupFilter_ReportParameters.getSelectedDate());\n" +
                                    "gCalWorkGroup_GroupFilter_ReportParameters.show();\n" +
                                  "}\n" +
                            "}\n" +

                            "\nfunction btn_gdpWorkGroup_GroupFilter_ReportParameters_OnMouseUp(event) {\n" +
                                "if (gCalWorkGroup_GroupFilter_ReportParameters.get_popUpShowing()) {\n" +
                                    "event.cancelBubble = true;\n" +
                                    "event.returnValue = false;\n" +
                                    "return false;\n" +
                                "}\n" +
                                "else {\n" +
                                    "return true;\n" +
                                "}\n" +
                            "}\n" +

                            "\nfunction gCalWorkGroup_GroupFilter_ReportParameters_onLoad(sender, e) {\n" +
                                "window.gCalWorkGroup_GroupFilter_ReportParameters.PopUpObject.z = 25000000;\n" +
                            "}\n" + 

                           "\nfunction Init_DynamicReportParameters() {\n" +
                               "ResetCalendars_ReportParameters();\n" +
                               "ViewCurrentLangCalendars_ReportParameters();\n" +
                           "}\n" +

                           "\nfunction ResetCalendars_ReportParameters() {\n" +
                               "var currentDate_ReportParameters = document.getElementById('hfCurrentDate_ReportParameters').value;\n" +
                               "switch (parent.parent.SysLangID) {\n" +
                                   "case 'en-US':\n" +
                                       "currentDate_ReportParameters = new Date(currentDate_ReportParameters);\n" +
                                       "gdpWorkGroup_GroupFilter_ReportParameters.setSelectedDate(currentDate_ReportParameters);\n" +
                                       "break;\n" +
                                   "case 'fa-IR':\n" +
                                       "document.getElementById('pdpWorkGroup_GroupFilter_ReportParameters').value = currentDate_ReportParameters;\n" +
                                       "break;\n" +
                               "}\n" +
                           "}\n" +

                           "\nfunction ViewCurrentLangCalendars_ReportParameters() {\n" +
                               "switch (parent.parent.SysLangID) {\n" +
                                   "case 'en-US':\n" +
                                       "document.getElementById('pdpWorkGroup_GroupFilter_ReportParameters').parentNode.removeChild(document.getElementById('pdpWorkGroup_GroupFilter_ReportParameters'));\n" +
                                       "document.getElementById('pdpWorkGroup_GroupFilter_ReportParametersimgbt').parentNode.removeChild(document.getElementById('pdpWorkGroup_GroupFilter_ReportParametersimgbt'));\n" +
                                       "break;\n" +
                                   "case 'fa-IR':\n" +
                                       "document.getElementById('Container_WorkGroupCalendars_GroupFilter_ReportParameters').removeChild(document.getElementById('Container_gCalWorkGroup_GroupFilter_ReportParameters'));\n" +
                                       "break;\n" +
                               "}\n" +
                           "}\n"
                            ;
            ScriptManager.RegisterClientScriptBlock(this, typeof(DynamicReportParameters), "Test", script, true);
        }
    }
}


