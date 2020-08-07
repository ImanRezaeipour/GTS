using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.MonthlyReport;

namespace GTS.Clock.Model.RequestFlow
{
    public interface IManagerRepository : IRepository<Manager>
    {
        Manager IsManager(string username);

        int GetSearchCountByPersonName(string personName, decimal[] ids);
        int GetSearchCountByPersonCode(string personCode, decimal[] ids);
        int GetSearchCountByOrganName(string organName, decimal[] ids);
        IList<Manager> GetSearchByPersonName(string personName, int pageSize, int pageIndex, decimal[] ids);
        IList<Manager> GetSearchByPersonCode(string personCode, int pageSize, int pageIndex, decimal[] ids);
        IList<Manager> GetSearchByOrganName(string organName, int pageSize, int pageIndex, decimal[] ids);
        IList<UnderManagementPerson> GetUnderManagmentByDepartment(GridSearchFields SearchField, decimal managerID, decimal departmentID, string personName, string personbarCode, int dateRangeOrder, int dateRangeOrderIndex, string CurrentDateTime, GridOrderFields order, GridOrderFieldType orderType, int pageIndex, int pageSize);
        int GetUnderManagmentByDepartmentCount(GridSearchFields SearchField, decimal managerID, decimal departmentID, string personName, string personbarCode);
         
    }
}
