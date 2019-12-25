using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    public static class Part1Chapter14
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
                Rule1a(), Rule1b(), Rule1c(), Rule1d(), Rule1e(), Rule1f(), Rule1g(), Rule1h(),
                Rule2a(), Rule2b(), Rule2c(), Rule2d(), Rule2e(), Rule2f(), Rule2g(), Rule2h(), Rule2i(),
                Rule3a(), Rule3b(),
                //Rule4a(), Rule4b(), Rule4c(), Rule4d(), Rule4e(),
                //Rule5a(), Rule5b(),
                //Rule6a(),
                //Rule7a(), Rule7b(), Rule7c(), Rule7d(),
                //Rule8a(), Rule8b(), Rule8c(), Rule8d(),
            };
        }

        public static Rule Rule1a()
        {
            return R.Rule(c => c
                .Id("p1c14r1a")
                .From(1000).To(1100)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("a"))
                    .After(a => a.Phon(p => !IPA.IsVowel(p))))
                .Rules(r => r
                    .Named("Nasalisation de /a/ en syllabe fermée")
                    .Phono(px => new[] { "ã" })));
        }

        public static Rule Rule1b()
        {
            return R.Rule(c => c
                .Id("p1c14r1b")
                .From(1000).To(1050)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("e"))
                    .After(a => a.Phon(p => !IPA.IsVowel(p))))
                .Rules(r => r
                    .Named("Nasalisation de /e/ en syllabe fermée")
                    .Phono(px => new[] { "ẽ" })));
        }

        public static Rule Rule1c()
        {
            return R.Rule(c => c
                .Id("p1c14r1c")
                .From(1100).To(1200)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("o"))
                    .After(a => a.Phon(p => !IPA.IsVowel(p))))
                .Rules(r => r
                    .Named("Nasalisation de /o/ en syllabe fermée")
                    .Phono(px => new[] { "õ" })));
        }

        public static Rule Rule1d()
        {
            return R.Rule(c => c
                .Id("p1c14r1d")
                .From(1200).To(1300)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("i"))
                    .After(a => a.Phon(p => !IPA.IsVowel(p))))
                .Rules(r => r
                    .Named("Nasalisation de /i/ en syllabe fermée")
                    .Phono(px => new[] { "ĩ" })));
        }

        public static Rule Rule1e()
        {
            return R.Rule(c => c
                .Id("p1c14r1e")
                .From(1300).To(1400)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("y"))
                    .After(a => a.Phon(p => !IPA.IsVowel(p))))
                .Rules(r => r
                    .Named("Nasalisation de /y/ en syllabe fermée")
                    .Phono(px => new[] { "ỹ" })));
        }

        public static Rule Rule1f()
        {
            return R.Rule(c => c
                .Id("p1c14r1f")
                .From(1000).To(1100)
                .Query(q => q
                    .Match(m => m.Phon("a"))
                    .After(a => a.Seq(
                        s => s.Phon(p => !IPA.IsVowel(p)),
                        s => s.Phon(IPA.IsVowel))))
                .Rules(r => r
                    .Named("Nasalisation de /a/ en syllabe ouverte")
                    .Phono(px => new[] { "ã" })));
        }

        public static Rule Rule1g()
        {
            return R.Rule(c => c
                .Id("p1c14r1g")
                .From(1000).To(1050)
                .Query(q => q
                    .Match(m => m.Phon("e"))
                    .After(a => a.Seq(
                        s => s.Phon(p => !IPA.IsVowel(p)),
                        s => s.Phon(IPA.IsVowel))))
                .Rules(r => r
                    .Named("Nasalisation de /e/ en syllabe ouverte")
                    .Phono(px => new[] { "ẽ" })));
        }

        public static Rule Rule1h()
        {
            return R.Rule(c => c
                .Id("p1c14r1h")
                .From(1100).To(1200)
                .Query(q => q
                    .Match(m => m.Phon("o"))
                    .After(a => a.Seq(
                        s => s.Phon(p => !IPA.IsVowel(p)),
                        s => s.Phon(IPA.IsVowel))))
                .Rules(r => r
                    .Named("Nasalisation de /o/ en syllabe ouverte")
                    .Phono(px => new[] { "õ" })));
        }



        public static Rule Rule2a()
        {
            return R.Rule(c => c
                .Id("p1c14r2a")
                .From(1050).To(1075)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("ẽ"))
                    .After(a => a.Phon(p => !IPA.IsVowel(p))))
                .Rules(r => r
                    .Named("Ouverture de /ẽ/ en syllabe fermée")
                    .Phono(px => new[] { "ɛ̃" })));
        }

        public static Rule Rule2b()
        {
            return R.Rule(c => c
                .Id("p1c14r2b")
                .From(1075).To(1100)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("ɛ̃"))
                    .After(a => a.Phon(p => !IPA.IsVowel(p))))
                .Rules(r => r
                    .Named("Ouverture de /ɛ̃/ en syllabe fermée")
                    .Phono(px => new[] { "ã" })));
        }

        public static Rule Rule2c()
        {
            return R.Rule(c => c
                .Id("p1c14r2c")
                .From(1200).To(1300)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("õ"))
                    .After(a => a.Phon(p => !IPA.IsVowel(p))))
                .Rules(r => r
                    .Named("Ouverture de /õ/ en syllabe fermée")
                    .Phono(px => new[] { "ɔ̃" })));
        }

        public static Rule Rule2d()
        {
            return R.Rule(c => c
                .Id("p1c14r2d")
                .From(1300).To(1350)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("ĩ"))
                    .After(a => a.Phon(p => !IPA.IsVowel(p))))
                .Rules(r => r
                    .Named("Ouverture de /ĩ/ en syllabe fermée")
                    .Phono(px => new[] { "ẽ" })));
        }

        public static Rule Rule2e()
        {
            return R.Rule(c => c
                .Id("p1c14r2e")
                .From(1350).To(1400)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("ẽ"))
                    .After(a => a.Phon(p => !IPA.IsVowel(p))))
                .Rules(r => r
                    .Named("Ouverture de /ẽ/ en syllabe fermée")
                    .Phono(px => new[] { "ɛ̃" })));
        }

        public static Rule Rule2f()
        {
            return R.Rule(c => c
                .Id("p1c14r2f")
                .From(1400).To(1450)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("ỹ"))
                    .After(a => a.Phon(p => !IPA.IsVowel(p))))
                .Rules(r => r
                    .Named("Ouverture de /ỹ/ en syllabe fermée")
                    .Phono(px => new[] { "ø̃" })));
        }

        public static Rule Rule2g()
        {
            return R.Rule(c => c
                .Id("p1c14r2g")
                .From(1450).To(1500)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon("ø̃"))
                    .After(a => a.Phon(p => !IPA.IsVowel(p))))
                .Rules(r => r
                    .Named("Ouverture de /ø̃/ en syllabe fermée")
                    .Phono(px => new[] { "œ̃" })));
        }

        public static Rule Rule2h()
        {
            return R.Rule(c => c
                .Id("p1c14r2h")
                .From(1050).To(1075)
                .Query(q => q
                    .Match(m => m.Phon("ẽ"))
                    .After(a => a.Seq(
                        s => s.Phon(p => !IPA.IsVowel(p)),
                        s => s.Phon(IPA.IsVowel))))
                .Rules(r => r
                    .Named("Ouverture de /ẽ/ en syllabe ouverte")
                    .Phono(px => new[] { "ɛ̃" })));
        }

        public static Rule Rule2i()
        {
            return R.Rule(c => c
                .Id("p1c14r2i")
                .From(1200).To(1300)
                .Query(q => q
                    .Match(m => m.Phon("õ"))
                    .After(a => a.Seq(
                        s => s.Phon(p => !IPA.IsVowel(p)),
                        s => s.Phon(IPA.IsVowel))))
                .Rules(r => r
                    .Named("Ouverture de /õ/ en syllabe ouverte")
                    .Phono(px => new[] { "ɔ̃" })));
        }



        public static Rule Rule3a()
        {
            return R.Rule(c => c
                .Id("p1c14r3a")
                .From(1550).To(1650)
                .Query(q => q
                    .Match(m => m.Phon(IPA.IsNasalVowel))
                    .After(a => a.Seq(
                        s => s.Phon(p => !IPA.IsVowel(p)),
                        s => s.Phon(IPA.IsVowel))))
                .Rules(r => r
                    .Named("Dénasalisation de la voyelle en syllabe ouverte")
                    .Phono(px => new[] { px[0].Replace("\u0303", "") })));
        }

        public static Rule Rule3b()
        {
            return R.Rule(c => c
                .Id("p1c14r3b")
                .From(1550).To(1650)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Seq(
                        s => s.Phon(IPA.IsNasalVowel),
                        s => s.Phon(IPA.IsNasalConsonant)))
                    .After(a => a.Seq(
                        s => s.Maybe(m => m.Phon(p => !IPA.IsVowel(p))),
                        Q.End)))
                .Rules(r => r
                    .Named("Amuïssement de la consonne nasale en syllabe fermée")
                    .Phono(px => new[] { px[0] })));
        }
    }
}
