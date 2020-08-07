using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using System.Web.Script.Serialization;
using GTS.Clock.Business;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.RequestFlow;
using System.Threading;
using System.Globalization;

public partial class Operators : GTSBasePage
{
    public BOperator OperatorBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BOperator>();
        }
    }

    public ISearchPerson PersonnelBusiness
    {
        get
        {
            return (ISearchPerson)(new BPerson());
        }
    }

    public AdvancedPersonnelSearchProvider APSProv
    {
        get
        {
            return new AdvancedPersonnelSearchProvider();
        }
    }

    public JavaScriptSerializer JsSeializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    internal class PersonnelDetails
    {
        public string ID { get; set; }
        public string OrganizationPostID { get; set; }
        public string OrganizationPostName { get; set; }
    }

    public enum LoadState
    {
        Normal,
        Search,
        AdvancedSearch
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
        Operators_onPageLoad,
        DialogOperators_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_cmbPersonnel_Operators.IsCallback && !CallBack_GridOperators_Operators.IsCallback)
        {
            Page OperatorsPage = this;
            Ajax.Utility.GenerateMethodScripts(OperatorsPage);

            this.SetPersonnelPageSize_cmbPersonnel_Operators();
            this.SetPersonnelPageCount_cmbPersonnel_Operators(LoadState.Normal, this.cmbPersonnel_Operators.DropDownPageSize, string.Empty);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckOperatorsLoadAccess_Operators();
        }
    }

    private void CheckOperatorsLoadAccess_Operators()
    {
        string[] retMessage = new string[4];
        try
        {
            this.OperatorBusiness.CheckOperatorsLoadAccess();
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

    private void SetPersonnelPageSize_cmbPersonnel_Operators()
    {
        this.hfPersonnelPageSize_Operators.Value = this.cmbPersonnel_Operators.DropDownPageSize.ToString();
    }

    private void SetPersonnelPageCount_cmbPersonnel_Operators(LoadState Ls, int pageSize, string SearchTerm)
    {
        string[] retMessage = new string[4];
        int PersonnelCount = 0;
        try
        {
            switch (Ls)
            {
                case LoadState.Normal:
                    PersonnelCount = this.PersonnelBusiness.GetPersonCount();
                    break;
                case LoadState.Search:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInQuickSearchCount(SearchTerm);
                    break;
                case LoadState.AdvancedSearch:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm));
                    break;
                default:
                    break;
            }
            this.hfPersonnelPageCount_Operators.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_Operators.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_Operators.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_Operators.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbPersonnel_Operators_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbPersonnel_Operators.Dispose();
        this.SetPersonnelPageCount_cmbPersonnel_Operators((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), this.StringBuilder.CreateString(e.Parameters[3]));
        this.Fill_cmbPersonnel_Operators((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), this.StringBuilder.CreateString(e.Parameters[3]));
        this.hfPersonnelPageCount_Operators.RenderControl(e.Output);
        this.ErrorHiddenField_Personnel_Operators.RenderControl(e.Output);
        this.cmbPersonnel_Operators.RenderControl(e.Output);
    }

    private void Fill_cmbPersonnel_Operators(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Person> PersonnelList = null;
            switch (Ls)
            {
                case LoadState.Normal:
                    PersonnelList = this.PersonnelBusiness.GetAllPerson(pageIndex, pageSize);
                    break;
                case LoadState.Search:
                    PersonnelList = this.PersonnelBusiness.QuickSearchByPage(pageIndex, pageSize, SearchTerm);
                    break;
                case LoadState.AdvancedSearch:
                    PersonnelList = this.PersonnelBusiness.GetPersonInAdvanceSearch(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize);
                    break;
            }
            foreach (Person personItem in PersonnelList)
            {
                ComboBoxItem personCmbItem = new ComboBoxItem(personItem.FirstName + " " + personItem.LastName);
                personCmbItem["BarCode"] = personItem.BarCode;
                personCmbItem["CardNum"] = personItem.CardNum;
                PersonnelDetails personnelDetails = new PersonnelDetails();
                personnelDetails.ID = personItem.ID.ToString();
                personnelDetails.OrganizationPostID = personItem.OrganizationUnit.ID.ToString();
                personnelDetails.OrganizationPostName = personItem.OrganizationUnit.Name;
                personCmbItem.Value = this.JsSeializer.Serialize(personnelDetails);
                cmbPersonnel_Operators.Items.Add(personCmbItem);
            }
            this.cmbPersonnel_Operators.Enabled = true;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_Operators.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_Operators.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_Operators.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_GridOperators_Operators_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_GridOperators_Operators(decimal.Parse(this.StringBuilder.CreateString(e.Parameter)));
        this.ErrorHiddenField_Operators.RenderControl(e.Output);
        this.GridOperators_Operators.RenderControl(e.Output);
    }

    private void Fill_GridOperators_Operators(decimal FlowID)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Operator> OperatorsList = this.OperatorBusiness.GetAllByFlowId(FlowID);
            this.GridOperators_Operators.DataSource = OperatorsList;
            this.GridOperators_Operators.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Operators.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Operators.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Operators.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateOperator_OperatorsPage", "UpdateOperator_OperatorsPage_onCallBack", null, null)]
    public string[] UpdateOperator_OperatorsPage(string state, string SelectedOperatorID, string FlowID, string IsActive, string PersonnelID, string Description)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal OperatorID = 0;
            decimal selectedOperatorID = decimal.Parse(this.StringBuilder.CreateString(SelectedOperatorID));
            decimal flowID = decimal.Parse(this.StringBuilder.CreateString(FlowID));
            bool isActive = bool.Parse(this.StringBuilder.CreateString(IsActive));
            decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID));
            Description = this.StringBuilder.CreateString(Description);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            Operator flowOperator = new Operator();
            flowOperator.ID = selectedOperatorID;
            if (uam != UIActionType.DELETE)
            {
                flowOperator.Active = isActive;
                flowOperator.Description = Description;
                Flow flow = new Flow();
                flow.ID = flowID;
                flowOperator.Flow = flow;
                Person personnel = new Person();
                personnel.ID = personnelID;
                flowOperator.Person = personnel;
            }

            switch (uam)
            {
                case UIActionType.ADD:
                    OperatorID = this.OperatorBusiness.InsertOperator(flowOperator, uam);
                    break;
                case UIActionType.EDIT:
                    if (selectedOperatorID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoOperatorSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    break;
                case UIActionType.DELETE:
                    if (selectedOperatorID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoOperatorSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    OperatorID = this.OperatorBusiness.DeleteOperator(flowOperator, uam);
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
            retMessage[3] = OperatorID.ToString();
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