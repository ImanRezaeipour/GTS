using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using GTS.Clock.Presentaion.Forms.App_Code;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.Rules;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using System.Web.Script.Serialization;
using System.Collections;
using GTS.Clock.Business.AppSettings;

public partial class CalculationRange : GTSBasePage
{
    private int OperationYear = 2010;

    public class SecondaryConceptView
    {
        public decimal ID { get; set; }
        public string Name { get; set; }
        public bool IsUsedByDateRange { get; set; }
    }

    public BDateRange CalculationRangeBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BDateRange>();
        }
    }

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

    enum ConceptsDataMode
    {
        Add,
        Edit,
        CheckAll,
        UnCheckAll
    }

    internal class CalculationRangeParts
    {
        public string M { get; set; }
        public string Fm { get; set; }
        public string Fd { get; set; }
        public string Tm { get; set; }
        public string Td { get; set; }
    }

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    enum Scripts
    {
        CalculationRange_onPageLoad,
        DialogCalculationRange_Operations,
        NumericUpDown,
        Alert_Box,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        Page CalculationRangePage = this;
        Ajax.Utility.GenerateMethodScripts(CalculationRangePage);
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
    }

    protected override void InitializeCulture()
    {
        this.SetCurrentCultureResObjs(this.LangProv.GetCurrentLanguage());
        base.InitializeCulture();
    }

    private void SetCurrentCultureResObjs(string LangID)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
    }

    [Ajax.AjaxMethod("GetMonthFeatures_CalculationRangePage", "GetMonthFeatures_CalculationRangePage_onCallBack", null, null)]
    public string[] GetMonthFeatures_CalculationRangePage(string temp)
    {
        this.InitializeCulture();

        string[] retMessage = new string[5];
        try
        {
            string MonthAxises = this.GetAxises_CalculationRange();
            string MonthsDayCol = this.JsSerializer.Serialize(this.GetMonthsDayCount_CalculationRangePage());

            retMessage[0] = this.GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = string.Empty;
            retMessage[2] = "success";
            retMessage[3] = MonthAxises;
            retMessage[4] = MonthsDayCol;

            return retMessage;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            return retMessage;
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            return retMessage;
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            return retMessage;
        }

    }

    public string GetAxises_CalculationRange()
    {
        string CurrentLangID = string.Empty;
        string SysLangID = string.Empty;
        string Identifier = string.Empty;
        string retAxises = string.Empty;
        this.InitializeCulture();
        CurrentLangID = this.LangProv.GetCurrentLanguage();
        SysLangID = LangProv.GetCurrentSysLanguage();
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
                break;
        }

        string Splitter = "@";
        for (int i = 1; i <= 12; i++)
        {
            if (i == 12)
                Splitter = string.Empty;
            retAxises += GetLocalResourceObject("lblMonth" + i + "" + Identifier + "").ToString() + Splitter;
        }
        return retAxises;
    }

    public string[] GetMonthsDayCount_CalculationRangePage()
    {
        InitializeCulture();
        string[] MonthsDayCol = new string[12];
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "en-US":
                for (int i = 0; i < 12; i++)
                {
                    GregorianCalendar gCal = new GregorianCalendar(GregorianCalendarTypes.USEnglish);
                    MonthsDayCol[i] = gCal.GetDaysInMonth(OperationYear, i + 1).ToString();
                }
                break;
            case "fa-IR":
                for (int j = 0; j < 6; j++)
                {
                    MonthsDayCol[j] = "31";
                }
                for (int k = 6; k < 12; k++)
                {
                    MonthsDayCol[k] = "30";
                }
                string l = "29";
                if (new GregorianCalendar(GregorianCalendarTypes.USEnglish).IsLeapYear(OperationYear))
                    l = "30";
                MonthsDayCol[11] = l;
                break;
        }
        return MonthsDayCol;
    }

    protected void CallBack_GridConcepts_CalculationRange_onCallBack(object sender, CallBackEventArgs e)
    {
        Fill_GridConcepts_CalculationRange(e);
        this.GridConcepts_CalculationRange.RenderControl(e.Output);
        this.ErrorHiddenField_CalculationRange.RenderControl(e.Output);
        this.CheckListHiddenField_CalculationRange.RenderControl(e.Output);
    }

    private void Fill_GridConcepts_CalculationRange(CallBackEventArgs e)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<SecondaryConcept> concepts = this.CalculationRangeBusiness.GetAllRanglyConcepts();
            IList<SecondaryConceptView> secondaryConceptViewList = new List<SecondaryConceptView>();
            ConceptsDataMode cdm = ConceptsDataMode.Add;
            cdm = (ConceptsDataMode)Enum.Parse(typeof(ConceptsDataMode), e.Parameters[0]);
            switch (cdm)
            {
                case ConceptsDataMode.CheckAll:
                    secondaryConceptViewList = this.CreateConceptsList_onCheckChange(concepts, true);
                    this.GridConcepts_CalculationRange.DataSource = secondaryConceptViewList;
                    break;
                case ConceptsDataMode.UnCheckAll:
                    secondaryConceptViewList = this.CreateConceptsList_onCheckChange(concepts, false);
                    this.GridConcepts_CalculationRange.DataSource = secondaryConceptViewList;
                    break;
                default:
                    this.GridConcepts_CalculationRange.DataSource = concepts;
                    break;
            }
            this.GridConcepts_CalculationRange.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_CalculationRange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_CalculationRange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_CalculationRange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private IList<SecondaryConceptView> CreateConceptsList_onCheckChange(IList<SecondaryConcept> Concepts, bool Checked)
    {
        //string CheckList = string.Empty;
        //for (int i = 0; i < Concepts.Count; i++)
        //{   
        //    //Concepts[i].IsUsedByDateRange = Checked;
        //    if (Checked)
        //    {
        //        string splitter = "#";
        //        if (i == Concepts.Count - 1)
        //            splitter = string.Empty;
        //        CheckList += Concepts[i].ID + splitter;
        //    }
        //}
        //this.CheckListHiddenField_CalculationRange.Value = CheckList;
        //return Concepts;
        IList<SecondaryConceptView> SecondaryConceptViewList = new List<SecondaryConceptView>();
        string CheckList = string.Empty;
        for (int i = 0; i < Concepts.Count; i++)
        {
            SecondaryConceptView secondaryConceptView = new SecondaryConceptView();
            secondaryConceptView.ID = Concepts[i].ID;
            secondaryConceptView.Name = Concepts[i].Name;
            secondaryConceptView.IsUsedByDateRange = Checked;
            SecondaryConceptViewList.Add(secondaryConceptView);
            if (Checked)
            {
                string splitter = "#";
                if (i == Concepts.Count - 1)
                    splitter = string.Empty;
                CheckList += Concepts[i].ID + splitter;
            }
        }
        this.CheckListHiddenField_CalculationRange.Value = CheckList;
        return SecondaryConceptViewList;

    }

    [Ajax.AjaxMethod("UpdateCaculationRange_CalculationRangePage", "UpdateCaculationRange_CalculationRangePage_onCallBack", null, null)]
    public string[] UpdateCaculationRange_CalculationRangePage(string state, string SelectedCalculationRangeID, string CalculationRangeName, string CalculationRangeDescription, string ConceptsList, string DefaultCalculationRangesList, string CalculationRangesList)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal CalculationRangeID = 0;
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            SelectedCalculationRangeID = this.StringBuilder.CreateString(SelectedCalculationRangeID);
            CalculationRangeName = this.StringBuilder.CreateString(CalculationRangeName);
            CalculationRangeDescription = this.StringBuilder.CreateString(CalculationRangeDescription);
            ConceptsList = this.StringBuilder.CreateString(ConceptsList);
            DefaultCalculationRangesList = this.StringBuilder.CreateString(DefaultCalculationRangesList);
            CalculationRangesList = this.StringBuilder.CreateString(CalculationRangesList);
            decimal selectedCalculationRangeID = decimal.Parse(SelectedCalculationRangeID);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;

            CalculationRangeGroup calculationRange = new CalculationRangeGroup();
            calculationRange.ID = selectedCalculationRangeID;
            calculationRange.Name = CalculationRangeName;
            calculationRange.Description = CalculationRangeDescription;

            List<CalculationDateRange> DefaultCalculationDateRanges = this.CreateCalculationRangesList_CalculationRange(DefaultCalculationRangesList);
            List<CalculationDateRange> CalculationDateRanges = this.CreateCalculationRangesList_CalculationRange(CalculationRangesList);
            List<decimal> Concepts = this.CreateConceptsList_CalculationRange(ConceptsList);

            switch (uam)
            {
                case UIActionType.ADD:
                    CalculationRangeID = this.CalculationRangeBusiness.InsertDateRange(calculationRange, DefaultCalculationDateRanges, CalculationDateRanges, Concepts);
                    SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                    break;
                case UIActionType.EDIT:
                    if (selectedCalculationRangeID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoCalculationRangeSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    CalculationRangeID = this.CalculationRangeBusiness.UpdateDateRange(calculationRange, CalculationDateRanges, Concepts);
                    SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            retMessage[3] = CalculationRangeID.ToString();
            return retMessage;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            return retMessage;
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            return retMessage;
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            return retMessage;
        }

    }

    private List<CalculationDateRange> CreateCalculationRangesList_CalculationRange(string CalculationRangesList)
    {
        List<CalculationDateRange> CalculationDateRanges = new List<CalculationDateRange>();
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        object[] ParamsBatchs = (object[])jsSerializer.DeserializeObject(CalculationRangesList);
        foreach (object paramBatch in ParamsBatchs)
        {
            ((Dictionary<string, object>)paramBatch).Add("IsNavigate",false);
            //Dictionary<string, object> paramDic = this.ConvertCalculationRangesList_CalculationRange((Dictionary<string, object>)paramBatch);
            Dictionary<string, object> paramDic = (Dictionary<string, object>)paramBatch;
            CalculationDateRange calculationDateRange = new CalculationDateRange();
            calculationDateRange.Order = (CalculationDateRangeOrder)Enum.Parse(typeof(CalculationDateRangeOrder), paramDic["M"].ToString());
            calculationDateRange.FromMonth = int.Parse(paramDic["Fm"].ToString());
            calculationDateRange.FromDay = int.Parse(paramDic["Fd"].ToString());
            calculationDateRange.ToMonth = int.Parse(paramDic["Tm"].ToString());
            calculationDateRange.ToDay = int.Parse(paramDic["Td"].ToString());
            CalculationDateRanges.Add(calculationDateRange);
        }
        return CalculationDateRanges;
    }

    private Dictionary<string, object> ConvertCalculationRangesList_CalculationRange(Dictionary<string, object> paramBatch)
    {
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        int operationYear = 0;
        if (!(bool)paramBatch["IsNavigate"])
        {
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    PersianCalendar pCal = new PersianCalendar();
                    operationYear = pCal.GetYear(new DateTime(this.OperationYear, 1, 1));
                    break;
                case "en-US":
                    operationYear = this.OperationYear;
                    break;
            }
        }
        else
            operationYear = this.OperationYear;
        paramDic.Add("M", paramBatch["M"].ToString());
        int Fm = 0;
        int Fd = 0;
        int Tm = 0;
        int Td = 0;
        string strFromDate = operationYear.ToString() + "/" + paramBatch["Fm"].ToString() + "/" + paramBatch["Fd"].ToString();
        string strToDate = operationYear.ToString() + "/" + paramBatch["Tm"].ToString() + "/" + paramBatch["Td"].ToString();
        if (!(bool)paramBatch["IsNavigate"])
        {
            DateTime fromDate = this.LangProv.GetSysDateTime(strFromDate);
            Fm = fromDate.Month;
            Fd = fromDate.Day;
            DateTime toDate = this.LangProv.GetSysDateTime(strToDate);
            Tm = toDate.Month;
            Td = toDate.Day;
        }
        else
        {
            GTS.Clock.Business.AppSettings.DateObj fromDateObj = this.LangProv.GetDBDateTime(strFromDate);
            Fm = fromDateObj.Month;
            Fd = fromDateObj.Day;
            GTS.Clock.Business.AppSettings.DateObj toDateObj = this.LangProv.GetDBDateTime(strToDate);
            Tm = toDateObj.Month;
            Td = toDateObj.Day;
        }
        paramDic.Add("Fm", Fm);
        paramDic.Add("Fd", Fd);
        paramDic.Add("Tm", Tm);
        paramDic.Add("Td", Td);
        return paramDic;
    }

    private List<decimal> CreateConceptsList_CalculationRange(string ConceptsList)
    {
        List<decimal> ConceptsListArray = new List<decimal>();
        string[] ConceptsListParts = ConceptsList.Split(new char[] { '#' });
        foreach (string conceptListPart in ConceptsListParts)
        {
            if (conceptListPart != string.Empty)
                ConceptsListArray.Add(decimal.Parse(conceptListPart));
        }
        return ConceptsListArray;
    }

    [Ajax.AjaxMethod("GetCalculationRangesOnDemand_CalculationRangePage", "GetCalculationRangesOnDemand_CalculationRangePage_onCallBack", null, null)]
    public string GetCalculationRangesOnDemand_CalculationRangePage(string CalculationRangeID, string ConceptID)
    {
        ArrayList arCalculationRange = new ArrayList();
        string strCalculationRangeList = string.Empty;
        CalculationRangeID = this.StringBuilder.CreateString(CalculationRangeID);
        ConceptID = this.StringBuilder.CreateString(ConceptID);
        IList<CalculationDateRange> CalculationRangeList = this.CalculationRangeBusiness.GetAllDateRange(decimal.Parse(ConceptID), decimal.Parse(CalculationRangeID));
        foreach (CalculationDateRange calculationRangeItem in CalculationRangeList)
        {
            Dictionary<string, object> dicCalculationRange = new Dictionary<string, object>();
            dicCalculationRange.Add("IsNavigate", true);
            dicCalculationRange.Add("M", ((int)calculationRangeItem.Order).ToString());
            dicCalculationRange.Add("Fm", calculationRangeItem.FromMonth.ToString());
            dicCalculationRange.Add("Fd", calculationRangeItem.FromDay.ToString());
            dicCalculationRange.Add("Tm", calculationRangeItem.ToMonth.ToString());
            dicCalculationRange.Add("Td", calculationRangeItem.ToDay.ToString());
            //dicCalculationRange = this.ConvertCalculationRangesList_CalculationRange(dicCalculationRange);
            CalculationRangeParts Crp = new CalculationRangeParts();
            Crp.M = dicCalculationRange["M"].ToString();
            Crp.Fm = dicCalculationRange["Fm"].ToString();
            Crp.Fd = dicCalculationRange["Fd"].ToString();
            Crp.Tm = dicCalculationRange["Tm"].ToString();
            Crp.Td = dicCalculationRange["Td"].ToString();
            arCalculationRange.Add(Crp);
        }
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        strCalculationRangeList = jsSerializer.Serialize(arCalculationRange);
        return strCalculationRangeList;
    }







}