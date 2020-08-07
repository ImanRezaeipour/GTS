using System;
using System.Linq;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using NHibernate.Criterion;
using NHibernate.Transform;


namespace GTS.Clock.Infrastructure.Repository
{
    public class ExecutablePersonCalcRepository : RepositoryBase<ExecutablePersonCalculation>, IExecutablePersonCalcRepository
    {
        public override string TableName 
        {
            get { return "Unknown"; }
        }
        public ExecutablePersonCalcRepository()
            : base()
        { }

        public ExecutablePersonCalcRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }

        /// <summary>
        ///  پرسنلی که باید محاسبات برای آنها انجام شود را برمی گرداند
        ///  اگر تاریخ آخرین محاسبه پرسنلی کوچکتر از تاریخ ورودی بود یا محاسباتش نا معتبر بود، آن پرسنل برای محاسبه واکشی می گردد
        /// </summary>
        /// <param name="Date">زمان حال که محاسبات تا آن زمان باید انجام شود</param>
        /// <returns></returns>
        public IList<ExecutablePersonCalculation> GetAll(DateTime Date)
        {
            string SQLCommand = @"declare @date datetime
                                    set @date=:Date
                                    SELECT CFP.*
                                    FROM TA_Calculation_Flag_Persons as CFP 
                                    join TA_Person on Prs_ID=CFP_PrsId
                                    WHERE prs_IsDeleted=0 AND Prs_Active=1 and
                                    CFP_Date < @date or (CFP_Date = @date and CFP_CalculationIsValid = 0)";
            IList<ExecutablePersonCalculation> result = NHibernateSession.CreateSQLQuery(SQLCommand)
                .AddEntity(typeof(ExecutablePersonCalculation))
                .SetParameter("Date", Date.Date)
                .List<ExecutablePersonCalculation>();
            //return NHibernateSession.QueryOver<ExecutablePersonCalculation>()
            //                        .Where(x => (x.FromDate < Date.Date) || (x.FromDate == Date.Date && !x.CalculationIsValid))
            //                        .List<ExecutablePersonCalculation>();
            return result;
        }

        /// <summary>
        ///  اگر پرسنل با بارکد ورودی نیاز به محاسبه داشت آن را در قالب شی "پرسنل قابل محاسبه" برمی گرداند
        ///  اگر تاریخ آخرین محاسبه پرسنل کوچکتر از تاریخ ورودی بود یا محاسباتش نا معتبر بود، آن پرسنل برای محاسبه واکشی می گردد
        /// </summary>
        /// <param name="Barcode"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public ExecutablePersonCalculation GetByBarcode(string Barcode, DateTime Date)
        {
            Person prs = null;
            ExecutablePersonCalculation ExePrsCalc = null;
            return NHibernateSession.QueryOver<ExecutablePersonCalculation>(() => ExePrsCalc)
                                    .Select(Projections.Property(() => ExePrsCalc.ID).WithAlias(() => ExePrsCalc.ID),
                                            Projections.Property(() => ExePrsCalc.PersonId).WithAlias(() => ExePrsCalc.PersonId),
                                            Projections.Property(() => ExePrsCalc.FromDate).WithAlias(() => ExePrsCalc.FromDate),
                                            Projections.Property(() => ExePrsCalc.MidNightCalculate).WithAlias(() => ExePrsCalc.MidNightCalculate),
                                            Projections.Property(() => ExePrsCalc.CalculationIsValid).WithAlias(() => ExePrsCalc.CalculationIsValid))
                                    .JoinAlias(() => ExePrsCalc.PersonId, () => prs)
                                    .Where(() => prs.BarCode == Barcode)
                                    .And(x => (x.FromDate < Date.Date) || (x.FromDate == Date.Date && !x.CalculationIsValid))                                    
                                    .TransformUsing(Transformers.AliasToBean<ExecutablePersonCalculation>())
                                    .SingleOrDefault();
        }

        /// <summary>
        ///  اگر پرسنل با شناسه ورودی نیاز به محاسبه داشت آن را در قالب شی "پرسنل قابل محاسبه" برمی گرداند
        ///  اگر تاریخ آخرین محاسبه پرسنل کوچکتر از تاریخ ورودی بود یا محاسباتش نا معتبر بود، آن پرسنل برای محاسبه واکشی می گردد   
        /// </summary>
        /// <param name="PrsId"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public ExecutablePersonCalculation GetByPrsId(decimal PrsId, DateTime Date)
        {
            return NHibernateSession.QueryOver<ExecutablePersonCalculation>()
                                    .Where(x => (x.FromDate < Date.Date) || (x.FromDate == Date.Date && !x.CalculationIsValid))
                                    .And(x => x.PersonId == PrsId)
                                    .SingleOrDefault<ExecutablePersonCalculation>();
        }

        /// <summary>
        ///  اگر پرسنل با شناسه های ورودی نیاز به محاسبه داشت آن را در قالب شی "پرسنل قابل محاسبه" برمی گرداند
        ///  اگر تاریخ آخرین محاسبه پرسنل کوچکتر از تاریخ ورودی بود یا محاسباتش نا معتبر بود، آن پرسنل برای محاسبه واکشی می گردد   
        /// </summary>
        /// <param name="PrsId"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public IList<ExecutablePersonCalculation> GetAllByPrsIds(IList<decimal> Persons, DateTime Date)
        {
            IList<ExecutablePersonCalculation> result = NHibernateSession.QueryOver<ExecutablePersonCalculation>()
                                    .Where(x => (x.FromDate < Date.Date) || (x.FromDate == Date.Date && !x.CalculationIsValid))
                                    .AndRestrictionOn(x => x.PersonId).IsIn(Persons.ToArray<decimal>())
                                    .List<ExecutablePersonCalculation>();
            return result;
            //return NHibernateSession.QueryOver<ExecutablePersonCalculation>()
            //                        .Where(x => !x.CalculationIsValid)
            //                        .AndRestrictionOn(x => x.PersonId).IsIn(Persons.ToArray<decimal>())
            //                        .List<ExecutablePersonCalculation>();
        }

    }
}
