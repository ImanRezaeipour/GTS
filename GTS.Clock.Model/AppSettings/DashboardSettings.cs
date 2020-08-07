using GTS.Clock.Model.AppSetting;
using GTS.Clock.Model.BaseInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.AppSetting
{
    public class DashboardSettings : IEntity
    {
        public virtual Decimal ID
        {
            get;
            set;
        }
        public virtual UserSettings UserSetting
        {
            get;
            set;
        }
        public virtual Dashboards Dashboard1
        {
            get;
            set;
        }
        public virtual Dashboards Dashboard2
        {
            get;
            set;
        }
        public virtual Dashboards Dashboard3
        {
            get;
            set;
        }
        public virtual Dashboards Dashboard4
        {
            get;
            set;
        }
        

    }
}
