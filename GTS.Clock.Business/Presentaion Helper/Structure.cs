using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GTS.Clock.Business
{
    [DataContract]
    public enum ContractRequestState
    {
        [EnumMember]
        Confirmed = 1,
        [EnumMember]
        Unconfirmed = 2,
        [EnumMember]
        UnderReview = 3,
        [EnumMember]
        Deleted = 4,
        [EnumMember]
        UnKnown = 5
    }

    /// <summary>
    /// روزانه, ساعتی,اضافه کار
    /// </summary>
    [DataContract]
    public enum ContractRequestType
    {
        None, Hourly, Daily, OverWork
    }

    [DataContract]
    public enum ContractRequestSource { Undermanagment, Substitute }

    [DataContract]
    public enum ContractPrecardGroupsName
    {
        leave = 1, duty = 2, overwork = 3, traffic = 4, leaveestelajy = 5
    }

    public enum UIActionType
    {
        ADD = 1, EDIT = 2, DELETE = 3
    }

    public enum SysLanguageResource
    {
        Parsi, English
    }

    public enum LocalLanguageResource
    {
        Parsi, English
    }

    public enum ManagerSearchFields
    {
        PersonCode = 1, PersonName = 2, OrganizationUnitName = 3, NotSpecified = 4
    }

    public enum FlowSearchFields
    {
        NotSpec = 0, FlowName = 1, AccessGroupName = 2
    }

    public enum ManagerType
    {
        None = 0, Person = 1, OrganizationUnit = 2
    }

    /// <summary>
    /// کد سفارشی پیشفرض نقشها
    /// </summary>
    public enum RoleCustomCode
    {
        SysAdmin = 1, Admin = 2, Manager = 3, Substitute = 4, Operator = 5, User = 6
    }

    

    



    
}
