using Intervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.Queries
{
    public class WithGraphQuery : IQuery
    {
        public IQuery Query { get; }
        public HashSet<string> GraphValues { get; }
        public bool Negated { get; }

        public WithGraphQuery(IQuery query,IEnumerable<string> fieldValues, bool negated = false)
        {
            Query = query;
            GraphValues = new HashSet<string>(fieldValues);
            Negated = negated;
        }

        public Interval<string[]> Match(Word word, int index, Interval scope = null)
        {
            var match = Query.Match(word, index, scope);
            if (match == null)
                return null;

            bool matchGraph = word.GraphicalForms.Any(g =>
                g.Intervals.IntersectWith(match)
                    .Where(i => GraphValues.Contains(i.Value))
                    .Count() > 0);

            bool isMatch = matchGraph == !Negated;

            return isMatch ? match : null;
        }
    }
}
