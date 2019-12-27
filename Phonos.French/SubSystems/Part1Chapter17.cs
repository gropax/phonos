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
            return RuleComponents();
            //return new IRule[] { };
        }

        public static IRule[] RuleComponents()
        {
            return new IRule[]
            {
                Rule1(),
                Rule2(),
                Rule3a(), Rule3b(), Rule3c(), Rule3d(), Rule3e(),
                Rule4a(), Rule4b(), Rule4c(), Rule4d(), Rule4e(),
            };
        }

        public static Rule Rule1()
        {
            return R.Rule(c => c
                .Id("p1c17r1")
                .From(200).To(300)
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
                    .Named("Réduction des groups intérieurs de 3 consonnes")
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
    }
}
