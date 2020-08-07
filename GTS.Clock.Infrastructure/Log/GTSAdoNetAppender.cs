using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.IO;
using System.Configuration;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Utility;
using log4net.Appender;

namespace GTS.Clock.Infrastructure.Log
{
    class GTSAdoNetAppender : AdoNetAppender
    {
        
       

        //public string ConnectionStringName { get; set; }
      
        public new string ConnectionString
        {
            get
            {
                try
                {                    
                    return base.ConnectionString;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            set
            {
                string conn = string.Empty;
                if (!Utility.Utility.IsEmpty(value))
                {
                    conn = ConfigurationManager.ConnectionStrings[value].ConnectionString;
                    base.ConnectionString = conn;

                }
                else
                {
                    throw new ValueNotProvidedException("log4net-->ConnectionStringName", "string", "log4net connection string name which is declared in web.config", "GTS.Clock.Infrastructure.Log.GTSAdoNetAppender.ConnectionString.Get");
                }
            }
        }

    }
}
