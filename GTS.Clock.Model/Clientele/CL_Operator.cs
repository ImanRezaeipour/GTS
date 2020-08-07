using System;

namespace GTS.Clock.Model.Clientele
{
    public class CL_Operator : IEntity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the Active value.
        /// </summary>
        public virtual Boolean Active { get; set; }

        /// <summary>
        /// Gets or sets the Description value.
        /// </summary>
        public virtual String Description { get; set; }

        /// <summary>
        /// Gets or sets the PersonId value.
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// جریان کاری
        /// </summary>
        public virtual CL_Flow Flow { get; set; }

        #endregion
    }
}
