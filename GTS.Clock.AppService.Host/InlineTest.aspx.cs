using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GTS.Clock.AppService.Host
{
    public partial class InlineTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            GTS.Clock.Business.Shifts.BShift shift = new GTS.Clock.Business.Shifts.BShift();
            myGrid.DataSource= shift.GetAll();
            myGrid.DataBind();
        }
    }
}
