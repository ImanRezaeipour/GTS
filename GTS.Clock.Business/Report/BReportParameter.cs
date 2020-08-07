using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Report;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Model.Report;
using GTS.Clock.Business.Proxy;
using Stimulsoft.Report;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.RequestFlow;

namespace GTS.Clock.Business.Reporting
{
    /// <summary>
    /// created at: 2011-11-22 12:50:50 PM
    ///Farhad Salavati
    /// </summary>
    public class BReportParameter:MarshalByRefObject
    {
        const string ExceptionSrc = "GTS.Clock.Business.Reporting.BReportParameter";
        BControlParameter_YearMonth bControlParameter_YearMonth = new BControlParameter_YearMonth();
        ISearchPerson personSearch = new BPerson();
        EntityRepository<ReportParameter> ReportParamRep = new EntityRepository<ReportParameter>();
        UIValidationExceptions exception = new UIValidationExceptions();
        private GTSEngineWS.TotalWebServiceClient gtsEngineWS = new GTS.Clock.Business.GTSEngineWS.TotalWebServiceClient();

        #region Fill Page ComboBoxes

        /// <summary>
        /// لیست گروهای کاری را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<WorkGroup> GetAllWorkGroups()
        {
            return personSearch.GetAllWorkGroup();
        }

        /// <summary>
        /// لیست گروهای قوانین را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<RuleCategory> GetAllRuleGroups()
        {
            return personSearch.GetAllRuleGroup();
        }

        /// <summary>
        /// لیست گروهای محدوده محاسبات را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<CalculationRangeGroup> GetAllDateRanges()
        {
            return personSearch.GetAllDateRanges();
        }

        /// <summary>
        /// لیست ایستگاههای کنترل را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<ControlStation> GetAllControlStations()
        {
            return personSearch.GetAllControlStation();
        }

        /// <summary>
        /// لیست انواع استخدام را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<EmploymentType> GetAllEmploymentTypes()
        {
            return personSearch.GetAllEmploymentTypes();
        }

        /// <summary>
        /// ریشه بخش را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public Department GetDepartmentRoot()
        {
            return personSearch.GetDepartmentRoot();
        }

        public IList<Department> GetAllDepartments()
        {
            //return new BDepartment().GetAll();
            return personSearch.GetAllDepartments();
        }

        /// <summary>
        /// بچههای یک گره را برمیگرداند
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentChilds(decimal parentId, IList<Department> allNodes)
        {
            return personSearch.GetDepartmentChild(parentId, allNodes);
        }

        /// <summary>
        /// ریشه چارت سازمانی را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public OrganizationUnit GetOrganizationUnitRoot()
        {
            return personSearch.GetOrganizationRoot();
        }

        /// <summary>
        /// بچههای یک گره را برمیگرداند
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<OrganizationUnit> GetOrganizationUnitChilds(decimal parentId)
        {
            return personSearch.GetOrganizationChild(parentId);
        }

        /// <summary>
        /// لیست پرسنل را برمیگرداند
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IList<Person> GetAllPersons(int pageIndex, int PageSize)
        {
            ISearchPerson searchTool = new BPerson();
            IList<Person> list = searchTool.GetAllPerson(pageIndex, PageSize);
            return list;
        }

        /// <summary>
        /// تعداد پرسنل را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public int GetAllPaeronsCount()
        {
            ISearchPerson searchTool = new BPerson();
            int count = searchTool.GetPersonCount();
            return count;
        }


        #endregion

        #region Persons
       
        /// <summary>
        /// لیست نتایج جستجو را برمیگرداند
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IList<Person> GetAllPersons(Business.Proxy.PersonAdvanceSearchProxy proxy, int pageIndex, int PageSize)
        {
            ISearchPerson bperson = new BPerson();
            IList<Person> list = bperson.GetPersonInAdvanceSearch(proxy, pageIndex, PageSize);
            return list;
        }

        /// <summary>
        /// تعداد کل نتایج جستجو را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public int GetAllPaeronsCount(Business.Proxy.PersonAdvanceSearchProxy proxy)
        {
            ISearchPerson bperson = new BPerson();
            int count = bperson.GetPersonInAdvanceSearchCount(proxy);
            return count;
        }
        
        #endregion
      
        /// <summary>
        /// پارامتر های یک گزارش را برمیگرداند
        /// </summary>
        /// <param name="reportId">شناسه گزارش</param>
        /// <returns></returns>
        public IList<ReportUIParameter> GetUIReportParameters(decimal reportFileId)
        {
            try
            {
                IList<ReportUIParameter> resultList = new List<ReportUIParameter>();
                IList<ReportParameter> list = ReportParamRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ReportParameter().ReportFile), new ReportFile() { ID = reportFileId }));
                var a = from o in list
                        select o.ReportUIParameter;
                var result = from y in a
                             group y by y;
                foreach (var found in result)
                {
                    ReportUIParameter parameter = found.Key;
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        parameter.ParameterTitle = parameter.fnName;
                    }
                    else
                    {
                        parameter.ParameterTitle = parameter.EnName;
                    }
                    resultList.Add(parameter);
                }

                return resultList;
            }
            catch (Exception ex)
            {
                BaseBusiness<ReportParameter>.LogException(ex, "BReportParameter", "GetReportParameter");
                throw ex;
            }
        }

        /// <summary>
        /// گزارش را نمایش میدهد
        /// </summary>
        /// <param name="reportFileId"></param>
        /// <param name="proxy"></param>
        /// <param name="parmeters"></param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public StiReport GetReport(decimal reportFileId, PersonAdvanceSearchProxy proxy, IList<ReportUIParameter> parmeters)
        {
            try
            {
                if (parmeters.Where(x => Utility.IsEmpty(x.ActionId)).Count() > 0) 
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ReportParameterActionIdIsEmpty, "شناسه عملیات مشخص نشده است", ExceptionSrc));
                    throw exception;
                }
                ISearchPerson searchTool = new BPerson();
                IList<decimal> personIdList;
                IList<Person> persons;
                if (Utility.IsEmpty(parmeters)) 
                {
                    //مثلا برای گزارش شیفتها چه نیازی به لیست پرسنل است
                    persons = new List<Person>();// searchTool.QuickSearch("", PersonCategory.Public);
                }
                else
                {
                    if (proxy.PersonIdList == null || proxy.PersonIdList.Count == 0)
                        persons = searchTool.GetPersonInAdvanceSearch(proxy);
                    else
                        persons = searchTool.GetPersonByPersonIdList(proxy.PersonIdList);
                }
                //کلیه پرسنل مدیر , جانشین و اپراتور
                if(persons.Count == 0 && !Utility.IsEmpty(parmeters) )
                {
                    persons = searchTool.QuickSearchByPage(0, 1000, "");
                }
                    
                if (persons.Count == 0 && !Utility.IsEmpty(parmeters))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ReportParameterPersonIsEmpty, "مجموعه انتخابی شامل هیچ پرسنلی نمیباشد", ExceptionSrc));
                    throw exception;
                }
                var ids = from o in persons
                          select o.ID;
                personIdList = ids.ToList<decimal>();

                IDictionary<string, object> ParamValues = new Dictionary<string, object>();
                foreach (ReportUIParameter param in parmeters)
                {
                    if (Utility.IsEmpty(param.Value))
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.ReportParametersIsEmpty, "مقدار پارامترها مشخص نشده است", ExceptionSrc));
                        throw exception;
                    }

                    string value = param.Value;
                    IDictionary<string, object> result;
                    if (BusinessFactory.Exists(param.ActionId))
                    {
                        result = BusinessFactory.GetBusiness<IBControlParameter>(param.ActionId).ParsParameter(value, param.ActionId);
                    }
                    else
                    {
                        result = BaseControlParameter.ParsParameter(value);
                    }
                    if (result != null)
                    {
                        foreach (var item in result)
                        {
                            ParamValues.Add(item);
                        }
                    }
                }

                ReportFile file = this.GetReportFile(reportFileId);
                ReportHelper reportHelper = ReportHelper.Instance("شرکت طرح و پردازش غدیر", BUser.CurrentUser.ID, BUser.CurrentUser.Person.Name, personIdList);
                StiReport report = reportHelper.GetReport(@file.File);
                reportHelper.InitAssemblyReport(report);
                reportHelper.InitReportParameter(report, ParamValues);
                reportHelper.InitReportConnection(report, ReportParamRep.GetConnectionString());


                return report;
            }
            catch (Exception ex)
            {
                BaseBusiness<Report>.LogException(ex, "BReportParameter", "GetReport");
                throw ex;
            }
        }

        /// <summary>
        /// یک فایل گزارش را میگرداند
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        private ReportFile GetReportFile(decimal fileId)
        {
            EntityRepository<ReportFile> reportFileReposiory = new EntityRepository<ReportFile>();
            ReportFile file = reportFileReposiory.GetById(fileId, false);
            return file;
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckReportParametersLoadAccess()
        { 
        }
    }
}
