using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.AppSetting;
using System.Linq;


namespace GTS.Clock.Infrastructure.Repository
{
    public class ApplicationSettingsRepository : RepositoryBase<ApplicationSettings>, IApplicationSettingsRepository
    {
        public override string TableName
        {
            get { return "TA_ApplicationSettings"; }
        }
        public ApplicationSettingsRepository()
            : base()
        { }

        public ApplicationSettingsRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }

        public ApplicationSettings GetSettings()
        {
            ApplicationSettings result = NHibernateSession.QueryOver<ApplicationSettings>()
                                                            .Take(1)
                                                            .SingleOrDefault();
            return result ?? new ApplicationSettings();


        }
    }
}
