using GTS.Clock.Business.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Proxy;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Report;

public partial class SystemReports : GTSBasePage
{
    public BSystemReports SystemReportsBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BSystemReports>();
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

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    enum PageState
    {
        View,
        DeleteAll
    }

    internal class CurrentSystemReportTypeObj
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    enum Scripts
    {
        SystemReports_onPageLoad,
        DialogSystemReports_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridSystemReportType_SystemReports.IsCallback)
        {
            Page SystemReportsPage = this;
            Ajax.Utility.GenerateMethodScripts(SystemReportsPage);

            this.SetSystemReportTypePageSize_SystemReports(SystemReportType.SystemBusinessReport);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckSystemReportsLoadAccess_SystemReports();
            this.ViewCurrentCalendars_SystemReports();
            this.Fill_cmbSystemReportType_SystemReports();
        }
    }

    private void CheckSystemReportsLoadAccess_SystemReports()
    {
        string[] retMessage = new string[4];
        try
        {
            this.SystemReportsBusiness.CheckSystemReportsLoadAccess();
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    private void ViewCurrentCalendars_SystemReports()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpFromDate_SystemReports.Visible = true;
                this.Container_pdpToDate_SystemReports.Visible = true;
                break;
            case "en-US":
                this.Container_gdpFromDate_SystemReports.Visible = true;
                this.Container_gdpFromDate_SystemReports.Visible = true;
                break;
        }
    }


    private void SetSystemReportTypePageSize_SystemReports(SystemReportType SRT)
    {
        int PageSize = 0;
        switch (SRT)
        {
            case SystemReportType.SystemBusinessReport:
                PageSize = this.GridSystemBusinessReport_SystemReports.PageSize;
                break;
            case SystemReportType.SystemEngineReport:
                PageSize = this.GridSystemEngineReport_SystemReports.PageSize;
                break;
            case SystemReportType.SystemWindowsServiceReport:
                PageSize = this.GridSystemWindowsServiceReport_SystemReports.PageSize;
                break;
            case SystemReportType.SystemUserActionReport:
                PageSize = this.GridSystemUserActionReport_SystemReports.PageSize;
                break;
        }
        this.hfSystemReportTypePageSize_SystemReports.Value = PageSize.ToString();
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

    private void Fill_cmbSystemReportType_SystemReports()
    {
        string[] retMessage = new string[4];

        this.InitializeCulture();
        foreach (SystemReportType systemReportTypeItem in Enum.GetValues(typeof(SystemReportType)))
        {
            ComboBoxItem cmbItemReportType = new ComboBoxItem(GetLocalResourceObject(systemReportTypeItem.ToString()).ToString());
            cmbItemReportType.Value = systemReportTypeItem.ToString();
            this.cmbSystemReportType_SystemReports.Items.Add(cmbItemReportType);
        }
        this.cmbSystemReportType_SystemReports.SelectedIndex = 0;
        this.hfCurrentSystemReportType_SystemReports.Value = this.JsSerializer.Serialize(new CurrentSystemReportTypeObj() {Text = this.cmbSystemReportType_SystemReports.SelectedItem.Text, Value = this.cmbSystemReportType_SystemReports.SelectedItem.Value });
    }

    protected void CallBack_GridSystemReportType_SystemReports_onCallBack(object sender, CallBackEventArgs e)
    {
        SystemReportType SRT = (SystemReportType)Enum.Parse(typeof(SystemReportType), this.StringBuilder.CreateString(e.Parameters[0]));
        int PageSize = int.Parse(this.StringBuilder.CreateString(e.Parameters[1]));
        int PageIndex = int.Parse(this.StringBuilder.CreateString(e.Parameters[2]));
        SystemReportTypeFilterConditions SrtFilterConditions = this.JsSerializer.Deserialize<SystemReportTypeFilterConditions>(this.StringBuilder.CreateString(e.Parameters[3]));

        this.SetSystemReportsPageCount_SystemReports(SRT, PageSize, SrtFilterConditions);
        this.Fill_GridSystemReportType_SystemReports(SRT, PageSize, PageIndex, SrtFilterConditions);
        this.hfSystemReportTypePageCount_SystemReports.RenderControl(e.Output);
        this.ErrorHiddenField_GridSystemReportType_SystemReports.RenderControl(e.Output);
        this.RenderGridSystemReportType_SystemReports(SRT, e);
    }

    private void RenderGridSystemReportType_SystemReports(SystemReportType SRT, CallBackEventArgs e)
    {
        switch (SRT)
        {
            case SystemReportType.SystemBusinessReport:
                this.GridSystemBusinessReport_SystemReports.Visible = true;
                this.GridSystemBusinessReport_SystemReports.RenderControl(e.Output);
                break;
            case SystemReportType.SystemEngineReport:
                this.GridSystemEngineReport_SystemReports.Visible = true;
                this.GridSystemEngineReport_SystemReports.RenderControl(e.Output);
                break;
            case SystemReportType.SystemWindowsServiceReport:
                this.GridSystemWindowsServiceReport_SystemReports.Visible = true;
                this.GridSystemWindowsServiceReport_SystemReports.RenderControl(e.Output);
                break;
            case SystemReportType.SystemUserActionReport:
                this.GridSystemUserActionReport_SystemReports.Visible = true;
                this.GridSystemUserActionReport_SystemReports.RenderControl(e.Output);
                break;
        }
    }

    private void SetSystemReportsPageCount_SystemReports(SystemReportType SRT, int PageSize, SystemReportTypeFilterConditions SrtFilterConditions)
    {
        int SystemReportTypeCount = this.SystemReportsBusiness.GetSystemReportTypeCount(SRT, SrtFilterConditions);
        this.hfSystemReportTypePageCount_SystemReports.Value = Utility.GetPageCount(SystemReportTypeCount, PageSize).ToString();
    }

    private void Fill_GridSystemReportType_SystemReports(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            switch (SRT)
            {
                case SystemReportType.SystemBusinessReport:
                    IList<SystemBusinessReport> SystemBusinessReportList = this.SystemReportsBusiness.GetSystemBusinessReportList(SRT, PageSize, PageIndex, SrtFilterConditions);
                    this.GridSystemBusinessReport_SystemReports.DataSource = SystemBusinessReportList;
                    this.GridSystemBusinessReport_SystemReports.DataBind();
                    break;
                case SystemReportType.SystemEngineReport:
                    IList<SystemEngineReport> SystemEngineReportList = this.SystemReportsBusiness.GetSystemEngineReportList(SRT, PageSize, PageIndex, SrtFilterConditions);
                    this.GridSystemEngineReport_SystemReports.DataSource = SystemEngineReportList;
                    this.GridSystemEngineReport_SystemReports.DataBind();
                    break;
                case SystemReportType.SystemWindowsServiceReport:
                    IList<SystemWindowsServiceReport> SystemWindowsServiceReportList = this.SystemReportsBusiness.GetSystemWindowsServiceReportList(SRT, PageSize, PageIndex, SrtFilterConditions);
                    this.GridSystemWindowsServiceReport_SystemReports.DataSource = SystemWindowsServiceReportList;
                    this.GridSystemWindowsServiceReport_SystemReports.DataBind();
                    break;
                case SystemReportType.SystemUserActionReport:
                    IList<SystemUserActionReport> SystemUserActionReportList = this.SystemReportsBusiness.GetSystemUserActionReportList(SRT, PageSize, PageIndex, SrtFilterConditions);
                    this.GridSystemUserActionReport_SystemReports.DataSource = SystemUserActionReportList;
                    this.GridSystemUserActionReport_SystemReports.DataBind();
                    break;
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_GridSystemReportType_SystemReports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_GridSystemReportType_SystemReports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (OutOfExpectedRangeException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            this.ErrorHiddenField_GridSystemReportType_SystemReports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_GridSystemReportType_SystemReports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateSystemReport_SystemReportsPage", "UpdateSystemReport_SystemReportsPage_onCallBack", null, null)]
    public string[] UpdateSystemReport_SystemReportsPage(string state, string systemReportType)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            PageState PS = (PageState)Enum.Parse(typeof(PageState), this.StringBuilder.CreateString(state));
            SystemReportType SRT = (SystemReportType)Enum.Parse(typeof(SystemReportType), this.StringBuilder.CreateString(systemReportType));
            switch (PS)
            {
                case PageState.DeleteAll:
                    switch (SRT)
                    {
                        case SystemReportType.SystemBusinessReport:
                            this.SystemReportsBusiness.DeleteAllSystemBusinessReport();
                            break;
                        case SystemReportType.SystemEngineReport:
                            this.SystemReportsBusiness.DeleteAllSystemEngineReport();
                            break;
                        case SystemReportType.SystemWindowsServiceReport:
                            this.SystemReportsBusiness.DeleteAllSystemWindowsServiceReport();
                            break;
                        case SystemReportType.SystemUserActionReport:
                            this.SystemReportsBusiness.DeleteAllSystemUserActionReport();
                            break;
                    }
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (PS)
            {
                case PageState.DeleteAll:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
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
}