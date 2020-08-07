using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.MustRemoved;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.UIValidation;

namespace GTS.Clock.Model
{
    public class Person : IEntity, IDisposable
    {
        #region Variablies

        private BasicTrafficController basicTrafficController = null;
        private ScndCnpValueCollection<string, BaseScndCnpValue> scndCnpValueList;
        private PersonRangeAssignment currentActiveDateRangeGroup;

        //private ControlStation controlStation;
        private Department department;
        private OrganizationUnit organizationUnit;
        private EmploymentType employmentType;
        private Grade grade;

        #endregion

        #region Constructor

        public Person()
        {
            EmploymentDate = Utility.GTSMinStandardDateTime;
            EndEmploymentDate = Utility.GTSMinStandardDateTime;
            this.ShiftPairsDictionary = new Dictionary<decimal, IList<ShiftPair>>();
            this.AsgScndCnpRangeDictionary = new Dictionary<decimal, IList<AssignedScndCnpRange>>();
            this.PeriodicScndCnpValueDictionary = new Dictionary<string, PersistedScndCnpPrdValue>();
            this.scndCnpValueList = new ScndCnpValueCollection<string, BaseScndCnpValue>();
            this.LeaveSetting = new LeaveSetting();
        }

        #endregion

        #region Properties



        public virtual Decimal ID
        {
            get;
            set;
        }

        public virtual String FirstName { get; set; }

        public virtual String LastName { get; set; }

        public virtual String BarCode { get; set; }

        /// <summary>
        /// همان بارکد که به این نام خوانده میشود
        /// </summary>
        public virtual String PersonCode
        {
            get { return this.BarCode; }
            set { this.BarCode = value; }
        }

        public virtual Boolean Active { get; set; }

        public virtual Boolean IsDeleted { get; set; }

        public virtual String CardNum { get; set; }

        public virtual String EmploymentNum { get; set; }

        //public virtual ControlStation ControlStation
        //{
        //    get
        //    {
        //        if (controlStation == null)
        //        {
        //            controlStation = new ControlStation();
        //        }
        //        return controlStation;
        //    }
        //    set
        //    {
        //        controlStation = value;
        //    }
        //}

        public virtual DateTime EmploymentDate { get; set; }

        public virtual DateTime EndEmploymentDate { get; set; }

        /// <summary>
        /// جهت استفاده در واسط کاربر
        /// </summary>
        public virtual String UIEmploymentDate { get; set; }

        /// <summary>
        /// جهت استفاده در واسط کاربر
        /// </summary>
        public virtual String UIEndEmploymentDate { get; set; }

        public virtual PersonSex Sex { get; set; }

        /// <summary>
        /// ط¬ظ‡طھ ط§ط³طھظپط§ط¯ظ‡ ط¯ط± ظˆط§ط³ط· ع©ط§ط±ط¨ط±
        /// </summary>
        public virtual String SexTitle
        {
            get { return this.Sex.ToString("G"); }
        }

        public virtual String Education { get; set; }

        public virtual MaritalStatus MaritalStatus { get; set; }

        /// <summary>
        /// ط¬ظ‡طھ ط§ط³طھظپط§ط¯ظ‡ ط¯ط± ظˆط§ط³ط· ع©ط§ط±ط¨ط±
        /// </summary>
        public virtual String MaritalStatusTitle
        {
            get
            {
                return this.MaritalStatus.ToString("G");
            }
        }

        /// <summary>
        /// ط¬ظ‡طھ ط±ط§ط­طھغŒ ع©ط§ط±
        /// </summary>
        public virtual string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public virtual DateRange CalcDateZone
        {
            get;
            set;
        }

        public virtual IList<PersonRuleCatAssignment> PersonRuleCatAssignList
        {
            get;
            set;
        }

        public virtual IList<PersonRangeAssignment> PersonRangeAssignList
        {
            get;
            set;
        }

        public virtual IList<AssignedRule> AssignedRuleList
        {
            get;
            set;
        }

        /// <summary>
        /// پارامتر کل قوانین
        /// </summary>
        public virtual IList<AssignedRuleParameter> RuleParameterList
        {
            get;
            set;
        }

        public virtual ScndCnpValueCollection<string, BaseScndCnpValue> ScndCnpValueList
        {
            get
            {
                return this.scndCnpValueList;
            }
            set
            {
                this.scndCnpValueList = value;
            }
        }

        public virtual PersonDetail PersonDetail
        {
            get
            {
                return this.PersonDetailList.FirstOrDefault();
            }
            set
            {
                if (this.PersonDetailList == null)
                {
                    this.PersonDetailList = new List<PersonDetail>();
                    this.PersonDetailList.Add(value);
                }
            }
        }

        public virtual PersonTASpec PersonTASpec
        {
            get
            {
                if (this.PersonTASpecList == null || this.PersonTASpecList.Count == 0)
                {
                    this.PersonTASpecList = new List<PersonTASpec>();
                    this.PersonTASpecList.Add(new PersonTASpec() { ID = this.ID });
                }
                return this.PersonTASpecList.FirstOrDefault();
            }
            set
            {
                if (this.PersonTASpecList == null)
                {
                    this.PersonTASpecList = new List<PersonTASpec>();
                    this.PersonTASpecList.Add(value);
                }
            }
        }

        public virtual PersonCLSpec PersonCLSpec
        {
            get
            {
                if (this.PersonCLSpecList == null || this.PersonCLSpecList.Count == 0)
                {
                    this.PersonCLSpecList = new List<PersonCLSpec>();
                    this.PersonCLSpecList.Add(new PersonCLSpec() { ID = this.ID });
                }
                return this.PersonCLSpecList.FirstOrDefault();
            }
            set
            {
                if (this.PersonCLSpecList == null)
                {
                    this.PersonCLSpecList = new List<PersonCLSpec>();
                    this.PersonCLSpecList.Add(value);
                }
            }
        }

        public virtual IList<PersonTASpec> PersonTASpecList { get; set; }

        public virtual IList<PersonCLSpec> PersonCLSpecList { get; set; }

        public virtual IList<PersonDetail> PersonDetailList { get; set; }

        public virtual IList<ProceedTraffic> ProceedTrafficList
        {
            get;
            set;
        }

        public virtual IList<Permit> PermitList
        {
            get;
            set;
        }

        public virtual IList<BasicTraffic> BasicTrafficList
        {
            get;
            set;
        }

        public virtual IList<LeaveIncDec> LeaveIncDecList
        {
            get;
            set;
        }

        public virtual IList<UsedLeaveDetail> UsedLeaveDetailList
        {
            get;
            set;
        }

        public virtual IList<LeaveCalcResult> LeaveCalcResultList
        {
            get;
            set;
        }

        public virtual IList<Budget> BudgetList
        {
            get;
            set;
        }

        public virtual LeaveSetting LeaveSetting
        {
            get;
            set;
        }

        public virtual IList<LeaveYearRemain> LeaveYearRemainList
        {
            get;
            set;
        }

        public virtual IList<CurrentYearBudget> CurrentYearBudgetList
        {
            get;
            set;
        }

        public virtual IList<TrafficSettings> TrafficSettingsList
        {
            get;
            set;
        }

        public virtual IDictionary<string, PersistedScndCnpPrdValue> PeriodicScndCnpValueDictionary
        {
            get;
            set;
        }

        public virtual IList<AssignedWGDShift> AssignedWGDShiftList
        {
            get;
            set;
        }

        public virtual IList<User> UserList
        {
            get;
            set;
        }

        /// <summary>
        /// ط¬ظ‡طھ ع©ط§ط±ط¨ط±غŒ ط±ط§ط­طھط± ط²غŒط±ط§ ط´ط®طµ ظ…ط¹ظ…ظˆظ„ط§ غŒع© ظ†ط§ظ… ع©ط§ط±ط¨ط±غŒ ط¯ط§ط±ط¯
        /// </summary>
        public virtual GTS.Clock.Model.Security.User User
        {
            get
            {
                User user = new User();
                if (!Utility.IsEmpty(this.UserList))
                {
                    User tmpUser = this.UserList.First();
                    user = new User() { ID = tmpUser.ID, UserName = tmpUser.UserName, Person = tmpUser.Person, Role = tmpUser.Role, UserSetting = tmpUser.UserSetting };
                }
                if (user.Role == null)
                {
                    user.Role = new Role();
                }
                return user;
            }
        }


        public virtual Department Department
        {
            get
            {
                if (department == null)
                    department = new Department();
                return department;
            }
            set
            {
                department = value;
            }
        }

        public virtual EmploymentType EmploymentType
        {
            get
            {
                if (employmentType == null)
                {
                    employmentType = new EmploymentType();
                }
                return employmentType;
            }
            set
            {
                employmentType = value;
            }
        }
        public virtual Grade Grade
        {
            get
            {
                if (grade == null)
                {
                    grade = new Grade();
                }
                return grade;
            }
            set
            {
                grade = value;
            }
        }
        /// <summary>
        /// خصوصیتی که در نگاشت شخص به جستجوی مورد نظر متصل می شود
        /// </summary>
        public virtual IList<AssignedScndCnpRange> AssignedScndCnpRangeList { get; set; }

        /// <summary>
        /// خصوصیتی که در مقداردهی اولیه کلاس پرسنل به منظور بالا بردن سرعت واکشی محدوده محاسبات مقداردهی می شود
        /// </summary>
        public virtual IDictionary<decimal, IList<AssignedScndCnpRange>> AsgScndCnpRangeDictionary { get; set; }

        /// <summary>
        /// ساختاری برای پردازش ترددهای پردازش نشده است
        /// </summary>
        public virtual BasicTrafficController BasicTrafficController
        {
            get
            {
                if (this.basicTrafficController == null)
                {
                    basicTrafficController = new BasicTrafficController(this.BasicTrafficList.ToList());
                }
                return basicTrafficController;
            }
        }

        public virtual IList<PersonWorkGroup> AssignedWorkGroupList
        {
            get;
            set;
        }

        public virtual IList<AssignWorkGroup> PersonWorkGroupList { get; set; }

        protected virtual IList<OrganizationUnit> OrganizationUnitList { get; set; }

        /// <summary>
        /// این رابطه بصورت منطقی بدین صورت است که رابطه شخص و پست سازمانی یک به یک است
        /// </summary>
        public virtual OrganizationUnit OrganizationUnit
        {
            get
            {
                if (this.OrganizationUnitList != null && OrganizationUnitList.Count > 1)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.PersonDetailNotExistsInDatabase, String.Format("تعداد سمت سازمانی شخض {0} در دیتابیس بیش از یک عدد است", this.PersonCode), "");
                }
                if (this.OrganizationUnitList != null && this.OrganizationUnitList.Count == 1)
                {
                    return this.OrganizationUnitList[0];
                }
                return new OrganizationUnit();
            }
            set
            {
                if (this.OrganizationUnitList == null)
                {
                    this.OrganizationUnitList = new List<OrganizationUnit>();
                }
                else
                    this.OrganizationUnitList.Clear();
                if (value == null)
                    this.OrganizationUnitList = null;
                else
                {
                    this.OrganizationUnitList.Add(value);
                }
            }
        }

        public virtual IList<ShiftException> ShiftExceptionList
        {
            get;
            set;
        }

        public virtual IList<RequestFlow.Manager> ManagerList
        {
            get;
            set;
        }

        /// <summary>
        /// از این خصوصیت برای کش "زوج"های یک شیفت اصتفاده می شود
        /// </summary>
        public virtual IDictionary<decimal, IList<ShiftPair>> ShiftPairsDictionary
        {
            get;
            set;
        }

        /// <summary>
        /// in BPerson.ApplyCultureAndFixCurrentObject this property have been set
        /// </summary>
        public virtual string CurrentActiveWorkGroup
        {
            get;
            set;
        }

        /// <summary>
        /// in BPerson.ApplyCultureAndFixCurrentObject this property have been initialized
        /// </summary>
        public virtual string CurrentActiveRuleGroup
        {
            get;
            set;
        }

        /// <summary>
        /// in BPerson.ApplyCultureAndFixCurrentObject this property have been set
        /// </summary>
        public virtual string CurrentActiveDateRangeGroup
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// in BPerson.ApplyCultureAndFixCurrentObject this property have been initialized
        /// </summary>
        public virtual PersonRangeAssignment CurrentRangeAssignment
        {
            get;
            set;
        }

        public virtual Department ExtDepartment
        {
            get;
            set;
        }

        //public virtual UIValidationGroup UIValidationGroup { get; set; }
        #endregion

        #region Methods

        #region ProceedTraffic

        /// <summary>
        /// تردد پرسنل در تاریخ درخواست شده را برمی گرداند
        /// </summary>
        /// <param name="Date">تاریخ تردد مورد نظر</param>
        /// <returns>یک نمونه از کلاس تردد شامل تردد تاریخ مشخص شده</returns>
        public virtual ProceedTraffic GetProceedTraficAllByDate(DateTime Date)
        {
            //قبلا تاریخ انتهای تردد پردازش شده هم در نظر گرفته میشد
            List<ProceedTraffic> list1 = this.ProceedTrafficList
                             .Where(x => x.FromDate == Date).ToList();

            ProceedTraffic procTraf = new ProceedTraffic();
            foreach (ProceedTraffic pro in list1)
            {
                foreach (ProceedTrafficPair pair in pro.Pairs)
                {
                    procTraf.Pairs.Add(pair);
                }
                procTraf.HasHourlyItem = procTraf.HasHourlyItem || pro.HasHourlyItem;
                procTraf.HasDailyItem = procTraf.HasDailyItem || pro.HasDailyItem;
                procTraf.IsNotDaily = procTraf.IsNotDaily && procTraf.IsNotDaily;
            }
            if (list1.Count > 0)
            {
                procTraf.FromDate = list1[0].FromDate;
                procTraf.ToDate = list1[0].ToDate;
                procTraf.Person = list1[0].Person;
                return procTraf;
            }
            else
            {
                return new ProceedTraffic();
            }
        }

        /// <summary>
        /// تردد پرسنل در تاریخ درخواست شده را برمی گرداند
        /// </summary>
        /// <param name="Date">تاریخ تردد مورد نظر</param>
        /// <returns>یک نمونه از کلاس تردد شامل تردد تاریخ مشخص شده</returns>
        public virtual ProceedTraffic GetProceedTraficByDate(DateTime Date)
        {
            //قبلا تاریخ انتهای تردد پردازش شده هم در نظر گرفته میشد
            List<ProceedTraffic> list1 = this.ProceedTrafficList
                             .Where(x => x.FromDate == Date).ToList();

            ProceedTraffic procTraf = new ProceedTraffic();
            foreach (ProceedTraffic pro in list1)
            {
                foreach (ProceedTrafficPair pair in pro.Pairs)
                {
                    if (pair.IsFilled)
                    {
                        procTraf.Pairs.Add(pair);
                    }
                }
                procTraf.HasHourlyItem = procTraf.HasHourlyItem || pro.HasHourlyItem;
                procTraf.HasDailyItem = procTraf.HasDailyItem || pro.HasDailyItem;
                procTraf.IsNotDaily = procTraf.IsNotDaily && procTraf.IsNotDaily;
            }
            if (list1.Count > 0)
            {
                procTraf.FromDate = list1[0].FromDate;
                procTraf.ToDate = list1[0].ToDate;
                procTraf.Person = list1[0].Person;
                return procTraf;
            }
            else
            {
                return new ProceedTraffic();
            }
        }


        /// <summary>
        /// تردد پرسنل در تاریخ درخواست شده را برمی گرداند
        /// </summary>
        /// <param name="Date">تاریخ تردد مورد نظر</param>
        /// <returns>یک نمونه از کلاس تردد شامل تردد تاریخ مشخص شده</returns>
        public virtual ProceedTraffic GetProceedTraficByDate(PersianDateTime Date)
        {
            return GetProceedTraficByDate(Date.GregorianDate);
        }

        #endregion

        #region ScndCnpValue&ScndCnpRange

        /// <summary>
        /// مقدار مفهوم مشخص شده در تاریخ مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="scndCnp">مفهومی که مقدار آن مدنظر است</param>
        /// <param name="Date">تاریخی که مقدار مفهوم در آن مدنظر است</param>
        /// <returns>یک نمونه از کلاس مقدار مفهوم که شامل مقدار مشخص شده می باشد</returns>
        public virtual BaseScndCnpValue GetScndCnpValueByDate(SecondaryConcept baseScndCnp, DateTime Date)
        {
            return (BaseScndCnpValue)this.ScndCnpValueList[BaseScndCnpValue.GetIndex(this.ID, baseScndCnp.IdentifierCode, Date)];
        }

        /// <summary>
        /// این تابع وظیفه جستجوی لیست مفاهیم موجود براساس "ایندکس" ورودی را برعهده دارد
        /// </summary>
        /// <param name="Index">شناسه مفهوم مورد نظر</param>
        /// <returns></returns>
        public virtual BaseScndCnpValue GetFromNotPersistedScndCnpValues(string Index)
        {
            //BaseScndCnpValue value = null;
            if (this.ScndCnpValueList.ContainsKey(Index))
            {
                return (BaseScndCnpValue)this.ScndCnpValueList[Index];
            }
            //else
            //{
            //    foreach (object val in this.ScndCnpValueList.Values)
            //    {
            //        value = (BaseScndCnpValue)val;
            //        if (value.Concept.IdentifierCode == scndCnpIdentifierCode && value.FromDate <= Date && value.ToDate >= Date)
            //        {
            //            return value;
            //        }
            //    }
            //}
            return null;
        }

        /// <summary>
        /// با ایجاد دسترسی مستقیم به "انباره ی پرسنل" مقدار مفهوم مورد نظر را براساس شناسه دریافتی بازیابی می نماید
        /// این تابع وظیفه ی یافتن مقدار مفهوم مورد نظر از لیست مفاهیمی محاسبه شده ی موجود در پایگاه داده را برعهده دارد
        /// </summary>
        /// <param name="Index">شناسه ای که مقدار مفهوم براساس آن بازیابی می شود </param>
        /// <returns>مقدار یافت شده با شناسه ارسال شده در این پارامتر قرار میگیرد</returns>
        public virtual BaseScndCnpValue GetFromPersistedScndCnpValues(string Index)
        {
            BaseScndCnpValue scndCnpVal = Person.GetPersonRepository(false).GetScndCnpValueByIndex(Index);

            if (scndCnpVal != null)
            {
                string strFroms = scndCnpVal.FromPairs, strTos = scndCnpVal.ToPairs;
                if (!Utility.IsEmpty(strFroms) && !Utility.IsEmpty(strTos)
                    &&
                    !strFroms.Equals("0") && !strTos.Equals("0")
                    && Utility.IsEmpty(((PairableScndCnpValue)scndCnpVal).Pairs))
                {
                    ((PairableScndCnpValue)scndCnpVal).Pairs = new List<IPair>();
                    if (strFroms[strFroms.Length - 1] == ';')
                    {
                        strFroms = strFroms.Remove(strFroms.Length - 1, 1);
                    }
                    if (strTos[strTos.Length - 1] == ';')
                    {
                        strTos = strTos.Remove(strTos.Length - 1, 1);
                    }

                    string[] froms = Utility.Spilit(strFroms, ';');
                    string[] tos = Utility.Spilit(strTos, ';');
                    for (int i = 0; i < froms.Length; i++)
                    {
                        IPair pair = new PairableScndCnpValuePair();
                        pair.From = Utility.ToInteger(froms[i]);

                        if (tos.Length > i)
                        {
                            pair.To = Utility.ToInteger(tos[i]);
                        }
                        ((PairableScndCnpValue)scndCnpVal).Pairs.Add(pair);
                    }
                }
            }
            return scndCnpVal;
        }

        /// <summary>
        /// وظیفه بازیابی مقدار ماهانه از پایگاه داده را برعهده دارد
        /// همچنین مقادیر احتمالی محاسبه شده ی موجود در حافظه را نیز به مقدار فوق اضافه می نماید
        /// </summary>
        /// <param name="CalculationDate"></param>
        /// <param name="RangelyScndCnpValue"></param>
        /// <returns></returns>
        public virtual PersistedScndCnpPrdValue GetPeriodicScndCnpValue(BaseScndCnpValue RangelyScndCnpValue, DateTime CalculationDate)
        {
            PersistedScndCnpPrdValue result = null;

            #region Get PeriodicScndCnpValue range

            IList<AssignedScndCnpRange> AsgCalcDateRangeList = this.AsgScndCnpRangeDictionary[RangelyScndCnpValue.Concept.IdentifierCode]
                                                                            .Where(x => x.AsgFromDate <= CalculationDate)
                                                                            .OrderByDescending(x => x.AsgFromDate)
                                                                            .Take(12)
                                                                            .ToList<AssignedScndCnpRange>();

            foreach (AssignedScndCnpRange ScndCnpRange in AsgCalcDateRangeList)
            {
                DateTime FromDate = ScndCnpRange.FromDate(CalculationDate, ScndCnpRange.UsedCulture);
                DateTime ToDate = ScndCnpRange.ToDate(CalculationDate, ScndCnpRange.UsedCulture);
                if (FromDate <= CalculationDate &&
                    ToDate >= CalculationDate)
                {
                    result = new PersistedScndCnpPrdValue()
                    {
                        ScndCnpId = RangelyScndCnpValue.Concept.ID,
                        CalcRangeGrpId = ScndCnpRange.CalcRangeGrpId,
                        FromDate = FromDate,
                        ToDate = ToDate
                    };
                    break;
                }
            }

            #endregion

            if (result == null)
                throw new BaseException(String.Format("برای مفهوم دوره ای {0} با کد شناسه {1}،در تاریخ {2} محدوده محاسبات تعریف نشده است", RangelyScndCnpValue.Concept.Name, RangelyScndCnpValue.Concept.IdentifierCode, Utility.ToPersianDate(CalculationDate)), "GetScndCnpRangeValue");


            #region Get PeriodicScndCnp value from persisted ScndCnpValue

            PersistedScndCnpPrdValue PrstScndCnpPrdValue = null;

            this.PeriodicScndCnpValueDictionary.TryGetValue(result.GetIndex(), out PrstScndCnpPrdValue);
            ///در اینجا به منظور کاهش تعداد مراجعه به پایگاه داده، محدوده مفهوم مورد نظر را
            ///به عنوان محدوده ی تمامی مفاهیم در نظر گرفته و در یک واکشی جمع مقادیر این محدوده را
            ///برای تمامی مفهوم استخراج و در یک دیکشنری با کلید تاریخ شروع و پایان محدوده نگهداری می نماییم
            ///در دسترسی های بعدی به این دیکشنری درصورتیکه با کلید(شامل شروع و پایان محدوده مفهوم ماهانه)
            ///مقداری یافت نشد به معنی این است که این مفهوم از محدوده ی دیگری استفاده می نماید
            ///و باید برای واکشی و جمع مقادیر در این محدوده مجددا به پایگاه داده مراجعه نمود
            if (PrstScndCnpPrdValue == null)
            {
                foreach (PersistedScndCnpPrdValue item in SecondaryConcept.GetRepository(false).GetPeriodicScndCnpValues(this.ID, this.CalcDateZone.FromDate, this.CalcDateZone.ToDate, result.FromDate, result.ToDate, result.CalcRangeGrpId))
                {
                    if (!this.PeriodicScndCnpValueDictionary.TryGetValue(item.GetIndex(), out PrstScndCnpPrdValue))
                    {
                        //مقدار مفهوم دوره ای قبلا واکشی نشده است
                        this.PeriodicScndCnpValueDictionary.Add(item.GetIndex(), item);
                    }
                }
                this.PeriodicScndCnpValueDictionary.TryGetValue(result.GetIndex(), out PrstScndCnpPrdValue);
            }

            if (PrstScndCnpPrdValue != null)
                result.Value = PrstScndCnpPrdValue.Value;

            #endregion

            #region Get PeriodicScndCnp value from nonPersisted ScndCnpValue

            ///مقادیر فعلی برای این مفهوم که در پایگاه داده ذخیره نشده است
            ///به مقدار ماهانه اضافه می گردد           
            foreach (BaseScndCnpValue ScndCnpValue in this.ScndCnpValueList.Values)
            {
                if (ScndCnpValue.ID == 0
                    && (RangelyScndCnpValue.Concept.PeriodicScndCnpDetails
                                                        .Where(x => x.IdentifierCode == ScndCnpValue.Concept.IdentifierCode)
                                                        .Count() > 0)
                    && ScndCnpValue.FromDate >= result.FromDate
                    && ScndCnpValue.ToDate <= result.ToDate)
                {
                    ScndCnpValue.CalcRangeGrpId = result.CalcRangeGrpId;
                    result.Value += ScndCnpValue.Value;
                }
            }

            #endregion
            if (result.Value > 0)
                result.Value = result.Value;
            return result;
        }

        /// <summary>
        /// وظیفه ایجاد یک "دوره" شامل دوره ی زمانیکه برای مفهوم دوره ای ارسال شده، تعریف گشته را برعهده دارد
        /// </summary>
        /// <param name="RangelyScndCnp"></param>
        /// <param name="CalculationDate"></param>
        public virtual DateRange GetPeriodicScndCnpRange(SecondaryConcept PeriodicScndCnp, DateTime CalculationDate)
        {
            DateRange tmp = null;
            if (this.AsgScndCnpRangeDictionary.ContainsKey(PeriodicScndCnp.IdentifierCode))
            {
                IList<AssignedScndCnpRange> AsgCalcDateRangeList = this.AsgScndCnpRangeDictionary[PeriodicScndCnp.IdentifierCode]
                                                                    .Where(x => x.AsgFromDate <= CalculationDate)
                                                                    .OrderByDescending(x => x.AsgFromDate)
                                                                    .Take(12)
                                                                    .ToList<AssignedScndCnpRange>();

                foreach (AssignedScndCnpRange ScndCnpRange in AsgCalcDateRangeList)
                {
                    DateTime FromDate = ScndCnpRange.FromDate(CalculationDate, ScndCnpRange.UsedCulture);
                    DateTime ToDate = ScndCnpRange.ToDate(CalculationDate, ScndCnpRange.UsedCulture);
                    int order = ScndCnpRange.GetOrder(CalculationDate, ScndCnpRange.UsedCulture);
                    if (FromDate <= CalculationDate &&
                        ToDate >= CalculationDate)
                    {
                        tmp = new DateRange(FromDate, ToDate, order);
                        break;
                    }
                }
            }
            if (tmp == null)
                throw new BaseException(String.Format("برای مفهوم دوره ای {0} با کد شناسه {1}،در تاریخ {2} محدوده محاسبات تعریف نشده است", PeriodicScndCnp.Name, PeriodicScndCnp.IdentifierCode, Utility.ToPersianDate(CalculationDate)), "GetPeriodicScndCnpRange");

            return tmp;
        }

        /// <summary>
        /// وظیفه مقداردهی دوره مربوط به یک مفهوم روزانه را بر عهده دارد
        /// بعبارتی با توجه به مفهوم ماهانه آن این کار را انجام میدهد
        /// </summary>
        /// <param name="RangelyScndCnp"></param>
        /// <param name="CalculationDate"></param>
        public virtual DateRange InitSecondaryConceptValueRange(SecondaryConcept DailyScndCnp, DateTime CalculationDate)
        {
            DateRange tmp = null;
           
            if (DailyScndCnp.DetailsScndCnpPeridics == null || DailyScndCnp.DetailsScndCnpPeridics.Count == 0) { return null; }
            decimal PeriodicScndCnpIdentifierCode = DailyScndCnp.DetailsScndCnpPeridics.First().IdentifierCode;
            if (this.AsgScndCnpRangeDictionary.ContainsKey(PeriodicScndCnpIdentifierCode))
            {
                IList<AssignedScndCnpRange> AsgCalcDateRangeList = this.AsgScndCnpRangeDictionary[PeriodicScndCnpIdentifierCode]
                                                                    .Where(x => x.AsgFromDate <= CalculationDate)
                                                                    .OrderByDescending(x => x.AsgFromDate)
                                                                    .Take(12)
                                                                    .ToList<AssignedScndCnpRange>();

                foreach (AssignedScndCnpRange ScndCnpRange in AsgCalcDateRangeList)
                {
                    DateTime FromDate = ScndCnpRange.FromDate(CalculationDate, ScndCnpRange.UsedCulture);
                    DateTime ToDate = ScndCnpRange.ToDate(CalculationDate, ScndCnpRange.UsedCulture);
                    int order = ScndCnpRange.GetOrder(CalculationDate, ScndCnpRange.UsedCulture);
                    if (FromDate <= CalculationDate &&
                        ToDate >= CalculationDate)
                    {
                        tmp = new DateRange(FromDate, ToDate, order);
                        tmp.ID = ScndCnpRange.ID;                       
                        break;
                    }
                }
            }
            if (tmp == null)
                return new DateRange();

            return tmp;
        }

        #endregion

        #region Leave

        /// <summary>
        /// مانده مرخصی تا تاریخ ارسالی را برمی گرداند
        /// از تنظیمات مرخصی برای تصمیم گیری در مورد استفاده از بودجه ی ماه های بعد استفاده می کند
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public virtual int GetRemainLeave(DateTime Date)
        {
            int Result = 0;

            LeaveCalcResult LastLCR = this.GetLastLCR(Date);

            //Result += LastLCR.Day * this.LeaveSetting.MinutesInDay + LastLCR.Minute;
            Result += LastLCR.DayRemain * this.LeaveSetting.MinutesInDay + LastLCR.MinuteRemain;

            #region استفاده از بودجه ماه های آینده

            LeaveInfo LeaveBudgetValue = new LeaveInfo();
            if (this.LeaveSetting.UseFutureMounthLeave)
            {
                DateTime endOfYear = this.GetEndOfContactYear(Date); //**Utility.GetDateOfEndYear(Date, LanguagesName.Parsi);
                //افزودن بودجه ماه های آینده به مانده مرخصی
                IList<CurrentYearBudget> FutureYearBudgets =
                    this.CurrentYearBudgetList.Where(x => x.Date > Date && x.Date <= endOfYear).ToList();

                LeaveBudgetValue = GetLeaveBudgetInfo(FutureYearBudgets);
                Result += LeaveBudgetValue.Day * this.LeaveSetting.MinutesInDay + LeaveBudgetValue.Minute;
            }

            #endregion

            return Result;
        }

        /// <summary>
        /// مانده مرخصی تا تاریخ ارسالی در پارامترهای ارجاعی روز و ساعت مقداردهی می کند       
        /// </summary>
        /// <param name="Date">تاریخ جاری</param>
        /// <returns></returns>
        public virtual LeaveInfo GetRemainLeaveToDateUI(DateTime Date)
        {
            LeaveInfo Result = new LeaveInfo();
            LeaveCalcResult LastLCR = this.GetLastLCR(Date);
            if (LastLCR.LeaveMinuteInDay == 0)
            {
                LastLCR.LeaveMinuteInDay = 7 * 60;//از هیچی بهتره!
            }
            Result.Day = LastLCR.DayRemain;
            Result.Minute = LastLCR.MinuteRemain;
            foreach (LeaveIncDec leaveIcDc in this.LeaveIncDecList.Where(x => x.Date <= Date && x.Applyed == false))
            {
                Result.Day += leaveIcDc.Day;
                Result.Minute += leaveIcDc.Minute;
            }
            return Result;
        }

        /// <summary>
        /// مانده مرخصی در بازه ارسالی برگردانده میشود
        /// </summary>
        /// <param name="fromDate">تاریخ شروع</param>
        /// <param name="toDate">تاریخ انتها</param>
        /// <returns></returns>
        public virtual LeaveInfo GetRemainLeaveToEndOfYearUI(DateTime fromDate, DateTime toDate)
        {

            LeaveInfo Result = new LeaveInfo();
            LeaveCalcResult LastLCR = this.GetLastLCR(fromDate, toDate);
            if (LastLCR.LeaveMinuteInDay == 0)
            {
                LastLCR.LeaveMinuteInDay = 7 * 60;
            }
            Result.Day = LastLCR.DayRemain;
            Result.Minute = LastLCR.MinuteRemain;
            foreach (LeaveIncDec leaveIcDc in this.LeaveIncDecList.Where(x => x.Date >= fromDate &&
                                                                        x.Date <= LastLCR.Date &&
                                                                        x.Applyed == false))
            {
                Result.Day += leaveIcDc.Day;
                Result.Minute += leaveIcDc.Minute;
            }

            IList<CurrentYearBudget> FutureYearBudgets =
                        this.CurrentYearBudgetList.Where(x => x.Date > LastLCR.Date &&
                                                            x.Date <= toDate).ToList();

            LeaveInfo LeaveBudgetValue = GetLeaveBudgetInfo(FutureYearBudgets);
            Result.Day += LeaveBudgetValue.Day;
            Result.Minute += LeaveBudgetValue.Minute;

            Result.Day += Result.Minute / LastLCR.LeaveMinuteInDay;
            Result.Minute = Result.Minute % LastLCR.LeaveMinuteInDay;
            if (Result.Minute < 0 && Result.Day > 0)
            {
                Result.Day--;
                Result.Minute += LastLCR.LeaveMinuteInDay;
            }
            return Result;
        }

        /// <summary>
        /// مقدار ارسالی را در تاریخ مشخص شده به عنوان مرخصی استفاده شده ثبت کرده
        /// مقدار "نتیجه مرخصی محاسبه شده" را بروزرسانی می کند
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="Value"></param>
        public virtual void AddUsedLeave(DateTime Date, int Value, Permit permit)
        {
            LeaveCalcResult CurrentLCR = this.LeaveCalcResultList.Where(x => x.Date == Date)
                                                                    .FirstOrDefault();

            ///اگر "مانده مرخصی محاسبه شده"ای قبلا وجود نداشت یا از نوع "مانده مرخصی محاسبه شده" از نوع استفاده شده نبود
            if (CurrentLCR == null || !(CurrentLCR is UsedLeaveDetailLCR))
            {
                ///در اینجا "مانده مرخصی محاسبه شده" ایجاد شده و
                ///بودجه ماه های بین آخرین مانده مرخصی محاسبه شده تا ماه جاری به آن اضافه میشود                

                #region ایجاد "مانده مرخصی محاسبه شده" جدید و مقداردهی اولیه آن

                LeaveCalcResult LastLCR = this.GetLastLCR(Date);

                CurrentLCR = new UsedLeaveDetailLCR()
                {
                    Date = Date,
                    Person = this,
                    Day = LastLCR.Day,
                    Minute = LastLCR.Minute,
                    DayRemain = LastLCR.DayRemain,
                    MinuteRemain = LastLCR.MinuteRemain,
                    DayUsed = LastLCR.DayUsed,
                    MinuteUsed = LastLCR.MinuteUsed,
                    LeaveMinuteInDay = this.LeaveSetting.MinutesInDay
                };
                this.LeaveCalcResultList.Add(CurrentLCR);

                #endregion

            }

            CurrentLCR.Minute -= Value;
            CurrentLCR.MinuteRemain -= Value;
            if (Value > 0)//برگردان مرخصی در استفاده شده اعمال نشود
            {
                CurrentLCR.MinuteUsed += Value;
            }
            UsedLeaveDetail UsedLeaveDtl = new UsedLeaveDetail(Value, this.LeaveSetting.MinutesInDay) { Date = Date, Person = this, Permit = permit };
            this.UsedLeaveDetailList.Add(UsedLeaveDtl);

            ((UsedLeaveDetailLCR)CurrentLCR).UsedLeaveDetail = UsedLeaveDtl;
            CurrentLCR.DoAdequate(this.LeaveSetting.MinutesInDay);
        }

        /// <summary>
        /// مقدار ارسالی را در تاریخ مشخص شده به عنوان مرخصی استفاده شده ثبت کرده
        /// مقدار "نتیجه مرخصی محاسبه شده" را بروزرسانی می کند
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="Value"></param>
        public virtual void AddRemainLeave(DateTime Date, int Value)
        {
            LeaveCalcResult LCR = this.GetLastLCR(Date);
            LCR.LeaveMinuteInDay = this.LeaveSetting.MinutesInDay;
            LCR.Minute += Value;
            LCR.MinuteRemain += Value;
            UsedLeaveDetail UsedLeaveDtl = new UsedLeaveDetail(Value * -1, this.LeaveSetting.MinutesInDay) { Date = Date, Person = this, Permit = null };
            this.UsedLeaveDetailList.Add(UsedLeaveDtl);

            LCR.DoAdequate(this.LeaveSetting.MinutesInDay);
        }

        /// <summary>
        /// اضافه کردن مقدار در قالب بودجه
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="Value"></param>
        public virtual void AddRemainLeaveBudget(DateTime Date, int Value)
        {

            LeaveCalcResult LastLCR = this.GetLastLCR(Date);
            //LCR.LeaveMinuteInDay = this.LeaveSetting.MinutesInDay;
            //LCR.Minute += Value;
            //LCR.MinuteRemain += Value;
            //UsedLeaveDetail UsedLeaveDtl = new UsedLeaveDetail(Value * -1, this.LeaveSetting.MinutesInDay) { Date = Date, Person = this, Permit = null };
            //this.UsedLeaveDetailList.Add(UsedLeaveDtl);

            //LCR.DoAdequate(this.LeaveSetting.MinutesInDay);

            LeaveInfo FinalLeave =
               new LeaveInfo()
               {
                   Day = 0,
                   Minute = 0
               };

            CurrentYearBudget FinalRemainLeave =
               new CurrentYearBudget()
               {
                   Day = LastLCR.DayRemain,
                   Minute = LastLCR.MinuteRemain
               };
            IList<CurrentYearBudget> Budget = this.CurrentYearBudgetList.Where(x => x.Date >= Utility.ToMildiDateTime(x.AsgFromDate) &&
                                                                                                  x.Date <= Utility.ToMildiDateTime(x.AsgToDate) &&
                                                                                                  Date >= Utility.ToMildiDateTime(x.AsgFromDate) &&
                                                                                                  Date <= Utility.ToMildiDateTime(x.AsgToDate) &&
                                                                                                  Date >= x.Date ).ToList();
            //در صورت نامعتبر بودن ریفرنس آی دی , گزارش کارتکس درست نمایش داده نمیشود
            //لذا در این حالت یک آی دی صوری بدست میآوریم
            //CurrentYearBudget refrenceBudget = null;
            //if (Budget != null && Budget.Count > 0)
            //{
            //    refrenceBudget = Budget.First();
            //}
            //else 
            //{
            //    throw new Exception("تابع اضافه کردن بودجه مرخصی حداقل به یک بودجه در تاریخ محاسبات احتیاج دارد");
            //}
            BudgetLCR BdgLCR = new BudgetLCR()
            {
                Date = Date,
                Person = this,
                DayUsed = LastLCR.DayUsed,
                MinuteUsed = LastLCR.MinuteUsed,
                LeaveMinuteInDay = this.LeaveSetting.MinutesInDay,
                RefrenceId = 0
            };

           
            FinalLeave.Minute += Value;

            BdgLCR.Day = FinalLeave.Day;
            BdgLCR.Minute = FinalLeave.Minute;
            BdgLCR.DayRemain = FinalLeave.Day + FinalRemainLeave.Day;
            BdgLCR.MinuteRemain = FinalLeave.Minute + FinalRemainLeave.Minute;

            BdgLCR.DoAdequate(this.LeaveSetting.MinutesInDay);
            this.LeaveCalcResultList.Add(BdgLCR);
        }

        /// <summary>
        /// در صورت وجود بودجه، افزودن/کاستن یا مانده مرخصی سال قبل در تاریخ ورودی "مانده مرخصی محاسبه شده" برای آن ایجاد میشود
        /// مانده مرخصی تا تاریخ ارسالی در پارامترهای ارجاعی روز و ساعت مقداردهی می کند
        /// از تنظیمات مرخصی برای تصمیم گیری در مورد استفاده از بودجه ی ماه های بعد استفاده می کند
        /// به منظور نمایش صحیح، مانده مرخصی در خارج از موتور محاسبه(کاردکس مرخصی)ا
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public virtual void VerifyRemainLeave(DateTime Date)
        {
            LeaveCalcResult LastLCR = this.GetLastLCR(Date);

            LeaveInfo FinalLeave =
                new LeaveInfo()
                {
                    Day = 0,
                    Minute = 0
                };
            CurrentYearBudget FinalRemainLeave =
                new CurrentYearBudget()
                {
                    Day = LastLCR.DayRemain,
                    Minute = LastLCR.MinuteRemain
                };

            #region Budget

            bool budgetFound = false;

            IList<CurrentYearBudget> spetialList = this.CurrentYearBudgetList.Where(x => x.Date >= Utility.ToMildiDateTime(x.AsgFromDate) &&
                                                                                                  x.Date <= Utility.ToMildiDateTime(x.AsgToDate) &&
                                                                                                  Date >= Utility.ToMildiDateTime(x.AsgFromDate) &&
                                                                                                  Date <= Utility.ToMildiDateTime(x.AsgToDate) &&
                                                                                                  Date >= x.Date &&
                                                                                                  x.Type == BudgetType.PerMonth &&
                                                                                                  x.Applyed == false).ToList();

            foreach (CurrentYearBudget CurrentYearBgt in spetialList)
            {
                CurrentYearBgt.Applyed = true;
                if (!this.LeaveCalcResultList.Any(y => y.GetType() == new BudgetLCR().GetType() && y.RefrenceId == CurrentYearBgt.ID))
                {
                    BudgetLCR BdgLCR = new BudgetLCR(CurrentYearBgt)
                    {
                        Date = CurrentYearBgt.Date,
                        Person = this,
                        DayUsed = LastLCR.DayUsed,
                        MinuteUsed = LastLCR.MinuteUsed,
                        LeaveMinuteInDay = this.LeaveSetting.MinutesInDay,
                        RefrenceId = CurrentYearBgt.BudgetId
                    };

                    FinalLeave.Day += CurrentYearBgt.Day;
                    FinalLeave.Minute += CurrentYearBgt.Minutes;

                    BdgLCR.Day = FinalLeave.Day;
                    BdgLCR.Minute = FinalLeave.Minute;
                    BdgLCR.DayRemain = FinalLeave.Day + FinalRemainLeave.Day;
                    BdgLCR.MinuteRemain = FinalLeave.Minute + FinalRemainLeave.Minute;

                    BdgLCR.DoAdequate(this.LeaveSetting.MinutesInDay);
                    this.LeaveCalcResultList.Add(BdgLCR);
                }
                budgetFound = true;
            }

            if (!budgetFound)
            {
                DateTime beginOfYear = this.GetBeginOfContactYear(Date);//شروع سال قراردادی //**Utility.GetDateOfBeginYear(Date, LanguagesName.Parsi);
                DateTime endOfYear = this.GetEndOfContactYear(Date);//پایان سال قراردادی //**Utility.GetDateOfEndYear(Date, LanguagesName.Parsi);
                DateTime beginOfRealYear = Utility.GetDateOfBeginYear(Date, LanguagesName.Parsi);//شروع سال واقعی که شروع قراداد سالیانه در آن واقع شده است
                
                //برای ارکان ثالث بودجه سال که شروع قرارداد در آن است یرای کل سال قراردادی لحاظ میگردد حتی اگر پایان قرارداد در سال آسنده باشد
                IList<CurrentYearBudget> usualList = this.CurrentYearBudgetList.Where(x => !(beginOfYear > Utility.ToMildiDateTime(x.AsgToDate) || endOfYear < Utility.ToMildiDateTime(x.AsgFromDate))
                                                         && Date >= Utility.ToMildiDateTime(x.AsgFromDate)
                                                         && Date <= Utility.ToMildiDateTime(x.AsgToDate)
                                                         && beginOfRealYear == x.Date
                                                         && x.Type == BudgetType.Usual
                    //&& x.Applyed == false
                                                         && !this.LeaveCalcResultList.Any(y => y.GetType() == new BudgetLCR().GetType() && y.RefrenceId == x.BudgetId)
                                                         ).ToList();

                foreach (CurrentYearBudget CurrentYearBgt in usualList)
                //if(usualList!=null &&usualList.Count==1)
                {
                    if (!this.LeaveCalcResultList.Any(y => y.GetType() == new BudgetLCR().GetType() && y.RefrenceId == CurrentYearBgt.ID))
                    {
                        int diffMonth = 0;
                        int currentYear = Utility.ToPersianDateTime(CurrentYearBgt.Date).Year;
                        PersianDateTime asgFromDate = Utility.ToPersianDateTime(Utility.ToMildiDateTime(CurrentYearBgt.AsgFromDate));
                        PersianDateTime asgToDate = Utility.ToPersianDateTime(Utility.ToMildiDateTime(CurrentYearBgt.AsgToDate));
                        if (asgFromDate.Year == currentYear && asgToDate.Year == currentYear)
                        {
                            diffMonth = asgToDate.Month - asgFromDate.Month + 1;
                        }
                        else if (asgFromDate.Year == currentYear && asgToDate.Year > currentYear)
                        {
                            diffMonth = 12 - asgFromDate.Month + 1;
                        }
                        else if (asgFromDate.Year < currentYear && asgToDate.Year > currentYear)
                        {
                            diffMonth = 12;
                        }
                        else if (asgFromDate.Year < currentYear && asgToDate.Year == currentYear)
                        {
                            diffMonth = asgToDate.Month;
                        }

                        CurrentYearBgt.Applyed = true;

                        BudgetLCR BdgLCR = new BudgetLCR(CurrentYearBgt)
                        {
                            Date = Utility.ToMildiDateTime(CurrentYearBgt.AsgFromDate) > CurrentYearBgt.Date ? Utility.ToMildiDateTime(CurrentYearBgt.AsgFromDate) : CurrentYearBgt.Date,
                            Person = this,
                            DayUsed = LastLCR.DayUsed,
                            MinuteUsed = LastLCR.MinuteUsed,
                            LeaveMinuteInDay = CurrentYearBgt.MinutesInDay,// this.LeaveSetting.MinutesInDay,
                            RefrenceId = CurrentYearBgt.BudgetId//,
                            //Type=CurrentYearBgt.Type
                        };
                        //تخصیص به نسبت انتساب در دسته قانون
                        int budgetMin = (CurrentYearBgt.Day * this.LeaveSetting.MinutesInDay) + CurrentYearBgt.Minute;
                        //FinalLeave.Day += CurrentYearBgt.Day;
                        FinalLeave.Minute += Utility.ToInteger((budgetMin / 12f) * diffMonth);

                        BdgLCR.Day = FinalLeave.Day;
                        BdgLCR.Minute = FinalLeave.Minute;
                        BdgLCR.DayRemain = FinalLeave.Day + FinalRemainLeave.Day;
                        BdgLCR.MinuteRemain = FinalLeave.Minute + FinalRemainLeave.Minute;

                        BdgLCR.DoAdequate(this.LeaveSetting.MinutesInDay);
                        this.LeaveCalcResultList.Add(BdgLCR);
                    }
                    budgetFound = true;
                }
            }
            #endregion

            #region Leave Inc Dec
            foreach (LeaveIncDec LeaveIcDc in this.LeaveIncDecList
                                                                  .Where(x => x.Applyed == false && x.Date <= Date)
                                                                  .ToList())
            {
                LeaveIcDc.Applyed = true;

                LeaveIncDecLCR LeaveIcDcLCR = new LeaveIncDecLCR()
                {
                    Date = LeaveIcDc.Date,
                    Person = this,
                    DayUsed = LastLCR.DayUsed,
                    MinuteUsed = LastLCR.MinuteUsed,
                    LeaveMinuteInDay = this.LeaveSetting.MinutesInDay,
                    LeaveIncDec = LeaveIcDc
                };

                FinalLeave.Day += LeaveIcDc.Day;
                FinalLeave.Minute += LeaveIcDc.Minute;

                LeaveIcDcLCR.Day = FinalLeave.Day;
                LeaveIcDcLCR.Minute = FinalLeave.Minute;
                LeaveIcDcLCR.DayRemain = FinalLeave.Day + FinalRemainLeave.Day;
                LeaveIcDcLCR.MinuteRemain = FinalLeave.Minute + FinalRemainLeave.Minute;

                LeaveIcDcLCR.DoAdequate(this.LeaveSetting.MinutesInDay);
                this.LeaveCalcResultList.Add(LeaveIcDcLCR);
            }
            #endregion

            //به منظور نمایش صحیح، مانده مرخصی در کاردکس مرخصی_
            IList<LeaveCalcResult> currentDayLCR = this.LeaveCalcResultList.Where(x => x.Date == Date).ToList<LeaveCalcResult>();
            foreach (LeaveCalcResult lcr in currentDayLCR)
            {
                lcr.LeaveMinuteInDay = this.LeaveSetting.MinutesInDay;
                lcr.DoAdequate(this.LeaveSetting.MinutesInDay);
            }

        }

        /// <summary>
        /// آخرین نشانه را برمیگرداند
        /// اگر موجود نباشد خطا میدهد زیرا تابه مقدار دهی اولیه حتما باید یکی حداقل درج کرده باشد
        /// </summary>
        /// <param name="date"></param>
        public virtual LeaveCalcResult GetLastLCR(DateTime fromDate, DateTime toDate)
        {
            IBudgetRepository rep = Person.GetBudgetRepository(false);

            IList<LeaveCalcResult> orderdLCR = rep.GetLCR(this.ID, fromDate, toDate).OrderBy(x => x.Date).ToList();
            LeaveCalcResult lastLCR = orderdLCR.LastOrDefault();
            if (lastLCR == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.LeaveLCRDoesNotExists, String.Format("مقدار «نتیجه مرخصی محاسبه شده» برای شخص با کد پرسنلی {0} نامعتبر است", this.PersonCode), "GTS.Clock.Model.Person." + String.Format("Person.GetRemainLeave({0},{1})", fromDate, toDate));
            }
            if (this.LeaveSetting.MinutesInDay > 0)
            {
                lastLCR.DoAdequate(this.LeaveSetting.MinutesInDay);
            }
            else if (lastLCR.LeaveMinuteInDay > 0)
            {
                lastLCR.DoAdequate(lastLCR.LeaveMinuteInDay);
            }
            return lastLCR;
        }

        /// <summary>
        /// آخرین نشانه را برمیگرداند
        /// اگر موجود نباشد خطا میدهد زیرا تابه مقدار دهی اولیه حتما باید یکی حداقل درج کرده باشد
        /// </summary>
        /// <param name="date"></param>
        private LeaveCalcResult GetLastLCR(DateTime date)
        {
            IBudgetRepository rep = Person.GetBudgetRepository(false);

            LeaveCalcResult lastLCR = this.LeaveCalcResultList.Where(x => x.Date <= date)
                                                              .OrderBy(x => x.Date)
                                                              .LastOrDefault();

            if (lastLCR == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.LeaveLCRDoesNotExists, String.Format("مقدار «نتیجه مرخصی محاسبه شده» برای شخص با کد پرسنلی {0} نامعتبر است", this.PersonCode), "GTS.Clock.Model.Person." + String.Format("Person.GetRemainLeave({0})", date));
            }
            if (this.LeaveSetting.MinutesInDay > 0)
            {
                lastLCR.DoAdequate(this.LeaveSetting.MinutesInDay);
            }
            else if (lastLCR.LeaveMinuteInDay > 0)
            {
                lastLCR.DoAdequate(lastLCR.LeaveMinuteInDay);
            }
            return lastLCR;
        }

        /// <summary>
        /// جمع بودجه در ماه های ارسالی را با در نظر گرفتن تاریخ انتساب بودجه ی ماه برمیگرداند 
        /// </summary>
        /// <param name="MostUsedYearBudgets"></param>
        /// <returns></returns>
        private LeaveInfo GetLeaveBudgetInfo(IList<CurrentYearBudget> MostUsedYearBudgets)
        {
            LeaveInfo Result = new LeaveInfo() { Day = 0, Minute = 0 };
            int embededMinutesInDay = 0;
            foreach (CurrentYearBudget CurrentYearBdt in MostUsedYearBudgets.Where(x => x.Date >= Convert.ToDateTime(x.AsgFromDate) &&
                                                                                        x.Date <= Convert.ToDateTime(x.AsgToDate)))
            {
                Result.Day += CurrentYearBdt.Day;
                Result.Minute += CurrentYearBdt.Minutes;
                embededMinutesInDay = CurrentYearBdt.MinutesInDay;
            }

            if (this.LeaveSetting.MinutesInDay > 0)
            {
                Result.Day += Result.Minute / this.LeaveSetting.MinutesInDay;
                Result.Minute = Result.Minute % this.LeaveSetting.MinutesInDay;
            }
            else if (embededMinutesInDay > 0)//از هیچی که بهتره! ا
            {
                Result.Day += Result.Minute / embededMinutesInDay;
                Result.Minute = Result.Minute % embededMinutesInDay;
            }

            return Result;
        }

        /// <summary>
        /// تاریخ شروع قرارداد فعلی و یا تاریخ شروع سال واقعی
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private DateTime GetBeginOfContactYear (DateTime date)
        {
            DateTime beginOfYear = Utility.GetDateOfBeginYear(date, LanguagesName.Parsi);
            return beginOfYear;
        }

        /// <summary>
        /// تاریخ پایان قرارداد فعلی و یا پایان سال
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private DateTime GetEndOfContactYear(DateTime date)
        {
            DateTime endOfYear = Utility.GetDateOfEndYear(date, LanguagesName.Parsi);
            return endOfYear;
        }

        /// <summary>
        /// نزدیکترین انتقال که قبل از تاریخ ارسالی باشد را برمیگرداند
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public virtual LeaveInfo GetLeaveYearRemain(DateTime date)
        {
            LeaveInfo info = new LeaveInfo();
            LeaveYearRemain remain = this.LeaveYearRemainList.Where(x => x.Date <= date).OrderBy(x => x.Date).LastOrDefault();
            if (remain != null)
            {
                info.Day = remain.DayRemainOK;
                info.Minute = remain.MinuteRemainOK;
            }
            return info;
        }
        #endregion

        #region Permit



        /// <summary>
        /// مجوز برای تاریخ و پیش کارت ارسالی را بر میگرداند
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="Precard"></param>
        public virtual Permit GetPermitByDate(DateTime Date, Precard Precard)
        {
            if (Precard == null)
                return new Permit();
            Permit permit =
                this.PermitList
                .FirstOrDefault(x =>
                    x.FromDate.Date <= Date.Date &&
                    x.ToDate.Date >= Date.Date &&
                    x.Pairs.Any(p => p.PreCardID == Precard.ID))
                    ?? new Permit();

            if (permit.ID > 0 && permit.PairCount > 1 && permit.IsPairly)
            {
                IList<PermitPair> list = GTS.Clock.Model.Concepts.Operations.Operation.Union(permit.Pairs);
                var p = new Permit
                {
                    ID = ID,
                    FromDate = permit.FromDate,
                    ToDate = permit.ToDate,
                    Pairs = list
                };

                return p;
            }
            return permit;

        }

        public virtual Permit GetPermitByDate(DateTime Date, bool isRangly, Precard Precard)
        {
            if (isRangly)
            {
                Permit permit = this.PermitList.Where(x => x.FromDate.Date <= Date.Date &&
                                                  x.ToDate.Date >= Date.Date &&
                                                  x.Pairs.Where(p => p.PreCardID == Precard.ID).Count() > 0).FirstOrDefault() ?? new Permit();
                if (permit.ID > 0 && permit.PairCount > 1 && permit.IsPairly)
                {
                    IList<PermitPair> list = GTS.Clock.Model.Concepts.Operations.Operation.Union(permit.Pairs);
                    var p = new Permit
                    {
                        ID = ID,
                        FromDate = permit.FromDate,
                        ToDate = permit.ToDate,
                        Pairs = list

                    };
                    return p;
                }
                return permit;
            }
            else
            {
                return GetPermitByDate(Date, Precard);
            }
        }

        /// <summary>
        /// تمامی مجوزهای در تاریخ ارسالی را برمی گرداند
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="Precard"></param>
        /// <returns></returns>
        public virtual IList<Permit> GetPermitByDate(DateTime Date)
        {
            return this.PermitList
                       .Where(x => x.FromDate.Date <= Date.Date &&
                                   x.ToDate.Date >= Date.Date &&
                                    x.PairCount > 0).ToList() ?? new List<Permit>();
        }

        #endregion

        #region Initialize

        /// <summary>
        /// وظیفه مقدار دهی اولیه به "شخص" برای اجرای قوانین بعهده این تابع است
        /// </summary>
        public virtual DateRange InitializeForExecuteRule(DateTime fromDate, DateTime toDate, DateTime beginYear, DateTime endYear)
        {
            this.CalcDateZone = new DateRange(fromDate, toDate, beginYear, endYear);
            //data load from last day caused rule which look up last day executs faster
            Person.GetPersonRepository(false).EnableEfectiveDateFilter(this.ID, this.CalcDateZone.FromDate.AddDays(-1), this.CalcDateZone.ToDate, beginYear, endYear, this.CalcDateZone.FromDate.AddDays(-20), this.CalcDateZone.ToDate.AddDays(+20));

            this.InitializeForLeave(this.CalcDateZone);

            #region AssignedScndCnpRangeList

            foreach (AssignedScndCnpRange AsgScndCnpRange in this.AssignedScndCnpRangeList)
            {
                IList<AssignedScndCnpRange> AsgScndCnpRangeList = null;
                this.AsgScndCnpRangeDictionary.TryGetValue(AsgScndCnpRange.ConceptIdentifier, out AsgScndCnpRangeList);
                if (AsgScndCnpRangeList == null)
                {
                    AsgScndCnpRangeList = new List<AssignedScndCnpRange>();
                    AsgScndCnpRangeList.Add(AsgScndCnpRange);
                    this.AsgScndCnpRangeDictionary.Add(AsgScndCnpRange.ConceptIdentifier, AsgScndCnpRangeList);
                }
                else
                {
                    AsgScndCnpRangeList.Add(AsgScndCnpRange);
                }
            }

            #endregion

            //this.ScndCnpRangeValueList = prsRepository.GetScndCnpRangeValue(this.ID);
            return this.CalcDateZone;
        }

       /// <summary>
       /// جهت مقداردهی اولیه به رینج محاسبات
       /// </summary>
       /// <param name="fromDate"></param>
       /// <param name="toDate"></param>
       /// <param name="beginYear"></param>
       /// <param name="endYear"></param>
       /// <returns></returns>
        public virtual DateRange InitializeForDateRange(DateTime fromDate, DateTime toDate, DateTime beginYear, DateTime endYear)
        {
            this.CalcDateZone = new DateRange(fromDate, toDate, beginYear, endYear);
            //data load from last day caused rule which look up last day executs faster
            Person.GetPersonRepository(false).EnableEfectiveDateFilter(this.ID, this.CalcDateZone.FromDate.AddDays(-1), this.CalcDateZone.ToDate, beginYear, endYear, this.CalcDateZone.FromDate.AddDays(-20), this.CalcDateZone.ToDate.AddDays(+20));
         
            #region AssignedScndCnpRangeList

            foreach (AssignedScndCnpRange AsgScndCnpRange in this.AssignedScndCnpRangeList)
            {
                IList<AssignedScndCnpRange> AsgScndCnpRangeList = null;
                this.AsgScndCnpRangeDictionary.TryGetValue(AsgScndCnpRange.ConceptIdentifier, out AsgScndCnpRangeList);
                if (AsgScndCnpRangeList == null)
                {
                    AsgScndCnpRangeList = new List<AssignedScndCnpRange>();
                    AsgScndCnpRangeList.Add(AsgScndCnpRange);
                    this.AsgScndCnpRangeDictionary.Add(AsgScndCnpRange.ConceptIdentifier, AsgScndCnpRangeList);
                }
                else
                {
                    AsgScndCnpRangeList.Add(AsgScndCnpRange);
                }
            }

            #endregion

            //this.ScndCnpRangeValueList = prsRepository.GetScndCnpRangeValue(this.ID);
            return this.CalcDateZone;
        }

        /// <summary>
        /// وظیفه مقدار دهی اولیه به "شخص" برای نگاشت ترددها بعهده این تابع است
        /// </summary>
        public virtual DateRange InitializeForTrafficMapper(DateTime fromDate, DateTime toDate)
        {
            this.CalcDateZone = new DateRange(fromDate, toDate, 0);

            #region بررسی ترددهای اولیه به منظور اینکه در صورت نیاز تاریخ شروع را عقب بکشیم

            CriteriaStruct[] CriStrs = new CriteriaStruct[3] { new CriteriaStruct(Utility.GetPropertyName(() => new BasicTraffic().Person), this), 
                                                                new CriteriaStruct(Utility.GetPropertyName(() => new BasicTraffic().Used), false),
                                                                new CriteriaStruct(Utility.GetPropertyName(() => new BasicTraffic().Active), true)};
            DateTime MinBasicTraficDate = BasicTraffic.GetEntityRepository(false).GetByCriteria(CriStrs).DefaultIfEmpty(new BasicTraffic() { Date = DateTime.Now }).Min(x => x.Date);
            if (MinBasicTraficDate < fromDate)
            {
                this.CalcDateZone = new DateRange(MinBasicTraficDate, toDate, 0);
            }

            #endregion

            this.CalcDateZone.FromDate = this.CalcDateZone.FromDate.AddDays(-3);
            this.CalcDateZone.ToDate = this.CalcDateZone.ToDate.AddDays(3);

            Person.GetPersonRepository(false).EnableEfectiveDateFilter(this.ID, this.CalcDateZone.FromDate, this.CalcDateZone.ToDate, DateTime.Now, DateTime.Now, this.CalcDateZone.FromDate.AddDays(-20), this.CalcDateZone.ToDate.AddDays(20));
            return this.CalcDateZone;

        }

        /// <summary>
        /// وظیفه مقدار دهی اولیه به "شخص" برای دسترسی بهقوانین بعهده این تابع است
        /// </summary>
        public virtual DateRange InitializeForAccessRules(DateTime fromDate, DateTime toDate)
        { //1
            this.CalcDateZone = new DateRange(fromDate, toDate, 0);         

            this.CalcDateZone.FromDate = this.CalcDateZone.FromDate.AddDays(-3);
            this.CalcDateZone.ToDate = this.CalcDateZone.ToDate.AddDays(3);

            Person.GetPersonRepository(false).EnableEfectiveDateFilter(this.ID, this.CalcDateZone.FromDate, this.CalcDateZone.ToDate, DateTime.Now, DateTime.Now, this.CalcDateZone.FromDate.AddDays(-20), this.CalcDateZone.ToDate.AddDays(20));
            return this.CalcDateZone;

        }

        /// <summary>
        ///وظیفه بررسی ابتدای سال تا تاریخ شروع محاسبه را دارد
        ///بررسی مانده مرخصی سال قبل که حتما در صورت وجود در تاریخ ابتدای سال ذخیره شده است
        ///بررسی اینکه اگر در سالی که داخل رینج محاسبه است , مانده ندارد , رکورد صفر درج میکند
        ///بررسی برگشت به عقب
        ///توجه: در اینجا به بودجه کاری نداریم . بودجه در قانون مربوطه لحاظ میگردد
        ///زیرا مطمئن هستیم اگر شخص بودجهداشته باشد آن ماه باید محاسبه گردد
        ///توجه: به افزوردن و کاهش مرخصی کاری نداریم و باید در قانون مربوطه مقداردهی گردد
        ///همچنین بودجههای گذشته را استفاده شده در نظر میگیرد تا دوباره در قوانین لحاظ نگردد
        /// </summary>
        public virtual void InitializeForLeave(DateRange dateRange)
        {
            #region برگشت به عقب ایتم های موثر در مرخصی

            foreach (UsedLeaveDetail UsedLeaveDtl in this.UsedLeaveDetailList.Where(x => x.Date >= dateRange.FromDate).ToList())
            {
                this.UsedLeaveDetailList.Remove(UsedLeaveDtl);
            }
            foreach (LeaveCalcResult LCR in this.LeaveCalcResultList.Where(x => x.Date >= dateRange.FromDate).ToList())
            {
                this.LeaveCalcResultList.Remove(LCR);
            }
            foreach (LeaveIncDec LeaveIcDc in this.LeaveIncDecList.Where(x => x.Date >= dateRange.FromDate).ToList())
            {
                LeaveIcDc.Applyed = false;
            }
            foreach (LeaveYearRemain LeaveYearRem in this.LeaveYearRemainList.Where(x => x.Date >= dateRange.FromDate).ToList())
            {
                LeaveYearRem.Applyed = false;
            }

            #endregion

            #region شکستن بازه محاسبات به سال
            PersianDateTime start = Utility.ToPersianDateTime(dateRange.FromDate);
            DateTime from = dateRange.FromDate;
            DateTime endOfYear = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", start.Year, 12, Utility.GetEndOfPersianMonth(start.Year, 12)));
            DateTime beginOfYear = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", start.Year, 1, 1));
            bool addYear = false;
            while (true)
            {
                DateTime to = endOfYear > dateRange.ToDate ? dateRange.ToDate : endOfYear;

                ///بررسی برای یک سال
                //آیا از اول سال محاسبه نشده است
                if (this.LeaveCalcResultList.Where(x => x.Date == beginOfYear).Count() == 0)
                {

                    #region ایجاد مانده مرخصی محاسبه شده برای اولین محاسبه در سال

                    LeaveInfo FinalLeave = new LeaveInfo() { Day = 0, Minute = 0 };


                    #region LeaveYearRemain
                    //برای انتقال مانده مرخصی حتما یک رکورد باید داشته باشیم
                    if (this.LeaveYearRemainList.Where(x => x.Date == beginOfYear && x.Applyed == false).Count() == 0)
                    {
                        LeaveYearRemain remain = new LeaveYearRemain();
                        remain.Date = beginOfYear;
                        remain.Applyed = false;
                        remain.DayRemainOK = 0;
                        remain.DayRemainReal = 0;
                        remain.MinuteRemainOK = 0;
                        remain.MinuteRemainReal = 0;
                        remain.Person = this;
                        this.LeaveYearRemainList.Add(remain);
                    }

                    foreach (LeaveYearRemain LeaveYearRem in this.LeaveYearRemainList.Where(x => x.Date == beginOfYear && x.Applyed == false))
                    {
                        LeaveYearRem.Applyed = true;

                        LeaveYearRemainLCR LeaveYearRemLCR = new LeaveYearRemainLCR() { Date = LeaveYearRem.Date, LeaveYearRemain = LeaveYearRem, Person = this };
                        FinalLeave.Day += LeaveYearRem.DayRemainOK;
                        FinalLeave.Minute += LeaveYearRem.MinuteRemainOK;

                        LeaveYearRemLCR.Day = FinalLeave.Day;
                        LeaveYearRemLCR.Minute = FinalLeave.Minute;
                        LeaveYearRemLCR.DayRemain = FinalLeave.Day;
                        LeaveYearRemLCR.MinuteRemain = FinalLeave.Minute;

                        this.LeaveCalcResultList.Add(LeaveYearRemLCR);
                    }

                    #endregion



                    #endregion

                    //addYear = true;

                }



                #region Add Year
                //if (addYear)
                //{
                from = to.AddDays(1);
                start = Utility.ToPersianDateTime(from);
                endOfYear = Utility.ToMildiDate(String.Format("{0}/{1}/{2}",
                    start.Year,
                    12,
                    Utility.GetEndOfPersianMonth(start.Year, 12)));
                beginOfYear = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", start.Year, 1, 1));
                addYear = false;
                //} 
                #endregion

                if (to == dateRange.ToDate)
                    break;
            }

            #endregion


            #region Budget گذشته باید استفاده شده در نظر گرفته شود

            foreach (CurrentYearBudget budget in this.CurrentYearBudgetList.Where(x => x.Type == BudgetType.PerMonth
                                                                                    && x.Date < dateRange.FromDate
                                                                                    && x.Applyed == false))
            {
                budget.Applyed = true;
            }

            #endregion
        }

        #endregion

        /// <summary>
        /// شیفت پرسنل در تاریخ مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="Date">تاریخی که شیفت آن مدنظر است</param>
        /// <returns>یک نمونه از کلاس شیفت که شامل شیفت پرسنل در تاریخ مشخص شده می باشد</returns>
        public virtual BaseShift GetShiftByDate(DateTime Date)
        {
            try
            {
                BaseShift result =
                    new BaseShift();
                ///اگر در آن روز شیفت استثناء وجود داشت آن شیفت را برمی گرداند
                ShiftException exShift = this.ShiftExceptionList.Where(x => x.Date == Date)
                                             .FirstOrDefault();
                if (exShift != null)
                {
                    if (exShift.Shift == null)
                    {
                        return new AssignedWGDShift();
                    }

                    AssignedWGDShift assgn = new AssignedWGDShift();
                    assgn.MyShiftId = exShift.ID;
                    assgn.Pairs = exShift.Shift.Pairs;
                    assgn.Name = exShift.Shift.Name;
                    assgn.NobatKari = exShift.Shift.NobatKari;
                    assgn.NobatKariID = exShift.Shift.NobatKariID;
                    assgn.Person = exShift.Shift.Person;
                    assgn.ShiftType = exShift.Shift.ShiftType;
                    assgn.FromDate = exShift.Date;
                    assgn.ToDate = exShift.Date;
                    assgn.Date = exShift.Date;
                    result = assgn;
                }
                else
                {
                    result = this.AssignedWGDShiftList
                               .Where(x => x.Date == Date)
                               .FirstOrDefault();
                    if (result == null)
                    {
                        //throw new BaseException(String.Format("برای پرسنل {0} در تاریخ {1} شیفت تعریف نشده است", this.BarCode, PersianDateTime.MiladiToShamsi(Date.ToShortDateString())), "Person.GetShiftByDate");
                        return new AssignedWGDShift();
                    }
                    result.MyShiftId = ((AssignedWGDShift)result).ShiftId;
                }

                //اگر "زوج"های شیفت قبلا واکشی شده اند برای همان شیفت در روز جدید دیگر واکشی نگردند
                IList<ShiftPair> pairs = null;
                this.ShiftPairsDictionary.TryGetValue(result.MyShiftId, out pairs);
                if (pairs == null)
                    this.ShiftPairsDictionary.Add(result.MyShiftId, result.Pairs);
                else
                {
                    List<ShiftPair> temp = new List<ShiftPair>();
                    temp.AddRange(pairs);
                    result.Pairs = temp;
                }

                // استخراج شیفتهای عادی
                if (!result.Pairs.Any())
                    return new AssignedWGDShift();

                List<ShiftPair> normalShiftPairList =
                    result.Pairs
                    .Where(x =>
                        x.ShiftPairType == null ||
                        x.ShiftPairType.CustomCode == "0"
                        ).ToList();

                result.Pairs = normalShiftPairList;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// شیفت پرسنل در تاریخ مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="Date">تاریخی که شیفت آن مدنظر است</param>
        /// <param name="ShiftCustomCode">نوع شیفت بر اساس کد اختصاص داده شده: عادی 0</param>
        /// <returns>یک نمونه از کلاس شیفت که شامل شیفت پرسنل در تاریخ مشخص شده می باشد</returns>
        public virtual BaseShift GetShiftByDate(DateTime Date, string ShiftCustomCode)
        {
            try
            {
                BaseShift result = new BaseShift();
                ///اگر در آن روز شیفت استثناء وجود داشت آن شیفت را برمی گرداند
                ShiftException exShift = this.ShiftExceptionList.Where(x => x.Date == Date)
                                             .FirstOrDefault();
                if (exShift != null)
                {
                    if (exShift.Shift == null)
                    {
                        return new AssignedWGDShift();
                    }

                    AssignedWGDShift assgn = new AssignedWGDShift();
                    assgn.MyShiftId = exShift.ID;
                    assgn.Pairs = exShift.Shift.Pairs;
                    assgn.Name = exShift.Shift.Name;
                    assgn.NobatKari = exShift.Shift.NobatKari;
                    assgn.NobatKariID = exShift.Shift.NobatKariID;
                    assgn.Person = exShift.Shift.Person;
                    assgn.ShiftType = exShift.Shift.ShiftType;
                    assgn.FromDate = exShift.Date;
                    assgn.ToDate = exShift.Date;
                    assgn.Date = exShift.Date;
                    result = assgn;
                }
                else
                {
                    result = this.AssignedWGDShiftList
                               .Where(x => x.Date == Date)
                               .FirstOrDefault();
                    if (result == null)
                    {
                        //throw new BaseException(String.Format("برای پرسنل {0} در تاریخ {1} شیفت تعریف نشده است", this.BarCode, PersianDateTime.MiladiToShamsi(Date.ToShortDateString())), "Person.GetShiftByDate");
                        return new AssignedWGDShift();
                    }
                    result.MyShiftId = ((AssignedWGDShift)result).ShiftId;
                }

                //اگر "زوج"های شیفت قبلا واکشی شده اند برای همان شیفت در روز جدید دیگر واکشی نگردند
                IList<ShiftPair> pairs = null;
                this.ShiftPairsDictionary.TryGetValue(result.MyShiftId, out pairs);
                if (pairs == null)
                    this.ShiftPairsDictionary.Add(result.MyShiftId, result.Pairs);
                else
                {
                    List<ShiftPair> temp = new List<ShiftPair>();
                    temp.AddRange(pairs);
                    result.Pairs = temp;
                }

                // استخراج شیفتهای عادی
                if (!result.Pairs.Any())
                    return new AssignedWGDShift();

                List<ShiftPair> normalShiftPairList =
                    result.Pairs
                    .Where(x => x.ShiftPairType != null &&
                        x.ShiftPairType.CustomCode == ShiftCustomCode
                        ).ToList();

                result.Pairs = normalShiftPairList;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public virtual PersonWorkGroup GetWorkGroupByDate(DateTime Date)
        {
            //by Farhad
            PersonWorkGroup tmp = this.AssignedWorkGroupList
                                       .Where(x => x.FromDate <= Date)
                                       .OrderByDescending(x => x.FromDate)
                                       .FirstOrDefault();
            return tmp ?? new PersonWorkGroup();



            /*PersonWorkGroup tmp = this.WorkGroupList
                                        .Where(x => x.FromDate >= Date.GregorianDate && x.ToDate <= Date.GregorianDate)
                                        .FirstOrDefault();
            return tmp ?? new PersonWorkGroup();*/

        }

        public override string ToString()
        {
            string summery = "";
            summery = String.Format("شماره پرسنلی:{0} نام:{1} شناسه سیستمی:{2}", this.PersonCode, this.Name, this.ID);
            return summery;
        }
        #endregion

        #region Static Methods

        public static IPersonRepository GetPersonRepository(bool Disconnectedly)
        {
            return RepositoryFactory.GetRepository<IPersonRepository, Person>(Disconnectedly);
        }

        public static IBudgetRepository GetBudgetRepository(bool Disconnectedly)
        {
            return RepositoryFactory.GetRepository<IBudgetRepository, Budget>(Disconnectedly);
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            /*  //this.ID = 0;
              if (this.ProceedTrafficList != null) this.ProceedTrafficList.Clear();
              if (this.PermitList != null) this.PermitList.Clear();
              if (this.PeriodicScndCnpValueDictionary != null) this.PeriodicScndCnpValueDictionary.Clear();
              if (this.AsgScndCnpRangeDictionary != null) this.AsgScndCnpRangeDictionary.Clear();
              if (this.AssignedRuleList != null) this.AssignedRuleList.Clear();
              if (this.AssignedScndCnpRangeList != null) this.AssignedScndCnpRangeList.Clear();
              if (this.AssignedWGDShiftList != null) this.AssignedWGDShiftList.Clear();
              if (this.AssignedWorkGroupList != null) this.AssignedWorkGroupList.Clear();
              if (this.BasicTrafficList != null) this.BasicTrafficList.Clear();
              if (this.BudgetList != null) this.BudgetList.Clear();
              if (this.LeaveCalcResultList != null) this.LeaveCalcResultList.Clear();
              if (this.LeaveIncDecList != null) this.LeaveIncDecList.Clear();
              if (this.LeaveYearRemainList != null) this.LeaveYearRemainList.Clear(); ;
              */
        }

        #endregion
    }
}
