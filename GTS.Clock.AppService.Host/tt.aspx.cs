using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class tt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
        string currentLoginId = identity.Name;
        UserNameLbl.Text = currentLoginId;
    }
}
