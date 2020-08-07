using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.UI;
using ComponentArt.Web.UI;
using Ajax;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure;
using System.Globalization;
using System.Threading;
using GTS.Clock.Infrastructure.Exceptions;


public partial class TrafficsTransfer : GTSBasePage
{
    public BTraffic TrafficBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BTraffic>();
        }
    }

    public BClock MachineBusiness
    {
        get
        {
            return new BClock();
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
        TrafficsTransfer_onPageLoad,
        DialogTrafficsTransfer_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_cmbMachines_TrafficsTransfer.IsCallback && !CallBack_cmbTrafficTransferType_TrafficsTransfer.IsCallback)
        {
            Page TrafficsTransferPage = this;
            Ajax.Utility.GenerateMethodScripts(TrafficsTransferPage);

            this.ViewCurrentCalendars_TrafficsTransfer();
            this.SetCurrentDate_TrafficsTransfer();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
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

    private void ViewCurrentCalendars_TrafficsTransfer()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                Container_pdpFromDate_TrafficsTransfer.Visible = true;
                Container_pdpToDate_TrafficsTransfer.Visible = true;
                break;
            case "en-US":
                Container_gdpFromDate_TrafficsTransfer.Visible = true;
                Container_gdpToDate_TrafficsTransfer.Visible = true;
                break;
        }
    }

    private void SetCurrentDate_TrafficsTransfer()
    {
        string strCurrentDate = string.Empty;
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "en-US":
                strCurrentDate = DateTime.Now.ToShortDateString();
                break;
            case "fa-IR":
                strCurrentDate = this.LangProv.GetSysDateString(DateTime.Now);
                break;
        }
        this.hfCurrentDate_TrafficsTransfer.Value = strCurrentDate;
    }

    protected void CallBack_cmbMachines_TrafficsTransfer_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_cmbMachines_TrafficsTransfer();
        this.ErrorHiddenField_Machines_TrafficsTransfer.RenderControl(e.Output);
        this.cmbMachines_TrafficsTransfer.RenderControl(e.Output);
    }

    private void Fill_cmbMachines_TrafficsTransfer()
    {
        string[] retMessage = new string[4];
        try
        {
            ComboBoxItem cmbItemAllMachines = new ComboBoxItem(GetLocalResourceObject("AllMachines").ToString());
            cmbItemAllMachines.Value = cmbItemAllMachines.Id = "0";
            this.cmbMachines_TrafficsTransfer.Items.Add(cmbItemAllMachines);

            IList<Clock> MachinesList = this.MachineBusiness.GetAll();
            foreach (Clock MachineItem in MachinesList)
            {
                ComboBoxItem cmbItemMachine = new ComboBoxItem(MachineItem.Name);
                cmbItemMachine.Value = MachineItem.CustomCode.ToString();
                cmbItemMachine.Id = MachineItem.ID.ToString();
                this.cmbMachines_TrafficsTransfer.Items.Add(cmbItemMachine);
            }
            this.cmbMachines_TrafficsTransfer.SelectedIndex = 0;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Machines_TrafficsTransfer.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Machines_TrafficsTransfer.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Machines_TrafficsTransfer.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbTrafficTransferType_TrafficsTransfer_onCallBack(object sender, CallBackEventArgs e)
    { 
        this.Fill_cmbTrafficTransferType_TrafficsTransfer();
        this.ErrorHiddenField_TrafficTransferType_TrafficsTransfer.RenderControl(e.Output);
        this.cmbTrafficTransferType_TrafficsTransfer.RenderControl(e.Output);
    }

    private void Fill_cmbTrafficTransferType_TrafficsTransfer()
    {
        string[] retMessage = new string[4];
        try
        {
            foreach (TrafficTransferType trafficTransferTypeItem in Enum.GetValues(typeof(TrafficTransferType)))
            {
                ComboBoxItem cmbItemTrafficTransferType = new ComboBoxItem(GetLocalResourceObject(trafficTransferTypeItem.ToString()).ToString());
                cmbItemTrafficTransferType.Value = trafficTransferTypeItem.ToString();
                this.cmbTrafficTransferType_TrafficsTransfer.Items.Add(cmbItemTrafficTransferType);
            }
            this.cmbTrafficTransferType_TrafficsTransfer.SelectedIndex = 0;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_TrafficTransferType_TrafficsTransfer.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_TrafficTransferType_TrafficsTransfer.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_TrafficTransferType_TrafficsTransfer.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("TrafficsTransferByConditions_TrafficsTransferPage", "TrafficsTransferByConditions_TrafficsTransferPage_onCallBack", null, null)]
    public string[] TrafficsTransferByConditions_TrafficsTransferPage(string trafficTransferMode, string trafficTransferType, string MachineID, string FromDate, string ToDate, string FromTime, string ToTime, string FromRecord, string ToRecord, string FromIdentifier, string ToIdentifier, string TransferDay, string TransferTime, string IsIntegralConditions)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            TrafficTransferMode TTM = (TrafficTransferMode)Enum.Parse(typeof(TrafficTransferMode), this.StringBuilder.CreateString(trafficTransferMode));
            TrafficTransferType TTT = (TrafficTransferType)Enum.Parse(typeof(TrafficTransferType), this.StringBuilder.CreateString(trafficTransferType));
            decimal machineID = decimal.Parse(this.StringBuilder.CreateString(MachineID));
            FromDate = this.StringBuilder.CreateString(FromDate);
            ToDate = this.StringBuilder.CreateString(ToDate);
            FromTime = this.StringBuilder.CreateString(FromTime);
            ToTime = this.StringBuilder.CreateString(ToTime);
            int fromRecored = int.Parse(this.StringBuilder.CreateString(FromRecord));
            int toRecored = int.Parse(this.StringBuilder.CreateString(ToRecord));
            decimal fromIdentifier = decimal.Parse(this.StringBuilder.CreateString(FromIdentifier));
            decimal toIdentifier = decimal.Parse(this.StringBuilder.CreateString(ToIdentifier));
            bool isIntegralConditions = bool.Parse(this.StringBuilder.CreateString(IsIntegralConditions));
            TransferDay = this.StringBuilder.CreateString(TransferDay);
            TransferTime = this.StringBuilder.CreateString(TransferTime);

            this.TrafficBusiness.TransferTrafficsByConditions(TTM, machineID, FromDate, ToDate, FromTime, ToTime, fromRecored, toRecored, fromIdentifier, toIdentifier, TransferDay, TransferTime, TTT, isIntegralConditions);

            retMessage[1] = GetLocalResourceObject("OperationComplete").ToString();
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