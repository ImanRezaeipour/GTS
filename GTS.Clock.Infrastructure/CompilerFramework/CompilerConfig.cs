using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;

namespace GTS.Clock.Infrastructure.CompilerFramework
{
    public class CompilerConfig
    {
        public string ReadReferencePath()
        {
            string config = ConfigurationSettings.AppSettings["CompileReferenceDirectory"];
            return config;
        }

        public string ReadOutputDLLFullPath()
        {
            string config = ConfigurationSettings.AppSettings["CompileOutputDLLFullPath"];
            return config;
        }

        public string ReadOutputCSharpCodeFullPath()
        {
            string config = ConfigurationSettings.AppSettings["CompileOutputCSharpCodeFullPath"];
            return config;
        }



    }
}
