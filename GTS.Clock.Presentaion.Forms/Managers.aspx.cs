using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.RequestFlow;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class Managers : GTSBasePage
    {
        public enum LoadState
        {
            Normal,
            Filter,
            Search
        }


        public BManager ManagerBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BManager>();
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

        public LoadState Ls { get; set; }
        public int PageIndex { get; set; }
        public decimal AccessGroupID { get; set; }
        public ManagerSearchFields SearchField { get; set; }
        public string SearchTerm { get; set; }

        enum Scripts
        {
            iFrameManagers_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            Page ManagersPage = this;
            Ajax.Utility.GenerateMethodScripts(ManagersPage);

            if (CallBack_GridManagers_Managers.IsCallback)
            {
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("LoadState"))
                    this.Ls = (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["LoadState"]));
                else
                    this.Ls = LoadState.Normal;
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("PageIndex"))
                    this.PageIndex = int.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["PageIndex"]));
                else
                    this.PageIndex = 0;
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("FilterBy"))
                    this.AccessGroupID = decimal.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["FilterBy"]));
                else
                    this.AccessGroupID = 0;
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("SearchField"))
                {
                    string strSearchField = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["SearchField"]);
                    if (strSearchField != string.Empty)
                        this.SearchField = (ManagerSearchFields)Enum.Parse(typeof(ManagerSearchFields), strSearchField);
                    else
                        this.SearchField = ManagerSearchFields.PersonName;
                }
                else
                    this.SearchField = ManagerSearchFields.PersonName;
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("SearchTerm"))
                    this.SearchTerm = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["SearchTerm"]);
                else
                    this.SearchTerm = string.Empty;

                this.SetManagersPageSize_Managers();
                this.SetManagersPageCount_Managers(Ls, AccessGroupID, SearchField, SearchTerm);
                this.Fill_GridManagers_Managers(Ls, PageIndex, AccessGroupID, SearchField, SearchTerm);
                this.GridManagers_Managers.DataBind();

                this.GridManagers_Managers.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(GridManagers_Managers_NeedRebind);
                this.GridManagers_Managers.NeedDataSource += new ComponentArt.Web.UI.Grid.NeedDataSourceEventHandler(GridManagers_Managers_NeedDataSource);
                this.GridManagers_Managers.NeedChildDataSource += new ComponentArt.Web.UI.Grid.NeedChildDataSourceEventHandler(GridManagers_Managers_NeedChildDataSource);
            }

            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }

        private void SetManagersPageSize_Managers()
        {
            this.hfManagesPageSize_Managers.Value = this.GridManagers_Managers.PageSize.ToString();
        }

        private void SetManagersPageCount_Managers(LoadState Ls, decimal AccessGroupID, ManagerSearchFields SearchField, string SearchTerm)
        {
            string[] retMessage = new string[4];
            try
            {
                int ManagersCount = 0;
                switch (Ls)
                {
                    case LoadState.Normal:
                        ManagersCount = this.ManagerBusiness.GetRecordCount();
                        break;
                    case LoadState.Filter:
                        ManagersCount = this.ManagerBusiness.GetRecordCountByAccessGroupFilter(AccessGroupID);
                        break;
                    case LoadState.Search:
                        ManagersCount = this.ManagerBusiness.GetRecordCountBySearch(SearchTerm, SearchField);
                        break;
                }
                this.hfManagersPageCount_Managers.Value = Utility.GetPageCount(ManagersCount, this.GridManagers_Managers.PageSize).ToString();

            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Managers_Managers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Managers_Managers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Managers_Managers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
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


        protected void CallBack_GridManagers_Managers_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.GridManagers_Managers.RenderControl(e.Output);
            this.hfManagersPageCount_Managers.RenderControl(e.Output);
            this.ErrorHiddenField_Managers_Managers.RenderControl(e.Output);
        }

        private void Fill_GridManagers_Managers(LoadState Ls, int PageIndex, decimal AccessGroupID, ManagerSearchFields SearchField, string SearchTerm)
        {
            string[] retMessage = new string[4];
            int PageSize = this.GridManagers_Managers.PageSize;
            IList<Manager> ManagersList = null;
            try
            {
                this.InitializeCulture();

                switch (Ls)
                {
                    case LoadState.Normal:
                        ManagersList = this.ManagerBusiness.GetAllByPage(PageIndex, PageSize);
                        break;
                    case LoadState.Filter:
                        ManagersList = this.ManagerBusiness.SearchByAccessGroup(AccessGroupID, PageIndex, PageSize);
                        break;
                    case LoadState.Search:
                        ManagersList = this.ManagerBusiness.SearchByPage(SearchTerm, SearchField, PageIndex, PageSize);
                        break;
                    default:
                        break;
                }
                this.GridManagers_Managers.DataSource = ManagersList;
                this.GridManagers_Managers.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Managers_Managers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Managers_Managers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Managers_Managers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }


        void GridManagers_Managers_NeedChildDataSource(object sender, ComponentArt.Web.UI.GridNeedChildDataSourceEventArgs e)
        {
            string[] retMessage = new string[4];
            try
            {
                if (e.Item.Level == 0)
                {
                    IList<Flow> ManagerDetailsList = this.ManagerBusiness.GetManagerDetail(decimal.Parse(e.Item["ID"].ToString()));
                    e.DataSource = ManagerDetailsList;
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Managers_Managers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Managers_Managers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Managers_Managers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        void GridManagers_Managers_NeedDataSource(object sender, EventArgs e)
        {
            this.Fill_GridManagers_Managers(this.Ls, this.PageIndex, this.AccessGroupID, this.SearchField, this.SearchTerm);
        }

        void GridManagers_Managers_NeedRebind(object sender, EventArgs e)
        {
            this.GridManagers_Managers.DataBind();
        }


    }
}