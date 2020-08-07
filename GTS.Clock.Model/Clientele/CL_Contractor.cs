using System;
using System.Collections.Generic;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Charts;

namespace GTS.Clock.Model.Clientele
{
    public class CL_Contractor : BaseClienteleEnity
    {
        #region Properties
     
        public virtual String CustomCode { get; set; }

        public virtual String Name { get; set; }

        public virtual String WorkField { get; set; }

        public virtual String ContractNumber { get; set; }

        public virtual int WorkerCount { get; set; }

        public virtual bool InternetAccess { get; set; }

        public virtual String Description { get; set; }

        public virtual DateTime FromDate { get; set; }

        public virtual DateTime ToDate { get; set; }

        public virtual string TheFromDate { get; set; }

        public virtual string TheToDate { get; set; }

        public virtual int FromTime { get; set; }

        public virtual int ToTime { get; set; }

        public virtual string TheFromTime { get; set; }

        public virtual string TheToTime { get; set; }

        public virtual int TheEmployeeCount { get; set; }

        public virtual string DaysOfWeek { get; set; }

        public virtual string Image { get; set; }

        public virtual String Position { get; set; }

        public virtual IList<CL_ClientelePerson> EmployeeList { get; set; }

        public virtual IList<CL_Equipment> EquipmentList { get; set; }

        public virtual IList<CL_Car> CarList { get; set; }

        public virtual Person MeetingPerson { get; set; }

        public virtual bool PreventPersistChilds { get; set; }

        public virtual ControlStation ControlStation { get; set; }

        public virtual Department Department { get; set; }

        #endregion
    }

    public class CL_ContractorComparer : IEqualityComparer<CL_Contractor>
    {
        public bool Equals(CL_Contractor x, CL_Contractor y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(CL_Contractor obj)
        {
            return obj.ID.GetHashCode();
        }
    }

}
