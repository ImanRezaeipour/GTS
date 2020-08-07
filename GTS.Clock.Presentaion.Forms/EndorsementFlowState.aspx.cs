using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.AppSettings;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure;


public partial class EndorsementFlowState : GTSBasePage
{
    public IKartablRequests KartableBusiness
    {
        get
        {
            return (IKartablRequests)(new BKartabl());

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

    enum Scripts
    {
        EndorsementFlowState_onPageLoad,
        DialogEndorsementFlowState_Operations,
        Alert_Box
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridEndorsementFlowState_EndorsementFlowState.IsCallback && !CallBack_txtEndorsementFlowState_EndorsementFlowState.IsCallback)
        {
            Page EndorsementFlowState = this;
            Ajax.Utility.GenerateMethodScripts(EndorsementFlowState);

            this.SetRequestStatesStr_EndorsementFlowState();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void SetRequestStatesStr_EndorsementFlowState()
    {
        string strRequestStates = string.Empty;
        foreach (RequestState requestStateItem in Enum.GetValues(typeof(RequestState)))
        {
            strRequestStates += "#" + GetLocalResourceObject(requestStateItem.ToString()).ToString() + ":" + ((int)requestStateItem).ToString();
        }
        this.hfRequestStates_EndorsementFlowState.Value = strRequestStates;
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

    protected void CallBack_GridEndorsementFlowState_EndorsementFlowState_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_GridEndorsementFlowState_EndorsementFlowState(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1])));
        this.ErrorHiddenField_InFlow_EndorsementFlowState.RenderControl(e.Output);
        this.GridEndorsementFlowState_EndorsementFlowState.RenderControl(e.Output);
    }

    private void Fill_GridEndorsementFlowState_EndorsementFlowState(decimal ManagerFlowID, decimal RequestID)
    { 
        string[] retMessage = new string[4];
        try
        {
            IList<KartablFlowLevelProxy> KartablFlowLevelProxyList = this.KartableBusiness.GetRequestLevelsByManagerFlowID(RequestID, ManagerFlowID);
            this.GridEndorsementFlowState_EndorsementFlowState.DataSource = KartablFlowLevelProxyList;
            this.GridEndorsementFlowState_EndorsementFlowState.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_InFlow_EndorsementFlowState.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_InFlow_EndorsementFlowState.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_InFlow_EndorsementFlowState.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_txtEndorsementFlowState_EndorsementFlowState_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_txtEndorsementFlowState_EndorsementFlowState(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1])));
        this.ErrorHiddenField_PendingFlow_EndorsementFlowState.RenderControl(e.Output);
        this.txtEndorsementFlowState_EndorsementFlowState.RenderControl(e.Output);
    }

    private void Fill_txtEndorsementFlowState_EndorsementFlowState(decimal PersonnelID, decimal RequestID)
    {
        string[] retMessage = new string[4];
        string Separator = "Or";
        string Result = string.Empty;
        try
        {
            IList<KartablFlowLevelProxy> KartablFlowLevelProxyList = this.KartableBusiness.GetRequestLevelsByPersonnelID(RequestID, PersonnelID);
            for (int i = 0; i < KartablFlowLevelProxyList.Count; i++)
            {
                Separator = i == KartablFlowLevelProxyList.Count - 1 ? string.Empty : GetLocalResourceObject(Separator) != null ? GetLocalResourceObject(Separator).ToString() : Separator;
                Result += KartablFlowLevelProxyList[i].ManagerName + " " + Separator + " ";
            }
            this.txtEndorsementFlowState_EndorsementFlowState.Text = Result;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_PendingFlow_EndorsementFlowState.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_PendingFlow_EndorsementFlowState.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_PendingFlow_EndorsementFlowState.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }

    }

}