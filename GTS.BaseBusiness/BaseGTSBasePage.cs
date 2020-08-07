using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using CpontArt = ComponentArt.Web.UI;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using GTS.Business;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

namespace GTS.Business
{
    public abstract class BaseGTSBasePage : Page
    {
        //AuthorizeRepository athorizeRep = new AuthorizeRepository();
        List<CpontArt.NavBar> NavBarControles = new List<CpontArt.NavBar>();
        List<CpontArt.NavBarItem> NavBarItems = new List<CpontArt.NavBarItem>();
        List<CpontArt.ToolBar> toolbarControles = new List<CpontArt.ToolBar>();
        List<CpontArt.ToolBarItem> toolbarItems = new List<CpontArt.ToolBarItem>();
        public IList<NavBarItem> AccessNotAllowdNavBarItemsList { get; set; }

        protected override void OnInit(EventArgs e)
        {           
            this.DispatchDefenderKey();
        }

        private void DispatchDefenderKey()
        {
            if (Session["CSRF"] == null)
                Session["CSRF"] = new Dictionary<string, string>();

            string page = HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1];
            Dictionary<string, string> keyDic = (Dictionary<string, string>)Session["CSRF"];
            if (!keyDic.ContainsKey(page))
                keyDic.Add(page, this.CreateHashData(page, true));
            else
                keyDic[page] = this.CreateHashData(page, true);

            Session["CSRF"] = keyDic;

            HiddenField hfCSRF = new HiddenField();
            hfCSRF.ID = "hfKey";
            hfCSRF.Value = keyDic[page];
            this.Page.Form.Controls.Add(hfCSRF);
        }

        private string CreateHashData(string data, bool isComplex)
        {
            SHA1 sha1 = SHA1.Create();
            if(isComplex)
                data = data + HttpContext.Current.Session.SessionID + HttpContext.Current.Request.UserHostAddress;
            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder returnValue = new StringBuilder();
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }
            return returnValue.ToString();
        }

        /// <summary>
        /// جهت سریال کردن و فرستادن به کلاینت تا 
        /// در زمان تغییر وضعیت ها در جاوا اسکریپت دستکاری نشود
        /// </summary>
        private List<Resource> accessDeniedList = new List<Resource>();
        private List<Resource> accessAllowedResourceList = new List<Resource>();
        protected User CurrentUSer = new User();
        

        /// <summary>
        /// نقش کاربر فعلی را برمیگرداند
        /// اگر شخص اعتبارسنجی نشده باشد خطا برمیگرداند
        /// </summary>
        public Role CurrnetUserRole
        {
            get
            {
                IllegalServiceAccess exception = new IllegalServiceAccess("کاربر اعتبار سنجی نشده است", "GTSBasePage");
                if (Page.User.Identity.IsAuthenticated)
                {                 
                    if (CurrentUSer.ID == 0)
                        throw exception;
                    else if (CurrentUSer.Role == null || CurrentUSer.Role.ID == 0)
                        throw new IllegalServiceAccess("برای کاربر نقش تعریف نشده است", "GTSBasePage");
                    else
                        return CurrentUSer.Role;
                }
                else
                {
                    throw exception;
                }
            }
        }

        public BaseGTSBasePage()
        {
        }

        //protected override void OnPreLoad(EventArgs e)
        //{
        //    try
        //    {
        //        if (!Page.IsPostBack)
        //        {
        //            base.OnPreLoad(e);
        //            Authorize();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BaseBusiness<Entity>.LogException(ex, "GTSBasePage", "Authorizing...");
        //        throw ex;
        //    }
        //}

        protected override void OnError(EventArgs e)
        {
            // At this point we have information about the error
            HttpContext ctx = HttpContext.Current;

            GTS.Clock.Infrastructure.Exceptions.UI.UIBaseException exception = ctx.Server.GetLastError() as GTS.Clock.Infrastructure.Exceptions.UI.UIBaseException;
            if (exception != null)
            {
                string errorInfo =
                   "<br>Fatal Identifier: " + exception.FatalExceptionIdentifier.ToString("G") +
                    "<br>Offending URL: " + ctx.Request.Url.ToString() +
                   "<br>Source: " + exception.Source +
                   "<br>Message: " + exception.Message +
                   "<br><br>Stack trace: " + exception.StackTrace;

                ctx.Response.Write(errorInfo);

                // --------------------------------------------------
                // To let the page finish running we clear the error
                // --------------------------------------------------
                ctx.Server.ClearError();
            }
            base.OnError(e);

        }

        #region Authorization 

        protected void Authorize(IAuthorizeServices service)
        {
            #region Retrive Page Controls
            List<ResourceControl> pageControles = new List<ResourceControl>();

            foreach (System.Web.UI.Control innerControl1 in Page.Controls)
            {
                if (innerControl1 is HtmlForm)
                {
                    GetControls(pageControles, innerControl1.Controls);
                }
            }

            if (NavBarControles.Count > 0)
            {
                foreach (NavBar navbar in NavBarControles)
                {
                    if (navbar.Items.Count > 0)
                    {
                        foreach (NavBarItem navbarItem in navbar.Items)
                        {
                            GetNavBarItems(navbarItem);
                        }
                    }
                }
            }

            if (toolbarControles.Count > 0)
            {
                foreach (ToolBar toolbar in toolbarControles)
                {
                    if (toolbar.Items.Count > 0)
                    {
                        foreach (ToolBarItem toolbarItem in toolbar.Items)
                        {
                            toolbarItems.Add(toolbarItem);
                        }
                    }
                }
            }

            #endregion

            accessDeniedList.AddRange(service.GetAccessDeniedList(CurrnetUserRole.ID));
            accessAllowedResourceList.AddRange(service.GetAlowedResourceList(CurrnetUserRole.ID));

            #region Apply Other Business Roles
            IList<RoleCustomCodeType> otherRoles = this.GetCurrentUserBusinessRole(service);
            Dictionary<string, object> managementState = (Dictionary<string, object>)SessionHelper.GetSessionValue(SessionHelper.GTSCurrentUserManagmentState);

            if (this.CurrentUSer.Role.CustomCode.Equals(((int)RoleCustomCodeType.User).ToString()))
            {

                if (otherRoles.Count > 0)
                {
                    foreach (RoleCustomCodeType roleCode in otherRoles)
                    {
                        decimal roleId = 0;
                        switch (roleCode)
                        {
                            case RoleCustomCodeType.Manager:
                                if (managementState.ContainsKey("ManagerRoleId"))
                                {
                                    roleId = Utility.ToDecimal(managementState["ManagerRoleId"]);
                                }
                                break;
                            case RoleCustomCodeType.Substitute:
                                if (managementState.ContainsKey("SubstituteRoleId"))
                                {
                                    roleId = Utility.ToDecimal(managementState["SubstituteRoleId"]);
                                }
                                break;
                            case RoleCustomCodeType.Operator:
                                if (managementState.ContainsKey("OperatorRoleId"))
                                {
                                    roleId = Utility.ToDecimal(managementState["OperatorRoleId"]);
                                }
                                break;
                            //Role role = service.GetRoleByCode(roleCode);
                        }
                        if (roleId > 0)
                        {
                            accessDeniedList.AddRange(service.GetAccessDeniedList(roleId));
                            accessAllowedResourceList.AddRange(service.GetAlowedResourceList(roleId));
                        }
                    }
                    accessDeniedList = accessDeniedList.Distinct().ToList();
                    accessAllowedResourceList = accessAllowedResourceList.Distinct().ToList();
                }
            }
            #endregion


            DoAthorizeOnNavBarItems();
            DoAthorizeOnToolBarItems();


        }

        /// <summary>
        /// کنترلها و کنترهای داخلی را بصورت بازگشتی استخراج میکند
        /// </summary>
        /// <param name="result"></param>
        /// <param name="control"></param>
        private void GetControls(List<ResourceControl> result, System.Web.UI.ControlCollection controls)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                System.Web.UI.Control control = controls[i];
                if (control.Controls != null && control.Controls.Count > 0)
                {
                    GetControls(result, control.Controls);
                }
                if (control is CpontArt.NavBar)
                {
                    CpontArt.NavBar navabar = (NavBar)control;
                    NavBarControles.Add(navabar);
                }
                else if (control is CpontArt.ToolBar)
                {
                    CpontArt.ToolBar toolabar = (ToolBar)control;
                    toolbarControles.Add(toolabar);
                }
            }
        }

        private void GetNavBarItems(CpontArt.NavBarItem navbarItem)
        {
            NavBarItems.Add(navbarItem);
            if (navbarItem.Items != null)
            {
                foreach (CpontArt.NavBarItem item in navbarItem.Items)
                {
                    if (item.Items != null)
                    {
                        GetNavBarItems(item);
                    }
                }
            }
        }

        private void DoAthorizeOnToolBarItems()
        {
            foreach (ToolBarItem item in toolbarItems)
            {
                string controlId = item.ID.ToString().ToLower();
                int denyCount = accessDeniedList.Where(x => x.ResourceID.ToLower().Equals(controlId)).Count();
                
                if (denyCount >= 1)
                {
                    int allowCount = accessAllowedResourceList.Where(x => x.ResourceID.ToLower().Equals(controlId)).Count();
                    if (allowCount > 0)
                        continue;
                    else
                        item.ParentToolBar.Items.Remove(item);
                }
            }
        }


        /// <summary>
        /// یک آیتم در صورتی نمایش داده میشود که سه شرط را دارا باشد
        /// * جزو عدم دسترسی نباشد
        /// ** باید آن آیتم حتما جزو دسترسی داده شده ها باشد
        /// ** کد چک آن درست باشد
        /// </summary>
        private void DoAthorizeOnNavBarItems()
        {

            if (this.AccessNotAllowdNavBarItemsList == null)
                this.AccessNotAllowdNavBarItemsList = new List<NavBarItem>();

            foreach (NavBarItem item in NavBarItems)
            {
                string controlId = item.ID.ToString().ToLower();
                Resource resource = accessAllowedResourceList.Where(x => x.ResourceID.ToLower().Equals(controlId)).FirstOrDefault();
               
                if (resource == null)
                {
                    if (item.ParentItem != null)
                    {
                        this.AccessNotAllowdNavBarItemsList.Add(item);
                        ((NavBarItem)(item.ParentItem)).Items.Remove(item);
                    }
                    else if (item.ParentNavBar != null)
                    {
                        ((NavBar)(item.ParentNavBar)).Items.Remove(item);
                    }
                }
            }
        }

        private IList<RoleCustomCodeType> GetCurrentUserBusinessRole(IAuthorizeServices service)
        {
            try
            {
                IList<RoleCustomCodeType> roles = new List<RoleCustomCodeType>();

                if (!SessionHelper.HasSessionValue(SessionHelper.GTSCurrentUserManagmentState))
                {
                    bool isManager = service.IsManager(this.CurrentUSer.Person.ID);

                    bool isSubstitute = service.IsSubstituteManager(this.CurrentUSer.Person.ID);

                    bool isOperator = service.IsOperator(this.CurrentUSer.Person.ID);
                    
                    Dictionary<string, object> ManagementState = new Dictionary<string, object>();

                    if (isManager)
                    {
                        Role role = service.GetRoleByCode(RoleCustomCodeType.Manager);
                        if (role != null)
                        {
                            ManagementState.Add("ManagerRoleId", role.ID);
                        }
                    }
                    if (isOperator)
                    {
                        Role role = service.GetRoleByCode(RoleCustomCodeType.Operator);
                        if (role != null)
                        {
                            ManagementState.Add("OperatorRoleId", role.ID);
                        }
                    }
                    if (isSubstitute)
                    {
                        Role role = service.GetRoleByCode(RoleCustomCodeType.Substitute);
                        if (role != null)
                        {
                            ManagementState.Add("SubstituteRoleId", role.ID);
                        }
                    }                  

                   
                    ManagementState.Add("IsManager", isManager);
                    ManagementState.Add("IsOperator", isOperator);
                    ManagementState.Add("IsSubstitute", isSubstitute);

                    SessionHelper.SaveSessionValue(SessionHelper.GTSCurrentUserManagmentState, ManagementState);
                }

                Dictionary<string, object> managementState = (Dictionary<string, object>)SessionHelper.GetSessionValue(SessionHelper.GTSCurrentUserManagmentState);

                if (Utility.ToBoolean(managementState["IsManager"]))
                    roles.Add(RoleCustomCodeType.Manager);

                if (Utility.ToBoolean(managementState["IsOperator"]))
                    roles.Add(RoleCustomCodeType.Operator);

                if (Utility.ToBoolean(managementState["IsSubstitute"]))
                    roles.Add(RoleCustomCodeType.Substitute);

                return roles;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        #endregion

    }
}
