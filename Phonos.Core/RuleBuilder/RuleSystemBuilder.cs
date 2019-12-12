using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.RuleBuilder
{
    public class RuleSystemBuilder
    {
        private List<RuleContext> _rules = new List<RuleContext>();

        public RuleContext[] Build()
        {
            return _rules.ToArray();
        }

        public RuleSystemBuilder Rule(RuleContext rule)
        {
            _rules.Add(rule);
            return this;
        }

        public RuleSystemBuilder Rule(Action<RuleContextBuilder> ruleDefinition)
        {
            var builder = new RuleContextBuilder();
            ruleDefinition(builder);
            var rule = builder.Build();
            _rules.Add(rule);
            return this;
        }
    }

    public static class R
    {
        public static RuleContext[] System(Action<RuleSystemBuilder> systemDefinition)
        {
            var builder = new RuleSystemBuilder();
            systemDefinition(builder);
            var system = builder.Build();
            return system;
        }

        public static RuleContext Rule(Action<RuleContextBuilder> ruleDefinition)
        {
            var builder = new RuleContextBuilder();
            ruleDefinition(builder);
            var system = builder.Build();
            return system;
        }
    }

    public static class Dev
    {
        public static void Method()
        {
            var rules = R.System(s => s
                .Rule(r => r
                    .From(0).To(200)
                    .Query(q => q
                        .Match(m => m.Phon("a").With("accent", "tonic"))
                        .Before(b => b.Seq(Q.Start, q2 => q2.Phon("b").With("auie", "nrst")))
                        .After(Q.End))
                    .Rules(m => m
                        .Named("My rule")
                        .Phono(P.Degeminate)
                        .Rewrite())));
        }
    }
}
