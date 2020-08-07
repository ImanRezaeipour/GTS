using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.RepositoryFramework;
using NHibernate.Mapping;
using GTS.Clock.Model;

namespace GTS.Clock.Infrastructure.Repository
{
    /// <summary>
    ///  این کلاس برای موجودیت هایی استفاده میشود که به هیچ سرویس اضافی احتیاج ندارند
    ///  و تنها سرویسهای موجود در والد آنها کفایت میکند
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityRepository<T> : RepositoryBase<T>
    {
        public override string TableName
        {
            get { throw new NotImplementedException(); }
        }

        public EntityRepository() 
            :base(false)
        {

        }

        public EntityRepository(bool Disconnectedly)
            : base(Disconnectedly)
        {

        }
    }
}
