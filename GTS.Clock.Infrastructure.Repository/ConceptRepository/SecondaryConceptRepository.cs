using System;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using GTS.Clock.Model;
using NHibernate.Transform;

namespace GTS.Clock.Infrastructure.Repository
{
    public class SecondaryConceptRepository : RepositoryBase<SecondaryConcept>, ISecondaryConceptRepository
    {
        public override string TableName
        {
            get { return "TA_SecondaryConcept"; }
        }
        public SecondaryConceptRepository()
            : base()
        { }

        public SecondaryConceptRepository(bool Disconnectedly)
            :base(Disconnectedly)
        { }

        public SecondaryConcept GetScndCnpWithIdentifier(decimal Identifier)
        {
            //string HQuery = "from ScndCnp sc " +
            //                "where sc.IdentifierCode = :IdenCode";
            //return base.NHibernateSession
            //            .CreateQuery(HQuery)
            //            .SetDecimal("IdenCode", Identifier)
            //            .UniqueResult<SecondaryConcept>();   

            return base.NHibernateSession.QueryOver<SecondaryConcept>()
                                         .Where(x => x.IdentifierCode == Identifier)
                                         .SingleOrDefault();
                     
        }

        public BaseScndCnpValue GetScndCnpWithIndex(string index) 
        {
            //string query = String.Format("select * from TA_SecondaryConceptValue where ScndCnpValue_Index='{0}'", index);

            //IList<BaseScndCnpValue> list = base.NHibernateSession.CreateSQLQuery(query).List<BaseScndCnpValue>();
            //return list.FirstOrDefault();

            return base.NHibernateSession.QueryOver<BaseScndCnpValue>()
                                         .Where(x => x.Index == index)
                                         .SingleOrDefault();
        }

        public IList<PersistedScndCnpPrdValue> GetPeriodicScndCnpValues(decimal PersonId, DateTime FromDate, DateTime ToDate, DateTime FromDateRange, DateTime ToDateRange, decimal CalcRangeGrpId)
        {
            return base.NHibernateSession.GetNamedQuery("GetPersistedScndCnpPeriodicValueList")
                                         .SetParameter("personId", PersonId)
                                         .SetDateTime("fromDate", FromDate)
                                         .SetDateTime("fromDateRange", FromDateRange)
                                         .SetDateTime("toDateRange", ToDateRange)
                                         .SetDecimal("CalcRangeGrpId", CalcRangeGrpId)
                                         .List<PersistedScndCnpPrdValue>();
        
        }

        public IList<SecondaryConcept> GetAllByKeyNames(IList<string> keys)
        {
            String SQLCommand = @"select * from TA_ConceptTemplate
                                    where ConceptTmp_KeyColumnName is not null 
                                    AND ConceptTmp_KeyColumnName <>''
                                    AND ConceptTmp_KeyColumnName in (:keys)";
            IList<SecondaryConcept> result = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                 //.SetResultTransformer(Transformers.AliasToBean(typeof(SecondaryConcept)))
                 .AddEntity(typeof(SecondaryConcept))
                 .SetParameterList("keys", keys.ToArray())
                 .List<SecondaryConcept>();
            return result;
        }



    }
}
