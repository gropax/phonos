using Phonos.Core.Analyzers;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core
{
    public class LinearRuleSequencer : IRuleSequencer
    {
        public IRule[] Rules { get; }
        public Dictionary<string, IAnalyzer> Analyzers { get; }

        public LinearRuleSequencer(IEnumerable<IRule> rules,
            Dictionary<string, IAnalyzer> analyzers = null)
        {
            Rules = rules
                .OrderBy(r => r.TimeSpan.Start)
                .ThenBy(r => r.TimeSpan.End)
                .ToArray();
            Analyzers = analyzers ?? new Dictionary<string, IAnalyzer>();
        }

        public WordDerivation[] Derive(Word word)
        {
            var originalDerivation = WordDerivation.Origin(word);

            var derivations = new WordDerivation[] { originalDerivation };
            var newDerivations = new List<WordDerivation>();

            foreach (var rule in Rules)
            {
                foreach (var derivation in derivations)
                {
                    foreach (var analyzer in rule.Analyzers)
                        Analyzers[analyzer].Analyze(derivation.Derived);

                    var results = rule.Derive(derivation);
                    if (results.Length > 0)
                        newDerivations.AddRange(results);
                    else
                        newDerivations.Add(derivation);
                }

                derivations = newDerivations.ToArray();
                newDerivations.Clear();
            }

            return derivations.ToArray();
        }
    }

    public class WordDerivation
    {
        public Rule Rule { get; }
        public Word Original { get; }
        public Word Derived { get; }
        public WordDerivation Previous { get; set; }
        //public WordDerivation[] LaterDerivations { get; set; }

        public WordDerivation(Rule rule, Word original, Word derived, WordDerivation previous)
        {
            Rule = rule;
            Original = original;
            Derived = derived;
            Previous = previous;
        }

        public static WordDerivation Origin(Word word)
        {
            return new WordDerivation(null, null, word, null);
        }


        //public WordDerivation(Rule rule, Word original, Word derived, WordDerivation[] laterDerivations = null)
        //{
        //    Rule = rule;
        //    Original = original;
        //    Derived = derived;
        //    LaterDerivations = laterDerivations ?? new WordDerivation[0];
        //}

        //public IEnumerable<Word> FinalWords()
        //{
        //    var q = new Stack<WordDerivation>();
        //    q.Push(this);

        //    while (q.Count > 0)
        //    {
        //        var derivation = q.Pop();
        //        if (derivation.LaterDerivations.Length > 0)
        //            foreach (var d in derivation.LaterDerivations)
        //                q.Push(d);
        //        else
        //            yield return derivation.Derived;
        //    }
        //}
    }
}
