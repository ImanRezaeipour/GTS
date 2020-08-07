using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class AccessGroupProxy
    {
        public decimal ID { get; set; }

        /// <summary>
        ///آیا این گره شامل یک گروه پیشکارت است
        /// </summary>
        public bool IsParent { get; set; }

        /// <summary>
        /// درخت شامل گروه پیشکارت و پیشکارت است
        /// اگر گروه باشد این خصیصه حتما برابر درست است
        /// در غیر این صورت شامل درست و نادرست است
        /// </summary>
        public bool Checked { get; set; }

    }
}
