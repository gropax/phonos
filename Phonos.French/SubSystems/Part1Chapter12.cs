using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.French.SubSystems
{
    public static class Part1Chapter12
    {
        public static IRule[] Rules()
        {
            return RuleComponents();
        }

        public static IRule[] RuleComponents()
        {
            return new[]
            {
                Rule1a(), /*Rule1b(),*/
                Rule2a(), Rule2b(),
                Rule3a(), Rule3b(), Rule3c(), Rule3d(), Rule3e(), Rule3f(), Rule3g(), Rule3h(),
                Rule4a(), Rule4b(),
                Rule5(),
            };
        }

        public static Rule Rule1a()
        {
            return R.Rule(c => c
                .Id("p1c12r1a")
                .From(100).To(200)
                .Query(q => q
                    .Match(m => m.Twice(t => t.Phon(IPA.IsVowel))))
                .Rules(r => r
                    .Named("Contraction de voyelles en hiatus")
                    .Phono(px => new[] { $"{px[1][0]}ː" })
                    .Rewrite(g => g.Last().ToString())));
        }

        //public static Rule Rule1b()
        //{
        //    return R.Rule(c => c
        //        .Id("p1c12r1b")
        //        .From(200).To(300)
        //        .Query(q => q
        //            .Match(m => m.Twice(t => t.Phon(IPA.IsVowel)))
        //            )
        //        .Rules(r => r
        //            .Named("Absorption des voyelles de part et d'autre de /r/")
        //            .Phono(px => new[] { $"{px[1][0]}ː" })
        //            .Rewrite(g => g.Last().ToString())));
        //}

        public static Rule Rule2a()
        {
            return R.Rule(c => c
                .Id("p1c12r2a")
                .From(700).To(800)
                .Query(q => q
                    .Match(m => m.Phon("a").With("accent", "initial"))
                    .After(a => a.Phon(IPA.IsVowel).With("accent", "tonic")))
                .Rules(r => r
                    .Named("Centralisation de /a/ en hiatus avec une voyelle accentuée")
                    .Phono(px => new[] { "ə" })
                    .Rewrite(_ => "e")));
        }

        public static Rule Rule2b()
        {
            return R.Rule(c => c
                .Id("p1c12r2b")
                .From(400).To(500)
                .Query(q => q
                    .Scope("syllabe")
                    .Before(b => b.Phon("kʲ", "gʲ"))
                    .Match(m => m.Phon("a").With("accent", "initial"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Fermeture de /a/ initial libre après une vélaire palatalisée")
                    .Phono(px => new[] { "e" })
                    .Rewrite(_ => "e")));
        }



        public static Rule Rule3a()
        {
            return R.Rule(c => c
                .Id("p1c12r3a")
                .From(300).To(400)
                .Query(q => q
                    .Match(m => m.Phon("ɛ").With("accent", "initial")))
                .Rules(r => r
                    .Named("Fermeture de /e/ initial")
                    .Phono(px => new[] { "e" })));
        }

        public static Rule Rule3b()
        {
            return R.Rule(c => c
                .Id("p1c12r3b")
                .From(300).To(400)
                .Query(q => q
                    .Match(m => m.Phon("ɔ").With("accent", "initial")))
                .Rules(r => r
                    .Named("Fermeture de /o/ initial")
                    .Phono(px => new[] { "o" })));
        }

        public static Rule Rule3c()
        {
            return R.Rule(c => c
                .Id("p1c12r3c")
                .From(700).To(800)
                .Query(q => q
                    .Match(m => m.Phon("e").With("accent", "initial"))
                    .After(a => a.Phon(IPA.IsVowel).With("accent", "tonic")))
                .Rules(r => r
                    .Named("Centralisation de /e/ en hiatus avec une voyelle accentuée")
                    .Phono(px => new[] { "ə" })));
        }

        public static Rule Rule3d()
        {
            return R.Rule(c => c
                .Id("p1c12r3d")
                .From(1000).To(1100)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("e").With("accent", "initial"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Centralisation de /e/ initial libre")
                    .Phono(px => new[] { "ə" })));
        }

        public static Rule Rule3e()
        {
            return R.Rule(c => c
                .Id("p1c12r3e")
                .From(1100).To(1200)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("e").With("accent", "initial"))
                    .After(a => a.Phon(p => !IPA.IsVowel(p))))
                .Rules(r => r
                    .Named("Ouverture de /e/ initial entravé")
                    .Phono(px => new[] { "ɛ" })));
        }

        public static Rule Rule3f()
        {
            return R.Rule(c => c
                .Id("p1c12r3f")
                .From(1150).To(1200)
                .Query(q => q
                    .Match(m => m.Phon("o")
                        .With("accent", "initial")
                        .Without("classical_latin", "au")))
                .Rules(r => r
                    .Named("Fermeture de /o/ initial (étymologique)")
                    .Phono(px => new[] { "u" })
                    .Rewrite(_ => "ou")));
        }

        public static Rule Rule3g()
        {
            return R.Rule(c => c
                .Id("p1c12r3g")
                .From(500).To(600)
                .Query(q => q
                    .Match(m => m.Phon("ɔ")
                        .With("accent", "initial")
                        .With("classical_latin", "au")))
                .Rules(r => r
                    .Named("Fermeture de /ɔ/ initial (issu de /au̯/)")
                    .Phono(px => new[] { "o" })));
        }

        public static Rule Rule3h()
        {
            return R.Rule(c => c
                .Id("p1c12r3h")
                .From(1150).To(1200)
                .Query(q => q
                    .Match(m => m.Phon("o").With("accent", "initial"))
                    .After(a => a.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Fermeture de /o/ initial (issu de /au̯/) devant une voyelle tonique")
                    .Phono(px => new[] { "u" })
                    .Rewrite(_ => "ou")));
        }



        public static Rule Rule4a()
        {
            return R.Rule(c => c
                .Id("p1c12r4a")
                .From(1600).To(1700)
                .Optional()
                .Query(q => q
                    .Match(m => m.Phon("ə")
                        .With("accent", "initial")
                        .With("classical_latin", "e")))
                .Rules(r => r
                    .Named("Fermeture de /ə/ initial (réforme Érasmienne)")
                    .Phono(px => new[] { "e" })
                    .Rewrite(_ => "é")));
        }

        public static Rule Rule4b()
        {
            return R.Rule(c => c
                .Id("p1c12r4b")
                .From(1600).To(1700)
                .Optional()
                .Query(q => q
                    .Match(m => m.Phon("u")
                        .With("accent", "initial")
                        .With("classical_latin", "o")))
                .Rules(r => r
                    .Named("Ouverture de /u/ initial (réforme Érasmienne)")
                    .Phono(px => new[] { "o" })
                    .Rewrite(_ => "o")));
        }



        public static Rule Rule5()
        {
            return R.Rule(c => c
                .Id("p1c12r5")
                .From(0).To(100)
                .Optional()
                .Query(q => q
                    .Match(m => m.Phon("iː", "uː").With("accent", "initial")))
                .Rules(r => r
                    .Named("Abrègement en position initiale")
                    .Phono(px => new[] { px[0][0].ToString() })));
        }
    }
}
