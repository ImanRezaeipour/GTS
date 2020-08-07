using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.BoxService;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.Exceptions.UI;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using GTS.Clock.Business.Security;
using GTS.Clock.Business;
using GTS.Clock.Model;

public partial class PersonnelInformationSummary : GTSBasePage
{
    public BPersonelInfoBoxService PersonnelInformationSummaryBusiness
    {
        get
        {
            return new BPersonelInfoBoxService();
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
            return BusinessHelper.GetBusinessInstance<BPerson>(new KeyValuePair<string, object>("sysLanguage", Slr), new KeyValuePair<string, object>("localLanguage", Llr));
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
        PersonnelInformationSummary_onPageLoad,
        PersonnelInformationSummary_Operations,
        Alert_Box
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        //this.GetCurrentPersonnel_PesonnelInformationSummary();
        this.GetCurrentPersonnelImage_PersonnelInformationSummary();
        this.Fill_InformationSummary_PesonnelInformationSummary();
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
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

    //private void GetCurrentPersonnel_PesonnelInformationSummary()
    //{
    //    this.CurrentPersonnelID_PersonnelInformationSummary.Value = this.PersonnelInformationSummaryBusiness.CurrentPersonId.ToString();
    //}

    private void GetCurrentPersonnelImage_PersonnelInformationSummary()
    {
        Person person = this.PersonBusiness.GetByID(this.PersonnelInformationSummaryBusiness.CurrentPersonId);
        this.hfCurrentPersonnelImage_PersonnelInformationSummary.Value = person.PersonDetail.Image;
    }

    private void Fill_InformationSummary_PesonnelInformationSummary()
    {
        string[] retMessage = new string[4];
        try
        {
            IList<PersonInfoProxy> PersonnelInformationSummaryList = this.PersonnelInformationSummaryBusiness.GetPersonInfo();

            this.PersonnelInformationSummaryContainer_PersonnelInformationSummary.Controls.Add(new LiteralControl("<table style='width:100%; font-family:Arial; font-size:small'>"));
            foreach (PersonInfoProxy PersonnelInformationSummaryItem in PersonnelInformationSummaryList)
            {
                this.PersonnelInformationSummaryContainer_PersonnelInformationSummary.Controls.Add(new LiteralControl("<tr><td style='width:50%' class='HeaderLabel'>" + PersonnelInformationSummaryItem.Title + "</td><td style='width:50%' class='HeaderLabel' align='center'>" + PersonnelInformationSummaryItem.Value + "</td></tr>"));
            }
            this.PersonnelInformationSummaryContainer_PersonnelInformationSummary.Controls.Add(new LiteralControl("</table>"));
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_PersonnelInformationSummary.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_PersonnelInformationSummary.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_PersonnelInformationSummary.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }

    }


}