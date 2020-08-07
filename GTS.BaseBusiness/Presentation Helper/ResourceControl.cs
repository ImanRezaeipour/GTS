using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebControls = System.Web.UI.WebControls;
using System.Web.UI;

namespace GTS.Business
{
    /// <summary>
    /// برای ذخیره کنترل های صفحه بمنظور پیدا کردن راحتر کنترل از روی شناسه استفاده میشود
    /// </summary>
    public class ResourceControl
    {
        public ResourceControl()
        {

        }
        public ResourceControl(string id, WebControls.WebControl control)
        {
            ID = id;
            Control = control;
        }
        public string ID
        {
            get;
            set;
        }

        public string LowerID
        {
            get
            {
                return ID.ToLower();
            }
        }

        public WebControls.WebControl Control
        {
            get;
            set;
        }

        public override string ToString()
        {
            return String.Format("{0}", ID);
        }
    }
}
