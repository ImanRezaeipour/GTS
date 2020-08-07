using GTS.Clock.Infrastructure;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model.Concepts;


namespace GTS.Clock.Business.RequestFlow
{
    public class BImperativeRequest : BaseBusiness<ImperativeRequest>
    {
        const string ExceptionSrc = "GTS.Clock.Business.BImperativeRequest";
        ImperativeRequestRepository imperativeRequestRepository = new ImperativeRequestRepository();
        BPerson personBusiness = new BPerson();
        PersonRepository pesronReppsitory = new PersonRepository();
        ManagerRepository managerRepository = new ManagerRepository(false);

        protected override void InsertValidate(ImperativeRequest obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if (obj.Precard == null || obj.Precard.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RequestPrecardIsEmpty, "پیشکارت نباید خالی باشد", ExceptionSrc));
            }
            if (obj.Year == null || obj.Year == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RequestYearIsEmpty, "سال نباید خالی باشد", ExceptionSrc));
            }
            if (obj.Month == null || obj.Month == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RequestMonthIsEmpty, "ماه نباید خالی باشد", ExceptionSrc));
            }
            if (exception.Count > 0)
                throw exception;
        }

        protected override void UpdateValidate(ImperativeRequest obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if (obj.Precard == null || obj.Precard.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RequestPrecardIsEmpty, "پیشکارت نباید خالی باشد", ExceptionSrc));
            }
            if (obj.Year == null || obj.Year == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RequestYearIsEmpty, "سال نباید خالی باشد", ExceptionSrc));
            }
            if (obj.Month == null || obj.Month == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RequestMonthIsEmpty, "ماه نباید خالی باشد", ExceptionSrc));
            }
            if (exception.Count > 0)
                throw exception;
        }

        protected override void DeleteValidate(ImperativeRequest obj)
        {
        }

        public int GetAdvancedSearchPersonCountByImperativeRequest(PersonAdvanceSearchProxy proxy, ImperativeRequestLoadState IRLS, ImperativeRequest imperativeRequest, PersonCategory searchInCategory)
        {
            try
            {

                BApplicationSettings.CheckGTSLicense();

                #region Date Convert
                if (!Utility.IsEmpty(proxy.FromBirthDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.FromBirthDate = Utility.ToMildiDateString(proxy.FromBirthDate);
                }
                if (!Utility.IsEmpty(proxy.ToBirthDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.ToBirthDate = Utility.ToMildiDateString(proxy.ToBirthDate);
                }
                if (!Utility.IsEmpty(proxy.FromEmploymentDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.FromEmploymentDate = Utility.ToMildiDateString(proxy.FromEmploymentDate);
                }
                if (!Utility.IsEmpty(proxy.ToEmploymentDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.ToEmploymentDate = Utility.ToMildiDateString(proxy.ToEmploymentDate);
                }
                if (!Utility.IsEmpty(proxy.WorkGroupFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.WorkGroupFromDate = Utility.ToMildiDateString(proxy.WorkGroupFromDate);
                }
                if (!Utility.IsEmpty(proxy.RuleGroupFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.RuleGroupFromDate = Utility.ToMildiDateString(proxy.RuleGroupFromDate);
                }
                if (!Utility.IsEmpty(proxy.RuleGroupToDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.RuleGroupToDate = Utility.ToMildiDateString(proxy.RuleGroupToDate);
                }
                if (!Utility.IsEmpty(proxy.CalculationFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.CalculationFromDate = Utility.ToMildiDateString(proxy.CalculationFromDate);
                }
                #endregion

                decimal managerId = this.personBusiness.GetCurentManagerId(ref searchInCategory);
                int count = this.imperativeRequestRepository.GetAdvancedSearchPersonCountByImperativeRequest(proxy, IRLS, imperativeRequest, BUser.CurrentUser.ID, managerId, searchInCategory);
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BImperativeRequest", "GetAdvancedSearchPersonCountByImperativeRequest");
                throw ex;
            }
        }

        public IList<ImperativeUndermanagementInfoProxy> GetAdvancedSearchPersonByImperativeRequest(PersonAdvanceSearchProxy proxy, ImperativeRequestLoadState IRLS, ImperativeRequest imperativeRequest, PersonCategory searchInCategory, int pageIndex, int pageSize)
        {
            try
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                if (imperativeRequest.Precard == null || imperativeRequest.Precard.ID == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestPrecardIsEmpty, "پیشکارت نباید خالی باشد", ExceptionSrc));
                }
                if (imperativeRequest.Year == null || imperativeRequest.Year == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestYearIsEmpty, "سال نباید خالی باشد", ExceptionSrc));
                }
                if (imperativeRequest.Month == null || imperativeRequest.Month == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestMonthIsEmpty, "ماه نباید خالی باشد", ExceptionSrc));
                }

                if (exception.Count == 0)
                {
                    BApplicationSettings.CheckGTSLicense();

                    #region Date Convert
                    if (!Utility.IsEmpty(proxy.FromBirthDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.FromBirthDate = Utility.ToMildiDateString(proxy.FromBirthDate);
                    }
                    if (!Utility.IsEmpty(proxy.ToBirthDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.ToBirthDate = Utility.ToMildiDateString(proxy.ToBirthDate);
                    }
                    if (!Utility.IsEmpty(proxy.FromEmploymentDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.FromEmploymentDate = Utility.ToMildiDateString(proxy.FromEmploymentDate);
                    }
                    if (!Utility.IsEmpty(proxy.ToEmploymentDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.ToEmploymentDate = Utility.ToMildiDateString(proxy.ToEmploymentDate);
                    }
                    if (!Utility.IsEmpty(proxy.WorkGroupFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.WorkGroupFromDate = Utility.ToMildiDateString(proxy.WorkGroupFromDate);
                    }
                    if (!Utility.IsEmpty(proxy.RuleGroupFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.RuleGroupFromDate = Utility.ToMildiDateString(proxy.RuleGroupFromDate);
                    }
                    if (!Utility.IsEmpty(proxy.RuleGroupToDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.RuleGroupToDate = Utility.ToMildiDateString(proxy.RuleGroupToDate);
                    }
                    if (!Utility.IsEmpty(proxy.CalculationFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.CalculationFromDate = Utility.ToMildiDateString(proxy.CalculationFromDate);
                    }
                    #endregion

                    IList<Person> list;
                    if (!Utility.IsEmpty(proxy.PersonId))
                    {
                        list = new List<Person>();
                        Person prs = this.personBusiness.GetByID((decimal)proxy.PersonId);
                        list.Add(prs);
                    }
                    else
                    {
                        decimal managerId = personBusiness.GetCurentManagerId(ref searchInCategory);
                        list = this.imperativeRequestRepository.GetAdvancedSearchPersonByImperativeRequest(proxy, IRLS, imperativeRequest, BUser.CurrentUser.ID, managerId, searchInCategory, pageIndex, pageSize);
                    }
                    return this.ConvertToImperativeUndermanagementInfoProxy(list, imperativeRequest, pageSize, pageIndex);
                }
                else
                    throw exception;
            }
            catch (Exception ex)
            {
                LogException(ex, "BImperativeRequest", "GetAdvancedSearchPersonByImperativeRequest");
                throw ex;
            }
        }

        public int GetQuickSearchPersonCountByImperativeRequest(string key, ImperativeRequestLoadState IRLS, ImperativeRequest imperativeRequest, PersonCategory searchCat)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();

                IList<Person> personList = new List<Person>();

                key = key == null ? String.Empty : key;
                key = key.Trim();
                decimal managerId = this.personBusiness.GetCurentManagerId(ref searchCat);
                int count = this.imperativeRequestRepository.GetQuickSearchPersonCountByImperativeRequest(key, IRLS, imperativeRequest, BUser.CurrentUser.ID, managerId, searchCat);
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BImperativeRequest", "GetQuickSearchPersonCountByImperativeRequest");
                throw ex;
            }
        }

        public IList<ImperativeUndermanagementInfoProxy> GetQuickSearchPersonByImperativeRequest(string searchValue, ImperativeRequestLoadState IRLS, ImperativeRequest imperativeRequest, PersonCategory searchInCategory, int pageIndex, int pageSize)
        {
            try
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                if (imperativeRequest.Precard == null || imperativeRequest.Precard.ID == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestPrecardIsEmpty, "پیشکارت نباید خالی باشد", ExceptionSrc));
                }
                if (imperativeRequest.Year == null || imperativeRequest.Year == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestYearIsEmpty, "سال نباید خالی باشد", ExceptionSrc));
                }
                if (imperativeRequest.Month == null || imperativeRequest.Month == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestMonthIsEmpty, "ماه نباید خالی باشد", ExceptionSrc));
                }

                if (exception.Count == 0)
                {
                    BApplicationSettings.CheckGTSLicense();

                    IList<Person> personList = new List<Person>();
                    ISearchPerson searchTool = new BPerson();
                    searchValue = searchValue == null ? String.Empty : searchValue;
                    searchValue = searchValue.Trim();
                    int count = searchTool.GetPersonInQuickSearchCount(searchValue, searchInCategory);
                    if (pageSize * pageIndex == 0 || pageSize * pageIndex < count)
                    {
                        decimal managerId = this.personBusiness.GetCurentManagerId(ref searchInCategory);
                        IList<Person> result = this.imperativeRequestRepository.GetQuickSearchPersonByImperativeRequest(searchValue, IRLS, imperativeRequest, BUser.CurrentUser.ID, managerId, searchInCategory, pageSize, pageIndex);
                        return this.ConvertToImperativeUndermanagementInfoProxy(result, imperativeRequest, pageSize, pageIndex);
                    }
                    else
                    {
                        throw new OutOfExpectedRangeException("0", Convert.ToString(count - 1), Convert.ToString(pageSize * (pageIndex + 1)), ExceptionSrc + "GetQuickSearchPersonByImperativeRequest");
                    }
                }
                else
                    throw exception;
            }
            catch (Exception ex)
            {
                LogException(ex, "BImperativeRequest", "GetQuickSearchPersonByImperativeRequest");
                throw ex;
            }
        }

        private IList<ImperativeUndermanagementInfoProxy> ConvertToImperativeUndermanagementInfoProxy(IList<Person> personList, ImperativeRequest imperativeRequest, int pageSize, int pageIndex)
        {
            IList<ImperativeUndermanagementInfoProxy> imperativeUndermanagementInfoProxyList = new List<ImperativeUndermanagementInfoProxy>();
            IList<UnderManagementPerson> underManagementPersonList = new List<UnderManagementPerson>();

            DateTime date = DateTime.Now;
            switch (BLanguage.CurrentSystemLanguage)
            {
                case LanguagesName.Parsi:
                    date = Utility.ToMildiDate(String.Format("{0}/{1}/1", imperativeRequest.Year, imperativeRequest.Month));
                    break;
                case LanguagesName.English:
                    date = new DateTime(imperativeRequest.Year, imperativeRequest.Month, 1);
                    break;
            }
            underManagementPersonList = managerRepository.GetUnderManagment(imperativeRequest.Month, imperativeRequest.Month > 0 ? 0 : Utility.ToDateRangeIndex(date, BLanguage.CurrentSystemLanguage), date.ToString("yyyy/MM/dd"), personList.Select(person => person.ID).ToList<decimal>(), pageIndex, pageSize);

            foreach (UnderManagementPerson underManagementPersonItem in underManagementPersonList)
            {
                ImperativeUndermanagementInfoProxy imperativeUndermanagementInfoProxy = new ImperativeUndermanagementInfoProxy();
                imperativeUndermanagementInfoProxy.PersonID = underManagementPersonItem.PersonId;
                imperativeUndermanagementInfoProxy.PersonName = underManagementPersonItem.PersonName;
                imperativeUndermanagementInfoProxy.PersonCode = underManagementPersonItem.BarCode;
                imperativeUndermanagementInfoProxy.PersonImage = this.personBusiness.GetByID(underManagementPersonItem.PersonId).PersonDetail.Image;
                imperativeRequest.Person = new Person() { ID = underManagementPersonItem.PersonId };
                ImperativeRequest impReq = this.imperativeRequestRepository.GetImperativeRequest(imperativeRequest);
                if (impReq != null)
                {
                    imperativeUndermanagementInfoProxy.ImperativeValue = impReq.Value;
                    imperativeUndermanagementInfoProxy.IsLockedImperative = impReq.IsLocked;
                    imperativeUndermanagementInfoProxy.ImperativeDescription = impReq.Description;
                }
                else
                {
                    imperativeUndermanagementInfoProxy.ImperativeValue = 0;
                    imperativeUndermanagementInfoProxy.IsLockedImperative = false;
                    imperativeUndermanagementInfoProxy.ImperativeDescription = string.Empty;
                }
                imperativeUndermanagementInfoProxy.CalcInfo = new CalcInfoProxy()
                {
                    DailyAbsence = underManagementPersonItem.DailyAbsence,
                    DailyLeave = underManagementPersonItem.DailyMeritoriouslyLeave,
                    HourlyAbsence = underManagementPersonItem.HourlyUnallowableAbsence,
                    HourlyLeave = underManagementPersonItem.HourlyMeritoriouslyLeave,
                    OverTime = underManagementPersonItem.AllowableOverTime
                };
                imperativeUndermanagementInfoProxyList.Add(imperativeUndermanagementInfoProxy);
            }
            return imperativeUndermanagementInfoProxyList;
        }

        public void UpdateImperativeCollectiveRequest(ImperativeRequest imperativeRequest, IList<decimal> PersonIDsList)
        {
            try
            {
                ImperativeRequest impReq = null;
                foreach (decimal personID in PersonIDsList)
                {
                    using (NHibernateSessionManager.Instance.BeginTransactionOn())
                    {
                        ImperativeRequest tempImpReq = (ImperativeRequest)imperativeRequest.Clone();
                        tempImpReq.Person = new Person() { ID = personID };
                        impReq = this.imperativeRequestRepository.GetImperativeRequest(tempImpReq);
                        if (impReq != null && !impReq.IsLocked)
                        {
                            impReq.Value = imperativeRequest.Value;
                            impReq.Description = imperativeRequest.Description;
                            base.SaveChanges(impReq, UIActionType.EDIT);
                        }
                        else
                            if (impReq == null)
                                base.SaveChanges(tempImpReq, UIActionType.ADD);
                        NHibernateSessionManager.Instance.CommitTransactionOn();
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BImperativeRequest", "GetQuickSearchPersonCountByImperativeRequest");
                throw ex;
            }
        }

        public ImperativeRequest GetImperativeRequest(ImperativeRequest imperativeRequest)
        {
            try
            {
                return imperativeRequestRepository.GetImperativeRequest(imperativeRequest);
            }
            catch (Exception ex)
            {
                LogException(ex, "BImperativeRequest", "GetImperativeRequest");
                throw ex;
            }
        }


    }
}
