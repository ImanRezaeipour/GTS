
using NHibernate.Engine;
using NHibernate.Impl;
using NHibernate.Loader.Collection;
using System;
using NHibernate.Persister.Entity;

namespace NHibernate.Persister.Collection
{
    public class NamedQueryCollectionInitializer : ICollectionInitializer
    {
        private readonly string queryName;
        private readonly ICollectionPersister persister;

        private static readonly IInternalLogger log = LoggerProvider.LoggerFor(typeof(NamedQueryCollectionInitializer));

        public NamedQueryCollectionInitializer(string queryName, ICollectionPersister persister)
        {
            this.queryName = queryName;
            this.persister = persister;
        }

        public void Initialize(object key, ISessionImplementor session)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(string.Format("initializing collection: {0} using named query: {1}", persister.Role, queryName));
            }

            //TODO: is there a more elegant way than downcasting?
            AbstractQueryImpl query = (AbstractQueryImpl)session.GetNamedSQLQuery(queryName);


            if (query.NamedParameters.Length > 0)
            {
                //این قسمت به دلیل حل نمودن مشکل 
                //not all named parameters have been set
                //اضافه شده است
                #region MyCode
                try
                {

                    foreach (string parameter in query.NamedParameters)
                    {
                        if (session.EnabledFilters.ContainsKey(parameter.Split('.')[0]))
                            query.SetParameter(parameter, session.GetFilterParameterValue(parameter));
                        else
                            query.SetParameter(parameter, key, persister.KeyType);
                        
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("{0}: {1}", "NamedQueryCollectionInitializer.Initialize", "خطا در کد نوشته شده توسط صفری نیا"), ex);
                }
                #endregion
            }
            query.SetCollectionKey(key).SetFlushMode(FlushMode.Never).List();
        }

        public void Initialize(object key, ISessionImplementor session, IEntityPersister OwnerEntityPersister, object owner)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(string.Format("initializing collection: {0} using named query: {1}", persister.Role, queryName));
            }

            //TODO: is there a more elegant way than downcasting?
            AbstractQueryImpl query = (AbstractQueryImpl)session.GetNamedSQLQuery(queryName);


            if (query.NamedParameters.Length > 0)
            {
                //این قسمت به دلیل حل نمودن مشکل 
                //not all named parameters have been set
                //اضافه شده است
                #region MyCode
                try
                {
                    foreach (string parameter in query.NamedParameters)
                    {
                        string alias = parameter.Split('.')[0];
                        int length = parameter.Split('.').Length;
                        if (session.EnabledFilters.ContainsKey(alias))
                        {
                            query.SetParameter(parameter, session.GetFilterParameterValue(parameter));
                        }
                        else
                        {
                            if (length > 1)
                            {
                                string paramName = parameter.Split('.')[1];
                                object value = OwnerEntityPersister.GetPropertyValue(owner, paramName, EntityMode.Poco);
                                if (value != null)
                                {
                                    query.SetParameter(parameter, value);
                                }
                            }
                            else
                            {
                                query.SetParameter(parameter, key, persister.KeyType);
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("{0}: {1}", "NamedQueryCollectionInitializer.Initialize", "خطا در کد نوشته شده توسط صفری نیا"), ex);
                }
                #endregion
            }
            query.SetCollectionKey(key).SetFlushMode(FlushMode.Never).List();
        }
    }
}