using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Utility;

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
    /// 		<description>7/13/2011</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class AssignWorkGroup : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the FromDate value.
        /// </summary>
        public virtual DateTime FromDate { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual string UIFromDate { get; set; }


        /// <summary>
        /// Gets or sets the WorkGroupId value.
        /// </summary>
        public virtual WorkGroup WorkGroup { get; set; }

        /// <summary>
        /// Gets or sets the PersonId value.
        /// </summary>
        public virtual Person Person { get; set; }

       
        #endregion

        public AssignWorkGroup() 
        {
            //FromDate = GTS.Clock.Infrastructure.Utility.Utility.GTSMinStandardDateTime;
        }

        public override string ToString()
        {
            return String.Format("تخصیص گروه کاری با گروه کاری {0} وبه شخص {1} در تاریخ {2}", this.WorkGroup != null ? this.WorkGroup.Name : "0", this.Person != null ? this.Person.Name : "0", Utility.ToPersianDate(this.FromDate));
        }
    }
}