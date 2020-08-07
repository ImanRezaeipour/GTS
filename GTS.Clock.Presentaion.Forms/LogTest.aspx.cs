using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Business.UI;
public partial class LogTest : GTSBasePage
{
    BusinessServiceLogger busLogger = new BusinessServiceLogger();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Btn1_Click(object sender, EventArgs e)
    {
        busLogger.Error("323232", "sdfasdfasdasdasdas", new Exception("asdasdasdas"));
        busLogger.Flush();
    }
}