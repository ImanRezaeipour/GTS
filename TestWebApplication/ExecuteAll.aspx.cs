using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.Engine;

namespace TestWebApplication
{
    public partial class ExecuteAll : System.Web.UI.Page
    {
        BEngineCalculator engingBus = new BEngineCalculator();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UpdateTimer_Tick(object sender, EventArgs e)
        {
            int total = engingBus.GetTotalCountInCalculating();
            int remain = engingBus.GetRemainCountInCalculating();

            progresaLBL.Text = String.Format("Total Executing: {0} --- Remain: {1} at Time: {2}", total, remain, DateTime.Now.TimeOfDay);
                // (Utility.ToInteger(progresaLBL.Text) + 1).ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            engingBus.Calculate("", Utility.ToPersianDate(Calendar1.SelectedDate),false);
        }
    }
}