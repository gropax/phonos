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
                RuleSet3(),  // Voyelles finales
            };
        }

        public static IRule[] RuleComponents()
        {
            return new[]
            {
                Rule1a(), Rule1b(), Rule1c(), Rule1d(), Rule1e(), Rule1f(), Rule1g(), Rule1h(),
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
                Rule1a(),  // /a/
                Rule1h(),  // /nt/
                Rule1b(),  // cluster conjoints
                Rule1c(),  // cluster disjoints
                Rule1f(),  // accent d'écho
                Rule1g());
        }

        public static IRule RuleSet3b()
        {
            return new FirstRule("p1c6s3b",
                Rule1d(),  // voyelle de support cluster conjoint
                Rule1e()); // voyelle de support cluster disjoint
        }

        public static Rule Rule1a()
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

        public static Rule Rule1b()
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

        public static Rule Rule1c()
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

        public static Rule Rule1d()
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

        public static Rule Rule1e()
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

        public static Rule Rule1f()
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

        public static Rule Rule1g()
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

        public static Rule Rule1h()
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
