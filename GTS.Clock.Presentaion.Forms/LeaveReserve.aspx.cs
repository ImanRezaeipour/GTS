using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.Leave;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions;

public partial class LeaveReserve : GTSBasePage
{
    public BLeaveIncDec LeaveReserveBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BLeaveIncDec>();
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

    enum Scripts
    {
        LeaveReserve_onPageLoad,
        DialogLeaveReserve_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_cmbOperationType_LeaveReserve.IsCallback && !CallBack_GridLeaveReserve_LeaveReserve.IsCallback)
        {
            Page LeaveReservePage = this;
            Ajax.Utility.GenerateMethodScripts(LeaveReservePage);

            this.CheckLeaveReserveLoadAccess_LeaveReserve();
            this.ViewCurrentLangCalendars_LeaveReserve();
            this.SetCurrentDate_LeaveReserve();
            this.SetOperationTypesStr_LeaveReserve();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void CheckLeaveReserveLoadAccess_LeaveReserve()
    {
        string[] retMessage = new string[4];
        try
        {
            this.LeaveReserveBusiness.CheckLeaveReserveLoadAccess();
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        } 

    }

    private void ViewCurrentLangCalendars_LeaveReserve()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpDate_LeaveReserve.Visible = true;
                break;
            case "en-US":
                this.Container_gdpDate_LeaveReserve.Visible = true;
                break;
        }
    }

    private void SetOperationTypesStr_LeaveReserve()
    {
        string strOperationTypes = string.Empty;
        foreach (LeaveIncDecAction OperationTypeItem in Enum.GetValues(typeof(LeaveIncDecAction)))
        {
            strOperationTypes += "#" + GetLocalResourceObject(OperationTypeItem.ToString()).ToString() + ":" + ((int)OperationTypeItem).ToString();
        }
        this.hfOperationTypes_LeaveReserve.Value = strOperationTypes;
    }


    private void SetCurrentDate_LeaveReserve()
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
        this.hfCurrentDate_LeaveReserve.Value = strCurrentDate;
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

    protected void CallBack_cmbOperationType_LeaveReserve_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbOperationType_LeaveReserve.Dispose();
        this.Fill_cmbOperationType_LeaveReserve();
        this.ErrorHiddenField_OperationTypes.RenderControl(e.Output);
        this.cmbOperationType_LeaveReserve.RenderControl(e.Output);
    }

    private void Fill_cmbOperationType_LeaveReserve()
    {
        string[] retMessage = new string[4];

        this.InitializeCulture();
        try
        {
            foreach (LeaveIncDecAction operationTypeItem in Enum.GetValues(typeof(LeaveIncDecAction)))
            {
                ComboBoxItem cmbItemShiftType = new ComboBoxItem(GetLocalResourceObject(operationTypeItem.ToString()).ToString());
                cmbItemShiftType.Value = operationTypeItem.ToString();
                cmbItemShiftType.Id = ((int)operationTypeItem).ToString();
                this.cmbOperationType_LeaveReserve.Items.Add(cmbItemShiftType);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_OperationTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_OperationTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_OperationTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_GridLeaveReserve_LeaveReserve_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_GridLeaveReserve_LeaveReserve(int.Parse(this.StringBuilder.CreateString(e.Parameter)));
        this.ErrorHiddenField_LeaveReserve.RenderControl(e.Output);
        this.GridLeaveReserve_LeaveReserve.RenderControl(e.Output);
    }

    private void Fill_GridLeaveReserve_LeaveReserve(decimal PersonnelID)
    {
        string[] retMessage = new string[4];

        this.InitializeCulture();
        try
        {
            IList<LeaveIncDecProxy> LeaveReserveList = this.LeaveReserveBusiness.GetAllByPersonId(PersonnelID);
            this.GridLeaveReserve_LeaveReserve.DataSource = LeaveReserveList;
            this.GridLeaveReserve_LeaveReserve.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_LeaveReserve.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_LeaveReserve.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_LeaveReserve.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateLeaveReserve_LeaveReservePage", "UpdateLeaveReserve_LeaveReservePage_onCallBack", null, null)]
    public string[] UpdateLeaveReserve_LeaveReservePage(string state, string LeaveRemainID, string PersonnelID, string SelectedLeaveReserveID, string LeaveReserveOperationType, string LeaveReserveDay, string LeaveReserveHour, string LeaveReserveDate, string LeaveReserveDescription)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal LeaveReserveID = 0;
            decimal leaveRemainID = decimal.Parse(this.StringBuilder.CreateString(LeaveRemainID));
            decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID));
            decimal selectedLeaveReserveID = decimal.Parse(this.StringBuilder.CreateString(SelectedLeaveReserveID));
            LeaveIncDecAction leaveReserveOperationType = (LeaveIncDecAction)Enum.Parse(typeof(LeaveIncDecAction), this.StringBuilder.CreateString(LeaveReserveOperationType));
            LeaveReserveDay = this.StringBuilder.CreateString(LeaveReserveDay);
            LeaveReserveHour = this.StringBuilder.CreateString(LeaveReserveHour);
            LeaveReserveDate = this.StringBuilder.CreateString(LeaveReserveDate);
            LeaveReserveDescription = this.StringBuilder.CreateString(LeaveReserveDescription);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            switch (uam)
            {
                case UIActionType.ADD:
                    LeaveReserveID = this.LeaveReserveBusiness.InsertLeaveIncDec(personnelID, LeaveReserveDay, LeaveReserveHour, leaveReserveOperationType, LeaveReserveDescription, LeaveReserveDate);
                    break;
                case UIActionType.DELETE:
                    if (selectedLeaveReserveID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoLeaveReserveSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    this.LeaveReserveBusiness.DeleteLeaveIncDec(selectedLeaveReserveID);
                    LeaveReserveID = selectedLeaveReserveID;
                    break;
                default:
                    break;
            }

            string SuccessMessageBody = string.Empty;
            switch (uam)
            {
                case UIActionType.ADD:
                    SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                    break;
                case UIActionType.DELETE:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
                default:
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            retMessage[3] = LeaveReserveID.ToString();
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