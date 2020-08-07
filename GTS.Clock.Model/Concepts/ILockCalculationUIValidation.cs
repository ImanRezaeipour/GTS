using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
    public interface ILockCalculationUIValidation
    {
        /// <summary>
        /// تاریخ بسته شدن محاسبات را برای یک شخص برمیگرداند
        /// بستن محاسبات تنها از طریق قوانین 4 و 5 صورت میگیرد
        /// دقت شود که همیشه حداکثر تنها یکی از این دو فعال هستند
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        DateTime GetCalculationLockDate(decimal personId);
        DateTime GetCalculationLockDateByGroup(decimal uiGroupId);
        void DoValidate(object permit);
    }
}
