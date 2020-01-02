using Intervals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.Rules
{
    public abstract class RuleBase : IRule
    {
        public string Id { get; }
        public Interval TimeSpan { get; }
        public string[] PreAnalyzers { get; }
        public string[] PostAnalyzers { get; }

        protected RuleBase(string id, Interval timeSpan,
            string[] preAnalyzers, string[] postAnalyzers)
        {
            Id = id;
            TimeSpan = timeSpan;
            PreAnalyzers = preAnalyzers ?? new string[0];
            PostAnalyzers = postAnalyzers ?? new string[0];
        }

        public WordDerivation[] Derive(ExecutionContext context, WordDerivation derivation)
        {
            foreach (var analyzer in PreAnalyzers)
                context.RunAnalyzer(analyzer, derivation.Derived);

            var results = DeriveImplementation(context, derivation);

            foreach (var result in results)
                foreach (var analyzer in PostAnalyzers)
                    context.RunAnalyzer(analyzer, result.Derived);

            return results;
        }

        public abstract WordDerivation[] DeriveImplementation(ExecutionContext context, WordDerivation derivation);
    }
}
