using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using System.Linq;
using System.Collections;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Model;

namespace GTS.Clock.Infrastructure.Repository
{
    public class OperatorRepository : RepositoryBase<Operator>
    {
        public override string TableName
        {
            get { return "TA_Operator"; }
        }

        public OperatorRepository(bool disconnectly) 
            :base(disconnectly)
        {
        }

        #region IManagerRepository Members

        /// <summary>
        /// لیست افرادی که اپراتور هستند را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<Person> GetAllOperator() 
        {
            IList<Operator> list = this.GetAll();
            var persons = from op in list
                          select op.Person;
            IList<Person> result = persons.ToList<Person>();
            return result;
        }

        /// <summary>
        /// یک شناسه پرسنلی میگیرد و اگر شخص اپراتور باشد آنرا برمیگرداند
        /// در غیر این صورت شی خالی برمیگردد
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public IList<Operator> GetOperator(decimal personId)
        {
            string HQLCommand = @"from Operator where Person.ID =:personId";
            IList<Operator> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("personId", personId)
                .List<Operator>();
            if (list != null && list.Count > 0)
                return list;

            return new List<Operator>();
        }


        #endregion
       
    }
}
