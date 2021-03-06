using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model.Concepts
{
    /// <summary>
    /// کلاس والد براي 
    /// ScndCnp , CategorisedCnp
    /// 
    /// </summary>
    public class SecondaryConcept : IEntity
    {
        #region properties

        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public virtual String Name { get; set; }

        /// <summary>
        /// Gets or sets the Script value.
        /// </summary>
        public virtual String Script { get; set; }

        /// <summary>
        /// Gets or sets the CSharpCode value.
        /// </summary>
        public virtual String CSharpCode { get; set; }

        /// <summary>
        /// Gets or sets the IdentifierCode value.
        /// </summary>
        public virtual decimal IdentifierCode { get; set; }

        /// <summary>
        /// Gets or sets the CustomCode value.
        /// </summary>
        public virtual String CustomCode { get; set; }

        /// <summary>
        /// Gets or sets the CustomCategoryCode value.
        /// </summary>
        public virtual String CustomCategoryCode { get; set; }

        /// <summary>
        /// Gets or sets the IsRangely value.
        /// </summary>
        public virtual ScndCnpPeriodicType PeriodicType { get; set; }
        public virtual String PeriodicTypeTitle
        {
            get { return this.PeriodicType.ToString("G"); }
        }

        /// <summary>
        /// Gets or sets the Type value.
        /// </summary>
        public virtual ScndCnpPairableType Type { get; set; }
        public virtual String TypeTitle
        {
            get { return this.Type.ToString("G"); }
        }

        /// <summary>
        /// Gets or sets the PColumn value.
        /// </summary>
        public virtual String PColumn { get; set; }

        /// <summary>
        /// این خصوصیت نام کلیدی است که ارتباط بین گزارشات نمایش دهنده نتایج محاسبات با مقادیر مفاهیم را 
        /// نگهداری می نماید
        /// </summary>
        public virtual string KeyColumnName { get; set; }
        
        public virtual string FnName { get; set; }

        public virtual string EnName { get; set; }

        /// <summary>
        /// مشخص میکند که از مفاهیم طراحی شده
        /// توسط کاربر است یا خیر
        /// </summary>
        public virtual bool UserDefined { get; set; }

        /// <summary>
        /// Json
        /// </summary>
        public virtual string JsonObject { get; set; }

        /// <summary>
        /// نوع داده ای که هنگام نمایش باید به آن تبدیل شود
        /// </summary>
        public virtual ConceptDataType DataType { get; set; }

        /// <summary>
        /// Gets or sets the Color value.
        /// </summary>
        public virtual string Color { get; set; }
        public virtual Boolean ShowInReport { get; set; }
        public virtual Boolean IsHourly { get; set; }
        /// <summary>
        /// زمان اجرای مفهوم به منظور بالا بردن سرعت محاساب را نگهداری می کند
        /// </summary>
        public virtual ScndCnpCalcSituationType CalcSituationType { get; set; }
        public virtual String CalcSituationTypeTitle
        {
            get { return this.CalcSituationType.ToString("G"); }
        }

        /// <summary>
        ///  لزوم یا عدم لزوم ذخیره سازی مفهوم در پایگاه داده را نگهداری می کند
        /// </summary>
        public virtual ScndCnpPersistSituationType PersistSituationType { get; set; }
        public virtual String PersistSituationTypeTitle
        {
            get { return this.PersistSituationType.ToString("G"); }
        }

        public virtual IList<CalculationDateRange> CalculationDateRangeList
        {
            get;
            set;
        }

        public virtual IList<SecondaryConcept> PeriodicScndCnpDetails
        {
            get;
            set;
        }

        public virtual IList<SecondaryConcept> DetailsScndCnpPeridics
        {
            get;
            set;
        }

        #endregion

        public static ISecondaryConceptRepository GetRepository(bool Disconnectedly)
        {
            return RepositoryFactory.GetRepository<ISecondaryConceptRepository, SecondaryConcept>(Disconnectedly);
        }
    }
}
