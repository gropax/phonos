using Phonos.Core.Rules;
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

        public RuleSystemBuilder Rule(Rule rule)
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
        public static Rule[] System(Action<RuleSystemBuilder> systemDefinition)
        {
            var builder = new RuleSystemBuilder();
            systemDefinition(builder);
            var system = builder.Build();
            return system;
        }

        public static Rule Rule(Action<RuleContextBuilder> ruleDefinition)
        {
            var builder = new RuleContextBuilder();
            ruleDefinition(builder);
            var system = builder.Build();
            return system;
        }
    }
}
