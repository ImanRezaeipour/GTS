using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Presentaion_Helper.Proxy;

public partial class SinglePageDataAccessLevels : GTSBasePage
{
    public BDataAccess SinglePageDataAccessLevelesBusiness
    {
        get
        {
            return new BDataAccess();
        }
    }

    public BUser UserBusiness
    {
        get
        {
            return new BUser();
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

    enum DataAccessLevelOperationState
    {
        Before,
        After
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
        SinglePageDataAccessLevels_onPageLoad,
        SinglePageDataAccessLevels_Operations,
        Alert_Box,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CallBack_GridDataAccessLevelsSource_SinglePageDataAccessLevels.IsCallback && !CallBack_GridDataAccessLevelsTarget_SinglePageDataAccessLevels.IsCallback)
        {
            Page SinglePageDataAccessLevelsPage = this;
            Ajax.Utility.GenerateMethodScripts(SinglePageDataAccessLevelsPage);
            this.CheckPageCaller_SinglePageDataAccessLevels();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    protected override void InitializeCulture()
    {
        this.SetCurrentCultureResObjs(LangProv.GetCurrentLanguage());
        base.InitializeCulture();
    }

    private void SetCurrentCultureResObjs(string LangID)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
    }

    private void CheckPageCaller_SinglePageDataAccessLevels()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("PageCaller"))
        {
            DataAccessParts DAP = (DataAccessParts)Enum.Parse(typeof(DataAccessParts), HttpContext.Current.Request.QueryString["PageCaller"]);
            if (DAP == DataAccessParts.Corporation)
                this.Container_chbSelectAll_SinglePageDataAccessLevels.Visible = false;
        }
    }

    protected void CallBack_GridDataAccessLevelsSource_SinglePageDataAccessLevels_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_GridDataAccessLevelsSource_SinglePageDataAccessLevels((DataAccessParts)Enum.Parse(typeof(DataAccessParts), this.StringBuilder.CreateString(e.Parameter)));
        this.ErrorHiddenField_DataAccessLevelsSource.RenderControl(e.Output);
        this.GridDataAccessLevelsSource_SinglePageDataAccessLevels.RenderControl(e.Output);
    }

    private void Fill_GridDataAccessLevelsSource_SinglePageDataAccessLevels(DataAccessParts DataAccessLevelKey)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<DataAccessProxy> SinglePageDataAccessLevelesSourceList = this.SinglePageDataAccessLevelesBusiness.GetAll(DataAccessLevelKey);
            this.GridDataAccessLevelsSource_SinglePageDataAccessLevels.DataSource = SinglePageDataAccessLevelesSourceList;
            this.GridDataAccessLevelsSource_SinglePageDataAccessLevels.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_DataAccessLevelsSource.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_DataAccessLevelsSource.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_DataAccessLevelsSource.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_GridDataAccessLevelsTarget_SinglePageDataAccessLevels_onCallBack(object sender, CallBackEventArgs e)
    {
        string SearchKey = this.StringBuilder.CreateString(e.Parameters[4]);
        this.Fill_GridDataAccessLevelsTarget_SinglePageDataAccessLevels(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0])), (DataAccessParts)Enum.Parse(typeof(DataAccessParts), this.StringBuilder.CreateString(e.Parameters[1])), (DataAccessLevelOperationType)Enum.Parse(typeof(DataAccessLevelOperationType), this.StringBuilder.CreateString(e.Parameters[2])), (DataAccessLevelOperationState)Enum.Parse(typeof(DataAccessLevelOperationState), this.StringBuilder.CreateString(e.Parameters[3])), SearchKey != "Null" ? (UserSearchKeys)Enum.Parse(typeof(UserSearchKeys), SearchKey) : (UserSearchKeys?)null, this.StringBuilder.CreateString(e.Parameters[5]));
        this.ErrorHiddenField_DataAccessLevelsTarget.RenderControl(e.Output);
        this.GridDataAccessLevelsTarget_SinglePageDataAccessLevels.RenderControl(e.Output);
    }

    private void Fill_GridDataAccessLevelsTarget_SinglePageDataAccessLevels(decimal UserID, DataAccessParts DataAccessLevelKey, DataAccessLevelOperationType Dalot, DataAccessLevelOperationState Dalos, UserSearchKeys? SearchKey, string SearchTerm)
    {
        string[] retMessage = new string[4];
        try
        {
            switch (Dalot)
            {
                case DataAccessLevelOperationType.Single:
                    this.Fill_GridDataAccessLevelsTarget_SinglePageDataAccessLevels(UserID, DataAccessLevelKey, Dalot, SearchKey, SearchTerm);
                    break;
                case DataAccessLevelOperationType.Group:
                    switch (Dalos)
                    {
                        case DataAccessLevelOperationState.Before:
                            break;
                        case DataAccessLevelOperationState.After:
                            this.Fill_GridDataAccessLevelsTarget_SinglePageDataAccessLevels(UserID, DataAccessLevelKey, Dalot, SearchKey, SearchTerm);
                            break;
                    }
                    break;
                default:
                    this.Fill_GridDataAccessLevelsTarget_SinglePageDataAccessLevels(UserID, DataAccessLevelKey, Dalot, SearchKey, SearchTerm);
                    break;
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_DataAccessLevelsTarget.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_DataAccessLevelsTarget.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_DataAccessLevelsTarget.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void Fill_GridDataAccessLevelsTarget_SinglePageDataAccessLevels(decimal UserID, DataAccessParts DataAccessLevelKey, DataAccessLevelOperationType Dalot, UserSearchKeys? SearchKey, string SearchTerm)
    {
        UserID = this.GetUserID_SinglePageDataAccessLevels(Dalot, UserID, SearchKey, SearchTerm);
        IList<DataAccessProxy> SinglePageDataAccessLevelesTargetList = this.SinglePageDataAccessLevelesBusiness.GetAllByUserId(DataAccessLevelKey, UserID);
        if (SinglePageDataAccessLevelesTargetList != null && SinglePageDataAccessLevelesTargetList.Count == 1 && SinglePageDataAccessLevelesTargetList[0].All)
            SinglePageDataAccessLevelesTargetList[0].Name = GetLocalResourceObject("All").ToString() + " " + GetLocalResourceObject(DataAccessLevelKey.ToString()).ToString();
        this.GridDataAccessLevelsTarget_SinglePageDataAccessLevels.DataSource = SinglePageDataAccessLevelesTargetList;
        this.GridDataAccessLevelsTarget_SinglePageDataAccessLevels.DataBind();
    }

    private decimal GetUserID_SinglePageDataAccessLevels(DataAccessLevelOperationType? Dalot, decimal UserID, UserSearchKeys? SearchKey, string SearchTerm)
    {
        switch (Dalot)
        {
            case DataAccessLevelOperationType.Single:
                break;
            case DataAccessLevelOperationType.Group:
                IList<decimal> UserIDList = this.UserBusiness.GetAllUserIDList(BUser.CurrentUser.ID, SearchKey, SearchTerm, true);
                if (UserIDList.Count() > 0)
                    UserID = UserIDList.First();
                break;
        }
        return UserID;
    }

    [Ajax.AjaxMethod("UpdateDataAccessLevels_SinglePageDataAccessLevelsPage", "UpdateDataAccessLevels_SinglePageDataAccessLevelsPage_onCallBack", null, null)]
    public string[] UpdateDataAccessLevels_SinglePageDataAccessLevelsPage(string state, string DataAccessLevelKey, string AccessLevelOperationType, string UserID, string SearchKey, string SearchTerm, string DataAccessLevelSourceID, string DataAccessLevelTargetID)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        state = this.StringBuilder.CreateString(state);
        DataAccessLevelOperationType dataAccessLevelOperationType = (DataAccessLevelOperationType)Enum.Parse(typeof(DataAccessLevelOperationType), this.StringBuilder.CreateString(AccessLevelOperationType));
        decimal userID = decimal.Parse(this.StringBuilder.CreateString(UserID));
        SearchKey = this.StringBuilder.CreateString(SearchKey);
        UserSearchKeys? searchKey = SearchKey != "Null" ? (UserSearchKeys)Enum.Parse(typeof(UserSearchKeys), SearchKey) : (UserSearchKeys?)null;
        SearchTerm = this.StringBuilder.CreateString(SearchTerm);
        DataAccessParts dataAccessLevelKey = (DataAccessParts)Enum.Parse(typeof(DataAccessParts), this.StringBuilder.CreateString(DataAccessLevelKey));
        decimal dataAccessLevelSourceID = decimal.Parse(this.StringBuilder.CreateString(DataAccessLevelSourceID));
        decimal dataAccessLevelTargetID = decimal.Parse(this.StringBuilder.CreateString(DataAccessLevelTargetID));
        UIActionType uam = UIActionType.ADD;
        try
        {
            switch (state)
            {
                case "Add":
                    uam = UIActionType.ADD;
                    if (dataAccessLevelSourceID == -1)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoDataAccessLevelSourceSelectedforInsert").ToString()), retMessage);
                        return retMessage;
                    }
                    this.SinglePageDataAccessLevelesBusiness.InsertDataAccess(dataAccessLevelOperationType, dataAccessLevelKey, dataAccessLevelSourceID, userID, searchKey, SearchTerm);
                    break;
                case "Delete":
                    uam = UIActionType.DELETE;
                    if (dataAccessLevelTargetID == -1)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoDataAccessLevelTargetSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    this.SinglePageDataAccessLevelesBusiness.DeleteAccess(dataAccessLevelKey, dataAccessLevelTargetID, userID);
                    break;
                default:
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {
                case UIActionType.ADD:
                    SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                    break;
                case UIActionType.DELETE:
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