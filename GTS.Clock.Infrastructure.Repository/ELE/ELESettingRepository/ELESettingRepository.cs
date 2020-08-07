using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;

namespace GTS.Clock.Infrastructure.Repository
{                
    public class ELESettingRepository: RepositoryBase<ELESetting>, IELESettingRepository
    {
        public override string TableName 
        {
            get { return "TA_ELESetting"; }
        }
        public ELESettingRepository()
            : base()
        { }

        public ELESettingRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }
    }
}
