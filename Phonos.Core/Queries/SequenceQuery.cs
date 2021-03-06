﻿using Intervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.Queries
{
    public class SequenceQuery : IQuery
    {
        public IQuery[] Queries { get; }

        public SequenceQuery(IEnumerable<IQuery> queries)
        {
            Queries = queries.ToArray();
        }

        public Interval<string[]> Match(Word word, int index, Interval scope = null)
        {
            int i = index;
            var matches = new List<Interval<string[]>>();

            foreach (var query in Queries)
            {
                var match = query.Match(word, i, scope);
                if (match == null)
                    return null;

                matches.Add(match);
                i = match.End;
            }

            return matches.ToArray()
                .Range(v => v.SelectMany(p => p).ToArray());
        }
    }
}
