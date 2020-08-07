using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using GTS.Clock.Infrastructure.Log;


namespace GTS.Clock.Business.NotificationServices
{
    partial class GTSNotificationService : ServiceBase
    {
        private static GTSWinSvcLogger logger = new GTSWinSvcLogger();
        private static System.Timers.Timer schadulerTimer;
        private static int Counter = 0;

        public GTSNotificationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.

            schadulerTimer = new System.Timers.Timer(10000);

            // Hook up the Elapsed event for the timer.
            schadulerTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Set the Interval to 2 seconds (2000 milliseconds).
            schadulerTimer.Interval = 2000;
            schadulerTimer.Enabled = true;
          
            
            logger.Info("", "GTSNotificationService Started");

        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.

            schadulerTimer.Stop();
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Counter++;
            logger.Info(Counter.ToString(), "The Elapsed event was raised at ");
        }

    }
}
