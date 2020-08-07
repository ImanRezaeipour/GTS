using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Business
{
    public interface ISearchPerson
    {
        int GetPersonCount();

        int GetPersonInAdvanceSearchCount(PersonAdvanceSearchProxy proxy);
        int GetPersonInAdvanceSearchCount(PersonAdvanceSearchProxy proxy, PersonCategory searchInCategory);

        int GetPersonInQuickSearchCount(string searchValue);
        int GetPersonInQuickSearchCount(string searchValue, PersonCategory searchInCategory);

        IList<Person> GetAllPerson(int pageIndex, int pageSize);

        IList<Person> GetPersonInAdvanceSearch(Business.Proxy.PersonAdvanceSearchProxy proxy);        
        IList<Person> GetPersonInAdvanceSearch(Business.Proxy.PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize);
        IList<Person> GetPersonInAdvanceSearch(Business.Proxy.PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize, PersonCategory searchInCategory);
        IList<Person> GetPersonInAdvanceSearchApplyCulture(Business.Proxy.PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize);

        IList<Person> QuickSearch(string searchValue, PersonCategory searchInCategory);
        IList<Person> QuickSearchByPage(int pageIndex, int pageSize, string searchValue);
        IList<Person> QuickSearchByPage(int pageIndex, int pageSize, string searchValue, PersonCategory searchInCategory);
        IList<Person> QuickSearchByPageApplyCulture(int pageIndex, int pageSize, string searchValue);

        IList<Person> GetPersonByPersonIdList(IList<decimal> personIdList);

        #region AdvanceSearch Items
        Department GetDepartmentRoot();
        IList<Department> GetAllDepartments();
        IList<Department> GetDepartmentChild(decimal parentId,IList<Department> allDepartmens);
        IList<Department> GetDepartmentChild(decimal parentId);
        OrganizationUnit GetOrganizationRoot();
        IList<OrganizationUnit> GetOrganizationChild(decimal parentId);
        IList<ControlStation> GetAllControlStation();
        IList<WorkGroup> GetAllWorkGroup();
        IList<RuleCategory> GetAllRuleGroup();
        IList<CalculationRangeGroup> GetAllDateRanges();
        IList<EmploymentType> GetAllEmploymentTypes();

        #endregion
    }
}
