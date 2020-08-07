using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.TranslatorFramework
{
    /// <summary>
    /// .کلاس پایه برای مترجم ها
    /// <typeparam name="TGTSSDictionary">.نوع دیکشنری مورد استفاده توسط مترجم</typeparam>
    /// </summary>
    public abstract class BaseGTSSTranslator<TGTSSDictionary>: IGTSSTranslator<TGTSSDictionary>
        where TGTSSDictionary : IGTSSDictionary

    {
        TGTSSDictionary _GTSSDictionary;

        public BaseGTSSTranslator(TGTSSDictionary GTSSDictionary)
        {
            this._GTSSDictionary = GTSSDictionary;
        }

        #region IGTSSTranslator<TGTSSDictionary> Members

        public TGTSSDictionary GTSSDictionary
        {
            get
            {
                return this._GTSSDictionary;
            }
        }

        #endregion


    }
}
