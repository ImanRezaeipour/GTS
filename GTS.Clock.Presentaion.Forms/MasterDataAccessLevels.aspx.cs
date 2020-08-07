using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System.Web.Script.Serialization;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Exceptions;

public partial class MasterDataAccessLevels : GTSBasePage
{
    public BDataAccess DataAccessBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BDataAccess>();
        }
    }

    public StringGenerator StringBuilder
    {
        get
        {
            return new StringGenerator();
        }
    }

    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
        }
    }

    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    internal class ObjDataAccessLevel
    {
        public string Key { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
    }

    enum Scripts
    {
        Alert_Box,
        MasterDataAccessLevels_onPageLoad,
        DialogMasterDataAccessLevels_Operations,
        HelpForm_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        this.CheckMasterDataAccessLevelsLoadAccess_MasterDataAccessLevels();
    }

    private void CheckMasterDataAccessLevelsLoadAccess_MasterDataAccessLevels()
    {
        string[] retMessage = new string[4];
        try
        {
            this.DataAccessBusiness.CheckMasterDataAccessLevelsLoadAccess();
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        } 
    }

    protected override void InitializeCulture()
    {
        this.SetCurrentCultureResObjs(LangProv.GetCurrentLanguage());
        base.InitializeCulture();
    }

    private void SetCurrentCultureResObjs(string LangID)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
    }

    protected void CallBack_cmbDataAccessLevels_MasterDataAccessLevels_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbDataAccessLevels_MasterDataAccessLevels.Dispose();
        this.Fill_cmbDataAccessLevels_MasterDataAccessLevels();
        this.ErrorHiddenField_DataAccessLevels.RenderControl(e.Output);
        this.cmbDataAccessLevels_MasterDataAccessLevels.RenderControl(e.Output);
    }

    private void Fill_cmbDataAccessLevels_MasterDataAccessLevels()
    {
        string[] retMessage = new string[4];
        try
        {
            foreach (DataAccessParts DataAccessPartsItem in Enum.GetValues(typeof(DataAccessParts)))
            {
                ComboBoxItem cmbItemDataAccessPartsItem = new ComboBoxItem(GetLocalResourceObject(DataAccessPartsItem.ToString()).ToString());
                ObjDataAccessLevel objDataAccessLevel = new ObjDataAccessLevel();
                objDataAccessLevel.Key = DataAccessPartsItem.ToString();
                objDataAccessLevel.Source = GetLocalResourceObject(DataAccessPartsItem.ToString() + "Source").ToString();
                objDataAccessLevel.Target = GetLocalResourceObject(DataAccessPartsItem.ToString() + "Target").ToString();
                cmbItemDataAccessPartsItem.Value = this.JsSerializer.Serialize(objDataAccessLevel);
                this.cmbDataAccessLevels_MasterDataAccessLevels.Items.Add(cmbItemDataAccessPartsItem);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_DataAccessLevels.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_DataAccessLevels.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_DataAccessLevels.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
}