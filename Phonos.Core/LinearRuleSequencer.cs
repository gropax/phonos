using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core
{
    public class LinearRuleSequencer : IRuleSequencer
    {
        public RuleContext[] Rules { get; }

        public LinearRuleSequencer(IEnumerable<RuleContext> rules)
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
                    var results = rule.Apply(derivation.Derived);

                    if (results.Length > 0)
                    {
                        var dx = results.Select(w => new WordDerivation(rule, derivation.Derived, w)).ToArray();
                        derivation.LaterDerivations = dx;
                        newDerivations.AddRange(dx);
                    }
                    else
                        newDerivations.Add(derivation);

                }

                derivations = newDerivations;
            }

            return originalDerivation;
        }
    }

    public class WordDerivation
    {
        public RuleContext Rule { get; }
        public Word Original { get; }
        public Word Derived { get; }
        public WordDerivation[] LaterDerivations { get; set; }

        public WordDerivation(RuleContext rule, Word original, Word derived, WordDerivation[] laterDerivations = null)
        {
            Rule = rule;
            Original = original;
            Derived = derived;
            LaterDerivations = laterDerivations ?? new WordDerivation[0];
        }

        public IEnumerable<Word> FinalWords()
        {
            var q = new Stack<WordDerivation>();
            q.Push(this);

            while (q.Count > 0)
            {
                var derivation = q.Pop();
                if (derivation.LaterDerivations.Length > 0)
                    foreach (var d in derivation.LaterDerivations)
                        q.Push(d);
                else
                    yield return derivation.Derived;
            }
        }
    }
}
