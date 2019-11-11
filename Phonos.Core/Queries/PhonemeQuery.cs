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

        public Interval<string[]> Match(Word word, int index)
        {
            if (index < 0 || index > word.Phonemes.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            else if (index == word.Phonemes.Length)
                return null;

            var first = word.Phonemes[index];
            if (Phonemes.Contains(first))
                return new Interval<string[]>(index, 1, new[] { first });
            else
                return null;
        }
    }
}
