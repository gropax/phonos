using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intervals;

namespace Phonos.Core.Queries
{
    public class TwiceQuery : IQuery
    {
        public IQuery Query { get; }

        public TwiceQuery(IQuery query)
        {
            Query = query;
        }

        public Interval<string[]> Match(Word word, int index, Interval scope = null)
        {
            var match = Query.Match(word, index, scope);
            if (match == null)
                return null;

            var match2 = Query.Match(word, match.End, scope);
            if (match2 != null && Enumerable.SequenceEqual(match.Value, match2.Value))
                return new Interval<string[]>(index, match2.End - index,
                    word.Phonemes.SubArray(index, match2.End - index));
            else
                return null;
        }
    }
}
