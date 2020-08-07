using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace GTS.Clock.Business.NotificationServices
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new GTSNotificationService() 
            };
            ServiceBase.Run(ServicesToRun);
            //Application.Run(new Form1());
        }
    }
}
