using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business.Security;
using GTS.Clock.Model;

namespace GTS.Clock.Business.Charts
{
    public class BDepartment : BaseBusiness<Department>
    {
        private IDataAccess accessPort = new BUser();
        const string ExceptionSrc = "GTS.Clock.Business.Shifts.Business.Department";
        private DepartmentRepository departmentRepository
            = new DepartmentRepository(false);

        /// <summary>
        /// جهت واکشی سریعتر بخش ها کاربرد دارد
        /// </summary>
        /// <returns></returns>
        public override IList<Department> GetAll()
        {
            try
            {
                IList<Department> list = base.GetAll();
                IList<decimal> accessableIDs = accessPort.GetAccessibleDeparments();
                list = list.Where(x => accessableIDs.Where(y => y == x.ID).Count() > 0).ToList();
                return list;
            }
            catch (Exception ex) 
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// ریشه درخت را برمیگرداند که با پیمایش بچههای آن درخت استخراج میشود
        /// </summary>
        /// <returns></returns>
        public Department GetDepartmentsTree()
        {
            try
            {
                IList<Department> departmentsList = departmentRepository.GetDepartmentTree();

                if (departmentsList.Count == 1)
                {
                    return departmentsList.First();
                }
                else
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.DepartmentRootMoreThanOne, "تعداد ریشه بخشها در دیتابیس نامعتبر است", ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BDepartment", "GetDepartmentsTree");
                throw ex;
            }
        }

        /// <summary>
        /// بچه های یک گره را برمیگرداند
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentChilds(decimal departmentId)
        {
            try
            {
                IList<decimal> accessableIDs = accessPort.GetAccessibleDeparments();
                IList<Department> depList = new List<Department>();

                depList = departmentRepository
                    .GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Department().Parent), new Department() { ID = departmentId }),
                                   new CriteriaStruct(Utility.GetPropertyName(() => new Department().ID), accessableIDs.ToArray(), CriteriaOperation.IN));

                if (depList != null && depList.Count > 0)
                {
                    depList = depList.OrderBy(x => x.CustomCode).ToList();
                }
                return depList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDepartment", "GetDepartmentChilds");
                throw ex;
            }
        }

        /// <summary>
        /// بچه های یک گره را بدون مراجعه به دیتابیس برمیگرداند
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="allDepartments"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentChilds(decimal departmentId, IList<Department> allDepartments)
        {
            try
            {
                IList<Department> list = new List<Department>();
                if (allDepartments != null && allDepartments.Count > 0)
                {
                    list = allDepartments.Where(x => x.ParentID == departmentId).ToList();
                }
                if (list != null && list.Count > 0)
                {
                    list = list.OrderBy(x => x.CustomCode).ToList();
                }
                return list;
            }
            catch (Exception ex) 
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// بچه های یک گره را برمیگرداند
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentChildsWithoutDA(decimal departmentId)
        {
            try
            {
                IList<Department> depList = new List<Department>();

                depList = departmentRepository
                    .GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Department().Parent), new Department() { ID = departmentId }));


                return depList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDepartment", "GetDepartmentChildsWithoutDA");
                throw ex;
            }
        }

        /// <summary>
        /// بچه های یک گره را با استفاده از آدرس پدران آن برمیگرداند
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentChildsByParentPath(decimal parentId)
        {
            IList<Department> depList = departmentRepository.GetByCriteria(
                new CriteriaStruct(
                    Utility.GetPropertyName(() => new Department().ParentPath)
                    , String.Format(",{0},", parentId)
                    , CriteriaOperation.Like));
            if (depList != null && depList.Count > 0)
            {
                depList = depList.OrderBy(x => x.CustomCode).ToList();
            }
            return depList;
        }

        #region استخراج بخش بر اساس دسترسی های مختلف

        /// <summary>
        /// درخت بخشهای تحت مدیریت یک مدیر را برمیگرداند
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public Department GetManagerDepartmentTree(decimal managerId) 
        {
            try
            {
                GTS.Clock.Business.RequestFlow.BManager bmanager = new GTS.Clock.Business.RequestFlow.BManager();
                GTS.Clock.Model.RequestFlow.Manager mng = bmanager.GetByID(managerId);

                List<decimal> nodeParentChildsId = new List<decimal>();
                var flows = new List<GTS.Clock.Model.RequestFlow.Flow>();
                flows .AddRange( from n in mng.ManagerFlowList
                            where n.Active && n.Flow.ActiveFlow
                            select n.Flow);
               
                #region اگر این شخص جانشین هم باشد
                SubstituteRepository subRep = new SubstituteRepository(false);
                if (subRep.IsSubstitute(Security.BUser.CurrentUser.Person.ID))
                {
                    IList<GTS.Clock.Model.RequestFlow.Substitute> subList = subRep.GetSubstitute(Security.BUser.CurrentUser.Person.ID);
                    foreach (GTS.Clock.Model.RequestFlow.Substitute sub in subList) 
                    {
                        flows.AddRange(from n in sub.Manager.ManagerFlowList
                                       where n.Active && n.Flow.ActiveFlow
                                       select n.Flow);
                    }
                }
                #endregion
              
                Department root = this.GetDepartmentsTree();
                IList<GTS.Clock.Model.RequestFlow.Flow> flowList = flows.ToList();
                root.Visible = true;
                foreach (GTS.Clock.Model.RequestFlow.Flow flow in flowList)
                {
                    SetVisibility(root, flow, new GTS.Clock.Business.RequestFlow.BFlow().GetDepartmentChilds(root.ID, flow.ID));
                }
                this.RemoveNotVisibleChilds(root);
                return root;
            }
            catch (Exception ex) 
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// بخشهای تحت مدیریت یک اپراتور
        /// </summary>
        /// <param name="operatorPersonId"></param>
        /// <returns></returns>
        public Department GetOperatorDepartmentTree(decimal operatorPersonId)
        {
            try
            {
                GTS.Clock.Business.RequestFlow.BOperator boperator = new GTS.Clock.Business.RequestFlow.BOperator();
                IList<GTS.Clock.Model.RequestFlow.Operator> opList = boperator.GetOperator(operatorPersonId);

                List<decimal> nodeParentChildsId = new List<decimal>();
                var flows = from n in opList
                            where n.Active && n.Flow.ActiveFlow
                            select n.Flow;
                Department root = this.GetDepartmentsTree();
                IList<GTS.Clock.Model.RequestFlow.Flow> flowList = flows.ToList();
                root.Visible = true;
                foreach (GTS.Clock.Model.RequestFlow.Flow flow in flowList)
                {
                    SetVisibility(root, flow, new GTS.Clock.Business.RequestFlow.BFlow().GetDepartmentChilds(root.ID, flow.ID));
                }
                this.RemoveNotVisibleChilds(root);
                return root;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// جهت نمایش بخش ها در جستجوی پیشرفته
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public IList<Department> GetAllManagerDepartmentTree(decimal managerId) 
        {
            try
            {
                Department root = this.GetManagerDepartmentTree(managerId);
                IList<Department> nodes = new List<Department>();
                nodes.Add(root);
                this.GetInnerNodes(root.ChildList, nodes);
                return nodes;
            }
            catch (Exception ex) 
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// جهت نمایش بخش ها در جستجوی پیشرفته
        /// </summary>
        /// <param name="operatorPersonId"></param>
        /// <returns></returns>
        public IList<Department> GetAllOperatorDepartmentTree(decimal operatorPersonId)
        {
            try
            {
                Department root = this.GetOperatorDepartmentTree(operatorPersonId);
                IList<Department> nodes = new List<Department>();
                nodes.Add(root);
                this.GetInnerNodes(root.ChildList, nodes);
                return nodes;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// درخت بخشهای تحت مدیریت یک مدیر را برمیگرداند
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public IList<Department> GetManagerDepartmentTree(decimal managerId, decimal parentId)
        {
            try
            {
                GTS.Clock.Business.RequestFlow.BManager bmanager = new GTS.Clock.Business.RequestFlow.BManager();
                GTS.Clock.Model.RequestFlow.Manager mng = bmanager.GetByID(managerId);

                List<decimal> nodeParentChildsId = new List<decimal>();
                var flows = from n in mng.ManagerFlowList
                            where n.Active && n.Flow.ActiveFlow
                            select n.Flow;
                Department root = this.GetByID(parentId);
                IList<GTS.Clock.Model.RequestFlow.Flow> flowList = flows.ToList();
                foreach (GTS.Clock.Model.RequestFlow.Flow flow in flowList)
                {
                    SetVisibility(root, flow, new GTS.Clock.Business.RequestFlow.BFlow().GetDepartmentChilds(root.ID, flow.ID));
                }
                this.RemoveNotVisibleChilds(root);
                return root.ChildList;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// درخت بخشهای تحت مدیریت یک مدیر را برمیگرداند
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public IList<Department> GetOperatorDepartmentTree(decimal operatorPersonId, decimal parentId)
        {
            try
            {
                GTS.Clock.Business.RequestFlow.BOperator boperator = new GTS.Clock.Business.RequestFlow.BOperator();
                IList<GTS.Clock.Model.RequestFlow.Operator> opList = boperator.GetOperator(operatorPersonId);

                List<decimal> nodeParentChildsId = new List<decimal>();
                var flows = from n in opList
                            where n.Active && n.Flow.ActiveFlow
                            select n.Flow;
                Department root = this.GetByID(parentId);
                IList<GTS.Clock.Model.RequestFlow.Flow> flowList = flows.ToList();
                foreach (GTS.Clock.Model.RequestFlow.Flow flow in flowList)
                {
                    SetVisibility(root, flow, new GTS.Clock.Business.RequestFlow.BFlow().GetDepartmentChilds(root.ID, flow.ID));
                }
                this.RemoveNotVisibleChilds(root);
                return root.ChildList;
            }
            catch (Exception ex)
            {
                LogException(ex);
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
                DepartmentRepository depRep = new DepartmentRepository(false);
                GTS.Clock.Model.RequestFlow.Flow flow = new GTS.Clock.Business.RequestFlow.BFlow().GetByID(flowId);
                List<Department> underManagmentTree = new List<Department>();
                IList<Department> containsNode = new GTS.Clock.Business.RequestFlow.BUnderManagment().GetUnderManagmentDepartmentByFlow(flow, true);
                foreach (Department dep in containsNode)
                {
                    underManagmentTree.Add(dep);
                }
                IList<Department> childs = this.GetDepartmentChildsWithoutDA(nodeID);
                IList<Department> result = new List<Department>();
                foreach (Department child in childs)
                {
                    if (underManagmentTree.Contains(child))
                    {
                        result.Add(child);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }
      
        
        #endregion      

        /// <summary>
        /// بصورت بازگشتی درحت را پیمایش و شرط نمایش را بررسی میکند
        ///  اگر تشخیص داده شد که گره ای نباید نشان داده شود نیازی به پیمایش گره های فرزند نیست
        ///  زیرا این تشخیص شامل آنها نیز میشود
        /// </summary>
        /// <param name="department"></param>
        /// <param name="visibleIds"></param>
        private void SetVisibility(Department department, GTS.Clock.Model.RequestFlow.Flow flow, IList<Department> containsChildList)
        {
            GTS.Clock.Business.RequestFlow.BFlow bFlow = new GTS.Clock.Business.RequestFlow.BFlow();            
            if (department.ChildList != null)
            {
                foreach (Department child in department.ChildList)
                {
                    if (!containsChildList.Contains(child))
                    {
                        child.Visible = child.Visible || false;//ممکن است در جریانهای قبلی مقدار یک گرفته باشد
                    }
                    else
                    {
                        child.Visible = true;
                        this.SetVisibility(child, flow, bFlow.GetDepartmentChilds(child.ID, flow.ID));
                    }
                }
            }
        }

        /// <summary>
        /// بصورت بازگشتی از بین بچها آنهاییکه پدیدار نیستند حذف میشوند
        /// </summary>
        /// <param name="department"></param>
        private void RemoveNotVisibleChilds(Department department) 
        {
            if (department != null && department.ChildList != null) 
            {
                department.ChildList = department.ChildList.Where(x => x.Visible).ToList();
                foreach (Department dep in department.ChildList) 
                {
                    this.RemoveNotVisibleChilds(dep);             
                }                
            }
        }

        private void GetInnerNodes(IList<Department> deps, IList<Department> allNodes) 
        {
            if (deps != null && deps.Count > 0) 
            {
                foreach (Department dep in deps) 
                {
                    allNodes.Add(dep);
                    this.GetInnerNodes(dep.ChildList, allNodes);
                }
            }
        }

        /// <summary>
        /// نام خالی نباشد
        /// شناسه والد معتبر باشد
        /// اعتبار سنجی آیتمی که قرار است درج شود
        /// شناسه والد خالی نباشد
        /// نام در گرهای همسطح تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="dep"></param>
        /// <param name="exception"></param>
        protected override void InsertValidate(Department dep)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(dep.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DepNameRequierd, "نام بخش باید مشخص شود", ExceptionSrc));
            }

            if (Utility.IsEmpty(dep.ParentID))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DepParentIDRequierd, "نام والد بخش باید مشخص شود", ExceptionSrc));
            }

            if (departmentRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dep.ID), dep.ParentID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DepParentNotExists, "دیپارتمان والدی با این شناسه موجود نمیباشد", ExceptionSrc));
            }

            else if (departmentRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dep.Name), dep.Name),
                                                                  new CriteriaStruct(Utility.GetPropertyName(() => dep.Parent), dep.Parent)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DepartmentRepeatedName, "نام بخش در یک سطح نباید تکراری باشد", ExceptionSrc));
            }

            if (!Utility.IsEmpty(dep.CustomCode))
            {
                if (departmentRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dep.CustomCode), dep.CustomCode)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DepartCustomCodeRepeated, "درج - کد تعریف شده در بخش نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// نام خالی نباشد
        /// شناسه والد معتبر باشد
        /// اعتبار سنجی آیتمی که قرار است بروزرسانی شود
        /// نام در گرهای همسطح تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="dep"></param>
        /// <param name="exception"></param>
        protected override void UpdateValidate(Department dep)
        {
            // والد یک گره بروزرسانی نمیشود .همچنین بنا به محدودیتهای کلاینت هنگام بروزرسانی والد مقداردهی نمیشود
            dep.ParentID = departmentRepository.GetParentID(dep.ID);

            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(dep.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DepNameRequierd, "نام بخش باید مشخص شود", ExceptionSrc));
            }

            else if (dep.ParentID != 0 &&
                departmentRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dep.Name), dep.Name),
                                                                  new CriteriaStruct(Utility.GetPropertyName(() => dep.Parent), dep.Parent),
                                                                  new CriteriaStruct(Utility.GetPropertyName(() => dep.ID), dep.ID, CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DepartmentRepeatedName, "نام بخش در یک سطح نباید تکراری باشد", ExceptionSrc));
            }

            if (!Utility.IsEmpty(dep.CustomCode))
            {
                if (departmentRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dep.CustomCode), dep.CustomCode),
                                                                 new CriteriaStruct(Utility.GetPropertyName(() => dep.ID), dep.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DepartCustomCodeRepeated, "بروزرسانی - کد تعریف شده در بخش نباید تکراری باشد", ExceptionSrc));
                }
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
        protected override void DeleteValidate(Department dep)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            PersonRepository personRep = new PersonRepository(false);
            int count = personRep.GetCountByCriteria(new CriteriaStruct(/*Utility.GetPropertyName(() => new Model.Person().Department)*/"department", dep));

            if (count > 0)
            {
                exception.Add(ExceptionResourceKeys.DepUsedByPersons, "این بخش به اشخاص انتساب داده شده است", ExceptionSrc);
            }

            if (departmentRepository.IsRoot(dep.ID))
            {
                exception.Add(ExceptionResourceKeys.DepartmentRootDeleteIllegal, "ریشه قابل حذف نیست", ExceptionSrc);
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// مقداردهی مسیر والد
        /// </summary>
        /// <param name="dep"></param>
        /// <param name="action"></param>
        protected override void GetReadyBeforeSave(Department dep, UIActionType action)
        {
            if (action == UIActionType.ADD && dep.ParentID > 0)
            {
                Department parent = base.GetByID(dep.ParentID);
                dep.ParentPath = parent.ParentPath + String.Format(",{0},", dep.ParentID);
            }
            else if (action == UIActionType.EDIT)
            {
                Department node = base.GetByID(dep.ID);
                dep.ParentPath = node.ParentPath;
                NHibernateSessionManager.Instance.ClearSession();
            }
        }


        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckDepartmentsLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertDepartment(Department department, UIActionType UAT)
        {
            return base.SaveChanges(department, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateDepartment(Department department, UIActionType UAT)
        {
            return base.SaveChanges(department, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteDepartment(Department department, UIActionType UAT)
        {
            IList<Department> depList = this.GetDepartmentChildsByParentPath(department.ID);
            foreach (Department dep in depList) 
            {
                if (dep.PersonList != null && dep.PersonList.Count > 0) 
                {
                    UIValidationExceptions exception = new UIValidationExceptions();
                    exception.Add(ExceptionResourceKeys.DepUsedByPersons, "این بخش به اشخاص انتساب داده شده است", ExceptionSrc);
                    throw exception;
                }
            }

            departmentRepository.DelateHirenchicalByParentId(department.ID);

            return base.SaveChanges(department, UAT);
        }

    }
}
