using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model.Clientele
{
    public class CL_UnderManagementPerson
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the Date value.
        /// </summary>
        public virtual String BarCode { get; set; }

        public virtual String PersonName { get; set; }

        public virtual string Family { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual decimal PersonId { get; set; }

        public virtual String DepartmentName { get; set; }
        

        #endregion
    }
}