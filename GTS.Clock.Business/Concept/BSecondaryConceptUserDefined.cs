using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model.Security;
using GTS.Clock.Infrastructure.Exceptions.UI;

namespace GTS.Clock.Business.RuleDesigner
{
    public class BSecondaryConceptUserDefined : BaseBusiness<SecondaryConcept>
    {
        readonly EntityRepository<SecondaryConcept> _cnpRep = new EntityRepository<SecondaryConcept>();
        const string ExceptionSrc = "GTS.Clock.Business.RuleDesigner";

        public int GetAllByPageBySearchCount(string searchTerm)
        {
            var count = 0;

            IEnumerable<SecondaryConcept> queryOnSecondaryConcept = null;

            try
            {
                if (string.IsNullOrEmpty(searchTerm.Trim()))
                {
                    queryOnSecondaryConcept =
                        _cnpRep.Find(concept =>
                        concept.UserDefined);
                }
                else
                {
                    var allSecondaryConcept =
                          _cnpRep.GetAll();

                    queryOnSecondaryConcept = allSecondaryConcept.Where(
                        concept =>
                              concept.UserDefined &&
                              (
                                concept.Name.Contains(searchTerm) ||
                                concept.IdentifierCode.ToString().Contains(searchTerm)
                              ));
                }

                count = 0;
                if (queryOnSecondaryConcept.FirstOrDefault() != null)
                    count = queryOnSecondaryConcept.Count();
            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business.RuleDesigner.MConceptTemplate", "GetAllByPageBySearchCount(ConceptSearchKeys searchKey, string searchTerm)");
                throw ex;
            }
            return count;
        }
        public IList<SecondaryConcept> GetAllByPageBySearch(int pageIndex, int pageSize, string searchTerm)
        {
            IEnumerable<SecondaryConcept> queryOnSecondaryConcept = null;
            try
            {
                if (string.IsNullOrEmpty(searchTerm.Trim()))
                {
                    queryOnSecondaryConcept =
                        _cnpRep.Find(concept =>
                        concept.UserDefined);
                }
                else
                {
                  var  allSecondaryConcept =
                        _cnpRep.GetAll();

                  queryOnSecondaryConcept = allSecondaryConcept.Where(
                      concept =>
                            concept.UserDefined &&
                            (
                              concept.Name.Contains(searchTerm) ||
                              concept.IdentifierCode.ToString().Contains(searchTerm)
                            ));
                }

                if (queryOnSecondaryConcept.FirstOrDefault() != null
                    )
                {
                    queryOnSecondaryConcept =
                        queryOnSecondaryConcept
                        .Skip(pageIndex * pageSize)
                        .Take(pageSize);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business.RuleDesigner.MConceptTemplate", "GetAllByPageBySearch");
                throw ex;
            }
            return queryOnSecondaryConcept.ToList();
        }


        public IList<SecondaryConcept> GetAllPeriodicByPageBySearch(int pageIndex, int pageSize, string searchTerm)
        {
            IEnumerable<SecondaryConcept> queryOnSecondaryConcept = null;
            try
            {
                if (string.IsNullOrEmpty(searchTerm.Trim()))
                {
                    queryOnSecondaryConcept =
                        _cnpRep.Find(concept =>
                        concept.UserDefined &&
                        concept.PeriodicType == ScndCnpPeriodicType.Periodic);
                }
                else
                {
                    var allSecondaryConcept =
                          _cnpRep.GetAll();

                    queryOnSecondaryConcept = allSecondaryConcept.Where(
                        concept =>
                              concept.UserDefined &&
                              concept.PeriodicType == ScndCnpPeriodicType.Periodic &&
                              (
                                concept.Name.Contains(searchTerm) ||
                                concept.IdentifierCode.ToString().Contains(searchTerm)
                              ));
                }

                if (queryOnSecondaryConcept.FirstOrDefault() != null
                    )
                {
                    queryOnSecondaryConcept =
                        queryOnSecondaryConcept
                        .Skip(pageIndex * pageSize)
                        .Take(pageSize);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business.RuleDesigner.MConceptTemplate", "GetAllByPageBySearch");
                throw ex;
            }
            return queryOnSecondaryConcept.ToList();
        }

        #region Overrided Methods
        protected override void InsertValidate(SecondaryConcept obj)
        {
            GeneralValidation(obj);

            UIValidationExceptions exception = new UIValidationExceptions();

            if (_cnpRep.GetAll().Any(x => x.Name.ToUpper().Equals(obj.Name.ToUpper())))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptCodeRepeated, "نام تكراري است", ExceptionSrc);

            if (_cnpRep.GetAll().Any(x => x.IdentifierCode.Equals(obj.IdentifierCode)))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptCodeRepeated, "كد تكراري است", ExceptionSrc);

            if (_cnpRep.GetAll().Where(x => x.Color == obj.Color).FirstOrDefault() != null)
                exception.Add(ExceptionResourceKeys.BSecondaryConceptCodeRepeated, "رنگ تكراري است", ExceptionSrc);

            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        protected override void UpdateValidate(SecondaryConcept obj)
        {
            GeneralValidation(obj);

            UIValidationExceptions exception = new UIValidationExceptions();

            if (_cnpRep.GetAll().Any(x => x.ID != obj.ID && x.Name.ToUpper().Equals(obj.Name.ToUpper())))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptCodeRepeated, "نام تكراري است", ExceptionSrc);

            if (_cnpRep.GetAll().Any(x => x.ID != obj.ID && x.IdentifierCode.Equals(obj.IdentifierCode)))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptCodeRepeated, "كد تكراري است", ExceptionSrc);

            if (_cnpRep.GetAll().Where(x => x.ID != obj.ID && x.Color == obj.Color).FirstOrDefault() != null)
                exception.Add(ExceptionResourceKeys.BSecondaryConceptCodeRepeated, "رنگ تكراري است", ExceptionSrc);

            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        protected override void DeleteValidate(SecondaryConcept obj)
        {

        }

        private void GeneralValidation(SecondaryConcept obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (string.IsNullOrEmpty(obj.Name))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptNameRepeated, "نام اجباري است", ExceptionSrc);

            if (obj.IdentifierCode < 1)
                exception.Add(ExceptionResourceKeys.BSecondaryConceptCodeRequierd, "كد اجباري است", ExceptionSrc);

            if (string.IsNullOrEmpty(obj.Color))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptNameRepeated, "رنگ اجباري است", ExceptionSrc);

            if (IsPeriodicTypeEmpty(obj))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptPeriodicTypeRequierd, "نوع مفهوم اجباري است", ExceptionSrc);

            if (IsTypeEmpty(obj))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptPeriodicTypeRequierd, "جنس مفهوم اجباري است", ExceptionSrc);

            if (IsCalcSituationTypeEmpty(obj))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptPeriodicTypeRequierd, "زمان اجرا اجباري است", ExceptionSrc);

            if (IsPersistSituationTypeEmpty(obj))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptPeriodicTypeRequierd, "نوع ذخيره‌سازي اجباري است", ExceptionSrc);

            if (exception.Count > 0)
            {
                throw exception;
            }

        }

        #endregion

        #region Validation Methods

        public UIValidationExceptions SecondaryConceptEnumJsonObjectsValidation(
            string PeriodicTypeString,
            string TypeString,
            string CalcSituationTypeString,
            string PersistSituationTypeString
            )
        {

            UIValidationExceptions uiValidationExceptions = new UIValidationExceptions();

            if (string.IsNullOrEmpty(PeriodicTypeString) || PeriodicTypeString == "-1")
                uiValidationExceptions.ExceptionList.Add(
                    new ValidationException(ExceptionResourceKeys.BSecondaryConceptPeriodicTypeRequierd, "فيلد  نوع نميتواند خالي باشد", ExceptionSrc));

            if (string.IsNullOrEmpty(TypeString) || TypeString == "-1")
                uiValidationExceptions.ExceptionList.Add(
                    new ValidationException(ExceptionResourceKeys.BSecondaryConceptTypeRequierd, "فيلد  جنس نميتواند خالي باشد", ExceptionSrc));

            if (string.IsNullOrEmpty(CalcSituationTypeString) || CalcSituationTypeString == "-1")
                uiValidationExceptions.ExceptionList.Add(
                    new ValidationException(ExceptionResourceKeys.BSecondaryConceptCalcSituationTypeRequierd, "فيلد  زمان اجرا نميتواند خالي باشد", ExceptionSrc));

            if (string.IsNullOrEmpty(PersistSituationTypeString) || PersistSituationTypeString == "-1")
                uiValidationExceptions.ExceptionList.Add(
                    new ValidationException(ExceptionResourceKeys.BSecondaryConceptPersistSituationTypeRequierd, "فيلد  نحوه ذخيره سازي نميتواند خالي باشد", ExceptionSrc));

            return uiValidationExceptions;
        }

        public UIValidationExceptions SecondaryConceptJsonObjectValidation(string ConceptString)
        {
            UIValidationExceptions uiValidationExceptions = new UIValidationExceptions();

            if (string.IsNullOrEmpty(ConceptString))
                uiValidationExceptions.ExceptionList.Add(
                    new ValidationException(ExceptionResourceKeys.BSecondaryConceptRequierd, "مفهومي انتخابي بدرستي پر نشده است", ExceptionSrc));

            try
            {
                Newtonsoft.Json.JsonConvert.DeserializeObject<SecondaryConcept>(ConceptString);
            }
            catch (Exception ex)
            {
                uiValidationExceptions.ExceptionList.Add(
                    new ValidationException(ExceptionResourceKeys.BSecondaryConceptRequierd, "مفهومي انتخابي بدرستي پر نشده است", ExceptionSrc));
            }

            return uiValidationExceptions;
        }

        public bool IsPeriodicTypeEmpty(SecondaryConcept secondaryConcept)
        {
            return secondaryConcept.PeriodicType == null;
        }
        public bool IsTypeEmpty(SecondaryConcept secondaryConcept)
        {
            return secondaryConcept.Type == null;
        }
        public bool IsCalcSituationTypeEmpty(SecondaryConcept secondaryConcept)
        {
            return secondaryConcept.CalcSituationType == null;
        }
        public bool IsPersistSituationTypeEmpty(SecondaryConcept secondaryConcept)
        {
            return secondaryConcept.PersistSituationType == null;
        }

        #endregion

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckConceptManagementLoadAccess()
        {
        }

        public decimal InsertConcept(SecondaryConcept secondaryConcept)
        {
            try
            {
                return this.SaveChanges(secondaryConcept, UIActionType.ADD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal UpdateConcept(SecondaryConcept secondaryConcept)
        {
            try
            {
                return this.SaveChanges(secondaryConcept, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal DeleteConcept(SecondaryConcept secondaryConcept)
        {
            try
            {
                return this.SaveChanges(secondaryConcept, UIActionType.DELETE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public decimal GetIdFromConceptJsonObject(string ConceptString)
        {
            Newtonsoft.Json.Linq.JObject cnpJObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(ConceptString);

            if (cnpJObject.HasValues && (decimal)cnpJObject.GetValue("ID") > 0)
            {
                return (decimal)cnpJObject.GetValue("ID");
            }

            return 0;

        }

        public void Copy(SecondaryConcept secondaryConceptFrom, ref SecondaryConcept secondaryConceptTo)
        {
            secondaryConceptTo.IdentifierCode = secondaryConceptFrom.IdentifierCode;
            secondaryConceptTo.Name = secondaryConceptFrom.Name;
            secondaryConceptTo.Script = secondaryConceptFrom.Script;
            secondaryConceptTo.Color = secondaryConceptFrom.Color;
            secondaryConceptTo.KeyColumnName = secondaryConceptFrom.KeyColumnName;
            secondaryConceptTo.CSharpCode = secondaryConceptFrom.CSharpCode;
            secondaryConceptTo.Script = secondaryConceptFrom.Script;
            secondaryConceptTo.PeriodicType = secondaryConceptFrom.PeriodicType;
            secondaryConceptTo.Type = secondaryConceptFrom.Type;
            secondaryConceptTo.CalcSituationType = secondaryConceptFrom.CalcSituationType;
            secondaryConceptTo.PersistSituationType = secondaryConceptFrom.PersistSituationType;
            secondaryConceptTo.UserDefined = secondaryConceptFrom.UserDefined;
            secondaryConceptTo.CSharpCode = secondaryConceptFrom.CSharpCode;
            secondaryConceptTo.JsonObject = secondaryConceptFrom.JsonObject;
        }

    }
}