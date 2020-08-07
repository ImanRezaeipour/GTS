using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure;
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
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Proxy;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class MorePrsTest 
    {
      
        [SetUp]
        public void TestSetup()
        {
           
        }

        [TearDown]
        public void TreatDown()
        {
           
        }

        [Test]
        public void GetByID_Test()
        {
            try
            {
                BPerson bus = new BPerson();
                int a = bus.GetPersonCountTest();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        [Test]
        public void GetByID_Test2()
        {
            try
            {
                PersonAdvanceSearchProxy p = new PersonAdvanceSearchProxy();
                p.Military = MilitaryStatus.GheireMashmool;
                ISearchPerson bus = new BPerson();
                bus.QuickSearchByPage(0, 10, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

    }
}
