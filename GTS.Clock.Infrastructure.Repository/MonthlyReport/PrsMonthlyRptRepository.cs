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
    public class PrsMonthlyRptRepository : RepositoryBase<PersonalMonthlyReport>, IPersonalMonthlyRptRepository
    {
        public override string TableName
        {
            get { return ""; }
        }

        public PrsMonthlyRptRepository()
            : base()
        { }

        public PrsMonthlyRptRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }


        #region IPrsMonthlyRptRepository Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<CurrentProceedTraffic> LoadDailyProceedTrafficList(decimal personId, DateTime fromDate, DateTime toDate)
        {
            return NHibernateSession.GetNamedQuery("GetDailyProceedTrafficList")
                                    .SetParameter("PersonId", personId)
                                    .SetParameter("FromDate", fromDate.ToString("yyyy/MM/dd"))
                                    .SetParameter("ToDate", toDate.ToString("yyyy/MM/dd"))
                                    .List<CurrentProceedTraffic>();
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PersonId"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public IList<PersonalMonthlyReportRowDetail> LoadPairableScndcnpValue(decimal PersonId, DateTime Date)
        {
            PairableScndCnpValue PairableScndCnpValue = null;
            SecondaryConcept ScndCnp = null;
            PersonalMonthlyReportRowDetail PrsMonthlyRptRowDtl = null;
            return NHibernateSession.QueryOver<PairableScndCnpValue>(() => PairableScndCnpValue)
                                    .Select(Projections.Property(() => ScndCnp.Name).WithAlias(() => PrsMonthlyRptRowDtl.ScndCnpName),
                                            Projections.Property(() => PairableScndCnpValue.FromPairs).WithAlias(() => PrsMonthlyRptRowDtl.Froms),
                                            Projections.Property(() => PairableScndCnpValue.ToPairs).WithAlias(() => PrsMonthlyRptRowDtl.Tos),
                                            Projections.Property(() => ScndCnp.Color).WithAlias(() => PrsMonthlyRptRowDtl.Color))

                                    .JoinAlias(() => PairableScndCnpValue.Concept, () => ScndCnp)
                                    .Where(x => x.Person.ID == PersonId)
                                    .And(x => x.FromDate == Date)
                                    .And(x => x.ToDate == Date)            
                                    .TransformUsing(Transformers.AliasToBean<PersonalMonthlyReportRowDetail>())
                                    .List<PersonalMonthlyReportRowDetail>();
        }

        public IList<PersonalMonthlyReportRowDetail> LoadPairableScndcnpValue(decimal PersonId, DateTime Date, ConceptsKeys key)
        {
            PairableScndCnpValue PairableScndCnpValue = null;
            SecondaryConcept ScndCnp = null;
            PersonalMonthlyReportRowDetail PrsMonthlyRptRowDtl = null;
            return NHibernateSession.QueryOver<PairableScndCnpValue>(() => PairableScndCnpValue)
                                    .Select(Projections.Property(() => ScndCnp.Name).WithAlias(() => PrsMonthlyRptRowDtl.ScndCnpName),
                                            Projections.Property(() => PairableScndCnpValue.FromPairs).WithAlias(() => PrsMonthlyRptRowDtl.Froms),
                                            Projections.Property(() => PairableScndCnpValue.ToPairs).WithAlias(() => PrsMonthlyRptRowDtl.Tos),
                                            Projections.Property(() => ScndCnp.Color).WithAlias(() => PrsMonthlyRptRowDtl.Color))
                                    .JoinAlias(() => PairableScndCnpValue.Concept, () => ScndCnp)
                                    .Where(x => x.Person.ID == PersonId)
                                    .And(x => x.FromDate == Date)
                                    .And(x => x.ToDate == Date)
                                    .And(() => ScndCnp.KeyColumnName == key.ToString())
                                    .TransformUsing(Transformers.AliasToBean<PersonalMonthlyReportRowDetail>())
                                    .List<PersonalMonthlyReportRowDetail>();
        }

        /// <summary>
        /// جهت بارگزاری آیتم های بازه ای
        /// </summary>
        /// <param name="PersonId"></param>
        /// <param name="Date"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        public IList<ScndCnpValue> LoadDailyScndCnpWithouthMonthlyList(decimal PersonId, DateTime fromDate,DateTime toDate)
        {
            return NHibernateSession.GetNamedQuery("GetDailyWithoutMonthlyScndCnpValueList")
                                    .SetParameter("PersonId", PersonId)
                                    .SetParameter("FromDate", fromDate)
                                    .SetParameter("ToDate", toDate)
                                    .List<ScndCnpValue>();
        }
        #endregion


    }
}

