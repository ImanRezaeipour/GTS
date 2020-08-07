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
    public partial class CollectiveTraffic : GTSBasePage
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
            CollectiveTraffic_onPageLoad,
            DialogCollectiveTraffic_Operations,
            Alert_Box,
            DropDownDive,
            HelpForm_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_GridRegisteredRequests_CollectiveTraffic.CausedCallback)
            {
                Page CollectiveTrafficPage = this.Page;
                Ajax.Utility.GenerateMethodScripts(CollectiveTrafficPage);

                this.ViewCurrentLangCalendars_CollectiveTraffic();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void ViewCurrentLangCalendars_CollectiveTraffic()
        {
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    this.Container_pdpFromDate_CollectiveTraffic.Visible = true;
                    this.Container_pdpToDate_CollectiveTraffic.Visible = true;
                    break;
                case "en-US":
                    this.Container_gdpFromDate_CollectiveTraffic.Visible = true;
                    this.Container_gdpToDate_CollectiveTraffic.Visible = true;
                    break;
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

        protected void CallBack_GridRegisteredRequests_CollectiveTraffic_OnCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
        }

        [Ajax.AjaxMethod("GetBoxesHeaders_CollectiveTrafficPage", "GetBoxesHeaders_CollectiveTrafficPage_onCallBack", null, null)]
        public string[] GetBoxesHeaders_CollectiveTrafficPage()
        {
            this.InitializeCulture();
            string[] retMessage = new string[2];
            retMessage[0] = GetLocalResourceObject("Title_DialogCollectiveTraffic").ToString();
            retMessage[1] = GetLocalResourceObject("header_RegisteredRequests_CollectiveTraffic").ToString();
            return retMessage;
        }

    }
}