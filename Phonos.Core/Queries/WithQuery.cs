using Intervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.Queries
{
    public class WithQuery : IQuery
    {
        public IQuery Query { get; }
        public string FieldName { get; }
        public HashSet<string> FieldValues { get; }

        public WithQuery(IQuery query, string fieldName, IEnumerable<string> fieldValues)
        {
            Query = query;
            FieldName = fieldName;
            FieldValues = new HashSet<string>(fieldValues);
        }

        public Interval<string[]> Match(Word word, int index, Interval scope = null)
        {
            var match = Query.Match(word, index, scope);
            if (match == null)
                return null;

            var field = word.GetField(FieldName);
            bool matchField = field.Intervals.IntersectWith(match)
                .Where(i => FieldValues.Contains(i.Value))
                .Count() > 0;

            return matchField ? match : null;
        }
    }
}
