using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Rules;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Exceptions;

public partial class MasterCalculationRange : GTSBasePage
{
    public BDateRange CalculationRangeBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BDateRange>();
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
        MasterCalculationRange_onPageLod,
        tbMasterCalculationRange_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        Page MasterCalculationRangePage = this;
        Ajax.Utility.GenerateMethodScripts(MasterCalculationRangePage);

        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        this.CheckCalculationRangeLoadAccess_MasterCalculationRange();
    }

    private void CheckCalculationRangeLoadAccess_MasterCalculationRange()
    {
        string[] retMessage = new string[4];
        try
        {
            this.CalculationRangeBusiness.CheckCalculationRangeLoadAccess();
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

    protected void CallBack_GridCalculationRange_MasterCalculationRange_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_GridCalculationRange_MasterCalculationRange();
        this.GridCalculationRange_MasterCalculationRange.RenderControl(e.Output);
        this.ErrorHiddenField_MasterCalculationRange.RenderControl(e.Output);
    }

    private void Fill_GridCalculationRange_MasterCalculationRange()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<CalculationRangeGroup> calculationRangeList = this.CalculationRangeBusiness.GetAll();
            this.GridCalculationRange_MasterCalculationRange.DataSource = calculationRangeList;
            this.GridCalculationRange_MasterCalculationRange.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_MasterCalculationRange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_MasterCalculationRange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_MasterCalculationRange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }


    [Ajax.AjaxMethod("UpdateCalculationRange_MasterCalculationRangePage", "UpdateCalculationRange_MasterCalculationRangePage_onCallBack", null, null)]
    public string[] UpdateCalculationRange_MasterCalculationRangePage(string state, string SelectedCalculationRangeID)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal CalculationRangeID = 0;
            decimal selectedCalculationRangeID = decimal.Parse(this.StringBuilder.CreateString(SelectedCalculationRangeID));
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            CalculationRangeGroup calculationRange = new CalculationRangeGroup();
            calculationRange.ID = selectedCalculationRangeID;

            switch (uam)
            {
                case UIActionType.DELETE:
                    if (selectedCalculationRangeID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoCalculationRangeSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    CalculationRangeID = this.CalculationRangeBusiness.DeleteDateRange(calculationRange);
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {
                case UIActionType.DELETE:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
                default:
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            retMessage[3] = CalculationRangeID.ToString();
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

    [Ajax.AjaxMethod("CopyCalculationRange_MasterCalculationRangePage", "CopyCalculationRange_MasterCalculationRangePage_onCallBack", null, null)]
    public string[] CopyCalculationRange_MasterCalculationRangePage(string SelectedCalculationRangeID)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal CalculationRangeID = 0;
            decimal selectedCalculationRangeID = decimal.Parse(this.StringBuilder.CreateString(SelectedCalculationRangeID));
            if (selectedCalculationRangeID == 0)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoCalculationRangeSelectedforCopy").ToString()), retMessage);
                return retMessage;
            }
            CalculationRangeID = this.CalculationRangeBusiness.CopyDateRangeGroup(selectedCalculationRangeID);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("CopyComplete").ToString();
            retMessage[2] = "success";
            retMessage[3] = CalculationRangeID.ToString();

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