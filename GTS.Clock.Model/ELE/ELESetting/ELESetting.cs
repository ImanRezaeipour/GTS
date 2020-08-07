using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.RepositoryFramework;

namespace GTS.Clock.Model
{ 
    public class ELESetting: IEntity
    {

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual DateTime LastExecuteDate
        {
            get;
            set;
        }

        public virtual PersianDateTime GetLastExecuteDate()
        {
            return new PersianDateTime(this.LastExecuteDate);
        }

        #endregion

        #region Methods

        public static IELESettingRepository GetELESettingRepository(bool Disconnectedly)
        {
            return RepositoryFactory.GetRepository<IELESettingRepository, ELESetting>(Disconnectedly);
        }

        #endregion
    }
}
