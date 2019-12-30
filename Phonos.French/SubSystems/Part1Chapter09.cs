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
    public static class Part1Chapter09
    {
        public static Rule[] Rules()
        {
            return new Rule[]
            {
                Rule1a(), Rule1b(), Rule1c(), Rule1d(),
                Rule2a(), Rule2b(), Rule2c(), Rule2d(), Rule2e(), Rule2f(), Rule2g(), Rule2h(),
                Rule3a(), Rule3b(), Rule3c(), Rule3d(), Rule3e(), Rule3f(),
                Rule4a(), Rule4b(), Rule4c(), Rule4d(), Rule4e(), Rule4f(), Rule4g(), Rule4h(), Rule4i(),
                Rule5a(), Rule5b(), Rule5c(), Rule5d(), Rule5e(), Rule5f(),
            };
        }

        public static Rule Rule1a()
        {
            return R.Rule(c => c
                .Id("p1c9r1a")
                .Group("Diphtongaison de /ɛ/ tonique libre ou monosyllabique")
                .From(100).To(200)
                .Query(
                    q => q  // tonique libre
                        .Scope("syllable")
                        .Match(m => m.Phon("ɛ").With("accent", "tonic"))
                        .After(Q.End),
                    q => q  // tonique monosyllabique
                        .Match(m => m.Phon("ɛ").With("accent", "tonic"))
                        .After(a => a.Seq(
                            s1 => s1.ZeroOrMany(z => z.Phon(IPA.IsConsonant)),
                            Q.End)))
                .Rules(r => r
                    .Named("Segmentation de /ɛ/ sous l'accent tonique")
                    .Phono(_ => new[] { "ɛɛ̯" })));
        }

        public static Rule Rule1b()
        {
            return R.Rule(c => c
                .Id("p1c9r1b")
                .Group("Diphtongaison de /ɛ/ tonique libre ou monosyllabique")
                .From(200).To(300)
                .Query(q => q.
                    Match(m => m.Phon("ɛɛ̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Dissimilation d'aperture de /ɛ/")
                    .Phono(_ => new[] { "iɛ̯" }).Rewrite(_ => "ie")));
        }

        public static Rule Rule1c()
        {
            return R.Rule(c => c
                .Id("p1c9r1c")
                .Group("Diphtongaison de /ɛ/ tonique libre ou monosyllabique")
                .From(600).To(700)
                .Query(q => q.
                    Match(m => m.Phon("iɛ̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Assimilation d'aperture progressive de /ɛ̯/")
                    .Phono(_ => new[] { "ie̯" })));
        }

        public static Rule Rule1d()
        {
            return R.Rule(c => c
                .Id("p1c9r1d")
                .Group("Diphtongaison de /ɛ/ tonique libre ou monosyllabique")
                .From(1100).To(1300)
                .Query(q => q.
                    Match(m => m.Phon("ie̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Consonantisation de /i/")
                    .Phono(_ => new[] { "j", "e" })));
        }

        public static Rule Rule2a()
        {
            return R.Rule(c => c
                .Id("p1c9r2a")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .From(200).To(300)
                .Query(
                    q => q  // tonique libre
                        .Scope("syllable")
                        .Match(m => m.Phon("ɔ").With("accent", "tonic"))
                        .After(Q.End),
                    q => q  // tonique monosyllabique
                        .Match(m => m.Phon("ɔ").With("accent", "tonic"))
                        .After(a => a.Seq(
                            s1 => s1.ZeroOrMany(z => z.Phon(IPA.IsConsonant)),
                            Q.End)))
                .Rules(p => p
                    .Named("Segmentation de /ɔ/ sous l'accent tonique")
                    .Phono(_ => new[] { "ɔɔ̯" })));
        }

        public static Rule Rule2b()
        {
            return R.Rule(c => c
                .Id("p1c9r2b")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .From(300).To(400)
                .Query(q => q.
                    Match(m => m.Phon("ɔɔ̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Dissimilation d'aperture de /ɔ/")
                    .Phono(_ => new[] { "uɔ̯" }).Rewrite(_ => "uo")));
        }

        public static Rule Rule2c()
        {
            return R.Rule(c => c
                .Id("p1c9r2c")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .From(600).To(700)
                .Query(q => q.
                    Match(m => m.Phon("uɔ̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Assimilation d'aperture de /ɔ̯/")
                    .Phono(_ => new[] { "uo̯" })));
        }

        public static Rule Rule2d()
        {
            return R.Rule(c => c
                .Id("p1c9r2d")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .From(1000).To(1100)
                .Query(q => q.
                    Match(m => m.Phon("uo̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Différenciation du point d'articulation de /o̯/")
                    .Phono(_ => new[] { "ue̯" }).Rewrite(_ => "ue")));
        }

        public static Rule Rule2e()
        {
            return R.Rule(c => c
                .Id("p1c9r2e")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .From(1100).To(1150)
                .Query(q => q.
                    Match(m => m.Phon("ue̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Antériorisation de /u/")
                    .Phono(_ => new[] { "ye̯" })));
        }

        public static Rule Rule2f()
        {
            return R.Rule(c => c
                .Id("p1c9r2f")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .From(1150).To(1200)
                .Query(q => q.
                    Match(m => m.Phon("ye̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Labialisation de /e̯/ ")
                    .Phono(_ => new[] { "yø̯" })));
        }

        public static Rule Rule2g()
        {
            return R.Rule(c => c
                .Id("p1c9r2g")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .From(1200).To(1250)
                .Query(q => q.
                    Match(m => m.Phon("yø̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Consonantisation de /y/ ")
                    .Phono(_ => new[] { "ɥ", "ø" })));
        }

        public static Rule Rule2h()
        {
            return R.Rule(c => c
                .Id("p1c9r2h")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .From(1250).To(1300)
                .Query(q => q.
                    Match(m => m.Seq(
                        s => s.Phon("ɥ"),
                        s2 => s2.Phon("ø").With("accent", "tonic"))))
                .Rules(p => p
                    .Named("Amuïssement de /ɥ/")
                    .Phono(_ => new[] { "ø" }).Rewrite(_ => "œu")));
        }

        // @fixme Ne pas match si suivi d'un nasale
        public static Rule Rule3a()
        {
            return R.Rule(c => c
                .Id("p1c9r3a")
                .Group("Diphtongaison de /a/ tonique libre ou monosyllabique non suivie d'une nasale")
                .From(400).To(500)
                .Query(
                    q => q  // tonique libre
                        .Scope("syllable")
                        .Match(m => m.Phon("a").With("accent", "tonic"))
                        .After(Q.End)
                        .Next(n => n.Seq(Q.Start, s => s.Phon(IPA.NON_NASAL_CONSONANTS))),
                    q => q  // tonique monosyllabique
                        .Scope("syllable")
                        .Match(m => m.Phon("a").With("accent", "tonic"))
                        .AfterNot(a => a.Phon(IPA.NASAL_CONSONANTS))
                        .Last())
                .Rules(p => p
                    .Named("Segmentation de /a/ sous l'accent tonique")
                    .Phono(_ => new[] { "aa̯" })));
        }

        public static Rule Rule3b()
        {
            return R.Rule(c => c
                .Id("p1c9r3b")
                .Group("Diphtongaison de /a/ tonique libre ou monosyllabique non suivie d'une nasale")
                .From(500).To(600)
                .Query(q => q.
                    Match(m => m.Phon("aa̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Dissimilation d'aperture de /a̯/")
                    .Phono(_ => new[] { "aɛ̯" })));
        }

        public static Rule Rule3c()
        {
            return R.Rule(c => c
                .Id("p1c9r3c")
                .Group("Diphtongaison de /a/ tonique libre ou monosyllabique non suivie d'une nasale")
                .From(600).To(650)
                .Query(q => q.
                    Match(m => m.Phon("aɛ̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Assimilation de /a/")
                    .Phono(_ => new[] { "ɛɛ̯" }).Rewrite(_ => "e")));
        }

        public static Rule Rule3d()
        {
            return R.Rule(c => c
                .Id("p1c9r3d")
                .Group("Diphtongaison de /a/ tonique libre ou monosyllabique non suivie d'une nasale")
                .From(650).To(700)
                .Query(q => q.
                    Match(m => m.Phon("ɛɛ̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Réduction de /ɛɛ̯/ et allongement du /ɛ/ résultant (conjecture)")
                    .Phono(_ => new[] { "ɛː" })));
        }

        public static Rule Rule3e()
        {
            return R.Rule(c => c
                .Id("p1c9r3e")
                .Group("Diphtongaison de /a/ tonique libre ou monosyllabique non suivie d'une nasale")
                .From(1000).To(1100)
                .Query(q => q.
                    Match(m => m.Phon("ɛː").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Fermeture de /ɛː/")
                    .Phono(_ => new[] { "eː" })));
        }

        public static Rule Rule3f()
        {
            return R.Rule(c => c
                .Id("p1c9r3f")
                .Group("Diphtongaison de /a/ tonique libre ou monosyllabique non suivie d'une nasale")
                .From(1000).To(1100)
                .Query(q => q.
                    Match(m => m.Phon("eː").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Abrègement de /eː/")
                    .Phono(_ => new[] { "e" })));
        }


        public static Rule Rule4a()
        {
            return R.Rule(c => c
                .Id("p1c9r4a")
                .Group("Diphtongaison de /e/ tonique libre ou monosyllabique")
                .From(500).To(550)
                .Query(
                    q => q  // tonique libre
                        .Scope("syllable")
                        .Match(m => m.Phon("e").With("accent", "tonic"))
                        .After(Q.End),
                    q => q  // tonique monosyllabique
                        .Match(m => m.Phon("e").With("accent", "tonic"))
                        .After(a => a.Seq(
                            s1 => s1.ZeroOrMany(z => z.Phon(IPA.IsConsonant)),
                            Q.End)))
                .Rules(p => p
                    .Named("Segmentation de /e/ sous l’accent tonique")
                    .Phono(_ => new[] { "ee̯" })));
        }

        public static Rule Rule4b()
        {
            return R.Rule(c => c
                .Id("p1c9r4b")
                .Group("Diphtongaison de /e/ tonique libre ou monosyllabique")
                .From(550).To(600)
                .Query(q => q.
                    Match(m => m.Phon("ee̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Dissimilation d’aperture de /e̯/")
                    .Phono(_ => new[] { "ei̯" })
                    .Rewrite(_ => "ei")));
        }

        public static Rule Rule4c()
        {
            return R.Rule(c => c
                .Id("p1c9r4c")
                .Group("Diphtongaison de /e/ tonique libre ou monosyllabique")
                .From(1100).To(1150)
                .Query(q => q.
                    Match(m => m.Phon("ei̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Dissimilation du point d’articulation de /e/")
                    .Phono(_ => new[] { "oi̯" })
                    .Rewrite(_ => "oi")));
        }

        public static Rule Rule4d()
        {
            return R.Rule(c => c
                .Id("p1c9r4d")
                .Group("Diphtongaison de /e/ tonique libre ou monosyllabique")
                .From(1150).To(1175)
                .Query(q => q.
                    Match(m => m.Phon("oi̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Assimilation de /i̯/")
                    .Phono(_ => new[] { "oe̯" })));
        }

        public static Rule Rule4e()
        {
            return R.Rule(c => c
                .Id("p1c9r4e")
                .Group("Diphtongaison de /e/ tonique libre ou monosyllabique")
                .From(1175).To(1200)
                .Query(q => q.
                    Match(m => m.Phon("oe̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Assimilation de /o/")
                    .Phono(_ => new[] { "ue̯" })));
        }

        public static Rule Rule4f()
        {
            return R.Rule(c => c
                .Id("p1c9r4f")
                .Group("Diphtongaison de /e/ tonique libre ou monosyllabique")
                .From(1200).To(1250)
                .Query(q => q.
                    Match(m => m.Phon("ue̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Consonantisation de /u/")
                    .Phono(_ => new[] { "w", "e" })));
        }

        public static Rule Rule4g()
        {
            return R.Rule(c => c
                .Id("p1c9r4g")
                .Group("Diphtongaison de /e/ tonique libre ou monosyllabique")
                .From(1250).To(1300)
                .Query(q => q.
                    Match(m => m
                        .Seq(s => s.Phon("w"), s => s.Phon("e"))
                        .With("accent", "tonic")))
                .Rules(p => p
                    .Named("Ouverture de /e/ sous l’influence de /w/ ")
                    .Phono(_ => new[] { "w", "ɛ" })));
        }

        public static Rule Rule4h()
        {
            return R.Rule(c => c
                .Id("p1c9r4h")
                .Group("Diphtongaison de /e/ tonique libre ou monosyllabique")
                .From(1300).To(1400)
                .Query(q => q.
                    Match(m => m
                        .Seq(s => s.Phon("w"), s => s.Phon("ɛ"))
                        .With("accent", "tonic")))
                .Rules(
                    r => r
                        .Named("Amuïssement de la consonne de la variante /wε/")
                        .Meta("Savant")
                        .Phono(_ => new[] { "ɛ" }),
                    r => r
                        .Named("Ouverture de /e/ sous l’influence de /w/ ")
                        .Meta("Populaire")
                        .Phono(_ => new[] { "w", "a" })));
        }

        public static Rule Rule4i()
        {
            return R.Rule(c => c
                .Id("p1c9r4i")
                .Group("Diphtongaison de /e/ tonique libre ou monosyllabique")
                .From(1700).To(1800)
                .Query(q => q.
                    Match(m => m.Phon("ɛ").With("accent", "tonic")))
                .Rules(p => p
                    .Named("(Orthographique) réécriture du son /ɛ/ écrit \"oi\" en \"ai\"")
                    .Phono(_ => new[] { "ɛ" })
                    .Rewrite(gx => gx == "oi" ? "ai" : gx)));
        }

        public static Rule Rule5a()
        {
            return R.Rule(c => c
                .Id("p1c9r5a")
                .Group("Diphtongaison de /o/ tonique libre ou monosyllabique")
                .From(400).To(500)
                .Query(
                    q => q  // tonique libre
                        .Scope("syllable")
                        .Match(m => m.Phon("o").With("accent", "tonic"))
                        .After(Q.End),
                    q => q  // tonique monosyllabique
                        .Match(m => m.Phon("o").With("accent", "tonic"))
                        .After(a => a.Seq(
                            s1 => s1.ZeroOrMany(z => z.Phon(IPA.IsConsonant)),
                            Q.End)))
                .Rules(p => p
                    .Named("Segmentation de /o/ sous l’accent tonique")
                    .Phono(_ => new[] { "oo̯" })));
        }

        public static Rule Rule5b()
        {
            return R.Rule(c => c
                .Id("p1c9r5b")
                .Group("Diphtongaison de /o/ tonique libre ou monosyllabique")
                .From(500).To(600)
                .Query(q => q.
                    Match(m => m.Phon("oo̯").With("accent", "tonic")))
                .Rules(p => p
                    .Named("Dissimilation d’aperture de /o̯/")
                    .Phono(_ => new[] { "ou̯" })
                    .Rewrite(_ => "ou")));
        }

        public static Rule Rule5c()
        {
            return R.Rule(c => c
                .Id("p1c9r5c")
                .Group("Diphtongaison de /o/ tonique libre ou monosyllabique")
                .From(1000).To(1100)
                .Query(q => q.
                    Match(m => m.Phon("ou̯").With("accent", "tonic")))
                .Rules(
                    r => r
                        .Named("Dissimilation du point d’articulation de /o/")
                        .Meta("Nord et Centre")
                        .Phono(_ => new[] { "eu̯" })
                        .Rewrite(_ => "eu"),
                    r => r
                        .Meta("Est et Ouest")));
        }

        public static Rule Rule5d()
        {
            return R.Rule(c => c
                .Id("p1c9r5d")
                .Group("Diphtongaison de /o/ tonique libre ou monosyllabique")
                .From(1100).To(1150)
                .Query(q => q.
                    Match(m => m.Phon("eu̯").With("accent", "tonic")))
                .Rules(r => r
                    .Named("Labialisation de /e/")
                    .Phono(_ => new[] { "øu̯" })));
        }

        public static Rule Rule5e()
        {
            return R.Rule(c => c
                .Id("p1c9r5e")
                .Group("Diphtongaison de /o/ tonique libre ou monosyllabique")
                .From(1150).To(1200)
                .Query(q => q.
                    Match(m => m.Phon("øu̯").With("accent", "tonic")))
                .Rules(r => r
                    .Named("Réduction de /øu̯/")
                    .Phono(_ => new[] { "ø" })));
        }

        public static Rule Rule5f()
        {
            return R.Rule(c => c
                .Id("p1c9r5f")
                .Group("Diphtongaison de /o/ tonique libre ou monosyllabique")
                .From(1100).To(1200)
                .Query(q => q.
                    Match(m => m.Phon("ou̯").With("accent", "tonic")))
                .Rules(r => r
                    .Named("Réduction de /ou̯/")
                    .Phono(_ => new[] { "u" })));
        }
    }
}
