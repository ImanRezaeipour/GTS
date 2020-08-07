using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using GTS.Clock.Business.Security;

/// <summary>
/// Summary description for BusinessHelper
/// </summary>
public class BusinessHelper
{
	public BusinessHelper()
	{
	}

    public static T GetBusinessInstance<T>(params KeyValuePair<string,object>[] ParamsBatch)
    {
        T BusinessInstance = default(T);

        IUnityContainer container = new UnityContainer();
        container.AddNewExtension<Interception>();

        if (ParamsBatch.Count() > 0)
        {
            container.RegisterType(typeof(T), new Interceptor<TransparentProxyInterceptor>(), new InterceptionBehavior<BusinessServiceBehavior>());

            var ResolverOverrideBatch = new ResolverOverride[ParamsBatch.Count()];
            for (int i = 0; i < ParamsBatch.Count(); i++)
            {
                ResolverOverrideBatch[i] = new ParameterOverride(ParamsBatch[i].Key, ParamsBatch[i].Value);
            }
            BusinessInstance = container.Resolve<T>(ResolverOverrideBatch);
        }
        else
        {
            container.RegisterType(typeof(T), new Interceptor<TransparentProxyInterceptor>(), new InterceptionBehavior<BusinessServiceBehavior>(), new InjectionConstructor());
            BusinessInstance = UnityContainerExtensions.Resolve<T>(container);
        }

        return BusinessInstance;
    }

}