using System;

namespace GTS.Clock.Model.Clientele
{
    public class CL_Equipment : BaseClienteleEnity
    {
        #region Properties

        public virtual String Name { get; set; }

        public virtual String CustomCode { get; set; }

        public virtual String Carrier { get; set; }

        public virtual int Count { get; set; }

        public virtual String Description { get; set; }        
        
        public virtual CL_EquipmentType EquipmentType { get; set; }

        public virtual CL_Contractor Contractor { get; set; }

        public virtual CL_OffishRequest Offish { get; set; }

        #endregion

    }
}
