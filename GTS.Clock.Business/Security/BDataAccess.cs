using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Report;
using GTS.Clock.Model.Security;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Business.Rules;

namespace GTS.Clock.Business.Security
{
    /// <summary>
    /// created at: 2012-02-16 10:00:48 AM
    /// by        : Farhad Salavati
    /// write your name here
    /// </summary>
    public class BDataAccess : MarshalByRefObject
    {
        #region variables
        private const string ExceptionSrc = "GTS.Clock.Business.Security.BDataAccess";
        DepartmentRepository depRep = new DepartmentRepository(false);
        OrganizationUnitRepository organRep = new OrganizationUnitRepository(false);
        ShiftRepository shiftRep = new ShiftRepository(false);
        WorkGroupRepository wrkGrpRep = new WorkGroupRepository(false);
        PrecardRepository precardRep = new PrecardRepository(false);
        EntityRepository<ControlStation> ctlStRep = new EntityRepository<ControlStation>();
        EntityRepository<Doctor> docRep = new EntityRepository<Doctor>();
        ManagerRepository managerRep = new ManagerRepository(false);
        RuleCategoryRepository ruleCatRep = new RuleCategoryRepository();
        FlowRepository flowRep = new FlowRepository(false);
        EntityRepository<Report> reportRep = new EntityRepository<Report>();
        UserRepository userRepository = new UserRepository(false);
        EntityRepository<Corporation> organizationRepository = new EntityRepository<Corporation>();
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetAll(DataAccessParts part)
        {
            IList<DataAccessProxy> result = new List<DataAccessProxy>();
            switch (part)
            {
                case DataAccessParts.Shift:
                    result = this.GetAllShifts();
                    break;
                case DataAccessParts.WorkGroup:
                    result = this.GetAllWorkGroups();
                    break;
                case DataAccessParts.Precard:
                    result = this.GetAllPrecards();
                    break;
                case DataAccessParts.ControlStation:
                    result = this.GetAllControlStations();
                    break;
                case DataAccessParts.Doctor:
                    result = this.GetAllDoctors();
                    break;
                case DataAccessParts.Flow:
                    result = this.GetAllFlow();
                    break;
                case DataAccessParts.Corporation:
                    result = this.GetAllCorporations();
                    break;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetAllByUserId(DataAccessParts part, decimal userId)
        {
            IList<DataAccessProxy> result = new List<DataAccessProxy>();
            switch (part)
            {
                case DataAccessParts.Shift:
                    result = this.GetAllShiftsOfUser(userId);
                    break;
                case DataAccessParts.WorkGroup:
                    result = this.GetAllWorkGroupsOfUser(userId);
                    break;
                case DataAccessParts.Precard:
                    result = this.GetAllPrecardOfUser(userId);
                    break;
                case DataAccessParts.ControlStation:
                    result = this.GetAllControlStationsOfUser(userId);
                    break;
                case DataAccessParts.Doctor:
                    result = this.GetAllDoctorsOfUser(userId);
                    break;
                case DataAccessParts.Flow:
                    result = this.GetAllFlowOfUser(userId);
                    break;
                case DataAccessParts.Corporation:
                    result = this.GetAllCorporationsOfUser(userId);
                    break;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="dataAccessId"></param>
        /// <returns></returns>
        public bool DeleteAccess(DataAccessParts part, decimal dataAccessId, decimal userId)
        {
            bool result = false;
            switch (part)
            {
                case DataAccessParts.Department:
                    result = this.DeleteDepartment(dataAccessId, userId);
                    break;
                case DataAccessParts.OrganizationUnit:
                    result = this.DeleteOrganization(dataAccessId, userId);
                    break;
                case DataAccessParts.Report:
                    result = this.DeleteReport(dataAccessId, userId);
                    break;
                case DataAccessParts.Shift:
                    result = this.DeleteShift(dataAccessId);
                    break;
                case DataAccessParts.WorkGroup:
                    result = this.DeleteWorkGroup(dataAccessId);
                    break;
                case DataAccessParts.Precard:
                    result = this.DeletePrecard(dataAccessId);
                    break;
                case DataAccessParts.ControlStation:
                    result = this.DeleteControlStation(dataAccessId);
                    break;
                case DataAccessParts.Doctor:
                    result = this.DeleteDoctor(dataAccessId);
                    break;
                case DataAccessParts.Manager:
                    result = this.DeleteManager(dataAccessId, userId);
                    break;
                case DataAccessParts.RuleGroup:
                    result = this.DeleteRule(dataAccessId, userId);
                    break;
                case DataAccessParts.Flow:
                    result = this.DeleteFlow(dataAccessId);
                    break;
                case DataAccessParts.Corporation:
                    result = this.DeleteCorporation(dataAccessId);
                    break;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="partId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool InsertDataAccess(DataAccessLevelOperationType Dalot, DataAccessParts part, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            bool result = false;
            switch (part)
            {
                case DataAccessParts.Department:
                    result = this.InsertDepartment(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.OrganizationUnit:
                    result = this.InsertOrganization(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.Report:
                    result = this.InsertReport(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.Shift:
                    result = this.InsertShift(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.WorkGroup:
                    result = this.InsertWorkGroup(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.Precard:
                    result = this.InsertPrecard(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.ControlStation:
                    result = this.InsertControlStaion(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.Doctor:
                    result = this.InsertDoctor(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.Manager:
                    result = this.InsertManager(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.RuleGroup:
                    result = this.InsertRule(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.Flow:
                    result = this.InsertFlow(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.Corporation:
                    result = this.InsertCorporation(Dalot, partId, userId, searchKey, searchTerm);
                    break;
            }
            return result;
        }


        #region Department

        /// <summary>
        /// ریشه را برای هردو درخت برمیگرداند
        /// اگر شخص دسترسی به همه داشته باشد ریشه باید قابل حذف باشد
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataAccessProxy GetDepartmentRoot(DataAccessLevelsType type, decimal userId)
        {
            if (type == DataAccessLevelsType.Source)
            {
                IList<Department> list = depRep.GetDepartmentTree();
                Department result = new Department();
                if (list.Count > 0)
                {
                    result = list.First();
                }

                return new DataAccessProxy() { ID = 0, Name = result.Name };

            }
            else
            {
                DataAccessProxy proxy = new DataAccessProxy();

                if (userRepository.HasAllDepartmentAccess(userId))
                {
                    proxy.DeleteEnable = true;
                }
                return proxy;
            }
        }

        /// <summary>
        /// زیر بخشهای یک بخش را برمیگرداند
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetDepartmentChilds(decimal parentId)
        {
            if (parentId == 0)
            {
                parentId = new BDepartment().GetDepartmentsTree().ID;
            }
            IList<Department> list = depRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Department().Parent), new Department() { ID = parentId }));
            var result = from o in list
                         select new DataAccessProxy()
                         {
                             ID = o.ID,
                             Name = o.Name
                         };
            return result.ToList<DataAccessProxy>();
        }

        /// <summary>
        /// زیربخش های قابل دسترس برای یک بخش را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetDepartmentsOfUser(decimal userId, decimal parentId)
        {
            try
            {
                BDepartment bDep = new BDepartment();
                IList<Department> result = new List<Department>();

                if (parentId == 0)
                {
                    EntityRepository<DADepartment> rep = new EntityRepository<DADepartment>();
                    if (userRepository.HasAllDepartmentAccess(userId))
                    {
                        Department root = bDep.GetDepartmentsTree();
                        result = bDep.GetDepartmentChildsWithoutDA(root.ID);
                    }
                    else
                    {
                        IList<DADepartment> daList = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), userId));
                        var ids = from o in daList
                                  select o.Department;
                        result = ids.ToList();

                        ///حذف بچه از بین والدها
                        foreach (DADepartment da1 in daList)
                        {
                            foreach (DADepartment da2 in daList)
                            {
                                if (da2.Department.ParentPath.Contains(String.Format(",{0},", da1.DepID.ToString())))
                                {
                                    result.Remove(da2.Department);
                                }
                            }
                        }

                        foreach (Department dep in result)
                        {
                            dep.Visible = true;
                        }
                    }
                }
                else
                {
                    result = bDep.GetByID(parentId).ChildList;
                }
                var lastResult = from o in result
                                 select new DataAccessProxy()
                                 {
                                     ID = o.ID,
                                     Name = o.Name,
                                     DeleteEnable = o.Visible
                                 };
                return lastResult.ToList<DataAccessProxy>();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetDepartmentsOfUser");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool InsertDepartment(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool succes = false;
                    DADepartment daDep = new DADepartment();
                    EntityRepository<DADepartment> departmentDARep = new EntityRepository<DADepartment>();
                    EntityRepository<DADepartment> daRep = new EntityRepository<DADepartment>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    if (partId == 0)//درج همه
                    {
                        IList<DADepartment> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = departmentDARep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                daPartList = departmentDARep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                break;
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DADepartment da in daPartList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daDep = daRep.WithoutTransactSave(new DADepartment() { UserID = userID, All = true, DepID = null });
                        }
                        succes = true;
                    }
                    else
                    {
                        IList<DADepartment> daSinglePartList = null;
                        IList<DADepartment> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = departmentDARep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), userID),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().DepID), partId));
                            daAllPartsList = departmentDARep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), userID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daDep = daRep.WithoutTransactSave(new DADepartment() { DepID = partId, UserID = userID, All = false });
                        }
                        succes = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return succes;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertDepartment");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// حذف یک گره 
        /// اگر دسترسی همه بخواهد حذف شود باید شناسه صفر باشد
        /// در هنگام واکشی در پرکسی شناسه بخش قرارداده میشود لذا در هنگام حذف نیز باید بر اساس شناسه بخش کار کنیم
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool DeleteDepartment(decimal departmentId, decimal userId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    EntityRepository<DADepartment> daRep = new EntityRepository<DADepartment>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DADepartment> daPrtList = null;
                    if (departmentId == 0)//ریشه مجازی برای کسانی که دسترسی یه همه دارند
                    {
                        daPrtList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), userId),
                                                   new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().All), true));
                        if (daPrtList.Count > 0)
                        {
                            foreach (DADepartment da in daPrtList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                        }
                        success = true;
                    }
                    else
                    {
                        daPrtList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), userId),
                                                   new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().DepID), departmentId));
                        if (daPrtList.Count > 0)
                        {
                            foreach (DADepartment da in daPrtList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                        }
                        success = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteDepartment");
                    throw ex;
                }
            }
        }

        #endregion

        #region OrganizationUnit

        /// <summary>
        /// ریشه را برای هردو درخت برمیگرداند
        /// اگر شخص دسترسی به همه داشته باشد ریشه باید قابل حذف باشد
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataAccessProxy GetOrganizationRoot(DataAccessLevelsType type, decimal userId)
        {
            if (type == DataAccessLevelsType.Source)
            {
                IList<OrganizationUnit> list = organRep.GetOrganizationUnitTree();
                OrganizationUnit result = new OrganizationUnit();
                if (list.Count > 0)
                {
                    result = list.First();
                }
                result.ChildList = null;
                return new DataAccessProxy() { ID = 0, Name = result.Name };
            }
            else
            {
                DataAccessProxy proxy = new DataAccessProxy();

                if (userRepository.HasAllOrganAccess(userId))
                {
                    proxy.DeleteEnable = true;
                }
                return proxy;
            }
        }

        /// <summary>
        /// زیر بخشهای یک بخش را برمیگرداند
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetOrganizationChilds(decimal parentId)
        {
            if (parentId == 0)
            {
                parentId = new BOrganizationUnit().GetOrganizationUnitTree().ID;
            }
            IList<OrganizationUnit> list = organRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new OrganizationUnit().Parent), new OrganizationUnit() { ID = parentId }));
            var result = from o in list
                         select new DataAccessProxy()
                         {
                             ID = o.ID,
                             Name = o.Name
                         };
            return result.ToList<DataAccessProxy>();
        }

        /// <summary>
        /// زیربخش های قابل دسترس برای یک بخش را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetOrganizationOfUser(decimal userId, decimal parentId)
        {
            try
            {
                BaseBusiness<Entity>.LogException(new Exception(userId.ToString() + ":user 444 parent:" + parentId.ToString()), "BDataAccess", "GetOrganizationOfUser");
                BOrganizationUnit borgan = new BOrganizationUnit();
                IList<OrganizationUnit> result = new List<OrganizationUnit>();

                if (parentId == 0)
                {
                    EntityRepository<DAOrganizationUnit> rep = new EntityRepository<DAOrganizationUnit>();
                    if (userRepository.HasAllOrganAccess(userId))
                    {
                        OrganizationUnit root = borgan.GetOrganizationUnitTree();
                        result = borgan.GetDepartmentChildsWithoutDA(root.ID);
                    }
                    else
                    {
                        IList<DAOrganizationUnit> daList = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().UserID), userId));
                        var ids = from o in daList
                                  select o.Organization;
                        result = ids.ToList();

                        ///حذف بچه از بین والدها
                        foreach (DAOrganizationUnit da1 in daList)
                        {
                            foreach (DAOrganizationUnit da2 in daList)
                            {
                                if (da2.Organization.ParentPath.Contains(String.Format(",{0},", da1.OrgUnitID.ToString())))
                                {
                                    result.Remove(da2.Organization);
                                }
                            }
                        }

                        foreach (OrganizationUnit organ in result)
                        {
                            organ.Visible = true;
                        }
                    }
                }
                else
                {
                    result = borgan.GetByID(parentId).ChildList;
                }
                var lastResult = from o in result
                                 select new DataAccessProxy()
                                 {
                                     ID = o.ID,
                                     Name = o.Name,
                                     DeleteEnable = o.Visible
                                 };
                return lastResult.ToList<DataAccessProxy>();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(new Exception(userId.ToString() + ":user -- parent:" + parentId.ToString()), "BDataAccess", "GetOrganizationOfUser");
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetOrganizationOfUser");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool InsertOrganization(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool succes = false;
                    DAOrganizationUnit daorgan = new DAOrganizationUnit();
                    EntityRepository<DAOrganizationUnit> daRep = new EntityRepository<DAOrganizationUnit>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    if (partId == 0)//درج همه
                    {
                        IList<DAOrganizationUnit> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                break;
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DAOrganizationUnit da in daPartList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daorgan = daRep.WithoutTransactSave(new DAOrganizationUnit() { UserID = userID, All = true, OrgUnitID = null });
                        }
                        succes = true;
                    }
                    else
                    {
                        IList<DAOrganizationUnit> daSinglePartList = null;
                        IList<DAOrganizationUnit> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().UserID), userID),
                                                                                                                    new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().OrgUnitID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().UserID), userID),
                                                                                                                  new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daorgan = daRep.WithoutTransactSave(new DAOrganizationUnit() { OrgUnitID = partId, UserID = userID, All = false });
                        }
                        succes = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return succes;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertOrganization");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// حذف یک گره 
        /// اگر دسترسی همه بخواهد حذف شود باید شناسه صفر باشد
        /// در هنگام واکشی در پرکسی شناسه بخش قرارداده میشود لذا در هنگام حذف نیز باید بر اساس شناسه بخش کار کنیم
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool DeleteOrganization(decimal departmentId, decimal userId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    EntityRepository<DAOrganizationUnit> daRep = new EntityRepository<DAOrganizationUnit>(false);

                    if (departmentId == 0)//ریشه مجازی برای کسانی که دسترسی یه همه دارند
                    {
                        IList<DAOrganizationUnit> daList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().UserID), userId),
                                                                               new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().All), true));
                        if (daList.Count > 0 && daList.First().All)
                        {
                            daRep.WithoutTransactDelete(daList.First());
                            success = true;
                        }
                    }
                    else
                    {
                        IList<DAOrganizationUnit> daList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().UserID), userId),
                                                                               new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().OrgUnitID), departmentId));
                        if (daList.Count > 0)
                        {
                            foreach (DAOrganizationUnit da in daList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                            success = true;
                        }
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteOrganization");
                    throw ex;
                }
            }
        }


        #endregion

        #region Shift

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllShifts()
        {
            IList<Shift> list = shiftRep.GetAll();
            var result = from o in list
                         select new DataAccessProxy()
                         {
                             ID = o.ID,
                             Name = o.Name,
                             CustomCode = o.CustomCode,
                             DeleteEnable = false,
                             All = false
                         };
            return result.ToList<DataAccessProxy>();
        }

        /// <summary>
        /// لیستی از شیفتها که کاربر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllShiftsOfUser(decimal userId)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllShiftAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<Shift> list = userRepository.GetUserShiftList(userId);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = o.CustomCode };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllShiftOfUser");
                throw ex;
            }
        }

        /// <summary>
        /// دادن سطح دسترسی یک شیفت به یک کاربر
        /// اگر شناسه شیفت برابر صفر بود معانیش این است که دسترسی [همه] داده شدود
        /// </summary>
        /// <param name="shiftId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool InsertShift(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DAShift daShift = new DAShift();
                    EntityRepository<DAShift> shiftDARep = new EntityRepository<DAShift>();
                    EntityRepository<DAShift> daRep = new EntityRepository<DAShift>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    if (partId == 0)
                    {
                        IList<DAShift> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = shiftDARep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAShift().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                daPartList = shiftDARep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAShift().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                break;
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DAShift da in daPartList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daShift = daRep.WithoutTransactSave(new DAShift() { UserID = userID, All = true, ShiftID = null });
                        }
                        success = true;
                    }
                    else
                    {
                        IList<DAShift> daSinglePartList = null;
                        IList<DAShift> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = shiftDARep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAShift().UserID), userID),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DAShift().ShiftID), partId));
                            daAllPartsList = shiftDARep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAShift().UserID), userID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new DAShift().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daShift = daRep.WithoutTransactSave(new DAShift() { ShiftID = partId, UserID = userID, All = false });
                        }
                        success = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertShift");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataAccessId"></param>
        /// <returns></returns>
        private bool DeleteShift(decimal dataAccessId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    EntityRepository<DAShift> daRep = new EntityRepository<DAShift>(false);

                    daRep.WithoutTransactDelete(new DAShift() { ID = dataAccessId });

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return true;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteShift");
                    throw ex;
                }
            }
        }

        #endregion

        #region WorkGroup

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllWorkGroups()
        {
            IList<WorkGroup> list = wrkGrpRep.GetAll();
            var result = from o in list
                         select new DataAccessProxy()
                         {
                             ID = o.ID,
                             Name = o.Name,
                             CustomCode = o.CustomCode,
                             DeleteEnable = false,
                             All = false
                         };
            return result.ToList<DataAccessProxy>();
        }

        /// <summary>
        /// لیستی از شیفتها که کاربر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllWorkGroupsOfUser(decimal userId)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllWorkGroupAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<WorkGroup> list = userRepository.GetUserWorkGroupList(userId);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = o.CustomCode };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllShiftOfUser");
                throw ex;
            }
        }

        private bool InsertWorkGroup(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DAWorkGroup daObject = new DAWorkGroup();
                    IList<decimal> TempUserIDList = new List<decimal>();
                    EntityRepository<DAWorkGroup> daRep = new EntityRepository<DAWorkGroup>(false);
                    IList<DAWorkGroup> daPartList = null;
                    if (partId == 0)
                    {
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAShift().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAWorkGroup().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                break;
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DAWorkGroup da in daPartList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daObject = daRep.WithoutTransactSave(new DAWorkGroup() { UserID = userID, All = true, WorkGrpID = null });
                        }
                        success = true;
                    }
                    else
                    {
                        IList<DAWorkGroup> daSinglePartList = null;
                        IList<DAWorkGroup> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAWorkGroup().UserID), userID),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DAWorkGroup().WorkGrpID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAWorkGroup().UserID), userID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new DAWorkGroup().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daObject = daRep.WithoutTransactSave(new DAWorkGroup() { WorkGrpID = partId, UserID = userID, All = false });
                        }
                        success = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertWorkGroup");
                    throw ex;
                }
            }
        }

        private bool DeleteWorkGroup(decimal dataAccessId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    EntityRepository<DAWorkGroup> daRep = new EntityRepository<DAWorkGroup>(false);

                    daRep.WithoutTransactDelete(new DAWorkGroup() { ID = dataAccessId });

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return true;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteWorkGroup");
                    throw ex;
                }
            }
        }
        #endregion

        #region Precard

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllPrecards()
        {
            IList<Precard> list = precardRep.GetAll();
            var result = from o in list
                         select new DataAccessProxy()
                         {
                             ID = o.ID,
                             Name = o.Name,
                             CustomCode = o.Code,
                             DeleteEnable = false,
                             All = false
                         };
            return result.ToList<DataAccessProxy>();
        }

        /// <summary>
        /// لیستی از شیفتها که کاربر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllPrecardOfUser(decimal userId)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllPrecardAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<Precard> list = userRepository.GetUserPrecardList(userId);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = o.Code };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllShiftOfUser");
                throw ex;
            }
        }

        private bool InsertPrecard(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DAPrecard daObject = new DAPrecard();
                    EntityRepository<DAPrecard> daRep = new EntityRepository<DAPrecard>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    if (partId == 0)
                    {
                        IList<DAPrecard> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAPrecard().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAPrecard().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                break;
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DAPrecard da in daPartList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daObject = daRep.WithoutTransactSave(new DAPrecard() { UserID = userID, All = true, PreCardID = null });
                        }
                        success = true;
                    }
                    else
                    {
                        IList<DAPrecard> daSinglePartList = null;
                        IList<DAPrecard> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAPrecard().UserID), userID),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DAPrecard().PreCardID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAPrecard().UserID), userID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new DAPrecard().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daObject = daRep.WithoutTransactSave(new DAPrecard() { PreCardID = partId, UserID = userID, All = false });
                        }
                        success = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertPrecard");
                    throw ex;
                }
            }
        }

        private bool DeletePrecard(decimal dataAccessId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    EntityRepository<DAPrecard> daRep = new EntityRepository<DAPrecard>(false);

                    daRep.WithoutTransactDelete(new DAPrecard() { ID = dataAccessId });

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return true;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeletePrecard");
                    throw ex;
                }
            }
        }

        #endregion

        #region Control Station
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllControlStations()
        {
            IList<ControlStation> list = ctlStRep.GetAll();
            var result = from o in list
                         select new DataAccessProxy()
                         {
                             ID = o.ID,
                             Name = o.Name,
                             CustomCode = o.CustomCode,
                             DeleteEnable = false,
                             All = false
                         };
            return result.ToList<DataAccessProxy>();
        }

        /// <summary>
        /// لیستی از شیفتها که کاربر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllControlStationsOfUser(decimal userId)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllControlStationAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<ControlStation> list = userRepository.GetUserControlStationList(userId);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = o.CustomCode };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllControlStationsOfUser");
                throw ex;
            }
        }

        private bool InsertControlStaion(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DACtrlStation daObject = new DACtrlStation();
                    IList<decimal> TempUserIDList = new List<decimal>();
                    EntityRepository<DACtrlStation> daRep = new EntityRepository<DACtrlStation>(false);
                    IList<DACtrlStation> daPartList = null;
                    if (partId == 0)
                    {
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DACtrlStation().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DACtrlStation().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                break;
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DACtrlStation da in daPartList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daObject = daRep.WithoutTransactSave(new DACtrlStation() { UserID = userID, All = true, CtrlStationID = null });
                        }
                        success = true;
                    }
                    else
                    {
                        IList<DACtrlStation> daSinglePartList = null;
                        IList<DACtrlStation> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DACtrlStation().UserID), userID),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DACtrlStation().CtrlStationID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DACtrlStation().UserID), userID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new DACtrlStation().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daObject = daRep.WithoutTransactSave(new DACtrlStation() { CtrlStationID = partId, UserID = userID, All = false });
                        }
                        success = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertControlStation");
                    throw ex;
                }
            }
        }

        private bool DeleteControlStation(decimal dataAccessId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    EntityRepository<DACtrlStation> daRep = new EntityRepository<DACtrlStation>(false);

                    daRep.WithoutTransactDelete(new DACtrlStation() { ID = dataAccessId });

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return true;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteControlStation");
                    throw ex;
                }
            }
        }
        #endregion

        #region Doctors

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllDoctors()
        {
            IList<Doctor> list = docRep.GetAll();
            var result = from o in list
                         select new DataAccessProxy()
                         {
                             ID = o.ID,
                             Name = o.Name,
                             CustomCode = "",
                             DeleteEnable = false,
                             All = false
                         };
            return result.ToList<DataAccessProxy>();
        }

        /// <summary>
        /// لیستی از شیفتها که کاربر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllDoctorsOfUser(decimal userId)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllDoctorAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<Doctor> list = userRepository.GetUserDoctorList(userId);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = "" };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllShiftOfUser");
                throw ex;
            }
        }

        private bool InsertDoctor(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DADoctor daObject = new DADoctor();
                    IList<decimal> TempUserIDList = new List<decimal>();
                    EntityRepository<DADoctor> daRep = new EntityRepository<DADoctor>(false);
                    IList<DADoctor> daPartList = null;
                    if (partId == 0)
                    {
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                IList<DADoctor> list = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADoctor().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADoctor().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                break;
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DADoctor da in daPartList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daObject = daRep.WithoutTransactSave(new DADoctor() { UserID = userID, All = true, DoctorID = null });
                        }
                    }
                    else
                    {
                        IList<DADoctor> daSinglePartList = null;
                        IList<DADoctor> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADoctor().UserID), userID),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DADoctor().DoctorID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADoctor().UserID), userID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new DADoctor().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daObject = daRep.WithoutTransactSave(new DADoctor() { DoctorID = partId, UserID = userID, All = false });
                        }
                        success = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertDoctor");
                    throw ex;
                }
            }
        }

        private bool DeleteDoctor(decimal dataAccessId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    EntityRepository<DADoctor> daRep = new EntityRepository<DADoctor>(false);

                    daRep.WithoutTransactDelete(new DADoctor() { ID = dataAccessId });

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return true;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteDoctor");
                    throw ex;
                }
            }
        }

        #endregion

        #region Manager

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<DataAccessProxy> GetAllManagers(string searchKey, int pageIndex, int pageSize)
        {
            try
            {
                IList<Manager> list = managerRep.GetQuickSearch(searchKey, pageSize, pageIndex);
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.ThePerson.Name,
                                 CustomCode = "",
                                 DeleteEnable = false,
                                 All = false
                             };
                return result.ToList<DataAccessProxy>();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllManagers");
                throw ex;
            }
        }

        public int GetManagerCount(string searchKey)
        {
            try
            {
                int count = managerRep.GetQuickSearchCount(searchKey);
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetManagerCount");
                throw ex;
            }
        }

        /// <summary>
        /// لیستی از شیفتها که کاربر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetAllManagerOfUser(decimal userId, string searchKey, int pageIndex, int pageSize)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllManagerAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = 0, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<Manager> list = userRepository.GetUserManagerList(searchKey, userId, pageIndex, pageSize);
                    var l = from o in list
                            //.Where(x => x.ThePerson.Name.ToLower().Contains(searchKey) ||
                            //        (x.ThePerson != null && x.ThePerson.PersonCode.Contains(searchKey)) ||
                            //        (x.TheOrganizationUnit != null && x.TheOrganizationUnit.Name.ToLower().Contains(searchKey))
                            //        )
                            //.Skip(pageIndex * pageSize).Take(pageSize)
                            select new DataAccessProxy() { ID = o.ID, Name = o.ThePerson.Name, DeleteEnable = true, CustomCode = "" };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllManagerOfUser");
                throw ex;
            }
        }

        public int GetManagerOfUserCount(decimal userId, string searchKey)
        {
            int result = 0;
            decimal allId = userRepository.HasAllManagerAccess(userId);

            if (allId > 0)
            {
                result = 1;
            }
            else
            {
                IList<Manager> list = userRepository.GetUserManagerList(searchKey, userId, 0, this.GetManagerCount(""));

                result = list.Count();
            }

            return result;
        }

        private bool InsertManager(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DAManager daObject = new DAManager();
                    IList<decimal> TempUserIDList = new List<decimal>();
                    EntityRepository<DAManager> daRep = new EntityRepository<DAManager>(false);
                    IList<DAManager> daPartList = null;
                    if (partId == 0)
                    {
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                break;
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DAManager da in daPartList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daObject = daRep.WithoutTransactSave(new DAManager() { UserID = userId, All = true, ManagerID = null });                            
                        }
                        success = true;
                    }
                    else
                    {
                        IList<DAManager> daSinglePartList = null;
                        IList<DAManager> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().UserID), userID),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().ManagerID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().UserID), userID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daObject = daRep.WithoutTransactSave(new DAManager() { ManagerID = partId, UserID = userID, All = false });
                        }
                        success = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertManager");
                    throw ex;
                }
            }
        }

        private bool DeleteManager(decimal mangerId, decimal userId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    EntityRepository<DAManager> daRep = new EntityRepository<DAManager>(false);
                    IList<DAManager> daList = new List<DAManager>();
                    bool success = false;
                    if (mangerId == 0)
                    {
                        daList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().UserID), userId),
                                                                         new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().All), true));
                    }
                    else
                    {
                        daList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().UserID), userId),
                                                                         new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().ManagerID), mangerId));
                    }
                    if (daList.Count > 0)
                    {
                        foreach (DAManager da in daList)
                        {
                            daRep.WithoutTransactDelete(da);
                        }
                        success = true;
                    }

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteManager");
                    throw ex;
                }
            }
        }

        #endregion

        #region Rule Category

        /// <summary>
        /// ریشه را برای هردو درخت برمیگرداند
        /// اگر شخص دسترسی به همه داشته باشد ریشه باید قابل حذف باشد
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataAccessProxy GetRuleRoot(DataAccessLevelsType type, decimal userId)
        {
            if (type == DataAccessLevelsType.Source)
            {
                RuleCategory cat = new BRuleCategory().GetRoot();
                RuleCategory result = new RuleCategory();
                result = cat;
                return new DataAccessProxy() { ID = 0, Name = result.Name };
            }
            else
            {
                DataAccessProxy proxy = new DataAccessProxy();

                if (userRepository.HasAllRuleGroupAccess(userId) > 0)
                {
                    proxy.DeleteEnable = true;
                }
                return proxy;
            }
        }

        /// <summary>
        /// زیر گزارشات یک دسته گزارش را برمیگرداند
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetRuleChilds(decimal parentId)
        {
            BRuleCategory bRuleCat = new BRuleCategory();
            if (parentId == 0)
            {
                parentId = bRuleCat.GetRoot().ID;
            }
            IList<RuleCategory> list = bRuleCat.GetByID(parentId).ChildList;
            var result = from o in list
                         select new DataAccessProxy()
                         {
                             ID = o.ID,
                             Name = o.Name,
                         };
            return result.ToList<DataAccessProxy>();
        }

        /// <summary>
        /// زیرگزارشهای قابل دسترس برای یک گروه گزارش را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetRuleOfUser(decimal userId, decimal parentId)
        {
            try
            {
                EntityRepository<DARuleGroup> rep = new EntityRepository<DARuleGroup>(false);
                BRuleCategory bRuleCat = new BRuleCategory();
                IList<RuleCategory> result = new List<RuleCategory>();

                if (parentId == 0)
                {
                    if (userRepository.HasAllRuleGroupAccess(userId) > 0)
                    {
                        RuleCategory root = bRuleCat.GetRoot();
                        result = root.ChildList;
                    }
                    else
                    {
                        IList<DARuleGroup> daList = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().UserID), userId));
                        var ids = from o in daList
                                  select o.RuleCategory;
                        result = ids.ToList();

                        ///حذف بچه از بین والدها
                        foreach (DARuleGroup da1 in daList)
                        {
                            foreach (DARuleGroup da2 in daList)
                            {
                                if (da1.RuleCategory.ChildList.Contains(da2.RuleCategory))
                                {
                                    result.Remove(da2.RuleCategory);
                                }
                            }
                        }

                        foreach (RuleCategory organ in result)
                        {
                            organ.Visible = true;
                        }
                    }
                }
                else
                {
                    result = bRuleCat.GetByID(parentId).ChildList;
                }
                var lastResult = from o in result
                                 select new DataAccessProxy()
                                 {
                                     ID = o.ID,
                                     Name = o.Name,
                                     DeleteEnable = o.Visible,
                                 };
                return lastResult.ToList<DataAccessProxy>();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetRuleOfUser");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool InsertRule(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool succes = false;
                    DARuleGroup darule = new DARuleGroup();
                    EntityRepository<DARuleGroup> daRep = new EntityRepository<DARuleGroup>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    if (partId == 0)//درج همه
                    {
                        IList<DARuleGroup> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                break;
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DARuleGroup da in daPartList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            darule = daRep.WithoutTransactSave(new DARuleGroup() { UserID = userID, All = true, RuleGrpID = null });
                        }
                        succes = true;
                    }
                    else
                    {
                        IList<DARuleGroup> daSinglePartList = null;
                        IList<DARuleGroup> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().UserID), userID),
                                                                                     new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().RuleGrpID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().UserID), userID),
                                                                                     new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                darule = daRep.WithoutTransactSave(new DARuleGroup() { RuleGrpID = partId, UserID = userID, All = false });
                        }
                    }
                    succes = true;
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return succes;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertRule");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// حذف یک گره 
        /// اگر دسترسی همه بخواهد حذف شود باید شناسه صفر باشد
        /// در هنگام واکشی در پرکسی شناسه بخش قرارداده میشود لذا در هنگام حذف نیز باید بر اساس شناسه بخش کار کنیم
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool DeleteRule(decimal reportId, decimal userId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    EntityRepository<DARuleGroup> daRep = new EntityRepository<DARuleGroup>(false);

                    if (reportId == 0)//ریشه مجازی برای کسانی که دسترسی یه همه دارند
                    {
                        IList<DARuleGroup> daList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().UserID), userId),
                                                                     new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().All), true));
                        if (daList.Count > 0 && daList.First().All)
                        {
                            daRep.WithoutTransactDelete(daList.First());
                            success = true;
                        }
                    }
                    else
                    {
                        IList<DARuleGroup> daList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().UserID), userId),
                                                                     new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().RuleGrpID), reportId));
                        if (daList.Count > 0)
                        {
                            foreach (DARuleGroup da in daList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                            success = true;
                        }
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteRule");
                    throw ex;
                }
            }
        }

        #endregion

        #region Flow

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllFlow()
        {
            IList<Flow> list = flowRep.GetAll();
            var result = from o in list
                         select new DataAccessProxy()
                         {
                             ID = o.ID,
                             Name = o.FlowName,
                             CustomCode = "",
                             DeleteEnable = false,
                             All = false
                         };
            return result.ToList<DataAccessProxy>();
        }

        /// <summary>
        /// لیستی از شیفتها که کاربر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllFlowOfUser(decimal userId)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllFlowAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<Flow> list = userRepository.GetUserFlowList(userId);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.FlowName, DeleteEnable = true, CustomCode = "" };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllFlowOfUser");
                throw ex;
            }
        }

        private bool InsertFlow(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DAFlow daObject = new DAFlow();
                    IList<decimal> TempUserIDList = new List<decimal>();
                    EntityRepository<DAFlow> daRep = new EntityRepository<DAFlow>(false);
                    IList<DAFlow> daPartList = null;
                    if (partId == 0)
                    {
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAFlow().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAFlow().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                break;
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DAFlow da in daPartList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daObject = daRep.WithoutTransactSave(new DAFlow() { UserID = userID, All = true, FlowID = null });
                        }
                        success = true;
                    }
                    else
                    {
                        IList<DAFlow> daSinglePartList = null;
                        IList<DAFlow> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAFlow().UserID), userID),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DAFlow().FlowID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAFlow().UserID), userID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new DAFlow().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daObject = daRep.WithoutTransactSave(new DAFlow() { FlowID = partId, UserID = userID, All = false });
                        }
                        success = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertFlow");
                    throw ex;
                }
            }
        }

        private bool DeleteFlow(decimal dataAccessId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    EntityRepository<DAFlow> daRep = new EntityRepository<DAFlow>(false);

                    daRep.WithoutTransactDelete(new DAFlow() { ID = dataAccessId });

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return true;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteFlow");
                    throw ex;
                }
            }
        }

        #endregion

        #region Report

        /// <summary>
        /// ریشه را برای هردو درخت برمیگرداند
        /// اگر شخص دسترسی به همه داشته باشد ریشه باید قابل حذف باشد
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataAccessProxy GetReportRoot(DataAccessLevelsType type, decimal userId)
        {
            if (type == DataAccessLevelsType.Source)
            {
                Report list = new BReport().GetReportRoot();
                Report result = new Report();
                result = list;
                return new DataAccessProxy() { ID = 0, Name = result.Name };
            }
            else
            {
                DataAccessProxy proxy = new DataAccessProxy();

                if (userRepository.HasAllReportAccess(userId) > 0)
                {
                    proxy.DeleteEnable = true;
                }
                return proxy;
            }
        }

        /// <summary>
        /// زیر گزارشات یک دسته گزارش را برمیگرداند
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetReportChilds(decimal parentId)
        {
            if (parentId == 0)
            {
                parentId = new BReport().GetReportRoot().ID;
            }
            IList<Report> list = reportRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Report().ParentId), parentId));
            var result = from o in list
                         select new DataAccessProxy()
                         {
                             ID = o.ID,
                             Name = o.Name,
                             IsReport = o.IsReport
                         };
            return result.ToList<DataAccessProxy>();
        }

        /// <summary>
        /// زیرگزارشهای قابل دسترس برای یک گروه گزارش را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetReportOfUser(decimal userId, decimal parentId)
        {
            try
            {
                BReport breport = new BReport();
                IList<Report> result = new List<Report>();

                if (parentId == 0)
                {
                    EntityRepository<DAReport> rep = new EntityRepository<DAReport>();
                    if (userRepository.HasAllReportAccess(userId) > 0)
                    {
                        Report root = breport.GetReportRoot();
                        result = breport.GetReportChildsWidoutDA(root.ID);
                    }
                    else
                    {
                        IList<DAReport> daList = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().UserID), userId));
                        var ids = from o in daList
                                  select o.Report;
                        result = ids.ToList();

                        ///حذف بچه از بین والدها
                        foreach (DAReport da1 in daList)
                        {
                            foreach (DAReport da2 in daList)
                            {
                                if (da1.Report.ChildList.Contains(da2.Report))
                                {
                                    result.Remove(da2.Report);
                                }
                            }
                        }

                        foreach (Report organ in result)
                        {
                            organ.Visible = true;
                        }
                    }
                }
                else
                {
                    result = breport.GetByID(parentId).ChildList;
                }
                var lastResult = from o in result
                                 select new DataAccessProxy()
                                 {
                                     ID = o.ID,
                                     Name = o.Name,
                                     DeleteEnable = o.Visible,
                                     IsReport = o.IsReport
                                 };
                return lastResult.ToList<DataAccessProxy>();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetReportOfUser");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool InsertReport(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool succes = false;
                    DAReport dareport = new DAReport();
                    EntityRepository<DAReport> daRep = new EntityRepository<DAReport>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    if (partId == 0)//درج همه
                    {
                        IList<DAReport> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                break;
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DAReport da in daPartList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            dareport = daRep.WithoutTransactSave(new DAReport() { UserID = userID, All = true, ReportID = null });
                        }
                        succes = true;
                    }
                    else
                    {
                        IList<DAReport> daSinglePartList = null;
                        IList<DAReport> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().UserID), userID),
                                                                                     new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().ReportID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().UserID), userID),
                                                                                     new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                dareport = daRep.WithoutTransactSave(new DAReport() { ReportID = partId, UserID = userID, All = false });
                        }
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return succes;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertReport");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// حذف یک گره 
        /// اگر دسترسی همه بخواهد حذف شود باید شناسه صفر باشد
        /// در هنگام واکشی در پرکسی شناسه بخش قرارداده میشود لذا در هنگام حذف نیز باید بر اساس شناسه بخش کار کنیم
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool DeleteReport(decimal reportId, decimal userId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    EntityRepository<DAReport> daRep = new EntityRepository<DAReport>(false);

                    if (reportId == 0)//ریشه مجازی برای کسانی که دسترسی یه همه دارند
                    {
                        IList<DAReport> daList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().UserID), userId),
                                                                     new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().All), true));
                        if (daList.Count > 0 && daList.First().All)
                        {
                            daRep.WithoutTransactDelete(daList.First());
                            success = true;
                        }
                    }
                    else
                    {
                        IList<DAReport> daList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().UserID), userId),
                                                                     new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().ReportID), reportId));
                        if (daList.Count > 0)
                        {
                            foreach (DAReport da in daList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                            success = true;
                        }
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteReport");
                    throw ex;
                }
            }
        }


        #endregion

        #region Corporations
        private IList<DataAccessProxy> GetAllCorporations()
        {
            try
            {
                IList<Corporation> CorporationList = organizationRepository.GetAll();
                IList<DataAccessProxy> DataAccessProxyList = CorporationList.Select(corporation => new DataAccessProxy() { ID = corporation.ID, Name = corporation.Name, CustomCode = corporation.Code, DeleteEnable = false, All = false }).ToList<DataAccessProxy>();
                return DataAccessProxyList;

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllCorporations");
                throw ex;
            }
        }

        private IList<DataAccessProxy> GetAllCorporationsOfUser(decimal userId)
        {
            try
            {
                IList<Corporation> CorporationsOfUserList = userRepository.GetUserCorporationList(userId);
                IList<DataAccessProxy> DataAccessProxyList = CorporationsOfUserList.Select(corporation => new DataAccessProxy() { ID = corporation.ID, Name = corporation.Name, DeleteEnable = true, CustomCode = corporation.Code }).ToList<DataAccessProxy>();
                return DataAccessProxyList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllCorporationsOfUser");
                throw ex;
            }
        }
        #endregion


        /// <summary>
        /// یک کاربر به همراه دسترسی اطلاعاتی را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserSecurityInfo(decimal userId)
        {
            try
            {
                User user = new BUser().GetByID(userId);
                return user;
            }
            catch (Exception ex)
            {
                BaseBusiness<User>.LogException(ex, "BDataAccess", "GetUserSecurityInfo");
                throw ex;
            }
        }


        private bool InsertCorporation(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DACorporation daCorporationObj = new DACorporation();
                    IList<decimal> TempUserIDList = new List<decimal>();
                    EntityRepository<DACorporation> DACorporationRep = new EntityRepository<DACorporation>(false);
                    switch (Dalot)
                    {
                        case DataAccessLevelOperationType.Single:
                            TempUserIDList.Add(userId);
                            break;
                        case DataAccessLevelOperationType.Group:
                            TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                            break;
                    }
                    foreach (decimal userID in TempUserIDList)
                    {
                        IList<DACorporation> DACorporationList = DACorporationRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DACorporation().UserID), userID));
                        if (DACorporationList.Count > 0)
                        {
                            foreach (DACorporation daCorporation in DACorporationList)
                            {
                                DACorporationRep.Delete(daCorporation);
                            }
                        }
                        DACorporationRep.Save(new DACorporation() { UserID = userID, CorporationID = partId });
                    }
                    success = true;
                    return success;
                }
                catch (Exception ex)
                {
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertCorporation");
                    throw ex;
                }
            }
        }

        private bool DeleteCorporation(decimal dataAccessId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    EntityRepository<DACorporation> DAOrganizationRep = new EntityRepository<DACorporation>(false);

                    DAOrganizationRep.WithoutTransactDelete(new DACorporation() { ID = dataAccessId });

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return true;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteCorporation");
                    throw ex;
                }
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMasterDataAccessLevelsLoadAccess()
        {
        }


    }
}
