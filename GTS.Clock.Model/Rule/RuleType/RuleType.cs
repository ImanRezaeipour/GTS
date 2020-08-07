using System;
using System.Collections.Generic;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.CustomeXMLConvertor;
using GTS.Clock.Infrastructure.RepositoryFramework;


namespace GTS.Clock.Model
{
    public class RuleType: IEntity
    {
        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual IList<Rule> RuleList
        {
            get;
            set;
        }

        public virtual IList<RuleTemplate> RuleTemplateList
        {
            get;
            set;
        }

        #endregion

    }
}