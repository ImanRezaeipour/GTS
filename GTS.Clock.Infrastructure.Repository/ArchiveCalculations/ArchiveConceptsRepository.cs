using System;
using System.Collections.Generic;
using System.Collections;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using GTS.Clock.Model;
using System.Linq;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using NHibernate.SqlTypes;
using NHibernate.Type;
using GTS.Clock.Model.MonthlyReport;
using NHibernate.Transform;

namespace GTS.Clock.Infrastructure.Repository
{
    public class ArchiveConceptsRepository : RepositoryBase<ArchiveConceptValue>
    {
        public override string TableName
        {
            get { return ""; }
        }

        public ArchiveConceptsRepository()
            : base()
        { }

        public ArchiveConceptsRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PersonId"></param>
        /// <param name="Date"></param>
        /// <param name="OrderIndex"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        public IList<ScndCnpValue> LoadDailyScndCnpList(decimal PersonId, DateTime Date, int Order)
        {
            return NHibernateSession.GetNamedQuery("GetDailyScndCnpValueList")
                                    .SetParameter("PersonId", PersonId)
                                    .SetParameter("Date", Date)
                                    .SetParameter("Order", Order)
                                    .List<ScndCnpValue>();
        }

        public IList<ArchiveConceptValue> LoadArchiveValueList(IList<decimal> personList, int year, int rangeOrder)
        {
            personList = base.CheckListParameter(personList).ToList();
            ArchiveConceptValue cnpValues = null;
            SecondaryConcept concept = null;
            Person person = null;
            IList<ArchiveConceptValue> result = NHibernateSession.QueryOver<ArchiveConceptValue>(() => cnpValues)
                .JoinAlias(() => cnpValues.Person, () => person)
                .Where(() => cnpValues.RangeOrder == rangeOrder)
                .And(() => cnpValues.Year == year)
                .And(() => person.ID.IsIn(personList.ToArray()))
                .List();
            return result;
        }

        public void ArchiveConceptValues(decimal personId,int year, int rangeOrder, DateTime todate) 
        {
            #region SQL Command
            string SQLCommand = @"
        declare @personId decimal(28,5),@date datetime,@order int,@year int
        set @personId=:personId
        set @date=:toDate
        set @order=:rangeOrder
        set @year=:year
        insert into TA_ArchiveConceptValue(CnpValue_ConceptTmpId,CnpValue_PersonId,CnpValue_Index,CnpValue_Value,CnpValue_ChangedValue,CnpValue_Year,CnpValue_RangeOrder,CnpValue_FromDate,CnpValue_ToDate,CnpValue_ModifiedDate,CnpValue_ModifiedPersonId)
        select 

			           PeriodicCnp_CnpTmpId		 AS ScndCnpValue_PeriodicScndCnpId, 			   
	                   PeriodicCnp_PersonId		 AS ScndCnpValue_PersonId,
	                   isnull(ScndCnpValue_Index,'-1')		 AS ScndCnpValue_Index,
	                   isnull(ScndCnpValue.ScndCnpValue_Value,0) AS ScndCnpValue_PeriodicValue,
	                   isnull(ScndCnpValue.ScndCnpValue_Value,0) AS ScndCnpValue_PeriodicValue,
                       @year,
                       @order,	                   
                       PeriodicCnp_FromDate		 AS ScndCnpValue_PeriodicFromDate,	   
	                   PeriodicCnp_ToDate		 AS ScndCnpValue_PeriodicToDate,	          
	                   GETDATE(),
	                   0	          
	            
	                   FROM (SELECT  CalcDateRange_ID					         AS PeriodicCnp_ID,
                                 PrsRangeAsg.PrsRangeAsg_PersonId		 AS PeriodicCnp_PersonId, 
					                       CalcDateRange_ConceptTmpId	 AS PeriodicCnp_CnpTmpId, 
					                       ConceptTmp_KeyColumnName 	 AS PeriodicCnp_KeyColumnName, 
					                       dbo.TA_ASM_CalculateFromDateRange(@date, CalcDateRange_Order, CalcDateRange_FromMonth, CalcDateRange_FromDay, CalcDateRange_ToMonth, CalcDateRange_ToDay, CalcRangeGroup_UsedCalendar)
													                     AS PeriodicCnp_FromDate,
					                       dbo.TA_ASM_CalculateToDateRange(@date, CalcDateRange_Order, CalcDateRange_FromMonth, CalcDateRange_FromDay, CalcDateRange_ToMonth, CalcDateRange_ToDay, CalcRangeGroup_UsedCalendar)
													                     AS PeriodicCnp_ToDate
			                   FROM (SELECT * 
		 		                       FROM dbo.TA_CalculationDateRange 
				                       WHERE CalcDateRange_Order = @order
                              ) AS CalcDateRng
			                   INNER JOIN (SELECT * 
						                         FROM dbo.TA_ConceptTemplate 
						                         WHERE ConceptTmp_IsPeriodic = 1
                                    ) AS Concept
			                   ON CalcDateRange_ConceptTmpId = Concept.ConceptTmp_ID		  
			                   INNER JOIN (SELECT TOP(1) PrsRangeAsg_CalcRangeGrpId, PrsRangeAsg_PersonId
						                         FROM TA_PersonRangeAssignment								 
						                         WHERE PrsRangeAsg_FromDate <= @date
						                         AND PrsRangeAsg_PersonId = @personId	
						                         ORDER BY PrsRangeAsg_FromDate DESC
                                    ) AS PrsRangeAsg
			                   ON CalcDateRange_CalcRangeGrpId = PrsRangeAsg.PrsRangeAsg_CalcRangeGrpId
			                   INNER JOIN TA_CalculationRangeGroup
			                   ON CalcDateRange_CalcRangeGrpId = CalcRangeGroup_ID
                    WHERE ConceptTmp_KeyColumnName IS NOT NULL
					        AND
			              ConceptTmp_KeyColumnName <> ''              
		           ) AS PeriodicCnp       
              OUTER APPLY dbo.TA_GetPeriodicScndCnpValue(PeriodicCnp.PeriodicCnp_PersonId, 
									                     PeriodicCnp.PeriodicCnp_CnpTmpId, 
									                     PeriodicCnp.PeriodicCnp_FromDate, 
									                     PeriodicCnp.PeriodicCnp_ToDate)AS ScndCnpValue
        Left Outer JOIN TA_SecondaryConceptValue   on 	ScndCnpValue.ScndCnpValue_ID=TA_SecondaryConceptValue.ScndCnpValue_ID
"; 
            #endregion

            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("personId", personId)
                .SetParameter("rangeOrder", rangeOrder)
                .SetParameter("toDate", todate)
                 .SetParameter("year", year)
                .ExecuteUpdate();
        }

        public int ExsitsArchiveCount(IList<decimal> personList, int Year, int rangeOrder)
        {
            personList = base.CheckListParameter(personList).ToList();
            ArchiveConceptValue cnpValues = null;
            Person person = null;
            var result = NHibernateSession
                .QueryOver<ArchiveConceptValue>(() => cnpValues)
                .JoinAlias(() => cnpValues.Person, () => person)
                .Where(() => cnpValues.RangeOrder == rangeOrder)
                .And(() => cnpValues.Year == Year)
                .And(() => person.ID.IsIn(personList.ToArray()))
                .Select
                (
                    Projections.Group<ArchiveConceptValue>(x => x.PersonId),
                    Projections.Count<ArchiveConceptValue>(x => x.PersonId)
                )
                .List<object[]>();
            return result.Count;
        }

        public void DeleteArchiveValues(IList<decimal> personList, int year, int rangeOrder)
        {
            personList = base.CheckListParameter(personList).ToList();
            String HQLCommand = @"delete ArchiveConceptValue archiveVal 
                                 where archiveVal.RangeOrder = :order 
                                       AND archiveVal.Year=:year
                                       AND archiveVal.PersonId in (:personList)";
            object obj = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("order", rangeOrder)
                .SetParameter("year", year)
                .SetParameterList("personList", personList)
                .ExecuteUpdate();

        }

        public IList<decimal> GetExsitsArchivePersons(IList<decimal> personList, int Year, int rangeOrder)
        {
            personList = base.CheckListParameter(personList).ToList();
            ArchiveConceptValue cnpValues = null;
            Person person = null;
            var result = NHibernateSession
                .QueryOver<ArchiveConceptValue>(() => cnpValues)
                .JoinAlias(() => cnpValues.Person, () => person)
                .Where(() => cnpValues.RangeOrder == rangeOrder)
                .And(() => cnpValues.Year == Year)
                .And(() => person.ID.IsIn(personList.ToArray()))
                .Select
                (
                    Projections.Group<ArchiveConceptValue>(x => x.PersonId),
                    Projections.Count<ArchiveConceptValue>(x => x.PersonId)
                )
                .List<object[]>();
            var persons = from o in result
                          select (decimal)o.First();
            return persons.ToList();
        }

    }
}

