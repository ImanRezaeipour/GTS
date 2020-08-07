using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using System.Diagnostics;
using System.Threading;

namespace GTS.Clock.Business.Engine
{
    public delegate void FinishedCallback(string CallerIdentity, decimal PersonId);

    public class ExecutableThread
    {
        public class ThreadParam
        {
            public string CallerIdentity;
            public decimal ExecutablePrsCalcId;
            public bool ExecuteByRobot;
            public Decimal PersonId;
            public DateTime FromDate;
            public DateTime ToDate;
            public Thread ThreadContext;
            public FinishedCallback FinishedCallback
            {
                get;
                set;
            }
        }

        public class GroupThreadParam
        {
            public GroupThreadParam()
            {
                this.ThreadParams = new List<ThreadParam>();
            }
            public IList<ThreadParam> ThreadParams { get; set; }
        }


        public ExecutableThread(string CallerIdentity, Decimal ExecutablePrsCalcID, Decimal PersonId, DateTime FromDate, DateTime ToDate, FinishedCallback Callback)
        {
            Executing = false;
            this.Param = new ThreadParam
            {
                ExecuteByRobot = false,
                CallerIdentity = CallerIdentity,
                ExecutablePrsCalcId = ExecutablePrsCalcID,
                PersonId = PersonId,
                FromDate = FromDate,
                ToDate = ToDate,
                ThreadContext = null,
                FinishedCallback = Callback
            };
            Duration = new Stopwatch();
        }

        public bool Executing { get; set; }

        public Stopwatch Duration { get; set; }

        public ThreadParam Param;

    }       
}