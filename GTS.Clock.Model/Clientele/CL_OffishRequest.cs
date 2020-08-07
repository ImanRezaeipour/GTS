using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Security;

namespace GTS.Clock.Model.Clientele
{
    public class CL_OffishRequest : BaseClienteleEnity
    {
        #region Properties

        public virtual String Name { get; set; }

        public virtual String CustomCode { get; set; }

        public virtual DateTime FromDate { get; set; }

        public virtual DateTime ToDate { get; set; }

        public virtual string TheFromDate { get; set; }

        public virtual string TheToDate { get; set; }

        public virtual int FromTime { get; set; }

        public virtual int ToTime { get; set; }

        public virtual string TheFromTime { get; set; }

        public virtual string TheToTime { get; set; }

        public virtual bool ActiveDirectory { get; set; }

        public virtual bool InternetAccess { get; set; }

        public virtual String Description { get; set; }

        public virtual Department Department { get; set; }

        public virtual CL_OffishType OffishType { get; set; }

        public virtual IList<CL_ClientelePerson> PersonList { get; set; }

        public virtual string StrPersonList { get; set; }

        public virtual bool IsSingleReferredPerson { get; set; }

        public virtual Person MeetingPerson { get; set; }

        public virtual Person SubstituteMeetingPerson { get; set; }

        public virtual PersonSex Sex { get; set; }

        public virtual ControlStation ControlStation { get; set; }

        public virtual string MeetingLocation { get; set; }

        public virtual IList<CL_OffishRequestStatus> OffishRequestStatusList { get; set; }

        public virtual IList<CL_Equipment> EquipmentList { get; set; }

        public virtual IList<CL_Car> CarList { get; set; }

        public virtual string ActiveDirectoryUserName { get; set; }

        public virtual string Attachment { get; set; }

        public virtual bool FoodRecieve { get; set; }

        public virtual bool PreventPersistChilds { get; set; }

        public virtual User User { get; set; }

        public virtual string OperatorUser { get; set; }

        public virtual bool IsDateSetByUser { get; set; }

        public virtual DateTime RegisterDate { get; set; }

        public virtual string RegistrationDate { get; set; }

        public virtual bool AddClientSide { get; set; }

        public virtual bool EndFlow { get; set; }

        public virtual RequestState Status { get; set; }

        public virtual decimal PersonId { get; set; }

        #endregion
    }

    public class CL_OffishReuestComparer : IEqualityComparer<CL_OffishRequest>
    {
        public bool Equals(CL_OffishRequest x, CL_OffishRequest y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(CL_OffishRequest obj)
        {
            return obj.ID.GetHashCode();
        }
    }
}
