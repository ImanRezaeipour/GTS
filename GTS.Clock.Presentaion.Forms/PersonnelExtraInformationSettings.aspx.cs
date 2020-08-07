using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Business.PersonInfo;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Model.PersonInfo;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business;

public partial class PersonnelExtraInformationSettings : GTSBasePage
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

    internal class PersonnelReserveFieldObj
    {
        public string ControlType { get; set; }
        public string OriginalName { get; set; }
    }

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    public enum PageState
    {
        EditPersonnelExtraInformationSettings,
        AddReserveFieldItem,
        EditReserveFieldItem,
        DeleteReserveFieldItem
    }

    enum Scripts
    {
        PersonnelExtraInformationSettings_onPageLoad,
        DialogPersonnelExtraInformationSettings_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_cmbReserveFields_PersonnelExtraInformationSettings.IsCallback && !CallBack_GridReserveFieldItems_PersonnelExtraInformationSettings.IsCallback)
        {
            Page PersonnelExtraInformationSettingsPage = this;
            Ajax.Utility.GenerateMethodScripts(PersonnelExtraInformationSettingsPage);
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

    protected void CallBack_cmbReserveFields_PersonnelExtraInformationSettings_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbReserveFields_PersonnelExtraInformationSettings.Dispose();
        this.Fill_cmbReserveFields_PersonnelExtraInformationSettings();
        this.ErrorHiddenField_ReserveFields_PersonnelExtraInformationSettings.RenderControl(e.Output);
        this.cmbReserveFields_PersonnelExtraInformationSettings.RenderControl(e.Output);
    }

    private void Fill_cmbReserveFields_PersonnelExtraInformationSettings()
    {
        string[] retMessage = new string[4];

        this.InitializeCulture();
        try
        {
            IList<PersonReserveField> PersonReserveFieldsList = this.PersonnelReservedFieldBusiness.GetAllReservedFields();
            foreach (PersonReserveField PersonReserveFieldItem in PersonReserveFieldsList)
            {
                ComboBoxItem cmbItemReserveField = new ComboBoxItem(PersonReserveFieldItem.Lable);
                PersonnelReserveFieldObj personnelReserveFieldObj = new PersonnelReserveFieldObj();
                personnelReserveFieldObj.OriginalName = PersonReserveFieldItem.OrginalName;
                personnelReserveFieldObj.ControlType = PersonReserveFieldItem.ControlType.ToString();
                cmbItemReserveField.Value = this.JsSerializer.Serialize(personnelReserveFieldObj);
                cmbItemReserveField.Id = PersonReserveFieldItem.ID.ToString();
                this.cmbReserveFields_PersonnelExtraInformationSettings.Items.Add(cmbItemReserveField);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_ReserveFields_PersonnelExtraInformationSettings.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_ReserveFields_PersonnelExtraInformationSettings.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_ReserveFields_PersonnelExtraInformationSettings.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_GridReserveFieldItems_PersonnelExtraInformationSettings_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_GridReserveFieldItems_PersonnelExtraInformationSettings(decimal.Parse(this.StringBuilder.CreateString(e.Parameter)));
        this.ErrorHiddenField_ReserveFieldItems_PersonnelExtraInformationSettings.RenderControl(e.Output);
        this.GridReserveFieldItems_PersonnelExtraInformationSettings.RenderControl(e.Output);
    }

    private void Fill_GridReserveFieldItems_PersonnelExtraInformationSettings(decimal ReserveFieldID)
    {
        string[] retMessage = new string[4];

        this.InitializeCulture();

        try
        {
            PersonReserveField personnelReserveField = this.PersonnelReservedFieldBusiness.GetByID(ReserveFieldID);
            IList<PersonReserveFieldComboValue> PersonnelReserveFieldItemsList = personnelReserveField.ComboItems;
            this.GridReserveFieldItems_PersonnelExtraInformationSettings.DataSource = PersonnelReserveFieldItemsList;
            this.GridReserveFieldItems_PersonnelExtraInformationSettings.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_ReserveFieldItems_PersonnelExtraInformationSettings.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_ReserveFieldItems_PersonnelExtraInformationSettings.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_ReserveFieldItems_PersonnelExtraInformationSettings.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdatePersonnelExtraInformationSetting_PersonnelExtraInformationSettingsPage", "UpdatePersonnelExtraInformationSetting_PersonnelExtraInformationSettingsPage_onCallBack", null, null)]
    public string[] UpdatePersonnelExtraInformationSetting_PersonnelExtraInformationSettingsPage(string PersonnelState, string ReserveFieldID, string ReserveFieldName)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        decimal reserveFieldID = decimal.Parse(this.StringBuilder.CreateString(ReserveFieldID));
        ReserveFieldName = this.StringBuilder.CreateString(ReserveFieldName);

        try
        {
            if (reserveFieldID == 0)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoPersonnelReserveFieldSelectedforEdit").ToString()), retMessage);
                return retMessage;
            }

            switch ((UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(PersonnelState).ToUpper()))
            {
                case UIActionType.ADD:
                    this.PersonnelReservedFieldBusiness.UpdateReservedFieldLable_onPersonnelInsert(reserveFieldID, ReserveFieldName);
                    break;
                case UIActionType.EDIT:
                    this.PersonnelReservedFieldBusiness.UpdateReservedFieldLable_onPersonnelUpdate(reserveFieldID, ReserveFieldName);
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("EditComplete").ToString();
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

    [Ajax.AjaxMethod("UpdateReserveFieldItem_PersonnelExtraInformationSettingsPage", "UpdateReserveFieldItem_PersonnelExtraInformationSettingsPage_onCallback", null, null)]
    public string[] UpdateReserveFieldItem_PersonnelExtraInformationSettingsPage(string state, string personnelState, string ControlType, string ReserveFieldID, string SelectedReserveFieldItemID, string ReserveFieldItemName, string ReserveFieldItemAlias)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        PageState PG = (PageState)Enum.Parse(typeof(PageState), this.StringBuilder.CreateString(state));
        UIActionType PersonnelState = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(personnelState).ToUpper());
        PersonReservedFieldsType PRFT = (PersonReservedFieldsType)Enum.Parse(typeof(PersonReservedFieldsType), this.StringBuilder.CreateString(ControlType));
        decimal reserveFieldID = decimal.Parse(this.StringBuilder.CreateString(ReserveFieldID));
        decimal selectedReserveFieldItemID = decimal.Parse(this.StringBuilder.CreateString(SelectedReserveFieldItemID));
        ReserveFieldItemName = this.StringBuilder.CreateString(ReserveFieldItemName);
        ReserveFieldItemAlias = this.StringBuilder.CreateString(ReserveFieldItemAlias);

        retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
        string SuccessMessageBody = string.Empty;
        try
        {
            switch (PG)
            {
                case PageState.AddReserveFieldItem:
                    switch (PRFT)
	                {
                        case PersonReservedFieldsType.ComboValue:
                            switch (PersonnelState)
	                        {
                                case UIActionType.ADD:
                                    this.PersonnelReservedFieldBusiness.InsertComboItem_onPersonnelInsert(reserveFieldID, ReserveFieldItemName, ReserveFieldItemAlias);
                                    break;
                                case UIActionType.EDIT:
                                    this.PersonnelReservedFieldBusiness.InsertComboItem_onPersonnelUpdate(reserveFieldID, ReserveFieldItemName, ReserveFieldItemAlias);
                                    break;
	                        }
                            break;
	                }
                    SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                    break;
                case PageState.EditReserveFieldItem:
                    if (selectedReserveFieldItemID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoPersonnelReserveFieldItemSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    switch (PRFT)
	                {
                        case PersonReservedFieldsType.ComboValue:
                            switch (PersonnelState)
                            {
                                case UIActionType.ADD:
                                    this.PersonnelReservedFieldBusiness.EditComboItem_onPersonnelInsert(reserveFieldID, selectedReserveFieldItemID, ReserveFieldItemName, ReserveFieldItemAlias);
                                    break;
                                case UIActionType.EDIT:
                                    this.PersonnelReservedFieldBusiness.EditComboItem_onPersonnelUpdate(reserveFieldID, selectedReserveFieldItemID, ReserveFieldItemName, ReserveFieldItemAlias);
                                    break;
                            }
                            break;
	                }
                    SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                    break;
                case PageState.DeleteReserveFieldItem:
                    if (selectedReserveFieldItemID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoPersonnelReserveFieldItemSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    switch (PRFT)
	                {
                        case PersonReservedFieldsType.ComboValue:
                            switch (PersonnelState)
                            {
                                case UIActionType.ADD:
                                    this.PersonnelReservedFieldBusiness.DeleteComboItem_onPersonnelInsert(reserveFieldID, selectedReserveFieldItemID);
                                    break;
                                case UIActionType.EDIT:
                                    this.PersonnelReservedFieldBusiness.DeleteComboItem_onPersonnelUpdate(reserveFieldID, selectedReserveFieldItemID);
                                    break;
                            }
                            break;
	                }
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
            }

            retMessage[1] = SuccessMessageBody;
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