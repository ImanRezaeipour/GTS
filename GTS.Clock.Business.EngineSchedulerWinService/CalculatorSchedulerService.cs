using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using GTS.Clock.Infrastructure.CalculatorSchedulerFramework.Configuration;
using GTS.Clock.Infrastructure.CalculatorSchedulerFramework;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.ELESchedulingService.ServiceReference;

namespace GTS.Clock.Business.EngineSchedulingService
{
    public partial class CalculatorSchedulerService : ServiceBase
    {
        #region variables

        private System.Timers.Timer schedulerTimer = new System.Timers.Timer();
        private CalculatorSchedulerSettings setting;
        private GTSWinSvcLogger GTSLogger = new GTSWinSvcLogger();
        #endregion


        public CalculatorSchedulerService()
        {
            InitializeComponent();
            this.setting = CalculatorSchedulerFactory.GetSetting();
            try
            {
                this.schedulerTimer.Elapsed += new System.Timers.ElapsedEventHandler(schedulerTimer_Elapsed);
            }
            catch (Exception ex)
            {
                GTSLogger.Error("ELE", Utility.GetExecptionMessage(ex));
                throw new BaseException(ex.Message, "GTSCalculatorSchedulerService");
            }
        }

        /// <summary>
        /// در اینجا تابع بررسی شرط هریک از زمانبندهای تعریف شده در فایل تنظیمات
        /// فراخوانی می گردد و در صورت رخداد شرایط هرکدام از آنها وب سرویس را فراخوانی می نماید
        /// همچنین وقایع انجام شده را در جدول لاگ ذخیره می کند
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void schedulerTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (CalculatorSchedulerFactory.IsConditionOccurenced(this.setting, DateTime.Now))
                {
                    TotalWebServiceClient ExcRuleSvc = new GTS.Clock.ELESchedulingService.ServiceReference.TotalWebServiceClient();
                    if (!Utility.IsEmpty(this.setting.GTSWebServiceAddress))
                    {
                        ExcRuleSvc = new GTS.Clock.ELESchedulingService.ServiceReference.TotalWebServiceClient("BasicHttpBinding_ITotalWebService", this.setting.GTSWebServiceAddress);
                    }
                    ExcRuleSvc.GTS_ExecuteAll("CalculatorSchedulerService");

                    GTSLogger.Logger.Info(@"وب سرویس اجراکننده قوانین فراخوانی شد");
                    if (!this.setting.BatchFlush)
                        GTSLogger.Flush();
                }
            }
            catch (Exception ex)
            {
                GTSLogger.Logger.Info(String.Format("{0}: {1}", "عدم موفقیت در فراخوانی وب سرویس", ex.Message));
                if (!this.setting.BatchFlush)
                    GTSLogger.Flush();                
            }
        }

        protected override void OnStart(string[] args)
        {
            int interval;
            if (int.TryParse(this.setting.Interval, out interval))
            {
                this.schedulerTimer.Interval = Convert.ToInt32(interval);
            }
            else
            {
                interval = 5000;
                this.schedulerTimer.Interval = Convert.ToInt32(5000);
            }
            this.schedulerTimer.Enabled = true;
            GTSLogger.Logger.Info(String.Format("سرویس آغاز به کار نمود با بسامد {0} ", interval));
            GTSLogger.Flush();
        }

        protected override void OnStop()
        {
            GTSLogger.Logger.Info(@"سرویس متوقف شد");
            GTSLogger.Flush();
            this.schedulerTimer.Enabled = false;
        }
    }
}
