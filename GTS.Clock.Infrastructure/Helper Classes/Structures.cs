using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure
{

    public struct LeaveInfo
    {
        public int Day;
        public int Minute;
    }


    public enum LanguagesName
    {
        Unknown = 0, Parsi = 1, English = 2
    }

    public enum SessionWorkSpace
    {
        WEB, WinService
    }

    /// <summary>
    /// جهت استخراج مفاهیم ساعتی
    /// </summary>
    public enum ConceptsKeys
    {
        gridFields_AllowableOverTime,
        gridFields_HostelryMission,
        gridFields_HourlyMeritoriouslyLeave,
        gridFields_HourlyMission,
        gridFields_HourlyPureOperation,
        gridFields_HourlySickLeave,
        gridFields_HourlyUnallowableAbsence,
        gridFields_HourlyWithoutPayLeave,
        gridFields_HourlyWithPayLeave,
        gridFields_PresenceDuration,
        gridFields_UnallowableOverTime,
    }

    public enum PrecardGroupsName
    {
        leave = 1, duty = 2, overwork = 3, traffic = 4, leaveestelajy = 5, imperative = 6
    }

    /// <summary>
    /// تایید شده , ردشده,تحت بررسی
    /// </summary>
    public enum RequestState
    {
        Confirmed = 1, Unconfirmed = 2, UnderReview = 3, Deleted = 4, UnKnown
    }

    /// <summary>
    /// روزانه, ساعتی,اضافه کار
    /// </summary>
    public enum OfficeRequestType
    {
        None =0, NotStarted=1, InProcess=2, Completed=3
    }

    public enum RequestType
    {
        None, Hourly, Daily, Monthly, OverWork, Imperative
    }

    public enum RequestSource { Undermanagment, Substitute }

    public enum UserSearchKeys
    {
        PersonCode = 1, Name = 2, Username = 3, RoleName = 4, NotSpecified = 5
    }

    public enum HashStandards
    {
        None, MD5, SHA1, SHA256, SHA384, SHA512
    }

    public enum MembershipProviders
    {
        ADMembershipProvider, GTSMembershipProvider
    }

    public enum MarriageStatus
    {
        Married, Single
    };

    #region Person Advance Search Paramerters

    public enum PersonSex
    {
        Male = 0, Female = 1
    }

    public enum MaritalStatus
    {
        Mojarad = 1, Motahel = 2, Motaleghe = 3
    }

    public enum MilitaryStatus
    {
        GheireMashmool = 1, AmadeBeKhedmat = 2, HeineKhedmat = 3, DarayeKartePayanKhedmat = 4,
        MoafiatTahsili = 5, MoafiatTakafol = 6, MoafiatPezeshki = 7, Sayer = 8
    }

    /// <summary>
    /// پارمترهای جستجوی پیشرفته
    /// جهت ارسال به انباره داده
    /// </summary>
    public class PersonSearchProxy
    {
        public PersonSearchProxy()
        {
            Sex = null;
            Military = null;
            Education = null;
            MaritalStatus = null;
            DepartmentId = null;
            WorkGroupId = null;
            WorkGroupFromDate = null;
            RuleGroupId = null;
            RuleGroupFromDate = null;
            RuleGroupToDate = null;
            CalculationDateRangeId = null;
            CalculationFromDate = null;
            ControlStationId = null;
            EmploymentType = null;
            PersonId = null;
            SearchInCategory = PersonCategory.Public;
            GradeId = null;
            DepartmentListId = null;
               
        }

        /// <summary>
        /// اگر مقداردهی شود تنها همین شخص جستجو میشود
        /// </summary>
        public decimal? PersonId { get; set; }

        /// <summary>
        /// جنسیت
        /// </summary>
        public PersonSex? Sex { get; set; }

        /// <summary>
        /// نظام وظیفه
        /// </summary>
        public MilitaryStatus? Military { get; set; }

        /// <summary>
        /// تحصیلات
        /// </summary>
        public String Education { get; set; }

        /// <summary>
        /// وضعیت تاهل
        /// </summary>
        public MaritalStatus? MaritalStatus { get; set; }

        /// <summary>
        /// کد بخش
        /// </summary>
        public decimal? DepartmentId { get; set; }
        public List<decimal> DepartmentListId { get; set; }
        /// <summary>
        /// شامل زیر بخش ها هم بشود
        /// </summary>
        public bool IncludeSubDepartments { get; set; }

        /// <summary>
        /// کد گروه کاری
        /// </summary>
        public decimal? WorkGroupId { get; set; }

        /// <summary>
        /// تاریخ شروع انتساب گروه کاری
        /// </summary>
        public String WorkGroupFromDate { get; set; }

        /// <summary>
        /// کد گروه قوانین
        /// </summary>
        public decimal? RuleGroupId { get; set; }

        /// <summary>
        /// تاریخ شروع انتساب گروه قوانین
        /// </summary>
        public String RuleGroupFromDate { get; set; }

        /// <summary>
        /// تاریخ انتهای انتساب گروه قوانین
        /// </summary>
        public String RuleGroupToDate { get; set; }

        /// <summary>
        /// کد دوره محاسبات
        /// </summary>
        public decimal? CalculationDateRangeId { get; set; }

        /// <summary>
        /// تاریخ شروع دوره محاسبات
        /// </summary>
        public String CalculationFromDate { get; set; }

        /// <summary>
        /// ایستگاه کنترل
        /// </summary>
        public decimal? ControlStationId { get; set; }
        public decimal? GradeId { get; set; }
        /// <summary>
        /// نوع استخدام
        /// </summary>
        public decimal? EmploymentType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal OrganizationUnitId { get; set; }

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public String FromBirthDate { get; set; }

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public String ToBirthDate { get; set; }

        /// <summary>
        /// تاریخ استخدام
        /// </summary>
        public String FromEmploymentDate { get; set; }

        /// <summary>
        /// تاریخ استخدام
        /// </summary>
        public String ToEmploymentDate { get; set; }

        /// <summary>
        /// شماره استخدامی
        /// </summary>
        public String EmployeeNumber { get; set; }

        /// <summary>
        /// محل تولد
        /// </summary>
        public String BirthPlace { get; set; }

        /// <summary>
        /// شماره کارت
        /// </summary>
        public String CartNumber { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        public String FirstName { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public String LastName { get; set; }     

        /// <summary>
        /// نام پدر
        /// </summary>
        public String FatherName { get; set; }

        /// <summary>
        /// شماره پرسنلی
        /// </summary>
        public String PersonCode { get; set; }

        /// <summary>
        /// کد ملی
        /// </summary>
        public String MelliCode { get; set; }

        public bool? PersonActivateState { get; set; }

        public PersonCategory SearchInCategory { get; set; }

        public IList<decimal> PersonIdList { get; set; }
    }

    /// <summary>
    /// پارمترهای جستجوی پیشرفته
    /// جهت ارسال به انباره داده
    /// </summary>
    public class PersonCLSearchProxy
    {
        public PersonCLSearchProxy()
        {
            Sex = null;
            Military = null;
            Education = null;
            MaritalStatus = null;
            DepartmentId = null;
            DepartmentPositionId = null;
            ControlStationId = null;
            EmploymentType = null;
            PersonId = null;
            SearchInCategory = PersonCategory.Public;
        }

        /// <summary>
        /// اگر مقداردهی شود تنها همین شخص جستجو میشود
        /// </summary>
        public decimal? PersonId { get; set; }

        /// <summary>
        /// جنسیت
        /// </summary>
        public PersonSex? Sex { get; set; }

        /// <summary>
        /// نظام وظیفه
        /// </summary>
        public MilitaryStatus? Military { get; set; }

        /// <summary>
        /// تحصیلات
        /// </summary>
        public String Education { get; set; }

        /// <summary>
        /// وضعیت تاهل
        /// </summary>
        public MaritalStatus? MaritalStatus { get; set; }

        /// <summary>
        /// کد بخش
        /// </summary>
        public decimal? DepartmentId { get; set; }

        public decimal? DepartmentPositionId { get; set; }

        /// <summary>
        /// شامل زیر بخش ها هم بشود
        /// </summary>
        public bool IncludeSubDepartments { get; set; }

        /// <summary>
        /// ایستگاه کنترل
        /// </summary>
        public decimal? ControlStationId { get; set; }

        /// <summary>
        /// نوع استخدام
        /// </summary>
        public decimal? EmploymentType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal OrganizationUnitId { get; set; }

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public String FromBirthDate { get; set; }

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public String ToBirthDate { get; set; }

        /// <summary>
        /// تاریخ استخدام
        /// </summary>
        public String FromEmploymentDate { get; set; }

        /// <summary>
        /// تاریخ استخدام
        /// </summary>
        public String ToEmploymentDate { get; set; }

        /// <summary>
        /// شماره استخدامی
        /// </summary>
        public String EmployeeNumber { get; set; }

        /// <summary>
        /// محل تولد
        /// </summary>
        public String BirthPlace { get; set; }

        /// <summary>
        /// شماره کارت
        /// </summary>
        public String CartNumber { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        public String FirstName { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public String LastName { get; set; }

        /// <summary>
        /// نام پدر
        /// </summary>
        public String FatherName { get; set; }

        /// <summary>
        /// شماره پرسنلی
        /// </summary>
        public String PersonCode { get; set; }

        /// <summary>
        /// کد ملی
        /// </summary>
        public String MelliCode { get; set; }

        public bool? PersonActivateState { get; set; }

        public PersonCategory SearchInCategory { get; set; }

        public IList<decimal> PersonIdList { get; set; }
    }

    /// <summary>
    /// جهت ارسال ایمیل 
    /// </summary>
    public class InfoServiceProxy
    {
        public decimal PersonId { get; set; }

        public PersonSex Sex { get; set; }

        public string PersonName { get; set; }
        public string PersonCode { get; set; }

        public string EmailAddress { get; set; }

        public string SmsNumber { get; set; }

        public Boolean SendByDay { get; set; }

        public TimeSpan RepeatePeriod { get; set; }

    }

    #endregion

    /// <summary>
    /// public:Athorized Person By Department Access
    /// Admin:
    /// Operator:
    /// Manager:
    /// Operator_Manager_UnderManagment:
    /// Sentry_UnderManagment:
    /// </summary>
    public enum PersonCategory
    {
        Public = 1, Manager_UnderManagment = 2, Operator_UnderManagment = 3, Manager = 4, Sentry_UnderManagment = 5, Substitute_UnderManagment = 6,SubstitudeManager = 7
    }

    public enum ReportParametersActionId
    {
        PersonDateRange,
        ToDate_Implicit_StartOfYear_EndOfYear
    }
     
    public enum KartablSummaryItems
    {
        MainRecievedRequestCount, SubstituteRecievedRequestCount,
        ConfirmedRequestCount, NotConfirmedRequestCount,
        InFlowRequestCount, PrivateMessageCount
    }

    public enum KartablOrderBy
    {
        RequestDate, PersonCode, PersonName, RegisteredBy, RequestSubject, None
    }

    public enum SentryPermitsOrderBy
    {
        PersonCode, PersonName, PermitSubject
    }

    public enum CalanderTypeItems
    {
        Rasmi = 1, GheireRasmi = 2,
        NoRooz = 3
    }

    public enum RequestSubmiter { USER, OPERATOR }

    public enum BudgetType { Usual = 1, PerMonth = 2 }

    public enum LeaveIncDecAction { Increase, Decrease }

    public enum DataAccessParts
    {
        Department, OrganizationUnit, Shift, WorkGroup, Precard,
        ControlStation, Doctor, Manager, RuleGroup, Flow, Report, Corporation, EmploymentType
    }

    public enum ClienteleDataAccessParts
    {
        Department, OrganizationUnit, OfficeType,
        ControlStation, Manager, Flow, Report, Corporation
    }


    public enum DataAccessLevelsType { Source, Target }

    public enum ScndCnpCustomeCategoryCode
    {
        Work = 1,
        Leave= 2,
        Mission = 3,
        Absence = 4,
        OverTime=5,
    }

    public enum ScndCnpCalcSituationType
    {
        EveryDay = 0,
        BeginOfPeriode = 1,
        EndOfPeriode = 2
    }
    /// <summary>
    /// نوع مفهوم
    /// </summary>
    public enum ScndCnpPairableType
    {
        /// <summary>
        /// PairableSecondaryConcept
        /// </summary>
        PSC = 0,
        /// <summary>
         /// NonPairableSecondaryConcept
        /// </summary>
        NPSC = 1
    }

    public enum ScndCnpPeriodicType
    {
        NoPeriodic = 0,
        /// <summary>
        /// مفهوم دوره ای که از جمع مفاهیم روزانه بدست می آید
        /// </summary>
        Periodic = 1,
        /// <summary>
    }

    public enum ScndCnpPersistSituationType
    {
        Persistable = 1,
        NotPersist = 2,
        AlwaysPersist = 3
    }

    public enum NotificationsServices
    {
        EMAILRequestStatus = 1, EMAILTrafficItems = 2, EMAILKartabl = 3,
        SmsRequestStatus = 4, SmsTrafficItems = 5, SmsKartabl = 6
    }

    public enum PersonReservedFieldsType
    {
        TextValue = 1, ComboValue = 2
    }

    public enum PersonReservedFieldComboItems
    {
        R16, R17, R18, R19, R20
    }

    public enum ConceptReservedFields
    {
        ReserveField1, ReserveField2, ReserveField3, ReserveField4, ReserveField5,
        ReserveField6, ReserveField7, ReserveField8, ReserveField9, ReserveField10
    }

    public enum RuleParametersValidationType
    {
        RuleParametersNoRegulation,
        RuleParametersDateRangesNoCover
    }

    public enum TrafficTransferType
    {
        Backward,
        Forward
    }

    public enum TrafficTransferMode
    {
        Normal,
        RecordBase,
        IdentifierBase
    }

    public enum ServiceAuthorizeType
    {
        Legal,
        Illegal
    }

    public enum ManagerCreator
    {
        Personnel,
        OrganizationPost,
        None
    }

    public enum SystemReportType
    {
        SystemBusinessReport,
        SystemEngineReport,
        SystemWindowsServiceReport,
        SystemUserActionReport
    }

    public enum DataAccessLevelOperationType
    {
        Single,
        Group
    }

    public class SystemReportTypeFilterConditions
    {
        public string SearchTerm { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public enum AttachmentType
    {
        Request,
        Personnel,
        Authorization,
        ReferredPerson,
        Office,
        DeliveryItem
    }

    public enum ImperativeRequestLoadState
    {
        Normal,
        Applied,
        NotApplied
    }

    public class Range 
    {
        public Range()
        {

        }
        //public Range(DateTime from,DateTime to) 
        //{
        //    From = from;
        //    To = to;
        //}
        public Range(DateTime from, DateTime to, decimal aditionalField)
        {
            From = from;
            To = to;
            AditionalField = aditionalField;
        }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal AditionalField { get; set; }
    }

    public enum ArchiveExistsConditions
    {
        NotExists = 0, SomeExists = 1, AllExists = 2
    }

    public enum ConceptDataType
    {
        Int = 0, Hour = 1
    }

    public enum RoleCustomCodeType
    {
        SystemTechnicalAdmin = 1, SystemAdmin = 2, Manager = 3, Substitute = 4, Operator = 5, User = 6
    }

    public enum SubSystemIdentifier
    {
        TimeAtendance = 1, Clientele = 2
    }


    /// <summary>
    /// حالت درخواست آفیش روز
    /// </summary>
    public enum RequestOffishStateInDay
    {
        /// <summary>
        /// همه
        /// </summary>
        All,
        /// <summary>
        /// بسته
        /// </summary>
        Closed,
        /// <summary>
        /// در فرآیند
        /// </summary>
        InProcces,
    }

    /// <summary>
    /// بمنظور کلاس بندی رکوردهای استخراج شده از 
    /// دیتابیس بمنظور بررسی سطح دسترسی کاربر استفاده میشود
    /// </summary>
    public class UserAuthorizationProxy
    {
        public string Username { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
        public bool Allow { get; set; }

        public override string ToString()
        {
            return String.Format("{0} , {1}", Method, Allow);
        }
    }
    public enum DesignedReportTypeEnum
    {
        Daily, Monthly
    }
    public enum DesignedReportParameterType
    {
        DateRange, FromToDate
    }
    public enum ClientelePersonTrafficCaller
    {
        Contractor,
        Office
    }

    public enum StringGeneratorExceptionType
    {
        ClientAttachments,
        ReportCondition,
        Shifts
    }
   


    public enum DeliveryItemLoadState
    {
        None,
        Returned,
        NotReturned
    }

    

}