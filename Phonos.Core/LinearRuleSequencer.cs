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

        public LinearRuleSequencer(IEnumerable<IRule> rules)
        {
            Rules = rules
                .OrderBy(r => r.TimeSpan.Start)
                .ThenBy(r => r.TimeSpan.End)
                .ToArray();
        }

        public WordDerivation[] Derive(Word word)
        {
            var originalDerivation = WordDerivation.Origin(word);
            var derivations = new WordDerivation[] { originalDerivation };

            foreach (var rule in Rules)
                derivations = derivations.SelectMany(d => rule.Derive(d)).ToArray();

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
