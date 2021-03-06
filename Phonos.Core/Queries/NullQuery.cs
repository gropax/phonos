﻿using Intervals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.Queries
{
    public class NullQuery : IQuery
    {
        public Interval<string[]> Match(Word word, int index, Interval scope = null)
        {
            var range = scope ?? new Interval(0, word.Phonemes.Length);

            if (!range.Contains(index))
                throw new ArgumentOutOfRangeException(nameof(index));
            else
                return new Interval<string[]>(index, 0, new string[0]);
        }
    }
}
