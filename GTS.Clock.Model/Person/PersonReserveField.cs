using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model.PersonInfo
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
    /// 		<description>2012/08/01</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class PersonReserveField : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the OrginalName value.
        /// </summary>
        public virtual String OrginalName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the EnName value.
        /// </summary>
        public virtual String Lable { get; set; }

        public virtual SubSystemIdentifier SubSystemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual PersonReservedFieldsType ControlType { get; set; }

        public virtual IList<PersonReserveFieldComboValue> ComboItems { get; set; }

        #endregion
    }
}