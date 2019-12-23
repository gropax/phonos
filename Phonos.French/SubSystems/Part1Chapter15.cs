using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    public static class Part1Chapter15
    {
        public static IRule[] Rules()
        {
            return new IRule[]
            {
            };
        }

        public static IRule[] RuleComponents()
        {
            return new[]
            {
                Rule1a(), Rule1b(), Rule1c(), Rule1d(), Rule1e(), Rule1f(),
                //Rule2a(), Rule2b(), Rule2c(), Rule2d(),
                //Rule3a(), Rule3b(), Rule3c(), Rule3d(), Rule3e(), Rule3f(), Rule3g(), Rule3h(), Rule3i(), Rule3j(), Rule3k(),
                //Rule4a(), Rule4b(), Rule4c(), Rule4d(),
                //Rule5a(), Rule5b(), Rule5c(), Rule5d(), Rule5e(),
                //Rule6a(), Rule6b(), Rule6c(), Rule6d(), Rule6e(), Rule6f(), Rule6g(), Rule6h(), Rule6i(),
            };
        }

        public static Rule Rule1a()
        {
            return R.Rule(c => c
                .Id("p1c15r1a")
                .From(600).To(700)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Seq(s => s.Phon("j"), s => s.Phon("j")))
                    .After(a => a.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Dégémination de /jj/ intervocalique")
                    .Phono(px => new[] { px[0] })));
        }

        public static Rule Rule1b()
        {
            return R.Rule(c => c
                .Id("p1c15r1b")
                .From(800).To(900)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m =>
                        m.Seq(
                            s => s.Phon(IPA.IsVowel),
                            s => s.Phon("j"))))
                .Rules(r => r
                    .Named("Vocalisation de /j/ et formation d'une diphtongue de coalescence")
                    .Phono(px => new[] { px[0] + "i̯" })));
        }

        public static Rule Rule1c()
        {
            return R.Rule(c => c
                .Id("p1c15r1c")
                .From(200).To(250)
                .Query(q => q
                    .Before(Q.Start)
                    .Match(m => m.Phon("j")))
                .Rules(r => r
                    .Named("Palatalisation de /j/ initial")
                    .Phono(px => new[] { "dʲ" })));
        }

        public static Rule Rule1d()
        {
            return R.Rule(c => c
                .Id("p1c15r1d")
                .From(250).To(300)
                .Query(q => q .Match(m => m.Phon("dʲ")))
                .Rules(r => r
                    .Named("Assibilation de /dʲ/")
                    .Phono(px => new[] { "ʤʲ" })));
        }

        public static Rule Rule1e()
        {
            return R.Rule(c => c
                .Id("p1c15r1e")
                .From(600).To(700)
                .Query(q => q.Match(m => m.Phon("ʤʲ")))
                .Rules(r => r
                    .Named("Dépalatalisation de /ʤʲ/")
                    .Phono(px => new[] { "ʤ" })));
        }

        public static Rule Rule1f()
        {
            return R.Rule(c => c
                .Id("p1c15r1f")
                .From(1200).To(1300)
                .Query(q => q.Match(m => m.Phon("ʤ")))
                .Rules(r => r
                    .Named("Réduction de /ʤ/")
                    .Phono(px => new[] { "ʒ" })
                    .Rewrite(g =>
                    {
                        if (g.StartsWith("g"))
                            return "ge";
                        else
                            return "j";
                    })));
        }
    }
}
