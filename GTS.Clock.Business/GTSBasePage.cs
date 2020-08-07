using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using WebControls = System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using System.Web.UI.HtmlControls;
using CpontArt = ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;

namespace GTS.Clock.Business.UI
{
    public abstract class GTSBasePage : Page
    {
        AuthorizeRepository athorizeRep = new AuthorizeRepository();
        List<CpontArt.NavBar> NavBarControles = new List<CpontArt.NavBar>();
        List<CpontArt.NavBarItem> NavBarItems = new List<CpontArt.NavBarItem>();
        List<CpontArt.ToolBar> toolbarControles = new List<CpontArt.ToolBar>();
        List<CpontArt.ToolBarItem> toolbarItems = new List<CpontArt.ToolBarItem>();
        public IList<NavBarItem> AccessNotAllowdNavBarItemsList { get; set; }
        //public const string BussinessSecurityResourceList = "BussinessSecurityResourceList";

        /// <summary>
        /// جهت سریال کردن و فرستادن به کلاینت تا 
        /// در زمان تغییر وضعیت ها در جاوا اسکریپت دستکاری نشود
        /// </summary>
        private IList<Resource> accessDeniedList = new List<Resource>();
        private IList<Resource> accessAllowedResourceList = new List<Resource>();

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
                    User curentUser = BUser.CurrentUser;
                    if (curentUser.ID == 0)
                        throw exception;
                    else if (curentUser.Role == null || curentUser.Role.ID == 0)
                        throw new IllegalServiceAccess("برای کاربر نقش تعریف نشده است", "GTSBasePage");
                    else
                        return curentUser.Role;
                }
                else
                {
                    throw exception;
                }
            }
        }

        protected override void OnPreLoad(EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    base.OnPreLoad(e);
                    Authorize();
                    //base.Page.Title = "برنامه تحت وب اطلس حضور و غیاب";
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "GTSBasePage", "Authorizing...");
                throw ex;
            }
        }

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

        protected void Authorize()
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

            accessDeniedList = this.GetAccessDeniedList(CurrnetUserRole.ID);
            accessAllowedResourceList = this.GetAlowedResourceList(CurrnetUserRole.ID);
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
            IAuthorizeRepository athorizeRep = new AuthorizeRepository();

            foreach (ToolBarItem item in toolbarItems)
            {
                string controlId = item.ID.ToString().ToLower();
                int denyCount = accessDeniedList.Where(x => x.ResourceID.ToLower().Equals(controlId)).Count();

                //if (denyCount > 1)
                //{
                //    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.ResourceControlsWithRepeatedId, String.Format("Control ID more than one was found!:{0}", controlId), "GTSBasePage");
                //}
                //دسترسی به یک آیتم مشابه مانند اعتبار سنجی قوانین که شناسه کنترل 
                //یکسان دارد را نمایش میدهیم با نامسرویس اعتبارسنجی میکنیم 
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
            IAuthorizeRepository athorizeRep = new AuthorizeRepository();

            if (this.AccessNotAllowdNavBarItemsList == null)
                this.AccessNotAllowdNavBarItemsList = new List<NavBarItem>();

            foreach (NavBarItem item in NavBarItems)
            {
                string controlId = item.ID.ToString().ToLower();
                //int denyCount = accessDeniedList.Where(x => x.ResourceID.ToLower().Equals(controlId)).Count();
                //int allowedCount = accessAllowedResourceList.Where(x => x.ResourceID.ToLower().Equals(controlId)).Count();//باید در لیست اجازه داده شده ها هم موجود باشد 
                Resource resource = accessAllowedResourceList.Where(x => x.ResourceID.ToLower().Equals(controlId)).FirstOrDefault();
                /*allowedCount = 1;*/
                /* if (denyCount > 1)
                 {
                     throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.ResourceControlsWithRepeatedId, String.Format("Control ID more than one was found!:{0}", controlId), "GTSBasePage");
                 }*/
                //if (denyCount == 1 || allowedCount == 0)
                if (resource == null /*|| !Utility.VerifyHashCode(resource.ResourceID, resource.CheckKey)*/)
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

        private IList<Resource> GetAccessDeniedList(decimal roleId)
        {
            string extension = roleId.ToString();
            IList<Resource> list = new List<Resource>();
            if (!SessionHelper.HasSessionValue(SessionHelper.BussinessSecurityResourceList + extension))
            {
                list = athorizeRep.GetAccessDenied(roleId);

                if (list != null && list.Count > 0)
                {
                    SessionHelper.SaveSessionValue(SessionHelper.BussinessSecurityResourceList + extension, list);
                }
                else
                {
                    SessionHelper.ClearSessionValue(SessionHelper.BussinessSecurityResourceList + extension);
                }
            }
            object obj = SessionHelper.GetSessionValue(SessionHelper.BussinessSecurityResourceList + extension);
            if (obj != null)
            {
                list = (IList<Resource>)obj;
            }
            return list;
        }

        private IList<Resource> GetAlowedResourceList(decimal roleId)
        {
            string extension = roleId.ToString();
            IList<Resource> list = new List<Resource>();
            if (!SessionHelper.HasSessionValue(SessionHelper.BussinessSecurityAllResourceList + extension))
            {
                list = athorizeRep.GetAccessAllowed(roleId);

                if (list != null && list.Count > 0)
                {
                    SessionHelper.SaveSessionValue(SessionHelper.BussinessSecurityAllResourceList + extension, list);
                }
                else
                {
                    SessionHelper.ClearSessionValue(SessionHelper.BussinessSecurityAllResourceList + extension);
                }
            }
            object obj = SessionHelper.GetSessionValue(SessionHelper.BussinessSecurityAllResourceList + extension);
            if (obj != null)
            {
                list = (IList<Resource>)obj;
            }
            return list;
        }

        #endregion

    }
}
