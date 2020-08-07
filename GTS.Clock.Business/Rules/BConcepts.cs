using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Business.Rules
{
    public class BConcept : BaseBusiness<SecondaryConcept>
    {
        protected override void InsertValidate(SecondaryConcept obj)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateValidate(SecondaryConcept obj)
        {
            throw new NotImplementedException();
        }

        protected override void DeleteValidate(SecondaryConcept obj)
        {
            throw new NotImplementedException();
        }
    }
}
