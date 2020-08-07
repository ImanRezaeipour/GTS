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
    public class BOrganizationUnit : BaseBusiness<OrganizationUnit>
    {
        private IDataAccess accessPort = new BUser();
        const string ExceptionSrc = "GTS.Clock.Business.Shifts.Business.OrganizationUnit";
        private OrganizationUnitRepository organizationUnitRepository
            = new OrganizationUnitRepository(false);

        /// <summary>
        /// ریشه درخت را برمیگرداند که با پیمایش بچههای آن درخت استخراج میشود
        /// حتما باید ریشه در دیتابیس موجود باشد
        /// </summary>
        /// <returns></returns>
        public OrganizationUnit GetOrganizationUnitTree()
        {
            try
            {
                IList<OrganizationUnit> organizationUnitList = organizationUnitRepository.GetOrganizationUnitTree();

                if (organizationUnitList.Count == 1)
                {
                    return organizationUnitList.First();
                }
                else
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.OrganizationUnitRootMoreThanOne, "تعداد ریشه چارت سازمانی در دیتابیس نامعتبر است", ExceptionSrc);
                }
            }
            catch (Exception ex) 
            {
                LogException(ex, "BOrganizationUnit", "GetOrganizationUnitTree");
                throw ex;
            }
        }

        /// <summary>
        /// برای بایند شدن درخت در هنگام باز کردن گره این تابع استفاده میشود
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns>JSON فرمت</returns>
        public IList<OrganizationUnit> GetChilds(decimal parentId)//, int pageSize, int pageIndex) 
        {
            try
            {
                IList<decimal> accessableIDs = accessPort.GetAccessibleOrgans();
                OrganizationUnit organizationUnit = new OrganizationUnit();
                IList<OrganizationUnit> list = organizationUnitRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new OrganizationUnit().Parent), new OrganizationUnit() { ID = parentId }),
                                                                                        new CriteriaStruct(Utility.GetPropertyName(() => new OrganizationUnit().ID), accessableIDs.ToArray(), CriteriaOperation.IN));
                return list;

            }
            catch (Exception ex)
            {
                LogException(ex, "BOrganizationUnit", "GetChilds");
                throw ex;
            }
        }

        /// <summary>
        /// بچه های یک گره را برمیگرداند
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public IList<OrganizationUnit> GetDepartmentChildsWithoutDA(decimal organId)
        {
            try
            {
                IList<OrganizationUnit> organList = new List<OrganizationUnit>();

                organList = organizationUnitRepository
                    .GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new OrganizationUnit().Parent), new OrganizationUnit() { ID = organId }));


                return organList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BOrganizationUnit", "GetDepartmentChildsWithoutDA");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organName"></param>
        /// <returns></returns>
        public IList<OrganizationUnit> SearchByUnitName(string organName)
        {
            try
            {
                IList<decimal> ids = accessPort.GetAccessibleOrgans();
                EntityRepository<OrganizationUnit> organRep = new EntityRepository<OrganizationUnit>(false);
                IList<OrganizationUnit> list = organRep
                    .GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new OrganizationUnit().Name), organName, CriteriaOperation.Like),
                                   new CriteriaStruct(Utility.GetPropertyName(() => new OrganizationUnit().Parent), null, CriteriaOperation.IsNotNull),
                                   new CriteriaStruct(Utility.GetPropertyName(() => new OrganizationUnit().ID), ids.ToArray(), CriteriaOperation.IN));
                               
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BOrganizationUnit", "SearchByUnitName");
                throw ex;
            }
        }

        /// <summary>
        /// بچه های یک گره را با استفاده از آدرس پدران آن برمیگرداند
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public IList<OrganizationUnit> GetOrganiztionChildsByParentPath(decimal parentId)
        {
            IList<OrganizationUnit> depList = organizationUnitRepository.GetByCriteria(
                new CriteriaStruct(
                    Utility.GetPropertyName(() => new Department().ParentPath)
                    , String.Format(",{0},", parentId)
                    , CriteriaOperation.Like));
            return depList;
        }

        /// <summary>
        /// نام خالی نباشد
        /// شناسه والد معتبر باشد
        /// اعتبار سنجی آیتمی که قرار است درج شود
        /// شناسه والد خالی نباشد
        /// نام  تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="dep"></param>
        /// <param name="exception"></param>
        protected override void InsertValidate(OrganizationUnit organizationUnit)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(organizationUnit.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitNameRequierd, "نام چارت باید مشخص شود", ExceptionSrc));
            }

            if (Utility.IsEmpty(organizationUnit.ParentID))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitParentIDRequierd, "نام والد چارت باید مشخص شود", ExceptionSrc));
            }

            else if (organizationUnitRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.ID), organizationUnit.ParentID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitParentNotExists, "چارتی با این شناسه موجود نمیباشد", ExceptionSrc));
            }

            else if (organizationUnitRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.Name), organizationUnit.Name),
                                                                        new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.Parent), organizationUnit.Parent)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitNameRepeated, "نام در یک سطح چارت نباید تکراری باشد", ExceptionSrc));
            }

            if (!Utility.IsEmpty(organizationUnit.CustomCode))
            {
                if (organizationUnitRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.CustomCode), organizationUnit.CustomCode)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitCustomCodeRepeated, "درج - کد تعریف شده در چارت نباید تکراری باشد", ExceptionSrc));
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
        protected override void UpdateValidate(OrganizationUnit organizationUnit)
        {
            // والد یک گره بروزرسانی نمیشود .همچنینبنا به محدودیتهای کلاینت هنگام بروزرسانی والد مقداردهی نمیشود
            organizationUnit.ParentID = organizationUnitRepository.GetParentID(organizationUnit.ID);

            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(organizationUnit.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitNameRequierd, "نام چارت باید مشخص شود", ExceptionSrc));
            }
          
            else if (organizationUnitRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.Name), organizationUnit.Name),
                                                                        new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.Parent), organizationUnit.Parent),
                                                                        new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.ID), organizationUnit.ID, CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitNameRepeated, "نام چارت در یک سطح نباید تکراری باشد", ExceptionSrc));
            }

            if (organizationUnit.ParentID != 0 &&
                organizationUnitRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.Parent), organizationUnit.Parent)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitParentNotExists, "چارت والدی با این شناسه موجود نمیباشد", ExceptionSrc));
            }
            

            if (!Utility.IsEmpty(organizationUnit.CustomCode))
            {
                if (organizationUnitRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.CustomCode), organizationUnit.CustomCode),
                                                                       new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.ID), organizationUnit.ID,CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitCustomCodeRepeated, "درج - کد تعریف شده در چارت نباید تکراری باشد", ExceptionSrc));
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
        protected override void DeleteValidate(OrganizationUnit organizationUnit)
        {
            if (organizationUnitRepository.IsRoot(organizationUnit.ID))
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                exception.Add(ExceptionResourceKeys.OrganizationUnitRootDeleteIllegal, "ریشه چارت سازمانی نباید حذف شود", ExceptionSrc);
                throw exception;
            }
            if (organizationUnitRepository.HasPerson(organizationUnit.ID))
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                exception.Add(ExceptionResourceKeys.OrganizationUnitUsedByPerson, "این چارت به اشخاص انتساب داده شده است", ExceptionSrc);
                throw exception;
            }
        }

        //protected override void GetReadyBeforeSave(OrganizationUnit organ, UIActionType action)
        //{
        //    if (action == UIActionType.ADD && organ.ParentID > 0)
        //    {
        //        OrganizationUnit parent = base.GetByID(organ.ParentID);
        //        organ.ParentPath = parent.ParentPath + String.Format(",{0},", organ.ParentID);
        //    }
        //    else if (action == UIActionType.EDIT)
        //    {
        //        OrganizationUnit node = base.GetByID(organ.ID);
        //        organ.ParentPath = node.ParentPath;
        //        NHibernateSessionManager.Instance.ClearSession();
        //    }
        //}

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckOrganizationPostsLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertOrganizationPost(OrganizationUnit organizationPost, UIActionType UAT)
        {
            return base.SaveChanges(organizationPost, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateOrganizationPost(OrganizationUnit organizationPost, UIActionType UAT)
        {
            return base.SaveChanges(organizationPost, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteOrganizationPost(OrganizationUnit organizationPost, UIActionType UAT)
        {
            return base.SaveChanges(organizationPost, UAT);
        }

    }
}

