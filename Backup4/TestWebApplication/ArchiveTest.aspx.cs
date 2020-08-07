using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.ArchiveCalculations;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;

namespace TestWebApplication
{
    public partial class ArchiveTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void button1_Click(object sender, EventArgs e)
        {
            BArchiveCalculator bus = new BArchiveCalculator();
            PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy() { PersonIdList = new List<decimal>() { 1, 870,527 } };
            ArchiveExsitsConditions result = bus.IsArchiveExsits(1392, 4, proxy);
        }

        protected void button2_Click(object sender, EventArgs e)
        {
            PersonRepository pr = new PersonRepository(false);
            Person p = pr.GetByBarcode(barcodeTb.Text);
            BArchiveCalculator bus = new BArchiveCalculator();
            bus.ArchiveData(1392, 4, p.ID);
        }

        protected void button3_Click(object sender, EventArgs e)
        {
            BArchiveCalculator bus = new BArchiveCalculator();
            PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy() { PersonIdList = new List<decimal>() { 1, 870, 527, 554 } };
            bus.GetArchiveValues(1392, 4, proxy, 0, 10);
        }

        protected void button4_Click(object sender, EventArgs e)
        {
            BArchiveCalculator bus = new BArchiveCalculator();
            PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy() { PersonIdList = new List<decimal>() { 1, 870, 527, 554 } };
            int a = bus.GetSearchCount(proxy, 1392, 4);
            bus.GetSearchCount("", 1392, 4);
            bus.GetArchiveValues(1392, 4, proxy, 0, 10);
            bus.GetArchiveValues(1392, 4, "", 0, 10);
        }

        protected void button5_Click(object sender, EventArgs e)
        {
            BArchiveCalculator bus = new BArchiveCalculator();
            bus.GetArchiveGridSettings();
        }

        protected void button6_Click(object sender, EventArgs e)
        {
            BArchiveCalculator bus = new BArchiveCalculator();
            IList<ArchiveFieldMap> list = bus.GetArchiveGridSettings();
            list[1].Visible = false;
            bus.SetArchiveGridSettings(list);
        }

        protected void button7_Click(object sender, EventArgs e)
        {
            BArchiveCalculator bus = new BArchiveCalculator();
            ArchiveCalcValuesProxy proxy = bus.GetArchiveValues(1392, 4, 870).First();
            proxy.P1 = "-9";
            proxy.P2 = "-10";
            proxy.P3 = "-10";
            bus.SetArchiveValues(1392, 4, 870, proxy);

        }
    }
}