using System;
using System.Collections.Generic;
using System.Linq;
using GTS.Clock.Infrastructure.RepositoryFramework;
using System.Text;
using GTS.Clock.Model.Concepts;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Model;

namespace GTS.Clock.Infrastructure.Repository
{
    public class BasicTrafficRepository : RepositoryBase<BasicTraffic>
    {
        public override string TableName
        {
            get { return "TA_BaseTraffic"; }
        }

        public IList<BasicTraffic> GetAllBaiscTrafficsByConditions(TrafficTransferMode TTM, decimal machineID, DateTime fromDate, DateTime toDate, int fromTime, int toTime, int fromRecord, int toRecord, decimal fromIdentifier, decimal toIdentifier, bool IsIntegralConditions)
        {
            IList<BasicTraffic> BasicTrafficsList = null;
            IEnumerable<BasicTraffic> BasicTrafficEnumerable = null;
            IQueryOver<BasicTraffic, BasicTraffic> IQueryOverBasicTraffic = null;
            Expression<Func<BasicTraffic, bool>> conditions = conditions = x => x.Date >= fromDate && x.Date <= toDate && x.Time >= fromTime && x.Time <= toTime && x.Active == true;

            switch (TTM)
            {
                case TrafficTransferMode.Normal:
                    IQueryOverBasicTraffic = NHibernateSession.QueryOver<BasicTraffic>()
                                            .Where(conditions);
                    BasicTrafficsList = this.GetBasicTrafficsOverMachine(machineID, IQueryOverBasicTraffic);
                    break;
                case TrafficTransferMode.RecordBase:
                    IQueryOverBasicTraffic = NHibernateSession.QueryOver<BasicTraffic>()
                                                              .OrderBy(basicTraffic => basicTraffic.ID).Asc
                                                              .Skip(fromRecord - 1)
                                                              .Take(toRecord - fromRecord + 1)
                                                              .Clone();
                    if (IsIntegralConditions)
                    {
                        IQueryOverBasicTraffic = IQueryOverBasicTraffic.Where(conditions);
                        BasicTrafficsList = this.GetBasicTrafficsOverMachine(machineID, IQueryOverBasicTraffic);
                    }
                    else
                        BasicTrafficsList = IQueryOverBasicTraffic.List<BasicTraffic>();
                    break;
                case TrafficTransferMode.IdentifierBase:
                    IQueryOverBasicTraffic = NHibernateSession.QueryOver<BasicTraffic>()
                                                              .Where(basicTraffic => basicTraffic.ID >= fromIdentifier && basicTraffic.ID <= toIdentifier && basicTraffic.Active)
                                                              .Clone();
                    if (IsIntegralConditions)
                    {
                        IQueryOverBasicTraffic = IQueryOverBasicTraffic.Where(conditions);
                        BasicTrafficsList = this.GetBasicTrafficsOverMachine(machineID, IQueryOverBasicTraffic);
                    }
                    else
                        BasicTrafficsList = BasicTrafficEnumerable.ToList<BasicTraffic>();
                    break;
            }
            return BasicTrafficsList;
        }

        private IList<BasicTraffic> GetBasicTrafficsOverMachine(decimal machineID, IQueryOver<BasicTraffic, BasicTraffic> IQueryOverBasicTraffic)
        {
            IList<BasicTraffic> BasicTrafficsList = new List<BasicTraffic>();
            if (machineID == 0)
                BasicTrafficsList = IQueryOverBasicTraffic.List<BasicTraffic>();
            else
                BasicTrafficsList = IQueryOverBasicTraffic.JoinQueryOver(basicTraffic => basicTraffic.Clock)
                                                          .Where(clock => clock.ID == machineID)
                                                          .List<BasicTraffic>();
            return BasicTrafficsList;
        }

        public int GetBasicTrfficsRowCount()
        {
            return NHibernateSession.QueryOver<BasicTraffic>().RowCount();
        }

        public decimal GetBaseTrafficsLastRowIdentifier()
        {
            int rowCount = this.GetBasicTrfficsRowCount();
            IList<BasicTraffic> BasicTrafficList = NHibernateSession.QueryOver<BasicTraffic>()
                                                   .OrderBy(basicTraffic => basicTraffic.ID).Desc
                                                   .Take(1).List<BasicTraffic>();
            return BasicTrafficList[0].ID;
        }

        public DateTime GetMinInvalidDate(decimal personId)
        {
            //IList<BasicTraffic> BasicTrafficList = NHibernateSession.QueryOver<BasicTraffic>()
            //                                                        .JoinQueryOver<Person>(basicTraffic => basicTraffic.Person)
            //                                                        .Where(person => person.ID == personId)
            //                                                        .Clone()
            //                                                        .Where(basicTraffic => !basicTraffic.Used && basicTraffic.Active)
            //                                                        .OrderBy(basicTraffic => basicTraffic.Date)
            //                                                        .Asc
            //                                                        .Take(1)
            //                                                        .List<BasicTraffic>();

            IList<DateTime> dt = NHibernateSession.QueryOver<BasicTraffic>()
                                                                  .JoinQueryOver<Person>(basicTraffic => basicTraffic.Person)
                                                                  .Where(person => person.ID == personId)
                                                                  .Clone()
                                                                  .Where(basicTraffic => !basicTraffic.Used && basicTraffic.Active)
                                                                  .OrderBy(basicTraffic => basicTraffic.Date)
                                                                  .Asc
                                                                  .Select(basicTraffic => basicTraffic.Date)
                                                                  .Take(1)
                                                                  .List<DateTime>();
            if (dt != null && dt.Count == 1)
            {
                return dt.First();
            }
            else
            {
                return Utility.Utility.GTSMinStandardDateTime;
            }
        }
    }
}
