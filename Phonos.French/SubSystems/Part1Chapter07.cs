﻿using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    public static class Part1Chapter07
    {
        public static IRule[] Rules()
        {
            return new[]
            {
                Rule1a(), RuleSet1(), Rule1d(),
            };
        }

        public static IRule[] RuleComponents()
        {
            return new[]
            {
                Rule1a(), Rule1b(), Rule1c(), Rule1d(),
                RuleSet1(),
            };
        }

        /// <summary>
        /// Effacement de /œ/ final
        /// </summary>
        public static IRule RuleSet1()
        {
            return new RuleSequence("p1c7s1", new []
            {
                Rule1b(),
                Rule1c(),
                Part1Chapter27.RuleSet1(),  // Lois de position
            });
        }

        public static Rule Rule1a()
        {
            return R.Rule(c => c
                .Id("p1c7r1a")
                .Group("")
                .From(1500).To(1600)
                .Query(q => q
                    .Match(m => m.Phon("ə"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Labialisation de /ə/")
                    .Phono(_ => new[] { "œ" })));
        }

        public static Rule Rule1b()
        {
            return R.Rule(c => c
                .Id("p1c7r1b")
                .Group("")
                .From(1600).To(1700)
                .Query(q => q
                    .Before(b => b.Phon(p => !IPA.IsVowel(p)))
                    .Match(m => m.Phon("œ"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Effacement de /œ/ après consonne")
                    .Phono(P.Erase)));
        }

        public static Rule Rule1c()
        {
            return R.Rule(c => c
                .Id("p1c7r1c")
                .Group("")
                .From(1600).To(1700)
                .Query(q => q
                    .Match(m => m
                        .Seq(s => s.Phon(IPA.IsVowel),
                             s => s.Phon("œ")))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Effacement de /œ/ et allongement de la voyelle précédente")
                    .Phono(px => new[] { px[0] + "ː" })
                    .Rewrite(g =>
                    {
                        if (g[0] == 'e')
                            return "ée";
                        else
                            return $"{g[0]}e";
                    })));
        }

        public static Rule Rule1d()
        {
            return R.Rule(c => c
                .Id("p1c7r1d")
                .Group("")
                .From(1900).To(1950)
                .Query(q => q
                    .Match(m => m.Phon(IPA.IsLongVowel)))
                .Rules(r => r
                    .Named("Abrègement des voyelles longues")
                    .Phono(px =>
                        new[] { px[0].Substring(0, px[0].Length - 1) })));
        }
    }
}
