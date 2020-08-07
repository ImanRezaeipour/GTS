using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Model.PersonInfo;
using GTS.Clock.Model.Rules;
using GTS.Clock.Model.Charts;


namespace GTS.Clock.Model
{
    public class PersonCLSpec : IEntity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        public virtual ControlStation ControlStation { get; set; }

        public virtual UIValidationGroup UIValidationGroup { get; set; }

        public virtual DepartmentPosition DepartmentPosition { get; set; }


        #region Reserved Fields
        /// <summary>
        /// Gets or sets the R1 value.
        /// </summary>
        public virtual String R1 { get; set; }

        /// <summary>
        /// Gets or sets the R2 value.
        /// </summary>
        public virtual String R2 { get; set; }

        /// <summary>
        /// Gets or sets the R3 value.
        /// </summary>
        public virtual String R3 { get; set; }

        /// <summary>
        /// Gets or sets the R4 value.
        /// </summary>
        public virtual String R4 { get; set; }

        /// <summary>
        /// Gets or sets the R5 value.
        /// </summary>
        public virtual String R5 { get; set; }

        /// <summary>
        /// Gets or sets the R6 value.
        /// </summary>
        public virtual String R6 { get; set; }

        /// <summary>
        /// Gets or sets the R7 value.
        /// </summary>
        public virtual String R7 { get; set; }

        /// <summary>
        /// Gets or sets the R8 value.
        /// </summary>
        public virtual String R8 { get; set; }

        /// <summary>
        /// Gets or sets the R9 value.
        /// </summary>
        public virtual String R9 { get; set; }

        /// <summary>
        /// Gets or sets the R10 value.
        /// </summary>
        public virtual String R10 { get; set; }

        /// <summary>
        /// Gets or sets the R11 value.
        /// </summary>
        public virtual String R11 { get; set; }

        /// <summary>
        /// Gets or sets the R12 value.
        /// </summary>
        public virtual String R12 { get; set; }

        /// <summary>
        /// Gets or sets the R13 value.
        /// </summary>
        public virtual String R13 { get; set; }

        /// <summary>
        /// Gets or sets the R14 value.
        /// </summary>
        public virtual String R14 { get; set; }

        /// <summary>
        /// Gets or sets the R15 value.
        /// </summary>
        public virtual String R15 { get; set; }

        /// <summary>
        /// combo value
        /// </summary>
        public virtual decimal R16 { get; set; }
        public virtual PersonReserveFieldComboValue R16Value { get; set; }

        /// <summary>
        /// combo value
        /// </summary>
        public virtual decimal R17 { get; set; }
        public virtual PersonReserveFieldComboValue R17Value { get; set; }

        /// <summary>
        /// combo value
        /// </summary>
        public virtual decimal R18 { get; set; }
        public virtual PersonReserveFieldComboValue R18Value { get; set; }

        /// <summary>
        /// combo value
        /// </summary>
        public virtual decimal R19 { get; set; }
        public virtual PersonReserveFieldComboValue R19Value { get; set; }

        /// <summary>
        /// combo value
        /// </summary>
        public virtual decimal R20 { get; set; }
        public virtual PersonReserveFieldComboValue R20Value { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual String R16Text { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual String R17Text { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual String R18Text { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual String R19Text { get; set; }

        /// <summary>
        /// جهت نمایش در واسط کاربر
        /// </summary>
        public virtual String R20Text { get; set; }
        #endregion

        public virtual IList<PersonParamValue> ParameterValueList { get; set; }

        #endregion

        #region Methods

        #region PersonParam Field/Value

        public PersonParamValue GetParamValue(decimal personId, string key)
        {
            return
                ParameterValueList
                .FirstOrDefault(x =>
                    x.Person.ID.Equals(personId) &&
                    x.ParamField.Key.ToUpper().Equals(key.ToUpper())
                    );
        }

        public PersonParamValue GetParamValue(decimal personId, string key, DateTime date)
        {
            return GetParamValue(personId, key, date, date);
        }
        public PersonParamValue GetParamValue(decimal personId, string key, DateTime dateFrom, DateTime dateTo)
        {
            return
                ParameterValueList
                .FirstOrDefault(x =>
                    x.Person.ID.Equals(personId) &&
                    x.FromDate.Date <= dateFrom.Date &&
                    x.ToDate.Date >= dateFrom.Date &&
                    x.ParamField.Key.ToUpper().Equals(key.ToUpper())
                    );
        }

        #endregion

        #endregion

    }
}