using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model.AppSetting
{
    public interface IApplicationSettingsRepository : IRepository<ApplicationSettings>
    {
        ApplicationSettings GetSettings();
    }
}
