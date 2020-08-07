using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using ComponentArt.Web.UI;
using System.Collections;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;

public partial class CartableFilter : GTSBasePage
{
    public class Filter
    {
        private int id;
        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        private string filterCondition;
        public string FilterCondition
        {
            get
            {
                return this.filterCondition;
            }
            set
            {
                this.filterCondition = value;
            }
        }

        private string conditionOperator;
        public string ConditionOperator
        {
            get
            {
                return this.conditionOperator;
            }
            set
            {
                this.conditionOperator = value;
            }
        }

    }

    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_cmbOperator_CartableFilter.IsCallback && !this.CallBack_GridCombinationalConditions_CartableFilter.IsCallback && !this.Page.IsPostBack)
        {
            Page CartableFilterPage = this;
            Ajax.Utility.GenerateMethodScripts(this.GetType(), ref CartableFilterPage);

            this.ViewCurrentLangCalendars_CartableFilter();
            this.Fill_cmbFilterField_CartableFilter();
            this.Fill_cmbOperator_CartableFilter("Date");
            this.InitializeSkin();
        }
    }

    private void ViewCurrentLangCalendars_CartableFilter()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpDate_CartableFilter.Visible = true;
                break;
            case "en-US":
                this.Container_gdpDate_CartableFilter.Visible = true;
                break;
        }
    }

    private void InitializeSkin()
    {
        SkinHelper.InitializeSkin(this.Page);
        SkinHelper.SetRelativeTabStripImageBaseUrl(this.Page, this.TabStripFilterTerms);
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

    [Ajax.AjaxMethod("GetBoxesHeaders_CartableFilterPage", "GetBoxesHeaders_CartableFilterPage_onCallBack", null, false, null)]
    public string[] GetBoxesHeaders_CartableFilterPage()
    {
        this.InitializeCulture();
        string[] retMessage = new string[5];
        retMessage[0] = GetLocalResourceObject("Title_DialogCartableFilter").ToString();
        retMessage[1] = GetLocalResourceObject("header_CombinationalConditions_CartableFilter").ToString();
        retMessage[2] = GetLocalResourceObject("FilterValueIsNull").ToString();
        retMessage[3] = "And:" + GetLocalResourceObject("And").ToString();
        retMessage[4] = "Or:" + GetLocalResourceObject("Or").ToString();
        return retMessage;
    }

    private void Fill_cmbFilterField_CartableFilter()
    {
        ComboBoxItem cmbItemDate = new ComboBoxItem(GetLocalResourceObject("Date").ToString());
        cmbItemDate.Value = "Date";
        ComboBoxItem cmbItemSelective = new ComboBoxItem(GetLocalResourceObject("Selective").ToString());
        cmbItemSelective.Value = "Selective";
        ComboBoxItem cmbItemString = new ComboBoxItem(GetLocalResourceObject("String").ToString());
        cmbItemString.Value = "String";
        ComboBoxItem cmbItemTime = new ComboBoxItem(GetLocalResourceObject("Time").ToString());
        cmbItemTime.Value = "Time";
        this.cmbFilterField_CartableFilter.Items.Add(cmbItemDate);
        this.cmbFilterField_CartableFilter.Items.Add(cmbItemSelective);
        this.cmbFilterField_CartableFilter.Items.Add(cmbItemString);
        this.cmbFilterField_CartableFilter.Items.Add(cmbItemTime);
        this.cmbFilterField_CartableFilter.SelectedIndex = 0;
    }



    protected void CallBack_cmbOperator_CartableFilter_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        this.cmbOperator_CartableFilter.Dispose();
        this.Fill_cmbOperator_CartableFilter(e.Parameter);
        this.cmbOperator_CartableFilter.RenderControl(e.Output);
    }

    private void Fill_cmbOperator_CartableFilter(string Key)
    {
        this.InitializeCulture();
        Dictionary<string, string> DicOperators = new Dictionary<string, string>();
        switch (Key)
        {
            case "Date":
                DicOperators.Add("Equal", GetLocalResourceObject("Equal").ToString());
                DicOperators.Add("LessThan", GetLocalResourceObject("LessThan").ToString());
                DicOperators.Add("GreatherThan", GetLocalResourceObject("GreatherThan").ToString());
                break;
            case "Selective":
                DicOperators.Add("Equal", GetLocalResourceObject("Equal").ToString());
                break;
            case "Time":
                DicOperators.Add("Equal", GetLocalResourceObject("Equal").ToString());
                DicOperators.Add("LessThan", GetLocalResourceObject("LessThan").ToString());
                DicOperators.Add("GreatherThan", GetLocalResourceObject("GreatherThan").ToString());
                break;
            case "String":
                DicOperators.Add("Equal", GetLocalResourceObject("Equal").ToString());
                DicOperators.Add("StartsWith", GetLocalResourceObject("StartsWith").ToString());
                DicOperators.Add("EndsWith", GetLocalResourceObject("EndsWith").ToString());
                DicOperators.Add("Contains", GetLocalResourceObject("Contains").ToString());
                break;
        }
        foreach (string key in DicOperators.Keys)
        {
            ComboBoxItem cmbItem = new ComboBoxItem(DicOperators[key]);
            cmbItem.Value = key;
            this.cmbOperator_CartableFilter.Items.Add(cmbItem);
        }
        this.cmbOperator_CartableFilter.SelectedIndex = 0;
    }

    [Ajax.AjaxMethod("GetCurrentDateTime_CartableFilterPage", "GetCurrentDateTime_CartableFilterPage_onCallBack", null, false, null)]
    public string GetCurrentDateTime_CartableFilterPage()
    {
        string CurrentDateTime = string.Empty;
        string CurrentCulture = this.LangProv.GetCurrentLanguage();
        switch (CurrentCulture)
        {
            case "fa-IR":
                PersianCalendar pCal = new PersianCalendar();
                CurrentDateTime = pCal.GetYear(DateTime.Now).ToString() + "/" + pCal.GetMonth(DateTime.Now).ToString() + "/" + pCal.GetDayOfMonth(DateTime.Now).ToString();
                break;
            case "en-US":
                CurrentDateTime = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString();
                break;
        }
        return CurrentDateTime;
    }

    protected void CallBack_GridCombinationalConditions_CartableFilter_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        this.InitializeCulture();
        ArrayList arFilters = this.InsertFilterConditions_CartableFilter(e.Parameter);
        this.GridCombinationalConditions_CartableFilter.DataSource = arFilters;
        this.GridCombinationalConditions_CartableFilter.DataBind();
        this.GridCombinationalConditions_CartableFilter.RenderControl(e.Output);
    }

    private ArrayList InsertFilterConditions_CartableFilter(string FilterConditions)
    {
        ArrayList arFilters = new ArrayList();
        if (FilterConditions != string.Empty)
        {
            string[] Conditions = FilterConditions.Split(new char[] { '%' });
            if (Conditions.Length > 0)
            {
                for (int i = 0; i < Conditions.Length; i++)
                {
                    string[] ConditionProps = Conditions[i].Split(new char[] { '@' });
                    Filter filter = new Filter()
                    {
                        ID = int.Parse(ConditionProps[0]),
                        FilterCondition = GetLocalResourceObject(ConditionProps[1]).ToString() + " " + GetLocalResourceObject(ConditionProps[2]).ToString() + " " + ConditionProps[3],
                        ConditionOperator = GetLocalResourceObject(ConditionProps[4]).ToString()
                    };
                    arFilters.Add(filter);
                }
            }
        }
        return arFilters;
    }



}