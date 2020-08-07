using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
    public interface IPairableConceptValue<TPair>: IConceptValue
        where TPair: IPair
    {
        IList<TPair> Pairs { get; set; }
        TPair First { get; }
        TPair Last { get; }
        TPair Intermediate { get; }
        int PairCount { get; }
        TPair PairPart(int Part);
        int PairValues { get; }
        string ExPairValues { get; }
        void AddPairs(IList<TPair> Source);
        void AppendPairs(IList<TPair> Source);
        void RemovePair(TPair Pair);
        int TotalBeforeTime(int Time);
        int TotalAfterTime(int Time);
        void RemovePairAt(int index);
    }
}
