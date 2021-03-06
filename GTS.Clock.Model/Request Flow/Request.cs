using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model.RequestFlow
{
	#region Comments	
	/// <h3>Changes</h3>
	/// 	<listheader>
	/// 		<th>Author</th>
	/// 		<th>Date</th>
	/// 		<th>Details</th>
	/// 	</listheader>
	/// 	<item>
	/// 		<term>Farhad Salavati</term>
	/// 		<description>5/23/2011</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

    public class Request : IEntity, ICloneable
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
		/// جهت نمایشدر واسط کاربر
		/// </summary>
        public virtual String Title
        {
            get
            {
                if (this.Precard != null)
                    return this.Precard.Name;
                return "";
            }
        }

        #region Date And Time
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
        /// Gets or sets the RegisterDate value.
        /// </summary>
        public virtual DateTime RegisterDate { get; set; }

        #endregion

		/// <summary>
		/// Gets or sets the Description value.
		/// </summary>
		public virtual String Description { get; set; }		

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

        /// <summary>
        /// ادامه در روز بعد درخواست های ساعتی
        /// </summary>
        public virtual bool ContinueOnTomorrow { get; set; }
        #endregion

		/// <summary>
		/// Gets or sets the UserID value.
		/// </summary>
		public virtual Security.User User { get; set; }

        /// <summary>
        /// این آیتم در کارتابل نمایش داده میشود
        /// واکشی اسن آیتم در هنگام بارگزاری کارتابل بسیار زمانگیر است
        /// در نتیجه با ذخیره این فیلد ، در زمان صرفه جویی میکنیم
        /// </summary>
        public virtual string OperatorUser { get; set; }

        /// <summary>
        /// Get or Set Request Attachment File
        /// </summary>
        public virtual string AttachmentFile { get; set; }

        /// <summary>
        /// Gets or sets the Precard value.
        /// </summary>
        public virtual Precard Precard { get; set; }

        /// <summary>
        /// Gets or sets the Person value.
        /// </summary>
        public virtual Person Person { get; set; }

        public virtual decimal request_ID { get; set; }

        public virtual bool EndFlow { get; set; }

        public virtual IList<RequestStatus> RequestStatusList { get; set; }

        public virtual IList<Doctor> DoctorList { get; set; }

        public virtual IList<Illness> IllnessList { get; set; }

        public virtual IList<DutyPlace> DutyPlaceList { get; set; }

        public override string ToString()
        {
            string summery = "";
            summery = String.Format("شخص:{0} نوع درخواست:{1} تاریخ:{2}", this.Person.Name, this.Precard.Name, Utility.ToPersianDate(this.RegisterDate));
            return summery;
        }

		#endregion		
   

        #region ICloneable Members

        public virtual object Clone()
        {
            Request request = new Request();
            request.AddClientSide = this.AddClientSide;
            request.Description = this.Description;
            request.DoctorID = this.DoctorID;
            request.DutyPositionID = this.DutyPositionID;
            request.FromDate = this.FromDate;
            request.ToDate = this.ToDate;
            request.TheFromDate = this.TheFromDate;
            request.TheToDate = this.TheToDate;
            request.TheFromTime = this.TheFromTime;
            request.TheToTime = this.TheToTime;
            request.TheTimeDuration = this.TheTimeDuration;
            request.TimeDuration = this.TimeDuration;
            request.TimePlusFlag = this.TimePlusFlag;
            request.FromTime = this.FromTime;
            request.ToTime = this.ToTime;
            request.AttachmentFile = this.AttachmentFile;
            if (this.Person != null)
                request.Person = new Person() { ID = this.Person.ID };
            if (this.User != null)
                request.User = new Security.User() { ID = this.User.ID };
            if (this.Precard != null)
                request.Precard = new Precard() { ID = this.Precard.ID, IsHourly = this.Precard.IsHourly, IsDaily = this.Precard.IsMonthly, IsMonthly = this.Precard.IsMonthly };
            return request;
        }

        #endregion
    }
}