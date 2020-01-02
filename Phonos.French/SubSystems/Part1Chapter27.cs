using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    /// <summary>
    /// [G. Zink, Phonétique historique du français, ch. XXVII p.167]
    /// </summary>
    public static class Part1Chapter27
    {
        public static IRule[] Rules()
        {
            return new IRule[]
            {
                RuleSet1(),
            };
        }

        public static IRule[] RuleComponents()
        {
            return new[]
            {
                Rule2a(),
                Rule2b(),
                RuleSet1(),
            };
        }

        /// <summary>
        /// Lois de position : ouvertures de /e/ et /ø/ en syllabe fermée
        /// </summary>
        public static IRule RuleSet1()
        {
            return new RuleSequence("p1c27s1", new []
            {
                Rule2a(),
                Rule2b(),
            }, preAnalyzers: new[] { "syllable" });
        }

        public static Rule Rule2a()
        {
            return R.Rule(c => c
                .Id("p1c27r2a")
                .Group("")
                .From(1550).To(1600)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("ø"))
                    .After(a => a.Phon(IPA.IsConsonant)))
                .Rules(r => r
                    .Named("Ouverture de /ø/")
                    .Phono(_ => new[] { "œ" })));
        }

        public static Rule Rule2b()
        {
            return R.Rule(c => c
                .Id("p1c27r2b")
                .Group("")
                .From(1550).To(1600)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("e"))
                    .After(a => a.Phon(IPA.IsConsonant)))
                .Rules(r => r
                    .Named("Ouverture de /e/")
                    .Phono(_ => new[] { "ɛ" })));
        }
    }
}
