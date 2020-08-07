using System;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using NHibernate.Criterion;
using System.Collections.Generic;
using GTS.Clock.Model;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace GTS.Clock.Infrastructure.Repository
{
    public class ShiftExceptionRepository : RepositoryBase<ShiftException> 
    {
        public override string TableName
        {
            get { return "TA_ExceptionShift"; }
        }       

        public ShiftExceptionRepository() 
        {

        }

        public ShiftExceptionRepository(bool Disconnectedly)
            : base(Disconnectedly)
        {

        }

        /// <summary>
        /// حذف شیفتهای استثنا قبلی
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        public void DeleteExceptionShift(decimal personId, DateTime fromDate, DateTime toDate) 
        {
            String sqlCommand = @"delete from TA_ExceptionShift
                                    where ExceptionShift_PersonID=:personId 
                                    AND ExceptionShift_Date>= :fromDate 
                                    AND ExceptionShift_Date<= :toDate";

            NHibernateSession.CreateSQLQuery(sqlCommand)
                .SetParameter("personId", personId)
                .SetParameter("fromDate", fromDate.Date)
                .SetParameter("toDate", toDate.Date)
                .ExecuteUpdate();

        }

        public ShiftException GetPersonnelExceptionShiftByDate(decimal personnelID, DateTime date)
        {
            ShiftException ExceptionShift = null;
            IList<ShiftException> ExceptionShiftsList = this.NHibernateSession.QueryOver<ShiftException>()
                                                                  .Where(exceptionShift => exceptionShift.Date == date)
                                                                  .JoinQueryOver<Person>(exceptionShift => exceptionShift.Person)
                                                                  .Where(person => person.ID == personnelID)
                                                                  .List();
            if (ExceptionShiftsList.Count() > 0)
                ExceptionShift = ExceptionShiftsList.First();                                                                                                                   
            return ExceptionShift;
        }

        public Shift GetShiftByCustomCode(string customCode)
        {
            Shift shift = null;
            IList<Shift> ShiftsList = this.NHibernateSession.QueryOver<Shift>()
                                              .Where(x => x.CustomCode == customCode)
                                              .List();
            if (ShiftsList.Count() > 0)
                shift = ShiftsList.First();
            return shift;
        }




    
    }
}
