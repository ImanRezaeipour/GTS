using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;

namespace GTS.Clock.Business.Security
{
    public class BusinessServiceBehavior : IInterceptionBehavior
    {
        public ResourceRepository resourceRepository
        {
            get
            {
                return new ResourceRepository();
            }
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            IMethodReturn msg = null;
            if (BUser.CurrentUser != null)
            {
                bool IsAuthorizableService = false;
                foreach (var customAttribute in input.MethodBase.GetCustomAttributes(false))
                {
                    if (customAttribute is ServiceAuthorizeBehavior)
                    {
                        IsAuthorizableService = true;
                        ServiceAuthorizeBehavior SAB = (ServiceAuthorizeBehavior)customAttribute;
                        switch (SAB.serviceAuthorizeState)
                        {
                            case ServiceAuthorizeState.Enforce:
                                ServiceAuthorizeType SAT = this.resourceRepository.CheckServiceAuthorize(BUser.CurrentUser.Role.ID, input);
                                switch (SAT)
                                {
                                    case ServiceAuthorizeType.Illegal:
                                        msg = input.CreateExceptionMethodReturn(new IllegalServiceAccess("دسترسی غیر مجاز به سرویس", input.Target.ToString()));
                                        BaseBusiness<Entity>.LogException(new IllegalServiceAccess("دسترسی غیر مجاز به سرویس", input.Target.ToString()), input.Target.GetType().Name, input.MethodBase.Name);                                        
                                        break;
                                    case ServiceAuthorizeType.Legal:
                                        msg = getNext()(input, getNext);
                                        break;
                                }
                                break;
                            case ServiceAuthorizeState.Avoid:
                                msg = getNext()(input, getNext);
                                break;
                        }
                        break;
                    }
                }
                if (!IsAuthorizableService)
                    msg = getNext()(input, getNext);
            }
            return msg;
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}
