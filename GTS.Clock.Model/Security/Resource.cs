using System;
using System.Collections.Generic;
using System.Linq;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model.Security
{
    public class Resource : IEntity
    {
        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual string ResourceID
        {
            get;
            set;
        }

        public virtual decimal ParentId
        {
            get;
            set;
        }

        public virtual string Description
        {
            get;
            set;
        }

        public virtual string MethodFullName
        {
            get;
            set;
        }

        public virtual string MethodPath
        {
            get;
            set;
        }

        public virtual string ParentPath { get; set; }

        public virtual string CheckKey { get; set; }

        public virtual SubSystemIdentifier SubSystemId { get; set; }


        public virtual IList<decimal> ParentPathList
        {
            get
            {
                List<decimal> list = new List<decimal>();
                string path = this.ParentPath == null ? "" : this.ParentPath;
                string[] ids = path.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string id in ids)
                {
                    list.Add(Utility.ToInteger(id));
                }
                return list;
            }
        }

        public virtual IList<Authorize> AuthorizeList
        {
            get;
            set;
        }

        public virtual IList<Resource> ChildList
        {
            get;
            set;
        }

        #endregion
    }
}
