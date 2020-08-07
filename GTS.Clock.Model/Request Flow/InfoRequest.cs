using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.BaseInformation;

namespace GTS.Clock.Model.RequestFlow
{

    public class InfoRequest 
	{
		#region Properties

        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// شماره سری که در سرویس جهت صفحه بندی استفاده میشود
        /// </summary>
        public virtual Int64 Number { get; set; }

        public virtual decimal mngrFlowID { get; set; }

        public virtual decimal ManagerID { get; set; }

        public virtual string FirstName { get; set; }

		/// <summary>
		/// جهت نمایشدر واسط کاربر
		/// </summary>
        public virtual String Title
        {
            get
            {
                return PrecardName;
            }
        }

		/// <summary>
		/// Gets or sets the FromDate value.
		/// </summary>
		public virtual DateTime FromDate { get; set; }

		/// <summary>
		/// Gets or sets the ToDate value.
		/// </summary>
		public virtual DateTime ToDate { get; set; }

		/// <summary>
		/// Gets or sets the FromTime value.
		/// </summary>
		public virtual Int32 FromTime { get; set; }

		/// <summary>
		/// Gets or sets the ToTime value.
		/// </summary>
		public virtual Int32 ToTime { get; set; }

        /// <summary>
        /// مدت زمان
        /// </summary>
        public virtual Int32 TimeDuration { get; set; }

		/// <summary>
		/// Gets or sets the Description value.
		/// </summary>
		public virtual String Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual String ManagerDescription { get; set; }

		/// <summary>
		/// Gets or sets the RegisterDate value.
		/// </summary>
		public virtual DateTime RegisterDate { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual string RegistrationDate { get; set; }

        /// <summary>
        /// نام دکتر در درخواست مرخصی استعلاجی
        /// </summary>
        public virtual decimal DoctorID { get; set; }

        /// <summary>
        /// نام بیماری در درخواست مرخصی استعلاجی
        /// </summary>
        public virtual decimal IllnessID { get; set; }

        /// <summary>
        /// نام محل ماموریت در درخواست ماموریت
        /// </summary>
        public virtual decimal DutyPositionID { get; set; }

        #region جهت کمک به  واسط کاربر
       
        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual string TheFromTime { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual string TheToTime { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual string TheTimeDuration { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual string TheFromDate { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual string TheToDate { get; set; }

        /// <summary>
        /// جهت استفاده در واسط کاربر
        /// وقتی درخواست جدید درج میشود اگر اسن خصیصه درست باشد به جدول پایین صفحه اضافه میشود
        /// </summary>
        public virtual bool AddClientSide { get; set; }

        /// <summary>
        /// شماره ردیف جهت نمایش در واسط کاربر
        /// </summary>
        public virtual int Row { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual RequestState Status { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// از فایل منابع باید خواند شود و سپس بایند شود
        /// </summary>
        public virtual string StatusTitle
        {
            get;
            set;
        }

        /// <summary>
        /// آیا تاریخ که از واسط کاربر مقداردهی شده است توسط کامپوننت تاریخ مقداردهی شده یا خودمان آنرا ارسال کرده ایم
        /// مثلا درخواست ماموریت ساعتی که در گزارش کارکرد ماهانه باز میشود
        /// این تاریخ از فیلد مخفی که بصورت رشته تاریخ میلادی در گزارش کارکرد ذخیره شده است مقداردهی میشود
        /// که در این حالت این خصیصه نباید مقداردهی شود
        /// </summary>
        public virtual bool IsDateSetByUser { get; set; }

        /// <summary>
        /// اگر واسط کاربر این عبارت را برابر درست بفرستد
        /// بدین معنی است که انتهای بازه زمان در روزبعد واقع شده است
        /// و باید با 1440 جمع شود
        /// </summary>
        public virtual bool TimePlusFlag { get; set; }
        
        #endregion

        public virtual decimal UserID { get; set; }

        public virtual string OperatorUser { get; set; }

        public virtual string LookupKey { get; set; }

        public virtual decimal PrecardID { get; set; }

        public virtual string PrecardName { get; set; }

        public virtual bool IsHourly { get; set; }

        public virtual bool IsDaily { get; set; }

        public virtual bool IsMonthly { get; set; }

        public virtual decimal PersonID { get; set; }

        public virtual string Applicant { get; set; }

        public virtual string ApplicantLastName { get; set; }

        public virtual string ApplicantFirstName { get; set; }

        public virtual string PersonCode { get; set; }

        public virtual decimal request_ID { get; set; }

        public virtual bool? Confirm { get; set; }

        public virtual bool? IsDeleted { get; set; }

        public virtual IList<RequestStatus> RequestStatusList { get; set; }

        public virtual IList<Doctor> DoctorList { get; set; }

        public virtual IList<Illness> IllnessList { get; set; }

        public virtual IList<DutyPlace> DutyPlaceList { get; set; }

        public virtual string AttachmentFile { get; set; }

        public virtual string PersonImage { get; set; }

		#endregion		
	}
}