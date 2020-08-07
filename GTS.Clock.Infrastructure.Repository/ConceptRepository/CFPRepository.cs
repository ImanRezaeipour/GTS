using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model;
using NHibernate;
using NHibernate.Transform;

namespace GTS.Clock.Infrastructure.Repository
{
    public class CFPRepository : RepositoryBase<CFP>
    {
        public CFPRepository(bool disconnectly) 
            :base(disconnectly)
        {

        }
        public CFPRepository()
            : base(false)
        {

        }

        public override string TableName
        {
            get { return "TA_Calculation_Flag_Persons"; }
        }

        public CFP GetByPersonID(decimal personID) 
        {
            string HQLCommand = @"select cfp from CFP as cfp
                                  where cfp.PrsId=:personId";
            IList<CFP> list = base.NHibernateSession.CreateQuery(HQLCommand)
                            .SetParameter("personId", personID)
                            .List<CFP>();
            return list.FirstOrDefault();
               
        }

        public void InsertCFP(decimal personId, DateTime date) 
        {
            string SqlCommand = @"insert into TA_Calculation_Flag_Persons (CFP_CalculationIsValid,CFP_Date,CFP_MidNightCalculate,CFP_PrsId)
                                  values(0,:date,0,:personId)";
            base.NHibernateSession.CreateSQLQuery(SqlCommand)
                           .SetParameter("personId", personId)
                           .SetParameter("date", date)
                           .ExecuteUpdate();

        }

        public IList<CFP> GetCFPListByRuleCategory(decimal ruleCatId)
        {

            string SQLCommand = @"select * from TA_Calculation_Flag_Persons where CFP_PrsId in 
                                 (select PrsRulCatAsg_PersonId from TA_PersonRuleCategoryAssignment where PrsRulCatAsg_RuleCategoryId=:RuleCatId)";
            IList<CFP> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                //.SetResultTransformer(Transformers.AliasToBean(typeof(CFP)))
               .AddEntity(typeof(CFP))
               .SetParameter("RuleCatId", ruleCatId)
               .List<CFP>();

            return list;
        }

    }
}
