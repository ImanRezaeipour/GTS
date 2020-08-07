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
using GTS.Clock.Business.BoxService;
using GTS.Clock.Model.BoxService;
using GTS.Clock.Model.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;

public partial class PublicNews : GTSBasePage
{
    public BMainPageBox PublicNewsBusiness
    {
        get
        {
            return new BMainPageBox();
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
        PublicNews_onPageLoad,
        PublicNews_Operations,
        Alert_Box
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        this.Fill_rotrPublicNews_PublicNews();
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

    private void Fill_rotrPublicNews_PublicNews()
    {
        string[] retMessage = new string[4];
        try
        {
            IList<PublicMessage> PublicNewsList = this.PublicNewsBusiness.GetPublicMessages();
            this.bulletedListPublicNews_PublicNews.DataSource = PublicNewsList;
            this.bulletedListPublicNews_PublicNews.DataBind();
            //this.rotrPublicNews_PublicNews.DataSource = PublicNewsList;
            //this.rotrPublicNews_PublicNews.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_PublicNews.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_PublicNews.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_PublicNews.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

}