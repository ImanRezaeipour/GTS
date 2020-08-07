using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Model.PersonInfo;
using GTS.Clock.Infrastructure;


namespace GTS.Clock.Model.Rules
{
    public class PersonParamField : IEntity
    {       
        #region Properties

        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        public virtual string Key{get; set; }

        public virtual string FnTitle { get; set; }

        public virtual string EnTitle { get; set; }

        public virtual bool Active { get; set; }

        public virtual string Title { get; set; }

        public virtual SubSystemIdentifier SubSystemId { get; set; }

        #endregion
    }
}