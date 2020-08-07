using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.RepositoryFramework;

namespace GTS.Clock.Infrastructure.Repository
{
    public class BudgetRepository : RepositoryBase<Budget>, IBudgetRepository
    {

        public override string TableName
        {
            get { return "TA_Budget"; }
        }

        public BudgetRepository()
            : base()
        { }

        public BudgetRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }

        public void DeleteExistingBudget(decimal ruleCategoryId, DateTime fromDate,DateTime toDate) 
        {
            string SQLCommand = @"Delete FROM TA_Budget 
                                    WHERE Budget_RuleCatId=:ruleCat and Budget_Date>=:fromDate and  Budget_Date<=:toDate";
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("ruleCat", ruleCategoryId)
                .SetParameter("fromDate", fromDate)
                .SetParameter("toDate", toDate)
                .ExecuteUpdate();
        }

        public IList<LeaveCalcResult> GetLCR(decimal personId, DateTime fromDate, DateTime toDate) 
        {
            EntityRepository<LeaveCalcResult> rep = new EntityRepository<LeaveCalcResult>();
            IList<LeaveCalcResult> result = rep.Find().Where(x => x.Person.ID == personId && x.Date <= toDate && x.Date >= fromDate).ToList();
            return result;
        }
    }
}
