using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GTS.Clock.Business.AppSettings;
using ComponentArt.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.UI;

/// <summary>
/// Summary description for OperationYearProvider
/// </summary>
public class OperationYearMonthProvider
{
    const int YearLag = 4;
    const int ForwardYearLag = 1;

    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
        }
    }

    public class OperationYearObj
    {
        public int Year { get; set; }
        public int Index { get; set; }
    }

    public OperationYearObj operationYearObj;

    public class OperationMonthObj
    {
        public int Month { get; set; }
        public int Index { get; set; }
    }

    public OperationMonthObj operationMonthObj;

    public OperationYearMonthProvider()
    {
        this.operationYearObj = new OperationYearObj();
        this.operationMonthObj = new OperationMonthObj();
    }

    private int GetCurrentYear()
    {
        int CurrentYear = 0;
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "en-US":
                CurrentYear = DateTime.Now.Year;
                break;
            case "fa-IR":
                PersianCalendar pCal = new PersianCalendar();
                CurrentYear = pCal.GetYear(DateTime.Now);
                break;
        }
        return CurrentYear;
    }

	public int GetOperationYear(ComboBox cmbYear, HiddenField hfYear)
	{
        int CurrentYear = this.GetCurrentYear();

        for (int i = (CurrentYear + ForwardYearLag - YearLag); i <= (CurrentYear + ForwardYearLag); i++)
        {
            ComboBoxItem cmbItemYear = new ComboBoxItem(i.ToString());
            cmbItemYear.Value = i.ToString();
            cmbYear.Items.Add(cmbItemYear);
        }

        if (HttpContext.Current.Session["CurrentOperationYear"] == null)
        {
            this.operationYearObj.Year = CurrentYear;
            this.operationYearObj.Index = cmbYear.Items.Count - 2;

            HttpContext.Current.Session.Add("CurrentOperationYear", operationYearObj);
        }
        this.operationYearObj = (OperationYearObj)HttpContext.Current.Session["CurrentOperationYear"];

        hfYear.Value = this.operationYearObj.Year.ToString();
        cmbYear.SelectedIndex = this.operationYearObj.Index;

        return CurrentYear;
	}

    public int GetOperationMonth(Page page, ComboBox cmbMonth, HiddenField hfMonth)
    {
        string CurrentLangID = string.Empty;
        string SysLangID = string.Empty;
        string Identifier = string.Empty;
        int currentMonth = 0;
        string pagePath = "~/" + HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1];
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
                currentMonth = currentDateTime.Month;
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
                currentMonth = pCal.GetMonth(currentDateTime);
                break;
        }

        for (int i = 1; i <= 12; i++)
        {
            ComboBoxItem cmbItemMonth = new ComboBoxItem(HttpContext.GetLocalResourceObject(pagePath, "Month" + i + "" + Identifier + "").ToString());
            cmbItemMonth.Value = i.ToString();
            cmbMonth.Items.Add(cmbItemMonth);
        }

        if (HttpContext.Current.Session["CurrentOperationMonth"] == null)
        {
            this.operationMonthObj.Month = currentMonth;
            this.operationMonthObj.Index = currentMonth - 1;

            HttpContext.Current.Session.Add("CurrentOperationMonth", this.operationMonthObj);
        }
        this.operationMonthObj = (OperationMonthObj)HttpContext.Current.Session["CurrentOperationMonth"];

        hfMonth.Value = this.operationMonthObj.Month.ToString();
        cmbMonth.SelectedIndex = this.operationMonthObj.Index;

        return currentMonth;
    }

    public void SetOperationYearMonth(int Year, int Month)
    {
        if(HttpContext.Current.Session["CurrentOperationYear"] != null)
           HttpContext.Current.Session["CurrentOperationYear"] = null;
        if (HttpContext.Current.Session["CurrentOperationMonth"] != null)
            HttpContext.Current.Session["CurrentOperationMonth"] = null;

        this.operationYearObj.Year = Year;
        this.operationYearObj.Index = Year + (YearLag - ForwardYearLag) - this.GetCurrentYear();      
        HttpContext.Current.Session.Add("CurrentOperationYear", this.operationYearObj);

        this.operationMonthObj.Month = Month;
        this.operationMonthObj.Index = Month - 1;
        HttpContext.Current.Session.Add("CurrentOperationMonth", this.operationMonthObj);
    }

    public void ResetOperationYearMonth()
    {
        if (HttpContext.Current.Session["CurrentOperationYear"] != null)
            HttpContext.Current.Session["CurrentOperationYear"] = null;
        if (HttpContext.Current.Session["CurrentOperationMonth"] != null)
            HttpContext.Current.Session["CurrentOperationMonth"] = null;
    }


}