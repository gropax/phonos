using Intervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.Rules
{
    public class FirstRule : IRule
    {
        public string Id { get; }
        public IRule[] Rules { get; }
        public string[] Analyzers { get; }
        public Interval TimeSpan => Rules.Select(r => r.TimeSpan).Range();

        public FirstRule(string id, params IRule[] rules)
        {
            Id = id;
            Rules = rules;
            Analyzers = new string[0];
        }

        public FirstRule(string id, IRule[] rules, string[] analyzers = null)
        {
            Id = id;
            Rules = rules;
            Analyzers = analyzers ?? new string[0];
        }

        public WordDerivation[] Derive(WordDerivation derivation)
        {
            return First(r => r.Derive(derivation));
        }

        public Word[] Apply(Word word)
        {
            return First(r => r.Apply(word));
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
