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
using GTS.Clock.Business.UI;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class UnderManagementPersonnelExeptionAccessCreation : GTSBasePage
    {
        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
            }
        }

        enum Scripts
        {
            UnderManagementPersonnelExeptionAccessCreation_onPageLoad,
            DialogUnderManagementPersonnelExeptionAccessCreation_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            Page UnderManagementPersonnelExeptionAccessCreationPage = this;
            Ajax.Utility.GenerateMethodScripts(UnderManagementPersonnelExeptionAccessCreationPage);
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


        [Ajax.AjaxMethod("GetBoxesHeaders_UnderManagementPersonnelExeptionAccessCreationPage", "GetBoxesHeaders_UnderManagementPersonnelExeptionAccessCreationPage_onCallBack", null, null)]
        public string[] GetBoxesHeaders_UnderManagementPersonnelExeptionAccessCreationPage()
        {
            this.InitializeCulture();
            string[] retMessage = new string[3];
            retMessage[0] = GetLocalResourceObject("Title_DialogUnderManagementPersonnelExeptionAccessCreation").ToString();
            retMessage[1] = GetLocalResourceObject("header_UnderManagemetPersonnelBox_UnderManagementPersonnelExeptionAccessCreation").ToString();
            retMessage[2] = GetLocalResourceObject("header_AccessLevelBox_UnderManagementPersonnelExeptionAccessCreation").ToString();
            return retMessage;
        }

    }
}