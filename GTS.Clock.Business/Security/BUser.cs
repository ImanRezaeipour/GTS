using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Security;
using GTS.Clock.Model;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using System.Web;
using GTS.Clock.Model.Charts;
using GTS.Clock.Business.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Report;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Business.Rules;

namespace GTS.Clock.Business.Security
{
    public class BUser : BaseBusiness<User>,IDataAccess
    {
        private const string ExceptionSrc = "GTS.Clock.Business.Security.BUser";
        UserRepository userRepository = new UserRepository(false);

        #region Login Services
  
        /// <summary>
        /// :اربر فعلی را برمیگرداند
        /// اگر خالی باشد تهی برمیگرداند
        /// </summary>
        public static User CurrentUser
        {
            get
            {
                try
                {
                    string username = "";
                    if (System.Web.HttpContext.Current != null &&
                        System.Web.HttpContext.Current.User != null &&
                        System.Web.HttpContext.Current.User.Identity != null)
                    {
                        username = System.Web.HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        username = "NUnitUser";
                    }                  

                    if (!SessionHelper.HasSessionValue(SessionHelper.BussinessCurentUser))
                    {
                        UserRepository userRep = new UserRepository(false);
                        Model.Security.User proxyUser = new User();
                        proxyUser.Person = new Person();
                        proxyUser.Role = new Role();
                        
                        Model.Security.User user = userRep.GetByUserName(Utility.GetSimpleUsername(username));
                        if (user == null || user.ID == 0)
                        {
                            user = new User();
                            user.UserName = "";
                            user.Role = new Role();
                            user.Person = new GTS.Clock.Model.Person();
                        }
                        proxyUser.ID = user.ID;
                        proxyUser.UserName = user.UserName;
                        proxyUser.Role.ID = user.Role.ID;
                        proxyUser.Role.CustomCode = user.Role.CustomCode;
                        proxyUser.Role.Name = user.Role.Name;
                        proxyUser.Person.ID = user.Person.ID;
                        proxyUser.Person.FirstName = user.Person.FirstName;
                        proxyUser.Person.LastName = user.Person.LastName;
                        proxyUser.Person.PersonCode = user.Person.PersonCode;
                        proxyUser.Person.Sex = user.Person.Sex;
                        SessionHelper.SaveSessionValue(SessionHelper.BussinessCurentUser, proxyUser);
                        //NHibernateSessionManager.Instance.ClearSession();
                        return proxyUser;
                    }
                    object obj = SessionHelper.GetSessionValue(SessionHelper.BussinessCurentUser);
                    if (obj != null)
                        return (User)obj;
                    else
                    {
                        Model.Security.User user = new User();
                        user = new User();
                        user.UserName = "";
                        user.Role = new Role();
                        user.Person = new GTS.Clock.Model.Person();
                        return user;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// نشست مربوط به کاربر را خالی میکند
        /// </summary>
        public static void ClearUserCach()
        {
            SessionHelper.ClearAllCachedData();           
        }

        /// <summary>
        /// اگر شناسه کاربری موجود نباشد تهی برمیگرداند
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetByUsername(string username)
        {
            User user = userRepository.GetByUserName(username);
            return user;
        }

        public decimal GetPersonIdByUsername(string username)
        {
            User user = userRepository.GetByUserName(username);
            if (user != null && user.Person != null)
                return user.Person.ID;
            return 0;
        }

        /// <summary>
        ///  باید پروایدر آن تغییر کنداگر شخص قرار است از اکتیو دایرکتوری استفاده کند 
        /// </summary>
        /// <param name="username"></param>
        public MembershipProviders GetDefaultAuthenticationProvider(string username)
        {
            //if (this.ActiveDirectoryISAllowed(username))
            //{
            //    return MembershipProviders.ADMembershipProvider;
            //}
            return MembershipProviders.GTSMembershipProvider;
        }

        /// <summary>
        /// تنظیم نام کاربری کامل در دامین
        /// </summary>
        /// <param name="username"></param>
        public string GetCompleteDoaminUsername(string username)
        {
            username = Utility.GetSimpleUsername(username);
            User user = this.GetByUsername(username);
            if (user.ActiveDirectoryAuthenticate)
            {
                string doamin = "";// this.GetDomainInfo;
                if (doamin.Length > 0)
                {
                    return username + "@" + doamin;
                }
            }
            return username;
        }

        /// <summary>
        /// تعویض کلمه عبور
        /// </summary>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="reNewPassword"></param>
        /// <returns></returns>
        public bool ChangePassword(string currentPassword, string newPassword, string reNewPassword)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    UIValidationExceptions exception = new UIValidationExceptions();
                    if (Utility.IsEmpty(newPassword) || Utility.IsEmpty(currentPassword))
                    {
                        exception.Add(ExceptionResourceKeys.UserPasswordIsNull, "Password is null", ExceptionSrc);
                    }
                    if (!newPassword.Equals(reNewPassword))
                    {
                        exception.Add(ExceptionResourceKeys.UserConfirmPasswordNotEqual, "ConfirmPassword is not equal to password", ExceptionSrc);
                    }
                    if (exception.Count > 0)
                    {
                        throw exception;
                    }
                    GTSMembershipProvider mermbership = new GTSMembershipProvider();
                    bool success = mermbership.ChangePassword(BUser.CurrentUser.UserName, currentPassword, newPassword);
                    if (!success)
                    {
                        exception.Add(ExceptionResourceKeys.UserConfirmPasswordNotEqual, "password is not corrent", ExceptionSrc);
                        throw exception;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    LogUserAction("Edit");
                    return success;
                }
                catch (Exception ex)
                {
                    LogException(ex, "BUser", "ChangePassword");
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    throw ex;
                }               
            }
        }

        /// <summary>
        /// شناسه کاربر جاری
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUserName() 
        {
            return BUser.CurrentUser.UserName;
        }

        #endregion

        #region List And Search

        /// <summary>
        /// تعداد پرسنل را باتوجه به کلمه جستجو برمیگرداند
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public int QuickSearchPersonCount(string searchKey)
        {
            try
            {
                ISearchPerson bperson = new BPerson();
                int count = bperson.GetPersonInQuickSearchCount(searchKey);
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "QuickSearchPersonCount");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد کل کاربران
        /// </summary>
        /// <returns></returns>
        public int GetAllUsersCount()
        {
            try
            {
                ISearchPerson bperson = new BPerson();
                int count = bperson.GetPersonCount();
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "GetAllUsersCount");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد نتایج جستجو را برمیگرداند
        /// </summary>
        /// <param name="key"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int GetAllByPageBySearchCount(UserSearchKeys key, string searchValue)
        {
            try
            {
                IDataAccess dataAccess = new BUser();
                int userCount = 0;
                //decimal [] arr=dataAccess.GetAccessiblePersonByDepartment().ToArray();
                userCount = userRepository.GetNumberOfUsersByQuickSearch(key, searchValue, BUser.CurrentUser.ID);
                /*switch (key)
                {
                    case UserSearchKeys.PersonCode:
                        userCount = userRepository.GetNumberOfUsersByBarcode(searchValue,arr );
                        break;
                    case UserSearchKeys.Name:
                        userCount = userRepository.GetNumberOfUsersByName(searchValue, arr);
                        break;
                    case UserSearchKeys.Username:
                        userCount = userRepository.GetNumberOfUsersByUserName(searchValue, arr);
                        break;
                    case UserSearchKeys.RoleName:
                        userCount = userRepository.GetNumberOfUsersByRoleName(searchValue, arr);
                        break;
                    case UserSearchKeys.NotSpecified:
                        userCount = userRepository.GetNumberOfUsersByQuickSearch(searchValue, BUser.CurrentUser.ID);
                        break;
                    default:
                        return 0;
                }*/
                return userCount;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "GetAllByPageBySearchCount");
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
                ISearchPerson bperson = new BPerson(SysLanguageResource.English, LocalLanguageResource.English);
                IList<Person> list = bperson.QuickSearchByPage(pageIndex, pageSize, searchKey);
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "QuickSearchPersonByPage");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<UserProxy> GetAllByPage(int startIndex, int pageSize)
        {
            try
            {
                IDataAccess dataAccess = new BUser();
                int userCount = 0;
                //decimal[] arr = dataAccess.GetAccessiblePersonByDepartment().ToArray();
                IList<User> list = userRepository.GetAllByPage(BUser.CurrentUser.ID, startIndex, pageSize);
                IList<UserProxy> users = new List<UserProxy>();
                for (int i = 0; i < list.Count; i++)
                {
                    User user = list[i];
                    UserProxy proxy = new UserProxy(user);
                    if (proxy.Active && !Utility.IsEmpty(user.Person.EndEmploymentDate))
                    {
                        proxy.Active = DateTime.Now > user.Person.EndEmploymentDate ? false : true;
                    }
                    users.Add(proxy);
                }
                return users;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "GetAllUsers");
                throw ex;
            }
        }

        /// <summary>
        /// نتایج جستجو
        /// </summary>
        /// <param name="key"></param>
        /// <param name="searchValue"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<UserProxy> GetAllByPageBySearch(UserSearchKeys key, string searchValue, int pageIndex, int pageSize)
        {
            try
            {
                IList<User> list = null;
                IDataAccess dataAccess = new BUser();
                list = userRepository.GetAllByPageQuickSearch(key, searchValue, BUser.CurrentUser.ID, pageIndex, pageSize);
                /*
                decimal[] arr = dataAccess.GetAccessiblePersonByDepartment().ToArray();
                switch (key)
                {
                    case UserSearchKeys.PersonCode:
                        list = userRepository.GetAllByPageBarcode(searchValue, pageIndex, pageSize, arr);
                        break;
                    case UserSearchKeys.Name:
                        list = userRepository.GetAllByPageName(searchValue, pageIndex, pageSize, arr);
                        break;
                    case UserSearchKeys.Username:
                        list = userRepository.GetAllByPageUserName(searchValue, pageIndex, pageSize, arr);
                        break;
                    case UserSearchKeys.RoleName:
                        list = userRepository.GetAllByPageRoleName(searchValue, pageIndex, pageSize, arr);
                        break;
                    case UserSearchKeys.NotSpecified:
                        list = userRepository.GetAllByPageQuickSearch(searchValue, pageIndex, pageSize, arr);
                        break;

                }*/
                if (list != null)
                {
                    IList<UserProxy> users = new List<UserProxy>();
                    foreach (User user in list)
                    {
                        UserProxy proxy = new UserProxy(user);
                        if (proxy.Active && !Utility.IsEmpty(user.Person.EndEmploymentDate))
                        {
                            proxy.Active = DateTime.Now > user.Person.EndEmploymentDate ? false : true;
                        }
                        users.Add(proxy);
                    }

                    return users;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetAllByPageBySearch");
                throw ex;
            }
        }

        /// <summary>
        /// درخت نقشها را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public Role GetRoleTree()
        {
            try
            {
                BRole brole = new BRole();
                return brole.GetRoleTree();
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "GetRoleTree");
                throw ex;
            }
        }

        public IList<Role> GetRoleChilds(decimal roleId) 
        {
            try
            {
                BRole brole = new BRole();
                return brole.GetRoleChilds(roleId);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "GetRoleChilds");
                throw ex;
            }
        } 

        /// <summary>
        /// لیست دامین های نوجود در سرور را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<Domains> GetActiveDirectoryDomains()
        {
            try
            {
                EntityRepository<Domains> rep = new EntityRepository<Domains>();
                IList<Domains> list = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Domains().Active), true));
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "GetActiveDirectoryDomains");
                throw ex;
            }
        }

        /// <summary>
        /// لیست کاربران یک دامین را برمیگرداند
        /// </summary>
        /// <param name="doaminName"></param>
        /// <returns></returns>
        public IList<string> GetActiveDirectoryUsers(decimal domianId)
        {
            try
            {
                IList<string> result = new List<string>();
                BActiveDirectory bAD = new BActiveDirectory();
                Domains domain = bAD.GetById(domianId);

                LdapAuthentication ldap = new LdapAuthentication(domain.Domain, domain.UserName, domain.Password);
                result = ldap.GetAllDomainUsers();
                if (result != null) 
                {
                    result = result.OrderBy(a => a).ToList();
                    //Array.Sort<string>(result.ToArray());
                }
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "GetActiveDirectoryUsers");
                throw ex;
            }
        }

        #endregion


        /// <summary>
        /// درج
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertUser(UserProxy proxy)
        {
            try
            {
                User user = UserProxy.Export(proxy);
                return this.SaveChanges(user, UIActionType.ADD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal EditUser(UserProxy proxy)
        {
            try
            {
                User user = UserProxy.Export(proxy);
                return this.SaveChanges(user, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "EditUser");
                throw ex;
            }
        }

        /// <summary>
        /// بروز رسانی نقش کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public decimal EditUser(decimal userId, decimal roleId) 
        {
            try
            {
                User user = this.GetByID(userId);
                user.Role = new Role() { ID = roleId };
                return this.SaveChanges(user, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "EditUser Role");
                throw ex;
            }
        }

        /// <summary>
        /// حذف
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteUser(UserProxy proxy)
        {
            try
            {
                User user = UserProxy.Export(proxy);
                return this.SaveChanges(user, UIActionType.DELETE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void InsertValidate(User user)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (user.Person == null || user.Person.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserPersonIsNotSpecified, "درج - Person not Provided", ExceptionSrc));
            }
            if (user.Role == null || user.Role.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserRoleIsNotSpecified, "درج - Person not Provided", ExceptionSrc));
            }
            if (Utility.IsEmpty(user.UserName))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UsernameNotProvided, "درج - Username not Provided", ExceptionSrc));
            }
            else if (!user.ActiveDirectoryAuthenticate && Utility.IsEmpty(user.Password))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserPasswordIsNull, "درج - Password not Provided", ExceptionSrc));
            }
            else if (!user.ActiveDirectoryAuthenticate && user.Password != user.ConfirmPassword)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserConfirmPasswordNotEqual, "درج - ConfirmPassword is not equal to password", ExceptionSrc));
            }
            if (userRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => user.UserName), user.UserName)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UsernameReplication, "درج - Username Replication", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(User user)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (user.Person == null || user.Person.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserPersonIsNotSpecified, "بروزرسانی - Person not Provided", ExceptionSrc));
            }
            if (user.Role == null || user.Role.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserRoleIsNotSpecified, "بروزرسانی - Person not Provided", ExceptionSrc));
            }
            if (Utility.IsEmpty(user.UserName))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UsernameNotProvided, "بروزرسانی - Username not Provided", ExceptionSrc));
            }
            else if (!user.ActiveDirectoryAuthenticate && Utility.IsEmpty(user.Password))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserPasswordIsNull, "بروزرسانی - Password not Provided", ExceptionSrc));
            }
            else if (!user.ActiveDirectoryAuthenticate && user.IsPasswodChanged && user.Password != user.ConfirmPassword)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserConfirmPasswordNotEqual, "بروزرسانی - ConfirmPassword is not equal to password", ExceptionSrc));
            }

            if (userRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => user.UserName), user.UserName),
                                                  new CriteriaStruct(Utility.GetPropertyName(() => user.ID), user.ID, CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UsernameReplication, "بروزرسانی - Username Replication", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void DeleteValidate(User user)
        {
            UIValidationExceptions exception = new UIValidationExceptions();


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void GetReadyBeforeSave(User user, UIActionType action)
        {
            if (!Utility.IsEmpty(user.Password) && (action == UIActionType.ADD || (action == UIActionType.EDIT && user.IsPasswodChanged)))
            {
                user.Password = Utility.GetHashCode(user.Password);
                if (Utility.VerifyHashCode(user.ConfirmPassword, user.Password))
                {
                    //این تساوی بعدا در اعتبارسنجی بررسی میشود
                    user.ConfirmPassword = user.Password;
                }
            }
            ///ممکن است شخص نخواهد کلمه عبور را ویرایش کند
            else if (action == UIActionType.EDIT && !user.IsPasswodChanged)
            {
                user.Password = userRepository.GetPasswordByUserId(user.ID);
            }
            if (action == UIActionType.ADD)
            {
                user.LastActivityDate = DateTime.Now;
            }
            else if (action == UIActionType.EDIT)
            {
                user.LastActivityDate = userRepository.GetLastActivityDateByUserId(user.ID);
                if (Utility.IsEmpty(user.LastActivityDate))
                {
                    user.LastActivityDate = DateTime.Now;
                }
            }

            
        }

        protected override void OnSaveChangesSuccess(User user, UIActionType action)
        {
            try
            {
                if (action == UIActionType.ADD) //ایجاد تنظیمات
                {
                    UserSettings userSettings = new UserSettings();
                    userSettings.Language = new Languages() { ID = BLanguage.GetCurrentSystemLanguage().ID };
                    userSettings.User = user;

                    EntityRepository<UserSettings> setRep = new EntityRepository<UserSettings>(false);
                    setRep.Save(userSettings);
                }
            }
            catch (Exception ex) 
            {
                LogException(ex, "UserSettings Creation");
                throw ex;
            }
        }

        #region IDataAccess Members

        /// <summary>
        /// بخشهایی که اجازه دسترسی به آنها دارد به همراه پدران و بچه ها را برمیگرداند
        /// </summary>
        /// <returns></returns>
        IList<decimal> IDataAccess.GetAccessibleDeparments()
        {
            BApplicationSettings.CheckGTSLicense();

            if (userRepository.HasAllDepartmentAccess(this.CurrentUserId))
            {
                IList<Department> list = new DepartmentRepository(false).GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>();
            }
            else
            {
                BDepartment bDepartment = new BDepartment();
                List<decimal> depList = new List<decimal>();
                List<decimal> childAndParentsList = new List<decimal>();
                depList.AddRange(userRepository.GetUserDepartmentList(this.CurrentUserId));
                foreach (decimal depId in depList)//اضافه کردن بچه ها و والد ها 
                {
                    IList<Department> childs = new List<Department>();
                    childs = bDepartment.GetDepartmentChildsByParentPath(depId);
                    childAndParentsList.AddRange(bDepartment.GetByID(depId).ParentPathList);
                    var ids = from child in childs
                              select child.ID;
                    childAndParentsList.AddRange(ids.ToList<decimal>());
                }
                depList.AddRange(childAndParentsList);
                return depList;
            }
        }

        IList<decimal> IDataAccess.GetAccessibleOrgans()
        {
            if (userRepository.HasAllOrganAccess(this.CurrentUserId))
            {
                IList<OrganizationUnit> list = new BOrganizationUnit().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>();
            }
            else
            {
                IDataAccess asscessPort=new BUser();
                BOrganizationUnit borgan = new BOrganizationUnit();

                List<decimal> organList = new List<decimal>();
                List<decimal> childAndParentsList = new List<decimal>();
                organList.AddRange(userRepository.GetUserOrganList(this.CurrentUserId));
                foreach (decimal organId in organList)//اضافه کردن بچه ها و والد ها 
                {
                    IList<OrganizationUnit> childs = new List<OrganizationUnit>();
                    childs = borgan.GetOrganiztionChildsByParentPath(organId);
                    childAndParentsList.AddRange(borgan.GetByID(organId).ParentPathList);
                    var ids = from child in childs
                              select child.ID;
                    childAndParentsList.AddRange(ids.ToList<decimal>());
                }
                organList.AddRange(childAndParentsList);
                return organList;
            }
        }

        IList<decimal> IDataAccess.GetAccessibleWorkGroups()
        {
            if (userRepository.HasAllWorkGroupAccess(this.CurrentUserId)>0)
            {
                IList<WorkGroup> list = new WorkGroupRepository(false).GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>();
            }
            else
            {
                return userRepository.GetUserWorkGroupIdList(this.CurrentUserId);
            }
        }

        IList<decimal> IDataAccess.GetAccessibleRuleGroups()
        {
            if (userRepository.HasAllRuleGroupAccess(this.CurrentUserId)>0)
            {
                IList<RuleCategory> list = new EntityRepository<RuleCategory>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>();
            }
            else
            {
                //return userRepository.GetUserRuleGroupIdList(this.CurrentUserId);
                BRuleCategory bRuleCat = new BRuleCategory();
                List<decimal> ruleCatList = new List<decimal>();
                List<decimal> childAndParentsList = new List<decimal>();
                ruleCatList.AddRange(userRepository.GetUserRuleGroupIdList(this.CurrentUserId));
                foreach (decimal ruleCatId in ruleCatList)//اضافه کردن بچه ها و والد ها 
                {
                    IList<RuleCategory> childs = new List<RuleCategory>();
                    //childs = bRuleCat.GetReportChildsByParentPath(ruleCatId);
                    childAndParentsList.Add(bRuleCat.GetByID(ruleCatId).Parent.ID);
                    var ids = from child in childs
                              select child.ID;
                    childAndParentsList.AddRange(ids.ToList<decimal>());
                }
                ruleCatList.AddRange(childAndParentsList);
                return ruleCatList;
            }
        }

        IList<decimal> IDataAccess.GetAccessibleShifts()
        {
            if (userRepository.HasAllShiftAccess(this.CurrentUserId)>0)
            {
                IList<Shift> list = new EntityRepository<Shift>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>();
            }
            else
            {
                return userRepository.GetUserShiftIdList(this.CurrentUserId);
            }
        }

        IList<decimal> IDataAccess.GetAccessiblePrecards()
        {
            if (userRepository.HasAllPrecardAccess(this.CurrentUserId)>0)
            {
                IList<Precard> list = new EntityRepository<Precard>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>();
            }
            else
            {
                return userRepository.GetUserPrecardIdList(this.CurrentUserId);
            }
        }

        IList<decimal> IDataAccess.GetAccessibleControlStations()
        {
            if (userRepository.HasAllControlStationAccess(this.CurrentUserId)>0)
            {
                IList<ControlStation> list = new EntityRepository<ControlStation>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>();
            }
            else
            {
                return userRepository.GetUserControlStationIDList(this.CurrentUserId);
            }
        }

        IList<decimal> IDataAccess.GetAccessibleDoctors()
        {
            if (userRepository.HasAllDoctorAccess(this.CurrentUserId)>0)
            {
                IList<Doctor> list = new EntityRepository<Doctor>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>();
            }
            else
            {
                return userRepository.GetUserDoctorIdList(this.CurrentUserId);
            }
        }

        IList<decimal> IDataAccess.GetAccessibleManagers()
        {
            if (userRepository.HasAllManagerAccess(this.CurrentUserId)>0)
            {
                IList<Manager> list = new ManagerRepository(false).GetAll().Where(x => x.Active).ToList();
                var ids = from mng in list
                          select mng.ID;
                return ids.ToList<decimal>();
            }
            else 
            {
                return userRepository.GetUserManagerIdList(this.CurrentUserId);
            }           
        }

        IList<decimal> IDataAccess.GetAccessibleFlows()
        {
            if (userRepository.HasAllFlowAccess(this.CurrentUserId)>0)
            {
                IList<Flow> list = new EntityRepository<Flow>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>();
            }
            else
            {
                return userRepository.GetUserFlowIdList(this.CurrentUserId);
            }
        }

        IList<decimal> IDataAccess.GetAccessibleReports()
        {
            if (userRepository.HasAllReportAccess(this.CurrentUserId)>0)
            {
                IList<Report> list = new EntityRepository<Report>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>();
            }
            else
            {
                BReport bReport = new BReport();
                List<decimal> reportList = new List<decimal>();
                List<decimal> childAndParentsList = new List<decimal>();
                reportList.AddRange(userRepository.GetUserReportIdList(this.CurrentUserId));
                foreach (decimal reportId in reportList)//اضافه کردن بچه ها و والد ها 
                {
                    IList<Report> childs = new List<Report>();
                    childs = bReport.GetReportChildsByParentPath(reportId);
                    childAndParentsList.AddRange(bReport.GetByID(reportId).ParentPathList);
                    var ids = from child in childs
                              select child.ID;
                    childAndParentsList.AddRange(ids.ToList<decimal>());
                }
                reportList.AddRange(childAndParentsList);
                return reportList;
            }
        }

        /// <summary>
        /// تمام پرسنل منتسب به بخشهای یک کاربر را بصورت سلسله مراتبی استخراج میکند
        /// </summary>
        /// <returns></returns>
        IList<decimal> IDataAccess.GetAccessiblePersonByDepartment()
        {
            BDepartment bDepartment=new BDepartment();
            IDataAccess port = new BUser();
            List<decimal> personList = new List<decimal>();
            IList<decimal> depList = port.GetAccessibleDeparments();
            foreach (decimal depId in depList) 
            {
                Department dep = bDepartment.GetByID(depId);
                var persons = from prs in dep.PersonList
                              select prs.ID;
                personList.AddRange(persons.ToList());              
            }
            return personList;
        }

        /// <summary>
        /// تمام پرسنل منتسب به بخشهای یک کاربر و ایستگاه کنترل را بصورت سلسله مراتبی استخراج میکند
        /// </summary>
        /// <returns></returns>
        IList<decimal> IDataAccess.GetAccessiblePersonByDepartmentAndControlSatation()
        {
            BDepartment bDepartment = new BDepartment();
            IDataAccess port = new BUser();
            List<decimal> personList = new List<decimal>();
            IList<decimal> depList = port.GetAccessibleDeparments();
            foreach (decimal depId in depList)
            {
                Department dep = bDepartment.GetByID(depId);
                var persons = from prs in dep.PersonList
                              select prs.ID;
                personList.AddRange(persons.ToList());
               
            }
            IList<decimal> ctlStationList = port.GetAccessibleControlStations();
            PersonRepository prsRep = new PersonRepository(false);
            personList.AddRange(prsRep.GetAllPersonByControlStaion(ctlStationList.ToArray()));

            return personList;     
        }

        #endregion

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUsersLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPasswordChangeLoadAccess()
        { 
        }

        public IList<decimal> GetAllUserIDList(decimal currentUserID, UserSearchKeys? searchKey, string searchTerm, bool singleResult)
        {
            IList<decimal> UserIDList = new List<decimal>();
            try
            {
                return userRepository.GetAllUserIDList(currentUserID, searchKey, searchTerm, singleResult);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "GetAllUserIDList");
                return UserIDList;
            }
        }

        public void CreateBasePersonUser(Person person)
        {
            try
            {
                if (!this.userRepository.CheckIsUserDefined(person))
                {
                    UserProxy userProxy = new UserProxy();
                    userProxy.Active = true;
                    userProxy.PersonID = person.ID;
                    userProxy.RoleID = (new BRole()).GetByCustomCode("3").ID;
                    userProxy.UserName = person.BarCode;
                    userProxy.Password = person.BarCode;
                    userProxy.ConfirmPassword = person.BarCode;
                    userProxy.IsPasswodChanged = true;
                    userProxy.ActiveDirectoryAuthenticate = false;
                    this.InsertUser(userProxy);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "CreateBasePersonUser");                
                throw ex;
            }
        }



    }
}