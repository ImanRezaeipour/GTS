using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.UIValidation
{
    #region Comments
    /// <h3>Changes</h3>
    /// 	<listheader>
    /// 		<th>Author</th>
    /// 		<th>Date</th>
    /// 		<th>Details</th>
    /// 	</listheader>
    /// 	<item>
    /// 		<term>Farhad Salavati</term>
    /// 		<description>4/4/2012</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class UIValidationRuleParameter : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public virtual String Value { get; set; }

        public virtual String TheValue { get; set; }

        public virtual RuleParamType Type { get; set; }

        public virtual String Name { get; set; }

        public virtual String KeyName { get; set; }

        /// <summary>
        /// ادامه در روز بعد پارامترهایی از جنس زمان
        /// در پایگاه داده ذخیره نمیشود
        /// </summary>
        public virtual bool ContinueOnTomorrow { get; set; } 

        public virtual UIValidationGrouping Grouping
        {
            get;
            set;
        }

        #endregion

    }
}