using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using System.Data;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;

namespace GTS.Clock.Presentaion.WebForms
{

    public partial class UnderManagementPersonnelExeptionAccessView : GTSBasePage
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
            iFrameUnderManagementPersonnelExeptionAccessView_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            Page UnderManagementPersonnelExeptionAccessViewPage = this;
            Ajax.Utility.GenerateMethodScripts(UnderManagementPersonnelExeptionAccessViewPage);

            int PageIndex_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView = 0;
            if (Session["PageIndex_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessViewPage"] != null)
                PageIndex_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView = (int)Session["PageIndex_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessViewPage"];
            this.Fill_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView(PageIndex_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView);

            this.GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView.DataBind();

            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));

            this.GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView_NeedRebind);
            this.GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView.NeedDataSource += new ComponentArt.Web.UI.Grid.NeedDataSourceEventHandler(GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView_NeedDataSource);
            this.GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView.NeedChildDataSource += new ComponentArt.Web.UI.Grid.NeedChildDataSourceEventHandler(GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView_NeedChildDataSource);
        }

        private void Fill_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView(int PageIndex)
        {
            string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
            conStr += Server.MapPath("~/common/db/demo.mdb");
            System.Data.OleDb.OleDbConnection srcDB = new System.Data.OleDb.OleDbConnection(conStr);
            srcDB.Open();

            string sql = string.Empty;
            switch (PageIndex)
            {
                case 0:
                    sql = "SELECT top 10 * FROM MasterUnderManagementPersonnelExeptionAccessView";
                    break;
                case 1:
                    sql = "SELECT * FROM MasterUnderManagementPersonnelExeptionAccessView where id = 14";
                    break;
            }

            System.Data.OleDb.OleDbDataAdapter daSrc = new System.Data.OleDb.OleDbDataAdapter(sql, srcDB);
            DataTable oTable = new DataTable();
            daSrc.Fill(oTable);

            srcDB.Close();

            this.GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView.DataSource = oTable;
        }

        protected void CallBack_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView.RenderControl(e.Output);
        }

        void GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView_NeedChildDataSource(object sender, ComponentArt.Web.UI.GridNeedChildDataSourceEventArgs e)
        {
            string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
            conStr += Server.MapPath("~/common/db/demo.mdb");
            System.Data.OleDb.OleDbConnection srcDB = new System.Data.OleDb.OleDbConnection(conStr);
            srcDB.Open();

            if (e.Item.Level == 0)
            {
                string sql = "SELECT * FROM DetailedUnderManagementPersonnelExeptionAccessView WHERE MasterUnderManagementPersonnelExeptionID = " + e.Item["ID"];
                System.Data.OleDb.OleDbDataAdapter daSrc = new System.Data.OleDb.OleDbDataAdapter(sql, srcDB);
                DataTable oTable = new DataTable();
                daSrc.Fill(oTable);

                e.DataSource = oTable;
            }
            srcDB.Close();
        }

        void GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView_NeedDataSource(object sender, EventArgs e)
        {
            int PageIndex_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView = 0;
            if (Session["PageIndex_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessViewPage"] != null)
                PageIndex_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView = (int)Session["PageIndex_GridUnderManagementPersonnelExeptionAccessView_MasterUnderManagementPersonnelExeptionAccessViewPage"];
            this.Fill_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView(PageIndex_GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView);
        }

        void GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView_NeedRebind(object sender, EventArgs e)
        {
            this.GridUnderManagementPersonnelExeptionAccessView_UnderManagementPersonnelExeptionAccessView.DataBind();
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

    }
}