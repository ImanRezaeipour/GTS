using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;

namespace GTS.Clock.Business.Rules
{
    public class BRuleType : BaseBusiness<RuleType>
    {
        const string ExceptionSrc = "GTS.Clock.Business.Rules.BRuleType";       

        /// <summary>
        /// وظیفه این تابع مشخص نمودن "نوع قانون" های است که در لیست ورودی وجود ندارند
        /// </summary>
        /// <param name="ExistsRuleTypes"></param>
        /// <returns></returns>
        private IList<RuleType> GetNotExistsRuleTypes(IList<RuleType> ExistsRuleTypes)
        {
            IList<RuleType> Result = new List<RuleType>();
            if (ExistsRuleTypes
                        .Where(x => x.Name == RuleTypeNames.OverTime.ToString())
                        .FirstOrDefault() == null)
            {
                Result.Add(new RuleType() { Name = RuleTypeNames.OverTime.ToString() });
            }

            if (ExistsRuleTypes
                        .Where(x => x.Name == RuleTypeNames.Mission.ToString())
                        .FirstOrDefault() == null)
            {
                Result.Add(new RuleType() { Name = RuleTypeNames.Mission.ToString() });
            }

            if (ExistsRuleTypes
                  .Where(x => x.Name == RuleTypeNames.Leave.ToString())
                  .FirstOrDefault() == null)
            {
                Result.Add(new RuleType() { Name = RuleTypeNames.Leave.ToString() });
            }

            if (ExistsRuleTypes
                  .Where(x => x.Name == RuleTypeNames.Work.ToString())
                  .FirstOrDefault() == null)
            {
                Result.Add(new RuleType() { Name = RuleTypeNames.Work.ToString() });
            }

            if (ExistsRuleTypes
                  .Where(x => x.Name == RuleTypeNames.Absence.ToString())
                  .FirstOrDefault() == null)
            {
                Result.Add(new RuleType() { Name = RuleTypeNames.Absence.ToString() });
            }

            if (ExistsRuleTypes
                  .Where(x => x.Name == RuleTypeNames.Miscellaneous.ToString())
                  .FirstOrDefault() == null)
            {
                Result.Add(new RuleType() { Name = RuleTypeNames.Miscellaneous.ToString() });
            }

            return Result;
        }

        /// <summary>
        /// وظیفه این تابع حصول اطمینان از وجود هر 6 "نوع قانون" ثابت در پایگاه داده می باشد
        /// </summary>
        private void FixRuleType()
        {
            try
            {
                IList<RuleType> RuleTypes = this.GetAll();
                switch (RuleTypes.Count)
                {
                    case 6: return;
                    case 0:
                        {
                            this.SaveChanges(new RuleType() { Name = RuleTypeNames.OverTime.ToString() }, UIActionType.ADD);
                            this.SaveChanges(new RuleType() { Name = RuleTypeNames.Mission.ToString() }, UIActionType.ADD);
                            this.SaveChanges(new RuleType() { Name = RuleTypeNames.Work.ToString() }, UIActionType.ADD);
                            this.SaveChanges(new RuleType() { Name = RuleTypeNames.Absence.ToString() }, UIActionType.ADD);
                            this.SaveChanges(new RuleType() { Name = RuleTypeNames.Leave.ToString() }, UIActionType.ADD);
                            this.SaveChanges(new RuleType() { Name = RuleTypeNames.Miscellaneous.ToString() }, UIActionType.ADD);
                            if (this.GetAll().Count > 6)
                            {
                                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.RuleCategoryRootMoreThanOne, "تعداد گره های نوع قانون در دیتابیس نامعتبر است", ExceptionSrc);
                            }
                            break;
                        }
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        {
                            foreach (RuleType item in GetNotExistsRuleTypes(RuleTypes))
                            {
                                this.SaveChanges(item, UIActionType.ADD);
                            }
                            if (this.GetAll().Count > 6)
                            {
                                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.RuleCategoryRootMoreThanOne, "تعداد گره های نوع قانون در دیتابیس نامعتبر است", ExceptionSrc);
                            }
                            break;
                        }
                    default:
                        {
                            throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.RuleCategoryRootMoreThanOne, "تعداد گره های نوع قانون در دیتابیس نامعتبر است", ExceptionSrc);
                        }
                }
            }
            catch (Exception ex) 
            {
                LogException(ex, "BRuleType", "FixRuleType");
                throw ex;
            }
        }

        public override IList<RuleType> GetAll()
        {
            try
            {
                IList<RuleType> list = base.GetAll();
                if (list.Count != 6)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.RuleCategoryRootMoreThanOne, "تعداد گره های نوع قانون در دیتابیس نامعتبر است", ExceptionSrc);
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRuleType", "GetAll");
                throw ex;
            }
        }

        protected override void InsertValidate(RuleType obj)
        {
            return;
        }

        protected override void UpdateValidate(RuleType obj)
        {
            return;
        }

        protected override void DeleteValidate(RuleType obj)
        {
            return;
        }
    }
}
