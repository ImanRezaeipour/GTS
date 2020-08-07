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
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class EmployTypes : GTSBasePage   
    {
        public BEmployment EmployTypeBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BEmployment>();
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
            EmployTypes_onPageLoad,
            tbEmployTypesIntroduction_TabStripMenus_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!this.CallBack_GridEmployTypes_EmployTypes.CausedCallback)
            {
                 Page EmployTypes = this;
                 Ajax.Utility.GenerateMethodScripts(EmployTypes);
                 SkinHelper.InitializeSkin(this.Page);
                 ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                 this.CheckEmployTypesLoadAccess_EmployTypes();
            }
        }

        private void CheckEmployTypesLoadAccess_EmployTypes()
        {
            string[] retMessage = new string[4];
            try
            {
                this.EmployTypeBusiness.CheckEmployTypesLoadAccess();
            }
            catch (UIBaseException ex)
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
        /// CallBack گرید نوع استخدام
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CallBack_GridEmployTypes_EmployTypes_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_GridEmployTypes_EmployTypes();
            this.GridEmployTypes_EmployTypes.RenderControl(e.Output);
            this.ErrorHiddenField_EmployTypes.RenderControl(e.Output);
        }

        /// <summary>
        /// پر کردن گرید نوع استخدام
        /// </summary>
        private void Fill_GridEmployTypes_EmployTypes()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<EmploymentType> employTypeList = this.EmployTypeBusiness.GetAll();
                this.GridEmployTypes_EmployTypes.DataSource = employTypeList;
                this.GridEmployTypes_EmployTypes.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_EmployTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_EmployTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_EmployTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        /// <summary>
        /// درج و ویرایش وحذف نوع استخدام
        /// </summary>
        /// <param name="state">عملیات جاری</param>
        /// <param name="SelectedWorkHeatID">شناسه نوبت کاری انتخاب شده</param>
        /// <param name="WorkHeatCode">کد نوبت کاری</param>
        /// <param name="WorkHeatName">نام نوبت کاری</param>
        /// <param name="WorkHeatDescription">توضیحات نوبت کاری</param>
        /// <returns>آرایه ای از پیغام و شناسه</returns>
        [Ajax.AjaxMethod("UpdateEmploy_EmployTypesPage", "UpdateEmploy_EmployTypesPage_onCallBack", null, null)]
        public string[] UpdateEmploy_EmployTypesPage(string state, string SelectedEmployTypeID, string EmployTypeCode, string EmployTypeName)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                decimal EmployTypeID = 0;
                decimal selectedEmployTypeID = decimal.Parse(this.StringBuilder.CreateString(SelectedEmployTypeID));
                EmployTypeCode = this.StringBuilder.CreateString(EmployTypeCode);
                EmployTypeName = this.StringBuilder.CreateString(EmployTypeName);
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                EmploymentType employType = new EmploymentType();
                employType.ID = selectedEmployTypeID;
                if (uam != UIActionType.DELETE)
                {
                    employType.CustomCode = EmployTypeCode;
                    employType.Name = EmployTypeName;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        EmployTypeID = this.EmployTypeBusiness.InsertEmploymentType(employType, uam);
                        break;
                    case UIActionType.EDIT:
                        if (selectedEmployTypeID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoEmployTypeSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        EmployTypeID = this.EmployTypeBusiness.UpdateEmploymentType(employType, uam);
                        break;
                    case UIActionType.DELETE:
                        if (selectedEmployTypeID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoEmployTypeSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        EmployTypeID = this.EmployTypeBusiness.DeleteEmploymentType(employType, uam);
                        break;
                }

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
                retMessage[3] = EmployTypeID.ToString();
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