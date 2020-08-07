using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Security;

namespace GTS.Clock.Business.Proxy
{
    /// <summary>
    /// Resource Class + IsAllowed Property ==> for presenter
    /// </summary>
    public class ResourceProxy
    {

        public ResourceProxy(Resource resource, decimal roleId)
        {
            ID = resource.ID;
            ResourceName = resource.Description;
            ParentId = resource.ParentId;
            RoleID = roleId;
        }

        public bool IsAllowed
        {
            get;
            set;

        }

        public decimal ID
        {
            get;
            set;
        }

        public decimal ParentId
        {
            get;
            set;
        }

        public string ResourceName { get; set; }

        public decimal RoleID
        {
            get;
            set;
        }

        public IList<ResourceProxy> ChildList
        {
            get;
            set;
        }

    }
}
