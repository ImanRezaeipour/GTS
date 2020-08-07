using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using System.Data;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.Exceptions.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class ShiftsView : GTSBasePage
    {
        public IOverTimeBRequest RequestBusiness
        {
            get
            {
                return (IOverTimeBRequest)(BusinessHelper.GetBusinessInstance<BRequest>());
            }
        }

        enum RequestCaller
        {
            Grid,
            GanttChart
        }

        enum RequestLoadState
        {
            Personnel,
            Manager
        }

        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
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

        enum Scripts
        {
            ShiftsView_onPageLoad,
            DialogShiftsView_Operations,
            Alert_Box
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_GridShiftsView_ShiftsView.IsCallback)
            {
                Page ShiftsViewPage = this.Page;
                Ajax.Utility.GenerateMethodScripts(ShiftsViewPage);
                ClearSessions_ShiftsView();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckShiftsViewLoadState_ShiftsView();
            }
            else
            {
                if (this.GridShiftsView_ShiftsView.CausedCallback)
                {
                    if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestDate"))
                    {
                        DateTime RequestDate = DateTime.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestDate"]));
                        Fill_GridShiftsView_ShiftsView(RequestDate);
                    }
                }
            }
            this.GridShiftsView_ShiftsView.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(GridShiftsView_ShiftsView_NeedRebind);
            this.GridShiftsView_ShiftsView.NeedDataSource += new ComponentArt.Web.UI.Grid.NeedDataSourceEventHandler(GridShiftsView_ShiftsView_NeedDataSource);
            this.GridShiftsView_ShiftsView.NeedChildDataSource += new ComponentArt.Web.UI.Grid.NeedChildDataSourceEventHandler(GridShiftsView_ShiftsView_NeedChildDataSource);
        }

        private void CheckShiftsViewLoadState_ShiftsView()
        {
            string[] retMessage = new string[4];
            try
            {
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RC") && HttpContext.Current.Request.QueryString.AllKeys.Contains("RLS"))
                {
                    RequestLoadState requestLoadState = (RequestLoadState)Enum.Parse(typeof(RequestLoadState), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RLS"]));
                    RequestCaller requestCaller = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RC"]));

                    switch (requestLoadState)
                    {
                        case RequestLoadState.Personnel:
                            switch (requestCaller)
                            {
                                case RequestCaller.Grid:
                                    this.RequestBusiness.CheckShiftsViewLoadAccess_onPersonnelLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckShiftsViewLoadAccess_onPersonnelLoadStateInGanttChartSchema();
                                    break;
                            }
                            break;
                        case RequestLoadState.Manager:
                            switch (requestCaller)
                            {
                                case RequestCaller.Grid:
                                    this.RequestBusiness.CheckShiftsViewLoadAccess_onManagerLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckShiftsViewLoadAccess_onManagerLoadStateInGanttChartSchema();
                                    break;
                            }
                            break;
                    }
                }
            }
            catch (BaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            }
        }


        private void ClearSessions_ShiftsView()
        {
            Session["RequestDate_ShiftsView"] = null;
            Session["ShiftsSource_ShiftsView"] = null;
        }

        private DateTime GetRequesDate_ShiftsView()
        {
            DateTime RequestDate = DateTime.Now;
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestDate"))
                RequestDate = DateTime.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestDate"]));
            return RequestDate;
        }

        private void Fill_GridShiftsView_ShiftsView(DateTime RequestDate)
        {
            string[] retMessage = new string[3];
            try
            {
                if (Session["RequestDate_ShiftsView"] != null)
                    if (!DateTime.Equals((DateTime)Session["RequestDate_ShiftsView"], RequestDate))
                        Session["ShiftsSource_ShiftsView"] = null;
                if (Session["ShiftsSource_ShiftsView"] == null)
                {
                    IList<ShiftProxy> ShiftProxyList = this.RequestBusiness.GetAllShifts(RequestDate);
                    Session.Add("RequestDate_ShiftsView", RequestDate);
                    Session.Add("ShiftsSource_ShiftsView", ShiftProxyList);
                }
                this.GridShiftsView_ShiftsView.DataSource = (IList<ShiftProxy>)Session["ShiftsSource_ShiftsView"];
                this.GridShiftsView_ShiftsView.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErroHiddenField_ShiftsView.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErroHiddenField_ShiftsView.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErroHiddenField_ShiftsView.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        void GridShiftsView_ShiftsView_NeedChildDataSource(object sender, ComponentArt.Web.UI.GridNeedChildDataSourceEventArgs e)
        {
            FillChilds_GridShiftsView_ShiftsView(e);
        }

        void GridShiftsView_ShiftsView_NeedDataSource(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestDate"))
            {
                DateTime RequestDate = DateTime.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestDate"]));
                Fill_GridShiftsView_ShiftsView(RequestDate);
            }
        }

        private void FillChilds_GridShiftsView_ShiftsView(GridNeedChildDataSourceEventArgs e)
        {
            if (e.Item.Level == 0)
            {
                IList<ShiftPairProxy> ShiftPairProxyList = this.RequestBusiness.GetShiftDetail((decimal)e.Item["ShiftID"]);
                e.DataSource = ShiftPairProxyList;
            }             
        }

        void GridShiftsView_ShiftsView_NeedRebind(object sender, EventArgs e)
        {
            this.GridShiftsView_ShiftsView.DataBind();
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

        protected void CallBack_GridShiftsView_ShiftsView_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.Fill_GridShiftsView_ShiftsView(DateTime.Parse(this.StringBuilder.CreateString(e.Parameter)));
            this.GridShiftsView_ShiftsView.RenderControl(e.Output);
            this.ErroHiddenField_ShiftsView.RenderControl(e.Output);
        }


    }
    }