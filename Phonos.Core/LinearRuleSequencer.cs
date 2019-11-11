using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core
{
    public class LinearRuleSequencer : IRuleSequencer
    {
        public Rule[] Rules { get; }

        public LinearRuleSequencer(IEnumerable<Rule> rules)
        {
            Rules = rules.OrderBy(r => r.TimeSpan.Start)
                .ThenBy(r => r.TimeSpan.End).ToArray();
        }

        public WordDerivation Apply(Word originalWord)
        {
            var originalDerivation = new WordDerivation(null, null, originalWord);
            var derivations = new List<WordDerivation>() { originalDerivation };

            foreach (var rule in Rules)
            {
                var newDerivations = new List<WordDerivation>();

                foreach (var derivation in derivations)
                {
                    var results = rule.Apply(derivation.Derived)
                        .Select(w => new WordDerivation(rule, derivation.Derived, w))
                        .ToArray();

                    derivation.LaterDerivations = results;
                    newDerivations.AddRange(results);
                }

                derivations = newDerivations;
            }

            return originalDerivation;
        }
    }

    public class WordDerivation
    {
        public Rule Rule { get; }
        public Word Original { get; }
        public Word Derived { get; }
        public WordDerivation[] LaterDerivations { get; set; }

        public WordDerivation(Rule rule, Word original, Word derived, WordDerivation[] laterDerivations = null)
        {
            Rule = rule;
            Original = original;
            Derived = derived;
            LaterDerivations = laterDerivations;
        }
    }
}
