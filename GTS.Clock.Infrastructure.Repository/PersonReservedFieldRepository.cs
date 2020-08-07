using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.RepositoryFramework;
using NHibernate.Mapping;
using GTS.Clock.Model;
using GTS.Clock.Model.PersonInfo;

namespace GTS.Clock.Infrastructure.Repository
{
    public class PersonReservedFieldRepository : RepositoryBase<PersonReserveField>
    {
        public override string TableName
        {
            get { throw new NotImplementedException(); }
        }

         public PersonReservedFieldRepository()
            : base()
        { }

         public PersonReservedFieldRepository(bool Disconnectedly)
            :base(Disconnectedly)
        { }
    }
}
