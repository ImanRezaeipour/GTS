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
using GTS.Clock.Business.Shifts;
using GTS.Clock.Model.Concepts;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using System.Web.Script.Serialization;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class WorkGroups  : GTSBasePage
    {
        public BWorkgroup WorkGroupBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BWorkgroup>();
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

        public JavaScriptSerializer JsSerializer
        {
            get
            {
                return new JavaScriptSerializer();
            }
        }

        internal class CurrentYearObj
        {
            public string Year { get; set; }
            public string UIYear { get; set; }
            public string Index { get; set; }
        }

        enum Scripts
        {
            tbWorkGroups_TabStripMenus_Operations,
            WorkGroups_onPageLoad,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!this.CallBack_GridWorkGroups_WorkGroups.CausedCallback)
            {
                Page WorkGroupPage = this;
                Ajax.Utility.GenerateMethodScripts(WorkGroupPage);
                Fill_cmbYear_WorkGroups();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckWorkGroupsLoadAccess_WorkGroups();
            }
        }

        private void CheckWorkGroupsLoadAccess_WorkGroups()
        {
            string[] retMessage = new string[4];
            try
            {
                this.WorkGroupBusiness.CheckWorkGroupsLoadAccess();
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);            
            }
        }

        private void Fill_cmbYear_WorkGroups()
        {
            int CurrentYear = DateTime.Now.Year;
            string CurrentUIYear = string.Empty;
            int CurrentYearIndex = 0;
            int YearCounter = 0;
            string cmbItemText = string.Empty;
            string SysLangID = this.LangProv.GetCurrentSysLanguage();
            PersianCalendar pCal = new PersianCalendar();
            for (int i = CurrentYear - 4; i <= (CurrentYear + 4); i++)
            {
                ComboBoxItem cmbItemYear = new ComboBoxItem(i.ToString());
                cmbItemYear.Value = i.ToString();
                switch (SysLangID)
                {
                    case "fa-IR":
                        cmbItemText = pCal.GetYear(new DateTime(i, 12, 1)).ToString();
                        break;
                    case "en-US":
                        cmbItemText = i.ToString();
                        break;
                }
                cmbItemYear.Text = cmbItemText;
                this.cmbYear_WorkGroup.Items.Add(cmbItemYear);
                ++YearCounter;
                if (i == CurrentYear)
                {
                    CurrentUIYear = cmbItemText;
                    CurrentYearIndex = YearCounter - 1;
                }
            }

            CurrentYearObj currentYearObj = new CurrentYearObj();
            currentYearObj.Year = CurrentYear.ToString();
            currentYearObj.UIYear = CurrentUIYear;
            currentYearObj.Index = CurrentYearIndex.ToString();
            this.hfCurrentYear_WorkGroup.Value = this.JsSerializer.Serialize(currentYearObj);
            this.cmbYear_WorkGroup.SelectedIndex = CurrentYearIndex;
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
        /// CallBack گرید گروه کاری
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CallBack_GridWorkGroups_WorkGroups_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_GridWorkGroup_WorkGroup();
            this.GridWorkGroups_WorkGroups.RenderControl(e.Output);
            this.ErrorHiddenField_WorkGroups.RenderControl(e.Output);
        }

        /// <summary>
        /// پر کردن گرید گروه کاری
        /// </summary>
        private void Fill_GridWorkGroup_WorkGroup()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<WorkGroup> workGroupList = this.WorkGroupBusiness.GetAll();
                this.GridWorkGroups_WorkGroups.DataSource = workGroupList;
                this.GridWorkGroups_WorkGroups.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_WorkGroups.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_WorkGroups.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_WorkGroups.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        /// <summary>
        /// درج و ویرایش وحذف گروه کاری
        /// </summary>
        /// <param name="state">عملیات جاری</param>
        /// <param name="SelectedWorkHeatID">شناسه گروه کاری انتخاب شده</param>
        /// <param name="WorkHeatCode">کد گروه کاری</param>
        /// <param name="WorkHeatName">نام گروه کاری</param>
        /// <returns>آرایه ای از پیغام و شناسه</returns>
        [Ajax.AjaxMethod("UpdateWorkGroup_WorkGroupPage", "UpdateWorkGroup_WorkGroupPage_onCallBack", null, null)]
        public string[] UpdateWorkGroup_WorkGroupPage(string state, string SelectedWorkGroupID, string WorkGroupCode, string WorkGroupName)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                decimal WorkGroupID = 0;
                decimal selectedWorkGroupID = decimal.Parse(this.StringBuilder.CreateString(SelectedWorkGroupID));
                WorkGroupCode = this.StringBuilder.CreateString(WorkGroupCode);
                WorkGroupName = this.StringBuilder.CreateString(WorkGroupName);
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                WorkGroup workGroup = new WorkGroup();
                workGroup.ID = selectedWorkGroupID;
                if (uam != UIActionType.DELETE)
                {
                    workGroup.CustomCode = WorkGroupCode;
                    workGroup.Name = WorkGroupName;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        WorkGroupID = this.WorkGroupBusiness.InsertWorkGroup(workGroup, uam);
                        break;
                    case UIActionType.EDIT:
                        if (selectedWorkGroupID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoWorkGroupSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        WorkGroupID = this.WorkGroupBusiness.UpdateWorkGroup(workGroup, uam);
                        break;
                    case UIActionType.DELETE:
                        uam = UIActionType.DELETE;
                        if (selectedWorkGroupID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoWorkGroupSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        WorkGroupID = this.WorkGroupBusiness.DeleteWorkGroup(workGroup, uam);
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
                retMessage[3] = WorkGroupID.ToString();
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