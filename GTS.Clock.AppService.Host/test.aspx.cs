using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business;
using GTS.Clock.Business.Charts;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.Rules;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.Assignments;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.TrafficMapping;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.Engine;

namespace GTS.Clock.AppService.Host
{
    public partial class test :Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["MyKey"] = "Hello!";
            Response.Write(DateTime.Now.ToString());
        }

        protected void ExecuteBtn_Click(object sender, EventArgs e)
        {
            //BEngineCalculator bus = new BEngineCalculator();
            GTS.Clock.Business.Engine.WebServices.TotalWebService total = new GTS.Clock.Business.Engine.WebServices.TotalWebService();
            PersonRepository pr = new PersonRepository(false);
            Person p = pr.GetByBarcode(prsIdTB.Text);

            //bus.Calculate(p.ID, Utility.ToPersianDate(DateTime.Now));
            //total.GTS_ExecuteByPersonID("Me", p.ID);


            decimal[] ids = new decimal[1];
            ids[0] = p.ID;
            total.GTS_ExecutePersonsByToDate(BUser.CurrentUser.UserName, ids,DateTime.Now);
        }

        protected void Traffic_Click(object sender, EventArgs e)
        {
            BPerson bus=new BPerson();
            Person prs = bus.GetByUsername("Salavati");
            prs.InitializeForTrafficMapper(Convert.ToDateTime("2012-07-20"), Convert.ToDateTime("2012-12-20"));

            TrafficMapper tm = new TrafficMapper(bus.GetByUsername("Salavati"));
            tm.DoMap();

            //BEngineCalculator bus1 = new BEngineCalculator();
            //bus1.Calculate(bus.GetByUsername("Salavati").ID, Utility.ToString(DateTime.Now.AddMonths(2)));
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            BTraffic bus = new BTraffic();
            bus.GetPersonDailyReport(32688, Utility.ToString(new DateTime(2012, 2, 20)));
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Infrastructure.Log.BusinessServiceLogger log = new BusinessServiceLogger();
            log.Error("21321", "test");
            log.Flush();
        }
    }
}
