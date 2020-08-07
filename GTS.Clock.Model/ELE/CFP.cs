using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model
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
    /// 		<description>2012/04/18</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class CFP
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the PrsId value.
        /// </summary>
        public virtual Decimal PrsId { get; set; }

        /// <summary>
        /// Gets or sets the Date value.
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the MidNightCalculate value.
        /// </summary>
        public virtual Boolean MidNightCalculate { get; set; }

        /// <summary>
        /// Gets or sets the CalculationIsValid value.
        /// </summary>
        public virtual Boolean CalculationIsValid { get; set; }
        #endregion
    }
}