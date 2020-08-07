using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model
{
    public interface IPersonRepository : IRepository<Person>
    {
        void EnableEfectiveDateFilter(decimal PersonId, DateTime FromDate, DateTime ToDate, DateTime beginYear, DateTime endYear, DateTime safeFromDate, DateTime safeToDate);
        void EnableEfectiveDateFilter(decimal PersonId, DateTime FromDate, DateTime ToDate);
        void DisableEfectiveDateFilter();
        void DeleteProceedTraffic(ProceedTraffic proceedTraffic);
        void DeleteScndCnpValue(Decimal PersonID, DateTime FromDate, DateTime ToDate);
        void UpdatePTable(string Barcode, PersianDateTime date);
        Person GetByBarcode(string Barcode);
        BaseScndCnpValue GetScndCnpValueByIndex(string Index);
        decimal UpdatePersonImage(decimal personDtlId, string image);
        string GetPersonImage(decimal personDtlId);
        PersonalMonthlyReport GetPersonalMonthlyReport(decimal personId, DateTime date, int order);
        Person AttachPerson(decimal PrsId);
        IList<Person> GetPersonByPersonIdList(IList<decimal> personIdList);
    }
}
