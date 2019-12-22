using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    public static class Part1Chapter6
    {
        public static IRule[] Rules()
        {
            return new[]
            {
                Rule1a(), Rule1b(), Rule1c(),
                RuleSet3(),  // Voyelles finales
            };
        }

        public static IRule[] RuleComponents()
        {
            return new[]
            {
                Rule1a(), Rule1b(), Rule1c(),
                Rule3a(), Rule3b(), Rule3c(), Rule3d(), Rule3e(), Rule3f(), Rule3g(), Rule3h(),
                RuleSet3a(), RuleSet3b(), RuleSet3()
            };
        }

        public static IRule RuleSet3()
        {
            return new RuleSequence("p1c6s3",
                RuleSet3a(),  // Affaiblissement ou disparition de la voyelle finale
                RuleSet3b()); // Reconstitution d'une voyelle de support
        }

        public static IRule RuleSet3a()
        {
            return new FirstRule("p1c6s3a",
                Rule3a(),  // /a/
                Rule3h(),  // /nt/
                Rule3b(),  // cluster conjoints
                Rule3c(),  // cluster disjoints
                Rule3f(),  // accent d'écho
                Rule3g());
        }

        public static IRule RuleSet3b()
        {
            return new FirstRule("p1c6s3b",
                Rule3d(),  // voyelle de support cluster conjoint
                Rule3e()); // voyelle de support cluster disjoint
        }

        public static Rule Rule1a()
        {
            return R.Rule(c => c
                .Id("p1c6r1a")
                .Group("Amuïssements précoces des post-toniques")
                .From(0).To(200)
                .Query(q => q
                    .Match(Q.PostTonicVowel)
                    .Before(b => b.Phon("b", "k"))
                    .After(a => a.Phon("l", "r")))
                .Rules(r => r
                    .Named("Syncope des voyelles post-toniques entre une occlusive orale et une consonne liquide")
                    .Phono(P.Erase).Rewrite(G.Erase)));
        }

        public static Rule Rule1b()
        {
            return R.Rule(c => c
                .Id("p1c6r1b")
                .Group("Amuïssements précoces des post-toniques")
                .From(0).To(200)
                .Query(q => q
                    .Match(Q.PostTonicVowel)
                    .Before(b => b.Phon("r", "l", "n", "s"))
                    .After(a => a.Phon("t", "d")))
                .Rules(r => r
                    .Named("Syncope des voyelles post-toniques entre une consonne homorganique et une dentale")
                    .Phono(P.Erase).Rewrite(G.Erase)));
        }

        public static Rule Rule1c()
        {
            return R.Rule(c => c
                .Id("p1c6r1c")
                .Group("Amuïssements précoces des post-toniques")
                .From(0).To(200)
                .Query(q => q
                    .Match(m => m.Phon(IPA.VOWELS).With("accent", "post-tonic"))
                    .After(a => a.Seq(_ => _.Phon(IPA.CONSONANTS),
                        q2 => q2.Maybe(_ => _.Phon(IPA.CONSONANTS)),
                        q3 => q3.Phon("a", "a").With("accent", "final"))))
                .Rules(r => r
                    .Named("Syncope des voyelles post-toniques suivies d'un /a/ final")
                    .Phono(P.Erase).Rewrite(G.Erase)));
        }
        public static Rule Rule3a()
        {
            return R.Rule(c => c
                .Id("p1c6r3a")
                .Group("Effacement des voyelles finales")
                .From(600).To(700)
                .Query(q => q
                    .Match(m => m.Phon("a").With("accent", "final")))
                .Rules(r => r
                    .Named("Affaiblissement de /a/ final")
                    .Phono(_ => new[] { "ə" })
                    .Rewrite(_ => "e")));
        }

        public static Rule Rule3b()
        {
            return R.Rule(c => c
                .Id("p1c6r3b")
                .Group("Effacement des voyelles finales")
                .From(600).To(700)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon(IPA.IsVowel).With("accent", "final"))
                    .Before(b => b.Seq(s => s.Phon(IPA.CONSONANTS), s => s.Phon(IPA.CONSONANTS))))
                .Rules(r => r
                    .Named("Affaiblissement de la voyelle finale précédée d'un groupe consonantique conjoint")
                    .Phono(_ => new[] { "ə" })
                    .Rewrite(_ => "e")));
        }

        public static Rule Rule3c()
        {
            return R.Rule(c => c
                .Id("p1c6r3c")
                .Group("Effacement des voyelles finales")
                .From(600).To(700)
                .Query(
                    q => q
                        .Match(m => m.Phon(IPA.IsVowel).With("accent", "final"))
                        .Before(b => b.Seq(s => s.Phon(IPA.CONSONANTS), s => s.Phon("ʤ"))),
                    q => q
                        .Match(m => m.Phon(IPA.IsVowel).With("accent", "final"))
                        .Before(b => b.Seq(s => s.Phon("ɫ"), s => s.Phon("m", "n"))),
                    q => q
                        .Match(m => m.Phon(IPA.IsVowel).With("accent", "final"))
                        .Before(b => b.Seq(s => s.Phon("y"), s => s.Phon("y"), s => s.Phon("r"))),
                    q => q
                        .Match(m => m.Phon(IPA.IsVowel).With("accent", "final"))
                        .Before(b => b.Seq(s => s.Phon("m"), s => s.Phon("n"))))
                .Rules(r => r
                    .Named("Affaiblissement de la voyelle finale précédée de certains groupes consonantiques disjoints")
                    .Phono(_ => new[] { "ə" })
                    .Rewrite(_ => "e")));
        }

        public static Rule Rule3d()
        {
            return R.Rule(c => c
                .Id("p1c6r3d")
                .Group("Effacement des voyelles finales")
                .From(600).To(700)
                .Query(q => q
                    .Match(m => m.Nothing())
                    .Before(Q.ConjointCluster)
                    .After(a => a.End()))
                .Rules(r => r
                    .Named("Apparition d'un /ə/ de soutien après groupe consonantique conjoint")
                    .Phono(_ => new[] { "ə" })
                    .Rewrite(_ => "e")));
        }

        public static Rule Rule3e()
        {
            return R.Rule(c => c
                .Id("p1c6r3e")
                .Group("Effacement des voyelles finales")
                .From(600).To(700)
                .Query(
                    q => q
                        .Match(m => m.Nothing())
                        .Before(b => b.Seq(s => s.Phon(IPA.CONSONANTS), s => s.Phon("ʤ")))
                        .After(a => a.End()),
                    q => q
                        .Match(m => m.Nothing())
                        .Before(b => b.Seq(s => s.Phon("ɫ"), s => s.Phon("m", "n")))
                        .After(a => a.End()),
                    q => q
                        .Match(m => m.Nothing())
                        .Before(b => b.Seq(s => s.Phon("y"), s => s.Phon("y"), s => s.Phon("r")))
                        .After(a => a.End()),
                    q => q
                        .Match(m => m.Nothing())
                        .Before(b => b.Seq(s => s.Phon("m"), s => s.Phon("n")))
                        .After(a => a.End()))
                .Rules(r => r
                    .Named("Apparition d'un /ə/ de soutien après groupe consonantique disjoint")
                    .Phono(_ => new[] { "ə" })
                    .Rewrite(_ => "e")));
        }

        public static Rule Rule3f()
        {
            return R.Rule(c => c
                .Id("p1c6r3f")
                .Group("Effacement des voyelles finales")
                .From(600).To(700)
                .Query(q => q
                    .Match(m => m.Phon(IPA.IsVowel)
                        .With("accent", "final")
                        .With("echo", "echo")))
                .Rules(r => r
                    .Named("Affaiblissement de la voyelle finale sous accent d'écho")
                    .Phono(_ => new[] { "ə" })
                    .Rewrite(_ => "e")));
        }

        public static Rule Rule3g()
        {
            return R.Rule(c => c
                .Id("p1c6r3g")
                .Group("Effacement des voyelles finales")
                .From(600).To(700)
                .Query(q => q
                    .Match(m => m.Phon(IPA.IsVowel)
                        .With("accent", "final")
                        .Without("echo", "echo")))
                .Rules(r => r
                    .Named("Disparition de la voyelle finale hors de l'accent d'écho")
                    .Phono(_ => new string[0])
                    .Rewrite(_ => "")));
        }

        public static Rule Rule3h()
        {
            return R.Rule(c => c
                .Id("p1c6r3h")
                .Group("Effacement des voyelles finales")
                .From(600).To(700)
                .Query(q => q
                    .Match(m => m.Phon(IPA.IsVowel).With("accent", "final"))
                    .After(a => a.Seq(s => s.Phon("n"), s => s.Phon("t"), s => s.End())))
                .Rules(r => r
                    .Named("Affaiblissement de la voyelle finale entravée par /nt/")
                    .Phono(_ => new[] { "ə" })
                    .Rewrite(_ => "e")));
        }
    }
}
