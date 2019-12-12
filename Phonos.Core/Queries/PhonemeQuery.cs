using Intervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.Queries
{
    public class PhonemeQuery : IQuery
    {
        public Func<string, bool> Condition { get; }

        public PhonemeQuery(Func<string, bool> condition)
        {
            Condition = condition;
        }

        public PhonemeQuery(IEnumerable<string> phonemes)
        {
            var phonemeSet = new HashSet<string>(phonemes);
            Condition = p => phonemeSet.Contains(p);
        }

        public Interval<string[]> Match(Word word, int index, Interval scope = null)
        {
            var range = scope ?? new Interval(0, word.Phonemes.Length);

            if (!range.Contains(index))
                throw new ArgumentOutOfRangeException(nameof(index));
            else if (index == range.End)
                return null;

            var first = word.Phonemes[index];
            if (Condition(first))
                return new Interval<string[]>(index, 1, new[] { first });
            else
                return null;
        }
    }
}
