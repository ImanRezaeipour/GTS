using System;
using System.Collections.Generic;
using System.Collections;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.SqlTypes;
using NHibernate.Type;
using NHibernate.Transform;
using GTS.Clock.Model;
using System.Linq;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.BaseInformation;
using System.IO;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Model.Security;

namespace GTS.Clock.Infrastructure.Repository
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public override string TableName
        {
            get { return "TA_Person"; }
        }
        public PersonRepository()
            : base()
        { }

        public PersonRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }


        #region IPersonRepository Members

        public Person GetByBarcode(string Barcode)
        {
            return base.NHibernateSession.CreateCriteria(typeof(Person))
                .Add(Expression.Eq("IsDeleted", false))                         
                .Add(Expression.Eq("BarCode", Barcode))
                                         .UniqueResult<Person>();
        }

        public void EnableEfectiveDateFilter(decimal PersonId, DateTime FromDate, DateTime ToDate, DateTime beginYear, DateTime endYear, DateTime safeFromDate, DateTime safeToDate)
        {
            string strFromDate = FromDate.ToString("yyy/MM/dd");
            string strToDate = ToDate.ToString("yyyy/MM/dd");
            string strEndYear = endYear.ToString("yyy/MM/dd");
            string strBeginYear = beginYear.ToString("yyyy/MM/dd");
            string strSafeFromDate = safeFromDate.ToString("yyy/MM/dd");
            string strSafeToDate = safeToDate.ToString("yyyy/MM/dd");
            NhibernateFilters filters = base.NhibernateFilters;
            NhibernateFilter filter = new NhibernateFilter();
            filter.FilterName = "effectiveDate";
            filter.Add("fromDate", strFromDate);
            filter.Add("toDate", strToDate);
            filter.Add("personId", PersonId);
            filter.Add("endYear", strEndYear);
            filter.Add("beginYear", strBeginYear);
            filter.Add("safeFromDate", strSafeFromDate);
            filter.Add("safeToDate", strSafeToDate);
            filters.Clear();
            filters.Add(filter);
            base.NHibernateSession.EnableFilter("effectiveDate")
                        .SetParameter("fromDate", strFromDate)
                        .SetParameter("toDate", strToDate)
                        .SetParameter("personId", PersonId)
                        .SetParameter("endYear", strEndYear)
                        .SetParameter("beginYear", strBeginYear)
                        .SetParameter("safeFromDate", strSafeFromDate)
                        .SetParameter("safeToDate", strSafeToDate);
        }

        public void EnableEfectiveDateFilter(decimal PersonId, DateTime FromDate, DateTime ToDate)
        {
            string strFromDate = FromDate.ToString("yyy/MM/dd");
            string strToDate = ToDate.ToString("yyyy/MM/dd");
            NhibernateFilters filters = base.NhibernateFilters;
            NhibernateFilter filter = new NhibernateFilter();
            filter.FilterName = "effectiveDate";
            filter.Add("fromDate", strFromDate);
            filter.Add("toDate", strToDate);
            filter.Add("personId", PersonId);
            filter.Add("endYear", "");
            filter.Add("beginYear", "");
            filter.Add("safeFromDate", "");
            filter.Add("safeToDate", "");
            filters.Clear();
            filters.Add(filter);
            base.NHibernateSession.EnableFilter("effectiveDate")
                        .SetParameter("fromDate", strFromDate)
                        .SetParameter("toDate", strToDate)
                        .SetParameter("personId", PersonId)
                        .SetParameter("endYear", "")
                        .SetParameter("beginYear", "")
                        .SetParameter("safeFromDate", "")
                        .SetParameter("safeToDate", "");
        }      

        public void DisableEfectiveDateFilter()
        {
            base.NhibernateFilters.Clear();
            base.NHibernateSession.DisableFilter("effectiveDate");
        }

        public void DeleteProceedTraffic(ProceedTraffic proceedTraffic)
        {
            string SQLCommand = String.Format("DELETE " +
                                              "FROM TA_ProceedTraffic " +
                                              "WHERE ProceedTraffic_ID= {0}", proceedTraffic.ID);
            base.RunSQL(SQLCommand);
        }

        /// <summary>
        /// نتایج مفاهیم در تاریخ مشخص شده برای پرسنل ارسالی را پاک می نماید
        /// </summary>
        /// <param name="PersonID"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        public void DeleteScndCnpValue(Decimal PersonID, DateTime FromDate, DateTime ToDate)
        {
           /* string SQLCommand = String.Format(@" DELETE FROM TA_SecondaryConceptValue  
                                                 WHERE ScndCnpValue_PersonId = {0} 
                                                 AND 
                                                 (
	                                                 (ScndCnpValue_FromDate BETWEEN '{1}' AND '{2}')   
	                                                 OR 
	                                                 (ScndCnpValue_ToDate BETWEEN '{1}' AND '{2}')   
	                                                 OR 
	                                                 ('{0}' >= ScndCnpValue_FromDate AND ScndCnpValue_ToDate >= '{2}')
                                                 )", PersonID, FromDate.ToShortDateString(), ToDate.ToShortDateString());
            */
            string HQLCommand = String.Format(" DELETE FROM " +
                                                "BaseScndCnpValue bsScndCnpVal " +
                                                "WHERE bsScndCnpVal.Person.id = {0} " +
                                                "AND " +
                                                "((bsScndCnpVal.FromDate BETWEEN '{1}' AND '{2}') " +
                                                "  OR " +
                                                "(bsScndCnpVal.ToDate BETWEEN '{1}' AND '{2}') " +
                                                "  OR " +
                                                "('{1}' >= bsScndCnpVal.FromDate AND bsScndCnpVal.ToDate >= '{2}'))", PersonID, FromDate.ToString("yyyy-MM-dd"), ToDate.ToString("yyyy-MM-dd"));

            // base.RunSQL(SQLCommand);
            //base.NHibernateSession.CreateSQLQuery(SQLCommand).ExecuteUpdate();
            base.NHibernateSession.CreateQuery(HQLCommand).ExecuteUpdate();
        }

        /// <summary>
        /// در اینجا یک "پروسیجر ذخیره شده ی" "اسکیوال" فراخوانی می شود که
        /// جدول "خروجی" پرسنل مشخص شده را در تاریخ ارسالی پر می نماید
        /// </summary>
        /// <param name="Barcode"></param>
        /// <param name="date"></param>
        public void UpdatePTable(string Barcode, PersianDateTime date)
        {
            IQuery query = this.NHibernateSession
                                .GetNamedQuery("UpdatePTableQuery")
                                .SetString("barCode", Barcode)
                                .SetString("year", date.Year.ToString())
                                .SetString("month", date.Month.ToString())
                                .SetString("day", date.Day.ToString())
                                .SetString("GregorianDate", date.GregorianDate.ToString("yyyy/MM/dd"));
            query.List();
        }

        /// <summary>
        /// شی ارسال شده را دوباره بارگذاری می نماید
        /// </summary>
        /// <param name="Entity"></param>
        public override void Refresh(Person Entity)
        {
            if (Entity != null)
            {
                //Entity.shiftList.Clear();
                base.Refresh(Entity);
            }
        }   

        /// <summary>
        /// با ارسال "جستجوی" مستقیم برروی پایگاه داده مقدار مفهوم مورد نظر را براساس شناسه ی ارسالی برمی گرداند
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public BaseScndCnpValue GetScndCnpValueByIndex(string Index)
        {
            //بدلیل تکراری برگرداندن کامنت شد تا بعدا تست دقیق شود - 5 تیر 1393
            //return base.NHibernateSession.QueryOver<BaseScndCnpValue>()
            //                             .Where(x => x.Index == Index)
            //                             .SingleOrDefault();

            IList<BaseScndCnpValue> list = base.NHibernateSession.QueryOver<BaseScndCnpValue>()
                                        .Where(x => x.Index == Index)
                                        .List();
            return list.FirstOrDefault();
        }

        /// <summary>
        /// اگر بارکد تهی باشد نباید در لیست جواب بیاید
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conOp"></param>
        /// <param name="cretriaParam"></param>
        /// <returns></returns>
        public override IList<Person> GetByCriteriaByPage(int pageIndex, int pageSize, ConditionOperations conOp, params CriteriaStruct[] cretriaParam)
        {
            ICriteria crit = this.NHibernateSession.CreateCriteria(typeof(Person));
            Junction disjunction = Restrictions.Disjunction();
            crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().IsDeleted), false));
            for (int i = 0; i < cretriaParam.Length; i++)
            {
                CriteriaStruct c = cretriaParam[i];

                switch (c.Operation)
                {
                    case CriteriaOperation.Equal:
                        if (conOp == ConditionOperations.AND)
                            crit.Add(Restrictions.Eq(c.PropertyName, c.Value));
                        else if (conOp == ConditionOperations.OR)
                            disjunction.Add(Restrictions.Eq(c.PropertyName, c.Value));
                        break;
                    case CriteriaOperation.NotEqual:
                        if (conOp == ConditionOperations.AND)
                            crit.Add(Restrictions.Not(Restrictions.Eq(c.PropertyName, c.Value)));
                        else if (conOp == ConditionOperations.OR)
                            disjunction.Add(Restrictions.Not(Restrictions.Eq(c.PropertyName, c.Value)));
                        break;
                    case CriteriaOperation.GreaterThan:
                        if (conOp == ConditionOperations.AND)
                            crit.Add(Restrictions.Gt(c.PropertyName, c.Value));
                        else if (conOp == ConditionOperations.OR)
                            disjunction.Add(Restrictions.Gt(c.PropertyName, c.Value));
                        break;
                    case CriteriaOperation.LessThan:
                        if (conOp == ConditionOperations.AND)
                            crit.Add(Restrictions.Lt(c.PropertyName, c.Value));
                        else if (conOp == ConditionOperations.OR)
                            disjunction.Add(Restrictions.Lt(c.PropertyName, c.Value));
                        break;
                    case CriteriaOperation.GreaterEqThan:
                        if (conOp == ConditionOperations.AND)
                            crit.Add(Restrictions.Ge(c.PropertyName, c.Value));
                        else if (conOp == ConditionOperations.OR)
                            disjunction.Add(Restrictions.Ge(c.PropertyName, c.Value));
                        break;
                    case CriteriaOperation.LessEqThan:
                        if (conOp == ConditionOperations.AND)
                            crit.Add(Restrictions.Le(c.PropertyName, c.Value));
                        else if (conOp == ConditionOperations.OR)
                            disjunction.Add(Restrictions.Le(c.PropertyName, c.Value));
                        break;
                    case CriteriaOperation.Like:
                        if (conOp == ConditionOperations.AND)
                        {
                            if (c.Value is string)
                            {
                                crit.Add(Restrictions.Like(c.PropertyName, c.Value.ToString(), MatchMode.Anywhere));
                            }
                            else
                            {
                                crit.Add(Restrictions.Like(c.PropertyName, c.Value));
                            }
                        }
                        else if (conOp == ConditionOperations.OR)
                        {
                            if (c.Value is string)
                            {
                                disjunction.Add(Restrictions.Like(c.PropertyName, c.Value.ToString(), MatchMode.Anywhere));
                            }
                            else
                            {
                                disjunction.Add(Restrictions.Like(c.PropertyName, c.Value));
                            }
                        }
                        break;
                }
            }
            if (cretriaParam.Length > 0)
            {
                crit.Add(disjunction);
            }
            crit.Add(Restrictions.IsNotNull(Utility.Utility.GetPropertyName(() => new Person().BarCode)));
            crit.Add(Restrictions.Not(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().BarCode), "00000000")));
            IList<Person> list = crit.SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize).List<Person>();
            return list;
        }

        /// <summary>
        /// اگر بارکد تهی باشد نباید در لیست جواب بیاید
        /// </summary>
        /// <param name="conOp"></param>
        /// <param name="cretriaParam"></param>
        /// <returns></returns>
        public int GetCountByCriteriaNotNull(ConditionOperations conOp, params CriteriaStruct[] cretriaParam)
        {
            ICriteria crit = this.NHibernateSession.CreateCriteria(typeof(Person));
            Junction disjunction = Restrictions.Disjunction();
            crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().IsDeleted), false));
            for (int i = 0; i < cretriaParam.Length; i++)
            {
                CriteriaStruct c = cretriaParam[i];

                switch (c.Operation)
                {
                    case CriteriaOperation.Equal:
                        if (conOp == ConditionOperations.AND)
                            crit.Add(Restrictions.Eq(c.PropertyName, c.Value));
                        else if (conOp == ConditionOperations.OR)
                            disjunction.Add(Restrictions.Eq(c.PropertyName, c.Value));
                        break;
                    case CriteriaOperation.NotEqual:
                        if (conOp == ConditionOperations.AND)
                            crit.Add(Restrictions.Not(Restrictions.Eq(c.PropertyName, c.Value)));
                        else if (conOp == ConditionOperations.OR)
                            disjunction.Add(Restrictions.Not(Restrictions.Eq(c.PropertyName, c.Value)));
                        break;
                    case CriteriaOperation.GreaterThan:
                        if (conOp == ConditionOperations.AND)
                            crit.Add(Restrictions.Gt(c.PropertyName, c.Value));
                        else if (conOp == ConditionOperations.OR)
                            disjunction.Add(Restrictions.Gt(c.PropertyName, c.Value));
                        break;
                    case CriteriaOperation.LessThan:
                        if (conOp == ConditionOperations.AND)
                            crit.Add(Restrictions.Lt(c.PropertyName, c.Value));
                        else if (conOp == ConditionOperations.OR)
                            disjunction.Add(Restrictions.Lt(c.PropertyName, c.Value));
                        break;
                    case CriteriaOperation.GreaterEqThan:
                        if (conOp == ConditionOperations.AND)
                            crit.Add(Restrictions.Ge(c.PropertyName, c.Value));
                        else if (conOp == ConditionOperations.OR)
                            disjunction.Add(Restrictions.Ge(c.PropertyName, c.Value));
                        break;
                    case CriteriaOperation.LessEqThan:
                        if (conOp == ConditionOperations.AND)
                            crit.Add(Restrictions.Le(c.PropertyName, c.Value));
                        else if (conOp == ConditionOperations.OR)
                            disjunction.Add(Restrictions.Le(c.PropertyName, c.Value));
                        break;
                    case CriteriaOperation.Like:
                        if (conOp == ConditionOperations.AND)
                        {
                            if (c.Value is string)
                            {
                                crit.Add(Restrictions.Like(c.PropertyName, c.Value.ToString(), MatchMode.Anywhere));
                            }
                            else
                            {
                                crit.Add(Restrictions.Like(c.PropertyName, c.Value));
                            }
                        }
                        else if (conOp == ConditionOperations.OR)
                        {
                            if (c.Value is string)
                            {
                                disjunction.Add(Restrictions.Like(c.PropertyName, c.Value.ToString(), MatchMode.Anywhere));
                            }
                            else
                            {
                                disjunction.Add(Restrictions.Like(c.PropertyName, c.Value));
                            }
                        }
                        break;
                }
            }
            if (cretriaParam.Length > 0)
            {
                crit.Add(disjunction);
            }
            crit.Add(Restrictions.IsNotNull(Utility.Utility.GetPropertyName(() => new Person().BarCode)));
            crit.Add(Restrictions.Not(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().BarCode), "00000000")));
            int count = (int)crit.SetProjection(Projections.Count("ID")).UniqueResult();
            return count;
        }

        public int GetPersonCount(decimal userId) 
        {
            DetachedCriteria criteria = DetachedCriteria.For(this.persistanceType);
            criteria.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().IsDeleted), false));
            criteria.Add(Restrictions.IsNotNull(Utility.Utility.GetPropertyName(() => new Person().BarCode)));
            criteria.Add(Restrictions.Not(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().BarCode), "00000000")));
            criteria.Add(Expression.Sql("prs_Id in (select * from fn_GetAccessiblePersons(0,?,?))", new object[] { userId, (int)PersonCategory.Public }, new IType[] { NHibernateUtil.Decimal, NHibernateUtil.Int32 }));
            int count = 0;
            try
            {
                count = (int)criteria.GetExecutableCriteria(NHibernateSession)
                        .SetProjection(Projections.Count("ID")).UniqueResult();
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
            return count;
        }

        /// <summary>
        /// صفحه بندی روی اشخاصی که شماره پرسنلی آنها تهی نباشد
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<Person> GetAllByPage(decimal userId, int pageIndex, int pageSize)
        {
            DetachedCriteria criteria = DetachedCriteria.For(this.persistanceType);
            criteria.Add(Restrictions.IsNotNull(Utility.Utility.GetPropertyName(() => new Person().BarCode)));
            criteria.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().IsDeleted), false));
            criteria.Add(Restrictions.Not(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().BarCode), "00000000")));
            criteria.Add(Expression.Sql("prs_Id in (select * from fn_GetAccessiblePersons(0,?,?))", new object[] { userId, (int)PersonCategory.Public }, new IType[] { NHibernateUtil.Decimal, NHibernateUtil.Int32 }));
            IList<Person> entities = null;
            try
            {
                entities = criteria.GetExecutableCriteria(NHibernateSession)

                    .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                    .List<Person>() as IList<Person>;
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
            return entities;
        }

        #region Image Region
        ///بدلیل عدم استفاده از مدل در ذخیره و بازیابی عکس پرسنل این عملیات در اینجا بصورت مستقیم انجام می شود

        /// <summary>
        /// بروزرسانی عکس پرسنل
        /// </summary>
        /// <param name="personDtlId"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public decimal UpdatePersonImage(decimal personDtlId, string image)
        {
            try
            {
                base.Transact<int>(() => NHibernateSession.CreateSQLQuery(String.Format("UPDATE {0} " +
                                                                                        "SET PrsDtl_Image = :image " +
                                                                                        "WHERE PrsDtl_ID = {1}", "TA_PersonDetail", personDtlId)).SetParameter("image", image).ExecuteUpdate());
                return personDtlId;
            }
            catch (Exception ex)
            {
                throw new UpdatPersonImageException(UIFatalExceptionIdentifiers.UpdatePersonImageHasError, ex.Message, "");
            }
        }

        /// <summary>
        /// بازیابی عکس پرسنل
        /// </summary>
        /// <param name="personDtlId"></param>
        /// <returns></returns>
        public string GetPersonImage(decimal personDtlId)
        {
            return base.Transact<string>(() => NHibernateSession.QueryOver<PersonDetail>()
                                                    .Select(x => x.Image)
                                                    .Where(x => x.ID == personDtlId)
                                                    .SingleOrDefault<string>());
        }

        #endregion        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personId">شناسه شخص</param>
        /// <param name="date">تاریخ جهت بررسی تاریخ انتساب شخص</param>
        /// <param name="order">ترتیب ماه</param>
        /// <returns></returns>
        public PersonalMonthlyReport GetPersonalMonthlyReport(decimal personId, DateTime date, int order)
        {
            return NHibernateSession.GetNamedQuery("GetPersonalMonthlyReport")
                                    .SetParameter("PersonId", personId)
                                    .SetParameter("Date", date)
                                    .SetParameter("Order", order)
                                    .UniqueResult<PersonalMonthlyReport>();

        }

        public Person AttachPerson(decimal PrsId)
        {
            try
            {
                Person p = new Person() { ID = PrsId };
                NHibernateSession.Refresh(p, LockMode.Read);
                NHibernateSession.SetReadOnly(p, true);
                return p;
            }
            catch(Exception ex)
            {
                throw new NHibernateException("پرسنلی با این شناسه قبلا در جلسه وجود داشته است", ExceptionType.FATAL, "PersonRepository.AttachPerson", ex);
            }
        }

        public IList<decimal> GetAllActivePersonIds() 
        {
            string HQLCommnad = @" select prs.ID from Person as prs
                                  Where prs.IsDeleted=false AND prs.Active=true";

            IList<decimal> list = base.NHibernateSession.CreateQuery(HQLCommnad)               
               .List<decimal>();
            return list;

        }
        #endregion

        public bool ExistsPerson(decimal personId)
        {
            string SQLQuery = " SELECT Count(*) FROM TA_Person WHERE prs_IsDeleted=0 AND prs_ID=:personId ";

            int count = base.NHibernateSession.CreateSQLQuery(SQLQuery)
                                            .SetParameter("personId", personId)
                                            .List<int>().First();
            return count > 0;
        }

        /// <summary>
        /// پرسنل را براساس ایستگاه کنترلی برمیگرداند
        /// </summary>
        /// <param name="controlStationIds"></param>
        /// <returns></returns>
        public IList<decimal> GetAllPersonByControlStaion(decimal[] controlStationIds) 
        {
            if (controlStationIds == null || controlStationIds.Length == 0)
                return new List<decimal>();
            string SQLQuery = @" SELECT prs_ID FROM TA_Person person
                                 Inner Join TA_PersonTASpec personSpec
                                 on person.Prs_ID = personSpec.prsTA_ID
                                 WHERE person.Prs_IsDeleted=0 AND personSpec.prsTA_ControlStationId in (:ctrlSationIds)";

            IList<decimal> prsIds = base.NHibernateSession.CreateSQLQuery(SQLQuery)
                .SetParameterList("ctrlSationIds", base.CheckListParameter(controlStationIds))
                                           .List<decimal>();
            return prsIds;
        }

        public IList<Department> GetAllPersonnelDepartmentParents(decimal departmentID)
        {
            IList<Department> DepartmentsList = new List<Department>();

            Department childDepartment = NHibernateSession.QueryOver<Department>()
                            .Where(department => department.ID == departmentID)
                            .List<Department>()
                            .FirstOrDefault();

            if (childDepartment != null)
                DepartmentsList.Add(childDepartment);
            else
                return DepartmentsList;

            this.GetAllPersonnelDepartmentParents(ref DepartmentsList, childDepartment);
            return DepartmentsList.Reverse().ToList<Department>();
        }

        private void GetAllPersonnelDepartmentParents(ref IList<Department> DepartmentsList, Department childDepartment)
        {
            Department parentDepartment = NHibernateSession.QueryOver<Department>()
                                          .Where(department => department.ID == childDepartment.ParentID)
                                          .List<Department>()
                                          .FirstOrDefault();
            if (parentDepartment != null)
            {
                DepartmentsList.Add(parentDepartment);
                this.GetAllPersonnelDepartmentParents(ref DepartmentsList, parentDepartment);
            }
        }

        /// <summary>
        /// یک شخص را بطور منطقی از دیتابیس حذف مینماید
        /// </summary>
        /// <param name="personID"></param>
        public void DeletePerson(decimal personID)
        {
            string SQLCommand = @"update TA_Person
                                set prs_IsDeleted=1
                                where prs_ID =:personId";
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("personId", personID)
                .ExecuteUpdate();

            UserRepository userRep = new UserRepository();
            userRep.DeleteDeletedPersonUsers(personID);
        }

        #region Search
     
        public IList<Person> Search(PersonSearchProxy proxy, decimal userId,decimal managerId, PersonCategory searchCat, int pageIndex, int pageSize)
        {
            const string PersonDetailAlias = "prsDtl";
            const string WorkGroupAlias = "wg";
            const string RuleGroupAlias = "rg";
            const string CalculationDateRangeGroupAlias = "cdrg";
            const string DepartmentAlias = "dep";
            const string OrganizationUnitAlias = "organ";
            const string PersonTASpecAlias = "prsTs";
            const string GradeAlias = "grade";


            ICriteria crit = base.NHibernateSession.CreateCriteria(typeof(Person));
            Junction disjunction = Restrictions.Disjunction();
            crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().IsDeleted), false));
            crit.Add(Restrictions.Not(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().BarCode), "00000000")));
            //crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonDetail), PersonDetailAlias);
            crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonDetailList), PersonDetailAlias);
     

            //فعال
            if (proxy.PersonActivateState != null)
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Active), (bool)proxy.PersonActivateState));
            }

            //کد پرسنلی
            if (!Utility.Utility.IsEmpty(proxy.PersonCode))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().BarCode), proxy.PersonCode, MatchMode.Anywhere));
            }

            //کد ملی
            if (!Utility.Utility.IsEmpty(proxy.MelliCode))
            {
                crit.Add(Restrictions.Like(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().MeliCode), proxy.MelliCode, MatchMode.Anywhere));
            }

            //نام
            if (!Utility.Utility.IsEmpty(proxy.FirstName))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().FirstName), proxy.FirstName, MatchMode.Anywhere));
            }

            //نام خانوادگی
            if (!Utility.Utility.IsEmpty(proxy.LastName))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().LastName), proxy.LastName, MatchMode.Anywhere));
            }

            //نام پدر
            if (!Utility.Utility.IsEmpty(proxy.FatherName))
            {
                crit.Add(Restrictions.Like(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().FatherName), proxy.FatherName, MatchMode.Anywhere));
            }

            //جنسیت ,پیش فرض آن از واسط کاربر -1 است
            if (!Utility.Utility.IsEmpty(proxy.Sex) && proxy.Sex >= 0)
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Sex), proxy.Sex));
            }

            //شماره کارت
            if (!Utility.Utility.IsEmpty(proxy.CartNumber))
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().CardNum), proxy.CartNumber));
            }

            //نظام وضیفه , پیش فرض آن از واسط کاربر 0 است
            if (!Utility.Utility.IsEmpty(proxy.Military) && proxy.Military > 0)
            {
                crit.Add(Restrictions.Eq(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().MilitaryStatus), proxy.Military));
            }

            //تحصیلات
            if (!Utility.Utility.IsEmpty(proxy.Education))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().Education), proxy.Education, MatchMode.Anywhere));
            }

            //تاهل , پیش فرض آن از واسط کاربر 0 است
            if (!Utility.Utility.IsEmpty(proxy.MaritalStatus) && proxy.MaritalStatus > 0)
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().MaritalStatus), proxy.MaritalStatus));
            }

            //شروع تاریخ تولد
            if (!Utility.Utility.IsEmpty(proxy.FromBirthDate))
            {
                crit.Add(Restrictions.Ge(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().BirthDate), proxy.FromBirthDate));
            }

            //پایان تاریخ تولد
            if (!Utility.Utility.IsEmpty(proxy.ToBirthDate))
            {
                crit.Add(Restrictions.Le(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().BirthDate), proxy.ToBirthDate));
            }

            //شروع تاریخ استخدام
            if (!Utility.Utility.IsEmpty(proxy.FromEmploymentDate))
            {
                crit.Add(Restrictions.Ge(Utility.Utility.GetPropertyName(() => new Person().EmploymentDate), proxy.FromEmploymentDate));
            }

            //پایان تاریخ استخدام
            if (!Utility.Utility.IsEmpty(proxy.ToEmploymentDate))
            {
                crit.Add(Restrictions.Ge(Utility.Utility.GetPropertyName(() => new Person().EndEmploymentDate), proxy.ToEmploymentDate));
            }

            //بخش
            //if (!Utility.Utility.IsEmpty(proxy.DepartmentId))
            //{
            //    crit.CreateAlias("department", DepartmentAlias);

            //    if (proxy.IncludeSubDepartments)
            //    {
            //        disjunction.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Department).ToLower(), new Department() { ID = (decimal)proxy.DepartmentId }));
            //        disjunction.Add(Restrictions.Like(DepartmentAlias + "." + Utility.Utility.GetPropertyName(() => new Department().ParentPath), "," + proxy.DepartmentId.ToString() + ",", MatchMode.Anywhere));

            //    }
            //    else
            //    {
            //        //crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Department).ToLower(), new Department() { ID = (decimal)proxy.DepartmentId }));
            
            //    }

            //}
            if (!Utility.Utility.IsEmpty(proxy.DepartmentListId))
            {
                crit.CreateAlias("department", DepartmentAlias);

                if (proxy.IncludeSubDepartments)
                {
                    disjunction.Add(Restrictions.In(DepartmentAlias + "." + Utility.Utility.GetPropertyName(() => new Department().ID), proxy.DepartmentListId.ToArray()));
                    
                    foreach (decimal item in proxy.DepartmentListId)
                    {
                        disjunction.Add(Restrictions.Like(DepartmentAlias + "." + Utility.Utility.GetPropertyName(() => new Department().ParentPath), "," + item.ToString() + ",", MatchMode.Anywhere));
                    }
                    

                }
                else
                {

                    crit.Add(Restrictions.In(DepartmentAlias + "."  + Utility.Utility.GetPropertyName(() => new Department().ID), proxy.DepartmentListId.ToArray()));
                }

            }
            //پست سازمانی
            if (!Utility.Utility.IsEmpty(proxy.OrganizationUnitId))
            {
                crit.CreateAlias("OrganizationUnitList", OrganizationUnitAlias);
                crit.Add(Restrictions.Eq(OrganizationUnitAlias + "." + Utility.Utility.GetPropertyName(() => new OrganizationUnit().ID), (decimal)proxy.OrganizationUnitId));
            }
            //رتبه
            if (!Utility.Utility.IsEmpty(proxy.GradeId))
            {
                crit.CreateAlias("grade", GradeAlias);
                crit.Add(Restrictions.Eq(GradeAlias + "." + Utility.Utility.GetPropertyName(() => new Grade().ID), (decimal)proxy.GradeId));
            }

            //گروه کاری
            if (!Utility.Utility.IsEmpty(proxy.WorkGroupId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonWorkGroupList), WorkGroupAlias);
                crit.Add(Restrictions.Eq(WorkGroupAlias + "." + Utility.Utility.GetPropertyName(() => new AssignWorkGroup().WorkGroup), new WorkGroup() { ID = (decimal)proxy.WorkGroupId }));

                if (!Utility.Utility.IsEmpty(proxy.WorkGroupFromDate))
                {
                    crit.Add(Restrictions.Le(WorkGroupAlias + "." + Utility.Utility.GetPropertyName(() => new AssignWorkGroup().FromDate), proxy.WorkGroupFromDate));
                }
            }

            //گروه قوانین
            if (!Utility.Utility.IsEmpty(proxy.RuleGroupId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonRuleCatAssignList), RuleGroupAlias);
                crit.Add(Restrictions.Eq(RuleGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRuleCatAssignment().RuleCategory), new RuleCategory() { ID = (decimal)proxy.RuleGroupId }));

                if (!Utility.Utility.IsEmpty(proxy.RuleGroupFromDate))
                {
                    crit.Add(Restrictions.Le(RuleGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRuleCatAssignment().FromDate), proxy.RuleGroupFromDate));
                }
                if (!Utility.Utility.IsEmpty(proxy.RuleGroupToDate))
                {
                    crit.Add(Restrictions.Ge(RuleGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRuleCatAssignment().ToDate), proxy.RuleGroupToDate));
                }
            }

            //محدوده محاسبات
            if (!Utility.Utility.IsEmpty(proxy.CalculationDateRangeId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonRangeAssignList), CalculationDateRangeGroupAlias);
                crit.Add(Restrictions.Eq(CalculationDateRangeGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRangeAssignment().CalcDateRangeGroup), new CalculationRangeGroup() { ID = (decimal)proxy.CalculationDateRangeId }));

                if (!Utility.Utility.IsEmpty(proxy.CalculationFromDate))
                {
                    crit.Add(Restrictions.Le(CalculationDateRangeGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRangeAssignment().FromDate), proxy.CalculationFromDate));
                }
            }

            //ایستگاه کنترل
            if (!Utility.Utility.IsEmpty(proxy.ControlStationId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonTASpecList), PersonTASpecAlias);
                crit.Add(Restrictions.Eq(PersonTASpecAlias + "." + Utility.Utility.GetPropertyName(() => new PersonTASpec().ControlStation), new ControlStation() { ID = (decimal)proxy.ControlStationId }));
                //crit.Add(Restrictions.Eq("controlStation", new ControlStation() { ID = (decimal)proxy.ControlStationId }));
            }

            //نوع استخدام
            if (!Utility.Utility.IsEmpty(proxy.EmploymentType))
            {
                crit.Add(Restrictions.Eq("employmentType", new EmploymentType() { ID = (decimal)proxy.EmploymentType }));
            }

            //اشخاص مشخص شده
            if (!Utility.Utility.IsEmpty(proxy.PersonIdList))
            {
                crit.Add(Restrictions.In(Utility.Utility.GetPropertyName(() => new Person().ID), CheckListParameter(proxy.PersonIdList)));
            }

            //جستجو در بین مدیران و اپراتورها
            if (proxy.SearchInCategory != PersonCategory.Public
                && !Utility.Utility.IsEmpty(proxy.SearchInCategory))
            {
                if (proxy.SearchInCategory == PersonCategory.Manager)
                {
                    IList<Person> personList = new ManagerRepository(false).GetAllManager();
                    var ids = from person in personList
                              select person.ID;
                    IList<decimal> idList = ids.ToList<decimal>();

                    crit.Add(Restrictions.In(Utility.Utility.GetPropertyName(() => new Person().ID), idList.ToArray()));
                }                
            }
            IList<Person> list = new List<Person>();
           
            crit.Add(Expression.Sql(" prs_Id in (select * from fn_GetAccessiblePersons(?,?,?))", new object[] { managerId, userId, (int)searchCat }, new IType[] { NHibernateUtil.Decimal, NHibernateUtil.Decimal, NHibernateUtil.Int32 }));

            if (!disjunction.ToString().Equals("()"))
            {
                crit.Add(disjunction);
            }
            if (!Utility.Utility.IsEmpty(crit.ToString()))
            {
                if (pageIndex == 0 && pageSize == 0)
                {
                    list = crit
                        .List<Person>();
                }
                else
                {
                    list = crit
                        .SetFirstResult(pageIndex * pageSize)
                        .SetMaxResults(pageSize)
                        .List<Person>();
                }
            }
            return list;
        }

        public IList<Person> SearchByOperator(PersonSearchProxy proxy, decimal userId, int pageIndex, int pageSize, out int count)
        {
            const string PersonDetailAlias = "prsDtl";
            const string WorkGroupAlias = "wg";
            const string RuleGroupAlias = "rg";
            const string CalculationDateRangeGroupAlias = "cdrg";
            const string DepartmentAlias = "dep";
            const string OrganizationUnitAlias = "organ";
            const string PersonTASpecAlias = "prsTs";
            const string GradeAlias = "grade";
            count = 0;


            ICriteria crit = base.NHibernateSession.CreateCriteria(typeof(Person));
            Junction disjunction = Restrictions.Disjunction();
            crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().IsDeleted), false));
            crit.Add(Restrictions.Not(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().BarCode), "00000000")));
            //crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonDetail), PersonDetailAlias);
            crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonDetailList), PersonDetailAlias);


            //فعال
            if (proxy.PersonActivateState != null)
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Active), (bool)proxy.PersonActivateState));
            }

            //کد پرسنلی
            if (!Utility.Utility.IsEmpty(proxy.PersonCode))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().BarCode), proxy.PersonCode, MatchMode.Anywhere));
            }

            //کد ملی
            if (!Utility.Utility.IsEmpty(proxy.MelliCode))
            {
                crit.Add(Restrictions.Like(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().MeliCode), proxy.MelliCode, MatchMode.Anywhere));
            }

            //نام
            if (!Utility.Utility.IsEmpty(proxy.FirstName))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().FirstName), proxy.FirstName, MatchMode.Anywhere));
            }

            //نام خانوادگی
            if (!Utility.Utility.IsEmpty(proxy.LastName))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().LastName), proxy.LastName, MatchMode.Anywhere));
            }

            //نام پدر
            if (!Utility.Utility.IsEmpty(proxy.FatherName))
            {
                crit.Add(Restrictions.Like(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().FatherName), proxy.FatherName, MatchMode.Anywhere));
            }

            //جنسیت ,پیش فرض آن از واسط کاربر -1 است
            if (!Utility.Utility.IsEmpty(proxy.Sex) && proxy.Sex >= 0)
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Sex), proxy.Sex));
            }

            //شماره کارت
            if (!Utility.Utility.IsEmpty(proxy.CartNumber))
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().CardNum), proxy.CartNumber));
            }

            //نظام وضیفه , پیش فرض آن از واسط کاربر 0 است
            if (!Utility.Utility.IsEmpty(proxy.Military) && proxy.Military > 0)
            {
                crit.Add(Restrictions.Eq(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().MilitaryStatus), proxy.Military));
            }

            //تحصیلات
            if (!Utility.Utility.IsEmpty(proxy.Education))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().Education), proxy.Education, MatchMode.Anywhere));
            }

            //تاهل , پیش فرض آن از واسط کاربر 0 است
            if (!Utility.Utility.IsEmpty(proxy.MaritalStatus) && proxy.MaritalStatus > 0)
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().MaritalStatus), proxy.MaritalStatus));
            }

            //شروع تاریخ تولد
            if (!Utility.Utility.IsEmpty(proxy.FromBirthDate))
            {
                crit.Add(Restrictions.Ge(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().BirthDate), proxy.FromBirthDate));
            }

            //پایان تاریخ تولد
            if (!Utility.Utility.IsEmpty(proxy.ToBirthDate))
            {
                crit.Add(Restrictions.Le(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().BirthDate), proxy.ToBirthDate));
            }

            //شروع تاریخ استخدام
            if (!Utility.Utility.IsEmpty(proxy.FromEmploymentDate))
            {
                crit.Add(Restrictions.Ge(Utility.Utility.GetPropertyName(() => new Person().EmploymentDate), proxy.FromEmploymentDate));
            }

            //پایان تاریخ استخدام
            if (!Utility.Utility.IsEmpty(proxy.ToEmploymentDate))
            {
                crit.Add(Restrictions.Ge(Utility.Utility.GetPropertyName(() => new Person().EndEmploymentDate), proxy.ToEmploymentDate));
            }

            //بخش
            if (!Utility.Utility.IsEmpty(proxy.DepartmentId))
            {
                crit.CreateAlias("department", DepartmentAlias);

                if (proxy.IncludeSubDepartments)
                {
                    disjunction.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Department).ToLower(), new Department() { ID = (decimal)proxy.DepartmentId }));
                    disjunction.Add(Restrictions.Like(DepartmentAlias + "." + Utility.Utility.GetPropertyName(() => new Department().ParentPath), "," + proxy.DepartmentId.ToString() + ",", MatchMode.Anywhere));

                }
                else
                {
                    crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Department).ToLower(), new Department() { ID = (decimal)proxy.DepartmentId }));
                }

            }

            //پست سازمانی
            if (!Utility.Utility.IsEmpty(proxy.OrganizationUnitId))
            {
                crit.CreateAlias("OrganizationUnitList", OrganizationUnitAlias);
                crit.Add(Restrictions.Eq(OrganizationUnitAlias + "." + Utility.Utility.GetPropertyName(() => new OrganizationUnit().ID), (decimal)proxy.OrganizationUnitId));
            }
            //رتبه
            if (!Utility.Utility.IsEmpty(proxy.GradeId))
            {
                crit.CreateAlias("grade", GradeAlias);
                crit.Add(Restrictions.Eq(GradeAlias + "." + Utility.Utility.GetPropertyName(() => new Grade().ID), (decimal)proxy.GradeId));
            }

            //گروه کاری
            if (!Utility.Utility.IsEmpty(proxy.WorkGroupId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonWorkGroupList), WorkGroupAlias);
                crit.Add(Restrictions.Eq(WorkGroupAlias + "." + Utility.Utility.GetPropertyName(() => new AssignWorkGroup().WorkGroup), new WorkGroup() { ID = (decimal)proxy.WorkGroupId }));

                if (!Utility.Utility.IsEmpty(proxy.WorkGroupFromDate))
                {
                    crit.Add(Restrictions.Le(WorkGroupAlias + "." + Utility.Utility.GetPropertyName(() => new AssignWorkGroup().FromDate), proxy.WorkGroupFromDate));
                }
            }

            //گروه قوانین
            if (!Utility.Utility.IsEmpty(proxy.RuleGroupId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonRuleCatAssignList), RuleGroupAlias);
                crit.Add(Restrictions.Eq(RuleGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRuleCatAssignment().RuleCategory), new RuleCategory() { ID = (decimal)proxy.RuleGroupId }));

                if (!Utility.Utility.IsEmpty(proxy.RuleGroupFromDate))
                {
                    crit.Add(Restrictions.Le(RuleGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRuleCatAssignment().FromDate), proxy.RuleGroupFromDate));
                }
                if (!Utility.Utility.IsEmpty(proxy.RuleGroupToDate))
                {
                    crit.Add(Restrictions.Ge(RuleGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRuleCatAssignment().ToDate), proxy.RuleGroupToDate));
                }
            }

            //محدوده محاسبات
            if (!Utility.Utility.IsEmpty(proxy.CalculationDateRangeId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonRangeAssignList), CalculationDateRangeGroupAlias);
                crit.Add(Restrictions.Eq(CalculationDateRangeGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRangeAssignment().CalcDateRangeGroup), new CalculationRangeGroup() { ID = (decimal)proxy.CalculationDateRangeId }));

                if (!Utility.Utility.IsEmpty(proxy.CalculationFromDate))
                {
                    crit.Add(Restrictions.Le(CalculationDateRangeGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRangeAssignment().FromDate), proxy.CalculationFromDate));
                }
            }

            //ایستگاه کنترل
            if (!Utility.Utility.IsEmpty(proxy.ControlStationId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonTASpecList), PersonTASpecAlias);
                crit.Add(Restrictions.Eq(PersonTASpecAlias + "." + Utility.Utility.GetPropertyName(() => new PersonTASpec().ControlStation), new ControlStation() { ID = (decimal)proxy.ControlStationId }));
                //crit.Add(Restrictions.Eq("controlStation", new ControlStation() { ID = (decimal)proxy.ControlStationId }));
            }

            //نوع استخدام
            if (!Utility.Utility.IsEmpty(proxy.EmploymentType))
            {
                crit.Add(Restrictions.Eq("employmentType", new EmploymentType() { ID = (decimal)proxy.EmploymentType }));
            }

            //اشخاص مشخص شده
            if (!Utility.Utility.IsEmpty(proxy.PersonIdList))
            {
                crit.Add(Restrictions.In(Utility.Utility.GetPropertyName(() => new Person().ID), CheckListParameter(proxy.PersonIdList)));
            }

            //جستجو در بین مدیران و اپراتورها
            if (proxy.SearchInCategory != PersonCategory.Public
                && !Utility.Utility.IsEmpty(proxy.SearchInCategory))
            {
                if (proxy.SearchInCategory == PersonCategory.Manager)
                {
                    IList<Person> personList = new ManagerRepository(false).GetAllManager();
                    var ids = from person in personList
                              select person.ID;
                    IList<decimal> idList = ids.ToList<decimal>();

                    crit.Add(Restrictions.In(Utility.Utility.GetPropertyName(() => new Person().ID), idList.ToArray()));
                }
            }
            IList<Person> list = new List<Person>();

            crit.Add(Expression.Sql(@" prs_Id in (SELECT Prs_Id FROM
	                                                                (SELECT opr_FlowId
                                                                     FROM TA_Operator     
                                                                     WHERE opr_PersonId = (select user_personid from ta_securityuser where user_ID = ? AND user_Active=1)  
			                                                         AND opr_Active=1
                                                                    ) oprFlow
	                                                                 INNER JOIN (SELECT UndermanagmentsPersons.Prs_Id, Flow.Flow_ID
		                                                             FROM TA_Flow Flow		 
			                                                         CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (Flow.Flow_ID) as UndermanagmentsPersons
		                                                            ) AS UnderMgn
	                                                                ON oprFlow.opr_FlowId = UnderMgn.Flow_Id
                                                 )", new object[] { userId }, new IType[] { NHibernateUtil.Decimal }));

            if (!disjunction.ToString().Equals("()"))
            {
                crit.Add(disjunction);
            }
            if (!Utility.Utility.IsEmpty(crit.ToString()))
            {
                list = crit.List<Person>();
                count = list.Count();
                list = list.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            return list;
        }

        
        public IList<Person> Search(string key, decimal userId,decimal managerId, PersonCategory searchCat, int pageSize, int pageIndex)
        {
            string SQLCommand = "";

            SQLCommand = @"SELECT prs.* FROM TA_Person as prs
                                  where prs_IsDeleted=0 AND prs_Active=1 AND prs_BarCode <> '00000000' 
                                        AND ( prs_BarCode like :searchKey OR
                                        prs_CardNum like :searchKey OR
                                        prs_FirstName + ' ' + prs_LastName like :searchKey)
                                        AND prs_ID in (select * from fn_GetAccessiblePersons(:managerId,:userId,:searchCat))";           



            IQuery query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .AddEntity(typeof(Person))
                .SetParameter("searchKey", "%" + key + "%")
                .SetParameter("userId", userId)
                .SetParameter("managerId", managerId)
                .SetParameter("searchCat", (int)searchCat)
                .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize);

            IList<Person> list = new List<Person>();
           
            list = query.List<Person>();

            return list;
        }

        public IList<Person> SearchByOperator(string key, decimal userId, int pageSize, int pageIndex, out int count)
        {
            string SQLCommand = "";
            IList<Person> list = new List<Person>();

            SQLCommand = @"select * from TA_Person prs
                                  where Prs_IsDeleted=0  AND prs_Active=1 AND 
                                        (prs_BarCode like :searchKey OR
                                        prs_CardNum like :searchKey OR
                                        prs_FirstName + ' ' + prs_LastName like :searchKey)
                                        AND prs.prs_BarCode <> '00000000'
                                        AND prs.prs_ID in (SELECT Prs_Id FROM
	                                                                         (SELECT opr_FlowId
                                                                              FROM TA_Operator     
                                                                              WHERE opr_PersonId = (select user_personid from ta_securityuser where user_ID = :userId AND user_Active=1)  
			                                                                        AND opr_Active=1
                                                                             ) oprFlow
	                                                                         INNER JOIN (SELECT UndermanagmentsPersons.Prs_Id, Flow.Flow_ID
		                                                                     FROM TA_Flow Flow		 
			                                                                 CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (Flow.Flow_ID) as UndermanagmentsPersons
		                                                                     ) AS UnderMgn
	                                                                         ON oprFlow.opr_FlowId = UnderMgn.Flow_Id
            
                                                          )";


            IQuery query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .AddEntity(typeof(Person))
                .SetParameter("searchKey", "%" + key + "%")
                .SetParameter("userId", userId);
            list = query.List<Person>();

            count = list.Count();
            list = list.Skip(pageIndex * pageSize).Take(pageSize).ToList<Person>();
            return list;
        }


        public IList<Person> Search(string key, decimal userId, decimal managerId, PersonCategory searchCat)
        {
            string SQLCommand = "";

            SQLCommand = @"SELECT prs.* FROM TA_Person as prs
                                  where prs_IsDeleted=0 AND prs_Active=1 AND prs_BarCode <> '00000000' 
                                        AND ( prs_BarCode like :searchKey OR
                                        prs_CardNum like :searchKey OR
                                        prs_FirstName + ' ' + prs_LastName like :searchKey)
                                        AND prs_ID in (select * from fn_GetAccessiblePersons(:managerId,:userId,:searchCat))";

            IQuery query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .AddEntity(typeof(Person))
                .SetParameter("searchKey", "%" + key + "%")
                .SetParameter("userId", userId)
                .SetParameter("managerId", managerId)
                .SetParameter("searchCat", (int)searchCat);               

            IList<Person> list = new List<Person>();

            list = query.List<Person>();

            return list;
        }

        public int SearchCount(PersonSearchProxy proxy, decimal userId, decimal managerId, PersonCategory searchCat)
        {
            const string PersonDetailAlias = "prsDtl";
            const string WorkGroupAlias = "wg";
            const string RuleGroupAlias = "rg";
            const string CalculationDateRangeGroupAlias = "cdrg";
            const string DepartmentAlias = "dep";
            const string OrganizationUnitAlias = "organ";
            const string PersonTASpecAlias = "prsTs";


            ICriteria crit = base.NHibernateSession.CreateCriteria(typeof(Person));
            Junction disjunction = Restrictions.Disjunction();
            //crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonDetail), PersonDetailAlias);
            crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonDetailList), PersonDetailAlias);
            crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().IsDeleted), false));
            crit.Add(Restrictions.Not(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().BarCode), "00000000")));

            //فعال
            if (proxy.PersonActivateState != null)
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Active), (bool)proxy.PersonActivateState));
            }

            //کد پرسنلی
            if (!Utility.Utility.IsEmpty(proxy.PersonCode))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().BarCode), proxy.PersonCode, MatchMode.Anywhere));
            }

            //کد ملی
            if (!Utility.Utility.IsEmpty(proxy.MelliCode))
            {
                crit.Add(Restrictions.Like(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().MeliCode), proxy.MelliCode, MatchMode.Anywhere));
            }

            //نام
            if (!Utility.Utility.IsEmpty(proxy.FirstName))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().FirstName), proxy.FirstName, MatchMode.Anywhere));
            }

            //نام خانوادگی
            if (!Utility.Utility.IsEmpty(proxy.LastName))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().LastName), proxy.LastName, MatchMode.Anywhere));
            }

            //نام پدر
            if (!Utility.Utility.IsEmpty(proxy.FatherName))
            {
                crit.Add(Restrictions.Like(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().FatherName), proxy.FatherName, MatchMode.Anywhere));
            }

            //جنسیت
            if (!Utility.Utility.IsEmpty(proxy.Sex))
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Sex), proxy.Sex));
            }

            //شروع تاریخ تولد
            if (!Utility.Utility.IsEmpty(proxy.FromBirthDate))
            {
                crit.Add(Restrictions.Ge(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().BirthDate), proxy.FromBirthDate));
            }

            //پایان تاریخ تولد
            if (!Utility.Utility.IsEmpty(proxy.ToBirthDate))
            {
                crit.Add(Restrictions.Le(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().BirthDate), proxy.ToBirthDate));
            }

            //شروع تاریخ استخدام
            if (!Utility.Utility.IsEmpty(proxy.FromEmploymentDate))
            {
                crit.Add(Restrictions.Ge(Utility.Utility.GetPropertyName(() => new Person().EmploymentDate), proxy.FromEmploymentDate));
            }

            //پایان تاریخ استخدام
            if (!Utility.Utility.IsEmpty(proxy.ToEmploymentDate))
            {
                crit.Add(Restrictions.Ge(Utility.Utility.GetPropertyName(() => new Person().EndEmploymentDate), proxy.ToEmploymentDate));
            }

            //شماره کارت
            if (!Utility.Utility.IsEmpty(proxy.CartNumber))
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().CardNum), proxy.CartNumber));
            }

            //نظام وضیفه
            if (!Utility.Utility.IsEmpty(proxy.Military))
            {
                crit.Add(Restrictions.Eq(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().MilitaryStatus), proxy.Military));
            }

            //تحصیلات
            if (!Utility.Utility.IsEmpty(proxy.Education))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().Education), proxy.Education, MatchMode.Anywhere));
            }

            //تاهل
            if (!Utility.Utility.IsEmpty(proxy.MaritalStatus))
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().MaritalStatus), proxy.MaritalStatus));
            }

            //بخش
            //if (!Utility.Utility.IsEmpty(proxy.DepartmentId))
            //{
            //    crit.CreateAlias("department", DepartmentAlias);

            //    if (proxy.IncludeSubDepartments)
            //    {
            //        disjunction.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Department).ToLower(), new Department() { ID = (decimal)proxy.DepartmentId }));
            //        disjunction.Add(Restrictions.Like(DepartmentAlias + "." + Utility.Utility.GetPropertyName(() => new Department().ParentPath), "," + proxy.DepartmentId.ToString() + ",", MatchMode.Anywhere));

            //    }
            //    else
            //    {
            //        crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Department).ToLower(), new Department() { ID = (decimal)proxy.DepartmentId }));
            //    }

            //}
            if (!Utility.Utility.IsEmpty(proxy.DepartmentListId))
            {
                crit.CreateAlias("department", DepartmentAlias);

                if (proxy.IncludeSubDepartments)
                {
                    disjunction.Add(Restrictions.In(DepartmentAlias + "." + Utility.Utility.GetPropertyName(() => new Department().ID), proxy.DepartmentListId.ToArray()));

                    foreach (decimal item in proxy.DepartmentListId)
                    {
                        disjunction.Add(Restrictions.Like(DepartmentAlias + "." + Utility.Utility.GetPropertyName(() => new Department().ParentPath), "," + item.ToString() + ",", MatchMode.Anywhere));
                    }


                }
                else
                {

                    crit.Add(Restrictions.In(DepartmentAlias + "." + Utility.Utility.GetPropertyName(() => new Department().ID), proxy.DepartmentListId.ToArray()));
                }

            }
            //پست سازمانی
            if (!Utility.Utility.IsEmpty(proxy.OrganizationUnitId))
            {
                crit.CreateAlias("OrganizationUnitList", OrganizationUnitAlias);
                crit.Add(Restrictions.Eq(OrganizationUnitAlias + "." + Utility.Utility.GetPropertyName(() => new OrganizationUnit().ID), (decimal)proxy.OrganizationUnitId));
            }

            //گروه کاری
            if (!Utility.Utility.IsEmpty(proxy.WorkGroupId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonWorkGroupList), WorkGroupAlias);
                crit.Add(Restrictions.Eq(WorkGroupAlias + "." + Utility.Utility.GetPropertyName(() => new AssignWorkGroup().WorkGroup), new WorkGroup() { ID = (decimal)proxy.WorkGroupId }));

                if (!Utility.Utility.IsEmpty(proxy.WorkGroupFromDate))
                {
                    crit.Add(Restrictions.Le(WorkGroupAlias + "." + Utility.Utility.GetPropertyName(() => new AssignWorkGroup().FromDate), proxy.WorkGroupFromDate));
                }
            }

            //گروه قوانین
            if (!Utility.Utility.IsEmpty(proxy.RuleGroupId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonRuleCatAssignList), RuleGroupAlias);
                crit.Add(Restrictions.Eq(RuleGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRuleCatAssignment().RuleCategory), new RuleCategory() { ID = (decimal)proxy.RuleGroupId }));

                if (!Utility.Utility.IsEmpty(proxy.RuleGroupFromDate))
                {
                    crit.Add(Restrictions.Le(RuleGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRuleCatAssignment().FromDate), proxy.RuleGroupFromDate));
                }
                if (!Utility.Utility.IsEmpty(proxy.RuleGroupToDate))
                {
                    crit.Add(Restrictions.Ge(RuleGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRuleCatAssignment().ToDate), proxy.RuleGroupToDate));
                }
            }

            //محدوده محاسبات
            if (!Utility.Utility.IsEmpty(proxy.CalculationDateRangeId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonRangeAssignList), CalculationDateRangeGroupAlias);
                crit.Add(Restrictions.Eq(CalculationDateRangeGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRangeAssignment().CalcDateRangeGroup), new CalculationRangeGroup() { ID = (decimal)proxy.CalculationDateRangeId }));

                if (!Utility.Utility.IsEmpty(proxy.CalculationFromDate))
                {
                    crit.Add(Restrictions.Le(CalculationDateRangeGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRangeAssignment().FromDate), proxy.CalculationFromDate));
                }
            }

            //ایستگاه کنترل
            if (!Utility.Utility.IsEmpty(proxy.ControlStationId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonTASpecList), PersonTASpecAlias);
                crit.Add(Restrictions.Eq(PersonTASpecAlias + "." + Utility.Utility.GetPropertyName(() => new PersonTASpec().ControlStation), new ControlStation() { ID = (decimal)proxy.ControlStationId }));
                //crit.Add(Restrictions.Eq("controlStation", new ControlStation() { ID = (decimal)proxy.ControlStationId }));
            }

            //نوع استخدام
            if (!Utility.Utility.IsEmpty(proxy.EmploymentType))
            {
                crit.Add(Restrictions.Eq("employmentType", new EmploymentType() { ID = (decimal)proxy.EmploymentType }));
            }

            //جستجو در بین مدیران و اپراتورها
            if (proxy.SearchInCategory != PersonCategory.Public
                && !Utility.Utility.IsEmpty(proxy.SearchInCategory))
            {
                if (proxy.SearchInCategory == PersonCategory.Manager)
                {
                    IList<Person> personList = new ManagerRepository(false).GetAllManager();
                    var ids = from person in personList
                              select person.ID;
                    IList<decimal> idList = ids.ToList<decimal>();

                    crit.Add(Restrictions.In(Utility.Utility.GetPropertyName(() => new Person().ID), idList.ToArray()));
                }
            }         
            crit.Add(Expression.Sql(" prs_Id in (select * from fn_GetAccessiblePersons(?,?,?))", new object[] { managerId, userId, (int)searchCat }, new IType[] { NHibernateUtil.Decimal, NHibernateUtil.Decimal, NHibernateUtil.Int32 }));
            if (!disjunction.ToString().Equals("()"))
            {
                crit.Add(disjunction);
            }
            crit.SetProjection(Projections.Count(Utility.Utility.GetPropertyName(() => new Person().ID)));
            if (!Utility.Utility.IsEmpty(crit.ToString()))
            {
                object count = crit.UniqueResult();
                return (int)count;
            }
            return 0;
        }

        public int SearchCountByOperator(PersonSearchProxy proxy, decimal userId)
        {
            const string PersonDetailAlias = "prsDtl";
            const string WorkGroupAlias = "wg";
            const string RuleGroupAlias = "rg";
            const string CalculationDateRangeGroupAlias = "cdrg";
            const string DepartmentAlias = "dep";
            const string OrganizationUnitAlias = "organ";
            const string PersonTASpecAlias = "prsTs";


            ICriteria crit = base.NHibernateSession.CreateCriteria(typeof(Person));
            Junction disjunction = Restrictions.Disjunction();
            //crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonDetail), PersonDetailAlias);
            crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonDetailList), PersonDetailAlias);
            crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().IsDeleted), false));
            crit.Add(Restrictions.Not(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().BarCode), "00000000")));

            //فعال
            if (proxy.PersonActivateState != null)
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Active), (bool)proxy.PersonActivateState));
            }

            //کد پرسنلی
            if (!Utility.Utility.IsEmpty(proxy.PersonCode))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().BarCode), proxy.PersonCode, MatchMode.Anywhere));
            }

            //کد ملی
            if (!Utility.Utility.IsEmpty(proxy.MelliCode))
            {
                crit.Add(Restrictions.Like(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().MeliCode), proxy.MelliCode, MatchMode.Anywhere));
            }

            //نام
            if (!Utility.Utility.IsEmpty(proxy.FirstName))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().FirstName), proxy.FirstName, MatchMode.Anywhere));
            }

            //نام خانوادگی
            if (!Utility.Utility.IsEmpty(proxy.LastName))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().LastName), proxy.LastName, MatchMode.Anywhere));
            }

            //نام پدر
            if (!Utility.Utility.IsEmpty(proxy.FatherName))
            {
                crit.Add(Restrictions.Like(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().FatherName), proxy.FatherName, MatchMode.Anywhere));
            }

            //جنسیت
            if (!Utility.Utility.IsEmpty(proxy.Sex))
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Sex), proxy.Sex));
            }

            //شروع تاریخ تولد
            if (!Utility.Utility.IsEmpty(proxy.FromBirthDate))
            {
                crit.Add(Restrictions.Ge(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().BirthDate), proxy.FromBirthDate));
            }

            //پایان تاریخ تولد
            if (!Utility.Utility.IsEmpty(proxy.ToBirthDate))
            {
                crit.Add(Restrictions.Le(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().BirthDate), proxy.ToBirthDate));
            }

            //شروع تاریخ استخدام
            if (!Utility.Utility.IsEmpty(proxy.FromEmploymentDate))
            {
                crit.Add(Restrictions.Ge(Utility.Utility.GetPropertyName(() => new Person().EmploymentDate), proxy.FromEmploymentDate));
            }

            //پایان تاریخ استخدام
            if (!Utility.Utility.IsEmpty(proxy.ToEmploymentDate))
            {
                crit.Add(Restrictions.Ge(Utility.Utility.GetPropertyName(() => new Person().EndEmploymentDate), proxy.ToEmploymentDate));
            }

            //شماره کارت
            if (!Utility.Utility.IsEmpty(proxy.CartNumber))
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().CardNum), proxy.CartNumber));
            }

            //نظام وضیفه
            if (!Utility.Utility.IsEmpty(proxy.Military))
            {
                crit.Add(Restrictions.Eq(PersonDetailAlias + "." + Utility.Utility.GetPropertyName(() => new PersonDetail().MilitaryStatus), proxy.Military));
            }

            //تحصیلات
            if (!Utility.Utility.IsEmpty(proxy.Education))
            {
                crit.Add(Restrictions.Like(Utility.Utility.GetPropertyName(() => new Person().Education), proxy.Education, MatchMode.Anywhere));
            }

            //تاهل
            if (!Utility.Utility.IsEmpty(proxy.MaritalStatus))
            {
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().MaritalStatus), proxy.MaritalStatus));
            }

            //بخش
            if (!Utility.Utility.IsEmpty(proxy.DepartmentId))
            {
                crit.CreateAlias("department", DepartmentAlias);

                if (proxy.IncludeSubDepartments)
                {
                    disjunction.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Department).ToLower(), new Department() { ID = (decimal)proxy.DepartmentId }));
                    disjunction.Add(Restrictions.Like(DepartmentAlias + "." + Utility.Utility.GetPropertyName(() => new Department().ParentPath), "," + proxy.DepartmentId.ToString() + ",", MatchMode.Anywhere));

                }
                else
                {
                    crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().Department).ToLower(), new Department() { ID = (decimal)proxy.DepartmentId }));
                }

            }

            //پست سازمانی
            if (!Utility.Utility.IsEmpty(proxy.OrganizationUnitId))
            {
                crit.CreateAlias("OrganizationUnitList", OrganizationUnitAlias);
                crit.Add(Restrictions.Eq(OrganizationUnitAlias + "." + Utility.Utility.GetPropertyName(() => new OrganizationUnit().ID), (decimal)proxy.OrganizationUnitId));
            }

            //گروه کاری
            if (!Utility.Utility.IsEmpty(proxy.WorkGroupId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonWorkGroupList), WorkGroupAlias);
                crit.Add(Restrictions.Eq(WorkGroupAlias + "." + Utility.Utility.GetPropertyName(() => new AssignWorkGroup().WorkGroup), new WorkGroup() { ID = (decimal)proxy.WorkGroupId }));

                if (!Utility.Utility.IsEmpty(proxy.WorkGroupFromDate))
                {
                    crit.Add(Restrictions.Le(WorkGroupAlias + "." + Utility.Utility.GetPropertyName(() => new AssignWorkGroup().FromDate), proxy.WorkGroupFromDate));
                }
            }

            //گروه قوانین
            if (!Utility.Utility.IsEmpty(proxy.RuleGroupId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonRuleCatAssignList), RuleGroupAlias);
                crit.Add(Restrictions.Eq(RuleGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRuleCatAssignment().RuleCategory), new RuleCategory() { ID = (decimal)proxy.RuleGroupId }));

                if (!Utility.Utility.IsEmpty(proxy.RuleGroupFromDate))
                {
                    crit.Add(Restrictions.Le(RuleGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRuleCatAssignment().FromDate), proxy.RuleGroupFromDate));
                }
                if (!Utility.Utility.IsEmpty(proxy.RuleGroupToDate))
                {
                    crit.Add(Restrictions.Ge(RuleGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRuleCatAssignment().ToDate), proxy.RuleGroupToDate));
                }
            }

            //محدوده محاسبات
            if (!Utility.Utility.IsEmpty(proxy.CalculationDateRangeId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonRangeAssignList), CalculationDateRangeGroupAlias);
                crit.Add(Restrictions.Eq(CalculationDateRangeGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRangeAssignment().CalcDateRangeGroup), new CalculationRangeGroup() { ID = (decimal)proxy.CalculationDateRangeId }));

                if (!Utility.Utility.IsEmpty(proxy.CalculationFromDate))
                {
                    crit.Add(Restrictions.Le(CalculationDateRangeGroupAlias + "." + Utility.Utility.GetPropertyName(() => new PersonRangeAssignment().FromDate), proxy.CalculationFromDate));
                }
            }

            //ایستگاه کنترل
            if (!Utility.Utility.IsEmpty(proxy.ControlStationId))
            {
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonTASpecList), PersonTASpecAlias);
                crit.Add(Restrictions.Eq(PersonTASpecAlias + "." + Utility.Utility.GetPropertyName(() => new PersonTASpec().ControlStation), new ControlStation() { ID = (decimal)proxy.ControlStationId }));
                //crit.Add(Restrictions.Eq("controlStation", new ControlStation() { ID = (decimal)proxy.ControlStationId }));
            }

            //نوع استخدام
            if (!Utility.Utility.IsEmpty(proxy.EmploymentType))
            {
                crit.Add(Restrictions.Eq("employmentType", new EmploymentType() { ID = (decimal)proxy.EmploymentType }));
            }

            //جستجو در بین مدیران و اپراتورها
            if (proxy.SearchInCategory != PersonCategory.Public
                && !Utility.Utility.IsEmpty(proxy.SearchInCategory))
            {
                if (proxy.SearchInCategory == PersonCategory.Manager)
                {
                    IList<Person> personList = new ManagerRepository(false).GetAllManager();
                    var ids = from person in personList
                              select person.ID;
                    IList<decimal> idList = ids.ToList<decimal>();

                    crit.Add(Restrictions.In(Utility.Utility.GetPropertyName(() => new Person().ID), idList.ToArray()));
                }
            }
            crit.Add(Expression.Sql(@" prs_Id in (SELECT Prs_Id FROM
	                                                                (SELECT opr_FlowId
                                                                     FROM TA_Operator     
                                                                     WHERE opr_PersonId = (select user_personid from ta_securityuser where user_ID = ? AND user_Active=1)  
			                                                         AND opr_Active=1
                                                                    ) oprFlow
	                                                                 INNER JOIN (SELECT UndermanagmentsPersons.Prs_Id, Flow.Flow_ID
		                                                             FROM TA_Flow Flow		 
			                                                         CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (Flow.Flow_ID) as UndermanagmentsPersons
		                                                            ) AS UnderMgn
	                                                                ON oprFlow.opr_FlowId = UnderMgn.Flow_Id
                                                 )", new object[] {userId}, new IType[] {NHibernateUtil.Decimal}));
            if (!disjunction.ToString().Equals("()"))
            {
                crit.Add(disjunction);
            }
            crit.SetProjection(Projections.Count(Utility.Utility.GetPropertyName(() => new Person().ID)));
            if (!Utility.Utility.IsEmpty(crit.ToString()))
            {
                object count = crit.UniqueResult();
                return (int)count;
            }
            return 0;
        }


        public int SearchCount(string key, decimal userId,decimal managerId, PersonCategory searchCat)
        {
            string SQLCommand = "";

            SQLCommand = @"select count(prs_ID) from TA_Person prs
                                  where Prs_IsDeleted=0  AND prs_Active=1 AND 
                                        (prs_BarCode like :searchKey OR
                                        prs_CardNum like :searchKey OR
                                        prs_FirstName + ' ' + prs_LastName like :searchKey)
                                        AND prs.prs_BarCode <> '00000000'
                                        AND prs.prs_ID in (select * from fn_GetAccessiblePersons(:managerId,:userId,:searchCat))";        


            IQuery query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("searchKey", "%" + key + "%")
                .SetParameter("userId", userId)
                .SetParameter("managerId", managerId)
                .SetParameter("searchCat", (int)searchCat);
            object count = query.List<object>().First();
            return Utility.Utility.ToInteger(count.ToString());          
        }

        public int SearchCountByOperator(string key, decimal userId)
        {
            string SQLCommand = "";

            SQLCommand = @"select count(prs_ID) from TA_Person prs
                                  where Prs_IsDeleted=0  AND prs_Active=1 AND 
                                        (prs_BarCode like :searchKey OR
                                        prs_CardNum like :searchKey OR
                                        prs_FirstName + ' ' + prs_LastName like :searchKey)
                                        AND prs.prs_BarCode <> '00000000'
                                        AND prs.prs_ID in (SELECT Prs_Id FROM
	                                                                         (SELECT opr_FlowId
                                                                              FROM TA_Operator     
                                                                              WHERE opr_PersonId = (select user_personid from ta_securityuser where user_ID = :userId AND user_Active=1)  
			                                                                        AND opr_Active=1
                                                                             ) oprFlow
	                                                                         INNER JOIN (SELECT UndermanagmentsPersons.Prs_Id, Flow.Flow_ID
		                                                                     FROM TA_Flow Flow		 
			                                                                 CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (Flow.Flow_ID) as UndermanagmentsPersons
		                                                                     ) AS UnderMgn
	                                                                         ON oprFlow.opr_FlowId = UnderMgn.Flow_Id
                                                          )";
            IQuery query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("searchKey", "%" + key + "%")
                .SetParameter("userId", userId);
            object count = query.List<object>().First();
            return Utility.Utility.ToInteger(count.ToString());
        }

       
        #endregion

        public int GetTestData(string[] ids)
        {
            string SQLQuery = " SELECT count(*) FROM TA_Person WHERE prs_ID in (" + string.Join(",", ids) + ")";

            int count = base.NHibernateSession.CreateSQLQuery(SQLQuery)
                .List<int>().First();
            return count;
        }

        public void TEST(decimal userId)
        {
            try
            {
                Person personAlias = null;
                IQueryOver<Person, Person> PersonIqueryOver = null;
                //PersonIqueryOver = base.NHibernateSession.QueryOver<Person>().Where(()=> personAlias.BarCode == "");
                //PersonIqueryOver=PersonIqueryOver.Where(()=>personAlias.Active==true);
                //PersonIqueryOver.Skip(2*10)
                //                .Take(10)
                //                .List();

                const string PersonDetailAlias = "prsDtl";
                const string WorkGroupAlias = "wg";
                const string RuleGroupAlias = "rg";
                const string CalculationDateRangeGroupAlias = "cdrg";
                const string DepartmentAlias = "dep";
                const string OrganizationUnitAlias = "organ";

                ICriteria crit = base.NHibernateSession.CreateCriteria(typeof(Person));

                crit.Add(Expression.Sql("prs_Id in (select * from fn_GetAccessiblePersons(?))", 1, NHibernateUtil.Decimal));



                crit.SetProjection(Projections.Count(Utility.Utility.GetPropertyName(() => new Person().ID)));
                if (!Utility.Utility.IsEmpty(crit.ToString()))
                {
                    object count = crit.UniqueResult();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                int a = 0;
            }
        }

        public void DeletePersonnelImage(string path)
        {
            if (path != null && path != string.Empty && File.Exists(path))
                File.Delete(path);
        }

        /// <summary>
        /// Sysnc the Time Atendance System Persons with General system
        /// </summary>
        public void SyncPersonTASpec() 
        {
            string SQLCommand = @"INSERT INTO TA_PersonTASpec(prsTA_ID,prsTA_Active)
                                    SELECT Prs_ID,Prs_Active FROM TA_Person 
                                    WHERE prs_IsDeleted=0 
                                    AND Prs_ID NOT IN (SELECT prsTA_ID FROM TA_PersonTASpec)";
            base.RunSQL(SQLCommand);
        }

        public bool CheckIsControlStationInUseByPerson(ControlStation controlStation)
        {
            bool isInUse = false;
            int count = 0;
            count = NHibernateSession.QueryOver<PersonTASpec>()
                                     .JoinQueryOver<ControlStation>(pts => pts.ControlStation)
                                     .Where(c => c.ID == controlStation.ID)
                                     .RowCount();
            if (count > 0)
                isInUse = true;
            return isInUse;
        }

        public bool CheckIsUIValidationGroupInUse(UIValidationGroup uiValidationGroup)
        {
            bool isInUse = false;
            int count = 0;
            count = NHibernateSession.QueryOver<PersonTASpec>()
                                     .JoinQueryOver<UIValidationGroup>(pts => pts.UIValidationGroup)
                                     .Where(c => c.ID == uiValidationGroup.ID)
                                     .RowCount();
            if (count > 0)
                isInUse = true;
            return isInUse;
        }

        public IList<Person> GetPersonByPersonIdList(IList<decimal> personIdList)
        {
            IList<Person> PersonList = this.NHibernateSession.CreateCriteria<Person>()
                                                             .Add(Restrictions.In(Utility.Utility.GetPropertyName(() => new Person().ID), personIdList.ToArray()))
                                                             .List<Person>();
            return PersonList;
        }

        public PersonTASpec GetPersonTASpecByID(decimal personID)
        {
            PersonTASpec personTASpec = NHibernateSession.QueryOver<PersonTASpec>()
                                                         .Where(x => x.ID == personID)
                                                         .SingleOrDefault();
            return personTASpec;
        }

        /// <summary>
        /// حذف اطلاعات اضافی در هنگام درج
        /// Create Working
        /// </summary>
        public void DeleteExtraRecordFromDB()
        {
            string SQLCommand = @"
declare @daycount int
set @daycount=3

DELETE FROM CL_PersonCLSpec where prscl_ID in
 (
 SELECT prs_ID FROM TA_Person
 where Prs_Barcode='00000000' and Prs_Active=0 and prs_CreationDate <= dateadd(day,datediff(day,@daycount,GETDATE()),0)
 )

 DELETE FROM TA_PersonTASpec where prsta_ID in
 (
 SELECT prs_ID FROM TA_Person
 where Prs_Barcode='00000000' and Prs_Active=0 and prs_CreationDate <= dateadd(day,datediff(day,@daycount,GETDATE()),0)
 )

DELETE FROM TA_Person
 where Prs_Barcode='00000000' and Prs_Active=0 and prs_CreationDate <= dateadd(day,datediff(day,@daycount,GETDATE()),0)

 DELETE FROM CL_Contractor
 where contractor_Name='00000000'  and contractor_CreationDate <= dateadd(day,datediff(day,@daycount,GETDATE()),0)
";
            base.RunSQL(SQLCommand);
        }

    }
}

