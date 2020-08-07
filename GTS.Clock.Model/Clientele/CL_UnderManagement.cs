using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Charts;

namespace GTS.Clock.Model.Clientele
{
    public class CL_UnderManagement : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the ManagmentID value.
        /// </summary>
        public virtual CL_Flow Flow { get; set; }

        /// <summary>
        /// Gets or sets the PersonID value.
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// Gets or sets the UnitID value.
        /// </summary>
        public virtual Department Department { get; set; }

        /// <summary>
        /// Gets or sets the ContainInnerUnits value.
        /// </summary>
        public virtual Boolean ContainInnerChilds { get; set; }

        /// <summary>
        /// Gets or sets the Contains value.
        /// </summary>
        public virtual Boolean Contains { get; set; }

        /// <summary>
        /// جهت راحتی واسط کاربر
        /// </summary>
        public virtual UnderManagmentTypes Type
        {
            get
            {
                return this.Person == null ? UnderManagmentTypes.Department : UnderManagmentTypes.Person;
            }
        }

        /// <summary>
        /// جهت راحتی واسط کاربر
        /// نام شخص یا نام بحش
        /// </summary>
        public virtual string Name
        {
            get
            {
                if (this.Person != null || this.Department != null)
                {
                    return this.Person == null ? this.Department.Name : this.Person.Name;
                }
                return "";
            }
        }

        /// <summary>
        /// کلید جهت استفاده در واسط کاربر
        /// </summary>
        public virtual string KeyID
        {
            get
            {
                string depId = this.Department != null ? this.Department.ID.ToString() : "0";
                string prsId = this.Person != null ? this.Person.ID.ToString() : "0";
                return String.Format("dep{0}prs{1}", depId, prsId);
            }
        }
        #endregion
    }
}
