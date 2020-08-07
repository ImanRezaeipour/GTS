using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;

public partial class PersonnelMasterMonthlyOperation : GTSBasePage
{

    public BWorkedTime MonthlyOperationBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BWorkedTime>();
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
        PersonnelMasterMonthlyOperation_onPageLoad,
        tbPersonnelMasterMonthlyOperation_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));

        this.CheckPersonnelMasterMonthlyOperationLoadAccess_PersonnelMasterMonthlyOperation();
    }

    private void CheckPersonnelMasterMonthlyOperationLoadAccess_PersonnelMasterMonthlyOperation()
    {
        string[] retMessage = new string[4];
        try
        {
            this.MonthlyOperationBusiness.CheckPersonnelMasterMonthlyOperationLoadAccess();
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
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

}