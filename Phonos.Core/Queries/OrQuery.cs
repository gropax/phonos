using Intervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.Queries
{
    public class OrQuery : IQuery
    {
        public IQuery[] Queries { get; }

        public OrQuery(IEnumerable<IQuery> queries)
        {
            Queries = queries.ToArray();
        }

        public Interval<string[]> Match(Word word, int index, Interval scope = null)
        {
            foreach (var query in Queries)
            {
                var match = query.Match(word, index, scope);
                if (match != null)
                    return match;
            }
            return null;
        }
    }
}
