using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    public class CalendarType : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public virtual String Name { get; set; }

        /// <summary>
        /// Gets or sets the Description value.
        /// </summary>
        public virtual String Description { get; set; }

        /// <summary>
        /// Gets or sets the CustomCode value.
        /// </summary>
        public virtual String CustomCode { get; set; }

        public virtual IList<Calendar> CalanderList { get; set; }

        public virtual IList<HolidaysTemplate> HolidayTemplateList { get; set; }

        #endregion

        public override string ToString()
        {
            string summery = "";
            summery = String.Format("تقویم:{0} با کد:{1} میباشد ", this.Name, this.CustomCode);
            return summery;
        }
    }
}