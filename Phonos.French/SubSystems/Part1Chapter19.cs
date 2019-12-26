using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    public static class Part1Chapter19
    {
        public static IRule[] Rules()
        {
            return RuleComponents();
            //return new IRule[] { };
        }

        public static IRule[] RuleComponents()
        {
            return new[]
            {
                Rule1a(), Rule1b(), Rule1c(), Rule1d(), Rule1e(),
                Rule2(),
                Rule3a(), Rule3b(), Rule3c(),
                Rule4a(), Rule4b(), Rule4c(), Rule4d(),
                Rule5a(), Rule5b(),
                Rule6a(), Rule6b(),
                Rule7a(), Rule7b(),
                Rule8(),
            };
        }

        public static Rule Rule1a()
        {
            return R.Rule(c => c
                .Id("p1c19r1a")
                .From(900).To(1000)
                .Query(q => q
                    .Match(m => m.Seq(s => s.Phon("a"), s => s.Phon("j"))))
                .Rules(r => r
                    .Named("Vocalisation de /j/ et diphtongaison par soudure ")
                    .Phono(px => new[] { "ai̯" })));
        }

        public static Rule Rule1b()
        {
            return R.Rule(c => c
                .Id("p1c19r1b")
                .From(1100).To(1150)
                .Query(q => q.Match(m => m.Phon("ai̯")))
                .Rules(r => r
                    .Named("Fermeture de /a/")
                    .Phono(px => new[] { "ɛi̯" })));
        }

        public static Rule Rule1c()
        {
            return R.Rule(c => c
                .Id("p1c19r1c")
                .From(1150).To(1200)
                .Query(q => q.Match(m => m.Phon("ɛi̯")))
                .Rules(r => r
                    .Named("Monophtongaison de /ɛi̯/")
                    .Phono(px => new[] { "ɛ" })));
        }

        public static Rule Rule1d()
        {
            return R.Rule(c => c
                .Id("p1c19r1d")
                .From(1200).To(1300)
                .Query(q => q
                    .Match(m => m.Phon("ɛ"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Fermeture de /ɛ/ en finale absolue")
                    .Phono(px => new[] { "e" })));
        }

        public static Rule Rule1e()
        {
            return R.Rule(c => c
                .Id("p1c19r1e")
                .From(1900).To(2000)
                .Query(q => q
                    .Match(m => m.Phon("e"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Ouverture de /e/ en finale absolue")
                    .Phono(px => new[] { "ɛ" })));
        }



        public static Rule Rule2()
        {
            return R.Rule(c => c
                .Id("p1c19r2")
                .From(900).To(1000)
                .Query(q => q
                    .Match(m => m.Seq(s => s.Phon("e"), s => s.Phon("j"))))
                .Rules(r => r
                    .Named("Vocalisation de /j/ et diphtongaison par soudure ")
                    .Phono(px => new[] { "ei̯" })));
        }



        public static Rule Rule3a()
        {
            return R.Rule(c => c
                .Id("p1c19r3a")
                .From(600).To(700)
                .Query(q => q
                    .Match(m => m.Seq(s => s.Phon("u"), s => s.Phon("j"))))
                .Rules(r => r
                    .Named("Vocalisation de /j/ et diphtongaison par soudure ")
                    .Phono(px => new[] { "ui̯" })));
        }

        public static Rule Rule3b()
        {
            return R.Rule(c => c
                .Id("p1c19r3b")
                .From(700).To(800)
                .Query(q => q.Match(m => m.Phon("ui̯")))
                .Rules(r => r
                    .Named("Palatalisation de /u/")
                    .Phono(px => new[] { "yi̯" })));
        }

        public static Rule Rule3c()
        {
            return R.Rule(c => c
                .Id("p1c19r3c")
                .From(1100).To(1200)
                .Query(q => q.Match(m => m.Phon("yi̯")))
                .Rules(r => r
                    .Named("Consonantisation de /y/")
                    .Phono(px => new[] { "ɥ", "i" })
                    .Rewrite(g => "u i")));
        }



        public static Rule Rule4a()
        {
            return R.Rule(c => c
                .Id("p1c19r4a")
                .From(1300).To(1500)
                .Query(q => q.Match(m => m.Phon("au̯")))
                .Rules(r => r
                    .Named("Assimilation de /u/")
                    .Phono(px => new[] { "ao̯" })));
        }

        public static Rule Rule4b()
        {
            return R.Rule(c => c
                .Id("p1c19r4b")
                .From(1500).To(1525)
                .Query(q => q.Match(m => m.Phon("ao̯")))
                .Rules(r => r
                    .Named("Assimilation de /a/")
                    .Phono(px => new[] { "ɑo̯" })));
        }

        public static Rule Rule4c()
        {
            return R.Rule(c => c
                .Id("p1c19r4c")
                .From(1525).To(1550)
                .Query(q => q.Match(m => m.Phon("ɑo̯")))
                .Rules(r => r
                    .Named("Assimilation de /ɑ/")
                    .Phono(px => new[] { "oo̯" })));
        }

        public static Rule Rule4d()
        {
            return R.Rule(c => c
                .Id("p1c19r4d")
                .From(1550).To(1600)
                .Query(q => q.Match(m => m.Phon("oo̯")))
                .Rules(r => r
                    .Named("Monophtongaison de /oo̯/")
                    .Phono(px => new[] { "o" })));
        }

        public static Rule Rule5a()
        {
            return R.Rule(c => c
                .Id("p1c19r5a")
                .From(1100).To(1150)
                .Query(q => q.Match(m => m.Phon("eu̯")))
                .Rules(r => r
                    .Named("Labialisation de /e/")
                    .Phono(px => new[] { "øu̯" })));
        }

        public static Rule Rule5b()
        {
            return R.Rule(c => c
                .Id("p1c19r5b")
                .From(1150).To(1200)
                .Query(q => q.Match(m => m.Phon("øu̯")))
                .Rules(r => r
                    .Named("Monophtongaison de /øu̯/")
                    .Phono(px => new[] { "ø" })));
        }



        public static Rule Rule6a()
        {
            return R.Rule(c => c
                .Id("p1c19r6a")
                .From(1000).To(1050)
                .Query(q => q.Match(m => m.Phon("iu̯")))
                .Rules(r => r
                    .Named("Labialisation de /u/")
                    .Phono(px => new[] { "iy̯" })));
        }

        public static Rule Rule6b()
        {
            return R.Rule(c => c
                .Id("p1c19r6b")
                .From(1050).To(1100)
                .Query(q => q.Match(m => m.Phon("iy̯")))
                .Rules(r => r
                    .Named("Assimilation de /iy̯/")
                    .Phono(px => new[] { "i" })
                    .Rewrite(_ => "i")));
        }



        public static Rule Rule7a()
        {
            return R.Rule(c => c
                .Id("p1c19r7a")
                .From(1100).To(1150)
                .Query(q => q.Match(m => m.Phon("ɔu̯")))
                .Rules(r => r
                    .Named("Fermeture de /ɔ/")
                    .Phono(px => new[] { "ou̯" })));
        }

        public static Rule Rule7b()
        {
            return R.Rule(c => c
                .Id("p1c19r7b")
                .From(1150).To(1200)
                .Query(q => q.Match(m => m.Phon("ou̯")))
                .Rules(r => r
                    .Named("Assimilation de /o/")
                    .Phono(px => new[] { "u" })));
        }



        public static Rule Rule8()
        {
            return R.Rule(c => c
                .Id("p1c19r8")
                .From(1000).To(1100)
                .Query(q => q.Match(m => m.Phon("yu̯")))
                .Rules(r => r
                    .Named("Assimilation de /u/")
                    .Phono(px => new[] { "y" })));
        }

    }
}
