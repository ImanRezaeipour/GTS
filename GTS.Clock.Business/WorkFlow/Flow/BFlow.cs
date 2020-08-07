using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.RequestFlow
{
    public class BFlow : BaseBusiness<Flow>, IUnderManagmentTree
    {
        IDataAccess accessPort = new BUser();
        const string ExceptionSrc = "GTS.Clock.Business.RequestFlow.BFlow";
        FlowRepository flowRep = new FlowRepository(false);
        BDepartment bDep = new BDepartment();
        BUnderManagment bUnderManagment = new BUnderManagment();

        #region Manager Flow
      
        /// <summary>
        /// لیستی از مدیران یک جریان به همراه اولویت هر یک را برمیگرداند
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        public IList<ManagerProxy> GetAllManagers(decimal flowID)
        {
            try
            {
                EntityRepository<ManagerFlow> managerFlowRep = new EntityRepository<ManagerFlow>(false);
                IList<ManagerProxy> list = new List<ManagerProxy>();
                IList<ManagerFlow> mnagers = managerFlowRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ManagerFlow().Flow), new Flow() { ID = flowID }),
                                                                          new CriteriaStruct(Utility.GetPropertyName(() => new ManagerFlow().Active), true));
                foreach (ManagerFlow mngFlow in mnagers)
                {
                    ManagerProxy proxy = new ManagerProxy();
                    if (mngFlow.Manager.ManagerType == ManagerAssignType.Person)
                    {
                        proxy.ManagerType = ManagerType.Person;
                        proxy.OwnerID = mngFlow.Manager.Person.ID;
                        proxy.Name = mngFlow.Manager.Person.Name;
                    }
                    else if (mngFlow.Manager.ManagerType == ManagerAssignType.OrganizationUnit)
                    {
                        proxy.ManagerType = ManagerType.OrganizationUnit;
                        proxy.OwnerID = mngFlow.Manager.OrganizationUnit.ID;
                        proxy.Name = mngFlow.Manager.OrganizationUnit.Name;
                    }
                    else
                    {
                        proxy.ManagerType = ManagerType.None;
                    }
                    proxy.ID = mngFlow.ID;
                    proxy.Level = mngFlow.Level;
                    list.Add(proxy);
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetAllManagers");
                throw ex;
            }
        }

        /// <summary>
        /// بروزرسانی مدیرهای یک جریان خاص
        /// </summary>
        /// <param name="flowId">کد جریان</param>
        /// <param name="activeFlow">وضعیت فعال بودن</param>
        /// <param name="mngrFlows">مدیران جریان کاری</param>
        public void UpdateManagerFlows(decimal flowId, bool activeFlow, bool mainFlow, IList<ManagerProxy> mngrFlows)
        {
            ManagerRepository managerRep = new ManagerRepository(false);
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    if (Utility.IsEmpty(mngrFlows))
                    {
                        UIValidationExceptions exception = new UIValidationExceptions();
                        exception.Add(new ValidationException(ExceptionResourceKeys.FlowMustHaveOneManagerFlow, "جریان کاری باید حداقل دارای یک مدیر در خود باشد", ExceptionSrc));
                        throw exception;
                    }
                    
                    BManager bManager = new BManager();
                    Flow flow = base.GetByID(flowId);
                    flow.ActiveFlow = activeFlow;
                    flow.MainFlow = mainFlow;
                    flow.ManagerFlowList = new List<ManagerFlow>();
                    flowRep.DeleteManagerFlows(flowId);
                    foreach (ManagerProxy mp in mngrFlows)
                    {
                        ManagerFlow managerFlow = new ManagerFlow();
                        Manager mng;
                        if (mp.OwnerID == 0)
                        {
                            UIValidationExceptions exception = new UIValidationExceptions();
                            exception.Add(new ValidationException(ExceptionResourceKeys.FlowPersonOrOrganizationMustSpecified, "یا شخص یا پست سازمانی باید مقداردهی شود", ExceptionSrc));
                            throw exception;
                        }
                        if (mp.ManagerType == ManagerType.Person)
                        {
                            mng = managerRep.GetManagerByPersonID(mp.OwnerID);
                            if (mng == null || mng.ID == 0)
                            {
                                Manager manager = new Manager();
                                manager.Person = new Person() { ID = mp.OwnerID };
                                manager.Active = true;
                                bManager.SaveChanges(manager, UIActionType.ADD);
                                mng = manager;
                            }
                        }
                        else
                        {
                            mng = managerRep.GetManagerByOrganID(mp.OwnerID);
                            if (mng == null || mng.ID == 0)
                            {
                                Manager manager = new Manager();
                                manager.OrganizationUnit = new OrganizationUnit() { ID = mp.OwnerID };
                                manager.Active = true;
                                bManager.SaveChanges(manager, UIActionType.ADD);
                                mng = manager;
                            }
                        }
                        managerFlow.Active = true;
                        managerFlow.Manager = mng;
                        managerFlow.Flow = flow;
                        managerFlow.Level = mp.Level;
                        flow.ManagerFlowList.Add(managerFlow);
                    }
                    SaveChanges(flow, UIActionType.EDIT);
                    managerRep.SetManagerActivation();
                    LogUserAction(flow,"Change Manager Flow Levels");
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }

                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex);
                    throw ex;
                }
            }
        }

        #endregion

        public override IList<Flow> GetAll()
        {
            try
            {
                IList<decimal> ids = accessPort.GetAccessibleFlows();
                IList<Flow> list = flowRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Flow().ID), ids.ToArray(), CriteriaOperation.IN));
                return list;
            }
            catch (Exception ex) 
            {
                LogException(ex, "BFlow", "GetAll");
                throw ex;
            }
        }

        protected override void OnSaveChangesSuccess(Flow obj, UIActionType action)
        {
            if (action == UIActionType.DELETE) 
            {
                new ManagerRepository(false).SetManagerActivation();
            }
        }

        /// <summary>
        /// لیست جریانهایی که یک مدیر در آنها نقش دارد را برمیگرداند
        /// </summary>
        /// <param name="managerId">شناسه مدیر</param>
        /// <returns></returns>
        public IList<Flow> GetAllFlowByManager(decimal managerId) 
        {
            try
            {
                EntityRepository<ManagerFlow> mngFlowRep = new EntityRepository<ManagerFlow>(false);
                IList<ManagerFlow> list =
                    mngFlowRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ManagerFlow().Manager), new Manager() { ID = managerId }),
                                             new CriteriaStruct(Utility.GetPropertyName(() => new ManagerFlow().Active), true));
                IList<Flow> flows = 
                    list.GroupBy(x => x.Flow.ID)
                    .Select(x => x.First().Flow)
                    .ToList();
                IList<Flow> result = new List<Flow>();
                IList<decimal> ids=accessPort.GetAccessibleFlows();
                foreach (Flow flow in flows)
                {
                    if (ids.Contains(flow.ID)) 
                    {
                        result.Add(flow);
                    }
                }
                foreach (Flow flow in result)
                {
                    flow.DepartmentCount = bUnderManagment.GetUnderManagmentDepartmentByFlow(flow, false).Count;
                    flow.PersonCount = bUnderManagment.GetUnderManagmentPersonsByFlow(flow).Count;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetAllFlowByManager");
                throw ex;
            }
        }

        public IList<Flow> SearchFlow(FlowSearchFields field, string searchVal)
        {
            try
            {
                IList<Flow> list = new List<Flow>();
                IList<decimal> ids = accessPort.GetAccessibleFlows();
                switch (field)
                {
                    case FlowSearchFields.FlowName:
                    case FlowSearchFields.NotSpec:
                        list =
                            flowRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Flow().FlowName), searchVal, CriteriaOperation.Like),
                                                  new CriteriaStruct(Utility.GetPropertyName(() => new Flow().ID), ids.ToArray(), CriteriaOperation.IN));
                        break;

                    case FlowSearchFields.AccessGroupName:
                        list = flowRep.GetAllByAccessGroupName(searchVal, ids.ToArray());
                        break;
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "SearchFlow");
                throw ex;
            }
        }

        #region Tree

        /// <summary>
        /// ریشه درخت را برمیگرداند
        /// </summary>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Department GetDepartmentRoot()
        {
            try
            {
                return bDep.GetDepartmentsTree();
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetDepartmentRoot");
                throw ex;
            }
        }

        /// <summary>
        /// بچه های یک بخش را برمیگرداند
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentChilds(decimal nodeID, decimal flowId)
        {
            try
            {
                return bDep.GetDepartmentChilds(nodeID, flowId);
                /*DepartmentRepository depRep = new DepartmentRepository(false);
                Flow flow = base.GetByID(flowId);
                List<Department> underManagmentTree = new List<Department>();
                IList<Department> containsNode = bUnderManagment.GetUnderManagmentDepartmentByFlow(flow, true);
                foreach (Department dep in containsNode)
                {
                    underManagmentTree.Add(dep);
                }
                IList<Department> childs = bDep.GetDepartmentChildsWithoutDA(nodeID);
                IList<Department> result = new List<Department>();
                foreach (Department child in childs)
                {
                    if (underManagmentTree.Contains(child))
                    {
                        result.Add(child);
                    }
                }
                
                return result;*/
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetDepartmentChilds");
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
                Department dep = bDep.GetByID(departmentID);
                if (dep.PersonList != null)
                {
                    return dep.PersonList.Where(x => x.Active && !x.IsDeleted).ToList();
                }
                return new List<Person>();
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetDepartmentPerson");
                throw ex;
            }
        }

        /// <summary>
        /// پرسنل یک بخش را برمیگرداند
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public IList<Person> GetDepartmentPerson(decimal departmentID, decimal flowId)
        {
            try
            {
                Flow flow = base.GetByID(flowId);
                Department dep = bDep.GetByID(departmentID);
                List<Person> list = new List<Person>();
                IList<Department> contains = bUnderManagment.GetUnderManagmentDepartmentByFlow(flow, false);
                IList<UnderManagment> unders = flow.UnderManagmentList.Where(x => x.Department.ID == departmentID).ToList();
                Department containsDepartment = contains.Where(x => x.ID == departmentID).FirstOrDefault();
                if (containsDepartment != null)//manager has access to Department 
                {
                    if (unders.Where(x => x.Contains && x.Person != null).Count() == 0)
                    {
                        list.AddRange(dep.PersonList);
                    }
                    foreach (UnderManagment under in unders)
                    {
                        if (under.Person != null && under.Person.ID > 0)
                        {
                            if (under.Contains)
                            {
                                list.Add(under.Person);
                            }
                            else //this person must remove from accessable person list
                            {
                                list.Remove(under.Person);
                            }
                        }
                    }
                }
                return list.Where(x => x.Active && !x.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetDepartmentPerson");
                throw ex;
            }
        }
       
        #endregion

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
                IList<Manager> list = new BManager().GetManagerFlow(flowID);
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetManagerFlow");
                throw ex;
            }
        }

        /// <summary>
        /// نام جریان خالی نباشد
        /// نام جریان تکراری نباشد
        /// گروه پیشکارت خالی نباشد
        /// </summary>
        /// <param name="flow"></param>
        protected override void InsertValidate(Flow flow)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(flow.FlowName))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowNameRequierd, "نام جریان نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                int count =
                   flowRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Flow().FlowName), flow.FlowName));
                if (count > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.FlowNameRepeated, "نام جریان نباید تکراری باشد", ExceptionSrc));
                }
            }
            if (flow.AccessGroup == null || flow.AccessGroup.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowAccessGroupRequierd, "گروه پیشکارت نباید خالی باشد", ExceptionSrc));
            }
           


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// نام جریان خالی نباشد
        /// نام جریان تکراری نباشد
        /// گروه پیشکارت خالی نباشد
        /// </summary>
        /// <param name="flow"></param>
        protected override void UpdateValidate(Flow flow)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(flow.FlowName))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowNameRequierd, "نام جریان نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                int count =
                   flowRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Flow().FlowName), flow.FlowName),
                                         new CriteriaStruct(Utility.GetPropertyName(() => new Flow().ID), flow.ID, CriteriaOperation.NotEqual));
                if (count > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.FlowNameRepeated, "نام جریان نباید تکراری باشد", ExceptionSrc));
                }
            }
            if (flow.AccessGroup == null || flow.AccessGroup.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowAccessGroupRequierd, "گروه پیشکارت نباید خالی باشد", ExceptionSrc));
            }          


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(Flow flow)
        {
           
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckOrganizationWorkFlowLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteOrganizationFlow(Flow organizationFlow, UIActionType UAT)
        {
            return base.SaveChanges(organizationFlow, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateManagerFlows_onOrganizationFlowInsert(decimal flowID, bool isActiveFlow, bool isMainFlow, IList<ManagerProxy> ManagerProxyList)
        {
            this.UpdateManagerFlows(flowID, isActiveFlow, isMainFlow, ManagerProxyList);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateManagerFlows_onOrganizationFlowUpdate(decimal flowID, bool isActiveFlow, bool isMainFlow, IList<ManagerProxy> ManagerProxyList)
        {
            this.UpdateManagerFlows(flowID, isActiveFlow, isMainFlow, ManagerProxyList);
        }



    }
}
