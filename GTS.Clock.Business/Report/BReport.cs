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
using GTS.Clock.Model.Report;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.Reporting
{
    public class BReport : BaseBusiness<Report>
    {
        IDataAccess accessPort = new BUser();
        EntityRepository<Report> reportRep = new EntityRepository<Report>(false);
        const string ExceptionSrc = "GTS.Clock.Business.Reporting.BReport";

        #region Report Tree

        /// <summary>
        /// ریشه را برمیگرداند
        /// اما نشست خالی نمیشود تا اشیا پرسیست شده باشد
        /// </summary>
        /// <returns></returns>
        public Report GetReportRoot()
        {
            try
            {
                IList<Report> list = reportRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Report().ParentId), (decimal)0));
                if (list != null && list.Count == 1)
                {
                    return list.First();
                }
                else
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.ReportRootMoreThanOne, "تعداد ریشه درخت گزارشات در دیتابیس نامعتبر است", ExceptionSrc);
                }
            }
            catch (Exception ex) 
            {
                LogException(ex, "BReport", "GetReportRoot");
                throw ex;
            }
        }

        public IList<Report> GetReportChilds(decimal parentId) 
        {
            try
            {
                IList<decimal> ids = accessPort.GetAccessibleReports();
                IList<Report> list = reportRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Report().ParentId), parentId),
                                                             new CriteriaStruct(Utility.GetPropertyName(() => new Report().ID), ids.ToArray(), CriteriaOperation.IN));
                foreach (Report report in list)
                {
                    if (report.IsReport)
                    {
                        report.HasParameter = this.HasReportParameter(report.ReportFile.ID);
                    }
                }
                List<Report> result = new List<Report>();
                result.AddRange(list.Where(x => x.IsReport).OrderBy(x => x.Order));
                result.AddRange(list.Where(x => !x.IsReport).OrderBy(x => x.Order));
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetReportChilds");
                throw ex;
            }
        }

        public IList<Report> GetReportChildsWidoutDA(decimal parentId)
        {
            try
            {
                IList<Report> list = reportRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Report().ParentId), parentId));
                foreach (Report report in list)
                {
                    if (report.IsReport)
                    {
                        report.HasParameter = this.HasReportParameter(report.ReportFile.ID);
                    }
                }
                return list.OrderBy(x => x.Order).ToList(); ;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetReportChildsWidoutDA");
                throw ex;
            }
        }

        /// <summary>
        /// بچه های یک گره را با استفاده از آدرس پدران آن برمیگرداند
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public IList<Report> GetReportChildsByParentPath(decimal parentId)
        {
            IList<Report> reportList = reportRep.GetByCriteria(
                new CriteriaStruct(
                    Utility.GetPropertyName(() => new Report().ParentPath)
                    , String.Format(",{0},", parentId)
                    , CriteriaOperation.Like));
            foreach (Report report in reportList)
            {
                if (report.IsReport)
                {
                    report.HasParameter = this.HasReportParameter(report.ReportFile.ID);
                }
            }
            return reportList.OrderBy(x => x.Order).ToList(); ;
        }


        #endregion

        /// <summary>
        /// فایلهای گزارشی که هنوز منتسب به دسته گزارشی نیست را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<ReportFile> GetAllReportFiles() 
        {
            try
            {
                EntityRepository<ReportFile> reportFileReposiory = new EntityRepository<ReportFile>();
                IList<ReportFile> list = reportFileReposiory.GetAll();
                                
                return list;
            }
            catch (Exception ex) 
            {
                LogException(ex, "BReport", "GetAllReportFiles");
                throw ex;
            }
        }

        /// <summary>
        /// فایل گزارش را منتسب میکند
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="reportFile"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertReport(decimal reportGroupId, decimal reportFileId,string reportName) 
        {
            try
            {
                Report report = new Report();
                report.ParentId = reportGroupId;
                report.Name = reportName;
                report.IsReport = true;
                report.ReportFile = new ReportFile() { ID = reportFileId };
                decimal id = this.SaveChanges(report, UIActionType.ADD);
                return id;
            }
            catch (Exception ex) 
            {
                LogException(ex, "BReport", "InsertReport");
                throw ex;
            }
        }

        /// <summary>
        /// نام و فایل گزارش را بروزرسانی میکند
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="reportFile"></param>
        /// <returns></returns>
        public decimal UpdateReport(decimal reportId, decimal reportFileId, string reportName)
        {
            try
            {
                Report report = base.GetByID(reportId);                
                report.Name = reportName;
                report.IsReport = true;
                report.ReportFile = new ReportFile() { ID = reportFileId };
                decimal id = this.SaveChanges(report, UIActionType.EDIT);
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// فایل گزارش که به یک گروه منتسب شده است را آزاد میکند
        /// </summary>
        /// <param name="reportFile"></param>
        /// <returns></returns>
        public decimal DeleteReport(decimal reportId)
        {
            Report report = new Report() { ID = reportId };            
            decimal id = base.SaveChanges(report, UIActionType.DELETE);
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        protected override void InsertValidate(Report report)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(report.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportNameRequierd, "نام گزارش باید مشخص شود", ExceptionSrc));
            }

            if (Utility.IsEmpty(report.ParentId))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportParentIDRequierd, "نام والد گزارش باید مشخص شود", ExceptionSrc));
            }
            else 
            {
                int count = reportRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => report.ID), report.ParentId),
                                             new CriteriaStruct(Utility.GetPropertyName(() => report.IsReport), true));
                if (count != 0) 
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ReportCanNotBeParent, "گزارش نباید بعنوان والد درنظر گرفته شود", ExceptionSrc));
                }
            }

            if (reportRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => report.ID), report.ParentId)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportParentNotExists, "گزارش والدی با این شناسه موجود نمیباشد", ExceptionSrc));
            }

            else if (reportRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => report.Name), report.Name),
                                                                  new CriteriaStruct(Utility.GetPropertyName(() => report.ParentId), report.ParentId)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportRepeatedName, "نام گزارش در یک سطح نباید تکراری باشد", ExceptionSrc));
            }

            if (report.IsReport && (report.ReportFile == null || report.ReportFile.ID == 0)) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportFileNotSpecified, "فایل گزارش انتخاب نشده است", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        protected override void UpdateValidate(Report report)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (!this.IsReportRoot(report.ID)
                && Utility.IsEmpty(report.ParentId))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportParentIDRequierd, "نام والد گزارش باید مشخص شود", ExceptionSrc));
            }

            if (Utility.IsEmpty(report.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportNameRequierd, "نام گزارش باید مشخص شود", ExceptionSrc));
            }

            else if (report.ParentId != 0 &&
                reportRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => report.Name), report.Name),
                                                                  new CriteriaStruct(Utility.GetPropertyName(() => report.ParentId), report.ParentId),
                                                                  new CriteriaStruct(Utility.GetPropertyName(() => report.ID), report.ID, CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportRepeatedName, "نام گزارش در یک سطح نباید تکراری باشد", ExceptionSrc));
            }

            if (report.IsReport && (report.ReportFile == null || report.ReportFile.ID == 0))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportFileNotSpecified, "فایل گزارش انتخاب نشده است", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        protected override void DeleteValidate(Report report)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            int count = reportRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => report.ID), report.ID),
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => report.ParentId), (decimal)0));

            if (count>0)
            {
                exception.Add(ExceptionResourceKeys.ReportRootDeleteIllegal, "ریشه قابل حذف نیست", ExceptionSrc);
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// آیا این گزارش ریشه است؟
        /// این تابع بعلت جلوگیری از اشکال نشست را خالی میکند
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        private bool IsReportRoot(decimal reportId)
        {
            int count = reportRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Report().ParentId), (decimal)0),
                                                     new CriteriaStruct(Utility.GetPropertyName(() => new Report().ID), reportId));
            NHibernateSessionManager.Instance.ClearSession();
            return count > 0;
        }

        protected override void GetReadyBeforeSave(Report report, UIActionType action)
        {
            if (action == UIActionType.ADD && report.ParentId > 0)
            {
                Report parent = base.GetByID(report.ParentId);
                report.ParentPath = parent.ParentPath + String.Format(",{0},", report.ParentId);
            }
            else if (action == UIActionType.EDIT)
            {
                Report node = base.GetByID(report.ID);
                report.ParentPath = node.ParentPath;
                NHibernateSessionManager.Instance.ClearSession();
            }
        }

        /// <summary>
        /// اگر گزارش پارامتر دارد درست برمیکند
        /// </summary>
        /// <param name="reportFileId"></param>
        /// <returns></returns>
        private bool HasReportParameter(decimal reportFileId) 
        {
            EntityRepository<ReportParameter> rep = new EntityRepository<ReportParameter>(false);
            int count = rep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ReportParameter().ReportFile), new ReportFile() { ID = reportFileId }));
            return count > 0;
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckReportsLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertReportGroup(Report report, UIActionType UAT)
        {
            return base.SaveChanges(report, UAT);
        }

        public decimal UpdateReportGroup(Report report, UIActionType UAT)
        {
            return base.SaveChanges(report, UAT);
        }

        public decimal DeleteReportGroup(Report report, UIActionType UAT)
        {
            return base.SaveChanges(report, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUpdateAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckDeleteAccess()
        {
        }

    }

}
