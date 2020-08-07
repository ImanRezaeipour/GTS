using NHibernate.Engine;
using NHibernate.Persister.Entity;

namespace NHibernate.Loader.Collection
{
	/// <summary>
	/// An interface for collection loaders
	/// </summary>
	/// <seealso cref="BasicCollectionLoader"/>
	/// <seealso cref="OneToManyLoader"/>
	public interface ICollectionInitializer
	{
		/// <summary>
		/// Initialize the given collection
		/// </summary>
		void Initialize(object id, ISessionImplementor session);

        /// <summary>
        /// Initialize the given collection
        /// </summary>
        void Initialize(object key, ISessionImplementor session, IEntityPersister OwnerEntityPersister, object owner);
    }
}