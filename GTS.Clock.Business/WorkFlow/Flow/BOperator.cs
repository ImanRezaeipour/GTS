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
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.RequestFlow
{
    /// <summary>
    /// created at: 2012-01-01 2:07:57 PM
    /// by        : Farhad Salavati
    /// write your name here
    /// </summary>
    public class BOperator : BaseBusiness<Operator>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.WorkFlow.BOperator";
        private OperatorRepository objectRep = new OperatorRepository(false);

        /// <summary>
        /// آیا این شخص اپراتور است
        ///  اگر هست اپراتور را برگردان
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public IList<Operator> GetOperator(decimal personId) 
        {
            try
            {
                IList<Operator> op = objectRep.GetOperator(personId);                
                return op;
            }
            catch (Exception ex)
            {
                LogException(ex, "BOperator", "GetOperator");
                throw ex;
            }
        }

        /// <summary>
        /// لیست اپراتورهای یک جریان را برمیگرداند
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<Operator> GetAllByFlowId(decimal flowId)
        {
            IList<Operator> list = objectRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Operator().Flow), new Flow() { ID = flowId }));
            return list;
        }

        /// <summary>
        /// آیا کاربر فعلی اپراتور است
        /// </summary>
        /// <returns></returns>
        public bool IsOperator() 
        {
            IList<Operator> opList = this.GetOperator(BUser.CurrentUser.Person.ID);
            return opList != null && opList.Count > 0 ? true : false;
        }

        #region BaseBusiness Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(Operator opr)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (opr.Person == null || opr.Person.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OperatorPersonIsRequierd, "پرسنل برای اپراتور انتخاب نشده است", ExceptionSrc));
            }
            else if (objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Operator().Flow), opr.Flow),
                                                  new CriteriaStruct(Utility.GetPropertyName(() => new Operator().Person), opr.Person)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OperatorPersonIsRequierd, " اپراتور تکراری است", ExceptionSrc));
            }
            if (opr.Flow == null || opr.Flow.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OperatorPersonIsRequierd, "جریان کاری برای اپراتور انتخاب نشده است", ExceptionSrc));
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
        protected override void UpdateValidate(Operator opr)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
           
            if (opr.Person == null || opr.Person.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OperatorPersonIsRequierd, "پرسنل برای اپراتور انتخاب نشده است", ExceptionSrc));
            }
            else if (objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Operator().Flow), opr.Flow),
                                                  new CriteriaStruct(Utility.GetPropertyName(() => new Operator().Person), opr.Person),
                                                  new CriteriaStruct(Utility.GetPropertyName(() => new Operator().ID), opr.ID, CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OperatorPersonIsRequierd, " اپراتور تکراری است", ExceptionSrc));
            }
            if (opr.Flow == null || opr.Flow.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OperatorPersonIsRequierd, "جریان کاری برای اپراتور انتخاب نشده است", ExceptionSrc));
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
        protected override void DeleteValidate(Operator opr)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void OnSaveChangesSuccess(Operator opr, UIActionType action)
        {
            if (action == UIActionType.ADD || action == UIActionType.EDIT) 
            {
                new BRole().SetUserRole(opr.Person, RoleCustomCode.Operator);
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckOperatorsLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertOperator(Operator flowOperator, UIActionType UAT)
        {
            return base.SaveChanges(flowOperator, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteOperator(Operator flowOperator, UIActionType UAT)
        {
            return base.SaveChanges(flowOperator, UAT);
        }


        #endregion
    }
}
