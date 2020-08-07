using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Business.RuleDesigner
{
    /// <summary>
    /// 
    /// </summary>
    public class BConceptExpression : BaseBusiness<ConceptExpression>
    {
        readonly EntityRepository<ConceptExpression> _cnpRep = new EntityRepository<ConceptExpression>();
        const string ExceptionSrc = "GTS.Clock.Business.RuleDesigner";

        public List<ConceptExpression> GetByParentId(decimal? parentId)
        {
            var items = _cnpRep.GetAll().Where(x => x.Visible == true);
            items = items.Where(x => x.Visible && parentId == null ? x.Parent_ID == null : x.Parent_ID == parentId).ToList();
            return (List<ConceptExpression>)items;

            //if (parentId == null)
            //    return _cnpRep.Find(x => !x.Parent_ID.HasValue).ToList();
            //return _cnpRep.Find(x => x.Parent_ID == parentId).ToList();
        }

        public List<ConceptExpression> GetChildrenOnCreation(decimal parentId)
        {
            var items = _cnpRep.GetAll().Where(x => x.Visible == true);
            items = items.Where(x => x.Visible && x.Parent_ID == parentId && x.CandAddToFinal && x.AddOnParentCreation).ToList();
            return (List<ConceptExpression>)items;

            //if (parentId == null)
            //    return _cnpRep.Find(x => !x.Parent_ID.HasValue).ToList();
            //return _cnpRep.Find(x => x.Parent_ID == parentId).ToList();
        }

        #region Overrides of BaseBusiness<ConceptExpression>

        protected override void InsertValidate(ConceptExpression obj)
        {
            
        }

        protected override void UpdateValidate(ConceptExpression obj)
        {
            
        }

        protected override void DeleteValidate(ConceptExpression obj)
        {
            
        }

        #endregion


        public decimal InsertConceptExpression(ConceptExpression conceptExpression)
        {
            try
            {
                return this.SaveChanges(conceptExpression, UIActionType.ADD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal UpdateConceptExpression(ConceptExpression conceptExpression)
        {
            try
            {
                return this.SaveChanges(conceptExpression, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal DeleteConceptExpression(ConceptExpression conceptExpression)
        {
            try
            {
                return this.SaveChanges(conceptExpression, UIActionType.DELETE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public int GetAllByPageBySearchCount(string searchTerm)
        {
            IEnumerable<ConceptExpression> nn = _cnpRep.GetAll();

            if (nn.FirstOrDefault() == null)
                return 0;

            nn = nn.Where(
               concept =>
                   concept.ScriptBeginFa.Contains(searchTerm)
                   //|| concept.ScriptBeginEn.Contains(searchTerm)
                   );

            if (nn.FirstOrDefault() == null)
                return 0;

            return nn.Count();
        }

        public IList<ConceptExpression> GetAllByPageBySearch(int pageIndex, int pageSize, string searchTerm)
        {
            IEnumerable<ConceptExpression> queryOnConceptExression = null;
            try
            {
                if (string.IsNullOrEmpty(searchTerm.Trim()))
                {
                    queryOnConceptExression = _cnpRep.GetAll();
                }
                else
                {
                    queryOnConceptExression = _cnpRep.GetAll().Where(
                        concept =>
                        concept.ScriptBeginFa.Contains(searchTerm)
                        //|| concept.ScriptBeginEn.Contains(searchTerm)
                        );
                }

                if (queryOnConceptExression.FirstOrDefault() != null
                    //&& queryOnSecondaryConcept.Skip(pageIndex * pageSize).FirstOrDefault() != null
                    )
                {
                    queryOnConceptExression =
                        queryOnConceptExression
                        .Skip(pageIndex * pageSize)
                        .Take(pageSize);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business.RuleDesigner.BConceptExpression", "GetAllByPageBySearch");
                throw ex;
            }
            return queryOnConceptExression.ToList();
        }

        public UIValidationExceptions Validation(string ScriptBeginFa)
        {
            UIValidationExceptions uiValidationExceptions = new UIValidationExceptions();

            if (string.IsNullOrEmpty(ScriptBeginFa))
                uiValidationExceptions.ExceptionList.Add(
                    new ValidationException(ExceptionResourceKeys.BExpressionRequiedScriptBeginFa, "فيلد شروع اسكريپت فارسي نميتواند خالي باشد", ExceptionSrc));
            return uiValidationExceptions;
        }
        
        public void Copy(ConceptExpression conceptExpressionRecived, ref ConceptExpression conceptFromDb)
        {
            conceptFromDb.Parent_ID = conceptExpressionRecived.Parent_ID;
            conceptFromDb.ScriptBeginFa = conceptExpressionRecived.ScriptBeginFa;
            conceptFromDb.ScriptEndFa = conceptExpressionRecived.ScriptEndFa;
            conceptFromDb.ScriptBeginEn = conceptExpressionRecived.ScriptBeginEn;
            conceptFromDb.ScriptEndEn = conceptExpressionRecived.ScriptEndEn;
            conceptFromDb.AddOnParentCreation = conceptExpressionRecived.AddOnParentCreation;
            conceptFromDb.CandAddToFinal = conceptExpressionRecived.CandAddToFinal;
            conceptFromDb.CandEditInFinal = conceptExpressionRecived.CandEditInFinal;
            conceptFromDb.SortOrder = conceptExpressionRecived.SortOrder;
            conceptFromDb.Visible = conceptExpressionRecived.Visible;
        }
    }
}
