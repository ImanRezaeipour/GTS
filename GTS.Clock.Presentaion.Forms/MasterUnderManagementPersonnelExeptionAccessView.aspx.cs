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
    public partial class MasterUnderManagementPersonnelExeptionAccessView : GTSBasePage
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
            MasterUnderManagementPersonnelExeptionAccessView_onPageLoad,
            DialogUnderManagementPersonnelExeptionAccessView_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            Page MasterUnderManagementPersonnelExeptionAccessViewPage = this;
            Ajax.Utility.GenerateMethodScripts(MasterUnderManagementPersonnelExeptionAccessViewPage);
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

        [Ajax.AjaxMethod("GetBoxesHeaders_MasterUnderManagementPersonnelExeptionAccessViewPage", "GetBoxesHeaders_MasterUnderManagementPersonnelExeptionAccessViewPage_onCallBack", null, null)]
        public string[] GetBoxesHeaders_MasterUnderManagementPersonnelExeptionAccessViewPage()
        {
            this.InitializeCulture();
            string[] retMessage = new string[2];
            retMessage[0] = GetLocalResourceObject("Title_DialogUnderManagementPersonnelExeptionAccessView").ToString();
            retMessage[1] = GetLocalResourceObject("header_BoxUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView").ToString();
            return retMessage;
        }

        [Ajax.AjaxMethod("SetPageIndex_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessViewPage", "SetPageIndex_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessView_onCallBack", null, null)]
        public bool SetPageIndex_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessViewPage(string PageIndex)
        {
            Session["PageIndex_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessViewPage"] = Convert.ToInt32(PageIndex);
            return true;
        }


    }
}