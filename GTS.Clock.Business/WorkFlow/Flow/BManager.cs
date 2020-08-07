using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Business;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business.Charts;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Business.RequestFlow
{
    public class BManager : BaseBusiness<Manager>
    {
        IDataAccess accessPort = new BUser();
        const string ExceptionSrc = "GTS.Clock.Business.RequestFlow.BManager";
        ManagerRepository managerRep = new ManagerRepository(false);
        FlowRepository flowRep = new FlowRepository(false);
        BFlow bFlow = new BFlow();

        #region Master Manager Form

        /// <summary>
        /// تعداد مدیر را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public override int GetRecordCount()
        {
            IList<decimal> ids = accessPort.GetAccessibleManagers();
            int count = managerRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Manager().ID), ids.ToArray(), CriteriaOperation.IN));
            return count;
        }

        /// <summary>
        /// مدیرانی که در جریان کاری با دسترسی تعیین شده باشند را برمیگرداند
        /// </summary>
        /// <param name="accessGroupID"></param>
        /// <returns></returns>
        public virtual int GetRecordCountByAccessGroupFilter(decimal accessGroupID)
        {
            try
            {
                int count = 0;
                if (accessGroupID != 0)
                {
                    IList<decimal> ids = accessPort.GetAccessibleManagers();
                    count = managerRep.GetSearchCountByAccessGroupID(accessGroupID, ids.ToArray());
                }
                else
                {
                    count = base.GetRecordCount();
                }
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetRecordCountByAccessGroupFilter");
                throw ex;
            }
        }

        /// <summary>
        /// براساس کلمه کلیدی تعداد نتایج جستجورا برمیگرداند
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public virtual int GetRecordCountBySearch(string searchKey, ManagerSearchFields field)
        {
            try
            {
                int managerCount = 0;
                IList<decimal> ids = accessPort.GetAccessibleManagers();
                switch (field)
                {
                    case ManagerSearchFields.PersonCode:
                        managerCount = managerRep.GetSearchCountByPersonCode(searchKey,ids.ToArray());
                        break;
                    case ManagerSearchFields.PersonName:
                        managerCount = managerRep.GetSearchCountByPersonName(searchKey,ids.ToArray());
                        break;
                    case ManagerSearchFields.OrganizationUnitName:
                        managerCount = managerRep.GetSearchCountByOrganName(searchKey, ids.ToArray());
                        break;
                    case ManagerSearchFields.NotSpecified:
                        managerCount = managerRep.GetSearchCountByQuickSearch(searchKey, ids.ToArray());
                        break;
                }
                return managerCount;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetRecordCountBySearch");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public override IList<Manager> GetAllByPage(int pageIndex, int pageSize)
        {
            try
            {
                IList<decimal> ids = accessPort.GetAccessibleManagers();
                int count = this.GetRecordCount();
                if (pageSize > 0 && pageIndex <= (int)(count / pageSize))
                {
                    IList<Manager> result = managerRep.GetAllByPage(pageIndex, pageSize, ids.ToArray());
                    return result;
                }
                else
                {
                    throw new OutOfExpectedRangeException("0", Convert.ToString(count - 1), Convert.ToString(pageSize * (pageIndex + 1)), "BManager -> GetAllByPage ");
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetAllByPage");
                throw ex;
            }
        }

        /// <summary>
        ///نتایج فیلتر بر اساس گروه دسترسی را صفحه به صفحه برمیگرداند          
        /// </summary>
        public IList<Manager> SearchByAccessGroup(decimal accessGroupID, int pageIndex, int pageSize)
        {
            try
            {
                IList<Manager> managers = new List<Manager>();
                if (accessGroupID != 0)
                {
                    IList<decimal> ids = accessPort.GetAccessibleManagers();
                    managers = managerRep.GetSearchByAccessGroupID(accessGroupID, pageSize, pageIndex, ids.ToArray());
                }
                else
                {
                    managers = GetAllByPage(pageIndex, pageSize);
                }
                return managers;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "SearchByAccessGroup");
                throw ex;
            }
        }

        /// <summary>
        ///نتایج جستجو را صفحه به صفحه برمیگرداند          
        /// </summary>
        public IList<Manager> SearchByPage(string searchKey, ManagerSearchFields field, int pageIndex, int pageSize)
        {
            try
            {
                IList<decimal> ids = accessPort.GetAccessibleManagers();
                IList<Manager> managers = new List<Manager>();
                switch (field)
                {
                    case ManagerSearchFields.PersonCode:
                        managers = managerRep.GetSearchByPersonCode(searchKey, pageSize, pageIndex, ids.ToArray());
                        break;
                    case ManagerSearchFields.PersonName:
                        managers = managerRep.GetSearchByPersonName(searchKey, pageSize, pageIndex, ids.ToArray());
                        break;
                    case ManagerSearchFields.OrganizationUnitName:
                        managers = managerRep.GetSearchByOrganName(searchKey, pageSize, pageIndex, ids.ToArray());
                        break;
                    case ManagerSearchFields.NotSpecified:
                        managers = managerRep.GetSearchByQucikSearch(searchKey, pageSize, pageIndex, ids.ToArray());
                        break;
                }
                return managers;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "SearchByPage");
                throw ex;
            }
        }

        /// <summary>
        /// با توجه به نام کاربری اگر مدیری وجود داشته باشد آنرا برمیگرداند
        /// درصورتی که در رکورد مدیر بجای شناسه شخص از پست سازمانی استفاده شده باشد
        /// خصیصه شخص را مقداردهی میکند
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Manager GetManagerByUsername(string username)
        {
            try
            {
                Manager manager = managerRep.IsManager(username);
                if (manager.ID > 0 && manager.Person == null)
                {
                    if (manager.OrganizationUnit != null && manager.OrganizationUnit.Person != null)
                    {
                        manager.Person = manager.OrganizationUnit.Person;
                    }
                    else
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.ManagerOrganizationUnitProblem, "پست سازمانی یا شخص مربوط به پست سازمانی منتسب به مدیر بدرستی مقداردهی نشده است", ExceptionSrc);
                    }
                }
                return manager;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetManagerByUsername");
                throw ex;
            }
        }

        /// <summary>
        /// جزئیات شامل سطح دسترسی و غیره را برمیگرداند
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public IList<Flow> GetManagerDetail(decimal managerId)
        {          
            return bFlow.GetAllFlowByManager(managerId);
        }

        /// <summary>
        /// مدیران یک جریان را برمیگرداند
        /// بر اساس اولویت مرتب میشوند
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<Manager> GetManagerFlow(decimal flowID)
        {
            try
            {
                IList<Manager> list = new List<Manager>();
                if (flowID > 0)
                {
                    IList<decimal> ids = accessPort.GetAccessibleManagers();
                    list = managerRep.GetFlowManagers(flowID, ids.ToArray());
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetManagerFlow");
                throw ex;
            }
        }

        /// <summary>
        /// لیستی از شناسه مدیران میگیرد و شناسه پرسنل برمیگرداند
        /// </summary>
        /// <param name="managerIds"></param>
        /// <returns></returns>
        public IList<decimal> GetManagerPersons(IList<decimal> managerIds) 
        {
            IList<decimal> result = new List<decimal>();
            foreach (decimal id in managerIds) 
            {
                Manager mng = this.GetByID(id);
                if (mng != null && mng.Active)
                    result.Add(mng.ThePerson.ID);
            }
            return result;
        }

        /// <summary>
        /// اگر شخص مدیر باشد آنرا برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Manager GetManager(decimal personId) 
        {
            Manager mng = managerRep.GetManagerByPersonID(personId);
            if (mng == null) mng = new Manager();
            return mng;
        }

        #endregion

        #region Manager Detail Form

        public IList<UnderManagment> GetAllUnderManagments(decimal flowId)
        {
            if (flowId > 0) 
            {
                return flowRep.GetAllUnderManagments(flowId);
            }
            return new List<UnderManagment>();
        }

        /// <summary>
        /// تمام سطوح دسترسی را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<PrecardAccessGroup> GetAllAccessGroups()
        {
            try
            {
                EntityRepository<PrecardAccessGroup> rep = new EntityRepository<PrecardAccessGroup>(false);
                return rep.GetAll();
            }            
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetAllAccessGroups");
                throw ex;
            }
        }

        /// <summary>
        /// پرسنل یک بخش را برمیگرداند
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public IList<Person> GetDepartmentPerson(decimal departmentID)
        {
            try
            {
                Charts.BDepartment bdep = new GTS.Clock.Business.Charts.BDepartment();
                Department dep = bdep.GetByID(departmentID);
                return dep.PersonList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetDepartmentPerson");
                throw ex;
            }
        }

        /// <summary>
        /// ریشه درخت را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public Department GetDepartmentRoot()
        {
            try
            {
                Charts.BDepartment bDep = new GTS.Clock.Business.Charts.BDepartment();
                return bDep.GetDepartmentsTree();
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetDepartmentRoot");
                throw ex;
            }
        }

        /// <summary>
        /// بچه های یک بخش را برمیگرداند
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentChilds(decimal nodeID)
        {
            try
            {
                Charts.BDepartment bDep = new GTS.Clock.Business.Charts.BDepartment();
                return bDep.GetDepartmentChilds(nodeID);
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetDepartmentChilds");
                throw ex;
            }
        }

        /// <summary>
        /// درج جریان کاری
        /// </summary>
        /// <param name="personId">شناسه مدیر اولیه</param> 
        /// <param name="accessGroup">شناسه گروه دسترسی</param>        
        /// <param name="flowName">نام جریان کاری</param>
        /// <param name="underMnagList">افراد و بخشهای تحت مدیریت</param>
        /// <returns></returns>
        public decimal InsertFlowByPerson(decimal personId, decimal accessGroup, string flowName, IList<UnderManagment> underMnagList)
        {
            Flow flow = new Flow();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    flow.FlowName = flowName;
                    flow.AccessGroup = new PrecardAccessGroup() { ID = accessGroup };
                    flow.ActiveFlow = true;
                    flow.ManagerFlowList = new List<ManagerFlow>();
                    flow.UnderManagmentList = new List<UnderManagment>();

                    Manager mng = managerRep.GetManagerByPersonID(personId);
                    if (mng == null || mng.ID == 0)
                    {
                        Manager manager = new Manager();
                        manager.Person = new Person() { ID = personId };
                        manager.Active = true;
                        this.SaveChanges(manager, UIActionType.ADD);
                        mng = manager;
                    }

                    ManagerFlow managerFlow = new ManagerFlow();
                    managerFlow.Active = true;
                    managerFlow.Flow = flow;
                    managerFlow.Manager = mng;
                    managerFlow.Level = 1;
                    flow.ManagerFlowList.Add(managerFlow);

                    foreach (UnderManagment un in underMnagList)
                    {
                        un.Flow = flow;
                        flow.UnderManagmentList.Add(un);
                    }


                    BFlow bflow = new BFlow();
                    bflow.SaveChanges(flow, UIActionType.ADD);
                    return flow.ID;

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return flow.ID;
                }

                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BManager", "InsertFlowByPerson");
             
                    throw ex;
                }
            }
        }

        /// <summary>
        /// درج جریان کاری
        /// </summary>
        /// <param name="personId">شناسه پست سلزمانی مدیر اولیه</param>
        /// <param name="accessGroup">شناسه گروه دسترسی</param>
        /// <param name="flowName">نام جریان کاری</param>        
        /// <param name="underMnagList">افراد و بخشهای تحت مدیریت</param>
        /// <returns></returns>
        public decimal InsertFlowByOrganization(decimal organizationId, decimal accessGroup, string flowName, IList<UnderManagment> underMnagList)
        {
            Flow flow = new Flow();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    flow.FlowName = flowName;
                    flow.AccessGroup = new PrecardAccessGroup() { ID = accessGroup };
                    flow.ActiveFlow = true;
                    flow.ManagerFlowList = new List<ManagerFlow>();
                    flow.UnderManagmentList = new List<UnderManagment>();

                    Manager mng = managerRep.GetManagerByOrganID(organizationId);
                    if (mng==null || mng.ID == 0)
                    {
                        Manager manager = new Manager();
                        manager.OrganizationUnit = new OrganizationUnit() { ID = organizationId };
                        manager.Active = true;
                        this.SaveChanges(manager, UIActionType.ADD);
                        mng = manager;
                    }

                    ManagerFlow managerFlow = new ManagerFlow();
                    managerFlow.Flow = flow;
                    managerFlow.Manager = mng;
                    managerFlow.Level = 1;
                    managerFlow.Active = true;
                    flow.ManagerFlowList.Add(managerFlow);

                    foreach (UnderManagment un in underMnagList)
                    {
                        un.Flow = flow;
                        flow.UnderManagmentList.Add(un);
                    }


                    BFlow bflow = new BFlow();
                    bflow.SaveChanges(flow, UIActionType.ADD);                   

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return flow.ID;
                }

                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BManager", "InsertFlowByOrganization");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// بروزرسانی یک جریان
        /// </summary>
        /// <param name="flowID">شناسه جریان</param>
        /// <param name="accessGroupId">گروه دسترسی</param>
        /// <param name="flowName">نام جریان</param>
        /// <param name="underMnagList">لیست افراد تحت مدیریت</param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateFlow(decimal flowID, decimal accessGroupId, string flowName, IList<UnderManagment> underMnagList) 
        {
            try
            {
                BFlow bflow = new BFlow();
                Flow flow = bflow.GetByID(flowID);
                flow.ID = flowID;
                flow.FlowName = flowName;
                flow.AccessGroup = new PrecardAccessGroup() { ID = accessGroupId };
                flow.UnderManagmentList = new List<UnderManagment>();
                foreach (UnderManagment un in underMnagList)
                {
                    un.Flow = flow;
                    flow.UnderManagmentList.Add(un);
                }
                UnderManagmentRepository underRep = new UnderManagmentRepository(false);
                underRep.DeleteUnderManagments(flow.ID);
                bflow.SaveChanges(flow, UIActionType.EDIT);
                return flow.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "UpdateFlow");
                throw ex;
            }
        }

        /// <summary>
        /// یک جریان کاری را حذف میکند
        /// </summary>
        /// <param name="flowID"></param>
        public void DeleteFlow(decimal flowID) 
        {
            Flow flow = new Flow();
            flow.ID = flowID;
            bFlow.SaveChanges(flow, UIActionType.DELETE);
        }

        /// <summary>
        /// یک مدیر را حذف میکند
        /// </summary>
        /// <param name="managerID"></param>
        public void DeleteManager(decimal managerID) 
        {
            Manager manager = new Manager() { ID = managerID };
            this.SaveChanges(manager, UIActionType.DELETE);
        }
        #endregion

        #region Manager Flow From

        #region Manager Search
        public int QuickSearchPersonCount(string searchKey)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInQuickSearchCount(searchKey);
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "QuickSearchPersonCount");
                throw ex;
            }
        }

        /// <summary>
        /// در بین پرسنلی جستجوی سریع انجام میدهد
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<Person> QuickSearchPersonByPage(string searchKey, int pageIndex, int pageSize)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                IList<Person> list = searchTool.QuickSearchByPage(pageIndex, pageSize, searchKey);              
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "QuickSearchPersonByPage");
                throw ex;
            }
        }

        public OrganizationUnit GetOrganizationUnitTree()
        {
            try
            {
                Charts.BOrganizationUnit borgan = new GTS.Clock.Business.Charts.BOrganizationUnit();
                return borgan.GetOrganizationUnitTree();
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetOrganizationUnitTree");
                throw ex;
            }
        }

        public IList<OrganizationUnit> GetOrganizationUnitChilds(decimal parentId)
        {
            try
            {
                Charts.BOrganizationUnit borgan = new GTS.Clock.Business.Charts.BOrganizationUnit();
                return borgan.GetChilds(parentId);
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetOrganizationUnitChilds");
                throw ex;
            }
        }

        /// <summary>
        /// پستهای سازمانی را با نام جستجو میکند
        /// ریشه نباید جزو مجموعه جواب باشد
        /// </summary>
        /// <param name="organName"></param>
        /// <returns></returns>
        public IList<OrganizationUnit> QuickSearchByOrganizationUnitName(string organName)
        {
            try
            {
                IList<OrganizationUnit> list = new BOrganizationUnit().SearchByUnitName(organName);
                foreach (OrganizationUnit organ in list)
                {
                    if (organ.Person == null)
                        organ.Person = new Person();
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "QuickSearchByOrganizationUnitName");
                throw ex;
            }
        }
        
        #endregion



        #endregion

        /// <summary>
        /// - شخص یا پست باید انتحاب شود
        /// </summary>
        /// <param name="manager"></param>
        protected override void InsertValidate(Manager manager)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if ((manager.Person == null || manager.Person.ID == 0) 
                && 
                (manager.OrganizationUnit == null || manager.OrganizationUnit.ID == 0))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ManagerOwnerNotSpecified, "برای مدیر شخص یا پست سازمانی مشخص نشده است", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(Manager obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// - نباید مدیر در جریان کار استفاده شده باشد
        /// </summary>
        /// <param name="manager"></param>
        protected override void DeleteValidate(Manager manager)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            EntityRepository<ManagerFlow> rep = new EntityRepository<ManagerFlow>(false);
            int count = rep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ManagerFlow().Manager), manager));
            if (count > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ManagerUsedByFlow, "مدیر در سلسله مراتب جریان کار استفاده شده است و امکان حذف آن نیست", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void Insert(Manager manager)
        {
            managerRep.WithoutTransactSave(manager);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertFlowByManagerCreator(ManagerCreator MC, decimal targetManagerCreatorID, decimal accessGroupID, string flowName, IList<UnderManagment> UnderManagmentList)
        {
            decimal flowID = 0;
            switch (MC)
            {
                case ManagerCreator.Personnel:
                    flowID = this.InsertFlowByPerson(targetManagerCreatorID, accessGroupID, flowName, UnderManagmentList);
                    break;
                case ManagerCreator.OrganizationPost:
                    flowID = this.InsertFlowByOrganization(targetManagerCreatorID, accessGroupID, flowName, UnderManagmentList);
                    break;
                case ManagerCreator.None:
                    break;
            }
            return flowID;
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckManagesLoadAccess()
        { 
        }


      
    }
}
