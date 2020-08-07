using System;
namespace GTS.Clock.Model.Clientele
{
    public class CL_OffishRequestStatus : BaseClienteleEnity
    {

        #region Properties
        public virtual string Description { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual bool Confirm { get; set; }
        public virtual bool EndFlow { get; set; }
        public virtual CL_OffishRequest OffishRequest { get; set; }
        public virtual CL_ManagerFlow ManagerFlow { get; set; }
        #endregion
    }
}
