using System;
using System.Collections.Generic;


namespace GTS.Clock.Model.Clientele
{
    public class CL_Flow : BaseClienteleEnity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the AccessGroup value.
        /// </summary>
        public virtual CL_OffishTypeAccessGroup AccessGroup { get; set; }

        /// <summary>
        /// Gets or sets the WorkFlow value.
        /// </summary>
        public virtual Boolean WorkFlow { get; set; }

        /// <summary>
        /// Gets or sets the ActiveFlow value.
        /// </summary>
        public virtual Boolean ActiveFlow { get; set; }

        public virtual Boolean MainFlow { get; set; }

        public virtual String FlowName { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual int PersonCount
        {
            get;
            set;
        }

        public virtual CL_FlowGroup FlowGroup
        {
            get;
            set;
        }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual int DepartmentCount
        { get; set; }

        /// <summary>
        /// جهت استفاده در واسط کاربر
        /// </summary>
        public virtual bool IsAssignedToSubstitute { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual IList<CL_ManagerFlow> ManagerFlowList { get; set; }

        public virtual IList<CL_UnderManagement> UnderManagementList { get; set; }

        public virtual IList<CL_Operator> OperatorList { get; set; }

        #endregion

        public override string ToString()
        {
            return String.Format("جریان کاری با شناسه {0} و نام {1}", this.ID, this.FlowName);
        }
    }
}
