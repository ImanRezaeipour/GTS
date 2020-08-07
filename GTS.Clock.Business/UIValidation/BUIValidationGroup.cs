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
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.UIValidation
{
    /// <summary>
    /// created at: 4/4/2012 12:29:23 PM
    /// by        : Farhad Salvati
    /// write your name here
    /// </summary>
    public class BUIValidationGroup : BaseBusiness<UIValidationGroup>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.UIValidation.BUIValidationGroup";
        private EntityRepository<UIValidationGroup> objectRep = new EntityRepository<UIValidationGroup>();
        private EntityRepository<UIValidationGrouping> groupingRep = new EntityRepository<UIValidationGrouping>();

        /// <summary>
        /// لیست قوانین را برمیگرداند
        /// آگر قبلا قوانین انتساب داده نشده باشند آنها را منتسب میکند
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IList<UIValidationGroupingProxy> GetRuleList(decimal groupId) 
        {
            try 
            {
                IList<UIValidationGroupingProxy> result = new List<UIValidationGroupingProxy>();
                IList<UIValidationRule> ruleList = new EntityRepository<UIValidationRule>(false).GetAll();
                IList<UIValidationGrouping> exsitingGrouping = groupingRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UIValidationGrouping().ValidationGroup), new UIValidationGroup() { ID = groupId }));
                if (exsitingGrouping.Count > 0)
                {
                    foreach (UIValidationGrouping grouping in exsitingGrouping) 
                    {
                        result.Add(new UIValidationGroupingProxy() { ID = grouping.ID, Active = grouping.Active, OpratorRestriction = grouping.OperatorRestriction, RuleID = grouping.RuleID, RuleName = grouping.ValidationRule.Name, GroupID = groupId });
                    }
                }
                else
                {
                    ruleList = ruleList.Where(x => x.Active).ToList();
                    foreach (UIValidationRule rule in ruleList)
                    {
                        UIValidationGrouping grouping = new UIValidationGrouping();
                        grouping.RuleID = rule.ID;
                        grouping.GroupID = groupId;
                        grouping.Active = false;
                        groupingRep.Save(grouping);

                        result.Add(new UIValidationGroupingProxy() { ID = grouping.ID, Active = false, OpratorRestriction = false, RuleID = rule.ID, RuleName = rule.Name, GroupID = groupId });
                    }
                }
                return result;
            }
            catch (Exception ex) 
            {
                LogException(ex, "BUIValidationGroup", "GetRuleList");
                throw ex;
            }
        }

        /// <summary>
        /// اگر پارانتر مقداردهی شده باشد آنرا بارگزاری میکند در غیر این صورت 
        /// از تمپلیت میخواند
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public IList<UIValidationRuleParameter> GetRuleParameter(decimal groupingId, decimal ruleId)
        {
            try
            {
                EntityRepository<UIValidationRuleParameter> ruleParameterRep = new EntityRepository<UIValidationRuleParameter>(false);
                EntityRepository<UIValidationRuleParameterTemplate> tempRep = new EntityRepository<UIValidationRuleParameterTemplate>(false);
                IList<UIValidationRuleParameter> list = ruleParameterRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UIValidationRuleParameter().Grouping), new UIValidationGrouping() { ID = groupingId }));
                if (list.Count > 0)
                {
                    foreach (UIValidationRuleParameter param in list)
                    {
                        if (!Utility.IsEmpty(param.Value))
                        {
                            if (param.Type == RuleParamType.Date)
                            {
                                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                                {
                                    param.TheValue = Utility.ToPersianDate(param.Value);
                                }
                                else
                                {
                                    param.TheValue = param.Value;
                                }
                            }
                            else
                            {
                                param.TheValue = param.Value;
                            }
                        }
                    }
                    return list;
                }
                else
                {
                    IList<UIValidationRuleParameterTemplate> tempParam = tempRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UIValidationRuleParameterTemplate().ValidationRule), new UIValidationRule() { ID = ruleId }));
                    foreach (UIValidationRuleParameterTemplate param in tempParam)
                    {
                        UIValidationRuleParameter ruleParam = new UIValidationRuleParameter();
                        ruleParam.Name = param.Name;
                        ruleParam.Type = param.Type;
                        ruleParam.Value = "";
                        ruleParam.TheValue = "";
                        ruleParam.KeyName = param.KeyName;
                        ruleParam.Grouping = new UIValidationGrouping() { ID = groupingId };
                        ruleParameterRep.Save(ruleParam);
                        list.Add(ruleParam);
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BUIValidationGroup", "GetRuleParameter");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateRuleParameter(IList<UIValidationRuleParameter> parms)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    EntityRepository<UIValidationRuleParameter> ruleParameterRep = new EntityRepository<UIValidationRuleParameter>(false);
                    foreach (UIValidationRuleParameter param in parms)
                    {
                        
                        if (param.Type == RuleParamType.Date)
                        {
                            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                            {
                                param.Value = Utility.ToMildiDateString(param.TheValue);
                            }
                            else
                            {
                                param.Value = param.TheValue;
                            }
                        }
                        else if (param.Type == RuleParamType.Time && param.ContinueOnTomorrow) 
                        {
                            param.Value = Utility.ToString(Utility.ToInteger(param.TheValue) + 1440);
                        }
                        else
                        {
                            param.Value = param.TheValue;
                        }
                        ruleParameterRep.WithoutTransactUpdate(param);
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BUIValidationGroup", "UpdateRuleParameter");
                    throw ex;
                }
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateRuleList(IList<UIValidationGroupingProxy> ruleList) 
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    foreach (UIValidationGroupingProxy row in ruleList)
                    {
                        UIValidationGrouping grouping = new UIValidationGrouping() { ID = row.ID, Active = row.Active, OperatorRestriction = row.OpratorRestriction, RuleID = row.RuleID, GroupID = row.GroupID };
                        groupingRep.WithoutTransactUpdate(grouping);
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BUIValidationGroup", "UpdateRuleList");
                    throw ex;
                }
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUIValidationLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertUIValidationGroup(UIValidationGroup uiValidationGroup, UIActionType UAT)
        {
            return base.SaveChanges(uiValidationGroup, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateUIValidationGroup(UIValidationGroup uiValidationGroup, UIActionType UAT)
        {
            return base.SaveChanges(uiValidationGroup, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteUIValidationGroup(UIValidationGroup uiValidationGroup, UIActionType UAT)
        {
            return base.SaveChanges(uiValidationGroup, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUIValidationRulesLoadAccess()
        {
        }



        #region BaseBusiness Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(UIValidationGroup obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(obj.Name)) 
            {
                exception.Add(ExceptionResourceKeys.ValidationGroupNameIsEmpty, "نام گروه اعتبارسنجی نباید خالی باشد", ExceptionSrc);
            }
            else
            {
                if (objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UIValidationGroup().Name), obj.Name.ToLower())) > 0) 
                {
                    exception.Add(ExceptionResourceKeys.ValidationGroupNameIsRepeated, "نام گروه اعتبارسنجی نباید تکراری باشد", ExceptionSrc);
                }
            }
            //if (obj.GroupingList==null || obj.GroupingList.Count==0 || obj.GroupingList.Where(x => x.RuleID == 0).Count() > 0)
            //{
            //    exception.Add(ExceptionResourceKeys.ValidationGroupRulesIsEmpty, "برای گروه اعتبارسنجی باید قانون انتخاب شود", ExceptionSrc);
            //}

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void UpdateValidate(UIValidationGroup obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(obj.Name))
            {
                exception.Add(ExceptionResourceKeys.ValidationGroupNameIsEmpty, "نام گروه اعتبارسنجی نباید خالی باشد", ExceptionSrc);
            }
            else
            {
                if (objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UIValidationGroup().Name), obj.Name.ToLower()),
                                                 new CriteriaStruct(Utility.GetPropertyName(() => new UIValidationGroup().ID), obj.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(ExceptionResourceKeys.ValidationGroupNameIsRepeated, "نام گروه اعتبارسنجی نباید تکراری باشد", ExceptionSrc);
                }
            }
            //if (obj.GroupingList == null || obj.GroupingList.Count == 0 || obj.GroupingList.Where(x => x.RuleID == 0).Count() > 0)
            //{
            //    exception.Add(ExceptionResourceKeys.ValidationGroupRulesIsEmpty, "برای گروه اعتبارسنجی باید قانون انتخاب شود", ExceptionSrc);
            //}

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(UIValidationGroup obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            PersonRepository rep = new PersonRepository(false);

            if (rep.CheckIsUIValidationGroupInUse(obj))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UIValidationGroupUsedByPerson, "بدلیل استفاده در پرسنل نباید حذف شود", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void GetReadyBeforeSave(UIValidationGroup obj, UIActionType action)
        {
            if (action == UIActionType.ADD || action == UIActionType.EDIT)
            {
                if (obj.GroupingList != null)
                {
                    foreach (UIValidationGrouping g in obj.GroupingList)
                    {
                        g.ValidationGroup = obj;
                    }
                }
            }
        }

        /// <summary>
        /// بعد از بروز رسانی گروه اعتبارسنجی و تخصیص قوانین جدید به آن
        /// باید قوانین قبلی منتسب به آن را حذف شوند.این کار اینجا انجام میگیرد
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        protected override void OnSaveChangesSuccess(UIValidationGroup obj, UIActionType action)
        {
            //if (action == UIActionType.EDIT) 
            //{
            //    UIValidationGroupingRepository rep = new UIValidationGroupingRepository();
            //    var ids = from o in obj.GroupingList
            //              select o.RuleID;
            //    IList<decimal> idList = ids.ToList();
            //    rep.DeleteAfterUpdate(obj.ID, idList);
            //}
        }

        #endregion
    
    }
}
