using System;
using System.Collections.Generic;
using System.Text;
using Intervals;

namespace Phonos.Core.Queries
{
    public class ZeroOrManyQuery : IQuery
    {
        public IQuery Query { get; }

        public ZeroOrManyQuery(IQuery query)
        {
            Query = query;
        }

        public Interval<string[]> Match(Word word, int index, Interval scope = null)
        {
            int end = index;

            while (true)
            {
                var match = Query.Match(word, end, scope);
                if (match == null)
                    return new Interval<string[]>(index, end - index,
                        word.Phonemes.SubArray(index, end - index));
                else
                    end = match.End;
            }
        }
    }
}
