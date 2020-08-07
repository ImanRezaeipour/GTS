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
    /// 		<description>6/22/2011</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class CalculationDateRange : IEntity, ICloneable
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        public virtual int FromDay { get; set; }

        public virtual int ToDay { get; set; }

        public virtual int FromMonth { get; set; }

        public virtual int ToMonth { get; set; }

        public virtual DateTime FromDate { get; set; }

        public virtual DateTime ToDate { get; set; }

        public virtual int FromIndex { get; set; }

        public virtual int ToIndex { get; set; }

        public virtual CalculationDateRangeOrder Order
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ConceptTmpID value.
        /// </summary>
        public virtual SecondaryConcept Concept { get; set; }

        /// <summary>
        /// Gets or sets the RangeGrpId value.
        /// </summary>
        public virtual CalculationRangeGroup RangeGroup { get; set; }
        #endregion

        #region ICloneable Members

        public virtual object Clone()
        {
            CalculationDateRange range = new CalculationDateRange();
            //range.ID = this.ID;
            range.Order = this.Order;
            range.FromDate = this.FromDate;
            range.ToDate = this.ToDate;
            range.FromDay = this.FromDay;
            range.FromMonth = this.FromMonth;
            range.ToDay = this.ToDay;
            range.ToMonth = this.ToMonth;
            range.Concept = this.Concept;
            return range;
        }

        #endregion
    }
}