using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    /// <summary>
    /// [G. Zink, Phonétique historique du français, ch. VI]
    /// </summary>
    public static class Part1Chapter13
    {
        public static IRule[] Rules()
        {
            return new IRule[]
            {
                RuleSet1a(),
            };
        }

        public static IRule[] RuleComponents()
        {
            return new[]
            {
                Rule1a(), Rule1b(),
                RuleSet1a(),
            };
        }

        public static IRule RuleSet1a()
        {
            return new FirstRule("p1c13s1a",
                Rule1a(),  // monosyllabic
                Rule1b()); // other
        }

        public static Rule Rule1a()
        {
            return R.Rule(c => c
                .Id("p1c13r1a")
                .Group("")
                .From(0).To(100)
                .Query(q => q
                    .Match(m => m.Phon("m").With("accent", "tonic"))
                    .After(Q.End))
                .Rules(
                    r => r
                        .Named("Conservation du /m/ finale dans les monosyllabes")
                        .Phono(P.Nothing),
                    r => r
                        .Named("Chute du /m/ final")
                        .Phono(P.Erase)
                        .Rewrite(G.Erase)));
        }

        public static Rule Rule1b()
        {
            return R.Rule(c => c
                .Id("p1c13r1b")
                .Group("")
                .From(0).To(100)
                .Query(q => q
                    .Match(m => m.Phon("m"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Chute du /m/ final")
                    .Phono(P.Erase)
                    .Rewrite(G.Erase)));
        }
    }
}
