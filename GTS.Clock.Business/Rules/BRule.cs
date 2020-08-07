using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;

namespace GTS.Clock.Business.Rules
{
    public class BRule : BaseBusiness<Rule>
    {
        const string ExceptionSrc = "GTS.Clock.Business.Rules.BRule";
        private RuleRepository CategoryRep = new RuleRepository(false);


        protected override void InsertValidate(Rule obj)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateValidate(Rule obj)
        {
            throw new NotImplementedException();
        }

        protected override void DeleteValidate(Rule obj)
        {
            throw new NotImplementedException();
        }

    }
}
