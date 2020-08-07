using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Configuration;
using GTS.Clock.Presentaion.Forms.App_Code;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class PasswordChange : GTSBasePage
    {
        public BUser UserBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BUser>();
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
            PasswordChange_onPageLoad,
            tbPasswordChange_TabStripMenus_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            Page PasswordChangePage = this;
            Ajax.Utility.GenerateMethodScripts(PasswordChangePage);

            this.CheckPasswordChangeLoadAccess_PasswordChange();
            this.GetCurrentUser_PasswordChange();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }

        private void CheckPasswordChangeLoadAccess_PasswordChange()
        {
            string[] retMessage = new string[4];
            try
            {
                this.UserBusiness.CheckPasswordChangeLoadAccess();
            }
            catch (BaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            }
        }

        private void GetCurrentUser_PasswordChange()
        {
            this.hfCurrentUser_PasswordChange.Value = this.UserBusiness.GetCurrentUserName();
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

        [Ajax.AjaxMethod("UpdatePassword_PasswordChangePage", "UpdatePassword_PasswordChange_onCallBack", null, null)]
        public string[] UpdatePassword_PasswordChangePage(string CurrentPassword, string NewPassword, string NewPasswordRepeat)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];
            try
            {
                CurrentPassword = this.StringBuilder.CreateString(CurrentPassword);
                NewPassword = this.StringBuilder.CreateString(NewPassword);
                NewPasswordRepeat = this.StringBuilder.CreateString(NewPasswordRepeat);

                bool Result = this.UserBusiness.ChangePassword(CurrentPassword, NewPassword, NewPasswordRepeat);

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                retMessage[1] = GetLocalResourceObject("ChangeComplete").ToString();
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
}