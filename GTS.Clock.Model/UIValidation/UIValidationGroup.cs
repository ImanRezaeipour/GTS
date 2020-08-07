using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.UIValidation
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
    /// 		<description>4/4/2012</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class UIValidationGroup : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public virtual String Name { get; set; }

        public virtual String CustomCode { get; set; }

        public virtual int SubSystemId { get; set; }

        public virtual IList<UIValidationGrouping> GroupingList { get; set; }

        public virtual IList<PersonCLSpec> PersonCLList { get; set; }

        public virtual IList<PersonTASpec> PersonTAList { get; set; }

        #endregion

    }
}