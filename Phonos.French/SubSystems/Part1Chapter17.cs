using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    public static class Part1Chapter17
    {
        public static IRule[] Rules()
        {
            return new IRule[]
            {
                Rule2(),
                Rule3a(), Rule3b(), Rule3c(), Rule3d(), Rule3e(),
                Rule4a(), Rule4b(), Rule4c(), Rule4d(), Rule4e(),
                Rule5a(), Rule5b(), Rule5c(), 
                Rule6a(), Rule6b(), Rule6c(), Rule6d(),
                Rule6k(),  // Should be placed before 6e
                Rule6e(), Rule6f(), Rule6g(), Rule6h(), Rule6i(), Rule6j(),
                Rule7a(), Rule7b(), Rule7c(), Rule7d(), 
            };
        }

        public static IRule[] RuleComponents()
        {
            return new IRule[]
            {
                Rule1(),
                Rule2(),
                Rule3a(), Rule3b(), Rule3c(), Rule3d(), Rule3e(),
                Rule4a(), Rule4b(), Rule4c(), Rule4d(), Rule4e(),
                Rule5a(), Rule5b(), Rule5c(), 
                Rule6a(), Rule6b(), Rule6c(), Rule6d(),
                Rule6k(),  // Should be placed before 6e
                Rule6e(), Rule6f(), Rule6g(), Rule6h(), Rule6i(), Rule6j(),
                Rule7a(), Rule7b(), Rule7c(), Rule7d(), 
            };
        }

        /// <summary>
        /// Réduction des groupes intérieurs de 3 consonnes
        /// </summary>
        public static Rule Rule1()
        {
            return R.Rule(c => c
                .Id("p1c17r1")
                .Query(
                    q => q.Match(m => m.Seq(
                        s => s.Phon(p => IPA.IsConsonant(p) && !IPA.IsLiquide(p)
                            && !IPA.IsFricative(p) && !IPA.IsGlide(p)),
                        s => s.Phon(IPA.IsConsonant),
                        s => s.Phon(IPA.IsConsonant))),
                    q => q.Match(m => m.Seq(
                        s => s.Phon(IPA.IsConsonant),
                        s => s.Phon(IPA.IsConsonant),
                        s => s.Phon(p => IPA.IsConsonant(p) && !IPA.IsLiquide(p)
                            && !IPA.IsFricative(p) && !IPA.IsGlide(p)))))
                .Rules(r => r
                    .Named("Réduction des groupes intérieurs de 3 consonnes")
                    .Phono(px => new[] { px[0], px[2] })
                    .Rewrite(_ => _)
                    .Rewrite(g => $"{g[0]} {g[2]}")));
        }


        public static Rule Rule2()
        {
            return R.Rule(c => c
                .Id("p1c17r2")
                .From(100).To(200)
                .Query(q => q
                    .Match(m => m.Phon("n").Without("morpheme", "in", "con", "com", "cun", "cum"))
                    .After(a => a.Phon("s", "f")))
                .Rules(r => r
                    .Named("Amuïssement de /n/ devant /s/ et /f/, hors préfixes in- et con-")
                    .Phono(P.Erase)
                    .Rewrite(G.Erase)));
        }


        public static Rule Rule3a()
        {
            return R.Rule(c => c
                .Id("p1c17r3a")
                .From(1050).To(1100)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Seq(
                        s => s.Phon("ɔ"),
                        s => s.Phon("z")))
                    .After(a => a
                        .Seq(s => s.Maybe(m => m.Phon(IPA.IsConsonant)),
                             Q.End)))
                .Rules(r => r
                    .Named("Amuïssement de /z/ antéconsonantique, allongement et ouverture de /ɔ/")
                    .Phono(_ => new[] { "oː" })
                    .Rewrite(_ => "ô")));
        }

        public static Rule Rule3b()
        {
            return R.Rule(c => c
                .Id("p1c17r3b")
                .From(1050).To(1100)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Seq(
                        s => s.Phon("a"),
                        s => s.Phon("z")))
                    .After(a => a
                        .Seq(s => s.Maybe(m => m.Phon(IPA.IsConsonant)),
                             Q.End)))
                .Rules(r => r
                    .Named("Amuïssement de /z/ antéconsonantique, allongement et vélarisation de /a/")
                    .Phono(_ => new[] { "ɑː" })
                    .Rewrite(_ => "â")));
        }

        public static Rule Rule3c()
        {
            return R.Rule(c => c
                .Id("p1c17r3c")
                .From(1050).To(1100)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Seq(
                        s => s.Phon("e"),
                        s => s.Phon("z")))
                    .After(a => a
                        .Seq(s => s.Maybe(m => m.Phon(IPA.IsConsonant)),
                             Q.End)))
                .Rules(r => r
                    .Named("Amuïssement de /z/ antéconsonantique, allongement et ouverture de /e/")
                    .Phono(_ => new[] { "ɛː" })
                    .Rewrite(_ => "ê")));
        }

        public static Rule Rule3d()
        {
            return R.Rule(c => c
                .Id("p1c17r3d")
                .From(1050).To(1100)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Seq(
                        s => s.Phon("e"),
                        s => s.Phon("z")))
                    .After(a => a
                        .Seq(s => s.Maybe(m => m.Phon(IPA.IsConsonant)),
                             Q.End)))
                .Rules(r => r
                    .Named("Amuïssement de /z/ antéconsonantique, allongement de /e/")
                    .Phono(_ => new[] { "eː" })
                    .Rewrite(_ => "é")));
        }

        public static Rule Rule3e()
        {
            return R.Rule(c => c
                .Id("p1c17r3e")
                .From(1050).To(1100)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Seq(
                        s => s.Phon(IPA.IsVowel),
                        s => s.Phon("z")))
                    .After(a => a
                        .Seq(s => s.Maybe(m => m.Phon(IPA.IsConsonant)),
                             Q.End)))
                .Rules(r => r
                    .Named("Amuïssement de /z/ antéconsonantique, allongement de la voyelle précédente")
                    .Phono(px => new[] { $"{px[0]}ː" })
                    .Rewrite(g => g.Substring(0, g.Length - 1) + "\u0302")));
        }



        public static Rule Rule4a()
        {
            return R.Rule(c => c
                .Id("p1c17r4a")
                .From(1150).To(1200)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Seq(
                        s => s.Phon("ɔ"),
                        s => s.Phon("s")))
                    .After(a => a
                        .Seq(s => s.Maybe(m => m.Phon(IPA.IsConsonant)),
                             Q.End)))
                .Rules(r => r
                    .Named("Amuïssement de /s/ antéconsonantique, allongement et ouverture de /ɔ/")
                    .Phono(_ => new[] { "oː" })
                    .Rewrite(_ => "ô")));
        }

        public static Rule Rule4b()
        {
            return R.Rule(c => c
                .Id("p1c17r4b")
                .From(1150).To(1200)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Seq(
                        s => s.Phon("a"),
                        s => s.Phon("s")))
                    .After(a => a
                        .Seq(s => s.Maybe(m => m.Phon(IPA.IsConsonant)),
                             Q.End)))
                .Rules(r => r
                    .Named("Amuïssement de /s/ antéconsonantique, allongement et vélarisation de /a/")
                    .Phono(_ => new[] { "ɑː" })
                    .Rewrite(_ => "â")));
        }

        public static Rule Rule4c()
        {
            return R.Rule(c => c
                .Id("p1c17r4c")
                .From(1150).To(1200)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Seq(
                        s => s.Phon("e"),
                        s => s.Phon("s")))
                    .After(a => a
                        .Seq(s => s.Maybe(m => m.Phon(IPA.IsConsonant)),
                             Q.End)))
                .Rules(r => r
                    .Named("Amuïssement de /s/ antéconsonantique, allongement et ouverture de /e/")
                    .Phono(_ => new[] { "ɛː" })
                    .Rewrite(_ => "ê")));
        }

        public static Rule Rule4d()
        {
            return R.Rule(c => c
                .Id("p1c17r4d")
                .From(1150).To(1200)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Seq(
                        s => s.Phon("e"),
                        s => s.Phon("s")))
                    .After(a => a
                        .Seq(s => s.Maybe(m => m.Phon(IPA.IsConsonant)),
                             Q.End)))
                .Rules(r => r
                    .Named("Amuïssement de /s/ antéconsonantique, allongement de /e/")
                    .Phono(_ => new[] { "eː" })
                    .Rewrite(_ => "é")));
        }

        public static Rule Rule4e()
        {
            return R.Rule(c => c
                .Id("p1c17r4e")
                .From(1150).To(1200)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Seq(
                        s => s.Phon(IPA.IsVowel),
                        s => s.Phon("s")))
                    .After(a => a
                        .Seq(s => s.Maybe(m => m.Phon(IPA.IsConsonant)),
                             Q.End)))
                .Rules(r => r
                    .Named("Amuïssement de /s/ antéconsonantique, allongement de la voyelle précédente")
                    .Phono(px => new[] { $"{px[0]}ː" })
                    .Rewrite(g => g.Substring(0, g.Length - 1) + "\u0302")));
        }



        public static Rule Rule5a()
        {
            return R.Rule(c => c
                .Id("p1c17r5a")
                .From(600).To(700)
                .Query(q => q
                    .Match(m => m.Phon("p"))
                    .After(a => a.Phon("t")))
                .Rules(r => r
                    .Named("Spirantisation de /p/ devant /t/")
                    .Phono(_ => new[] { "f" })
                    .Rewrite(_ => "f")));
        }

        public static Rule Rule5b()
        {
            return R.Rule(c => c
                .Id("p1c17r5b")
                .From(800).To(900)
                .Query(q => q
                    .Match(m => m.Phon("f", "v"))
                    .After(a => a.Phon("t", "d")))
                .Rules(r => r
                    .Named("Amuïssement de /f/ et /v/ devant dentale")
                    .Phono(P.Erase)
                    .Rewrite(G.Erase)));
        }

        public static Rule Rule5c()
        {
            return R.Rule(c => c
                .Id("p1c17r5c")
                .From(600).To(700)
                .Query(q => q
                    .Match(m => m.Phon("m"))
                    .After(a => a.Phon("t")))
                .Rules(r => r
                    .Named("Assimilation de /m/")
                    .Phono(_ => new[] { "n" })
                    .Rewrite(_ => "n")));
        }



        public static Rule Rule6a()
        {
            return R.Rule(c => c
                .Id("p1c17r6a")
                .From(600).To(700)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsConsonant))
                    .Match(m => m.Phon("j"))
                    .After(a => a.Seq(
                        s => s.Maybe(m => m.Phon(IPA.IsConsonant)),
                        Q.End)))
                .Rules(r => r
                    .Named("Amuïssement de /j/ final post-consonantique")
                    .Phono(P.Erase)
                    .Rewrite(G.Erase)));
        }

        public static Rule Rule6b()
        {
            return R.Rule(c => c
                .Id("p1c17r6b")
                .From(600).To(700)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsConsonant))
                    .Match(m => m.Seq(
                        s => s.Phon("n"),
                        s => s.Phon("s")))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Formation d'un /t/ transitoire")
                    .Phono(_ => new[] { "ʦ" })
                    .Rewrite(_ => "ts")));
        }

        public static Rule Rule6c()
        {
            return R.Rule(c => c
                .Id("p1c17r6c")
                .From(600).To(700)
                .Query(q => q
                    .Match(m => m.Seq(
                        s => s.Phon("ɲ"),
                        s => s.Maybe(a => a.Phon("ɲ")),
                        s => s.Phon("s")))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Formation d'un /t/ transitoire")
                    .Phono(_ => new[] { "n", "ʦ" })
                    .Rewrite(_ => "n ts")));
        }

        public static Rule Rule6d()
        {
            return R.Rule(c => c
                .Id("p1c17r6d")
                .From(600).To(700)
                .Query(q => q
                    .Match(m => m.Seq(
                        s => s.Phon("ʎ"),
                        s => s.Phon("s")))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Formation d'un /t/ transitoire")
                    .Phono(_ => new[] { "l", "ʦ" })
                    .Rewrite(_ => "l ts")));
        }

        public static Rule Rule6e()
        {
            return R.Rule(c => c
                .Id("p1c17r6e")
                .From(600).To(700)
                .Query(q => q
                    .Before(a => a.Phon(IPA.IsConsonant))
                    .Match(a => a.Phon(IPA.IsConsonant))
                    .After(a => a.Seq(
                        s => s.Phon(IPA.IsConsonant),
                        Q.End)))
                .Rules(r => r
                    .Named("Amuïssement de la consonne médiane")
                    .Phono(P.Erase)
                    .Rewrite(
                        G.Erase,
                        g => $"{g}:0")));
        }

        public static Rule Rule6f()
        {
            return R.Rule(c => c
                .Id("p1c17r6f")
                .From(600).To(700)
                .Query(q => q
                    .Match(a => a.Seq(
                        s => s.Phon("t", "d", "ð"),
                        s => s.Phon("t")))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Réduction des dentales devant /t/")
                    .Phono(_ => new[] { "t" })
                    .Rewrite(_ => "t")));
        }

        public static Rule Rule6g()
        {
            return R.Rule(c => c
                .Id("p1c17r6g")
                .From(600).To(700)
                .Query(q => q
                    .Match(a => a.Seq(
                        s => s.Phon("t", "d", "ð", "ʦʲ", "ʣʲ"),
                        s => s.Phon("s")))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Réduction des dentales devant /s/")
                    .Phono(_ => new[] { "ʦ" })
                    .Rewrite(_ => "ts")));
        }

        public static Rule Rule6k()
        {
            return R.Rule(c => c
                .Id("p1c17r6k")
                .From(600).To(700)
                .Query(q => q
                    .Match(a => a.Seq(
                        s => s.Phon("s"),
                        s => s.Phon("t"),
                        s => s.Phon("s")))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Réduction des dentales devant /s/")
                    .Phono(_ => new[] { "ʦ" })
                    .Rewrite(_ => "ts")));
        }

        public static Rule Rule6h()
        {
            return R.Rule(c => c
                .Id("p1c17r6h")
                .From(600).To(700)
                .Query(q => q
                    .Match(a => a.Seq(
                        s => s.Phon("s"),
                        s => s.Phon("s"),
                        s => s.Maybe(mm => mm.Phon("s"))))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Réduction de /ss/ final")
                    .Phono(_ => new[] { "s" })
                    .Rewrite(_ => "s")));
        }

        public static Rule Rule6i()
        {
            return R.Rule(c => c
                .Id("p1c17r6i")
                .From(600).To(650)
                .Query(q => q
                    .Match(m => m.Phon("v", "b", "g"))
                    .After(a => a.Seq(
                        s => s.Phon("s"),
                        Q.End)))
                .Rules(r => r
                    .Named("Dévoisement devant /s/ final")
                    .Phono(P.Unvoice)
                    .Rewrite(G.Unvoice)));
        }

        public static Rule Rule6j()
        {
            return R.Rule(c => c
                .Id("p1c17r6j")
                .From(650).To(700)
                .Query(q => q
                    .Match(m => m.Phon("f", "p", "k"))
                    .After(a => a.Seq(
                        s => s.Phon("s"),
                        Q.End)))
                .Rules(r => r
                    .Named("Amuïssement devant /s/ final")
                    .Phono(P.Erase)
                    .Rewrite(G.Erase)));
        }



        public static Rule Rule7a()
        {
            return R.Rule(c => c
                .Id("p1c17r7a")
                .From(1200).To(1300)
                .Query(q => q.Match(m => m.Phon("ʧ")))
                .Rules(r => r
                    .Named("Réduction de /ʧ/")
                    .Phono(_ => new[] { "ʃ" })));
        }

        public static Rule Rule7b()
        {
            return R.Rule(c => c
                .Id("p1c17r7b")
                .From(1200).To(1300)
                .Query(q => q.Match(m => m.Phon("ʤ")))
                .Rules(r => r
                    .Named("Réduction de /ʤ/")
                    .Phono(_ => new[] { "ʒ" })));
        }

        public static Rule Rule7c()
        {
            return R.Rule(c => c
                .Id("p1c17r7c")
                .From(1200).To(1300)
                .Query(q => q.Match(m => m.Phon("ʦ")))
                .Rules(r => r
                    .Named("Réduction de /ʦ/")
                    .Phono(_ => new[] { "s" })
                    .Rewrite(_ => "s")));
        }

        public static Rule Rule7d()
        {
            return R.Rule(c => c
                .Id("p1c17r7d")
                .From(1200).To(1300)
                .Query(q => q.Match(m => m.Phon("ʣ")))
                .Rules(r => r
                    .Named("Réduction de /ʣ/")
                    .Phono(_ => new[] { "z" })));
        }
    }
}
