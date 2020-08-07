using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Model.Concepts
{
    public interface ICalculationDateRangeRepository : IRepository<CalculationDateRange>
    {
        IList<CalculationDateRange> GetCalculationDateRanges(CalculationRangeGroup calculationGroup, IList<SecondaryConcept> concepts);
    }
}
