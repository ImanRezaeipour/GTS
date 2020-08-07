using System;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using System.Linq;
using System.Collections.Generic;

namespace GTS.Clock.Infrastructure.Repository
{
    public class CalculationDateRangeRepository : RepositoryBase<CalculationDateRange>, ICalculationDateRangeRepository
    {
        public override string TableName
        {
            get { return "TA_CalculationDateRange"; }
        }
        public CalculationDateRangeRepository(bool disconectly)
            : base(disconectly)
        { }

        public IList<CalculationDateRange> GetCalculationDateRanges(CalculationRangeGroup calculationGroup, IList<SecondaryConcept> concepts)
        {
            IList<CalculationDateRange> list = base.NHibernateSession.CreateCriteria(typeof(CalculationDateRange))
            .Add(Expression.Eq("RangeGroup", calculationGroup))
            .Add(Expression.In("Concept", concepts.ToArray()))
            .List<CalculationDateRange>();
            return list;
        }

        public void UpdateCFP(decimal personId, DateTime date) 
        {
            IQuery query = NHibernateSession.CreateSQLQuery("exec spr_UpdateCFP :personId,:date")
                .SetParameter("personId", personId)
                .SetParameter("date", date);
            query.UniqueResult();
        }

        public void InvalidTraficAndPermit(decimal personId, DateTime date)
        {
            string SQLComand = @"
	                            UPDATE TA_BaseTraffic
	                            SET BasicTraffic_Used = 0
	                            WHERE BasicTraffic_PersonID	= :personId
			                            AND
                                BasicTraffic_Date >= :date ;
                                
                                DELETE FROM TA_ProceedTraffic
	                            WHERE ProceedTraffic_PersonId = :personId
                                AND
                                ProceedTraffic_FromDate >= :date ;				                			                    
			  		
	                            UPDATE TA_PermitPair
	                            SET PermitPair_IsApplyedOnTraffic = 0
	                            WHERE Permitpair_PermitId in (SELECT Permit_ID FROM TA_Permit
								                              WHERE Permit_PersonId = :personId
											                            AND 
										                            Permit_FromDate >= :date);";
            IQuery query = NHibernateSession.CreateSQLQuery(SQLComand)
                .SetParameter("personId", personId)
                .SetParameter("date", date);
            query.UniqueResult();
        }


       

    }
}
