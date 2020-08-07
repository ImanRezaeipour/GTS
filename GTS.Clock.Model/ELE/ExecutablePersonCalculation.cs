using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Model
{
    public class ExecutablePersonCalculation : IEntity
    {
        #region Variables

        private DateTime toDate;

        #endregion

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        /// <summary>
        /// در حالیه که از نظر منطقی باید شی پرسنل در اینجا نگهداری شود ولی به دلیل
        /// مسائل چند نخی شناسه پرسنل ذخیره میشود تا هر نخ در زمان نیاز به شی پرسنل آن
        /// را از پایگاه داده واکشی نماید
        /// </summary>
        public virtual decimal PersonId
        {
            get;
            set;
        }

        public virtual DateTime FromDate
        {
            get;
            set;
        }

        public virtual bool MidNightCalculate
        {
            get;
            set;
        }

        public virtual bool CalculationIsValid
        {
            get;
            set;
        }

        /// <summary>
        /// تاریخی که محاسبات به درخواست کاربر تا آن روز انجام می شود
        /// مقدار این خصوصیت در پایگاه داده ذخیره نمی شود
        /// </summary>
        public virtual DateTime ToDate
        {
            get
            {
                return this.toDate;
            }
            set
            {
                if (value < this.FromDate)
                    throw new DataValidationException(ExceptionType.ALARM, String.Format("مقدار تاریخ شروع از پایان بزرگتر است، نیازی به محاسبه ی مجدد برای این شخص نیست :FromDate {0} - ToDate {1} - PersonID{2}", this.FromDate, this.ToDate, this.PersonId));
                this.toDate = value;
            }
        }

        #endregion

        #region Static Methods

        public static IExecutablePersonCalcRepository GetExecutablePersonCalcRepositoy(bool Disconnectedly)
        {
            return RepositoryFactory.GetRepository<IExecutablePersonCalcRepository, ExecutablePersonCalculation>(Disconnectedly);
        }

        #endregion
    }
}
