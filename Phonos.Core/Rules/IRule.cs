using Intervals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.Rules
{
    public interface IRule
    {
        string Id { get; }
        Interval TimeSpan { get; }
        WordDerivation[] Derive(WordDerivation derivation);
        Word[] Apply(Word word);
    }
}
