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
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.BaseInformation;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class ControlStations : GTSBasePage
    {
        public BControlStation ControlStationBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BControlStation>() ;
            }
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
            ControlStations_onPageLoad,
            tbControlStations_TabStripMenus_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_GridControlStations_ControlStations.IsCallback)
            {
                Page ControlStationsPage = this;
                Ajax.Utility.GenerateMethodScripts(ControlStationsPage);
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckControlStationsLoadAccess_ControlStations();
            }
        }

        private void CheckControlStationsLoadAccess_ControlStations()
        {
            string[] retMessage = new string[4];
            try
            {
                this.ControlStationBusiness.CheckControlStationsLoadAccess();
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

        private void SetCurrentCultureResObjs(string LangID)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }

        protected void CallBack_GridControlStations_ControlStations_onCallback(object sender, CallBackEventArgs e)
        {
            this.Fill_GridControlStations_ControlStations();
            this.ErrorHiddenField_ControlStations.RenderControl(e.Output);
            this.GridControlStations_ControlStations.RenderControl(e.Output);
        }

        private void Fill_GridControlStations_ControlStations()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<ControlStation> ControlStationsList = this.ControlStationBusiness.GetAll();
                this.GridControlStations_ControlStations.DataSource = ControlStationsList;
                this.GridControlStations_ControlStations.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_ControlStations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_ControlStations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_ControlStations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdateControlStation_ControlStationsPage", "UpdateControlStation_ControlStationsPage_onCallBack", null, null)]
        public string[] UpdateControlStation_ControlStationsPage(string state, string SelectedControlStationID, string ControlStationCode, string ControlStationName)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                decimal ControlStationID = 0;
                decimal selectedControlStationID = decimal.Parse(this.StringBuilder.CreateString(SelectedControlStationID));
                ControlStationCode = this.StringBuilder.CreateString(ControlStationCode);
                ControlStationName = this.StringBuilder.CreateString(ControlStationName);
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                ControlStation controlStation = new ControlStation();
                controlStation.ID = selectedControlStationID;
                if (uam != UIActionType.DELETE)
                {
                    controlStation.CustomCode = ControlStationCode;
                    controlStation.Name = ControlStationName;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        ControlStationID = this.ControlStationBusiness.InsertControlStation(controlStation, uam);
                        break;
                    case UIActionType.EDIT:
                        if (selectedControlStationID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoControlStationSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        ControlStationID = this.ControlStationBusiness.UpdateControlStation(controlStation, uam);
                        break;
                    case UIActionType.DELETE:
                        if (selectedControlStationID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoControlStationSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        ControlStationID = this.ControlStationBusiness.DeleteControlStation(controlStation, uam);
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
                retMessage[3] = ControlStationID.ToString();
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