using Intervals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.Queries
{
    public class NotQuery : IQuery
    {
        public IQuery Query { get; }

        public NotQuery(IQuery query)
        {
            Query = query;
        }

        public Interval<string[]> Match(Word word, int index, Interval scope = null)
        {
            var match = Query.Match(word, index, scope);
            if (match != null)
                return null;
            else
                return new Interval<string[]>(scope, new string[0]);
        }
    }
}
