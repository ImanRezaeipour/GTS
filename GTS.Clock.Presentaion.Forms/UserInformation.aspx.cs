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
using GTS.Clock.Business.Leave;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Exceptions;
using ComponentArt.Web.UI;

public partial class UserInformation : GTSBasePage
{
    public BUserInfo UserInformationBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BUserInfo>();
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

    enum LoadState
    {
        Personnel,
        Manager
    }

    enum CallerSchema
    {
        Grid,
        GanttChart
    }

    enum Scripts
    {
        UserInformation_onPageLoad,
        DialogUserInformation_Operations,
        Alert_Box
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        this.CheckUserInformationLoadAccess_UserInformation();
    }

    private void CheckUserInformationLoadAccess_UserInformation()
    {
        string[] retMessage = new string[4];
        try
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("CallerSchema") && HttpContext.Current.Request.QueryString.AllKeys.Contains("LoadState"))
            {
                CallerSchema callerSchema = (CallerSchema)Enum.Parse(typeof(CallerSchema), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["CallerSchema"]));
                LoadState LS = (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["LoadState"]));

                switch (LS)
                {
                    case LoadState.Personnel:
                        switch (callerSchema)
	                    {
                            case CallerSchema.Grid:
                                this.UserInformationBusiness.CheckUserInformationLoadAccess_onPersonnelLoadStateInGridSchema();
                                break;
                            case CallerSchema.GanttChart:
                                this.UserInformationBusiness.CheckUserInformationLoadAccess_onPersonnelLoadStateInGanttChartSchema();
                                break;
                        }
                        break;
                    case LoadState.Manager:
                        switch (callerSchema)
                        {
                            case CallerSchema.Grid:
                                this.UserInformationBusiness.CheckUserInformationLoadAccess_onManagerLoadStateInGridSchema();
                                break;
                            case CallerSchema.GanttChart:
                                this.UserInformationBusiness.CheckUserInformationLoadAccess_onManagerLoadStateInGanttChartSchema();
                                break;
                        }
                        break;
                }
            }
        }
        catch (BaseException ex)
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

    protected void CallBack_BListUserInformation_onCallback(object sender, CallBackEventArgs e)
    {
        this.GetUserInformation((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1])));
        this.ErrorHiddenField_UserInformation.RenderControl(e.Output);
        this.BListUserInformation_UserInformation.RenderControl(e.Output);
    }

    private void GetUserInformation(LoadState LS, decimal PersonnelID)
    {
        switch (LS)
        {
            case LoadState.Personnel:
                PersonnelID = BUser.CurrentUser.Person.ID;
                break;
            case LoadState.Manager:
                break;
        }
        this.CreateUserInfomationList(PersonnelID);
    }

    private void CreateUserInfomationList(decimal PersonnelID)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<string> UserInformationList = this.UserInformationBusiness.GetUserInfo(PersonnelID);
            for (int i = 0; i < UserInformationList.Count; i++)
            {
                ListItem listItemUserInformation = new ListItem(UserInformationList[i]);
                this.BListUserInformation_UserInformation.Items.Add(listItemUserInformation);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_UserInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_UserInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_UserInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

}