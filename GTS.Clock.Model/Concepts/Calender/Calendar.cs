using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;

namespace GTS.Clock.Model.Concepts
{
    #region Comments
    /// <h3>Changes</h3>
    /// 	<listheader>
    /// 		<th>Author</th>
    /// 		<th>Date</th>
    /// 		<th>Details</th>
    /// 	</listheader>
    /// 	<item>
    /// 		<term>Farhad Salavati</term>
    /// 		<description>2011-12-01</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class Calendar : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the Date value.
        /// </summary>
        public virtual DateTime Date { get; set; }

        public virtual int ActiveDayCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public virtual CalendarType CalendarType { get; set; }

        /// <summary>
        /// Gets or sets the CustomCode value of CalendarType .
        /// </summary>
        public virtual String CustomCode { get; set; }

        #endregion

        public static ICalendarRepository GetRepository(bool Disconnectedly)
        {
            return RepositoryFactory.GetRepository<ICalendarRepository, Calendar>(Disconnectedly);
        }

       
    }
}