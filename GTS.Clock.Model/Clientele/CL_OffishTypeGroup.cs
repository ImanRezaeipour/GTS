using System;
using System.Collections.Generic;

namespace GTS.Clock.Model.Clientele
{
    public class CL_OffishTypeGroup : BaseClienteleEnity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public virtual String Name { get; set; }

        /// <summary>
        /// Gets or sets the LookupKey value.
        /// </summary>
        public virtual String LookupKey { get; set; }

        /// <summary>
        /// Gets or sets the LookupKey value.
        /// </summary>
        public virtual int IntLookupKey { get; set; }

        /// <summary>
        /// جهت نمایش درخت همراه با چک باکس در واسط کاربر
        /// </summary>
        public virtual bool ContainInOffishTypeAccessGroup
        {
            get;
            set;
        }

        public virtual IList<CL_OffishType> OffishTypeList { get; set; }
        #endregion
    }
}
