using Intervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.Rules
{
    public class RuleSequence : IRule
    {
        public string Id { get; }
        public IRule[] Rules { get; }
        public Interval TimeSpan => Rules.Select(r => r.TimeSpan).Range();

        public RuleSequence(string id, params IRule[] rules)
        {
            Id = id;
            Rules = rules;
        }

        public WordDerivation[] Derive(WordDerivation derivation)
        {
            var derivations = new WordDerivation[] { derivation };

            foreach (var rule in Rules)
                derivations = derivations.SelectMany(d => rule.Derive(d)).ToArray();

            return derivations.ToArray();
        }

        public Word[] Apply(Word word)
        {
            var words = new Word[] { word };

            foreach (var rule in Rules)
                words = words.SelectMany(w => rule.Apply(w)).ToArray();

            return words.ToArray();
        }
    }
}
