using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure;

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

    public class CalculationRangeGroup : IEntity,ICloneable
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
        /// Gets or sets the Des value.
        /// </summary>
        public virtual String Description { get; set; }

        public virtual IList<CalculationDateRange> DateRangeList
        {
            get;
            set;
        }

        public virtual LanguagesName Culture
        {
            get;
            set;
        }

        public virtual IList<PersonRangeAssignment> PersonRangeAssignmentList { get; set; }

        #endregion

        public override string ToString()
        {
            string summery = "";
            summery = String.Format("محدوده محاسبات:{0} با توضیح:{1} میباشد ", this.Name, this.Description);
            return summery;
        }

        #region ICloneable Members

        public virtual object Clone()
        {
            CalculationRangeGroup group = new CalculationRangeGroup();
           // group.ID = this.ID;
            group.Name = this.Name;
            group.Description = this.Description;
            return group;
        }

        #endregion
    }
}