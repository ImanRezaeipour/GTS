using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model.Concepts
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

    public class BaseShift : BasePairableConceptValue<ShiftPair>, IEntity
	{
        public BaseShift()
        {
            this.Pairs = new List<ShiftPair>();
        }

        #region variables

        private string minNobatKariTime = "";

        #endregion

        #region Properties

		/// <summary>
		/// Gets or sets the Name value.
		/// </summary>
		public virtual String Name { get; set; }	

        /// <summary>
        /// Gets or sets the Sobhane value.
        /// </summary>
        public virtual Boolean Breakfast { get; set; }

        /// <summary>
        /// Gets or sets the Nahar value.
        /// </summary>
        public virtual Boolean Lunch { get; set; }

        /// <summary>
        /// Gets or sets the Sham value.
        /// </summary>
        public virtual Boolean Dinner { get; set; }

        /// <summary>
        /// Gets or sets the Color value.
        /// </summary>
        public virtual string Color { get; set; }

        /// <summary>
        /// Gets or sets the CustomCode value.
        /// </summary>
        public virtual string CustomCode { get; set; }

        /// <summary>
        /// این خصیصه صرفا بمنظور راحت سازی کار واسط کاربر تعریف میگردد
        /// </summary>
        public virtual Decimal NobatKariID { get; set; }

        public virtual ShiftTypesEnum? ShiftType
        {
            get;
            set;
        }

        public virtual NobatKari NobatKari
        {
            get;
            set;
        }

        public virtual int MinNobatKari
        {
            get;
            set;
        }

        /// <summary>
        /// حدنصاب نوبت کاری را برای نمایش در واسط کاربر به زمان تبدیل میکند
        /// </summary>
        public virtual string MinNobatKariTime
        {
            get 
            {
                if (minNobatKariTime == "")
                {
                    minNobatKariTime = Infrastructure.Utility.Utility.IntTimeToRealTime(MinNobatKari);
                }
                return minNobatKariTime; 
            }
            set 
            {
                minNobatKariTime = value;
                MinNobatKari = Infrastructure.Utility.Utility.RealTimeToIntTime(minNobatKariTime);
            }
        }

        /// <summary>
        /// نوع شیفت که تنها برای نمایش راحت تر در واسط کاربر استفاده میشود
        /// </summary>
        public virtual string ShiftTypeTitle
        {
            get
            {
                return ShiftType.ToString();
            }
            //set
            //{
            //    if (ShiftTypesEnum.WORK.ToString("G").Equals(value))
            //    {
            //        ShiftType = ShiftTypesEnum.WORK;
            //    }
            //    else if (ShiftTypesEnum.OVERTIME.ToString("G").Equals(value))
            //    {
            //        ShiftType = ShiftTypesEnum.OVERTIME;
            //    }
            //    else if (ShiftTypesEnum.COMPENSATION_OVERTIME.ToString("G").Equals(value))
            //    {
            //        ShiftType = ShiftTypesEnum.COMPENSATION_OVERTIME;
            //    }
            //    else
            //    {

            //    }
            //}
        }
        
        public virtual IList<ShiftException> ShiftExceptionList
        {
            get;
            set;
        }

        public ShiftPair PastedPairs
        {
            get
            {
                if (base.PairCount > 0)
                {
                    return new ShiftPair(base.Pairs.First().From, base.Pairs.Last().To);
                }
                else
                {
                    return new ShiftPair(0, 0);
                }
            }
        }


        public override int Value
        {
            get
            {
                return this.PairValues;
            }
        }

        /// <summary>
        /// جهت استخراج جفتهای شیفت
        /// </summary>
        public virtual decimal MyShiftId { get; set; }

        //public virtual decimal ExpShiftId { get; set; }

		#endregion	
	
        public override string ToString()
        {
            string msg = "";
            foreach (ShiftPair paire in Pairs)
            {
                msg += String.Format(" ({0}-{1}) ", paire.ExFrom, paire.ExTo);
            }
            return msg;
        }


	}
}