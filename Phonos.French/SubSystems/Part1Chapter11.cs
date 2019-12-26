using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    public static class Part1Chapter11
    {
        public static IRule[] Rules()
        {
            return RuleComponents();
        }

        public static IRule[] RuleComponents()
        {
            return new[]
            {
                Rule1(),
            };
        }

        public static Rule Rule1()
        {
            return R.Rule(c => c
                .Id("p1c11r1")
                .From(100).To(200)
                .Query(q => q
                    .Before(Q.Start)
                    .Match(m => m.Nothing())
                    .After(a => a.Seq(s => s.Phon("s"), s => s.Phon("k", "p", "t"))))
                .Rules(r => r
                    .Named("Production d'un /i/ prosthétique")
                    .Phono(px => new[] { "i" })
                    .Rewrite(_ => "i")));
        }



        public static Rule Rule2a()
        {
            return R.Rule(c => c
                .Id("p1c18r2a")
                .From(250).To(300)
                .Query(q => q
                    .Match(m => m.Phon("l"))
                    .After(a => a.Phon(p => IPA.IsConsonant(p) && p != "l")))
                .Rules(r => r
                    .Named("Vélarisation de /l/ en coda")
                    .Phono(px => new[] { "ɫ" })));
        }

        public static Rule Rule2b()
        {
            return R.Rule(c => c
                .Id("p1c18r2b")
                .From(900).To(1000)
                .Query(q => q
                    .Match(m => m.Seq(
                        s => s.Phon(IPA.IsVowel),
                        s => s.Phon("ɫ"))))
                .Rules(r => r
                    .Named("Vocalisation de /ɫ/")
                    .Phono(px => new[] { px[0] + "u̯" })
                    .Rewrite(g => g.Substring(0, g.Length - 1) + "u")));
        }

        public static Rule Rule2c()
        {
            return R.Rule(c => c
                .Id("p1c18r2c")
                .From(700).To(750)
                .Query(q => q
                    .Before(b => b.Phon("ʎ"))
                    .Match(m => m.Phon("s")))
                .Rules(r => r
                    .Named("Production d'un /t/ épenthétique")
                    .Phono(px => new[] { "ʦ" })));
        }

        public static Rule Rule2d()
        {
            return R.Rule(c => c
                .Id("p1c18r2d")
                .From(750).To(800)
                .Query(q => q
                    .Match(b => b.Phon("ʎ"))
                    .After(a => a.Phon("ʦ")))
                .Rules(r => r
                    .Named("Dépalatalisation de /ʎ/ devant /s/")
                    .Phono(px => new[] { "l" })));
        }

        public static Rule Rule2e()
        {
            return R.Rule(c => c
                .Id("p1c18r2e")
                .From(800).To(900)
                .Query(q => q
                    .Match(b => b.Phon("l"))
                    .After(a => a.Phon("ʦ")))
                .Rules(r => r
                    .Named("Vélarisationt de /l/ devant /ʦ/")
                    .Phono(px => new[] { "ɫ" })));
        }
    }
}
