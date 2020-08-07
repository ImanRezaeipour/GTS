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
using ComponentArt.Web.UI;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business;
using GTS.Clock.Business.Shifts;
using System.IO;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Presentaion.Forms.App_Code;
using GTS.Clock.Business.AppSettings;


namespace GTS.Clock.Presentaion.WebForms
{
    public partial class WorkHeat : GTSBasePage
    {
        public BNobatkari WorkHeatBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BNobatkari>();
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
            WorkHeat_onPageLoad,
            tbWorkHeat_TabStripMenus_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!this.CallBack_GridWorkHeat_WorkHeat.CausedCallback)
            { 
                 Page WorkHeatPage = this;
                 Ajax.Utility.GenerateMethodScripts(WorkHeatPage);
                 SkinHelper.InitializeSkin(this.Page);
                 ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                 this.CheckWorkHeatsLoadAccess_WorkHeats();
            }
        }

        private void CheckWorkHeatsLoadAccess_WorkHeats()
        {
            string[] retMessage = new string[4];
            try
            {
                this.WorkHeatBusiness.CheckWorkHeatsLoadAccess();
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
        /// CallBack گرید نوبت کاری
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CallBack_GridWorkHeat_WorkHeat_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_GridWorkHeat_WorkHeat();
            this.GridWorkHeat_WorkHeat.RenderControl(e.Output);
            this.ErrorHiddenField_WorkHeat.RenderControl(e.Output);
        }

        /// <summary>
        /// پر کردن گرید نوبت کاری
        /// </summary>
        private void  Fill_GridWorkHeat_WorkHeat()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<NobatKari> workHeatList = this.WorkHeatBusiness.GetAll();
                this.GridWorkHeat_WorkHeat.DataSource = workHeatList;
                this.GridWorkHeat_WorkHeat.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_WorkHeat.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_WorkHeat.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_WorkHeat.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        /// <summary>
        /// درج و ویرایش وحذف نوبت کاری
        /// </summary>
        /// <param name="state">عملیات جاری</param>
        /// <param name="SelectedWorkHeatID">شناسه نوبت کاری انتخاب شده</param>
        /// <param name="WorkHeatCode">کد نوبت کاری</param>
        /// <param name="WorkHeatName">نام نوبت کاری</param>
        /// <param name="WorkHeatDescription">توضیحات نوبت کاری</param>
        /// <returns>آرایه ای از پیغام و شناسه</returns>
        [Ajax.AjaxMethod("UpdateWorkHeat_WorkHeatPage", "UpdateWorkHeat_WorkHeatPage_onCallBack", null, null)]
        public string[] UpdateWorkHeat_WorkHeatPage(string state, string SelectedWorkHeatID, string WorkHeatCode, string WorkHeatName, string WorkHeatDescription)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                decimal WorkHeatID = 0;
                decimal selectedWorkHeatID = decimal.Parse(this.StringBuilder.CreateString(SelectedWorkHeatID));
                WorkHeatCode = this.StringBuilder.CreateString(WorkHeatCode);
                WorkHeatName = this.StringBuilder.CreateString(WorkHeatName);
                WorkHeatDescription = this.StringBuilder.CreateString(WorkHeatDescription);
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                NobatKari workHeat = new NobatKari();
                workHeat.ID = selectedWorkHeatID;
                if (uam != UIActionType.DELETE)
                {
                    workHeat.CustomCode = WorkHeatCode;
                    workHeat.Name = WorkHeatName;
                    workHeat.Description = WorkHeatDescription;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        WorkHeatID = this.WorkHeatBusiness.InsertWorkHeat(workHeat, uam);
                        break;
                    case UIActionType.EDIT:
                        if (selectedWorkHeatID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoWorkHeatSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        WorkHeatID = this.WorkHeatBusiness.UpdateWorkHeat(workHeat, uam);
                        break;
                    case UIActionType.DELETE:
                        if (selectedWorkHeatID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoWorkHeatSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        WorkHeatID = this.WorkHeatBusiness.DeleteWorkHeat(workHeat, uam);
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
                retMessage[3] = WorkHeatID.ToString();
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