﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Threading;
using System.Globalization;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System.IO;
using System.Collections.Specialized;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Model.Charts;
using GTS.Clock.RuleDesigner.UI.Web.Classes;

namespace GTS.Clock.RuleDesigner.UI.Web
{
    public partial class OrganizationPosts : GTSBasePage
    {
        #region MyRegion
        public BOrganizationUnit OrganizationPostBusiness
        {
            get
            {
                return new BOrganizationUnit();
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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!this.CallBack_trvPosts_Post.IsCallback)
            {
                //Page OrganizationPostsPage = this;
                //Ajax.Utility.GenerateMethodScripts(this.GetType(), ref OrganizationPostsPage);
                Ajax.Utility.GenerateMethodScripts(this);
                this.OrgPostsLoadonDemandExceptionsHandler(HttpContext.Current.Request.QueryString);
                SkinHelper.InitializeSkin(this.Page);
            }
        }

        /// <summary>
        /// ذخیره ساز رشته حاوی اطلاعات خطای لود سفارشی پست های سازمانی
        /// </summary>
        /// <param name="QueryString">رشته خطا</param>
        private void OrgPostsLoadonDemandExceptionsHandler(NameValueCollection QueryString)
        {
            if (HttpContext.Current.Request.QueryString.Count > 0)
            {
                if (HttpContext.Current.Request.QueryString["OrgPostsErrorSender"] != null)
                {
                    string senderPage = "XmlOrganizationPostsLoadonDemand.aspx";
                    if (HttpContext.Current.Request.QueryString["OrgPostsErrorSender"].ToLower() == senderPage.ToLower())
                    {
                        string[] RetMessage = new string[3];
                        RetMessage[0] = HttpContext.Current.Request.QueryString["ErrorType"];
                        RetMessage[1] = HttpContext.Current.Request.QueryString["ErrorBody"];
                        RetMessage[2] = HttpContext.Current.Request.QueryString["error"];
                        Session.Add("LoadonDemandError_PostsPage", this.exceptionHandler.CreateErrorMessage(RetMessage));
                    }
                }
            }
        }

        /// <summary>
        ///   بازگشت رشته حاوی اطلاعات خطای لود سفارشی پست های سازمانی
        /// </summary>
        /// <returns></returns>
        [Ajax.AjaxMethod("GetLoadonDemandError_PostsPage", "GetLoadonDemandError_PostsPage_onCallBack", null, null)]
        public string GetLoadonDemandError_PostsPage()
        {
            this.InitializeCulture();
            string retError = string.Empty;
            if (Session["LoadonDemandError_PostsPage"] != null)
            {
                retError = Session["LoadonDemandError_PostsPage"].ToString();
                Session["LoadonDemandError_PostsPage"] = null;
            }
            else
            {
                string[] retMessage = new string[3];
                retMessage[0] = GetLocalResourceObject("RetErrorType").ToString();
                retMessage[1] = GetLocalResourceObject("ParentNodeFillProblem").ToString();
                retMessage[2] = "error";
                retError = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            return retError;
        }

        protected override void InitializeCulture()
        {
            this.SetCurrentCultureResObjs(this.LangProv.GetCurrentLanguage());
            base.InitializeCulture();
        }

        /// <summary>
        /// تنظیم زبان انتخابی کاربر 
        /// </summary>
        /// <param name="LangID"></param>
        private void SetCurrentCultureResObjs(string LangID)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }

        /// <summary>
        /// CallBack درخت پست های سازمانی
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CallBack_trvPosts_Post_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_trvPosts_Posts();
            this.trvPosts_Post.RenderControl(e.Output);
            this.ErrorHiddenField_Posts.RenderControl(e.Output);
        }

        /// <summary>
        /// درج و ویرایش و حذف پست سازمانی
        /// </summary>
        /// <param name="state">عملیات جاری</param>
        /// <param name="SelectedOrganizationPostID">در وضعیت درج شناسه بخش والد و در وضعیت ویرایش شناسه بخش انتخاب شده می باشد</param>
        /// <param name="OrganizationPostCode">کد پست سازمانی</param>
        /// <param name="OrganizationPostName">نام پست سازمانی</param>
        /// <returns>آرایه ای از پیغام و شناسه</returns>
        [Ajax.AjaxMethod("UpdatePost_PostsPage", "UpdatePost_PostsPage_onCallBack", null, null)]
        public string[] UpdatePost_PostsPage(string state, string SelectedOrganizationPostID, string OrganizationPostCode, string OrganizationPostName)
        {

            this.InitializeCulture();

            string[] retMessage = new string[4];

            decimal OrganizationPostID = 0;
            decimal selectedOrganizationPostID = decimal.Parse(this.StringBuilder.CreateString(SelectedOrganizationPostID));
            OrganizationPostCode = this.StringBuilder.CreateString(OrganizationPostCode);
            OrganizationPostName = this.StringBuilder.CreateString(OrganizationPostName);
            UIActionType uam = UIActionType.ADD;
            OrganizationUnit organizationPost = new OrganizationUnit();
            try
            {
                switch (this.StringBuilder.CreateString(state))
                {
                    case "Add":
                        uam = UIActionType.ADD;
                        organizationPost.ParentID = selectedOrganizationPostID;
                        organizationPost.ID = 0;
                        break;
                    case "Edit":
                        uam = UIActionType.EDIT;
                        if (selectedOrganizationPostID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoOrganizationPostSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        else
                            organizationPost.ID = selectedOrganizationPostID;
                        break;
                    case "Delete":
                        uam = UIActionType.DELETE;
                        if (selectedOrganizationPostID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoOrganizationPostSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        else
                            organizationPost.ID = selectedOrganizationPostID;
                        break;
                    default:
                        break;
                }

                if (uam != UIActionType.DELETE)
                {
                    organizationPost.CustomCode = OrganizationPostCode;
                    organizationPost.Name = OrganizationPostName;
                }
                OrganizationPostID = this.OrganizationPostBusiness.SaveChanges(organizationPost, uam);

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uam)
                {
                    case UIActionType.ADD:
                        SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                        break;
                    case UIActionType.EDIT:
                        SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                        break;
                    case UIActionType.DELETE:
                        SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                        break;
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = OrganizationPostID.ToString();
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

        /// <summary>
        ///پر کردن عمق 0 و1 درخت پست های سازمانی
        /// </summary>
        private void Fill_trvPosts_Posts()
        {
            string imageUrl = "Images\\TreeView\\folder.gif";
            string imagePath = "Images/TreeView/folder.gif";
            string[] retMessage = new string[4];
            this.InitializeCulture();
            try
            {
                OrganizationUnit rootOrgPost = this.OrganizationPostBusiness.GetOrganizationUnitTree();
                TreeViewNode rootOrgPostNode = new TreeViewNode();
                rootOrgPostNode.ID = rootOrgPost.ID.ToString();
                string rootOrgPostNodeText = string.Empty;
                if (GetLocalResourceObject("OrgNode_trvPosts_Post") != null)
                    rootOrgPostNodeText = GetLocalResourceObject("OrgNode_trvPosts_Post").ToString();
                else
                    rootOrgPostNodeText = rootOrgPost.Name;
                rootOrgPostNode.Text = rootOrgPostNodeText;
                rootOrgPostNode.Value = rootOrgPost.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                    rootOrgPostNode.ImageUrl = imagePath;
                this.trvPosts_Post.Nodes.Add(rootOrgPostNode);
                IList<OrganizationUnit> OrganizationUnitChlidList = this.OrganizationPostBusiness.GetChilds(rootOrgPost.ID);
                foreach (OrganizationUnit childOrgPost in OrganizationUnitChlidList)
                {
                    TreeViewNode childOrgPostNode = new TreeViewNode();
                    childOrgPostNode.ID = childOrgPost.ID.ToString();
                    childOrgPostNode.Text = childOrgPost.Name;
                    childOrgPostNode.Value = childOrgPost.CustomCode;
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                        childOrgPostNode.ImageUrl = imagePath;
                    childOrgPostNode.ContentCallbackUrl = "XmlOrganizationPostsLoadonDemand.aspx?ParentOrgPostID=" + childOrgPost.ID + "&LangID=" + this.LangProv.GetCurrentLanguage();
                    if (this.OrganizationPostBusiness.GetChilds(childOrgPost.ID).Count > 0)
                        childOrgPostNode.Nodes.Add(new TreeViewNode());
                    rootOrgPostNode.Nodes.Add(childOrgPostNode);
                }
                if (OrganizationUnitChlidList.Count > 0)
                    rootOrgPostNode.Expanded = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Posts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Posts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Posts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

    }
}