using System;
using System.Collections.Generic;
using System.Linq;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;
using System.Web.Security;

namespace GTS.Clock.Model.Security
{
    public class Role : IEntity
    {
        public Role()
        { 
        
        }

        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual bool Active
        {
            get;
            set;
        }

     

        public virtual decimal ParentId
        {
            get;
            set;
        }

        public virtual string CustomCode
        {
            get;
            set;
        }

        public virtual IList<User> UserList
        {
            get;
            set;
        }

        public virtual IList<Authorize> AuthorizeList
        {
            get;
            set;
        }

        public virtual string Schema
        {
            get;
            set;
        }

        public virtual IList<Role> ChildList { get; set; }

        //public virtual IList<Precard> AccessPrecardList
        //{
        //    get;
        //    set;
        //}

        #endregion    

        public override string ToString()
        {
            string summery = "";
            summery = String.Format("نام:{0} کد اختصاصی:{1} میباشد ", this.Name, this.CustomCode);
            return summery;
        }
    }
}
