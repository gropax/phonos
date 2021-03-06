﻿using Intervals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.Rules
{
    public interface IRule
    {
        string Id { get; }
        Interval TimeSpan { get; }
        string[] PreAnalyzers { get; }
        string[] PostAnalyzers { get; }

        WordDerivation[] Derive(ExecutionContext context, WordDerivation derivation);
    }
}
