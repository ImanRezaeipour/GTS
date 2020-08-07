using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business;

public partial class MultiPageDataAccessLevels : GTSBasePage
{
    public BDataAccess MultiPageDataAccessLevelesBusiness
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

    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }

    enum DataAccessLevelOperationState
    {
        Before,
        After
    }

    public enum LoadState
    {
        Normal,
        Search,
    }

    enum Scripts
    {
        MultiPageDataAccessLevels_onPageLoad,
        MultiPageDataAccessLevels_Operations,
        Alert_Box,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CallBack_GridDataAccessLevelsSource_MultiPageDataAccessLevels.IsCallback && !CallBack_GridDataAccessLevelsTarget_MultiPageDataAccessLevels.IsCallback)
        {
            Page MultiPageDataAccessLevelsPage = this;
            Ajax.Utility.GenerateMethodScripts(MultiPageDataAccessLevelsPage);
            decimal UserID = 0;
            DataAccessParts DataAccessLevelKey = DataAccessParts.Manager;
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("UID"))
                UserID = decimal.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["UID"]));
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Dalk"))
                DataAccessLevelKey = (DataAccessParts)Enum.Parse(typeof(DataAccessParts), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Dalk"]));
            this.SetDataAccessLevelsSourcePageSize_MultiPageDataAccessLevels();
            this.SetDataAccessLevelsSourcePageCount_MultiPageDataAccessLevels(DataAccessLevelKey, LoadState.Normal, this.GridDataAccessLevelsSource_MultiPageDataAccessLevels.PageSize, string.Empty);
            this.SetDataAccessLevelsTargetPageSize_MultiPageDataAccessLevels();
            this.SetDataAccessLevelsTargetPageCount_MultiPageDataAccessLevels(UserID, DataAccessLevelKey, LoadState.Normal, this.GridDataAccessLevelsTarget_MultiPageDataAccessLevels.PageSize, string.Empty);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void SetDataAccessLevelsSourcePageSize_MultiPageDataAccessLevels()
    {
        this.hfDataAccessLevelsSourcePageSize_MultiPageDataAccessLevels.Value = this.GridDataAccessLevelsSource_MultiPageDataAccessLevels.PageSize.ToString();
    }

    private void SetDataAccessLevelsSourcePageCount_MultiPageDataAccessLevels(DataAccessParts DataAccessLevelKey, LoadState Ls, int pageSize, string SearchTerm)
    {
        string[] retMessage = new string[4];
        int DataAccessLevelsSourceCount = 0;
        try
        {
            switch (DataAccessLevelKey)
            {
                case DataAccessParts.Manager:
                    switch (Ls)
                    {
                        case LoadState.Normal:
                            SearchTerm = string.Empty;
                            break;
                        case LoadState.Search:
                            break;
                    }
                    DataAccessLevelsSourceCount = this.MultiPageDataAccessLevelesBusiness.GetManagerCount(SearchTerm);
                    break;
            }
            this.hfDataAccessLevelsSourceCount_MultiPageDataAccessLevels.Value = DataAccessLevelsSourceCount.ToString();
            this.hfDataAccessLevelsSourcePageCount_MultiPageDataAccessLevels.Value = Utility.GetPageCount(DataAccessLevelsSourceCount, pageSize).ToString();
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

    private void SetDataAccessLevelsTargetPageSize_MultiPageDataAccessLevels()
    {
        this.hfDataAccessLevelsTargetPageSize_MultiPageDataAccessLevels.Value = this.GridDataAccessLevelsTarget_MultiPageDataAccessLevels.PageSize.ToString();
    }

    private void SetDataAccessLevelsTargetPageCount_MultiPageDataAccessLevels(decimal UserID, DataAccessParts DataAccessLevelKey, LoadState Ls, int pageSize, string SearchTerm)
    {
        string[] retMessage = new string[4];
        int DataAccessLevelsTargetCount = 0;
        try
        {
            switch (DataAccessLevelKey)
            {
                case DataAccessParts.Manager:
                    switch (Ls)
                    {
                        case LoadState.Normal:
                            SearchTerm = string.Empty;
                            break;
                        case LoadState.Search:
                            break;
                    }
                    DataAccessLevelsTargetCount = this.MultiPageDataAccessLevelesBusiness.GetManagerOfUserCount(UserID, SearchTerm);
                    break;
            }
            this.hfDataAccessLevelsTargetCount_MultiPageDataAccessLevels.Value = DataAccessLevelsTargetCount.ToString();
            this.hfDataAccessLevelsTargetPageCount_MultiPageDataAccessLevels.Value = Utility.GetPageCount(DataAccessLevelsTargetCount, pageSize).ToString();
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

    protected void CallBack_GridDataAccessLevelsSource_MultiPageDataAccessLevels_onCallBack(object sender, CallBackEventArgs e)
    {
        this.SetDataAccessLevelsSourcePageCount_MultiPageDataAccessLevels((DataAccessParts)Enum.Parse(typeof(DataAccessParts), this.StringBuilder.CreateString(e.Parameters[0])), (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[4]));
        this.Fill_GridDataAccessLevelsSource_MultiPageDataAccessLevels((DataAccessParts)Enum.Parse(typeof(DataAccessParts), this.StringBuilder.CreateString(e.Parameters[0])), (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])), this.StringBuilder.CreateString(e.Parameters[4]));
        this.ErrorHiddenField_DataAccessLevelsSource.RenderControl(e.Output);
        this.hfDataAccessLevelsSourceCount_MultiPageDataAccessLevels.RenderControl(e.Output);
        this.hfDataAccessLevelsSourcePageCount_MultiPageDataAccessLevels.RenderControl(e.Output);
        this.GridDataAccessLevelsSource_MultiPageDataAccessLevels.RenderControl(e.Output);
    }

    private void Fill_GridDataAccessLevelsSource_MultiPageDataAccessLevels(DataAccessParts DataAccessLevelKey, LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<DataAccessProxy> MultiPageDataAccessLevelesSourceList = null;
            switch (DataAccessLevelKey)
            {
                case DataAccessParts.Manager:
                    switch (Ls)
                    {
                        case LoadState.Normal:
                            SearchTerm = string.Empty;
                            break;
                        case LoadState.Search:
                            break;
                    }
                    MultiPageDataAccessLevelesSourceList = this.MultiPageDataAccessLevelesBusiness.GetAllManagers(SearchTerm, pageIndex, pageSize);
                    break;
            }
            this.GridDataAccessLevelsSource_MultiPageDataAccessLevels.DataSource = MultiPageDataAccessLevelesSourceList;
            this.GridDataAccessLevelsSource_MultiPageDataAccessLevels.DataBind();
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

    protected void CallBack_GridDataAccessLevelsTarget_MultiPageDataAccessLevels_onCallBack(object sender, CallBackEventArgs e)
    {
        string UserSearchKey = this.StringBuilder.CreateString(e.Parameters[8]);
        this.SetDataAccessLevelsTargetPageCount_MultiPageDataAccessLevels(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0])), (DataAccessParts)Enum.Parse(typeof(DataAccessParts), this.StringBuilder.CreateString(e.Parameters[1])), (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[3])), int.Parse(this.StringBuilder.CreateString(e.Parameters[4])), this.StringBuilder.CreateString(e.Parameters[6]));
        this.Fill_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0])), (DataAccessParts)Enum.Parse(typeof(DataAccessParts), this.StringBuilder.CreateString(e.Parameters[1])), (DataAccessLevelOperationType)Enum.Parse(typeof(DataAccessLevelOperationType), this.StringBuilder.CreateString(e.Parameters[2])), (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[3])), int.Parse(this.StringBuilder.CreateString(e.Parameters[4])), int.Parse(this.StringBuilder.CreateString(e.Parameters[5])), this.StringBuilder.CreateString(e.Parameters[6]), (DataAccessLevelOperationState)Enum.Parse(typeof(DataAccessLevelOperationState), this.StringBuilder.CreateString(e.Parameters[7])), UserSearchKey != "Null" ? (UserSearchKeys)Enum.Parse(typeof(UserSearchKeys), UserSearchKey) : (UserSearchKeys?)null, this.StringBuilder.CreateString(e.Parameters[9]));
        this.ErrorHiddenField_DataAccessLevelsTarget.RenderControl(e.Output);
        this.hfDataAccessLevelsTargetCount_MultiPageDataAccessLevels.RenderControl(e.Output);
        this.hfDataAccessLevelsTargetPageCount_MultiPageDataAccessLevels.RenderControl(e.Output);
        this.GridDataAccessLevelsTarget_MultiPageDataAccessLevels.RenderControl(e.Output);
    }

    private void Fill_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(decimal UserID, DataAccessParts DataAccessLevelKey, DataAccessLevelOperationType Dalot, LoadState Ls, int pageSize, int pageIndex, string SearchTerm, DataAccessLevelOperationState Dalos, UserSearchKeys? UserSearchKey, string UserSearchTerm)
    {
        string[] retMessage = new string[4];
        try
        {
            switch (Dalot)
            {
                case DataAccessLevelOperationType.Single:
                    this.Fill_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(UserID, DataAccessLevelKey, Dalot, Ls, pageSize, pageIndex, SearchTerm, UserSearchKey, UserSearchTerm);
                    break;
                case DataAccessLevelOperationType.Group:
                    switch (Dalos)
                    {
                        case DataAccessLevelOperationState.Before:
                            break;
                        case DataAccessLevelOperationState.After:
                            this.Fill_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(UserID, DataAccessLevelKey, Dalot, Ls, pageSize, pageIndex, SearchTerm, UserSearchKey, UserSearchTerm);
                            break;
                    }
                    break;
                default:
                    this.Fill_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(UserID, DataAccessLevelKey, Dalot, Ls, pageSize, pageIndex, SearchTerm, UserSearchKey, UserSearchTerm);
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

    private void Fill_GridDataAccessLevelsTarget_MultiPageDataAccessLevels(decimal UserID, DataAccessParts DataAccessLevelKey, DataAccessLevelOperationType Dalot, LoadState Ls, int pageSize, int pageIndex, string SearchTerm, UserSearchKeys? UserSearchKey, string UserSearchTerm)
    {
        IList<DataAccessProxy> MultiPageDataAccessLevelesTargetList = null;
        UserID = this.GetUserID_MultiPageDataAccessLevels(Dalot, UserID, UserSearchKey, UserSearchTerm);
        switch (DataAccessLevelKey)
        {
            case DataAccessParts.Manager:
                switch (Ls)
                {
                    case LoadState.Normal:
                        SearchTerm = string.Empty;
                        break;
                    case LoadState.Search:
                        break;
                }
                MultiPageDataAccessLevelesTargetList = this.MultiPageDataAccessLevelesBusiness.GetAllManagerOfUser(UserID, SearchTerm, pageIndex, pageSize);
                break;
        }
        if (MultiPageDataAccessLevelesTargetList != null && MultiPageDataAccessLevelesTargetList.Count == 1 && MultiPageDataAccessLevelesTargetList[0].All)
            MultiPageDataAccessLevelesTargetList[0].Name = GetLocalResourceObject("All").ToString() + " " + GetLocalResourceObject(DataAccessLevelKey.ToString()).ToString();
        this.GridDataAccessLevelsTarget_MultiPageDataAccessLevels.DataSource = MultiPageDataAccessLevelesTargetList;
        this.GridDataAccessLevelsTarget_MultiPageDataAccessLevels.DataBind();
    }

    private decimal GetUserID_MultiPageDataAccessLevels(DataAccessLevelOperationType? Dalot, decimal UserID, UserSearchKeys? UserSearchKey, string UserSearchTerm)
    {
        switch (Dalot)
        {
            case DataAccessLevelOperationType.Single:
                break;
            case DataAccessLevelOperationType.Group:
                IList<decimal> UserIDList = this.UserBusiness.GetAllUserIDList(BUser.CurrentUser.ID, UserSearchKey, UserSearchTerm, true);
                if (UserIDList.Count() > 0)
                    UserID = UserIDList.First();
                break;
        }
        return UserID;
    }

    [Ajax.AjaxMethod("UpdateDataAccessLevels_MultiPageDataAccessLevelsPage", "UpdateDataAccessLevels_MultiPageDataAccessLevelsPage_onCallBack", null, null)]
    public string[] UpdateDataAccessLevels_MultiPageDataAccessLevelsPage(string state, string DataAccessLevelKey, string AccessLevelOperationType, string UserID, string SearchKey, string SearchTerm, string DataAccessLevelSourceID, string DataAccessLevelTargetID)
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
                    this.MultiPageDataAccessLevelesBusiness.InsertDataAccess(dataAccessLevelOperationType, dataAccessLevelKey, dataAccessLevelSourceID, userID, searchKey, SearchTerm);
                    break;
                case "Delete":
                    uam = UIActionType.DELETE;
                    if (dataAccessLevelTargetID == -1)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoDataAccessLevelTargetSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    this.MultiPageDataAccessLevelesBusiness.DeleteAccess(dataAccessLevelKey, dataAccessLevelTargetID, userID);
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