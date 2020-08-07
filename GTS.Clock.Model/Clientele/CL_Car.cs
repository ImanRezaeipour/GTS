using System;

namespace GTS.Clock.Model.Clientele
{
    public class CL_Car : BaseClienteleEnity
    {
        #region Properties

        public virtual String Name { get; set; }

        public virtual String CustomCode { get; set; }

        public virtual String Driver { get; set; }

        public virtual String Color { get; set; }

        public virtual String Description { get; set; }

        public virtual CL_Contractor Contractor { get; set; }

        public virtual CL_OffishRequest Offish { get; set; }

        #endregion

    }
}
