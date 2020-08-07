using GTS.Clock.Model.RequestFlow;
using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.RepositoryFramework;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using NHibernate.Type;

namespace GTS.Clock.Infrastructure.Repository
{
    public class ImperativeRequestRepository : RepositoryBase<ImperativeRequest>
    {
        public override string TableName
        {
            get { return "TA_ImperativeRequest"; }
        }
        public ImperativeRequestRepository()
            : base()
        { }

        public int GetAdvancedSearchPersonCountByImperativeRequest(PersonSearchProxy proxy, ImperativeRequestLoadState IRLS, ImperativeRequest imperativeRequest, decimal userId, decimal managerId, PersonCategory searchCat)
        {
            {
                const string PersonDetailAlias = "prsDtl";
                const string WorkGroupAlias = "wg";
                const string RuleGroupAlias = "rg";
                const string CalculationDateRangeGroupAlias = "cdrg";
                const string DepartmentAlias = "dep";
                const string OrganizationUnitAlias = "organ";


                ICriteria crit = base.NHibernateSession.CreateCriteria(typeof(Person));
                Junction disjunction = Restrictions.Disjunction();
                crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonDetail), PersonDetailAlias);
                crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().IsDeleted), false));

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
                    crit.Add(Restrictions.Eq("controlStation", new ControlStation() { ID = (decimal)proxy.ControlStationId }));
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
                if (IRLS == ImperativeRequestLoadState.Applied || IRLS == ImperativeRequestLoadState.NotApplied)
                {
                    IList<decimal> ImperativeRequestIDsList = this.NHibernateSession.QueryOver<ImperativeRequest>()
                                                                                 .Where(impReq => impReq.Precard.ID == imperativeRequest.Precard.ID && impReq.IsLocked && impReq.Year == imperativeRequest.Year && impReq.Month == imperativeRequest.Month)
                                                                                 .Select(impReq => impReq.ID)
                                                                                 .List<decimal>();
                    if (imperativeRequest.IsLocked)
                        crit.Add(Restrictions.In(Utility.Utility.GetPropertyName(() => new Person().ID), ImperativeRequestIDsList.ToArray()));
                    else
                        crit.Add(Restrictions.Not(Restrictions.In(Utility.Utility.GetPropertyName(() => new Person().ID), ImperativeRequestIDsList.ToArray()))); 
                }
                crit.SetProjection(Projections.Count(Utility.Utility.GetPropertyName(() => new Person().ID)));
                if (!Utility.Utility.IsEmpty(crit.ToString()))
                {
                    object count = crit.UniqueResult();
                    return (int)count;
                }
                return 0;
            }
        }

        public IList<Person> GetAdvancedSearchPersonByImperativeRequest(PersonSearchProxy proxy, ImperativeRequestLoadState IRLS, ImperativeRequest imperativeRequest, decimal userId, decimal managerId, PersonCategory searchCat, int pageIndex, int pageSize)
        {
            const string PersonDetailAlias = "prsDtl";
            const string WorkGroupAlias = "wg";
            const string RuleGroupAlias = "rg";
            const string CalculationDateRangeGroupAlias = "cdrg";
            const string DepartmentAlias = "dep";
            const string OrganizationUnitAlias = "organ";

            ICriteria crit = base.NHibernateSession.CreateCriteria(typeof(Person));
            Junction disjunction = Restrictions.Disjunction();
            crit.Add(Restrictions.Eq(Utility.Utility.GetPropertyName(() => new Person().IsDeleted), false));
            crit.CreateAlias(Utility.Utility.GetPropertyName(() => new Person().PersonDetail), PersonDetailAlias);

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
                crit.Add(Restrictions.Eq("controlStation", new ControlStation() { ID = (decimal)proxy.ControlStationId }));
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
            IList<Person> list = new List<Person>();

            crit.Add(Expression.Sql(" prs_Id in (select * from fn_GetAccessiblePersons(?,?,?))", new object[] { managerId, userId, (int)searchCat }, new IType[] { NHibernateUtil.Decimal, NHibernateUtil.Decimal, NHibernateUtil.Int32 }));

            if (!disjunction.ToString().Equals("()"))
            {
                crit.Add(disjunction);
            }
            if (IRLS == ImperativeRequestLoadState.Applied || IRLS == ImperativeRequestLoadState.NotApplied)
            {
                IList<decimal> ImperativeRequestIDsList = this.NHibernateSession.QueryOver<ImperativeRequest>()
                                                             .Where(impReq => impReq.Precard.ID == imperativeRequest.Precard.ID && impReq.IsLocked && impReq.Year == imperativeRequest.Year && impReq.Month == imperativeRequest.Month)
                                                             .Select(impReq => impReq.ID)
                                                             .List<decimal>();
                if (imperativeRequest.IsLocked)
                    crit.Add(Restrictions.In(Utility.Utility.GetPropertyName(() => new Person().ID), ImperativeRequestIDsList.ToArray()));
                else
                    crit.Add(Restrictions.Not(Restrictions.In(Utility.Utility.GetPropertyName(() => new Person().ID), ImperativeRequestIDsList.ToArray())));
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

        public int GetQuickSearchPersonCountByImperativeRequest(string key, ImperativeRequestLoadState IRLS, ImperativeRequest imperativeRequest, decimal userId, decimal managerId, PersonCategory searchCat)
        {
            string SQLCommand = "";

            SQLCommand = @"select count(prs_ID) from TA_Person prs
                                  where Prs_IsDeleted=0  AND prs_Active=1 AND 
                                        (prs_BarCode like :searchKey OR
                                        prs_CardNum like :searchKey OR
                                        prs_FirstName + ' ' + prs_LastName like :searchKey)
                                        AND prs.prs_BarCode <> '00000000'
                                        AND prs.prs_ID in (select * from fn_GetAccessiblePersons(:managerId,:userId,:searchCat))";

            if (IRLS != ImperativeRequestLoadState.Normal)
            {
                SQLCommand += "AND prs_ID";
                switch (IRLS)
                {
                    case ImperativeRequestLoadState.Applied:
                        SQLCommand += " in";
                        break;
                    case ImperativeRequestLoadState.NotApplied:
                        SQLCommand += " not in";
                        break;
                }
                SQLCommand += @"(select impReq.imperativeRequest_PersonID from TA_ImperativeRequest impReq 
                             where impReq.imperativeRequest_PrecardID = :precardId
                             and impReq.imperativeRequest_IsLocked = :isLocked 
                             and impReq.imperativeRequest_Year = :year 
                             and impReq.imperativeRequest_Month = :month
                            )";
            }

            IQuery query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("searchKey", "%" + key + "%")
                .SetParameter("userId", userId)
                .SetParameter("managerId", managerId)
                .SetParameter("searchCat", (int)searchCat);
            if (IRLS == ImperativeRequestLoadState.Applied || IRLS == ImperativeRequestLoadState.NotApplied)
                query = query.SetParameter("precardId", imperativeRequest.Precard.ID)
                             .SetParameter("isLocked", imperativeRequest.IsLocked)
                             .SetParameter("year", imperativeRequest.Year)
                             .SetParameter("month", imperativeRequest.Month);
            object count = query.List<object>().First();
            return Utility.Utility.ToInteger(count.ToString());          
        }

        public IList<Person> GetQuickSearchPersonByImperativeRequest(string key, ImperativeRequestLoadState IRLS, ImperativeRequest imperativeRequest, decimal userId, decimal managerId, PersonCategory searchCat, int pageSize, int pageIndex)
        {
            string SQLCommand = "";

            SQLCommand = @"SELECT prs.* FROM TA_Person as prs
                                  where prs_IsDeleted=0 AND prs_Active=1 AND prs_BarCode <> '00000000' 
                                        AND ( prs_BarCode like :searchKey OR
                                        prs_CardNum like :searchKey OR
                                        prs_FirstName + ' ' + prs_LastName like :searchKey)
                                        AND prs_ID in (select * from fn_GetAccessiblePersons(:managerId,:userId,:searchCat))";

            if (IRLS != ImperativeRequestLoadState.Normal)
            {
                SQLCommand += "AND prs_ID";
                switch (IRLS)
                {
                    case ImperativeRequestLoadState.Applied:
                        SQLCommand += " in";
                        break;
                    case ImperativeRequestLoadState.NotApplied:
                        SQLCommand += " not in";
                        break;
                }
                SQLCommand += @"(select impReq.imperativeRequest_PersonID from TA_ImperativeRequest impReq 
                             where impReq.imperativeRequest_PrecardID = :precardId
                             and impReq.imperativeRequest_IsLocked = :isLocked 
                             and impReq.imperativeRequest_Year = :year 
                             and impReq.imperativeRequest_Month = :month
                            )";
            }

            IQuery query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .AddEntity(typeof(Person))
                .SetParameter("searchKey", "%" + key + "%")
                .SetParameter("userId", userId)
                .SetParameter("managerId", managerId)
                .SetParameter("searchCat", (int)searchCat);                
            if(IRLS == ImperativeRequestLoadState.Applied || IRLS == ImperativeRequestLoadState.NotApplied)
                query = query.SetParameter("precardId", imperativeRequest.Precard.ID)
                             .SetParameter("isLocked", imperativeRequest.IsLocked)
                             .SetParameter("year", imperativeRequest.Year)
                             .SetParameter("month", imperativeRequest.Month);
              query = query.SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize);

            IList<Person> list = new List<Person>();

            list = query.List<Person>();

            return list;
        }

        public ImperativeRequest GetImperativeRequest(ImperativeRequest imperativeRequest)
        {
            ImperativeRequest impRequset = null;
            IList<ImperativeRequest> imperativeRequestList = base.NHibernateSession.QueryOver<ImperativeRequest>()
                                                                                   .Where(impReq => impReq.Person.ID == imperativeRequest.Person.ID && impReq.Precard.ID == imperativeRequest.Precard.ID && impReq.Year == imperativeRequest.Year && impReq.Month == imperativeRequest.Month)
                                                                                   .List<ImperativeRequest>();
            if (imperativeRequestList.Count() > 0)
                impRequset = imperativeRequestList.First();
            return impRequset;      
        }

    }
}
