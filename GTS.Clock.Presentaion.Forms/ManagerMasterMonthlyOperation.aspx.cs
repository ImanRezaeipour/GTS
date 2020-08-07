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
using ComponentArt.Web.UI;
using System.Resources;
using System.Collections;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Model.Charts;
using System.IO;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.GridSettings;
using GTS.Clock.Model.UI;
using System.Reflection;
using GTS.Clock.Business.UI;


namespace GTS.Clock.Presentaion.WebForms
{
    public partial class ManagerMasterMonthlyOperation : GTSBasePage
    {
        private string currentUser;
        public string CurrentUser
        {
            get
            {
                this.currentUser = "2244";
                return this.currentUser;
            }
        }

        enum LoadState
        {
            Normal,
            Search
        }

        public BWorkedTime MonthlyOperationBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BWorkedTime>();
            }
        }

        public BGridMonthlyOperationMasterSettings MonthlyOperationGridManagerSettingsBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BGridMonthlyOperationMasterSettings>();
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
            ManagerMasterMonthlyOperation_onPageLoad,
            tbManagerMasterMonthlyOperation_TabStripMenus_Operations,
            DropDownDive,
            Alert_Box,
            HelpForm_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!this.CallBack_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.IsCallback && !this.CallBack_GridSummarySettings_ManagerMasterMonthlyOperation.IsCallback && !this.CallBack_cmbFilterBy_ManagerMasterMonthlyOperation.IsCallback && !this.CallBack_cmbSortBy_ManagerMasterMonthlyOperation.IsCallback && !this.CallBack_trvDepartments_ManagerMasterMonthlyOperation.IsCallback)
            {
                Page ManagerMasterMonthlyOperationPage = this;
                Ajax.Utility.GenerateMethodScripts(ManagerMasterMonthlyOperationPage);

                this.CheckManagerMasterMonthlyOperationLoadAccess_ManagerMasterMonthlyOperation();
                this.Fill_cmbMonth_ManagerMasterMonthlyOperation();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
            this.SetMonthlyOperationSummary_ManagerMasterMonthlyOperation();
        }

        private void CheckManagerMasterMonthlyOperationLoadAccess_ManagerMasterMonthlyOperation()
        {
            string[] retMessage = new string[4];
            try
            {
                this.MonthlyOperationBusiness.CheckManagerMasterMonthlyOperationLoadAccess();
            }
            catch (BaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);   
            }
        }

        private void SetMonthlyOperationSummary_ManagerMasterMonthlyOperation()
        {
            this.hfMonthlyOperationSummaryPageSize_ManagerMasterMonthlyOperation.Value = this.GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.PageSize.ToString();
        }

        private void SetMonthlyOperationSummaryPageCount_ManagerMasterMonthlyOperation(LoadState Ls, decimal DepartmentID, GridSearchFields Gsf, string SearchTerm, int Month)
        {
            int UnderManagmentCount = 0;
            switch (Ls)
            {
                case LoadState.Normal:
                    if (DepartmentID != 0)
                        UnderManagmentCount = this.MonthlyOperationBusiness.GetUnderManagmentByDepartmentCount(Month, DepartmentID);
                    break;
                case LoadState.Search:
                    UnderManagmentCount = this.MonthlyOperationBusiness.GetUnderManagmentBySearchCount(Month, SearchTerm, Gsf);
                    break;
            }
            this.hfMonthlyOperationSummaryCount_ManagerMasterMonthlyOperation.Value = UnderManagmentCount.ToString();
            this.hfMonthlyOperationSummaryPageCount_ManagerMasterMonthlyOperation.Value = Utility.GetPageCount(UnderManagmentCount, this.GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.PageSize).ToString();
        }


        public void ColumnsRecreation_ManagerMasterMonthlyOperationPage()
        {
            try
            {
                //ManageExternalResources("ManagerMasterMonthlyOperation.aspx.fa.resx", "fa-IR");
                //ManageExternalResources("ManagerMasterMonthlyOperation.aspx.resx", "en-US");
            }
            catch
            { }
        }

        //private void ManageExternalResources(string fileName, string culture)
        //{
        //    string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
        //    conStr += Server.MapPath("~/common/db/demo.mdb");
        //    System.Data.OleDb.OleDbConnection srcDB = new System.Data.OleDb.OleDbConnection(conStr);
        //    srcDB.Open();
        //    string sql = "Select * From ConceptsReserveFields";
        //    System.Data.OleDb.OleDbDataAdapter daSrc = new System.Data.OleDb.OleDbDataAdapter(sql, srcDB);
        //    DataTable oTable = new DataTable();
        //    daSrc.Fill(oTable);
        //    srcDB.Close();


        //    string filePath = "App_LocalResources\\" + fileName;
        //    ResXResourceReader resxReader = new ResXResourceReader(Server.MapPath(filePath));
        //    ResXResourceWriter resxWriter = new ResXResourceWriter(Server.MapPath(filePath));
        //    foreach (DictionaryEntry de in resxReader)
        //    {
        //        string key = de.Key.ToString().Replace(".HeadingText", string.Empty);
        //        DataRow[] RowCol = (oTable.Select("OriginalName='" + key + "'"));
        //        if (RowCol.Length == 0)
        //            resxWriter.AddResource(de.Key.ToString(), de.Value.ToString());
        //    }
        //    foreach (DataRow dr in oTable.Rows)
        //    {
        //        string NewValue = string.Empty;
        //        switch (culture)
        //        {
        //            case "fa-IR":
        //                NewValue = dr["Fa_Name"].ToString();
        //                break;
        //            case "en-US":
        //                NewValue = dr["En_Name"].ToString();
        //                break;
        //        }

        //        resxWriter.AddResource(dr["OriginalName"].ToString() + ".HeadingText", NewValue);
        //    }
        //    resxReader.Close();
        //    resxWriter.Generate();
        //    resxWriter.Close();
        //}

        private void Fill_GridSummaryMonthlyOperation_ManagerMasterMonthlyOperation(LoadState Ls, decimal departmentID, int pageSize, int pageIndex, GridOrderFields Gof, GridOrderFieldType Goft, GridSearchFields Gsf, string SearchTerm, int Month)
        {
            IList<UnderManagementPerson> UnderManagementPersonnelList = null;
            this.InitializeCulture();
            switch (Ls)
            {
                case LoadState.Normal:
                    if (departmentID == 0)
                        return;
                    UnderManagementPersonnelList = this.MonthlyOperationBusiness.GetUnderManagmentByDepartment(Month, departmentID, pageIndex, pageSize, Gof, Goft);
                    break;
                case LoadState.Search:
                    UnderManagementPersonnelList = this.MonthlyOperationBusiness.GetUnderManagmentBySearch(Month, SearchTerm, Gsf, pageIndex, pageSize, Gof, Goft);
                    break;
            }
            this.GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.DataSource = UnderManagementPersonnelList;
            this.GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.DataBind();
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

        protected void CallBack_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.BuildGrid_SummaryMonthlyOperation(e);
        }

        protected void CallBack_GridSummarySettings_ManagerMasterMonthlyOperation_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            string[] retMessage = new string[4];
            try
            {
                Dictionary<string, string> MasterColArray = this.CreateRecievedColumnsArray_SummaryMonthlyOperation(e.Parameters[0], "Get");
                Dictionary<string, string> SettingsColArray = null;
                if (e.Parameters[2] != string.Empty)
                    SettingsColArray = this.CreateRecievedColumnsArray_SummaryMonthlyOperation(e.Parameters[2], "Set");

                MonthlyOperationGridMasterSettings monthlyOperationGridMasterSettings = this.GetVisibleColumns_GridSummarySettings_SummaryMonthlyOperation(MasterColArray, e.Parameters[1], SettingsColArray, decimal.Parse(this.StringBuilder.CreateString(e.Parameters[4])));

                if (bool.Parse(e.Parameters[3]))
                    this.ColumnsRecreation_ManagerMasterMonthlyOperationPage();

                switch (e.Parameters[1])
                {
                    case "Get":
                        this.GridSummarySettings_ManagerMasterMonthlyOperation.DataSource = this.CreateDs_SummaryMonthlyOperationGrid_SettingsCode(monthlyOperationGridMasterSettings, MasterColArray, e.Parameters[1]).Tables[0];
                        this.GridSummarySettings_ManagerMasterMonthlyOperation.DataBind();
                       this.hfCurrentSettingID_GridSummarySettings_ManagerMasterMonthlyOperation.Value = monthlyOperationGridMasterSettings.ID.ToString();
                        break;
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_GridSummarySettings_ManagerMasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_GridSummarySettings_ManagerMasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_GridSummarySettings_ManagerMasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }

            this.ErrorHiddenField_GridSummarySettings_ManagerMasterMonthlyOperation.RenderControl(e.Output);
            this.hfCurrentSettingID_GridSummarySettings_ManagerMasterMonthlyOperation.RenderControl(e.Output);
            this.GridSummarySettings_ManagerMasterMonthlyOperation.RenderControl(e.Output);
        }

        private void BuildGrid_SummaryMonthlyOperation(CallBackEventArgs e)
        {
            string[] retMessage = new string[4];
            try
            {
                this.SetGridColumnsSize_SummaryMonthlyOperation();
                this.SetVisibleColumns_SummaryMonthlyOperation();
                this.SetMonthlyOperationSummaryPageCount_ManagerMasterMonthlyOperation((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1])), (GridSearchFields)Enum.Parse(typeof(GridSearchFields), this.StringBuilder.CreateString(e.Parameters[6])), this.StringBuilder.CreateString(e.Parameters[7]), this.GetSysMonth_SummaryMonthlyOperation(this.StringBuilder.CreateString(e.Parameters[8])));
                this.Fill_GridSummaryMonthlyOperation_ManagerMasterMonthlyOperation((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])), (GridOrderFields)Enum.Parse(typeof(GridOrderFields), this.StringBuilder.CreateString(e.Parameters[4])), (GridOrderFieldType)Enum.Parse(typeof(GridOrderFieldType), this.StringBuilder.CreateString(e.Parameters[5])), (GridSearchFields)Enum.Parse(typeof(GridSearchFields), this.StringBuilder.CreateString(e.Parameters[6])), this.StringBuilder.CreateString(e.Parameters[7]), this.GetSysMonth_SummaryMonthlyOperation(this.StringBuilder.CreateString(e.Parameters[8])));
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MonthlyOperationSummary.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MonthlyOperationSummary.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MonthlyOperationSummary.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }

            this.hfMonthlyOperationSummaryCount_ManagerMasterMonthlyOperation.RenderControl(e.Output);
            this.hfMonthlyOperationSummaryPageCount_ManagerMasterMonthlyOperation.RenderControl(e.Output);
            this.GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.RenderControl(e.Output);
            this.ErrorHiddenField_MonthlyOperationSummary.RenderControl(e.Output);
        }

        private int GetSysMonth_SummaryMonthlyOperation(string month)
        {
            int Month = 0;
            DateTime currentDateTime = DateTime.Now;
            string SysLangID = this.LangProv.GetCurrentSysLanguage();
            switch (month)
            {
                case "NONE":
                    switch (SysLangID)
                    {
                        case "en-US":
                            Month = currentDateTime.Month;
                            break;
                        case "fa-IR":
                            PersianCalendar pCal = new PersianCalendar();
                            Month = pCal.GetMonth(currentDateTime);
                            break;
                    }
                    break;
                default:
                    Month = int.Parse(month);
                    break;
            }
            return Month;
        }

        private void SetGridColumnsSize_SummaryMonthlyOperation()
        {
            MonthlyOperationGridMasterGeneralSettings monthlyOperationGridMasterSettings = this.MonthlyOperationGridManagerSettingsBusiness.GetMonthlyOperationGridGeneralMasterSettings();
            GridColumnCollection Gcc = GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.Levels[0].Columns;
            for (int i = 1; i < Gcc.Count; i++)
            {
                PropertyInfo PInfo = typeof(MonthlyOperationGridMasterGeneralSettings).GetProperty(Gcc[i].DataField);
                if (PInfo != null)
                    Gcc[i].Width = (int)PInfo.GetValue(monthlyOperationGridMasterSettings, null);
            }
        }

        private void SetVisibleColumns_SummaryMonthlyOperation()
        {
            MonthlyOperationGridMasterSettings monthlyOperationGridMasterSettings = this.MonthlyOperationGridManagerSettingsBusiness.GetMonthlyOperationGridMasterSettings();
            GridColumnCollection Gcc = GridMonthlyOperationSummary_ManagerMasterMonthlyOperation.Levels[0].Columns;
            for (int i = 1; i < Gcc.Count; i++)
            {
                PropertyInfo PInfo = typeof(MonthlyOperationGridMasterSettings).GetProperty(Gcc[i].DataField);
                if (PInfo != null)
                    Gcc[i].Visible = (bool)PInfo.GetValue(monthlyOperationGridMasterSettings, null);
            }
        }

        private Dictionary<string, string> CreateRecievedColumnsArray_SummaryMonthlyOperation(string RecievedStr, string State)
        {
            string[] ColStrArray = RecievedStr.Split(new char[] { ':' });
            Dictionary<string, string> ColArray = new Dictionary<string, string>();
            string[] ColStr;
            foreach (string Col in ColStrArray)
            {
                switch (State)
                {
                    case "Get":
                        ColStr = Col.Split(new char[] { '%' });
                        ColArray.Add(this.StringBuilder.CreateString(ColStr[0]), this.StringBuilder.CreateString(ColStr[1]));
                        break;
                    case "Set":
                        ColStr = Col.Split(new char[] { '%' });
                        ColArray.Add(this.StringBuilder.CreateString(ColStr[0]), ColStr[1]);
                        break;
                }
            }
            return ColArray;
        }

        private MonthlyOperationGridMasterSettings GetVisibleColumns_GridSummarySettings_SummaryMonthlyOperation(Dictionary<string, string> MasterColArray, string State, Dictionary<string, string> SettingsColArray, decimal CurrentSettingID)
        {
            MonthlyOperationGridMasterSettings monthlyOperationGridMasterSettings = new MonthlyOperationGridMasterSettings();
            switch (State)
            {
                case "Get":
                    monthlyOperationGridMasterSettings = this.MonthlyOperationGridManagerSettingsBusiness.GetMonthlyOperationGridMasterSettings();
                    break;
                case "Set":
                    foreach (PropertyInfo pInfo in typeof(MonthlyOperationGridMasterSettings).GetProperties())
                    {
                        monthlyOperationGridMasterSettings.ID = CurrentSettingID;
                        if (MasterColArray.ContainsKey(pInfo.Name))
                            pInfo.SetValue(monthlyOperationGridMasterSettings, Boolean.Parse(SettingsColArray[MasterColArray[pInfo.Name]]), null);
                    }
                    this.MonthlyOperationGridManagerSettingsBusiness.SaveChanges(monthlyOperationGridMasterSettings, Business.UIActionType.EDIT);
                    break;
            }
            return monthlyOperationGridMasterSettings;
        }

        private DataSet CreateDs_SummaryMonthlyOperationGrid_SettingsCode(MonthlyOperationGridMasterSettings monthlyOperationGridMasterSettings, Dictionary<string, string> ColArray, string State)
        {
            DataSet dsSettings = new DataSet();
            DataTable dtSettings = new DataTable();
            DataColumn dcID = new DataColumn("ID", typeof(int));
            dcID.AutoIncrement = true;
            dcID.AutoIncrementSeed = 0;
            dcID.AutoIncrementStep = 1;
            DataColumn dcViewState = new DataColumn("ViewState", typeof(bool));
            DataColumn dcGridColumn = new DataColumn("GridColumn", typeof(string));
            dtSettings.Columns.Add(dcID);
            dtSettings.Columns.Add(dcViewState);
            dtSettings.Columns.Add(dcGridColumn);
            dsSettings.Tables.Add(dtSettings);

            foreach (PropertyInfo pInfo in typeof(MonthlyOperationGridMasterSettings).GetProperties())
            {
                bool ViewState = false;
                string GridColumn = string.Empty;
                switch (State)
                {
                    case "Get":
                        try
                        {
                            ViewState = (bool)pInfo.GetValue(monthlyOperationGridMasterSettings, null);
                            GridColumn = ColArray[pInfo.Name];
                            DataRow dr = dsSettings.Tables[0].NewRow();
                            dr["ViewState"] = ViewState;
                            dr["GridColumn"] = GridColumn;
                            dsSettings.Tables[0].Rows.Add(dr);
                        }
                        catch
                        { }
                        break;
                }
            }
            return dsSettings;
        }

        protected void CallBack_cmbFilterBy_ManagerMasterMonthlyOperation_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.cmbFilterBy_ManagerMasterMonthlyOperation.Dispose();
            this.Fill_cmbFilterBy_ManagerMasterMonthlyOperation(int.Parse(this.StringBuilder.CreateString(e.Parameters[1])));
            this.ErrorHiddenField_FilterBy_ManagerMasterMonthlyOperation.RenderControl(e.Output);
            this.cmbFilterBy_ManagerMasterMonthlyOperation.RenderControl(e.Output);
        }

        private void Fill_cmbFilterBy_ManagerMasterMonthlyOperation(int selectedIndex)
        {
            string[] retMessage = new string[4];
            try
            {
                foreach (string searchItem in Enum.GetNames(typeof(GridSearchFields)))
                {
                    ComboBoxItem cmbItemSearch = new ComboBoxItem(GetLocalResourceObject(searchItem).ToString());
                    cmbItemSearch.Value = searchItem;
                    cmbFilterBy_ManagerMasterMonthlyOperation.Items.Add(cmbItemSearch);
                }
                if (selectedIndex != -1)
                    this.cmbFilterBy_ManagerMasterMonthlyOperation.SelectedIndex = selectedIndex;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_FilterBy_ManagerMasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_FilterBy_ManagerMasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_FilterBy_ManagerMasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbSortBy_ManagerMasterMonthlyOperation_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.cmbSortBy_ManagerMasterMonthlyOperation.Dispose();
            this.Fill_cmbSortBy_ManagerMasterMonthlyOperation(int.Parse(this.StringBuilder.CreateString(e.Parameters[1])));
            this.ErrorHiddenField_SortBy_ManagerMasterMonthlyOperation.RenderControl(e.Output);
            this.cmbSortBy_ManagerMasterMonthlyOperation.RenderControl(e.Output);
        }

        private void Fill_cmbSortBy_ManagerMasterMonthlyOperation(int selectedIndex)
        {
            string[] retMessage = new string[4];
            try
            {
                foreach (string sortItem in Enum.GetNames(typeof(GridOrderFields)))
                {
                    ComboBoxItem cmbItemSort = new ComboBoxItem(GetLocalResourceObject(sortItem).ToString());
                    cmbItemSort.Value = sortItem;
                    cmbSortBy_ManagerMasterMonthlyOperation.Items.Add(cmbItemSort);
                }
                if (selectedIndex != -1)
                    this.cmbSortBy_ManagerMasterMonthlyOperation.SelectedIndex = selectedIndex;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_SortBy_ManagerMasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_SortBy_ManagerMasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_SortBy_ManagerMasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_trvDepartments_ManagerMasterMonthlyOperation_onCallBack(object sender, CallBackEventArgs e)
        {
            this.Fill_trvDepartments_ManagerMasterMonthlyOperation();
            this.ErrorHiddenField_Departments_ManagerMasterMonthlyOperation.RenderControl(e.Output);
            this.trvDepartments_ManagerMasterMonthlyOperation.RenderControl(e.Output);
        }

        private void Fill_trvDepartments_ManagerMasterMonthlyOperation()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                Department rootDep = this.MonthlyOperationBusiness.GetManagerDepartmentTree();
                TreeViewNode rootDepNode = new TreeViewNode();
                rootDepNode.ID = rootDep.ID.ToString();
                string rootDepNodeText = string.Empty;
                if (GetLocalResourceObject("OrgNode_trvDepartments_ManagerMasterMonthlyOperation") != null)
                    rootDepNodeText = GetLocalResourceObject("OrgNode_trvDepartments_ManagerMasterMonthlyOperation").ToString();
                else
                    rootDepNodeText = rootDep.Name;
                rootDepNode.Text = rootDepNodeText;
                rootDepNode.Value = rootDep.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\TreeView\\folder.gif"))
                    rootDepNode.ImageUrl = "Images/TreeView/folder.gif";
                this.trvDepartments_ManagerMasterMonthlyOperation.Nodes.Add(rootDepNode);
                rootDepNode.Expanded = true;

                this.GetChildDepartment_trvDepartments_ManagerMasterMonthlyOperation(rootDepNode, rootDep);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Departments_ManagerMasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Departments_ManagerMasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Departments_ManagerMasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void GetChildDepartment_trvDepartments_ManagerMasterMonthlyOperation(TreeViewNode parentDepartmentNode, Department parentDepartment)
        {
            foreach (Department childDep in parentDepartment.ChildList)
            {
                if (childDep.Visible)
                {
                    TreeViewNode childDepNode = new TreeViewNode();
                    childDepNode.ID = childDep.ID.ToString();
                    childDepNode.Text = childDep.Name;
                    childDepNode.Value = childDep.CustomCode;
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\TreeView\\folder.gif"))
                        childDepNode.ImageUrl = "Images/TreeView/folder.gif";
                    parentDepartmentNode.Nodes.Add(childDepNode);
                    try
                    {
                        if (parentDepartmentNode.Parent.Parent == null)
                            parentDepartmentNode.Expanded = true;
                    }
                    catch
                    { }
                    if (childDep.ChildList.Count > 0)
                        this.GetChildDepartment_trvDepartments_ManagerMasterMonthlyOperation(childDepNode, childDep);
                }
            }
        }

        private void Fill_cmbMonth_ManagerMasterMonthlyOperation()
        {
            string[] retMessage = new string[4];
            try
            {
                string CurrentLangID = string.Empty;
                string SysLangID = string.Empty;
                string Identifier = string.Empty;
                int month = 0;
                DateTime currentDateTime = DateTime.Now;
                CurrentLangID = this.LangProv.GetCurrentLanguage();
                SysLangID = this.LangProv.GetCurrentSysLanguage();
                switch (SysLangID)
                {
                    case "en-US":
                        switch (CurrentLangID)
                        {
                            case "en-US":
                                Identifier = "ee";
                                break;
                            case "fa-IR":
                                Identifier = "ef";
                                break;
                        }
                        month = currentDateTime.Month;
                        break;
                    case "fa-IR":
                        switch (CurrentLangID)
                        {
                            case "en-US":
                                Identifier = "fe";
                                break;
                            case "fa-IR":
                                Identifier = "ff";
                                break;
                        }
                        PersianCalendar pCal = new PersianCalendar();
                        month = pCal.GetMonth(currentDateTime);
                        break;
                }

                ComboBoxItem cmbItemNone = new ComboBoxItem(GetLocalResourceObject("Default").ToString());
                cmbItemNone.Value = "-1";
                this.cmbMonth_ManagerMasterMonthlyOperation.Items.Add(cmbItemNone);
                for (int i = 1; i <= 12; i++)
                {
                    ComboBoxItem cmbItemMonth = new ComboBoxItem(GetLocalResourceObject("Month" + i + "" + Identifier + "").ToString());
                    cmbItemMonth.Value = i.ToString();
                    this.cmbMonth_ManagerMasterMonthlyOperation.Items.Add(cmbItemMonth);
                    this.hfCurrentMonth_ManagerMasterMonthlyOperation.Value = cmbItemNone.Value;
                    this.cmbMonth_ManagerMasterMonthlyOperation.SelectedIndex = 0;
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Months_ManagerMasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Months_ManagerMasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Months_ManagerMasterMonthlyOperation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }


    }
}