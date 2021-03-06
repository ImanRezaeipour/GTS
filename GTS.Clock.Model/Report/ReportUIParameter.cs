using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Report
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
    /// 		<description>2011-11-19</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class ReportUIParameter : IEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public virtual String Name { get; set; }

        /// <summary>
        /// نام صفحه ای که باید در فریم لود شود
        /// </summary>
        public virtual String Key { get; set; }

        /// <summary>
        /// Gets or sets the fnName value.
        /// </summary>
        public virtual String fnName { get; set; }

        /// <summary>
        /// Gets or sets the EnName value.
        /// </summary>
        public virtual String EnName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int ParameterOrder { get; set; }

        /// <summary>
        /// جهت بوجود آمدن امکان استفاده مجدد
        /// </summary>
        public virtual string ActionId { get; set; }

        /// <summary>
        /// اگر مقدار آن برابر درست باشد بدین معناست که این کنترل به فضای بیشتری
        /// جهت نمایش نساز دارد و باشد در دیالوگ نمایش داده شود
        /// </summary>
        public virtual bool ShowInDialog { get; set; }

        /// <summary>
        /// جهت ارتباط با واسط کاربر
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual string ParameterTitle { get; set; }

        #endregion


    }
}