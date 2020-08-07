using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using System.Linq;
using System.Collections;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Model;
using NHibernate.Transform;

namespace GTS.Clock.Infrastructure.Repository
{
    public class ManagerRepository : RepositoryBase<Manager>, IManagerRepository
    {
        public override string TableName
        {
            get { return "TA_Manager"; }
        }

        public ManagerRepository(bool disconnectly) 
            :base(disconnectly)
        {
        }

        #region IManagerRepository Members

        public IList<Manager> GetAllByPage(int pageIndex, int pageSize,decimal[] restirictionIds)
        {
            IList<Manager> entities = new List<Manager>();
            try
            {
                entities = base.GetByCriteriaByPage(pageIndex, pageSize, 
                    new CriteriaStruct(Utility.Utility.GetPropertyName(() => new Manager().Active), true),
                    new CriteriaStruct(Utility.Utility.GetPropertyName(() => new Manager().ID), restirictionIds, CriteriaOperation.IN));
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            return entities;
        }

        public Manager IsManager(string username)
        {
            string HQLCommand = @"from Manager where Active=1 AND Person.ID in
                        (select Person from User where UserName=:username)";
            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("username",username)
                .List<Manager>();
            if (list != null && list.Count > 0)
                return list[0];

            HQLCommand = @"from Manager where Active=1 AND OrganizationUnit.ID in (
                        select ID from OrganizationUnit where Person.ID in (
                        select Person from User where UserName=:username))";
             list = base.NHibernateSession.CreateQuery(HQLCommand)
                 .SetParameter("username", username)
                 .List<Manager>();
             if (list != null && list.Count > 0)
                 return list[0];
             return new Manager();
        }

        public IList<UnderManagment> GetAllUnderManagments(decimal managerId) 
        {
            string HQLCommand = @"select unmngt  from UnderManagment as unmngt inner join
                                  unmngt.Flow as flw 
                                  inner join flw.ManagerFlowList as mngList
                                  where mngList=:managerId and flw.IsDeleted = 0";
            IList<UnderManagment> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("managerId", managerId)
                .List<UnderManagment>();
            return list;
        }

        public int GetQuickSearchCount(string searchKey)
        {
            string HQLCommand = @"select count(mngr)  from Manager mngr
                                 left outer join mngr.Person prs 
                                 left outer join mngr.OrganizationUnit.Person organPrs 
                                 left outer join mngr.Person.OrganizationUnitList prsOrgan 
                                 where  mngr.Active=1 AND
                                 prs.FirstName + ' ' + prs.LastName like :key OR
                                 organPrs.FirstName + ' ' + organPrs.LastName like :key OR
                                 prs.BarCode like :key OR
                                 prsOrgan.Name like :key   ";

            object count = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("key", "%" + searchKey + "%")
                .List<object>().First();
            return Utility.Utility.ToInteger(count.ToString());
        }

        public int GetSearchCountByPersonName(string personName, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return 0;
            string HQLCommand = @"select count(mngr)  from Manager mngr
                                left outer join mngr.Person prs 
                                left outer join mngr.OrganizationUnit.Person organPrs 
                                where (prs.FirstName + ' ' + prs.LastName like :personName OR
                                        organPrs.FirstName + ' ' + organPrs.LastName like :personName ) 
                                AND mngr.ID in (:ids)
                                AND mngr.Active =1 ";
            object count = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("personName", "%" + personName + "%")
                .SetParameterList("ids", base.CheckListParameter(restrictIds))
                .List<object>().First();
            return Utility.Utility.ToInteger(count.ToString());
        }

        public int GetSearchCountByPersonCode(string personCode, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return 0;
            string HQLCommand = @"select count(mngr)  from Manager mngr
                                    left outer join mngr.Person prs 
                                    left outer join mngr.OrganizationUnit.Person organPrs 
                                    where (prs.BarCode =:personCode or organPrs.BarCode =:personCode) 
                                    AND mngr.ID IN (:ids) 
                                    AND mngr.Active =1 ";
            object count = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("personCode", personCode)
                .SetParameterList("ids", base.CheckListParameter(restrictIds))
                .List<object>().First();
            return Utility.Utility.ToInteger(count.ToString());
        }

        public int GetSearchCountByOrganName(string organName, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return 0;
            string HQLCommand = @"select count(mngr)  from Manager mngr
            left outer join mngr.Person.OrganizationUnitList prsOrgan 
            left outer join mngr.OrganizationUnit organ1 
            where (organ1.Name like :organName OR prsOrgan.Name like :organName )
            AND mngr.ID IN (:ids) 
            AND mngr.Active =1 ";
            object count = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("organName", organName)
                .SetParameterList("ids", base.CheckListParameter(restrictIds))
                .List<object>().First();
            return Utility.Utility.ToInteger(count.ToString());
        }

        public int GetSearchCountByQuickSearch(string searchKey, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return 0;
            string HQLCommand = @"select count(mngr)  from Manager mngr
            left outer join mngr.Person.OrganizationUnitList prsOrgan 
            left outer join mngr.OrganizationUnit organ1 
            left outer join mngr.Person prs 
            left outer join mngr.OrganizationUnit.Person organPrs 
            where (organ1.Name like :organName OR prsOrgan.Name like :organName 
                   OR 
                   prs.FirstName + ' ' + prs.LastName like :organName OR
                                        organPrs.FirstName + ' ' + organPrs.LastName like :organName 
                   OR
                   prs.BarCode =:organName or organPrs.BarCode =:organName )
            AND mngr.ID IN (:ids)  
            AND mngr.Active =1 ";
            object count = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("organName", "%" + searchKey + "%")
                .SetParameterList("ids", base.CheckListParameter(restrictIds))
                .List<object>().First();
            return Utility.Utility.ToInteger(count.ToString());
        }


        public int GetSearchCountByAccessGroupID(decimal accessGroupID,decimal[]restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return 0;
            string HQLCommand = @"SELECT mngrList from Flow flw
                                  JOIN flw.ManagerFlowList mngrList
                                  WHERE flw.AccessGroup =:accessGroup 
                                  AND flw.IsDeleted = 0
                                  AND mngrList.Manager.ID in (:ids)";
            IList<ManagerFlow> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("accessGroup", new PrecardAccessGroup() { ID = accessGroupID })
                .SetParameterList("ids", base.CheckListParameter(restrictIds))
                .List<ManagerFlow>();
            int count = 0;            
            var managers = from n in list
                           group n by n.Manager into g
                           select g;
            count = managers.Count();
            return count;
            
        }

        /// <summary>
        /// جست و جو برروی نام مدیر و نام پست سازمانی و شماره پرسنلی
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IList<Manager> GetQuickSearch(string searchKey, int pageSize, int pageIndex)
        {
            string HQLCommand = @"select mngr  from Manager mngr
                                 left outer join mngr.Person prs 
                                 left outer join mngr.OrganizationUnit.Person organPrs 
                                 left outer join mngr.Person.OrganizationUnitList prsOrgan 
                                 where mngr.Active=1 AND (
                                 prs.FirstName + ' ' + prs.LastName like :key OR
                                 organPrs.FirstName + ' ' + organPrs.LastName like :key OR
                                 prs.BarCode like :key  OR 
                                 prsOrgan.Name like :key )  ";

            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("key", "%" + searchKey + "%")               
                .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                .List<Manager>();
            return list;
        }

        public IList<Manager> GetSearchByPersonName(string personName, int pageSize, int pageIndex, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return new List<Manager>();
            string HQLCommand = @"select mngr  from Manager mngr
    left outer join mngr.Person prs 
    left outer join mngr.OrganizationUnit.Person organPrs 
    where (prs.FirstName + ' ' + prs.LastName like :personName OR
           organPrs.FirstName + ' ' + organPrs.LastName like :personName  )
           AND mngr.ID in (:ids)
           AND mngr.Active =1 ";

            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("personName", "%" + personName + "%")
                .SetParameterList("ids", base.CheckListParameter(restrictIds))
                .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                .List<Manager>();
            return list;
        }

        public IList<Manager> GetSearchByPersonCode(string personCode, int pageSize, int pageIndex, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return new List<Manager>();
            string HQLCommand = @"select mngr  from Manager mngr
    left outer join mngr.Person prs 
    left outer join mngr.OrganizationUnit.Person organPrs 
    where (prs.BarCode like :personCode or organPrs.BarCode like :personCode )
    AND mngr.ID in (:ids)
    AND mngr.Active =1 ";

            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("personCode", "%" + personCode + "%")
                .SetParameterList("ids", base.CheckListParameter(restrictIds))
                .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                .List<Manager>();
            return list;
        }

        public IList<Manager> GetSearchByOrganName(string organName, int pageSize, int pageIndex, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return new List<Manager>();
            string HQLCommand = @"select mngr from Manager mngr
            left outer join mngr.Person.OrganizationUnitList prsOrgan 
            left outer join mngr.OrganizationUnit organ1 
            where (organ1.Name like :organName OR prsOrgan.Name like :organName )
            AND mngr.ID in (:ids)
            AND mngr.Active =1 ";
            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("organName", "%" + organName + "%")
                .SetParameterList("ids", base.CheckListParameter(restrictIds))
                .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                .List<Manager>();
            return list;
        }

        public IList<Manager> GetSearchByQucikSearch(string searchKey, int pageSize, int pageIndex, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return new List<Manager>();
            string HQLCommand = @"select mngr from Manager mngr
            left outer join mngr.Person.OrganizationUnitList prsOrgan 
            left outer join mngr.OrganizationUnit organ1 
            left outer join mngr.Person prs 
            left outer join mngr.OrganizationUnit.Person organPrs 
            where (organ1.Name like :organName OR prsOrgan.Name like :organName 
                   OR 
                   prs.FirstName + ' ' + prs.LastName like :organName OR
                                        organPrs.FirstName + ' ' + organPrs.LastName like :organName 
                   OR
                   prs.BarCode =:organName or organPrs.BarCode =:organName )
            AND mngr.ID IN (:ids) 
            AND mngr.Active =1 ";
            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("organName", "%" + searchKey + "%")
                .SetParameterList("ids", base.CheckListParameter(restrictIds))
                .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                .List<Manager>();
            return list;
        }

        public IList<Manager> GetSearchByAccessGroupID(decimal accessGroupID, int pageSize, int pageIndex, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return new List<Manager>();
            string HQLCommand = @"select mngrList from Flow flw
            join flw.ManagerFlowList mngrList
            where flw.AccessGroup =:accessGroup 
            AND flw.IsDeleted = 0
            AND mngrList.Manager.ID in (:ids)
            AND mngrList.Manager.Active =1 ";
            IList<ManagerFlow> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("accessGroup", new PrecardAccessGroup() { ID = accessGroupID })
                .SetParameterList("ids", base.CheckListParameter(restrictIds))
                .List<ManagerFlow>();
            int count = 0;
            var managers = from n in list
                           group n by n.Manager into g
                           select g;
            IList<Manager> managerList = managers.Select(x => x.Key).ToList<Manager>();
            return managerList;
        }

        public IList<Manager> GetFlowManagers(decimal flowId)
        {
            //if (restrictIds == null || restrictIds.Length == 0)
            //    return new List<Manager>();
            string HQLCommand = @"select mngr  from ManagerFlow mngrFlow
            join mngrFlow.Manager mngr
            where mngrFlow.Flow=:flow
            and mngrFlow.Flow.IsDeleted = 0           
            and mngr.Active=1
            and mngrFlow.Active=1
            order by mngrFlow.Level";
            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("flow", new Flow() { ID = flowId })
                //.SetParameterList("ids", base.CheckListParameter(restrictIds))
                .List<Manager>();
            return list;
        }

        /// <summary>
        /// یک مدیر زا با توجه به پست سازمانی بین مدیران و پرسنل جستجو میکند
        /// اگر مدیر با شناسه پرسنل ذخیره شده بود آنرا با پست سازمانی ذخیره میکند
        /// </summary>
        /// <param name="organID"></param>
        /// <returns></returns>
        public Manager GetManagerByOrganID(decimal organID) 
        {
            string HQLCommand = @"select mngr from Manager mngr
            left outer join mngr.Person.OrganizationUnitList prsOrgan 
            left outer join mngr.OrganizationUnit organ1 
            where mngr.Active=1 AND (organ1.ID = :organID OR prsOrgan.ID = :organID )";
            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("organID", organID)              
                .List<Manager>();
            Manager manager= list.FirstOrDefault();
            if (manager != null && manager.ID > 0 && manager.OrganizationUnit==null)
            {
                OrganizationUnitRepository organRep = new OrganizationUnitRepository(false);
                OrganizationUnit unit= organRep.GetByPersonId(manager.Person.ID);
                if (unit != null && unit.ID > 0)
                {
                    manager.OrganizationUnit = unit;
                    manager.Person = null;
                    base.Update(manager);
                }
            }
            return manager;
        }

        public Manager GetManagerByPersonID(decimal personID)
        {
            string HQLCommand = @"select mngr  from Manager mngr
    left outer join mngr.Person prs 
    left outer join mngr.OrganizationUnit.Person organPrs 
    where  mngr.Active=1 AND (prs.ID = :personID or organPrs.ID = :personID )";

            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("personID", personID)
                .List<Manager>();
            return list.FirstOrDefault();
        }

        public IList<UnderManagementPerson> GetUnderManagmentByDepartment(GridSearchFields SearchField, decimal personId, decimal departmentID, string personName, string personbarCode,int dateRangeOrder,int dateRangeOrderIndex, string CurrentDateTime, GridOrderFields order,GridOrderFieldType orderType, int pageIndex, int pageSize)
        {
            string orderingType = orderType.ToString();
            string orderingField = order.ToString();
            return NHibernateSession.GetNamedQuery("GetUnderManagementPersonList")
                                    .SetParameter("personId", personId)
                                    .SetParameter("departmentId", departmentID)
                                    .SetParameter("searchField", (int)SearchField)
                                    .SetParameter("barcodeParam", personbarCode)
                                    .SetParameter("personNameParam", personName)
                                    .SetParameter("orderByField", orderingField)
                                    .SetParameter("orderType", orderingType)
                                    .SetParameter("dateRangeOrder",dateRangeOrder)
                                    .SetParameter("dateRangeOrderIndex", dateRangeOrderIndex)
                                    .SetParameter("curentDate", CurrentDateTime)
                                    .SetParameter("pageSize", pageSize)
                                    .SetParameter("pageIndex", pageIndex)
                                    .List<UnderManagementPerson>();
        }

        public IList<UnderManagementPerson> GetUnderManagmentOperatorByDepartment(GridSearchFields SearchField, decimal oprPersonId, decimal departmentID, string personName, string personbarCode, int dateRangeOrder, int dateRangeOrderIndex, string CurrentDateTime, GridOrderFields order, GridOrderFieldType orderType, int pageIndex, int pageSize)
        {
            string orderingType = orderType.ToString();
            string orderingField = order.ToString();
            return NHibernateSession.GetNamedQuery("GetUnderManagementOperatorPersonList")
                                    .SetParameter("oprPersonId", oprPersonId)
                                    .SetParameter("departmentId", departmentID)
                                    .SetParameter("searchField", (int)SearchField)
                                    .SetParameter("barcodeParam", personbarCode)
                                    .SetParameter("personNameParam", personName)
                                    .SetParameter("orderByField", orderingField)
                                    .SetParameter("orderType", orderingType)
                                    .SetParameter("dateRangeOrder", dateRangeOrder)
                                    .SetParameter("dateRangeOrderIndex", dateRangeOrderIndex)
                                    .SetParameter("curentDate", CurrentDateTime)
                                    .SetParameter("pageSize", pageSize)
                                    .SetParameter("pageIndex", pageIndex)
                                    .List<UnderManagementPerson>();
        }

        /// <summary>
        /// پرسنل تحت مدیریت قبلا استخراج شده اند
        /// </summary>
        /// <param name="dateRangeOrder"></param>
        /// <param name="dateRangeOrderIndex"></param>
        /// <param name="CurrentDateTime"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<UnderManagementPerson> GetUnderManagment(int dateRangeOrder, int dateRangeOrderIndex, string CurrentDateTime,IList<decimal> underMngList ,int pageIndex, int pageSize)
        {
            if (underMngList != null && underMngList.Count > 0)
            {
                return NHibernateSession.GetNamedQuery("GetUnderManagementPersonListByList")
                                        .SetParameter("dateRangeOrder", dateRangeOrder)
                                        .SetParameter("dateRangeOrderIndex", dateRangeOrderIndex)
                                        .SetParameter("curentDate", CurrentDateTime)
                                        .SetParameter("pageSize", pageSize)
                                        .SetParameter("pageIndex", pageIndex)
                                        .SetParameterList("underMngList", underMngList.ToArray())
                                        .List<UnderManagementPerson>();
            }
            return new List<UnderManagementPerson>();
        }

        public int GetUnderManagmentByDepartmentCount(GridSearchFields SearchField, decimal personId, decimal departmentID, string personName, string personbarCode) 
        {
            return NHibernateSession.GetNamedQuery("GetUnderManagementPersonCount")
                                    .SetParameter("personId", personId)
                                    .SetParameter("departmentId", departmentID)
                                    .SetParameter("searchField", SearchField)
                                    .SetParameter("barcodeParam", personbarCode)
                                    .SetParameter("personNameParam", personName)
                                    .UniqueResult<int>();
        }

        public int GetUnderManagmentOperatorByDepartmentCount(GridSearchFields SearchField, decimal oprPersonId, decimal departmentID, string personName, string personbarCode)
        {
            return NHibernateSession.GetNamedQuery("GetUnderManagementOperatorPersonCount")
                                    .SetParameter("oprPersonId", oprPersonId)
                                    .SetParameter("departmentId", departmentID)
                                    .SetParameter("searchField", SearchField)
                                    .SetParameter("barcodeParam", personbarCode)
                                    .SetParameter("personNameParam", personName)
                                    .UniqueResult<int>();
        }

        /// <summary>
        /// لیست افرادی که مدیر هستند را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<Person> GetAllManager() 
        {
            IList<Manager> list = this.GetAll().Where(x => x.Active == true).ToList();
            var persons = from mng in list
                          select mng.ThePerson;
            IList<Person> result = persons.ToList<Person>();
            return result;
        }

        /// <summary>
        /// تمام پیشکارتهایی که مدیر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="managerPersonID">شناسه پرسنلی مدیر</param>
        /// <returns></returns>
        public IList<Precard> GetAllAccessGroup(decimal managerID)
        {

            string SQLCommand = @"select distinct p.*
                                    from TA_ManagerFlow
                                    join TA_Flow flow on Flow_ID =mngrFlow_FlowID
                                    join TA_PrecardAccessGroup on accessGrp_ID= Flow_AccessGroupID
                                    join TA_PrecardAccessGroupDetail on accessGrpDtl_AccessGrpId = accessGrp_ID
                                    join TA_Precard as p on  Precrd_ID=accessGrpDtl_PrecardId 
                                    where mngrFlow_ManagerID=:managerId and flow_Deleted = 0";

            IList<Precard> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .AddEntity(typeof(Precard))
                .SetParameter("managerId", managerID)
                .List<Precard>();
            return list;
        }

        #endregion

        public IList<decimal> GetAllManagerPersons(IList<decimal> condidatePersonsId) 
        {
            string SQLCommand = @"select case when isnull(MasterMng_PersonID,0) > isnull(MasterMng_OrganizationUnitID,0)
                                then isnull(MasterMng_PersonID,0)
                                else isnull(MasterMng_OrganizationUnitID,0)
                                end
                                from TA_Manager
                                left outer join TA_OrganizationUnit on organ_ID=MasterMng_OrganizationUnitID
                                where MasterMng_Active=1 AND (
                                MasterMng_PersonID in (:condidatePersonsId)  OR 
                                organ_PersonID in (:condidatePersonsId))";
            IList<decimal> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
               .SetParameterList("condidatePersonsId", base.CheckListParameter( condidatePersonsId))
               .List<decimal>();
            return list;
        }

        /// <summary>
        /// اگر مدیری در جریانی استفاده نشده باشد آنرا غیر فعال میکند
        /// </summary>
        public void SetManagerActivation() 
        {
            string SQLCommand = @"update TA_Manager
                                set MasterMng_Active=0
                                where MasterMng_ID not in (select mngrFlow_ManagerID from TA_ManagerFlow where mngrFlow_Active=1)";
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
               .ExecuteUpdate();

            SQLCommand = @"update TA_Manager
                                set MasterMng_Active=1
                                where MasterMng_ID in (select mngrFlow_ManagerID from TA_ManagerFlow where mngrFlow_Active=1)";
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
               .ExecuteUpdate();
        }
    }

}
