using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.RuleBuilder
{
    public class RuleSystemBuilder
    {
        private List<Rule> _rules = new List<Rule>();

        public Rule[] Build()
        {
            return _rules.ToArray();
        }

        public RuleSystemBuilder Rule(Action<RuleBuilder> ruleDefinition)
        {
            var builder = new RuleBuilder();
            ruleDefinition(builder);
            var rule = builder.Build();
            _rules.Add(rule);
            return this;
        }
    }

    public static class R
    {
        public static Rule[] System(Action<RuleSystemBuilder> systemDefinition)
        {
            var builder = new RuleSystemBuilder();
            systemDefinition(builder);
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
                    .Named("My rule")
                    .From(0).To(200)
                    .Match(q => q.Phon("a").With("accent", "tonic"))
                    .Before(q => q.Seq(Q.Start, q2 => q2.Phon("b").With("auie", "nrst")))
                    .After(Q.End)
                    .Map(m => m.Phono(P.Degeminate).Rewrite())));
        }
    }
}
