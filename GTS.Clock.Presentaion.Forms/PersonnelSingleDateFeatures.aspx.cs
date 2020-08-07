using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.Assignments;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Model;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;

public partial class PersonnelSingleDateFeatures : GTSBasePage
{
    public BAssignWorkGroup PersonnelWorkGroupBusiness
    {
        get
        {
            SysLanguageResource Slr = SysLanguageResource.Parsi;
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    Slr = SysLanguageResource.Parsi;
                    break;
                case "en-US":
                    Slr = SysLanguageResource.English;
                    break;
            }
            return new BAssignWorkGroup(Slr);
        }
    }

    public BAssignDateRange PersonnelCalculationRangeBusiness
    {
        get
        {
            SysLanguageResource Slr = SysLanguageResource.Parsi;
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    Slr = SysLanguageResource.Parsi;
                    break;
                case "en-US":
                    Slr = SysLanguageResource.English;
                    break;
            }
            return new BAssignDateRange(Slr);
        }
    }

    public BPerson PersonBusiness
    {
        get
        {
            SysLanguageResource Slr = SysLanguageResource.Parsi;
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    Slr = SysLanguageResource.Parsi;
                    break;
                case "en-US":
                    Slr = SysLanguageResource.English;
                    break;
            }
            LocalLanguageResource Llr = LocalLanguageResource.Parsi;
            switch (this.LangProv.GetCurrentLanguage())
            {
                case "fa-IR":
                    Llr = LocalLanguageResource.Parsi;
                    break;
                case "en-US":
                    Llr = LocalLanguageResource.English;
                    break;
            }
            return new BPerson(Slr, Llr);
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

    enum Caller
    {
        WorkGroups,
        CalculationRangesGroups
    }

    enum Scripts
    {
        PersonnelSingleDateFeatures_onPageLoad,
        DialogPersonnelSingleDateFeatures_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.CausedCallback && !CallBackcmbSingleDateFeatures_PersonnelSingleDateFeatures.CausedCallback)
        {
            Page PersonnelSingleDateFeaturesPage = this;
            Ajax.Utility.GenerateMethodScripts(PersonnelSingleDateFeaturesPage);

            this.CustomizePersonnelSingleDateFeature_PersonnelSingleDateFeatures();
            this.ViewCurrentLangCalendars_PersonnelSingleDateFeatures();
            this.SetCurrentDate_PersonnelSingleDateFeatures();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void CustomizePersonnelSingleDateFeature_PersonnelSingleDateFeatures()
    {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Caller"))
            {
                Caller caller = (Caller)Enum.Parse(typeof(Caller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Caller"]));
                this.CustomizeGeneralComponents_PersonnelSingleDateFeatures(caller);
                this.CustomizeGridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures(caller);
            } 
    }

    private void CustomizeGeneralComponents_PersonnelSingleDateFeatures(Caller caller)
    {
        string PersonnelSingleDateFeaturesName = string.Empty;
        string GridHeader = string.Empty;
        string DialogTitle = string.Empty;
        string DeleteMessage = string.Empty;
        string CloseMessage = string.Empty;
        switch (caller)
        {
            case Caller.WorkGroups:
                PersonnelSingleDateFeaturesName = this.GetLocalResourceObject("lblWorkGroupName_PersonnelSingleDateFeatures").ToString();
                GridHeader = this.GetLocalResourceObject("hfWorkGroupsheader_PersonnelSingleDateFeatures_PersonnelSingleDateFeatures").ToString();
                DialogTitle = this.GetLocalResourceObject("hfWorkGroupsTitle_DialogPersonnelSingleDateFeatures").ToString();
                DeleteMessage = this.GetLocalResourceObject("hfWorkGroupDeleteMessage_PersonnelSingleDateFeatures").ToString();
                CloseMessage = this.GetLocalResourceObject("hfWorkGroupCloseMessage_PersonnelSingleDateFeatures").ToString();
                break;
            case Caller.CalculationRangesGroups:
                PersonnelSingleDateFeaturesName = this.GetLocalResourceObject("lblCalculationRangeGroupName_PersonnelSingleDateFeatures").ToString();
                GridHeader = this.GetLocalResourceObject("hfCalculationRangesheader_PersonnelSingleDateFeatures_PersonnelSingleDateFeatures").ToString();
                DialogTitle = this.GetLocalResourceObject("hfCalculationRangesGroupsTitle_DialogPersonnelSingleDateFeatures").ToString();
                DeleteMessage = this.GetLocalResourceObject("hfCalculationRangeDeleteMessage_PersonnelSingleDateFeatures").ToString();
                CloseMessage = this.GetLocalResourceObject("hfCalculationRangeGroupCloseMessage_PersonnelSingleDateFeatures").ToString();
                break;
        }
        this.lblPersonnelSingleDateFeatureName_PersonnelSingleDateFeatures.Text = PersonnelSingleDateFeaturesName;
        this.hfheader_PersonnelSingleDateFeatures_PersonnelSingleDateFeatures.Value = GridHeader;
        this.hfTitle_DialogPersonnelSingleDateFeatures.Value = DialogTitle;
        this.hfDeleteMessage_PersonnelSingleDateFeatures.Value = DeleteMessage;
        this.hfCloseMessage_PersonnelSingleDateFeatures.Value = CloseMessage;
    }

    private void CustomizeGridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures(Caller caller)
    {
        string clmnPersonnelSingleDateFeaturesIDDataField = string.Empty;
        string clmnPersonnelSingleDateFeaturesNameDataField = string.Empty;
        string clmnPersonnelSingleDateFeaturesNameHeadingsText = string.Empty;
        switch (caller)
        {
            case Caller.WorkGroups:
                clmnPersonnelSingleDateFeaturesIDDataField = "WorkGroup.ID";
                clmnPersonnelSingleDateFeaturesNameDataField = "WorkGroup.Name";
                clmnPersonnelSingleDateFeaturesNameHeadingsText = this.GetLocalResourceObject("clmnWorkGroupName_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures").ToString();
                break;
            case Caller.CalculationRangesGroups:
                clmnPersonnelSingleDateFeaturesIDDataField = "CalcDateRangeGroup.ID";
                clmnPersonnelSingleDateFeaturesNameDataField = "CalcDateRangeGroup.Name";
                clmnPersonnelSingleDateFeaturesNameHeadingsText = this.GetLocalResourceObject("clmnCalculationRangeName_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures").ToString();
                break;
        }
        this.GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.Levels[0].Columns[1].DataField = clmnPersonnelSingleDateFeaturesIDDataField;
        this.GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.Levels[0].Columns[2].DataField = clmnPersonnelSingleDateFeaturesNameDataField;
        this.GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.Levels[0].Columns[2].HeadingText = clmnPersonnelSingleDateFeaturesNameHeadingsText;
    }

    private void ViewCurrentLangCalendars_PersonnelSingleDateFeatures()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpFromDate_PersonnelSingleDateFeatures.Visible = true;
                break;
            case "en-US":
                this.Container_gdpFromDate_PersonnelSingleDateFeatures.Visible = true;
                break;
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


    private void SetCurrentDate_PersonnelSingleDateFeatures()
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
        this.hfCurrentDate_PersonnelSingleDateFeatures.Value = strCurrentDate;
    }


    protected void CallBack_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures_onCallBack(object senfder, CallBackEventArgs e)
    {
        Caller caller = (Caller)Enum.Parse(typeof(Caller), this.StringBuilder.CreateString(e.Parameters[0]));
        this.CustomizeGridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures(caller);
        this.Fill_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures(caller, decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1])));
        this.GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.RenderControl(e.Output);
        this.ErrorHiddenField_PersonnelSingleDateFeatures.RenderControl(e.Output);
    }

    private void Fill_GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures(Caller caller, decimal PersonID)
    {
        string[] retMessage = new string[4];

        this.InitializeCulture();

        try
        {
            switch (caller)
            {
                case Caller.WorkGroups:
                    IList<AssignWorkGroup> PersonnelWorkGroupsList = this.PersonnelWorkGroupBusiness.GetAll(PersonID);
                    this.GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.DataSource = PersonnelWorkGroupsList;
                    break;
                case Caller.CalculationRangesGroups:
                    IList<PersonRangeAssignment> PersonnelCalculationRangeGroupsList = this.PersonnelCalculationRangeBusiness.GetAll(PersonID);
                    this.GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.DataSource = PersonnelCalculationRangeGroupsList;
                    break;
            }
            this.GridPersonnelSingleDateFeatures_PersonnelSingleDateFeatures.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_PersonnelSingleDateFeatures.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_PersonnelSingleDateFeatures.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_PersonnelSingleDateFeatures.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBackcmbSingleDateFeatures_PersonnelSingleDateFeatures_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbSingleDateFeatures_PersonnelSingleDateFeatures.Dispose();
        this.Fill_cmbSingleDateFeatures_PersonnelSingleDateFeatures((Caller)Enum.Parse(typeof(Caller), this.StringBuilder.CreateString(e.Parameter)));
        this.cmbSingleDateFeatures_PersonnelSingleDateFeatures.Enabled = true;
        this.ErrorHiddenField_PersonnelSingleDateFeature_PersonnelSingleDateFeatures.RenderControl(e.Output);
        this.cmbSingleDateFeatures_PersonnelSingleDateFeatures.RenderControl(e.Output);
    }

    private void Fill_cmbSingleDateFeatures_PersonnelSingleDateFeatures(Caller caller)
    {
        string[] retMessage = new string[4];

        this.InitializeCulture();
        try
        {
            switch (caller)
            {
                case Caller.WorkGroups:
                    IList<WorkGroup> WorkGroupsList = this.PersonnelWorkGroupBusiness.GetAllWorkGroup();
                    this.cmbSingleDateFeatures_PersonnelSingleDateFeatures.DataSource = WorkGroupsList;
                    break;
                case Caller.CalculationRangesGroups:
                    IList<CalculationRangeGroup> CalculationRangeGroupsList = this.PersonnelCalculationRangeBusiness.GetAllCalculationRangeGroup();
                    this.cmbSingleDateFeatures_PersonnelSingleDateFeatures.DataSource = CalculationRangeGroupsList;
                    break;
            }
            this.cmbSingleDateFeatures_PersonnelSingleDateFeatures.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_PersonnelSingleDateFeature_PersonnelSingleDateFeatures.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_PersonnelSingleDateFeature_PersonnelSingleDateFeatures.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_PersonnelSingleDateFeature_PersonnelSingleDateFeatures.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdatePersonnelSingleDateFeature_PersonnelSingleDateFeaturesPage", "UpdatePersonnelSingleDateFeature_PersonnelSingleDateFeaturesPage_onCallBack", null, null)]
    public string[] UpdatePersonnelSingleDateFeature_PersonnelSingleDateFeaturesPage(string caller, string state, string SelectedPersonnelSingleDateFeatureID, string PersonnelID, string SingleDateFeatureID, string SingleDateFeatureName, string FromDate)
    {
        this.InitializeCulture();

        string[] retMessage = new string[5];

        Caller PersonnelSingleDateFeatureCaller = (Caller)Enum.Parse(typeof(Caller), this.StringBuilder.CreateString(caller));
        decimal PersonnelSingleDateFeatureID = 0;
        decimal selectedPersonnelSingleDateFeatureID = decimal.Parse(this.StringBuilder.CreateString(SelectedPersonnelSingleDateFeatureID));
        decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID));
        decimal singleDateFeatureID = decimal.Parse(this.StringBuilder.CreateString(SingleDateFeatureID));
        SingleDateFeatureName = this.StringBuilder.CreateString(SingleDateFeatureName);
        FromDate = this.StringBuilder.CreateString(FromDate);
        string CurrentActiveSingleDateFeatureName = string.Empty;

        string NoItemSelectedforEditMessage = string.Empty;
        string NoItemSelectedforDeleteMessage = string.Empty;
        switch (PersonnelSingleDateFeatureCaller)
        {
            case Caller.WorkGroups:
                NoItemSelectedforEditMessage = this.GetLocalResourceObject("NoWorkGroupPersonnelSingleDateFeatureselectedforEdit").ToString();
                NoItemSelectedforDeleteMessage = this.GetLocalResourceObject("NoWorkGroupPersonnelSingleDateFeatureselectedforDelete").ToString();
                break;
            case Caller.CalculationRangesGroups:
                NoItemSelectedforEditMessage = this.GetLocalResourceObject("NoCalculationRangePersonnelSingleDateFeatureselectedforEdit").ToString();
                NoItemSelectedforDeleteMessage = this.GetLocalResourceObject("NoCalculationRangePersonnelSingleDateFeatureselectedforDelete").ToString();
                break;
        }

        UIActionType uam = UIActionType.ADD;
        try
        {
            switch (this.StringBuilder.CreateString(state))
            {
                case "Add":
                    uam = UIActionType.ADD;
                    break;
                case "Edit":
                    uam = UIActionType.EDIT;
                    if (selectedPersonnelSingleDateFeatureID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(NoItemSelectedforEditMessage), retMessage);
                        return retMessage;
                    }
                    break;
                case "Delete":
                    uam = UIActionType.DELETE;
                    if (selectedPersonnelSingleDateFeatureID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(NoItemSelectedforDeleteMessage), retMessage);
                        return retMessage;
                    }
                    break;
                default:
                    break;
            }

            switch (PersonnelSingleDateFeatureCaller)
            {
                case Caller.WorkGroups:
                    AssignWorkGroup personnelWorkGroup = new AssignWorkGroup();
                    personnelWorkGroup.ID = selectedPersonnelSingleDateFeatureID;
                    if (uam != UIActionType.DELETE)
                    {
                        Person person = new Person();
                        person.ID = personnelID;
                        personnelWorkGroup.Person = person;
                        WorkGroup workGroup = new WorkGroup();
                        workGroup.ID = singleDateFeatureID;
                        workGroup.Name = SingleDateFeatureName;
                        personnelWorkGroup.WorkGroup = workGroup;
                        personnelWorkGroup.UIFromDate = FromDate;
                    }
                    PersonnelSingleDateFeatureID = this.PersonnelWorkGroupBusiness.SaveChanges(personnelWorkGroup, uam);
                    CurrentActiveSingleDateFeatureName = this.PersonBusiness.GetCurrentActiveWorkGroup(personnelID);
                    break;
                case Caller.CalculationRangesGroups:
                    PersonRangeAssignment PersonnelCalculationRange = new PersonRangeAssignment();
                    PersonnelCalculationRange.ID = selectedPersonnelSingleDateFeatureID;
                    if (uam != UIActionType.DELETE)
                    {
                        Person person = new Person();
                        person.ID = personnelID;
                        PersonnelCalculationRange.Person = person;
                        CalculationRangeGroup calculationRangeGroup = new CalculationRangeGroup();
                        calculationRangeGroup.ID = singleDateFeatureID;
                        calculationRangeGroup.Name = SingleDateFeatureName;
                        PersonnelCalculationRange.CalcDateRangeGroup = calculationRangeGroup;
                        PersonnelCalculationRange.UIFromDate = FromDate;
                    }
                    PersonnelSingleDateFeatureID = this.PersonnelCalculationRangeBusiness.SaveChanges(PersonnelCalculationRange, uam);
                    CurrentActiveSingleDateFeatureName = this.PersonBusiness.GetCurrentActiveDateRange(personnelID);
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
            retMessage[3] = PersonnelSingleDateFeatureID.ToString();
            retMessage[4] = CurrentActiveSingleDateFeatureName;
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