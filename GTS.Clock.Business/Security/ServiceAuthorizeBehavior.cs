using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Security
{
    public enum ServiceAuthorizeState
    {
        Enforce,
        Avoid
    }

    public class ServiceAuthorizeBehavior : Attribute
    {
        public ServiceAuthorizeState serviceAuthorizeState;

        public ServiceAuthorizeBehavior(ServiceAuthorizeState SAS)
        {
            this.serviceAuthorizeState = SAS;
        }
    }
}
