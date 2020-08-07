using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.Presentaion_Helper.Proxy;

public partial class TrafficsControl : GTSBasePage
{
    public BTraffic TrafficsControlBusiness
    {
        get
        {
            return new BTraffic();
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

    public OperationYearMonthProvider operationYearMonthProvider
    {
        get
        {
            return new OperationYearMonthProvider();
        }
    }

    enum Scripts
    {
        iFrameTrafficControls_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridTraffics_TrafficsControl.IsCallback)
        {
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
        else
            if (this.GridTraffics_TrafficsControl.CausedCallback)
            {
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("PersonnelID"))
                    this.Fill_GridTraffics_TrafficsControl(decimal.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["PersonnelID"])), int.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Year"])), int.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Month"])));
            }

        this.GridTraffics_TrafficsControl.NeedRebind += new Grid.NeedRebindEventHandler(GridTraffics_TrafficsControl_NeedRebind);
        this.GridTraffics_TrafficsControl.NeedDataSource += new Grid.NeedDataSourceEventHandler(GridTraffics_TrafficsControl_NeedDataSource);
        this.GridTraffics_TrafficsControl.NeedChildDataSource += new Grid.NeedChildDataSourceEventHandler(GridTraffics_TrafficsControl_NeedChildDataSource);
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

    void GridTraffics_TrafficsControl_NeedChildDataSource(object sender, GridNeedChildDataSourceEventArgs e)
    {
        this.FillChilds_GridTraffics_TrafficsControl(e);
    }

    void GridTraffics_TrafficsControl_NeedDataSource(object sender, EventArgs e)
    {
        decimal PersonnelID = 0;
        int Year = 0;
        int Month = 0;
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("PersonnelID"))
            PersonnelID = decimal.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["PersonnelID"]));
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Year"))
            Year = int.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Year"]));
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Month"))
            Month = int.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Month"]));
        if(PersonnelID != 0 && Year != 0 && Month != 0)
           this.Fill_GridTraffics_TrafficsControl(PersonnelID, Year, Month);
    }

    void GridTraffics_TrafficsControl_NeedRebind(object sender, EventArgs e)
    {
        this.GridTraffics_TrafficsControl.DataBind();
    }

    protected void CallBack_GridTraffics_TrafficsControl_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_GridTraffics_TrafficsControl(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])));
        this.ErrorHiddenField_Traffics_TrafficsControl.RenderControl(e.Output);
        this.GridTraffics_TrafficsControl.RenderControl(e.Output);
    }

    private void Fill_GridTraffics_TrafficsControl(decimal PersonnelID, int Year, int Month)
    {
        string[] retMessage = new string[4];
        IList<DayDateProxy> DayDateProxyList = null;
        try
        {
            if (PersonnelID != 0)
            {
                DayDateProxyList = this.TrafficsControlBusiness.GetDayList(PersonnelID, Year, Month);
                this.operationYearMonthProvider.SetOperationYearMonth(Year, Month);
                this.GridTraffics_TrafficsControl.DataSource = DayDateProxyList;
                this.GridTraffics_TrafficsControl.DataBind();
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Traffics_TrafficsControl.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Traffics_TrafficsControl.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Traffics_TrafficsControl.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void FillChilds_GridTraffics_TrafficsControl(GridNeedChildDataSourceEventArgs e)
    {
        if (e.Item.Level == 0)
        {
            decimal PersonnelID = 0;
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("PersonnelID"))
                PersonnelID = decimal.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["PersonnelID"]));

            IList<BasicTrafficProxy> BasicTrafficProxyList = PersonnelID != 0 ? this.TrafficsControlBusiness.GetDayTraffics(PersonnelID, e.Item["Date"].ToString()) : new List<BasicTrafficProxy>();
            e.DataSource = BasicTrafficProxyList;
        }
    }

}