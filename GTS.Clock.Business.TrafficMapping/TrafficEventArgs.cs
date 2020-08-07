using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model;


namespace GTS.Clock.Business.TrafficMapping
{
    public class TrafficEventArgs : EventArgs
    {
        public TrafficEventArgs() 
        {
            Reset();
        }
        #region Property
        public bool Cancel
        {
            get;
            set;
        }
        /// <summary>
        /// در صورتی که کاربر ایونت را کنسل کند باید این خصیصه را مقداردهی کند
        /// </summary>
        public object CustomeResult
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// حالت خصیصه های کلاس را مقداردهی اولیه میکند
        /// </summary>
        public void Reset() 
        {
            Cancel = false;
            CustomeResult = null;
        }
    }
}
