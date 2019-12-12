using Phonos.Core;
using Phonos.Core.RuleBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    /// <summary>
    /// [G. Zink, Phonétique historique du français, ch. VI]
    /// </summary>
    public static class Part1Chapter9
    {
        public static Rule[] Rules()
        {
            return new Rule[]
            {
                Rule1a(), Rule1b(), Rule1c(), Rule1d(),
                Rule2a(), Rule2b(), Rule2c(), Rule2d(), Rule2e(), Rule2f(), Rule2g(), Rule2h(),
                Rule3a(), Rule3b(), Rule3c(), Rule3d(), Rule3e(), Rule3f(),
            };
        }

        public static Rule Rule1a()
        {
            return R.Rule(r => r
                .Id("p1c9r1a")
                .Group("Diphtongaison de /ɛ/ tonique libre ou monosyllabique")
                .Named("Segmentation de /ɛ/ sous l'accent tonique")
                .From(100).To(200)
                .Query(
                    q => q  // tonique libre
                        .Scope("syllable")
                        .Match(m => m.Phon("ɛ").With("accent", "tonic"))
                        .After(Q.End),
                    q => q  // tonique monosyllabique
                        .Match(m => m.Phon("ɛ").With("accent", "tonic"))
                        .After(a => a.Seq(
                            s1 => s1.ZeroOrMany(z => z.Phon(Q.Consonant)),
                            Q.End)))
                .Map(p => p.Phono(_ => new[] { "ɛɛ̯" })));
        }

        public static Rule Rule1b()
        {
            return R.Rule(r => r
                .Id("p1c9r1b")
                .Group("Diphtongaison de /ɛ/ tonique libre ou monosyllabique")
                .Named("Dissimilation d'aperture de /ɛ/")
                .From(200).To(300)
                .Query(q => q.
                    Match(m => m.Phon("ɛɛ̯").With("accent", "tonic")))
                .Map(p => p.Phono(_ => new[] { "iɛ̯" }).Rewrite(_ => "ie")));
        }

        public static Rule Rule1c()
        {
            return R.Rule(r => r
                .Id("p1c9r1c")
                .Group("Diphtongaison de /ɛ/ tonique libre ou monosyllabique")
                .Named("Assimilation d'aperture progressive de /ɛ̯/")
                .From(600).To(700)
                .Query(q => q.
                    Match(m => m.Phon("iɛ̯").With("accent", "tonic")))
                .Map(p => p.Phono(_ => new[] { "ie̯" })));
        }

        public static Rule Rule1d()
        {
            return R.Rule(r => r
                .Id("p1c9r1d")
                .Group("Diphtongaison de /ɛ/ tonique libre ou monosyllabique")
                .Named("Consonantisation de /i/")
                .From(1100).To(1300)
                .Query(q => q.
                    Match(m => m.Phon("ie̯").With("accent", "tonic")))
                .Map(p => p.Phono(_ => new[] { "j", "e" })));
        }

        public static Rule Rule2a()
        {
            return R.Rule(r => r
                .Id("p1c9r2a")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .Named("Segmentation de /ɔ/ sous l'accent tonique")
                .From(200).To(300)
                .Query(
                    q => q  // tonique libre
                        .Scope("syllable")
                        .Match(m => m.Phon("ɔ").With("accent", "tonic"))
                        .After(Q.End),
                    q => q  // tonique monosyllabique
                        .Match(m => m.Phon("ɔ").With("accent", "tonic"))
                        .After(a => a.Seq(
                            s1 => s1.ZeroOrMany(z => z.Phon(Q.Consonant)),
                            Q.End)))
                .Map(p => p.Phono(_ => new[] { "ɔɔ̯" })));
        }

        public static Rule Rule2b()
        {
            return R.Rule(r => r
                .Id("p1c9r2b")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .Named("Dissimilation d'aperture de /ɔ/")
                .From(300).To(400)
                .Query(q => q.
                    Match(m => m.Phon("ɔɔ̯").With("accent", "tonic")))
                .Map(p => p.Phono(_ => new[] { "uɔ̯" }).Rewrite(_ => "uo")));
        }

        public static Rule Rule2c()
        {
            return R.Rule(r => r
                .Id("p1c9r2c")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .Named("Assimilation d'aperture de /ɔ̯/")
                .From(600).To(700)
                .Query(q => q.
                    Match(m => m.Phon("uɔ̯").With("accent", "tonic")))
                .Map(p => p.Phono(_ => new[] { "uo̯" })));
        }

        public static Rule Rule2d()
        {
            return R.Rule(r => r
                .Id("p1c9r2d")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .Named("Différenciation du point d'articulation de /o̯/")
                .From(1000).To(1100)
                .Query(q => q.
                    Match(m => m.Phon("uo̯").With("accent", "tonic")))
                .Map(p => p.Phono(_ => new[] { "ue̯" }).Rewrite(_ => "ue")));
        }

        public static Rule Rule2e()
        {
            return R.Rule(r => r
                .Id("p1c9r2e")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .Named("Antériorisation de /u/")
                .From(1100).To(1150)
                .Query(q => q.
                    Match(m => m.Phon("ue̯").With("accent", "tonic")))
                .Map(p => p.Phono(_ => new[] { "ye̯" })));
        }

        public static Rule Rule2f()
        {
            return R.Rule(r => r
                .Id("p1c9r2f")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .Named("Labialisation de /e̯/ ")
                .From(1150).To(1200)
                .Query(q => q.
                    Match(m => m.Phon("ye̯").With("accent", "tonic")))
                .Map(p => p.Phono(_ => new[] { "yø̯" })));
        }

        public static Rule Rule2g()
        {
            return R.Rule(r => r
                .Id("p1c9r2g")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .Named("Consonantisation de /y/ ")
                .From(1200).To(1250)
                .Query(q => q.
                    Match(m => m.Phon("yø̯").With("accent", "tonic")))
                .Map(p => p.Phono(_ => new[] { "ɥ", "ø" })));
        }

        public static Rule Rule2h()
        {
            return R.Rule(r => r
                .Id("p1c9r2h")
                .Group("Diphtongaison de /ɔ/ (issu do /ǒ/) tonique libre ou monosyllabique")
                .Named("Amuïssement de /ɥ/")
                .From(1250).To(1300)
                .Query(q => q.
                    Match(m => m.Seq(
                        s => s.Phon("ɥ"),
                        s2 => s2.Phon("ø").With("accent", "tonic"))))
                .Map(p => p.Phono(_ => new[] { "ø" }).Rewrite(_ => "œu")));
        }

        public static Rule Rule3a()
        {
            return R.Rule(r => r
                .Id("p1c9r3a")
                .Group("Diphtongaison de /a/ tonique libre ou monosyllabique non suivie d'une nasale")
                .Named("Segmentation de /a/ sous l'accent tonique")
                .From(400).To(500)
                .Query(
                    q => q  // tonique libre
                        .Scope("syllable")
                        .Match(m => m.Phon("a").With("accent", "tonic"))
                        .After(Q.End),
                    q => q  // tonique monosyllabique
                        .Match(m => m.Phon("a").With("accent", "tonic"))
                        .After(a => a.Seq(
                            s1 => s1.ZeroOrMany(z => z.Phon(Q.Consonant)),
                            Q.End)))
                .Map(p => p.Phono(_ => new[] { "aa̯" })));
        }

        public static Rule Rule3b()
        {
            return R.Rule(r => r
                .Id("p1c9r3b")
                .Group("Diphtongaison de /a/ tonique libre ou monosyllabique non suivie d'une nasale")
                .Named("Dissimilation d'aperture de /a̯/")
                .From(500).To(600)
                .Query(q => q.
                    Match(m => m.Phon("aa̯").With("accent", "tonic")))
                .Map(p => p.Phono(_ => new[] { "aɛ̯" })));
        }

        public static Rule Rule3c()
        {
            return R.Rule(r => r
                .Id("p1c9r3c")
                .Group("Diphtongaison de /a/ tonique libre ou monosyllabique non suivie d'une nasale")
                .Named("Assimilation de /a/")
                .From(600).To(650)
                .Query(q => q.
                    Match(m => m.Phon("aɛ̯").With("accent", "tonic")))
                .Map(p => p.Phono(_ => new[] { "ɛɛ̯" }).Rewrite(_ => "e")));
        }

        public static Rule Rule3d()
        {
            return R.Rule(r => r
                .Id("p1c9r3d")
                .Group("Diphtongaison de /a/ tonique libre ou monosyllabique non suivie d'une nasale")
                .Named("Réduction de /ɛɛ̯/ et allongement du /ɛ/ résultant (conjecture)")
                .From(650).To(700)
                .Query(q => q.
                    Match(m => m.Phon("ɛɛ̯").With("accent", "tonic")))
                .Map(p => p.Phono(_ => new[] { "ɛː" })));
        }

        public static Rule Rule3e()
        {
            return R.Rule(r => r
                .Id("p1c9r3e")
                .Group("Diphtongaison de /a/ tonique libre ou monosyllabique non suivie d'une nasale")
                .Named("Fermeture de /ɛː/")
                .From(1000).To(1100)
                .Query(q => q.
                    Match(m => m.Phon("ɛː").With("accent", "tonic")))
                .Map(p => p.Phono(_ => new[] { "eː" })));
        }

        public static Rule Rule3f()
        {
            return R.Rule(r => r
                .Id("p1c9r3f")
                .Group("Diphtongaison de /a/ tonique libre ou monosyllabique non suivie d'une nasale")
                .Named("Abrègement de /eː/")
                .From(1000).To(1100)
                .Query(q => q.
                    Match(m => m.Phon("eː").With("accent", "tonic")))
                .Map(p => p.Phono(_ => new[] { "e" })));
        }
    }
}
