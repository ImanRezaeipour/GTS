using System;
using System.Collections.Generic;
using GTS.Clock.Model.Charts;

namespace GTS.Clock.Model.Clientele
{
    public class CL_EquipmentBlackList : BaseClienteleEnity
    {
        #region Properties

        public virtual String Name { get; set; }

        public virtual String CustomCode { get; set; }

        public virtual String Description { get; set; }

        public virtual IList<Department> DepartmentList { get; set; }

        public virtual DateTime? FromDate { get; set; }

        public virtual DateTime? ToDate { get; set; }

        public virtual string TheFromDate { get; set; }

        public virtual string TheToDate { get; set; }

        #endregion

    }
}

