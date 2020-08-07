using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dundas.Charting.WebControl;
using ComponentArt.Web.UI;
using System.Drawing;
using Ajax;
using System.Collections;
using System.Globalization;
using System.Resources;
using System.Configuration;
using System.Data;
using System.IO;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Security.Cryptography;
using GTS.Clock.Business.UI;
using System.Web.Script.Serialization;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.GridSettings;
using GTS.Clock.Model.UI;
using System.Reflection;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class MonthlyOperationGanttChartSchema : GTSBasePage
    {
        enum LoadState
        {
            None,
            Manager,
            Personnel
        }

        public BGanttChartClientSettings MonthlyOperationGanttChartClientSettingsBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BGanttChartClientSettings>();
            }
        }


        public OperationYearMonthProvider operationYearMonthProvider
        {
            get
            {
                return new OperationYearMonthProvider();
            }
        }

        public JavaScriptSerializer JsSerializer
        {
            get
            {
                return new JavaScriptSerializer();
            }
        }

        public StringGenerator StringBuilder
        {
            get
            {
                return new StringGenerator();
            }
        }

        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
            }
        }

        public ExceptionHandler exceptionHandler
        {
            get
            {
                return new ExceptionHandler();
            }
        }

        internal class TaskFeatures
        {
            public string Key { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string FromTime { get; set; }
            public string ToTime { get; set; }
            public string SerieIndex { get; set; }
            public string DataPointIndex { get; set; }
            public string DateKey { get; set; }
            public string DayName { get; set; }
            public string Date { get; set; }
            public string UIDate { get; set; }
        }

        internal class CurrentUserObj
        {
            public string UserID { get; set; }
            public string UserName { get; set; }
            public string PersonnelID { get; set; }
            public string PersonnelName { get; set; }
        }

        internal class DateRangeDetails
        {
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string Order { get; set; }
        }

        internal class GanttChartSettingsProxy
        {
            public string ConceptCaption { get; set; }
            public string ConceptTitle { get; set; }
            public bool ViewState { get; set; }
        }

        enum Scripts
        {
            Alert_Box,
            MonthlyOperationGanttChartSchema_onPageLoad,
            DialogMonthlyOperationGanttChartSchema_Operations,
            DialogUserInformation_onPageLoad,
            DialogHourlyRequestOnAbsence_onPageLoad,
            DialogDailyRequestOnAbsence_onPageLoad,
            DialogRequestOnUnallowableOverTime_onPageLoad,
            HelpForm_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            this.GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.CallbackStateContent = CallbackStateContent.All;
            if (!this.CallBack_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.IsCallback && !CallBack_cmbMonth_MonthlyOperationGanttChartSchema.IsCallback && !CallBack_GridSettings_MonthlyOperationGanttChartSchema.IsCallback)
            {
                this.CheckMonthlyOperationGanttChartSchemaLoadAccess_MonthlyOperationGanttChartSchema();
                this.SetCurrentUser_MonthlyOperationGanttChartSchema();
                this.Fill_cmbYear_MonthlyOperationGanttChartSchema();
                this.ClearSessions_MonthlyOperationGanttChartSchema();
                this.CustomizeTlbMasterMonthlyOperation_MonthlyOperationGanttChartSchema();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void CheckMonthlyOperationGanttChartSchemaLoadAccess_MonthlyOperationGanttChartSchema()
        {
            string[] retMessage = new string[4];
            try
            {
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("PID") && HttpContext.Current.Request.QueryString.AllKeys.Contains("LoadState"))
                {
                    BPersonMonthlyWorkedTime MonthlyOperationBusiness = BusinessHelper.GetBusinessInstance<BPersonMonthlyWorkedTime>(new KeyValuePair<string, object>("personId", decimal.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["PID"]))));
                    LoadState LS = (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["LoadState"]));
                    switch (LS)
                    {
                        case LoadState.Manager:
                            MonthlyOperationBusiness.CheckMonthlyOperationGanttChartSchemaLoadAccess_onManagerState();
                            break;
                        case LoadState.Personnel:
                            MonthlyOperationBusiness.CheckMonthlyOperationGanttChartSchemaLoadAccess_onPersonnelState();
                            break;
                    }
                }
            }
            catch (BaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            }
        }

        private void CustomizeTlbMasterMonthlyOperation_MonthlyOperationGanttChartSchema()
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("LoadState"))
            {
                LoadState LS = (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["LoadState"]));
                switch (LS)
                {
                    case LoadState.Manager:
                        this.TlbMonthlyOperation.Items[0].Visible = false;
                        this.TlbMonthlyOperation.Items[2].Visible = false;
                        this.TlbMonthlyOperation.Items[3].Visible = false;
                        break;
                    case LoadState.Personnel:
                        break;
                }
            }
        }

        private void SetCurrentUser_MonthlyOperationGanttChartSchema()
        {
            CurrentUserObj currentUserObj = new CurrentUserObj();
            currentUserObj.UserID = BUser.CurrentUser.ID.ToString();
            currentUserObj.UserName = BUser.CurrentUser.UserName;
            currentUserObj.PersonnelID = BUser.CurrentUser.Person.ID.ToString();
            currentUserObj.PersonnelName = BUser.CurrentUser.Person.Name;

            this.hfCurrentUser_MonthlyOperationGanttChartSchema.Value = this.JsSerializer.Serialize(currentUserObj);
        }

        private void Fill_cmbYear_MonthlyOperationGanttChartSchema()
        {
            this.operationYearMonthProvider.GetOperationYear(this.cmbYear_MonthlyOperationGanttChartSchema, this.hfCurrentYear_MonthlyOperationGanttChartSchema);
        }

        protected override void InitializeCulture()
        {
            string LngID = this.LangProv.GetCurrentLanguage();
            this.SetCurrentCultureResObjs(LngID);
            base.InitializeCulture();
        }

        private void SetCurrentCultureResObjs(string LangID)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }

        private void GenerateGanttSeries_ChartMonthlyOperation_MonthlyOperationGanttChartSchema()
        {
            this.GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Series.Clear();
            this.GenerateBasicSeries_ChartMonthlyOperation_MonthlyOperationGanttChartSchema();

            IList<GanttChartSettingsProxy> GanttChartSettingsProxyList = this.GetGanttChartSettings_MonthlyOperationGanttChartSchema();
            if (GanttChartSettingsProxyList.Count > 0)
            {
                for (int i = 0; i < GanttChartSettingsProxyList.Count; i++)
                {
                    Series s = new Series(GanttChartSettingsProxyList[i].ConceptTitle);
                    s.YValuesPerPoint = 2;
                    s.Type = SeriesChartType.Gantt;
                    s.CustomAttributes = "PointWidth=1";
                    s.BorderColor = Color.FromArgb(180, 26, 59, 105);
                    s.BackGradientType = GradientType.HorizontalCenter;
                    s.LegendText = GanttChartSettingsProxyList[i].ConceptCaption;
                    s.ShowInLegend = false;
                    this.GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Series.Add(s);
                }
            }
        }

        private void CustomizeGanttLegends_MonthlyOperationGanttChartSchema(Dictionary<string, Series> AvailableGanttSeriesDic)
        {
            foreach (string serieKey in AvailableGanttSeriesDic.Keys)
            {
                this.GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Series[serieKey].ShowInLegend = true;
            }
        }

        private void GenerateBasicSeries_ChartMonthlyOperation_MonthlyOperationGanttChartSchema()
        {
            Series t0 = new Series("ConstractorSerie");
            t0.YValuesPerPoint = 2;
            t0.Type = SeriesChartType.Gantt;
            t0.CustomAttributes = "PointWidth=1";
            t0.BorderColor = Color.FromArgb(180, 26, 59, 105);
            t0.Color = Color.White;
            t0.ShowInLegend = false;
            this.GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Series.Add(t0);

            Series t1 = new Series("ShiftSerie");
            t1.YValuesPerPoint = 2;
            t1.Type = SeriesChartType.Gantt;
            t1.CustomAttributes = "PointWidth=1";
            t1.BorderColor = Color.Transparent;
            t1.Color = Color.Transparent;
            t1.ShowInLegend = false;
            this.GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Series.Add(t1);

            Series t2 = new Series("SyntheticSerie");
            t2.YValuesPerPoint = 2;
            t2.Type = SeriesChartType.Gantt;
            t2.CustomAttributes = "PointWidth=1";
            t2.BorderColor = Color.Transparent;
            t2.Color = Color.OrangeRed;
            t2.BackHatchStyle = ChartHatchStyle.DiagonalBrick;
            t2.ShowInLegend = false;
            this.GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Series.Add(t2);
        }

        private void SetAttributes_MonthlyOperationGanttChartSchema()
        {
            GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Series["ConstractorSerie"]["DrawSideBySide"] = "false";

            foreach (Series series in GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Series)
            {
                series.MapAreaAttributes = "onDblClick =\"javascript:DataPointCollection_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema_onDblClick('#SER','#INDEX');\"";
                //series.MapAreaAttributes += "onMouseOver =\"javascript:DataPointCollection_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema_onMouseOver('#SER','#INDEX');\"";
            }
        }

        protected void CallBack_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.InitializeScale_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema(double.Parse(this.StringBuilder.CreateString(e.Parameters[6])), double.Parse(this.StringBuilder.CreateString(e.Parameters[7])));
            this.GenerateGanttSeries_ChartMonthlyOperation_MonthlyOperationGanttChartSchema();
            this.Fill_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])), this.StringBuilder.CreateString(e.Parameters[4]), this.StringBuilder.CreateString(e.Parameters[5]));
            this.hfTaskFeatures_MonthlyOperationGanttChartSchema.RenderControl(e.Output);
            this.ErrorHiddenField_MonthlyOperationGanttChartSchema.RenderControl(e.Output);
            this.GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.RenderControl(e.Output);
        }

        private void InitializeScale_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema(double width, double height)
        {
            if (width >= (double)this.GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Width.Value)
                this.GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Width = new System.Web.UI.WebControls.Unit(width);
            if (height >= (double)this.GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Height.Value)
                this.GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Height = new System.Web.UI.WebControls.Unit(height);
        }

        private void Clear_DataPointCollection_MonthlyOperationGanttChartSchema()
        {
            foreach (Series ser in this.GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Series)
            {
                ser.Points.Clear();
            }
        }

        private void Fill_GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema(LoadState LS, decimal PersonnelID, int Year, int Month, string FromDate, string ToDate)
        {
            string[] retMessage = new string[4];
            IList<PersonalMonthlyReportRow> PersonnelMonthlyOperationList = null;
            IList<TaskFeatures> TaskFeaturesList = new List<TaskFeatures>();
            Dictionary<string, Series> AvailableGanttSeriesDic = new Dictionary<string, Series>();
            try
            {
                this.Clear_DataPointCollection_MonthlyOperationGanttChartSchema();

                if (Session["Year_MonthlyOperationGanttChartSchema"] != null)
                    if ((int)Session["Year_MonthlyOperationGanttChartSchema"] != Year)
                        Session["MonthlyOperationSource_MonthlyOperationGanttChartSchema"] = null;
                if (Session["Month_MonthlyOperationGanttChartSchema"] != null)
                    if ((int)Session["Month_MonthlyOperationGanttChartSchema"] != Month)
                        Session["MonthlyOperationSource_MonthlyOperationGanttChartSchema"] = null;

                switch (LS)
                {
                    case LoadState.Manager:
                        break;
                    case LoadState.Personnel:
                        PersonnelID = BUser.CurrentUser.Person.ID;
                        break;
                }

                BPersonMonthlyWorkedTime MonthlyOperationBusiness = new BPersonMonthlyWorkedTime(PersonnelID);

                if (Session["MonthlyOperationSource_MonthlyOperationGanttChartSchema"] == null)
                {
                    PersonnelMonthlyOperationList = MonthlyOperationBusiness.GetPersonGanttChart(Year, Month, FromDate, ToDate).Reverse().ToList();
                    Session.Add("MonthlyOperationSource_MonthlyOperationGanttChartSchema", PersonnelMonthlyOperationList);
                    Session.Add("Year_MonthlyOperationGanttChartSchema", Year);
                    Session.Add("Month_MonthlyOperationGanttChartSchema", Month);
                }
                else
                    PersonnelMonthlyOperationList = (IList<PersonalMonthlyReportRow>)(Session["MonthlyOperationSource_MonthlyOperationGanttChartSchema"]);

                this.SetAxisLables_MonthlyOperationGanttChartSchema(MonthlyOperationBusiness, PersonnelMonthlyOperationList.Reverse().ToList());

                IList<GanttChartSettingsProxy> GanttChartSettingsProxyList = this.GetGanttChartSettings_MonthlyOperationGanttChartSchema();
                for (int i = 1; i <= PersonnelMonthlyOperationList.Count; i++)
                {
                    foreach (GanttChartSettingsProxy ganttChartSettingsProxyItem in GanttChartSettingsProxyList)
                    {
                        if (ganttChartSettingsProxyItem.ViewState)
                        {
                            string ConceptType = "Pairly" + ganttChartSettingsProxyItem.ConceptTitle;
                            PersonalMonthlyReportRowDetail personalMonthlyReportRowDetail = ((PersonalMonthlyReportRowDetail)(typeof(PersonalMonthlyReportRow).GetProperty(ConceptType).GetValue(PersonnelMonthlyOperationList[i - 1], null)));
                            foreach (IPair pair in personalMonthlyReportRowDetail.Pairs)
                            {
                                if (pair.From != pair.To)
                                {
                                    string realFromTime = string.Empty;
                                    string realToTime = string.Empty;
                                    double FromTime = this.ConvertScaleByMin_MonthlyOperationGanttChartSchema(pair.From, out realFromTime);
                                    double ToTime = this.ConvertScaleByMin_MonthlyOperationGanttChartSchema(pair.To, out realToTime);
                                    Series serie = GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Series[ganttChartSettingsProxyItem.ConceptTitle];
                                    serie.Points.AddXY(i, FromTime, ToTime);
                                    DataPointCollection dataPointCollection = serie.Points;

                                    this.SetConceptColor_MonthlyOperationGanttChartSchema(personalMonthlyReportRowDetail, serie);

                                    this.SetDataPointFeatures_MonthlyOperationGanttChartSchema(TaskFeaturesList, ConceptType, realFromTime, realToTime, PersonnelMonthlyOperationList[i - 1], personalMonthlyReportRowDetail, serie, dataPointCollection);

                                    if (!AvailableGanttSeriesDic.ContainsKey(ganttChartSettingsProxyItem.ConceptTitle))
                                        AvailableGanttSeriesDic.Add(ganttChartSettingsProxyItem.ConceptTitle, serie);
                                }
                            }
                        }
                    }

                    this.SetDataPointFeatures_MonthlyOperationGanttChartSchema(TaskFeaturesList);

                    this.SetAttributes_MonthlyOperationGanttChartSchema();

                    this.CustomizeGanttLegends_MonthlyOperationGanttChartSchema(AvailableGanttSeriesDic);
                }

                this.operationYearMonthProvider.SetOperationYearMonth(Year, Month);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MonthlyOperationGanttChartSchema.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MonthlyOperationGanttChartSchema.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MonthlyOperationGanttChartSchema.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void SetConceptColor_MonthlyOperationGanttChartSchema(PersonalMonthlyReportRowDetail personalMonthlyReportRowDetail, Series serie)
        {
            serie.Color = (Color)(new ColorConverter()).ConvertFromString(personalMonthlyReportRowDetail.Color);
        }

        private void SetDataPointFeatures_MonthlyOperationGanttChartSchema(IList<TaskFeatures> TaskFeaturesList)
        {
            this.hfTaskFeatures_MonthlyOperationGanttChartSchema.Value = this.JsSerializer.Serialize(TaskFeaturesList);
        }

        private IList<TaskFeatures> SetDataPointFeatures_MonthlyOperationGanttChartSchema(IList<TaskFeatures> TaskFeaturesList, string ConceptType, string FromTime, string ToTime, PersonalMonthlyReportRow personalMonthlyReportRow, PersonalMonthlyReportRowDetail personalMonthlyReportRowDetail, Series serie, DataPointCollection dataPointCollection)
        {
            TaskFeatures[] TaskFeaturesCol = new TaskFeatures[1024];

            int serieIndex = this.GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Series.GetIndex(serie.Name);
            int dataPointIndex = dataPointCollection.Count - 1;

            TaskFeatures taskFeatures = new TaskFeatures();
            taskFeatures = new TaskFeatures();
            taskFeatures.Key = serie.Name + "%" + dataPointIndex.ToString();
            taskFeatures.Name = GetLocalResourceObject(serie.Name) != null ? GetLocalResourceObject(serie.Name).ToString() : serie.Name;
            taskFeatures.Type = ConceptType;
            taskFeatures.FromTime = FromTime;
            taskFeatures.ToTime = ToTime;
            taskFeatures.SerieIndex = serieIndex.ToString();
            taskFeatures.DataPointIndex = dataPointIndex.ToString();
            taskFeatures.DateKey = personalMonthlyReportRow.ID.ToString();
            taskFeatures.DayName = personalMonthlyReportRow.DayName;
            taskFeatures.Date = personalMonthlyReportRow.Date.Year + "/" + personalMonthlyReportRow.Date.Month + "/" + personalMonthlyReportRow.Date.Day;
            taskFeatures.UIDate = personalMonthlyReportRow.TheDate;
            TaskFeaturesList.Add(taskFeatures);

            dataPointCollection[dataPointIndex].ToolTip = taskFeatures.DayName + " " + taskFeatures.UIDate;
            dataPointCollection[dataPointIndex].ToolTip += " \n" + taskFeatures.Name;
            dataPointCollection[dataPointIndex].ToolTip += " \n" + GetLocalResourceObject("From").ToString() + " " + taskFeatures.FromTime;
            dataPointCollection[dataPointIndex].ToolTip += " \n" + GetLocalResourceObject("To").ToString() + " " + taskFeatures.ToTime;
            dataPointCollection[dataPointIndex].ToolTip += " \n" + GetLocalResourceObject("ImpureValue").ToString() + " " + personalMonthlyReportRowDetail.ImpureValue;

            return TaskFeaturesList;
        }

        private void SetAxisLables_MonthlyOperationGanttChartSchema(BPersonMonthlyWorkedTime MonthlyOperationBusiness, IList<PersonalMonthlyReportRow> PersonnelMonthlyOperationList)
        {
            int min = 0;
            int max = 24;
            string minRealTime = string.Empty;
            string maxRealTime = string.Empty;
            MonthlyOperationBusiness.GetMinMaxHourForGanttChart(out min, out max);
            min = (int)Math.Floor((double)this.ConvertScaleByMin_MonthlyOperationGanttChartSchema(min, out minRealTime));
            max = (int)Math.Floor((double)this.ConvertScaleByMin_MonthlyOperationGanttChartSchema(max, out maxRealTime));

            GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.ChartAreas["Default"].AxisY.Minimum = min;
            GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.ChartAreas["Default"].AxisY2.Minimum = min;
            GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.ChartAreas["Default"].AxisY.Maximum = max;
            GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.ChartAreas["Default"].AxisY2.Maximum = max;

            for (int j = min; j <= max; j++)
            {
                GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.ChartAreas["Default"].AxisY.CustomLabels.Add(j - 0.4, j + 0.4, "" + j + ":00");
                GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.ChartAreas["Default"].AxisY2.CustomLabels.Add(j - 0.4, j + 0.4, "" + j + ":00");
            }

            for (int i = 1; i <= PersonnelMonthlyOperationList.Count; i++)
            {
                GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Series["ConstractorSerie"].Points.AddXY(i, 0, 0);
                GanttChartMonthlyOperation_MonthlyOperationGanttChartSchema.Series["ConstractorSerie"].Points[i - 1].AxisLabel = PersonnelMonthlyOperationList[PersonnelMonthlyOperationList.Count - i].DayName + " " + PersonnelMonthlyOperationList[PersonnelMonthlyOperationList.Count - i].TheDate;
            }
        }

        private double ConvertScaleByMin_MonthlyOperationGanttChartSchema(int minute, out string realTime)
        {
            double scale = 0;
            int minRemainder = 0;
            int minResult = 0;
            long scaleRemainder = 0;
            minResult = Math.DivRem(minute, 60, out minRemainder);
            realTime = minResult + ":" + (minRemainder.ToString().Length < 2 ? "0" + minRemainder : minRemainder.ToString());
            scale = minResult + Math.DivRem(minRemainder * 100, 60, out scaleRemainder) * Math.Pow(10, -2);
            return scale;
        }

        protected void CallBack_cmbMonth_MonthlyOperationGanttChartSchema_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbMonth_MonthlyOperationGanttChartSchema.Dispose();
            this.Fill_cmbMonth_MonthlyOperationGanttChartSchema((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[2])));
            this.hfCurrentMonth_MonthlyOperationGanttChartSchema.RenderControl(e.Output);
            this.ErrorHiddenField_Months_MonthlyOperationGanttChartSchema.RenderControl(e.Output);
            this.cmbMonth_MonthlyOperationGanttChartSchema.RenderControl(e.Output);
        }

        private void Fill_cmbMonth_MonthlyOperationGanttChartSchema(LoadState LS, int Year, decimal PersonnelID)
        {
            string[] retMessage = new string[4];
            try
            {
                string CurrentLangID = string.Empty;
                string SysLangID = string.Empty;
                string Identifier = string.Empty;
                int month = 0;
                DateTime currentDateTime = DateTime.Now;
                CurrentLangID = this.LangProv.GetCurrentLanguage();
                SysLangID = this.LangProv.GetCurrentSysLanguage();
                switch (SysLangID)
                {
                    case "en-US":
                        switch (CurrentLangID)
                        {
                            case "en-US":
                                Identifier = "ee";
                                break;
                            case "fa-IR":
                                Identifier = "ef";
                                break;
                        }
                        month = currentDateTime.Month;
                        break;
                    case "fa-IR":
                        switch (CurrentLangID)
                        {
                            case "en-US":
                                Identifier = "fe";
                                break;
                            case "fa-IR":
                                Identifier = "ff";
                                break;
                        }
                        PersianCalendar pCal = new PersianCalendar();
                        month = pCal.GetMonth(currentDateTime);
                        break;
                }
                switch (LS)
                {
                    case LoadState.Manager:
                        break;
                    case LoadState.Personnel:
                        PersonnelID = BUser.CurrentUser.Person.ID;
                        break;
                }
                BPersonMonthlyWorkedTime MonthlyOperationBusiness = new BPersonMonthlyWorkedTime(PersonnelID);
                IList<DateRangeOrderProxy> DateRangeOrderProxyList = MonthlyOperationBusiness.GetDateRangeOrder(Year);
                DateRangeDetails dateRangeDetails = new DateRangeDetails();
                for (int i = 1; i <= DateRangeOrderProxyList.Count; i++)
                {
                    dateRangeDetails.FromDate = DateRangeOrderProxyList[i - 1].FromDate;
                    dateRangeDetails.ToDate = DateRangeOrderProxyList[i - 1].ToDate;
                    dateRangeDetails.Order = DateRangeOrderProxyList[i - 1].Order.ToString();

                    ComboBoxItem cmbItemMonth = new ComboBoxItem(GetLocalResourceObject("Month" + i + "" + Identifier + "").ToString());
                    cmbItemMonth.Value = this.JsSerializer.Serialize(dateRangeDetails);
                    this.cmbMonth_MonthlyOperationGanttChartSchema.Items.Add(cmbItemMonth);
                    if (DateRangeOrderProxyList[i - 1].Selected && Session["CurrentOperationMonth"] == null)
                    {
                        this.cmbMonth_MonthlyOperationGanttChartSchema.SelectedIndex = i - 1;
                        this.hfCurrentMonth_MonthlyOperationGanttChartSchema.Value = cmbItemMonth.Value;
                    }
                }
                if (Session["CurrentOperationMonth"] != null)
                {
                    OperationYearMonthProvider.OperationMonthObj operationMonthObj = (OperationYearMonthProvider.OperationMonthObj)Session["CurrentOperationMonth"];
                    dateRangeDetails.FromDate = DateRangeOrderProxyList[operationMonthObj.Index].FromDate;
                    dateRangeDetails.ToDate = DateRangeOrderProxyList[operationMonthObj.Index].ToDate;
                    dateRangeDetails.Order = DateRangeOrderProxyList[operationMonthObj.Index].Order.ToString();
                    this.cmbMonth_MonthlyOperationGanttChartSchema.Items[operationMonthObj.Index].Value = this.JsSerializer.Serialize(dateRangeDetails);
                    this.hfCurrentMonth_MonthlyOperationGanttChartSchema.Value = this.cmbMonth_MonthlyOperationGanttChartSchema.Items[operationMonthObj.Index].Value;
                    this.cmbMonth_MonthlyOperationGanttChartSchema.SelectedIndex = operationMonthObj.Index;
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Months_MonthlyOperationGanttChartSchema.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Months_MonthlyOperationGanttChartSchema.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Months_MonthlyOperationGanttChartSchema.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_GridSettings_MonthlyOperationGanttChartSchema_onCallBack(object sender, CallBackEventArgs e)
        {
            switch (e.Parameters[0])
            {
                case "Get":
                    this.Fill_GridSettings_MonthlyOperationGanttChartSchema();
                    break;
                case "Set":
                    Update_GridSettings_MonthlyOperationGanttChartSchema(this.StringBuilder.CreateString(e.Parameters[1]), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[2])), (LoadState)Enum.Parse(typeof(LoadState), e.Parameters[3]));
                    break;
            }
            this.ErrorHiddenField_GridSettings_MonthlyOperationGanttChartSchema.RenderControl(e.Output);
            this.hfCurrentSettingID_GridSettings_MonthlyOperationGanttChartSchema.RenderControl(e.Output);
            this.GridSettings_MonthlyOperationGanttChartSchema.RenderControl(e.Output);
        }

        private void Fill_GridSettings_MonthlyOperationGanttChartSchema()
        {
            string[] retMessage = new string[4];

            try
            {
                IList<GanttChartSettingsProxy> GanttChartSettingsProxyList = this.GetGanttChartSettings_MonthlyOperationGanttChartSchema();
                GanttChartClientSettings monthlyOperationGanttChartClientSettings = this.MonthlyOperationGanttChartClientSettingsBusiness.GetGanttChartClientSettings();
                this.hfCurrentSettingID_GridSettings_MonthlyOperationGanttChartSchema.Value = monthlyOperationGanttChartClientSettings.ID.ToString();

                this.GridSettings_MonthlyOperationGanttChartSchema.DataSource = GanttChartSettingsProxyList;
                this.GridSettings_MonthlyOperationGanttChartSchema.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_GridSettings_MonthlyOperationGanttChartSchema.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_GridSettings_MonthlyOperationGanttChartSchema.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_GridSettings_MonthlyOperationGanttChartSchema.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private IList<GanttChartSettingsProxy> GetGanttChartSettings_MonthlyOperationGanttChartSchema()
        {
            if (Session["GanttChartSettingsProxyList_MonthlyOperationGanttChartSchema"] == null)
            {
                IList<GanttChartSettingsProxy> GanttChartSettingsProxyList = new List<GanttChartSettingsProxy>();
                GanttChartClientSettings monthlyOperationGanttChartClientSettings = this.MonthlyOperationGanttChartClientSettingsBusiness.GetGanttChartClientSettings();
                foreach (PropertyInfo pInfo in typeof(GanttChartClientSettings).GetProperties())
                {
                    object pInfoVal = pInfo.GetValue(monthlyOperationGanttChartClientSettings, null);
                    if (pInfoVal is bool)
                    {
                        GanttChartSettingsProxy ganttChartSettingsProxy = new GanttChartSettingsProxy();
                        ganttChartSettingsProxy.ConceptTitle = pInfo.Name;
                        ganttChartSettingsProxy.ConceptCaption = GetLocalResourceObject(pInfo.Name).ToString();
                        ganttChartSettingsProxy.ViewState = bool.Parse(pInfoVal.ToString());
                        GanttChartSettingsProxyList.Add(ganttChartSettingsProxy);
                    }
                }
                Session.Add("GanttChartSettingsProxyList_MonthlyOperationGanttChartSchema", GanttChartSettingsProxyList);
            }
            return (IList<GanttChartSettingsProxy>)Session["GanttChartSettingsProxyList_MonthlyOperationGanttChartSchema"];
        }

        private void Update_GridSettings_MonthlyOperationGanttChartSchema(string StrSettingsTasksList, decimal CurrentSettingID, LoadState LS)
        {
            string[] retMessage = new string[4];

            try
            {
                GanttChartClientSettings monthlyOperationGanttChartClientSettings = new GanttChartClientSettings();
                monthlyOperationGanttChartClientSettings.ID = CurrentSettingID;

                object[] SettingsTasksParamsBatchList = (object[])this.JsSerializer.DeserializeObject(StrSettingsTasksList);
                foreach (object SettingsTasksParamsBatchItem in SettingsTasksParamsBatchList)
                {
                    Dictionary<string, object> SettingsTasksParamsBatchDic = (Dictionary<string, object>)SettingsTasksParamsBatchItem;
                    typeof(GanttChartClientSettings).GetProperty(SettingsTasksParamsBatchDic["ConceptTitle"].ToString()).SetValue(monthlyOperationGanttChartClientSettings, bool.Parse(SettingsTasksParamsBatchDic["ViewState"].ToString()), null);
                }
                switch (LS)
                {
                    case LoadState.Manager:
                        this.MonthlyOperationGanttChartClientSettingsBusiness.SaveChanges_onManagerState(monthlyOperationGanttChartClientSettings, Business.UIActionType.EDIT);
                        break;
                    case LoadState.Personnel:
                        this.MonthlyOperationGanttChartClientSettingsBusiness.SaveChanges_onPersonnelState(monthlyOperationGanttChartClientSettings, Business.UIActionType.EDIT);
                        break;
                }

                Session["GanttChartSettingsProxyList_MonthlyOperationGanttChartSchema"] = null;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_GridSettings_MonthlyOperationGanttChartSchema.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_GridSettings_MonthlyOperationGanttChartSchema.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_GridSettings_MonthlyOperationGanttChartSchema.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void ClearSessions_MonthlyOperationGanttChartSchema()
        {
            Session["Year_MonthlyOperationGanttChartSchema"] = null;
            Session["Month_MonthlyOperationGanttChartSchema"] = null;
            Session["MonthlyOperationSource_MonthlyOperationGanttChartSchema"] = null;
        }

    }

}