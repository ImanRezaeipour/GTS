using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model.BaseInformation
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
	/// 		<description>5/23/2011</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

    public class Doctor : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// Gets or sets the FirstName value.
		/// </summary>
		public virtual String FirstName { get; set; }

		/// <summary>
		/// Gets or sets the LastName value.
		/// </summary>
		public virtual String LastName { get; set; }

        /// <summary>
        /// ÃÂ  «” ›«œÂ œ— Ê«”ÿ ò«—»—
        /// </summary>
        public virtual String Name 
        {
            get
            {
                if (!Utility.IsEmpty(FirstName) && !Utility.IsEmpty(LastName)) 
                {
                    return FirstName + " " + LastName;
                }
                else if (Utility.IsEmpty(FirstName) && !Utility.IsEmpty(LastName))
                {
                    return LastName;
                }
                else if (!Utility.IsEmpty(FirstName) && Utility.IsEmpty(LastName))
                {
                    return FirstName;
                }
                return "";
            }
        }

		/// <summary>
		/// Gets or sets the Takhasos value.
		/// </summary>
		public virtual String Takhasos { get; set; }

		/// <summary>
		/// Gets or sets the Nezampezaeshki value.
		/// </summary>
		public virtual String Nezampezaeshki { get; set; }

		/// <summary>
		/// Gets or sets the Description value.
		/// </summary>
		public virtual String Description { get; set; }
		#endregion		
	}
}