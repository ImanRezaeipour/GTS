using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.TranslatorFramework
{
    public interface IGTSSTranslator<TGTSSDictionary>
                    where TGTSSDictionary : IGTSSDictionary
    {
        TGTSSDictionary GTSSDictionary { get; }
    }
}
