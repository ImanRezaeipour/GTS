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
using ComponentArt.Web.UI;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.UI;

public partial class Physicians : GTSBasePage
{
    public BDoctor PhysiciansBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BDoctor>();
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
        Physicians_onPageLoad,
        tbPhysicians_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridPhysicians_Physicians.IsCallback)
        {
            Page PhysiciansPage = this;
            Ajax.Utility.GenerateMethodScripts(PhysiciansPage);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckPhysiciansLoadAccess_Physicians();
        }
    }

    private void CheckPhysiciansLoadAccess_Physicians()
    {
        string[] retMessage = new string[4];
        try
        {
            this.PhysiciansBusiness.CheckPhysiciansLoadAccess();
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

    protected void CallBack_GridPhysicians_Physicians_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_GridPhysicians_Physicians();
        this.GridPhysicians_Physicians.RenderControl(e.Output);
        this.ErrorHiddenField_Physicians.RenderControl(e.Output);
    }

    private void Fill_GridPhysicians_Physicians()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<Doctor> physiciansList = this.PhysiciansBusiness.GetAll();
            this.GridPhysicians_Physicians.DataSource = physiciansList;
            this.GridPhysicians_Physicians.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Physicians.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Physicians.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Physicians.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdatePhysician_PhysiciansPage", "UpdatePhysician_PhysiciansPage_onCallBack", null, null)]
    public string[] UpdatePhysician_PhysiciansPage(string state, string SelectedPhysicianID, string FirstName, string LastName, string Proficiency, string MedicalAssociation, string Description)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal PhysicianID = 0;
            decimal selectedPhysicianID = decimal.Parse(this.StringBuilder.CreateString(SelectedPhysicianID));
            FirstName = this.StringBuilder.CreateString(FirstName);
            LastName = this.StringBuilder.CreateString(LastName);
            Proficiency = this.StringBuilder.CreateString(Proficiency);
            MedicalAssociation = this.StringBuilder.CreateString(MedicalAssociation);
            Description = this.StringBuilder.CreateString(Description);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            Doctor physician = new Doctor();
            physician.ID = selectedPhysicianID;
            if (uam != UIActionType.DELETE)
            {
                physician.FirstName = FirstName;
                physician.LastName = LastName;
                physician.Takhasos = Proficiency;
                physician.Nezampezaeshki = MedicalAssociation;
                physician.Description = Description;
            }

            switch (uam)
            {
                case UIActionType.ADD:
                    PhysicianID = this.PhysiciansBusiness.InsertPhyician(physician, uam);
                    break;
                case UIActionType.EDIT:
                    if (selectedPhysicianID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoPhysicianSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    PhysicianID = this.PhysiciansBusiness.UpdatePhyician(physician, uam);
                    break;
                case UIActionType.DELETE:
                    if (selectedPhysicianID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoPhysicianSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    PhysicianID = this.PhysiciansBusiness.DeletePhyician(physician, uam);
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
            retMessage[3] = PhysicianID.ToString();
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