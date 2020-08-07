using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using ComponentArt.Web.UI;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;

public partial class Corporations : GTSBasePage
{
    public BCorporation CorporationsBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BCorporation>();
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
        Corporations_onPageLoad,
        tbCorporations_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridCorporations_Corporations.IsCallback)
        {
            Page CorporationsPage = this;
            Ajax.Utility.GenerateMethodScripts(CorporationsPage);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckCorporationsLoadAccess_Corporations();
        }
    }

    private void CheckCorporationsLoadAccess_Corporations()
    {
        string[] retMessage = new string[4];
        try
        {
            this.CorporationsBusiness.CheckCorporationsLoadAccess();
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

    protected void CallBack_GridCorporations_Corporations_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_GridCorporations_Corporations();
        this.GridCorporations_Corporations.RenderControl(e.Output);
        this.ErrorHiddenField_Corporations.RenderControl(e.Output);
    }

    private void Fill_GridCorporations_Corporations()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<Corporation> CorporationsList = this.CorporationsBusiness.GetAll();
            this.GridCorporations_Corporations.DataSource = CorporationsList;
            this.GridCorporations_Corporations.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Corporations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Corporations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Corporations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateCorporation_CorporationsPage", "UpdateCorporation_CorporationsPage_onCallBack", null, null)]
    public string[] UpdateCorporation_CorporationsPage(string state, string SelectedCorporationID, string Name, string Code, string EconomicCode, string Phone, string Fax, string Address, string Description)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal CorporationID = 0;
            decimal selectedCorporationID = decimal.Parse(this.StringBuilder.CreateString(SelectedCorporationID));
            Name = this.StringBuilder.CreateString(Name);
            Code = this.StringBuilder.CreateString(Code);
            EconomicCode = this.StringBuilder.CreateString(EconomicCode);
            Phone = this.StringBuilder.CreateString(Phone);
            Fax = this.StringBuilder.CreateString(Fax);
            Address = this.StringBuilder.CreateString(Address);
            Description = this.StringBuilder.CreateString(Description);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            Corporation Corporation = new Corporation();

            Corporation.ID = selectedCorporationID;
            if (uam != UIActionType.DELETE)
            {
                Corporation.Name = Name;
                Corporation.Code = Code;
                Corporation.EconomicCode = EconomicCode;
                Corporation.Phone = Phone;
                Corporation.Fax = Fax;
                Corporation.Address = Address;
                Corporation.Description = Description;
            }

            switch (uam)
            {
                case UIActionType.ADD:
                    CorporationID = this.CorporationsBusiness.InsertCorporation(Corporation, uam);
                    break;
                case UIActionType.EDIT:
                    if (selectedCorporationID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoCorporationSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    CorporationID = this.CorporationsBusiness.UpdateCorporation(Corporation, uam);
                    break;
                case UIActionType.DELETE:
                    if (selectedCorporationID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoCorporationSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    CorporationID = this.CorporationsBusiness.DeleteCorporation(Corporation, uam);
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
            retMessage[3] = CorporationID.ToString();
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