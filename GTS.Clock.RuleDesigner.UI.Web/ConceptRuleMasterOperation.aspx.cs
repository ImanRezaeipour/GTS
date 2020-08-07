using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using GTS.Business;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Compiler;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.RuleDesigner.UI.Web;
using GTS.Clock.RuleDesigner.UI.Web.Classes;
using Microsoft.CSharp;

namespace GTS.Clock.RuleDesigner.UI.Web
{
    public partial class ConceptRuleMasterOperation : GTSBasePage
    {
        public StringGenerator StringBuilder;
        public ExceptionHandler ExceptionHandler;
        
        public BWorkedTime MonthlyOperationBusiness
        {
            get
            {
                return new BWorkedTime();
                //return BusinessHelper.GetBusinessInstance<BWorkedTime>();
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
            ConceptRuleMasterOperation_onPageLoad,
            tbConceptRuleMasterOperation_TabStripMenu_Operation,
            Alert_Box,
            HelpForm_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.GenerateMethodScripts(this);
            RefererValidationProvider.CheckReferer();
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

        [Ajax.AjaxMethod("CompileAndMakeDll_ConceptRuleMasterOperation", "CompileAndMakeDll_ConceptRuleMasterOperation_onCallBack", null, null)]
        public string[] CompileAndMakeDll_ConceptRuleMasterOperation()
        {
            var retMessage = new string[3];

            try
            {
                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;

                var concatMessages = new CsCompiler().GenerateAndCompile();

                if (!string.IsNullOrEmpty(concatMessages))
                {
                    retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(concatMessages), retMessage);
                    return retMessage;
                }

                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                //retMessage[3] = parentId.ToString();

                return retMessage;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                return retMessage;
            }
            catch (UIBaseException ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                return retMessage;
            }
            catch (Exception ex)
            {
                retMessage = this.ExceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                return retMessage;
            }
        }



    }
}
