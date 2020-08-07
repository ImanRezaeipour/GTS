using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Charts;

namespace GTS.Clock.Business.Proxy
{
    /// <summary>
    /// جهت فرستادن به کلاینت وقتی که بچه های یک گره مرحله به مرحله خواسته میشوند
    /// </summary>
    public struct OnDemandTreeChilds
    {
        public IList<Proxy.ChartNodeProxy> Childs;

        /// <summary>
        /// آیا این آخرین صفحه است
        /// </summary>
        public bool IsContinued;

        public int PageIndex;

        public decimal ParentID { get; set; }

    }
}
