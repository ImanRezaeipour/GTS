using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Model.Security;

namespace GTS.Clock.Model.Clientele
{
    public class CL_Manager : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        public virtual bool Active { get; set; }

        /// <summary>
        /// Gets or sets the Person value.
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// جهت کاربری راحتر در واسط کاربر
        /// </summary>
        public virtual Person ThePerson
        {
            get
            {
                if (Person != null)
                    return this.Person;
                if (OrganizationUnit != null && OrganizationUnit.Person != null)
                    return OrganizationUnit.Person;
                return new Person();
            }
        }

        /// <summary>
        /// جهت کاربری راحتر در واسط کاربر
        /// </summary>
        public virtual OrganizationUnit TheOrganizationUnit
        {
            get
            {
                if (OrganizationUnit != null)
                    return this.OrganizationUnit;
                if (Person != null && Person.OrganizationUnit != null)
                    return Person.OrganizationUnit;
                return new OrganizationUnit();
            }
        }

        /// <summary>
        /// Gets or sets the Unit value.
        /// </summary>
        public virtual OrganizationUnit OrganizationUnit { get; set; }

        public virtual IList<CL_ManagerFlow> ManagerFlowList { get; set; }

        public virtual ManagerAssignType ManagerType
        {
            get
            {
                if (Person == null)
                    return ManagerAssignType.OrganizationUnit;
                if (OrganizationUnit == null) return ManagerAssignType.Person;
                return ManagerAssignType.None;
            }
        }

        public virtual IList<CL_Substitute> SubstituteList { get; set; }

        public virtual IList<CL_DataAccessManager> DataAccessList { get; set; }

        #endregion

    }
}
