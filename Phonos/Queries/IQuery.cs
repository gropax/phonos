﻿using Intervals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Queries
{
    public interface IQuery
    {
        Interval<string[]> Match(Word word, int index);
        int Length { get; }
    }
}
