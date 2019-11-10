using Intervals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Queries
{
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
}
