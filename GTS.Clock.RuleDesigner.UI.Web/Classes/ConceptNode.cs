using System.Collections.Generic;

namespace GTS.Clock.RuleDesigner.UI.Web.Classes
{
	public class Expression
	{
		/// <summary>
		/// شناسه
		/// </summary>
		public int Expression_ID { get; set; }

		/// <summary>
		/// شناسه والد
		/// </summary>
		public int? Parent_ID { get; set; }

		/// <summary>
		/// مسیر دسترسی
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// نام نمایشس
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// کد سی شارپ
		/// </summary>
		public string CSharpCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public IList<Expression> ConceptsList { get; set; }
	}
}