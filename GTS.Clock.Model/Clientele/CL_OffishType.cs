using System;
using System.Collections.Generic;
using GTS.Clock.Model.Security;

namespace GTS.Clock.Model.Clientele
{
    public class CL_OffishType : BaseClienteleEnity
    {
        #region Properties
        public virtual String Name { get; set; }

        public virtual CL_OffishTypeGroup OffishTypeGroup { get; set; }

        public virtual Boolean Active { get; set; }

        public virtual Boolean IsLock { get; set; }

        public virtual string CustomCode { get; set; }

        public virtual string RealName { get; set; }
        
        /// <summary>
        /// جهت نمایش درخت همراه با چک باکس در واسط کاربر
        /// </summary>
        public virtual bool ContainInOffishTypeAccessGroup { get; set; }

        public virtual IList<CL_OffishTypeAccessGroup> AccessGroupList { get; set; }

        public virtual IList<Role> AccessRoleList { get; set; }
        #endregion


    }
}
