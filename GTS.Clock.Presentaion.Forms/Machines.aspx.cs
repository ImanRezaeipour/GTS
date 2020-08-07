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
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class Machines : GTSBasePage
    {
        public BClock MachineBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BClock>();
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
            Machines_onPageLoad,
            tbMachines_TabStripMenus_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbMachineTypes_Machines.IsCallback && !CallBack_GridMachines_Machines.IsCallback && !CallBcak_cmbControlStations_Machines.IsCallback)
            {
                Page MachinesPage = this;
                Ajax.Utility.GenerateMethodScripts(MachinesPage);
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckMachinesLoadAccess_Machines();
            }
        }

        private void CheckMachinesLoadAccess_Machines()
        {
            string[] retMessage = new string[4];
            try
            {
                this.MachineBusiness.CheckMachinesLoadAccess();
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

        protected void CallBack_GridMachines_Machines_onCallback(object sender, CallBackEventArgs e)
        {
            this.Fill_GridMachines_Machines();
            this.ErrorHiddenField_Machines.RenderControl(e.Output);
            this.GridMachines_Machines.RenderControl(e.Output);
        }

        private void Fill_GridMachines_Machines()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<GTS.Clock.Model.BaseInformation.Clock> MachinesList = this.MachineBusiness.GetAll();
                this.GridMachines_Machines.DataSource = MachinesList;
                this.GridMachines_Machines.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Machines.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Machines.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Machines.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbMachineTypes_Machines_onCallback(object sender, CallBackEventArgs e)
        {
            this.cmbMachineTypes_Machines.Dispose();
            this.Fill_cmbMachineTypes_Machines();
            this.ErrorHiddenField_MachineTypes.RenderControl(e.Output);
            this.cmbMachineTypes_Machines.RenderControl(e.Output);
        }

        private void Fill_cmbMachineTypes_Machines()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<ClockType> MachineTypesList = this.MachineBusiness.GetAllClockTypes();
                this.cmbMachineTypes_Machines.DataSource = MachineTypesList;
                this.cmbMachineTypes_Machines.DataBind();
                this.cmbMachineTypes_Machines.Enabled = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MachineTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MachineTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MachineTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBcak_cmbControlStations_Machines_onCallback(object sender, CallBackEventArgs e)
        {
            this.Fill_cmbControlStations_Machines();
            this.ErrorHiddenField_ControlStations.RenderControl(e.Output);
            this.cmbControlStation_Machines.RenderControl(e.Output);
        }

        private void Fill_cmbControlStations_Machines()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<ControlStation> ControlStationsList = this.MachineBusiness.GetAllControlStations();
                this.cmbControlStation_Machines.DataSource = ControlStationsList;
                this.cmbControlStation_Machines.DataBind();
                this.cmbControlStation_Machines.Enabled = true;
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

        [Ajax.AjaxMethod("UpdateMachine_MachinesPage", "UpdateMachine_MachinesPage_onCallBack", null, null)]
        public string[] UpdateMachine_MachinesPage(string state, string SelectedMachineID, string MachineCode, string MachineName, string MachineTypeID, string ControlStationID, string MachineConnectionTel, string IsActive)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                decimal MachineID = 0;
                decimal selectedMachineID = decimal.Parse(this.StringBuilder.CreateString(SelectedMachineID));
                MachineCode = this.StringBuilder.CreateString(MachineCode);
                MachineName = this.StringBuilder.CreateString(MachineName);
                decimal machineTypeID = decimal.Parse(this.StringBuilder.CreateString(MachineTypeID));
                decimal controlStationID = decimal.Parse(this.StringBuilder.CreateString(ControlStationID));
                MachineConnectionTel = this.StringBuilder.CreateString(MachineConnectionTel);
                bool isActive = bool.Parse(this.StringBuilder.CreateString(IsActive));
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                GTS.Clock.Model.BaseInformation.Clock machine = new Model.BaseInformation.Clock();
                machine.ID = selectedMachineID;
                if (uam != UIActionType.DELETE)
                {
                    machine.CustomCode = MachineCode;
                    machine.Name = MachineName;
                    ClockType machineType = new ClockType();
                    machineType.ID = machineTypeID;
                    machine.Clocktype = machineType;
                    ControlStation controlStation = new ControlStation();
                    controlStation.ID = controlStationID;
                    machine.Station = controlStation;
                    machine.Tel = MachineConnectionTel;
                    machine.Active = isActive;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        MachineID = this.MachineBusiness.InsertMachine(machine, uam);
                        break;
                    case UIActionType.EDIT:
                        if (selectedMachineID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoMachineSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        MachineID = this.MachineBusiness.UpdateMachine(machine, uam);
                        break;
                    case UIActionType.DELETE:
                        if (selectedMachineID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoMachineSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        MachineID = this.MachineBusiness.DeleteMachine(machine, uam);
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
                retMessage[3] = MachineID.ToString();
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