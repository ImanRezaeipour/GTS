using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Charts;

namespace GTS.Clock.Model.AppSetting
{
    public class ApplicationSettings : IEntity
    {
        public ApplicationSettings() 
        {
            ID = 0;
            RuleDebug = false;
        }

        public virtual decimal ID
        {
            get;
            set;
        }
        
        public virtual bool RuleDebug
        {
            get;
            set;
        }

        public virtual bool RuleDurationDebug
        {
            get;
            set;
        }

        public virtual bool BusinessLogEnable
        {
            get;
            set;
        }

        public virtual bool BusinessErrorLogEnable
        {
            get;
            set;
        }

        
    }
}
