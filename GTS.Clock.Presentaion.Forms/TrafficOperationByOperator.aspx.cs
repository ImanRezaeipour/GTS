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
    public partial class TrafficOperationByOperator : GTSBasePage
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
            TrafficOperationByOperator_onPageLoad,
            tbTrafficOperationByOperator_TabStripMenus_Operations,
            DropDownDive,
            Alert_Box,
            HelpForm_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbPersonnel_TrafficOperationByOperator.CausedCallback && !CallBack_GridTrafficDetails_TrafficOperationByOperator.CausedCallback)
            {
                Page TrafficOperationByOperatorPage = this;
                Ajax.Utility.GenerateMethodScripts(TrafficOperationByOperatorPage);

                this.ViewCurrentLangCalendars_TrafficOperationByOperator();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void ViewCurrentLangCalendars_TrafficOperationByOperator()
        {
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    this.Container_pdpFromDate_TrafficOperationByOperator.Visible = true;
                    this.Container_pdpToDate_TrafficOperationByOperator.Visible = true;
                    break;
                case "en-US":
                    this.Container_gdpFromDate_TrafficOperationByOperator.Visible = true;
                    this.Container_gdpToDate_TrafficOperationByOperator.Visible = true;
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

        [Ajax.AjaxMethod("GetBoxesHeaders_TrafficOperationByOperatorPage", "GetBoxesHeaders_TrafficOperationByOperatorPage_onCallBack", null, null)]
        public string[] GetBoxesHeaders_TrafficOperationByOperatorPage()
        {
            this.InitializeCulture();
            string[] retMessage = new string[4];
            retMessage[0] = GetLocalResourceObject("header_PersonnelSearchBox_TrafficOperationByOperator").ToString();
            retMessage[1] = GetLocalResourceObject("header_TrafficDetails_TrafficOperationByOperator").ToString();
            retMessage[2] = GetLocalResourceObject("clmnName_cmbPersonnel_TrafficOperationByOperator").ToString();
            retMessage[3] = GetLocalResourceObject("clmnBarCode_cmbPersonnel_TrafficOperationByOperator").ToString();
            return retMessage;
        }



        protected void CallBack_cmbPersonnel_TrafficOperationByOperator_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
        }

        protected void CallBack_GridTrafficDetails_TrafficOperationByOperator_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
        }



    }
}