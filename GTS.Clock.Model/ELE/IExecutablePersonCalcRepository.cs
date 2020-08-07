using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model
{
    public interface IExecutablePersonCalcRepository : IRepository<ExecutablePersonCalculation>
    {
        ExecutablePersonCalculation GetByBarcode(string Barcode, DateTime Date);
        ExecutablePersonCalculation GetByPrsId(decimal PrsId, DateTime Date);
        IList<ExecutablePersonCalculation> GetAll(DateTime Date);
        IList<ExecutablePersonCalculation> GetAllByPrsIds(IList<decimal> Persons, DateTime Date);
    }
}
