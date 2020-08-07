using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Clientele
{
    public class CL_OffishTypeAccessGroup : BaseClienteleEnity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public virtual String Name { get; set; }

        public virtual String Description { get; set; }

        public virtual IList<CL_OffishType> OffishTypeList { get; set; }

        public virtual IList<CL_Flow> FlowList { get; set; }
        #endregion
    }
}
