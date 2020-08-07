using System;

namespace GTS.Clock.Model.Clientele
{
    public class CL_ClienteleBlackList : BaseClienteleEnity
    {
        #region Properties

        public virtual bool TemporarilyOutOfList { get; set; }

        public virtual String Description { get; set; }

        public virtual CL_ClientelePerson ClientelePerson { get; set; }

        public virtual DateTime BlackListDate { get; set; }

        public virtual DateTime? FromDate { get; set; }

        public virtual DateTime? ToDate { get; set; }

        public virtual string TheFromDate { get; set; }

        public virtual string TheToDate { get; set; }

        public virtual int ContractorCount { get; set; }

        public virtual decimal ContractorID { get; set; }

        public virtual string ContractorName { get; set; }

        public virtual string StrContarctorsList { get; set; }

        #endregion

    }
}
