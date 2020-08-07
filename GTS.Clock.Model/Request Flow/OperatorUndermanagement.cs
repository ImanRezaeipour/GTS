using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.RequestFlow
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
    /// 		<description>2011-12-27</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class OperatorUndermanagement
    {
        #region Properties
       
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Operator Operator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Person Person { get; set; }
      
        #endregion
    }
}