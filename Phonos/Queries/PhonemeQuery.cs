using Intervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Queries
{
    public interface IQuery
    {
        Interval<string[]> Match(Word word, int index);
        int Length { get; }
    }

    public class PhonemeQuery : IQuery
    {
        public HashSet<string> Phonemes { get; }
        public int Length => 1;

        public PhonemeQuery(IEnumerable<string> phonemes)
        {
            Phonemes = new HashSet<string>(phonemes);
        }

        public Interval<string[]> Match(Word word, int index)
        {
            if (index < 0 || index > word.Phonemes.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            else if (index == word.Phonemes.Length)
                return null;

            var first = word.Phonemes[0];
            if (Phonemes.Contains(first))
                return new Interval<string[]>(index, 1, new[] { first });
            else
                return null;
        }
    }

    public class SequenceQuery : IQuery
    {
        public IQuery[] Queries { get; }
        public int Length => Queries.Sum(q => q.Length);

        public SequenceQuery(IEnumerable<IQuery> queries)
        {
            Queries = queries.ToArray();
        }

        public Interval<string[]> Match(Word word, int index)
        {
            int i = index;
            var matches = new List<Interval<string[]>>();

            foreach (var query in Queries)
            {
                var match = query.Match(word, i);
                if (match == null)
                    return null;

                matches.Add(match);
                i = match.End;
            }

            return matches.ToArray()
                .Range(v => v.SelectMany(p => p).ToArray());
        }
    }

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

    public class NullQuery : IQuery
    {
        public int Length => 0;
        public Interval<string[]> Match(Word word, int index)
        {
            if (index < 0 || index > word.Phonemes.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            else
                return new Interval<string[]>(index, 0, new string[0]);
        }
    }

    public class StartAnchorQuery : IQuery
    {
        public int Length => 0;
        public Interval<string[]> Match(Word word, int index)
        {
            if (index < 0 || index > word.Phonemes.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            else if (index == 0)
                return new Interval<string[]>(index, 0, new string[0]);
            else
                return null;
        }
    }

    public class EndAnchorQuery : IQuery
    {
        public int Length => 0;
        public Interval<string[]> Match(Word word, int index)
        {
            if (index < 0 || index > word.Phonemes.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            else if (index == word.Phonemes.Length)
                return new Interval<string[]>(index, 0, new string[0]);
            else
                return null;
        }
    }
}
