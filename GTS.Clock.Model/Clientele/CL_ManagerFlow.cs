using System.Collections.Generic;

namespace GTS.Clock.Model.Clientele
{
    public class CL_ManagerFlow : BaseClienteleEnity
    {
        public virtual int Level { get; set; }
        public virtual bool Active { get; set; }

        public virtual CL_Manager Manager { get; set; }
        public virtual CL_Flow Flow { get; set; }
        public virtual IList<CL_OffishRequestStatus> OffishStatusList { get; set; }

    }
}
