using Intervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.Queries
{
    public class PhonemeQuery : IQuery
    {
        public HashSet<string> Phonemes { get; }

        public PhonemeQuery(IEnumerable<string> phonemes)
        {
            Phonemes = new HashSet<string>(phonemes);
        }

        public Interval<string[]> Match(Word word, int index, Interval scope = null)
        {
            var range = scope ?? new Interval(0, word.Phonemes.Length);

            if (!range.Contains(index))
                throw new ArgumentOutOfRangeException(nameof(index));
            else if (index == range.End)
                return null;

            var first = word.Phonemes[index];
            if (Phonemes.Contains(first))
                return new Interval<string[]>(index, 1, new[] { first });
            else
                return null;
        }
    }
}
