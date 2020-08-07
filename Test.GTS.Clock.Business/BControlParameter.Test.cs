using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Security;
using GTS.Clock.Business;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Infrastructure;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class BControlParamterTest 
    {
        #region ADOObjects
        BControlParameter_YearMonth busControl_YearMonth;
        #endregion

        [SetUp]
        public void TestSetup()
        {
            busControl_YearMonth = new BControlParameter_YearMonth();
        }

        [TearDown]
        public void TreatDown()
        {           

        }

        [Test]
        public void GetParamterValue_YearMonth1() 
        {
            string value = busControl_YearMonth.GetParameterValue(1, 1, ReportParametersActionId.PersonDateRange.ToString(), 2000, 2);
            Assert.IsNotEmpty(value);
        }

        [Test]
        public void GetParamterValue_Parse_YearMonth()
        {
            string value = busControl_YearMonth.GetParameterValue(1, 1, ReportParametersActionId.PersonDateRange.ToString(), 1390, 2);
            IDictionary<string, object> list = busControl_YearMonth.ParsParameter(value, "PersonDateRange");
        }
    }
}
