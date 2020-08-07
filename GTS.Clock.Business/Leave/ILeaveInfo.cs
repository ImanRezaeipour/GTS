using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Leave
{
    /// <summary>
    /// جهت درست و معتبر بودن مقادیر برگشتی لازم است قبل از فراخوانی این سرویس ها
    /// محاسبات انجام گردد
    /// </summary>
    public interface ILeaveInfo
    {
        void GetRemainLeaveToEndOfMonth(decimal personId, int year, int month, out int day, out int minutes);
        void GetRemainLeaveToEndOfYear(decimal personId, int year, int month, out int day, out int minutes);
    }
}
