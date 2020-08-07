using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Exceptions.UI
{
    public enum ExceptionResourceKeys
    {
        #region TA
		
        #region General
        ItemNotExist,

        #endregion

        #region Department
        DepartmentRepeatedName, DepParentIDRequierd, DepUsedByPersons,
        DepNameRequierd, DepParentNotExists, DepartCustomCodeRepeated,
        DepartmentRootDeleteIllegal,ChildsDepUsedByPersons,
        #endregion

        #region Shift
        ShiftColorRepeated, ShiftNameRepeated, ShiftUsedInWorkGroup,
        ShiftPairNull, ShiftPairFromToEmpty, ShiftPairHasIntersect,
        ShiftNameRequierd, ShiftColorRequierd, ShiftFromAndToAreEquals,
        ShiftFromGreaterThanTo, ShiftItemNotExists, ShiftPairItemNotExists,
		ShiftTypeRequierd, ShiftCustomCodeRepeated, ShiftPairTypeIsEmpty,
        #endregion

        #region NobatKari
        NobatKariNameEmpty, NobatKariRepeated, NobatKariUsedByShift,
        NobatKariCustomCodeRepeated,
        #endregion

        #region Workgroup
        WorkGroupNameRequierd, WorkGroupNameRepeated,
        WorkGroupCustomCodeRepeated, WorkGroupUsedByPerson,
        #endregion

        #region organizationUnit
        OrganizationUnitUsedByPerson, OrganizationUnitNameRepeated, OrganizationUnitParentIDRequierd,
        OrganizationUnitNameRequierd, OrganizationUnitParentNotExists, OrganizationUnitCustomCodeRepeated,
        OrganizationUnitRootDeleteIllegal,ChildOrganizationUnitUsedByPerson,
        #endregion

        #region Employment
        EmploymentTypeNameRequierd, EmploymentTypeNameRepeated, EmploymentTypeCustomCodeRepeated,
        EmploymentTypeUsedByPerson,
        #endregion

        #region RuleCategory
        RuleCategoryNameRequierd, RuleCategoryNameRepeated,
        RuleCategroyInsertedRuleIsEmpty, RuleCategoryRootDeleteIllegal,
        RuleCategoryUsedByPerson,

        #endregion

        #region RuleParameter
        AssignParameterDateHasIntersect, AssignParameterDateIsInvalid,
        AssignParameterFromDateGreaterThanToDate, AssignParameterRuleIDInvalid,
        AssignParameterFromDateAndToDateAreEquals,
        #endregion

        #region RuleViewer
        RuleCategoryIdIsInValid,
        #endregion

        #region CalculationDateRange
        DateRangesCountNotEqualToTwelve, DateRangesGroupNameRequierd,
        DateRangesGroupNameRepeated, DateRangesMustHaveConcept,
        DateRangesUsedByPerson, DateRangesCopyIdIsNotValid,
        #endregion

        #region ControlStation
        StationNameRequierd, StationNameRepeated, StationCustomCodeRepeated,
        StationUsedByPerson, StationUsedByMachine,
        #endregion

        #region Person
        PersonNameRequied, PersonLastNameRequierd,
        PersonSexRequierd, PersonMarriedRequierd,
        PersonCodeRequierd, PersonDepartmentRequierd,
        PersonWorkGroupRequierd, PersonRuleGroupRequierd,
        PersonEmploymenttypeRequierd, PersonDateRangeRequierd,
        PersonBarcodeRequierd, PersonMeliCodeRepeated,
        PersonBarcodeRepeated, PersonCartNumberRepeated,
        PersonEmployeeNumRepeated, PersonMeliCodeInValid,
        PersonBarcodeInValid, PersonCartNumInValid,
        PersonShenasnameCodeInValid, PersonEmploymentFromDateGreaterThanToDate,
		PersonUIValidationRequierd, PersonBirthDateIsNotValid, PersonStartEmployeeDateIsNotValid,
        PersonEndEmployeeDateIsNotValid,
        #endregion

        #region PersonReservedFields
		PrsRsvFldLableIsEmpty, PrsRsvFldComboValueRepeated, PrsRsvFldComboValueUsedByPerson,
        PrsRsvFldComboValueIsEmpty,
        #endregion

        #region AssignWorkGroup
        AssignWorkGroupPersonIdNotExsits, AssignWorkGroupIdNotExsits,
        AssignWorkGroupSmallerThanStandardValue,
        #endregion

        #region AssignRule
        AssignRulePersonIdNotExsits, AssignRuleIdNotExsits,
        AssignRuleDateFormatProblem, AssignRuleDateSmallerThanStandardValue,
		AssignRuleFromDateGreaterThanToDate, AssignRuleDateHasConfilict,
        #endregion

        #region AssignRange
        AssignRangePersonIdNotExsits, AssignRangeGroupIdNotExsits,
		AssignRangeSmallerThanStandardValue, AssignRangeFirstMustBeFromStartYear,
        AssignRangeDateIsRepeated,
        #endregion

        #region WorkGroupCalendar
		WorkGroupCalendarDublicateDate, WorkGroupCalendarPriodIsEmpty, WorkGroupCalendarPriodDateIsNotValid,
        #endregion

        #region Manager
        ManagerOwnerNotSpecified, ManagerUsedByFlow,
        #endregion

        #region Flow
        FlowNameRepeated, FlowNameRequierd, FlowAccessGroupRequierd, FlowAccessGroupRepeated,
        FlowMustHaveOneManagerFlow, FlowPersonOrOrganizationMustSpecified, FlowGroupNameRequired, FlowGroupNameRepeated,
        #endregion

        #region Pishcart
        PrecardNameRequierd, PrecardNameRepeated, PrecardGroupRequierd,
        PrecardInvalidStatus, PrecardCodeRequierd, PrecardCodeRepeated,
        PrecardUsedBySubestitute, PrecardUsedByBasicTraffic, PrecardIsLock,
        PrecardNotSpec,
        #endregion

        #region Ilness
        IllnessNameRequierd, IllnessNameRepeated,
        #endregion
        #region
        GradeNameRequierd, GradeNameRepeated, GradeUsedByPersons,
        #endregion
        #region Doctor
        DoctorLastNameRequierd, DoctorNezampezeshkiRepeated, DoctorNazampezeshkiRequired,
        #endregion

        #region Precard Access Group
        AccessGroupNameRequierd, AccessGroupNameRepeated,
        AccessGroupUsedByFlow,
        #endregion

        #region MonthlyOperation
        MonthlyOpCurentUserIsNotValid, MonthlyOpIDMustSpecified,
        #endregion

        #region Request
        RequestUsedByFlow, RequestRepeated, RequestImperativeRepeated, RequestDateShouldNotEmpty,
        RequestFromDateGreaterThanToDate, RequestFromTimeGreaterThanToTime,
        RequestTimeShouldNotEmpty, RequestFromToDateNotEqual, RequestPrecardIsEmpty,
        RequestTimeIsNotValid, RequestPersonRequierd, RequestMonthIsEmpty, RequestYearIsEmpty,
        RequestIsNotAllowed,
        #endregion

        #region Role
        RoleNameRequierd, RoleNameReplication, RoleCodeReplication, RoleUSedByUser,
        RoleParentNotSpecified, RoleRootDeleteIllegal,
        #endregion

        #region User
        UsernameReplication, UserPasswordIsNull, UserConfirmPasswordNotEqual, UsernameNotProvided,
		UserPersonIsNotSpecified, UserRoleIsNotSpecified, UserPasswordIsNotCurrent,
        #endregion

        #region Report
        ReportRepeatedName, ReportParentIDRequierd, ReportRootDeleteIllegal,
        ReportNameRequierd, ReportParentNotExists, ReportFileNotSpecified,
		ReportCanNotBeParent, ReportParameterActionIdIsEmpty, ReportParametersIsEmpty,
        ReportParameterPersonIsEmpty,NoColumnSelectedForReport,

        #endregion

        #region ReportFile
        ReportFileRepeatedName, ReportFileParentIDRequierd, ReportFileNameRequierd,

        #endregion

        #region Calendar
        CalendarNameRequierd, CalendarNameRepeated, CalendarCustomCodeRequierd,
        CalendarCustomCodeRepeated, CalendarTypeUsedInHolidayTemplates,
        #endregion

        #region DutyPalce
        DutyPlaceNameRequierd, DutyPlaceNameRepeated, DutyPlaceCustomCodeRepeated,
        DutyPlaceUsedByRequest, DutyPlaceParentRequest,
        #endregion

        #region Clock
        ClockNameRequierd, ClockNameRepeated, ClockCustomCodeRepeated,
        ClockTypeRequierd, ClockControStationRequierd,
        #endregion

        #region Substitute
        SubstituteManagerRequiered, SubstitutePersonRequiered, SubstituteDateRequired, SubstituteFromDateGreaterThanToDate,
		SubstituteIsNotSpecified, SubstituteUpdateFlowAndSubstituteIdRequeiered, SubstitutePersonMustNotEqualtoManager,
        #endregion

        #region Operator
		OperatorPersonIsRequierd, OperatorFlowIsRequierd, OperatorRepeated,
        #endregion

        #region RemainLeave
        RemainLeaveExists, RemainLeavePersonNotSelected, RemainTransferFromToYearDiffrenceMoreThanOne, RemainTransferFromYearIsNotExists,
        #endregion

        #region Help
        HelpIdNotSpecified,
        #endregion

        #region Public Message
        PublicMessageContentRequierd, PublicMessageSubjecttRequierd,
        #endregion

        #region UIValidationGroup
        ValidationGroupNameIsEmpty, ValidationGroupNameIsRepeated, ValidationGroupRulesIsEmpty, UIValidationGroupUsedByPerson,
        #endregion

        #region BasicTraffic
        TrafficDateRequierd, TrafficTimeRequierd, TrafficPrecardRequierd, TrafficIsRepeated,
        TrafficPersonRequierd,
        #endregion

        #region ExceptionShift
        ExceptionShiftPersonIdRequierd, ExceptionShiftShiftIdRequierd, ExceptionShiftWorkGroupIdRequierd, ShiftWithThisCustomCodeNotExists,

        #endregion
         
        #region Validation
        UIValidation_R7TrafficRequestMaxCount, UIValidation_R8TrafficRequestDayTimeFinished, 
        UIValidation_R4_LockCalculationFromCurrentMonth, UIValidation_R5_LockCalculationFromBeforeMonth,
        UIValidation_R6_LockCalculationFromDate,
        UIValidation_R9_Before, UIValidation_R9_After, UIValidation_R10_Before, UIValidation_R10_After,
        UIValidation_R11_Before, UIValidation_R11_After, UIValidation_R12_Before, UIValidation_R12_After,
        UIValidation_R13_Before, UIValidation_R13_After, UIValidation_R14_Before, UIValidation_R14_After,
        UIValidation_R15_Before, UIValidation_R15_After, UIValidation_R16RequestMaxCount,
        UIValidation_R17RequestMaxCount, UIValidation_R18RequestMaxCount, UIValidation_R19RequestMaxCount,
        UIValidation_R20RequestMaxCount, UIValidation_R21RequestMaxCount, UIValidation_R22RequestMaxHrour,
		UIValidation_R23RequestMaxCount, UIValidation_R27RequestMaxHrourDasturyOverwork, UIValidation_R27RequestMustBeOperator,
        UIValidation_R27RequestMaxCount, UIValidation_R28RequestMaxValue, UIValidation_R30DutyPlace, UIValidation_R31Doctor, UIValidation_R32Illenss,
        UIValidation_R33LeaveRemain, UIValidation_R37OperatorRequestMaxCount, UIValidation_R38RequestMaxCount, R39_ArchiveCalculationKaheshi,





        #endregion

        #region USerSetting
		UserSet_EmailTimeIsNotValid, UserSet_EmailTimeLessThanMin, UserSet_SMSTimeIsNotValid, UserSet_SMSTimeLessThanMin,DashboardIsDuplicated,
        #endregion

        #region Corporation
        CorporationNameRequierd, CorporationNameRepeated,
        #endregion

        #region BSecondaryConceptUserDefined

        BSecondaryConceptRequierd,
        BSecondaryConceptCustomeCategoryParentExpressionRequierd,
        
        BSecondaryConceptNameRequierd, BSecondaryConceptNameRepeated,
        BSecondaryConceptCodeRequierd, BSecondaryConceptCodeRepeated,
        BSecondaryConceptColorRequierd, BSecondaryConceptColorRepeated,

        BSecondaryConceptTypeRequierd,
        BSecondaryConceptPeriodicTypeRequierd,
        BSecondaryConceptCalcSituationTypeRequierd,
        BSecondaryConceptPersistSituationTypeRequierd,
        BSecondaryConceptCustomeCategoryCodeRequierd,

        
        #endregion

        #region BSecondaryConceptUserDefined

        BRuleNameRequierd, BRuleNameRepeated,
        BRuleCodeRequierd, BRuleCodeRepeated,

        #endregion

        #region Archive Concepts

        ArchiveDataTypeIsNotValid,

        #endregion

        #region Person Param Value

		ParamFieldIsEmpty, ParamPersonIsEmpty, ParamFromDateGreaterThanToDate, ParamFieldNameIsEmpty, ParamFieldKeyIsEmpty, ParamFieldKeyRepeated,

        #endregion 

        #region ShiftPairType
        ShiftPairTypeNameIsEmpty, ShiftPairTypeUsedByShiftPair,
        ShiftPairTypeCustomCodeIsEmpty, ShiftPairTypeCustomCodeRepeated,
        #endregion

        #region SpecialKartable
           ManagerIsInvalid,
        #endregion
          
           #region OperatorPermit
           CurrentUserIsNotManager,
           CurrentUserIsNotOperator,
           ManagersCountInOperatorFlowIsGreaterThanOne,
        #endregion

        #endregion

           #region Clientele

           #region DepartmentPosition
           DepartmentPositionUnitNameRequired, DepartmentPositionLocationRequired, DepartmentPositionNameIsDuplicated,
        #endregion

        #region Contractor
		ContractorNameIsEmpty, ContractorNameRepeated, ContractorMeetingPersonIsEmpty,
		ContractorFromDateIsGreaterThanContractorToDate, ContractorDepartmentIsEmpty,
        #endregion

        #region ContractorEmp
        ContractorEmpNameIsEmpty, ContractorEmpNationalCodeRepeated,
        #endregion

        #region
		EquipmentNameIsEmpty,
        #endregion


        #region Car
		CarNameIsEmpty, CarCodeIsEmpty, CarCodeRepeated,
        #endregion

        #region Equipment
        EquipNameIsEmpty, EquipNameRepeated,
        #endregion

        #region EquipmentBlackList
		EquipBLNameIsEmpty, EquipBLNameRepeated, EquipBLCustomCodeRepeated, EquipBLFromDateIsEmpty, EquipBLToDateIsEmpty, EquipBLFromDateIsGreaterThanToDate,
        #endregion

        #region ClientelePerson
		CLPrsNameIsEmpty, CLPrsNationalCodeIsEmpty, CLPrsNationalCodeRepeated, CLPrsSexIsEmpty,
        #endregion

		#region Office
		OfficeDepartmentIsEmpty, OfficeMeetingPersonIsEmpty, OfficeReferredPersonNameIsEmpty, OfficeReferredPersonNationalCodeIsEmpty, OfficeRequestOfficeTypeIsEmpty,
		OfficeReferredPersonSexIsEmpty, OfficeActiveDirectoryUserNameIsEmpty, OfficeFromDateIsEmpty, OfficeToDateIsEmpty,
		OfficeMeetingPersonIsEqualtoOfficeSubstituteMeetingPerson, OfficeFromDateIsGreaterThanOfficeToDate, OfficeTypeNotSpec,
		#endregion

		#region OfficeType
		OfficeTypeNameIsEmpty,
		OfficeTypeNameIsDuplicated,
		OfficeTypeCustomCodeIsEmpty,
		OfficeTypeCustomCodeIsDuplicated,
		#endregion

        #region PersonBlackList
		CPBL_PersonIsEmpty, CPBL_FromDateIsEmpty, CPBL_ToDateIsEmpty, CPBL_FromDateIsGreaterThanToDate, CPBL_IsDuplicated,
		#endregion

		#region BPersonTraffic
		CPTA_PersonIsEmpty,
		CPTA_OffishRequestAndContractorIsEmpty,
		CPTA_FromDateIsEmpty,
		CPTA_FromDateGreateThanToDate,
		CPTA_FromTimeGreateThanToTime,
		CPTA_InvalidDateTimeRange,
		CPTA_InvalidDateRange,
		CPTA_InvalidTimeRange,

		CPTA_DuplicatedTraffic,

		CPTA_ErrorOnDeleting,



		#endregion


		#region BDeliveryItem

		DeliveryItem_TitleEmpty,
        DeliveryItem_OffishOrContractorIsEmpty,
        DeliveryItem_DeliveryItemIdListIsEmpty,
        DeliveryItem_ClientelePersonListIsEmpty,
        DeliveryItem_ClientelePersonListHasMoreThanOneItem,


        #endregion

		BExpressionRequiedScriptBeginFa,

        #region BRuleTempParameter

        BRuleTempParameterNameRequied,
        BRuleTempParameterRuleRequied,
        BRuleTempNameRepeated,
        BRuleTempShouldBeUserDefined,

        #endregion

        #endregion


    };

    public enum UIExceptionTypes 
    {
		Fatal, Reload, ShowMessage
    };

    public enum UIFatalExceptionIdentifiers
    {
        NONE = 1000,
        IllegalServiceAccess = 1001,
        DepartmentRootMoreThanOne = 1002,
        OrganizationUnitRootMoreThanOne = 1003,
        RuleCategoryRootMoreThanOne = 1004,
        RuleTypeNodesMoreThanSix = 1005,
        RuleCategoryInsertedRulesIsNULL = 1006,
        PersonDetailNotExistsInDatabase = 1007,
        UpdatePersonImageHasError = 1008,
        ShiftColorIsNotUnique = 1009,
        ManagerOrganizationUnitProblem = 1010,
        UnderManagmentDepartmentNull = 1011,
        UserSettingsLanguageOrUserNotExists = 1012,
        PersonDateRangeIsNotDefiend = 1013,
        ExpectedPrecardDoesNotExists = 1014,
        UsualPrecardIsNotExistsInDatabase = 1015,
        RoleRootMoreThanOne = 1016,
		ResourceControlsWithRepeatedId = 1017,
		ResourceRootMoreThanOne = 1018,
        ReportRootMoreThanOne = 1019,
		ReportParameterParsingIsNotMatch = 1020,
        DutyPlaceRootMoreThanOne = 1021,
		OperatorOrganizationUnitProblem = 1022,
        LeaveBudgetRecordsCountInDatabaseIsNotValid = 1023,
		LeaveLCRDoesNotExists = 1024,
		HelpFormKeyDoesNotExists = 1025,
		HelpRootIsMorThanOne = 1026,
        UserSettingsSkinOrUserNotExists = 1027,
        ReportParameterParsingSplitSign = 1028,
        UIValidationParameterCount = 1029,
        UIValidationParameterNotfound = 1030,
		CurrentUserIsNotValid = 1031,
		PersonReservedFiledsCount = 1032,
		UIValidationMinOneMustBeActive = 1033,
		PrecardGroupIsNull = 1034,
		OfficeTypeGroupIsNull = 1035,


    };
}
