using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Configuration;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Presentaion.Forms.App_Code;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Shifts;
using System.Text;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class Shifts : GTSBasePage
    {
        public BShift ShiftBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BShift>();
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
            Shifts_onPageLoad,
            tbShiftIntroduction_TabStripMenus_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!this.CallBack_GridShift_Shift.CausedCallback && !this.CallBack_GridShiftPairs_Shift.CausedCallback && !this.CallBackcmbShiftType_Shift.CausedCallback)
            {
                Page ShiftPage = this;
                Ajax.Utility.GenerateMethodScripts(ShiftPage);
                this.SetShiftTypesStr_Shift();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckShiftsLoadAccess_Shifts();
            }
        }

        private void CheckShiftsLoadAccess_Shifts()
        {
            string[] retMessage = new string[4];
            try
            {
                this.ShiftBusiness.CheckShiftsLoadAccess();
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);            
            }
        }
        
        /// <summary>
        /// رشته انواع شیفت ها با توجه به زبان انتخابی کاربر
        /// </summary>
        private void SetShiftTypesStr_Shift()
        {
            string strShiftTypes = string.Empty;
            foreach (ShiftTypesEnum shiftTypeItem in Enum.GetValues(typeof(ShiftTypesEnum)))
            {
                strShiftTypes +=  "#" + GetLocalResourceObject(shiftTypeItem.ToString()).ToString() + ":" + ((int)shiftTypeItem).ToString();
            }
            this.hfShiftTypes_Shift.Value = strShiftTypes;
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
        /// CallBack گرید شیفت
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CallBack_GridShift_Shift_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.Fill_GridShift_Shift();
            this.GridShift_Shift.RenderControl(e.Output);
            this.ErrorHiddenField_Shift.RenderControl(e.Output);
        }

        /// <summary>
        /// پر کردن گرید شیفت
        /// </summary>
        private void Fill_GridShift_Shift()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();   
                IList<Shift> ShiftList = this.ShiftBusiness.GetAll();
                this.GridShift_Shift.DataSource = ShiftList;
                this.GridShift_Shift.DataBind();
            }
            catch (UIValidationExceptions ex)
            {   
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Shift.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Shift.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Shift.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        /// <summary>
        /// پر گردن گرید بازه های زمانی شیفت
        /// </summary>
        /// <param name="shiftID">شناسه شیفت انتخاب شده</param>
        private void Fill_GridShiftPairs_Shift(decimal shiftID)
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<ShiftPair> ShiftPairList = this.ShiftBusiness.GetByID(shiftID).Pairs;
                this.GridShiftPairs_Shift.DataSource = ShiftPairList;
                this.GridShiftPairs_Shift.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Shift.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Shift.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Shift.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            } 
        }

        /// <summary>
        /// CallBack گرید بازه های زمانی شیفت
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CallBack_GridShiftPairs_Shift_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.Fill_GridShiftPairs_Shift(decimal.Parse(e.Parameter));
            this.GridShiftPairs_Shift.RenderControl(e.Output);
            this.ErrorHiddenField_ShiftPairs.RenderControl(e.Output);
        }

        /// <summary>
        /// CallBack لیست آبشاری انواع شیفت
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CallBackcmbShiftType_Shift_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbShiftType_Shift.Dispose();
            this.Fill_cmbShiftType_Shift();
            this.cmbShiftType_Shift.Enabled = true;
            this.cmbShiftType_Shift.RenderControl(e.Output);
            this.ErrorHiddenField_ShiftType.RenderControl(e.Output);
        }

        private void Fill_cmbShiftType_Shift()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                foreach (ShiftTypesEnum shiftTypeItem in Enum.GetValues(typeof(ShiftTypesEnum)))
                {
                    ComboBoxItem cmbItemShiftType = new ComboBoxItem(GetLocalResourceObject(shiftTypeItem.ToString()).ToString());
                    cmbItemShiftType.Value = shiftTypeItem.ToString();
                    cmbItemShiftType.Id = ((int)shiftTypeItem).ToString();
                    this.cmbShiftType_Shift.Items.Add(cmbItemShiftType);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_ShiftType.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_ShiftType.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_ShiftType.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            } 
        }

        /// <summary>
        /// CallBack لیست آبشاری نوبت کاری
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CallBackcmbWorkHeat_Shift_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbWorkHeat_Shift.Dispose();
            this.Fill_cmbWorkHeat_Shift();
            this.cmbWorkHeat_Shift.Enabled = true;
            this.cmbWorkHeat_Shift.RenderControl(e.Output);
            this.ErrorHiddenField_WorkHeat.RenderControl(e.Output);
        }

        /// <summary>
        /// پرکردن لیست آبشاری نوبت کاری
        /// </summary>
        private void Fill_cmbWorkHeat_Shift()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<NobatKari> WorkHeatList = this.ShiftBusiness.GetAllNobatKari(this.GetLocalResourceObject("cmbNoneItem").ToString());
                this.cmbWorkHeat_Shift.DataSource = WorkHeatList;
                this.cmbWorkHeat_Shift.DataBind();
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
        /// درج و ویرایش و حذف شیفت
        /// </summary>
        /// <param name="state">عملیات جاری</param>
        /// <param name="SelectedShiftID">شناسه شیفت انتخاب شده</param>
        /// <param name="ShiftCode">کد شبفت</param>
        /// <param name="ShiftName">نام شیفت</param>
        /// <param name="ShiftType">نوع شیفت</param>
        /// <param name="WorkHeatID">شناسه نوبت کاری انتخاب شده</param>
        /// <param name="WorkHeatMin">حداقل نوبت کاری</param>
        /// <param name="ShiftColor">رنگ شیفت</param>
        /// <param name="BreakFast">صبحانه</param>
        /// <param name="Lunch">ناهار</param>
        /// <param name="Dinner">شام</param>
        /// <returns>آرایه ای از پیغام و شناسه</returns>
        [Ajax.AjaxMethod("UpdateShift_ShiftPage", "UpdateShift_ShiftPage_onCallBack", null, null)]
        public string[] UpdateShift_ShiftPage(string state, string SelectedShiftID, string ShiftCode, string ShiftName, string ShiftType, string WorkHeatID, string WorkHeatMin, string ShiftColor,  string BreakFast, string Lunch, string Dinner)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                decimal ShiftID = 0;
                decimal selectedShiftID = decimal.Parse(this.StringBuilder.CreateString(SelectedShiftID));
                ShiftCode = this.StringBuilder.CreateString(ShiftCode);
                ShiftName = this.StringBuilder.CreateString(ShiftName);
                int shiftType = int.Parse(this.StringBuilder.CreateString(ShiftType));
                decimal workHeatID = decimal.Parse(this.StringBuilder.CreateString(WorkHeatID));
                WorkHeatMin = this.StringBuilder.CreateString(WorkHeatMin);
                ShiftColor = this.StringBuilder.CreateString(ShiftColor);
                bool breakfast = bool.Parse(this.StringBuilder.CreateString(BreakFast));
                bool lunch = bool.Parse(this.StringBuilder.CreateString(Lunch));
                bool dinner = bool.Parse(this.StringBuilder.CreateString(Dinner));
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                Shift shift = new Shift();
                shift.ID = selectedShiftID;
                if (uam != Business.UIActionType.DELETE)
                {
                    shift.CustomCode = ShiftCode;
                    shift.Name = ShiftName;
                    if (ShiftType != string.Empty)
                        shift.ShiftType = (ShiftTypesEnum)Enum.ToObject(typeof(ShiftTypesEnum), shiftType);
                    else
                        shift.ShiftType = null;
                    shift.NobatKariID = workHeatID;
                    shift.MinNobatKariTime = WorkHeatMin;
                    shift.Color = ShiftColor;
                    shift.Breakfast = breakfast;
                    shift.Lunch = lunch;
                    shift.Dinner = dinner;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        ShiftID = this.ShiftBusiness.InsertShift(shift, uam);
                        break;
                    case UIActionType.EDIT:
                        if (selectedShiftID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoShiftSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        ShiftID = this.ShiftBusiness.UpdateShift(shift, uam);
                        break;
                    case UIActionType.DELETE:
                        if (selectedShiftID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoShiftSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        this.ShiftBusiness.DeleteShift(shift, uam);
                        break;
                }

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uam)
                {
                    case Business.UIActionType.ADD:
                        SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                        break;
                    case Business.UIActionType.EDIT:
                        SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                        break;
                    case Business.UIActionType.DELETE:
                        SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                        break;
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = ShiftID.ToString();
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
        /// درج و ویرایش و حذف بازه زمانی شیفت
        /// </summary>
        /// <param name="state">عملیات جاری</param>
        /// <param name="SelectedShiftID">شناسه شیفت انتخاب شده</param>
        /// <param name="SelectedShiftPairID">شناسه بازه زمانی شیفت انتخاب شده</param>
        /// <param name="From">از</param>
        /// <param name="To">تا</param>
        /// <param name="FromTolerance">از تلرانس</param>
        /// <param name="ToTolerance">تا تلرانس</param>
        /// <returns>آرایه ای از پیغام و شناسه</returns>
        [Ajax.AjaxMethod("UpdateShiftPair_ShiftPage", "UpdateShiftPair_ShiftPage_onCallBack", null, null)]
        public string[] UpdateShiftPair_ShiftPage(string state, string SelectedShiftID, string SelectedShiftPairID, string From, string To, string FromTolerance, string ToTolerance, string ContinueInNextDay)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                decimal ShiftPairID = 0;
                decimal selectedShiftID = decimal.Parse(this.StringBuilder.CreateString(SelectedShiftID));
                decimal selectedShiftPairID = decimal.Parse(this.StringBuilder.CreateString(SelectedShiftPairID));
                From = this.StringBuilder.CreateString(From);
                To = this.StringBuilder.CreateString(To);
                FromTolerance = this.StringBuilder.CreateString(FromTolerance);
                ToTolerance = this.StringBuilder.CreateString(ToTolerance);
                bool continueInNextDay = bool.Parse(this.StringBuilder.CreateString(ContinueInNextDay));
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                ShiftPair shiftPair = new ShiftPair();
                shiftPair.ShiftId = selectedShiftID;
                shiftPair.ID = selectedShiftPairID;
                if (uam != Business.UIActionType.DELETE)
                {
                    shiftPair.FromTime = From;
                    shiftPair.ToTime = To;
                    shiftPair.BeforeToleranceTime = FromTolerance;
                    shiftPair.AfterToleranceTime = ToTolerance;
                    shiftPair.NextDayContinual = continueInNextDay;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        ShiftPairID = this.ShiftBusiness.InsertShiftPair(shiftPair, uam);
                        break;
                    case UIActionType.EDIT: 
                        if (selectedShiftPairID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoShiftPairSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        ShiftPairID = this.ShiftBusiness.UpdateShiftPair(shiftPair, uam);
                        break;
                    case UIActionType.DELETE:
                        if (selectedShiftPairID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoShiftPairSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        ShiftPairID = this.ShiftBusiness.DeleteShiftPair(shiftPair, uam);
                        break;
                }

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uam)
                {
                    case Business.UIActionType.ADD:
                        SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                        break;
                    case Business.UIActionType.EDIT:
                        SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                        break;
                    case Business.UIActionType.DELETE:
                        SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                        break;
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = ShiftPairID.ToString();
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