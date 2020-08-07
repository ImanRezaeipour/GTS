using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model.Clientele
{
    public class CL_ClientelePerson : BaseClienteleEnity
    {
        #region Properties

        public virtual String MeliCode { get; set; }
        
        public virtual String Name { get; set; }

        public virtual PersonSex Sex { get; set; }

        public virtual String Post { get; set; }

        public virtual String Tel1 { get; set; }

        public virtual String Tel2 { get; set; }

        public virtual String Tel3 { get; set; }

        public virtual String Email { get; set; }

        public virtual String TrafficCode { get; set; }

        public virtual String Address { get; set; }

        public virtual String Description { get; set; }

        public virtual string Image { get; set; }

        public virtual IList<CL_OffishRequest> OffishList { get; set; }

        public virtual IList<CL_Contractor> ContractorList { get; set; }

        public virtual IList<CL_ClientelePersonTraffic> PersonTrafficList { get; set; }



        #endregion

        #region UI Prop

        public virtual bool IsBlackList { get; set; }
        public virtual string Color { get; set; }
        public virtual string Title { get; set; }
        #endregion

    }
}
