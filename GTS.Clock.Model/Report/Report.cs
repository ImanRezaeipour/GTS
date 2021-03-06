using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model.Report
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
	/// 		<description>2011-11-19</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

	public class Report:IEntity
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

        public virtual int Order { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ReportFile ReportFile { get; set; }

		/// <summary>
		/// Gets or sets the ParentId value.
		/// </summary>
		public virtual Decimal ParentId { get; set; }
        
        public virtual IList<decimal> ParentPathList
        {
            get
            {
                List<decimal> list = new List<decimal>();
                string path = this.ParentPath == null ? "" : this.ParentPath;
                string[] ids = path.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string id in ids)
                {
                    list.Add(Utility.ToInteger(id));
                }
                return list;
            }
        }

		/// <summary>
		/// Gets or sets the Path value.
		/// </summary>
		public virtual String ParentPath { get; set; }

        public virtual bool Visible { get; set; }

        public virtual IList<Report> ChildList
        {
            get;
            set;
        }

        /// <summary>
        /// آیا گزارش است یا گروه گزارش
        /// </summary>
        public virtual bool IsReport
        {
            get;
            set;
        }

        public virtual bool HasParameter { get; set; }

        public virtual SubSystemIdentifier SubSystemId { get; set; }
        public virtual bool IsDesignedReport { get; set; }
        public virtual string Description { get; set; }
        //public virtual decimal DesignedReportTypeID { get; set; }
        public virtual DesignedReportType DesignedType { get; set; }
        public virtual Report ParentReport { get; set; }
        public virtual ReportParameterDesigned ReportParameterDesigned { get; set; }
        #endregion		
	}
}