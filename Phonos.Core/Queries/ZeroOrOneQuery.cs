using System;
using System.Collections.Generic;
using System.Text;
using Intervals;

namespace Phonos.Core.Queries
{
    public class MaybeQuery : IQuery
    {
        public IQuery Query { get; }

        public MaybeQuery(IQuery query)
        {
            Query = query;
        }

        public Interval<string[]> Match(Word word, int index, Interval scope = null)
        {
            return Query.Match(word, index, scope) ??
                new Interval<string[]>(index, 0, new string[0]);
        }
    }
}
