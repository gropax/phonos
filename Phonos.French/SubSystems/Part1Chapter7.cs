using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    public static class Part1Chapter7
    {
        public static IRule[] Rules()
        {
            return new[]
            {
                Rule1a(), Rule1b(), Rule1c(), Rule1d(),
            };
        }

        public static IRule[] RuleComponents()
        {
            return new[]
            {
                Rule1a(), Rule1b(), Rule1c(), Rule1d(),
            };
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
                    .Phono(px => new[] { px[0] + "ː" })));
        }

        public static Rule Rule1d()
        {
            return R.Rule(c => c
                .Id("p1c7r1d")
                .Group("")
                .From(1900).To(1910)
                .Query(q => q
                    .Match(m => m.Phon(IPA.IsLongVowel))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Abrègement des voyelles longues finales")
                    .Phono(px =>
                        new[] { px[0].Substring(0, px[0].Length - 1) })));
        }
    }
}
