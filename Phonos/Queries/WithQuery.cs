﻿using Intervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Queries
{
    public class WithQuery : IQuery
    {
        public IQuery Query { get; }
        public int Length => Query.Length;
        public string FieldName { get; }
        public HashSet<string> FieldValues { get; }

        public WithQuery(IQuery query, string fieldName, HashSet<string> fieldValues)
        {
            Query = query;
            FieldName = fieldName;
            FieldValues = fieldValues;
        }

        public Interval<string[]> Match(Word word, int index)
        {
            var match = Query.Match(word, index);
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
