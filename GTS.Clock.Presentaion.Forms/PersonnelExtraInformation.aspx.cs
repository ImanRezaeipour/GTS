using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.PersonInfo;
using GTS.Clock.Model.PersonInfo;
using System.Web.Script.Serialization;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business;

public partial class PersonnelExtraInformation : GTSBasePage
{
    public BPersonReservedField PersonnelReservedFieldBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BPersonReservedField>();
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

    internal class ReserveFieldObj
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    enum Scripts
    {
        PersonnelExtraInformation_onPageLoad,
        DialogPersonnelExtraInformation_Operations,
        DialogPersonnelExtraInformationSettings_onPageLoad,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CallBack_cmbValue_R16_DialogPersonnelExtraInformation.IsCallback && !CallBack_cmbValue_R17_DialogPersonnelExtraInformation.IsCallback && !CallBack_cmbValue_R18_DialogPersonnelExtraInformation.IsCallback && !CallBack_cmbValue_R19_DialogPersonnelExtraInformation.IsCallback && !CallBack_cmbValue_R20_DialogPersonnelExtraInformation.IsCallback)
        {
            RefererValidationProvider.CheckReferer();
            Page PersonnelExtraInformationPage = this;
            Ajax.Utility.GenerateMethodScripts(PersonnelExtraInformationPage);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckPersonnelReserveFieldsLoadAccess_PersonnelExtraInformation();
        }
    }

    private void CheckPersonnelReserveFieldsLoadAccess_PersonnelExtraInformation()
    {
        string[] retMessage = new string[4];
        try
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("PersonnelState"))
            {
                UIActionType UAT = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["PersonnelState"]).ToUpper());
                switch (UAT)
                {
                    case UIActionType.ADD:
                        this.PersonnelReservedFieldBusiness.CheckPersonnelReserveFieldsLoadAccess_onPersonnelInsert();
                        break;
                    case UIActionType.EDIT:
                        this.PersonnelReservedFieldBusiness.CheckPersonnelReserveFieldsLoadAccess_onPersonnelUpdate();
                        break;
                }
            }
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

    [Ajax.AjaxMethod("GetReserveFieldHeaders_PersonnelExtraInformationPage", "GetReserveFieldHeaders_PersonnelExtraInformationPage_onCallback", null, null)]
    public string[] GetReserveFieldHeaders_PersonnelExtraInformationPage(string temp)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            IList<ReserveFieldObj> ReserveFieldObjList = new List<ReserveFieldObj>();
            IList<PersonReserveField> PersonReserveFieldsList = this.PersonnelReservedFieldBusiness.GetAllReservedFields();
            foreach (PersonReserveField PersonReserveFieldItem in PersonReserveFieldsList)
            {
                ReserveFieldObj reserveFieldObj = new ReserveFieldObj();
                reserveFieldObj.Name = PersonReserveFieldItem.OrginalName.Trim();
                reserveFieldObj.Value = PersonReserveFieldItem.Lable != string.Empty ? PersonReserveFieldItem.Lable : string.Empty;
                ReserveFieldObjList.Add(reserveFieldObj);
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("OperationComplete").ToString();
            retMessage[2] = "success";
            retMessage[3] = this.JsSerializer.Serialize(ReserveFieldObjList);

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

    protected void CallBack_cmbValue_R16_DialogPersonnelExtraInformation_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbValue_R16_DialogPersonnelExtraInformation.Dispose();
        this.Fill_cmbValue_ReserveFieldControl_DialogPersonnelExtraInformation(PersonReservedFieldComboItems.R16, cmbValue_R16_DialogPersonnelExtraInformation, ErrorHiddenField_R16_DialogPersonnelExtraInformation);
        this.ErrorHiddenField_R16_DialogPersonnelExtraInformation.RenderControl(e.Output);
        this.cmbValue_R16_DialogPersonnelExtraInformation.RenderControl(e.Output);
    }

    protected void CallBack_cmbValue_R17_DialogPersonnelExtraInformation_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbValue_R17_DialogPersonnelExtraInformation.Dispose();
        this.Fill_cmbValue_ReserveFieldControl_DialogPersonnelExtraInformation(PersonReservedFieldComboItems.R17, this.cmbValue_R17_DialogPersonnelExtraInformation, this.ErrorHiddenField_R17_DialogPersonnelExtraInformation);
        this.ErrorHiddenField_R17_DialogPersonnelExtraInformation.RenderControl(e.Output);
        this.cmbValue_R17_DialogPersonnelExtraInformation.RenderControl(e.Output);
    }

    protected void CallBack_cmbValue_R18_DialogPersonnelExtraInformation_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbValue_R18_DialogPersonnelExtraInformation.Dispose();
        this.Fill_cmbValue_ReserveFieldControl_DialogPersonnelExtraInformation(PersonReservedFieldComboItems.R18, this.cmbValue_R18_DialogPersonnelExtraInformation, this.ErrorHiddenField_R18_DialogPersonnelExtraInformation);
        this.ErrorHiddenField_R18_DialogPersonnelExtraInformation.RenderControl(e.Output);
        this.cmbValue_R18_DialogPersonnelExtraInformation.RenderControl(e.Output);
    }

    protected void CallBack_cmbValue_R19_DialogPersonnelExtraInformation_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbValue_R19_DialogPersonnelExtraInformation.Dispose();
        this.Fill_cmbValue_ReserveFieldControl_DialogPersonnelExtraInformation(PersonReservedFieldComboItems.R19, this.cmbValue_R19_DialogPersonnelExtraInformation, this.ErrorHiddenField_R19_DialogPersonnelExtraInformation);
        this.ErrorHiddenField_R19_DialogPersonnelExtraInformation.RenderControl(e.Output);
        this.cmbValue_R19_DialogPersonnelExtraInformation.RenderControl(e.Output);
    }

    protected void CallBack_cmbValue_R20_DialogPersonnelExtraInformation_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbValue_R20_DialogPersonnelExtraInformation.Dispose();
        this.Fill_cmbValue_ReserveFieldControl_DialogPersonnelExtraInformation(PersonReservedFieldComboItems.R20, this.cmbValue_R20_DialogPersonnelExtraInformation, this.ErrorHiddenField_R20_DialogPersonnelExtraInformation);
        this.ErrorHiddenField_R20_DialogPersonnelExtraInformation.RenderControl(e.Output);
        this.cmbValue_R20_DialogPersonnelExtraInformation.RenderControl(e.Output);
    }

    private void Fill_cmbValue_ReserveFieldControl_DialogPersonnelExtraInformation(PersonReservedFieldComboItems PRFI, ComboBox cmb, HiddenField hf)
    {
        string[] retMessage = new string[4];

        this.InitializeCulture();

        try
        {
            IList<PersonReserveFieldComboValue> PersonReserveFieldComboValueItems = this.PersonnelReservedFieldBusiness.GetComboItemsByOrginalName(PRFI);
            foreach (PersonReserveFieldComboValue personReserveFieldComboValueItem in PersonReserveFieldComboValueItems)
            {
                ComboBoxItem cmbItem = new ComboBoxItem(personReserveFieldComboValueItem.ComboText);
                cmbItem.Value = personReserveFieldComboValueItem.ComboValue;
                cmbItem.Id = personReserveFieldComboValueItem.ID.ToString();
                cmb.Items.Add(cmbItem);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            hf.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            hf.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            hf.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }



}