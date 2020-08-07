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
    public class UtilityTest
    {
        [Test]
        public void CharecterCounter_test() 
        {
            Assert.AreEqual(3, Utility.CharOccuranceCount("@absd@sdffa@", "@"));
        }
    }
}
