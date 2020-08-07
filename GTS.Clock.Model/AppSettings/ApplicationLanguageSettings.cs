using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;


namespace GTS.Clock.Model.AppSetting
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

    public class ApplicationLanguageSettings : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// Gets or sets the LangID value.
		/// </summary>
		public virtual Languages Language { get; set; }

		/// <summary>
		/// Gets or sets the IsActive value.
		/// </summary>
		public virtual Boolean IsActive { get; set; }
        
		#endregion		

    }
}