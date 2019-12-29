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
        public string[] Analyzers { get; }

        public Interval TimeSpan => Rules.Select(r => r.TimeSpan).Range();

        public RuleSequence(string id, params IRule[] rules)
        {
            Id = id;
            Analyzers = new string[0];
            Rules = rules;
        }

        public RuleSequence(string id, IRule[] rules, string[] analyzers)
        {
            Id = id;
            Rules = rules;
            Analyzers = analyzers ?? new string[0];
        }

        public WordDerivation[] Derive(ExecutionContext context, WordDerivation derivation)
        {
            foreach (var analyzer in Analyzers)
                context.RunAnalyzer(analyzer, derivation.Derived);

            var derivations = new WordDerivation[] { derivation };
            var newDerivations = new List<WordDerivation>();

            foreach (var rule in Rules)
            {
                foreach (var d in derivations)
                {
                    var results = rule.Derive(context, d);
                    if (results.Length > 0)
                        newDerivations.AddRange(results);
                    else
                        newDerivations.Add(d);
                }

                derivations = newDerivations.ToArray();
                newDerivations.Clear();
            }

            return derivations.ToArray();

            return derivations.ToArray();
        }

        public Word[] Apply(ExecutionContext context, Word word)
        {
            var words = new Word[] { word };

            foreach (var rule in Rules)
                words = words.SelectMany(w => rule.Apply(context, w)).ToArray();

            return words.ToArray();
        }
    }
}
