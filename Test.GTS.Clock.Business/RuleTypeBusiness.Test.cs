using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using GTS.Clock.Business.Rules;
using GTS.Clock.Business;
using GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class RuleTypeBusiness : BaseFixture
    {
        TA_RuleTypeTableAdapter ruleTypeTA = new DatabaseGatewayTableAdapters.TA_RuleTypeTableAdapter();
        TA_RuleTemplateTableAdapter ruleTemplateTA = new DatabaseGatewayTableAdapters.TA_RuleTemplateTableAdapter();

        BRuleType businessRuleType;

        [SetUp]
        public void TestSetup()
        {
            businessRuleType = new BRuleType();
        }

        [TearDown]
        public void TreatDown()
        {
        }


        [Test]
        public void GetAllRuleTypeTest()
        {
            IList<RuleType> RuleTypes = businessRuleType.GetAll();
            Assert.AreEqual(RuleTypes.Count, ruleTypeTA.GetCount());
        }

        [Test]
        public void GetAllRuleTemplateTest()
        {
            IList<RuleType> RuleTypes = businessRuleType.GetAll();
            int count = 0;
            foreach (RuleType item in RuleTypes)
            {
                IList<RuleTemplate> RuleTemplates = item.RuleTemplateList;
                if (RuleTemplates != null)
                    count += RuleTemplates.Count;
            }
            Assert.AreEqual(ruleTemplateTA.GetCount(), count);
        }
     
    }
}
