using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Assignments;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business;
using GTS.Clock.Business.WorkFlow;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business.Leave;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.Rules;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Engine;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    class EngineCalcBusinessTest : BaseFixture
    {
        #region variables
        BEngineCalculator BEngineCalc;
        #endregion

        [SetUp]
        public void TestSetup()
        {
            BEngineCalc = new BEngineCalculator();
        }

        [TearDown]
        public void TreatDown()
        {
            
        }

        [Test]
        public void PersonCalculateTest()
        {
            BEngineCalc.Calculate(32660, Utility.ToPersianDate(DateTime.Now));
            Assert.True(true);
        }

        [Test]
        public void PersonCalculateAllTest()
        {
            dataAccessDepTA.Insert(BUser.CurrentUser.ID, null, true);
            PersonAdvanceSearchProxy p = new PersonAdvanceSearchProxy();
            p.DepartmentId = 221;
            BEngineCalc.Calculate(p, Utility.ToPersianDate(DateTime.Now));
            Assert.True(true);
        }

    }
}
