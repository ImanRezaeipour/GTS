using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.UIValidation
{
    public interface IUIRuleValidator
    {
        /// <summary>
        /// درخواست تردد عادی پرسنل تا __ روز بعد از روز درخواست قابل ثبت باشد
        /// </summary>
        /// <returns></returns>
        bool V1(decimal personId, DateTime requestDate);

        /// <summary>
        /// تعداد درخواست های تردد عادی پرسنل در ماه حداکثر __ عدد باشد 
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        bool V2(decimal personId);
    }
}
