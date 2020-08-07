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
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;

namespace GTS.Clock.Business.RequestFlow
{
    /// <summary>
    /// created at: 2011-12-31 9:40:21 AM
    /// by        : Farhad Salavati
    /// write your name here
    /// </summary>
    public class BSubstitute : BaseBusiness<Substitute>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.BSubstitute";
        private EntityRepository<Substitute> objectRep = new EntityRepository<Substitute>();
        private BOperator bOpoerator = new BOperator();
        private decimal curentUserPersonId = 0;
    
        #region Constructor

        public BSubstitute()
        {
            curentUserPersonId = BUser.CurrentUser.Person.ID;
        }

        /// <summary>
        /// تنها جهت تست
        /// </summary>
        /// <param name="personId"></param>
        public BSubstitute(decimal personId)
        {
            curentUserPersonId = personId;
        } 
        #endregion

        /// <summary>
        /// اگر اپراتور نباشد در واسط کاربر اجازه جستجوی مدیر داده نمیشود
        /// </summary>
        public bool ShowManagerSearchButtomns
        {
            get
            {
                return this.IsOperator();
            }
        }

        public IList<Substitute> GetSubstitute(decimal personId)
        {
            SubstituteRepository rep = new SubstituteRepository(false);
            IList<Substitute> list = rep.GetSubstitute(personId);
            if (list != null && list.Count > 0)
            {
                return list;
            }
            return new List<Substitute>();
        }

        /// <summary>
        /// مدیر یک جانشین را برمیگرداند
        /// 
        /// اگر جانشین چند نفر باشد فعلا اولی را برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public decimal GetSubstituteManager(decimal personId) 
        {
            SubstituteRepository rep = new SubstituteRepository(false);
            IList<Substitute> list = rep.GetSubstitute(personId);
            if (list != null && list.Count > 0) 
            {
                list = list.Where(x => x.Active && x.FromDate <= DateTime.Now.Date && x.ToDate >= DateTime.Now.Date).ToList();
                if (list.Count > 0) 
                {
                    if (list.First().Manager != null)
                    {
                        return list.First().Manager.ID;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// مدیران یک جانشین را برمیگرداند
        /// 
        /// اگر جانشین چند نفر باشد فعلا اولی را برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public IList<decimal> GetSubstituteManagerList(decimal personId)
        {
            SubstituteRepository rep = new SubstituteRepository(false);
            IList<Substitute> list = rep.GetSubstitute(personId);
            if (list != null && list.Count > 0)
            {
                list = list.Where(x => x.Active && x.FromDate <= DateTime.Now.Date && x.ToDate >= DateTime.Now.Date).ToList();
                if (list.Count > 0)
                {
                    var ids = from o in list
                              where o.Active
                              select o.Manager.ID;
                    return ids.ToList();                 
                }
            }
            return new List<decimal>();
        }

        #region GetAll

        /// <summary>
        /// تعداد مدیران را برمیگرداند
        /// اگر کاربر فعلی اپراتور فقط در بین مدیران زیر دست اپراتور جست و جو میکنیم
        /// ولی اگر مدیر باشد نتایج جست و جو تهی میباشد
        /// اگر هم مدیر باشد و هم اپراتور آنگاه خود شخص به علاوه مدیران تحت مدیریت برمیگردد
        /// </summary>
        /// <returns></returns>
        public int GetAllManagerCount()
        {
            try
            {
                int resultCount = 0;
                resultCount = this.GetAllManagerCount(String.Empty);               
                return resultCount;
            }
            catch (Exception ex)
            {
                BaseBusiness<Substitute>.LogException(ex, "BSubstitute", "GetAllManagerCount");
                throw ex;
            }
        }

        /// <summary>
        /// مدیران را برمیگرداند
        /// اگر کاربر فعلی اپراتور فقط در بین مدیران زیر دست اپراتور جست و جو میکنیم
        /// ولی اگر مدیر باشد نتایج جست و جو تهی میباشد
        /// اگر هم مدیر باشد و هم اپراتور آنگاه خود شخص به علاوه مدیران تحت مدیریت برمیگردد
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<Person> GetAllManager(int pageIndex, int pageSize)
        {
            try
            {
                IList<Person> result = new List<Person>();
                result = this.GetAllManager(String.Empty, pageIndex, pageSize);
               
                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Substitute>.LogException(ex, "BSubstitute", "GetAllManager");
                throw ex;
            }
        }

        /// <summary>
        /// جانشینان یک مدیر را برمیگرداند
        /// </summary>
        /// <param name="managerPersonId">شناسه پرسنلی مدیر</param>
        /// <returns></returns>
        public IList<Substitute> GetAllByManager(decimal managerPersonId, string searchKey)
        {
            try
            {
                ManagerRepository rep = new ManagerRepository(false);
                Manager manager = rep.GetManagerByPersonID(managerPersonId);
                if (manager == null)
                {
                    return new List<Substitute>();
                }
                IList<Substitute> list = manager.SubstituteList;
                if (list == null)
                {
                    list = new List<Substitute>();
                }
                list = list.Where(x => x.Active).ToList();
                if (!Utility.IsEmpty(searchKey))
                {
                    searchKey = searchKey.ToLower();
                    list = list.Where(x => x.Person.BarCode.ToLower().Contains(searchKey)
                        || x.Person.Name.ToLower().Contains(searchKey)
                        || x.Person.OrganizationUnit.Name.ToLower().Contains(searchKey)).ToList();
                }
                foreach (Substitute sub in list) 
                {
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        sub.TheFromDate = Utility.ToPersianDate(sub.FromDate);
                        sub.TheToDate = Utility.ToPersianDate(sub.ToDate);
                    }
                    else
                    {
                        sub.TheFromDate = Utility.ToPersianDate(sub.FromDate);
                        sub.TheToDate = Utility.ToPersianDate(sub.ToDate);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BSubstitute", "GetAllByManager");
                throw ex;
            }
        }

        /// <summary>
        /// جریان های یک مدیر را برمیگرداند
        /// مشخص میکند که آیا توسط جانشین استفاده شده است یا خیر
        /// </summary>
        /// <param name="managerPersonId">شناسه پرسنلی مدیر</param>
        /// <returns></returns>
        public IList<Flow> GetAllFlowByManager(decimal substituteId, decimal managerPersonId)
        {
            try
            {
                ManagerRepository rep = new ManagerRepository(false);
                FlowRepository flowRep = new FlowRepository(false);
                Manager manager = rep.GetManagerByPersonID(managerPersonId);
                if (manager == null)
                {
                    return new List<Flow>();
                }
                var flowList = from mngFlw in manager.ManagerFlowList
                               where mngFlw.Active
                               select mngFlw.Flow;
             
                foreach (Flow flow in flowList)
                {
                    if (flowRep.GetSubstituteAccessGroupCount(substituteId, flow.ID) > 0)
                    {
                        flow.IsAssignedToSubstitute = true;
                    }
                }
                return flowList.ToList<Flow>();
            }
            catch (Exception ex)
            {
                LogException(ex, "BSubstitute", "GetAllFlowByManager");
                throw ex;
            }
        }

        public Department GetDepartmentRoot() 
        {
            BFlow bFlow = new BFlow();
            return bFlow.GetDepartmentRoot();
        }

        public IList<Department> GetDepartmentChilds(decimal nodeID, decimal flowId) 
        {
            BFlow bFlow = new BFlow();
            return bFlow.GetDepartmentChilds(nodeID, flowId);
        }

        public IList<Person> GetDepartmentPerson(decimal departmentID, decimal flowId)
        {
            BFlow bFlow = new BFlow();
            return bFlow.GetDepartmentPerson(departmentID, flowId);
        }

        /// <summary>
        /// همه پیشکارتها را جهت نمایش گروهی برمیگرداند
        /// اگر پارامتر ورودی صفر باشد بدین معنی است که
        /// در مد اینزرت هستیم
        /// </summary>
        /// <param name="accessGroupId"></param>
        /// <returns></returns>
        public IList<PrecardGroups> GetPrecardTree(decimal substituteId, decimal flowId)
        {
            try
            {
                BPrecardAccessGroup bpAccess = new BPrecardAccessGroup();
                BFlow flow = new BFlow();
                PrecardAccessGroup group = flow.GetByID(flowId).AccessGroup;
                IList<PrecardGroups> list = bpAccess.GetPrecardTree(group.ID);

                Substitute substitute = this.GetByID(substituteId);
                IList<Precard> precards = substitute.PrecardList;


                foreach (PrecardGroups g in list)
                {
                    int childCount = 0;
                    foreach (Precard p in g.PrecardList)
                    {
                        p.ContainInPrecardAccessGroup = false;
                        if (precards.Where(x => x.ID == p.ID).Count() > 0)
                        {
                            p.ContainInPrecardAccessGroup = true;
                            childCount++;
                        }
                    }
                    g.ContainInPrecardAccessGroup = false;
                    if (childCount ==g.PrecardList.Count)
                        g.ContainInPrecardAccessGroup = true;
                }
                return list;
            }
            catch (Exception ex) 
            {
                LogException(ex, "BSubstitute", "GetPrecardTree");
                throw ex;
            }
        }


        #endregion


        /// <summary>
        /// پیشکارتهای یک جانشین را ثبت میکند
        /// اگر پارانتر آخر تهی باشد بدین معنی است که جریان حذف شود یعنی تمام پیشکارتهایش خالی شود
        /// </summary>
        /// <param name="substituteId"></param>
        /// <param name="flowId"></param>
        /// <param name="accessGroupList"></param>
        /// <returns></returns>
        public bool UpdateByProxy(decimal substituteId, decimal flowId, IList<AccessGroupProxy> accessGroupList)
        {
            try
            {
                if (substituteId == 0 || flowId == 0)
                {
                    UIValidationExceptions exception = new UIValidationExceptions();
                    exception.Add(new ValidationException(ExceptionResourceKeys.SubstituteUpdateFlowAndSubstituteIdRequeiered, "کد جریان و جانشین نامشخص است", ExceptionSrc));
                }
                bool substituteContainsFlow = false;//تیک جریان کاری در گرید مربوطه
                BFlow flow = new BFlow();
                PrecardAccessGroup precardAccessGroup = flow.GetByID(flowId).AccessGroup;
                EntityRepository<PrecardGroups> groupRep = new EntityRepository<PrecardGroups>(false);
                IList<Precard> removeList = new List<Precard>();
                Substitute substitute = this.GetByID(substituteId);
                substitute.PrecardAccessIsSet = true;
                substitute.TheFromDate = Utility.ToPersianDate(substitute.FromDate);
                substitute.TheToDate = Utility.ToPersianDate(substitute.ToDate);
                //ابتدا همگی حذف و سپس ایجاد میگردد
                substitute.PrecardList = new List<Precard>();

                if (accessGroupList != null)
                {
                    foreach (AccessGroupProxy proxy in accessGroupList)
                    {
                        if (proxy.IsParent)
                        {
                            PrecardGroups group = groupRep.GetById(proxy.ID, false);
                            foreach (Precard p in group.PrecardList)
                            {                                
                                    substituteContainsFlow = true;
                                    substitute.PrecardList.Add(p);
                            }
                        }
                        else if (proxy.Checked)
                        {
                            substituteContainsFlow = true;
                            substitute.PrecardList.Add(new Precard() { ID = proxy.ID });
                        }
                        else
                        {
                            removeList.Add(new Precard() { ID = proxy.ID });
                        }
                    }
                    foreach (Precard p in removeList)
                    {
                        substitute.PrecardList.Remove(p);
                    }
                }
                SaveChanges(substitute, UIActionType.EDIT);
                return substituteContainsFlow;
            }
            catch (Exception ex)
            {
                LogException(ex, "BSubstitute", "UpdateByProxy");
                throw ex;
            }
        }
        

        #region Manager Search

        /// <summary>
        /// تعداد مدیران در جستجو
        /// اگر کاربر فعلی اپراتور فقط در بین مدیران زیر دست اپراتور جست و جو میکنیم
        /// ولی اگر مدیر باشد نتایج جست و جو تهی میباشد
        /// اگر هم مدیر باشد و هم اپراتور آنگاه خود شخص به علاوه مدیران تحت مدیریت برمیگردد
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public int GetAllManagerCount(string searchKey)
        {
            ISearchPerson searchTools = new BPerson();           
            int count = searchTools.GetPersonInQuickSearchCount(searchKey, PersonCategory.Manager);
            return count;
        }

        /// <summary>
        /// مدیران در جستجو
        /// اگر کاربر فعلی اپراتور فقط در بین مدیران زیر دست اپراتور جست و جو میکنیم
        /// ولی اگر مدیر باشد نتایج جست و جو تهی میباشد
        /// اگر هم مدیر باشد و هم اپراتور آنگاه خود شخص به علاوه مدیران تحت مدیریت برمیگردد
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<Person> GetAllManager(string searchKey, int pageIndex, int pageSize)
        {
            IList<Person> result = new List<Person>();
            ISearchPerson searchTool = new BPerson();           
            result = searchTool.QuickSearchByPage(pageIndex, pageSize, searchKey, PersonCategory.Manager);
            return result;
        }

        /// <summary>
        /// تعداد مدیران جستجو شده
        /// اگر کاربر فعلی اپراتور فقط در بین مدیران زیر دست اپراتور جست و جو میکنیم
        /// ولی اگر مدیر باشد نتایج جست و جو تهی میباشد
        /// اگر هم مدیر باشد و هم اپراتور آنگاه خود شخص به علاوه مدیران تحت مدیریت برمیگردد
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public int GetAllManagerCount(PersonAdvanceSearchProxy proxy)
        {
            //if (!IsOperator()) { return 0; }
            ISearchPerson searchTools = new BPerson();
            int count = searchTools.GetPersonInAdvanceSearchCount(proxy, PersonCategory.Manager);
            return count;
        }

        /// <summary>
        /// لیست مدیران جستجو شده
        /// اگر کاربر فعلی اپراتور فقط در بین مدیران زیر دست اپراتور جست و جو میکنیم
        /// ولی اگر مدیر باشد نتایج جست و جو تهی میباشد
        /// اگر هم مدیر باشد و هم اپراتور آنگاه خود شخص به علاوه مدیران تحت مدیریت برمیگردد
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<Person> GetAllManager(PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize)
        {
            IList<Person> result = new List<Person>();
            //if (!IsOperator()) { return result; }
            //PersonRepository prsRep = new PersonRepository();
            //var ids = from mng in this.GetAllManager(0, this.GetAllManagerCount())
            //          select mng.ID;
            //IList<decimal> list = ids.ToList<decimal>();
            //proxy.SearchInCategory = PersonCategory.Manager;
            //result = prsRep.GetPersonInAdvanceSearch(proxy, list, pageIndex, pageSize);

            ISearchPerson searchTool = new BPerson();
            result = searchTool.GetPersonInAdvanceSearch(proxy, pageIndex, pageSize, PersonCategory.Manager);
            return result;
        }


        #endregion


        #region Base Business Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(Substitute substitute)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (substitute.Manager == null || substitute.Manager.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.SubstituteManagerRequiered, "مدیر جانشین مشخص نشده است", ExceptionSrc));
            }

            if (substitute.Person == null || substitute.Person.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.SubstitutePersonRequiered, "جانشین مشخص نشده است", ExceptionSrc));
            }

            if (substitute.FromDate == Utility.GTSMinStandardDateTime || substitute.ToDate == Utility.GTSMinStandardDateTime)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.SubstituteDateRequired, "بازه تاریخی جانشین نامعتبر است ", ExceptionSrc));
            }
            else if (substitute.FromDate > substitute.ToDate)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.SubstituteFromDateGreaterThanToDate, "ابتدای تاریخ از انتها بزرگتر است ", ExceptionSrc));

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
        protected override void UpdateValidate(Substitute substitute)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (substitute.ID == 0) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.SubstituteIsNotSpecified, "شناسه جانشین جهت بروزرسانی مشخص نشده است", ExceptionSrc));
            }
            if (substitute.Manager == null || substitute.Manager.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.SubstituteManagerRequiered, "مدیر جانشین مشخص نشده است", ExceptionSrc));
            }

            if (substitute.Person == null || substitute.Person.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.SubstitutePersonRequiered, "جانشین مشخص نشده است", ExceptionSrc));
            }

            if (substitute.FromDate == Utility.GTSMinStandardDateTime || substitute.ToDate == Utility.GTSMinStandardDateTime)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.SubstituteDateRequired, "بازه تاریخی جانشین نامعتبر است ", ExceptionSrc));
            }
            else if (substitute.FromDate > substitute.ToDate)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.SubstituteFromDateGreaterThanToDate, "ابتدای تاریخ از انتها بزرگتر است ", ExceptionSrc));
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
        protected override void DeleteValidate(Substitute substitute)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// تنظیم تاریخها
        /// </summary>
        /// <param name="substitute"></param>
        /// <param name="action"></param>
        protected override void GetReadyBeforeSave(Substitute substitute, UIActionType action)
        {
            decimal mangerPersonId = substitute.ManagerPersonId;
         
            substitute.FromDate = Utility.GTSMinStandardDateTime;
            substitute.ToDate = Utility.GTSMinStandardDateTime;
            if (!Utility.IsEmpty(substitute.TheFromDate) && !Utility.IsEmpty(substitute.TheToDate))
            {
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    substitute.FromDate = Utility.ToMildiDate(substitute.TheFromDate);
                    substitute.ToDate = Utility.ToMildiDate(substitute.TheToDate);
                }
                else
                {
                    substitute.FromDate = Utility.ToMildiDateTime(substitute.TheFromDate);
                    substitute.ToDate = Utility.ToMildiDateTime(substitute.TheToDate);
                }
            }
            if (substitute.ManagerPersonId > 0) 
            {
                BManager busManager = new BManager();
                Manager manager = busManager.GetManager(substitute.ManagerPersonId);
                substitute.Manager = manager;
            }
            //PrecardList Recover
            if (action == UIActionType.EDIT && !substitute.PrecardAccessIsSet)
            {
                Substitute sub = this.GetByID(substitute.ID);
                substitute.PrecardList = sub.PrecardList;
                NHibernateSessionManager.Instance.ClearSession();
            }
        }

        /// <summary>
        /// ذخیره سطوح دسترسی جانشین در هنگام درج
        /// </summary>
        /// <param name="substitute"></param>
        /// <param name="action"></param>
        protected override void OnSaveChangesSuccess(Substitute substitute, UIActionType action)
        {
            if (action == UIActionType.ADD) 
            {
                IList<Precard> list = new ManagerRepository(false).GetAllAccessGroup(substitute.Manager.ID);
                substitute.PrecardList = list;
                objectRep.Update(substitute);
                new BRole().SetUserRole(substitute.Person, RoleCustomCode.Substitute);
            }
            if (action == UIActionType.EDIT) 
            {
                new BRole().SetUserRole(substitute.Person, RoleCustomCode.Substitute);
            }
        }
        #endregion

        /// <summary>
        /// آیا کاربر فعلی اپراتور است
        /// </summary>
        /// <returns></returns>
        private bool IsOperator()
        {
            BOperator op = new BOperator();
            IList<Operator> opList = op.GetOperator(curentUserPersonId);
            return opList.Count > 0 ? true : false;
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSubstitiuteLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertSubstitute(Substitute substitute, UIActionType UAT)
        {
            return base.SaveChanges(substitute, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateSubstitute(Substitute substitute, UIActionType UAT)
        {
            return base.SaveChanges(substitute, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteSubstitute(Substitute substitute, UIActionType UAT)
        {
            return base.SaveChanges(substitute, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSubstituteSettingsLoadAccess()
        {
        }



       
    }
}
