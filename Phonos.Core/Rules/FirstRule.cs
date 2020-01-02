using Intervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.Rules
{
    public class FirstRule : RuleBase
    {
        public IRule[] Rules { get; }

        public FirstRule(string id, params IRule[] rules)
            : base(id, rules.Select(r => r.TimeSpan).Range(), null, null)
        {
            Rules = rules;
        }

        public FirstRule(string id, IRule[] rules, string[] preAnalyzers = null,
            string[] postAnalyzers = null)
            : base(id, rules.Select(r => r.TimeSpan).Range(), preAnalyzers, postAnalyzers)
        {
            Rules = rules;
        }

        public override WordDerivation[] DeriveImplementation(ExecutionContext context, WordDerivation derivation)
        {
            return First(r => r.Derive(context, derivation));
        }

        private T[] First<T>(Func<IRule, T[]> func)
        {
            foreach (var rule in Rules)
            {
                var ts = func(rule);
                if (ts.Length > 0)
                    return ts;
            }
            return new T[0];
        }
    }
}
