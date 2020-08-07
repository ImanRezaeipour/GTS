using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Security;
using GTS.Clock.Model;
using ComponentArt.Web.UI;
using System.IO;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.RuleDesigner.UI.Web.Classes;
using ExceptionHandler = GTS.Clock.RuleDesigner.UI.Web.Classes.ExceptionHandler;

namespace GTS.Clock.RuleDesigner.UI.Web
{
    public partial class Users : GTSBasePage
    {
        public BUser UserBusiness
        {
            get
            {
                return new BUser();
            }
        }
        public ISearchPerson PersonSearchBusiness
        {
            get
            {
                return (ISearchPerson)(new BPerson());
            }
        }

        public enum LoadState
        {
            Normal,
            Search,
            AdvancedSearch
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
        public AdvancedPersonnelSearchProvider APSProv
        {
            get
            {
                return new AdvancedPersonnelSearchProvider();
            }
        }

        #region grid related

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbDomainName_Users.IsCallback &&
                !CallBack_cmbDomainUserName_Users.IsCallback &&
                !CallBack_cmbPersonnel_Users.IsCallback &&
                !CallBack_cmbSearchField_Users.IsCallback &&
                !CallBack_cmbUserRole_Users.IsCallback &&
                !CallBack_GridUsers_Users.IsCallback)
            {
                Page UsersPage = this;
                //Ajax.Utility.GenerateMethodScripts(this.GetType(), ref UsersPage);
                //Ajax.Utility.GenerateMethodScripts(this);
                this.SetUsersPageSize_Users();
                this.SetUsersPageCount_Users(LoadState.Normal, UserSearchKeys.Name, string.Empty);
                this.SetPersonnelPageSize_cmbPersonnel_Users();
                SkinHelper.InitializeSkin(this.Page);
            }
        }
        private void SetUsersPageSize_Users()
        {
            this.hfUsersPageSize_Users.Value = this.GridUsers_Users.PageSize.ToString();
        }
        private void SetUsersPageCount_Users(LoadState Ls, UserSearchKeys SearchKey, string SearchTerm)
        {
            var UsersCount = 0;
            switch (Ls)
            {
                case LoadState.Normal:
                    UsersCount = this.UserBusiness.GetAllUsersCount();
                    break;
                case LoadState.Search:
                    UsersCount = this.UserBusiness.GetAllByPageBySearchCount(SearchKey, SearchTerm);
                    break;
            }
            this.hfUsersCount_Users.Value = UsersCount.ToString();
            this.hfUsersPageCount_Users.Value = Utility.GetPageCount(UsersCount, this.GridUsers_Users.PageSize).ToString();
        }
        protected void CallBack_GridUsers_Users_OnCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.SetUsersPageCount_Users(
                (LoadState)Enum.Parse(typeof(LoadState),this.StringBuilder.CreateString(e.Parameters[0])),
                (UserSearchKeys)Enum.Parse(typeof(UserSearchKeys), this.StringBuilder.CreateString(e.Parameters[3])),
                this.StringBuilder.CreateString(e.Parameters[4])
                );

            this.Fill_GridUsers_Users(
                (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])),
                int.Parse(this.StringBuilder.CreateString(e.Parameters[1])),
                int.Parse(this.StringBuilder.CreateString(e.Parameters[2])),
                (UserSearchKeys)Enum.Parse(typeof(UserSearchKeys), this.StringBuilder.CreateString(e.Parameters[3])), 
                this.StringBuilder.CreateString(e.Parameters[4])
                );

            this.GridUsers_Users.RenderControl(e.Output);
            this.hfUsersCount_Users.RenderControl(e.Output);
            this.hfUsersPageCount_Users.RenderControl(e.Output);
            this.ErrorHiddenField_Users.RenderControl(e.Output);
        }
        private void Fill_GridUsers_Users(LoadState Ls, int pageSize, int pageIndex, UserSearchKeys SearchKey, string SearchTerm)
        {
            var retMessage = new string[4];
            IList<UserProxy> UsersList = null;
            try
            {
                this.InitializeCulture();
                switch (Ls)
                {
                    case LoadState.Normal:
                        UsersList = this.UserBusiness.GetAllByPage(pageIndex, pageSize);
                        break;
                    case LoadState.Search:
                        UsersList = this.UserBusiness.GetAllByPageBySearch(SearchKey, SearchTerm, pageIndex, pageSize);
                        break;
                }
                this.GridUsers_Users.DataSource = UsersList;
                this.GridUsers_Users.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (OutOfExpectedRangeException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
                this.ErrorHiddenField_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        #endregion grid related


        private void SetPersonnelPageSize_cmbPersonnel_Users()
        {
            this.hfPersonnelPageSize_Users.Value = this.cmbPersonnel_Users.DropDownPageSize.ToString();
        }
        private void SetPersonnelPageCount_cmbPersonnel_Users(LoadState Ls, int pageSize, string SearchTerm)
        {
            string[] retMessage = new string[4];
            int PersonnelCount = 0;
            try
            {
                switch (Ls)
                {
                    case LoadState.Normal:
                        PersonnelCount = this.PersonSearchBusiness.GetPersonCount();
                        break;
                    case LoadState.Search:
                        PersonnelCount = this.PersonSearchBusiness.GetPersonInQuickSearchCount(SearchTerm);
                        break;
                    case LoadState.AdvancedSearch:
                        PersonnelCount = this.PersonSearchBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm));
                        break;
                }
                this.hfPersonnelPageCount_Users.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
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
        protected void CallBack_cmbPersonnel_Users_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbPersonnel_Users.Dispose();
            this.SetPersonnelPageCount_cmbPersonnel_Users(
                (LoadState)Enum.Parse(typeof(LoadState),this.StringBuilder.CreateString(e.Parameters[0])),
                int.Parse(this.StringBuilder.CreateString(e.Parameters[1])),
                this.StringBuilder.CreateString(e.Parameters[3])
                );

            this.Fill_cmbUsers_Users(
                (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])),
                int.Parse(this.StringBuilder.CreateString(e.Parameters[1])), 
                int.Parse(this.StringBuilder.CreateString(e.Parameters[2])),
                this.StringBuilder.CreateString(e.Parameters[3]));

            this.cmbPersonnel_Users.Enabled = true;
            this.cmbPersonnel_Users.RenderControl(e.Output);
            this.hfPersonnelPageCount_Users.RenderControl(e.Output);
            this.ErrorHiddenField_Personnel_Users.RenderControl(e.Output);
        }
        private void Fill_cmbUsers_Users(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Person> PersonList = null;
                switch (Ls)
                {
                    case LoadState.Normal:
                        PersonList = this.PersonSearchBusiness.QuickSearchByPageApplyCulture(pageIndex, pageSize, string.Empty);
                        break;
                    case LoadState.Search:
                        PersonList = this.PersonSearchBusiness.QuickSearchByPageApplyCulture(pageIndex, pageSize, SearchTerm);
                        break;
                    case LoadState.AdvancedSearch:
                        PersonList = this.PersonSearchBusiness.GetPersonInAdvanceSearchApplyCulture(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize);
                        break;
                }
                foreach (Person personItem in PersonList)
                {
                    ComboBoxItem personCmbItem = new ComboBoxItem(personItem.FirstName + " " + personItem.LastName);
                    personCmbItem["BarCode"] = personItem.BarCode;
                    personCmbItem["CardNum"] = personItem.CardNum;
                    personCmbItem.Value = personItem.ID.ToString();
                    this.cmbPersonnel_Users.Items.Add(personCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_cmbSearchField_Users_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbSearchField_Users.Dispose();
            this.Fill_cmbSearchField_Users();
            this.cmbSearchField_Users.Enabled = true;
            this.ErrorHiddenField_SearchFields.RenderControl(e.Output);
            this.cmbSearchField_Users.RenderControl(e.Output);
        }
        private void Fill_cmbSearchField_Users()
        {
            string[] retMessage = new string[4];
            try
            {
                foreach (UserSearchKeys userSearchKeyItem in Enum.GetValues(typeof(UserSearchKeys)))
                {
                    ComboBoxItem cmbItemUserSearchKey = new ComboBoxItem(GetLocalResourceObject(userSearchKeyItem.ToString()).ToString());
                    cmbItemUserSearchKey.Value = ((int)userSearchKeyItem).ToString();
                    this.cmbSearchField_Users.Items.Add(cmbItemUserSearchKey);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_SearchFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_SearchFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_SearchFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_cmbUserRole_Users_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbUserRole_Users.Dispose();
            this.Fill_cmbUserRole_cmbUserRole_Users();
            this.cmbUserRole_Users.Enabled = true;
            this.ErrorHiddenField_UsersRoles.RenderControl(e.Output);
            this.cmbUserRole_Users.RenderControl(e.Output);
        }
        private void Fill_cmbUserRole_cmbUserRole_Users()
        {
            this.Fill_trvUserRole_cmbUserRole_Users();
        }
        private void Fill_trvUserRole_cmbUserRole_Users()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                Role rootDep = this.UserBusiness.GetRoleTree();
                TreeViewNode rootRoleNode = new TreeViewNode();
                rootRoleNode.ID = rootDep.ID.ToString();
                string rootRoleNodeText = string.Empty;
                if (GetLocalResourceObject("RolesNode_trvRoles_Roles") != null)
                    rootRoleNodeText = GetLocalResourceObject("RolesNode_trvRoles_Roles").ToString();
                else
                    rootRoleNodeText = rootDep.Name;
                rootRoleNode.Text = rootRoleNodeText;
                rootRoleNode.Value = rootDep.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\TreeView\\folder.gif"))
                    rootRoleNode.ImageUrl = "Images/TreeView/folder.gif";
                this.trvUserRole_cmbUserRole_Users.Nodes.Add(rootRoleNode);
                rootRoleNode.Expanded = true;

                this.GetChildRoles_trvUserRole_cmbUserRole_Users(rootRoleNode, rootDep);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_UsersRoles.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_UsersRoles.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_UsersRoles.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        private void GetChildRoles_trvUserRole_cmbUserRole_Users(TreeViewNode parentRoleNode, Role parentRole)
        {
            foreach (Role childRole in this.UserBusiness.GetRoleChilds(parentRole.ID))
            {
                TreeViewNode childRoleNode = new TreeViewNode();
                childRoleNode.ID = childRole.ID.ToString();
                childRoleNode.Text = childRole.Name;
                childRoleNode.Value = childRole.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\TreeView\\folder.gif"))
                    childRoleNode.ImageUrl = "Images/TreeView/folder.gif";
                parentRoleNode.Nodes.Add(childRoleNode);
                try
                {
                    if (parentRoleNode.Parent.Parent == null)
                        parentRoleNode.Expanded = true;
                }
                catch
                { }
                if (this.UserBusiness.GetRoleChilds(childRole.ID).Count > 0)
                    this.GetChildRoles_trvUserRole_cmbUserRole_Users(childRoleNode, childRole);
            }
        }
        protected void CallBack_cmbDomainName_Users_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbDomainName_Users.Dispose();
            this.Fill_cmbDomainName_Users();
            this.cmbDomainName_Users.Enabled = true;
            this.ErrorHiddenField_Domains.RenderControl(e.Output);
            this.cmbDomainName_Users.RenderControl(e.Output);
        }
        private void Fill_cmbDomainName_Users()
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Domains> DomainsList = this.UserBusiness.GetActiveDirectoryDomains();
                this.cmbDomainName_Users.DataSource = DomainsList;
                this.cmbDomainName_Users.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_SearchFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_SearchFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_SearchFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_cmbDomainUserName_Users_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbDomainUserName_Users.Dispose();
            this.Fill_cmbDomainUserName_Users(decimal.Parse(this.StringBuilder.CreateString(e.Parameter)));
            this.cmbDomainUserName_Users.Enabled = true;
            this.ErrorHiddenField_DomainUsers.RenderControl(e.Output);
            this.cmbDomainUserName_Users.RenderControl(e.Output);
        }
        private void Fill_cmbDomainUserName_Users(decimal DomainID)
        {
            string[] retMessage = new string[4];
            try
            {
                if (DomainID == -1)
                {
                    retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoDomainSelected").ToString()), retMessage);
                    this.ErrorHiddenField_DomainUsers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
                    return;
                }
                IList<string> DomainUsersList = this.UserBusiness.GetActiveDirectoryUsers(DomainID);
                foreach (string domainUserItem in DomainUsersList)
                {
                    ComboBoxItem cmbItemDomainUser = new ComboBoxItem(domainUserItem);
                    this.cmbDomainUserName_Users.Items.Add(cmbItemDomainUser);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_DomainUsers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_DomainUsers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_DomainUsers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdateUser_UsersPage", "UpdateUser_UsersPage_onCallBack", null, null)]
        public string[] UpdateUser_UsersPage(string state, string SelectedUserID, string IsActiveUser, string PersonnelID, string RoleID, string UserName, string Password, string ConfirmPassword, string IsActiveDirectoryAuthenticated, string IsPasswordChange)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            decimal UserID = 0;
            decimal selectedUserID = decimal.Parse(this.StringBuilder.CreateString(SelectedUserID));
            bool isActiveUser = bool.Parse(this.StringBuilder.CreateString(IsActiveUser));
            decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID));
            decimal roleID = decimal.Parse(this.StringBuilder.CreateString(RoleID));
            UserName = this.StringBuilder.CreateString(UserName);
            Password = this.StringBuilder.CreateString(Password);
            ConfirmPassword = this.StringBuilder.CreateString(ConfirmPassword);
            bool isActiveDirectoryAuthenticated = bool.Parse(this.StringBuilder.CreateString(IsActiveDirectoryAuthenticated));
            bool isPasswordChange = bool.Parse(this.StringBuilder.CreateString(IsPasswordChange));
            UIActionType uam = UIActionType.ADD;
            state = this.StringBuilder.CreateString(state);
            UserProxy user = new UserProxy();
            user.ID = selectedUserID;
            if (state != "Delete")
            {
                user.Active = isActiveUser;
                user.PersonID = personnelID;
                user.RoleID = roleID;
                user.UserName = UserName;
                user.Password = Password;
                user.ConfirmPassword = ConfirmPassword;
                user.ActiveDirectoryAuthenticate = isActiveDirectoryAuthenticated;
                user.IsPasswodChanged = isPasswordChange;
            }
            try
            {
                switch (state)
                {
                    case "Add":
                        uam = UIActionType.ADD;
                        UserID = this.UserBusiness.InsertUser(user);
                        break;
                    case "Edit":
                        uam = UIActionType.EDIT;
                        if (selectedUserID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoUserSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        UserID = this.UserBusiness.EditUser(user);
                        break;
                    case "Delete":
                        uam = UIActionType.DELETE;
                        if (selectedUserID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoUserSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        UserID = this.UserBusiness.DeleteUser(user);
                        break;
                    default:
                        break;
                }

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uam)
                {
                    case UIActionType.ADD:
                        SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                        break;
                    case UIActionType.EDIT:
                        SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                        break;
                    case UIActionType.DELETE:
                        SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                        break;
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = UserID.ToString();
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

        //[Ajax.AjaxMethod("ExcelExport_GridUsers_Users_UsersPage", "ExcelExport_GridUsers_Users_UsersPage_onCallBack", null, false, null)]
        //public string ExcelExport_GridUsers_Users_UsersPage(string GridColumns, string SearchKey, string SearchText, string PageIndex, string PageSize)
        //{
        //    return string.Empty;
        //    IList<UserInfo> UserInfoList = null;
        //    if (SearchKey != null && SearchText != null)
        //        UserInfoList = this.usersPresenter.GetAllByPage(this.GetCurrentUserSearchKey(SearchKey), this.StringBuilder(SearchText), Convert.ToInt32(PageIndex), Convert.ToInt32(PageSize));
        //    else
        //        UserInfoList = this.usersPresenter.GetAllByPage(Convert.ToInt32(PageIndex), Convert.ToInt32(PageSize));

        //    string FileName = this.CreateExportDs_GridUsers_Users(GridColumns, UserInfoList);
        //    return FileName;
        //}

        //private string CreateExportDs_GridUsers_Users(string GridColumns, IList<UserInfo> UserInfoList)
        //{
        //    string FileName = "";
        //    if (UserInfoList != null)
        //    {
        //        DataSet ds = new DataSet();
        //        DataTable dt = new DataTable();
        //        string[] ColumnsCol = GridColumns.Split(new char[] { '#' });
        //        Dictionary<string, string> dicColumns = new Dictionary<string, string>();
        //        foreach (string ColumnItem in ColumnsCol)
        //        {
        //            if (ColumnItem != "")
        //            {
        //                string[] ColumnFeaturesCol = ColumnItem.Split(new char[] { ':' });
        //                string ColumnDataField = ColumnFeaturesCol[0];
        //                string ColumnCaption = this.StringBuilder(ColumnFeaturesCol[1]);
        //                dicColumns.Add(ColumnDataField, ColumnCaption);
        //            }
        //        }
        //        foreach (PropertyInfo PropInfo in typeof(UserInfo).GetProperties())
        //        {
        //            if (dicColumns.ContainsKey(PropInfo.Name))
        //            {
        //                DataColumn dc = new DataColumn(PropInfo.Name, PropInfo.PropertyType);
        //                dc.Caption = dicColumns[PropInfo.Name];
        //                dt.Columns.Add(dc);
        //            }
        //        }
        //        ds.Tables.Add(dt);

        //        foreach (UserInfo uInfo in UserInfoList)
        //        {
        //            DataRow dr = ds.Tables[0].NewRow();
        //            foreach (PropertyInfo pInfo in typeof(UserInfo).GetProperties())
        //            {
        //                if (dicColumns.ContainsKey(pInfo.Name))
        //                    dr[pInfo.Name] = pInfo.GetValue(uInfo, null);
        //            }
        //            ds.Tables[0].Rows.Add(dr);
        //        }

        //        FileName = this.ExportDs_GridUsers_Users(ds, dicColumns);
        //    }
        //    return FileName;
        //}

        //private string ExportDs_GridUsers_Users(DataSet ds, Dictionary<string, string> dicColumns)
        //{
        //    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/XLS"))
        //        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/XLS");

        //    string FileName = Guid.NewGuid().ToString();
        //    ds.WriteXml(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xml");

        //    Spreadsheet document = new Spreadsheet();
        //    SimpleXMLConvertor tools = new SimpleXMLConvertor(document);
        //    tools.ColumnsCol = dicColumns;
        //    this.InitializeCulture();
        //    tools.WorkSheetName = this.GetLocalResourceObject("WorkSheetName_ExportedData_GridUsers_Users").ToString();
        //    tools.LoadXML(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xml");
        //    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xml");
        //    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xls"))
        //        File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xls");
        //    document.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xls");
        //    document.Close();

        //    Spreadsheet exportedDocument = new Spreadsheet();
        //    exportedDocument.LoadFromFile(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xls");
        //    Worksheet worksheet = exportedDocument.Workbook.Worksheets[1];
        //    worksheet.Rows.Delete(0, 14);
        //    exportedDocument.Worksheet(1).Visible = SHEETVISIBILITY.VeryHidden;
        //    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xls");
        //    exportedDocument.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xls");
        //    exportedDocument.Close();

        //    return FileName + ".xls";
        //}

    }
}