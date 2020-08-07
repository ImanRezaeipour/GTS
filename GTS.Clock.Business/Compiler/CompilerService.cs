using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using GTS.Clock.Model;
using System.Collections.Generic;
using GTS.Clock.Business.Compiler;

namespace GTS.Clock.Model.AppService
{
    [ServiceContract]
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single,
        InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = 
        AspNetCompatibilityRequirementsMode.Required)]
    public class ExeceuteCompilerService
    {
        [OperationContract(IsOneWay = true)]
        public void CompileAssemblies() 
        {
            Compiler compiler = new Compiler();
            compiler.Run();
        }

    }
}