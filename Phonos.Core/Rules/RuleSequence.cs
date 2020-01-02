using Intervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.Rules
{
    public class RuleSequence : RuleBase
    {
        public IRule[] Rules { get; }

        public RuleSequence(string id, params IRule[] rules)
            : base(id, rules.Select(r => r.TimeSpan).Range(), null, null)
        {
            Rules = rules;
        }

        public RuleSequence(string id, IRule[] rules, string[] preAnalyzers = null,
            string[] postAnalyzers = null)
            : base(id, rules.Select(r => r.TimeSpan).Range(), preAnalyzers, postAnalyzers)
        {
            Rules = rules;
        }

        public override WordDerivation[] DeriveImplementation(ExecutionContext context, WordDerivation derivation)
        {
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
        }
    }
}
