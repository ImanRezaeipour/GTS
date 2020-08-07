using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Model
{
    public class Help : IEntity
    {

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual string FaName
        {
            get;
            set;
        }

        public virtual string EnName
        {
            get;
            set;
        }

        public virtual string FaHTMLContent
        {
            get;
            set;
        }

        public virtual string EnHTMLContent
        {
            get;
            set;
        }

        public virtual string FormKey
        {
            get;
            set;

        }

        public virtual int LevelOrder { get; set; }
        public virtual Help Parent
        {
            get;
            set;
        }

        public virtual IList<Help> ChildList
        {
            get;
            set;
        }

        #endregion

    }
}
