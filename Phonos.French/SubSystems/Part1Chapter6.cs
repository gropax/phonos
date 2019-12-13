using Phonos.Core;
using Phonos.Core.RuleBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    public static class Part1Chapter6
    {
        public static RuleContext[] Rules()
        {
            return new[]
            {
                Rule1a(), Rule1b(), Rule1c(),
            };
        }

        public static RuleContext Rule1a()
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

        public static RuleContext Rule1b()
        {
            return R.Rule(c => c
                .Id("p1c6r3b")
                .Group("Effacement des voyelles finales")
                .From(600).To(700)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m => m.Phon(Q.VOWELS).With("accent", "final"))
                    .Before(b => b.Seq(s => s.Phon(Q.CONSONANT), s => s.Phon(Q.CONSONANT))))
                .Rules(r => r
                    .Named("Affaiblissement de la voyelle finale précédée d'un groupe consonantique conjoint")
                    .Phono(_ => new[] { "ə" })
                    .Rewrite(_ => "e")));
        }

        public static RuleContext Rule1c()
        {
            return R.Rule(c => c
                .Id("p1c6r3c")
                .Group("Effacement des voyelles finales")
                .From(600).To(700)
                .Query(
                    q => q
                        .Match(m => m.Phon(Q.VOWELS).With("accent", "final"))
                        .Before(b => b.Seq(s => s.Phon(Q.CONSONANT), s => s.Phon("ʤ"))),
                    q => q
                        .Match(m => m.Phon(Q.VOWELS).With("accent", "final"))
                        .Before(b => b.Seq(s => s.Phon("ɫ"), s => s.Phon("m", "n"))),
                    q => q
                        .Match(m => m.Phon(Q.VOWELS).With("accent", "final"))
                        .Before(b => b.Seq(s => s.Phon("y"), s => s.Phon("y"), s => s.Phon("r"))),
                    q => q
                        .Match(m => m.Phon(Q.VOWELS).With("accent", "final"))
                        .Before(b => b.Seq(s => s.Phon("m"), s => s.Phon("n"))))
                .Rules(r => r
                    .Named("Affaiblissement de la voyelle finale précédée de certains groupes consonantiques disjoints")
                    .Phono(_ => new[] { "ə" })
                    .Rewrite(_ => "e")));
        }

        //public static RuleContext Rule1d()
        //{
        //    return R.Rule(c => c
        //        .Id("p1c6r3b")
        //        .Group("Effacement des voyelles finales")
        //        .From(600).To(700)
        //        .Query(q => q
        //            .Match(m => m.Nothing())
        //            .Before(b => b.Seq(s => s.Phon(Q.CONSONANT), s => s.Phon(Q.CONSONANT))))
        //        .Rules(r => r
        //            .Named("Apparition d'un /ə/ de soutien après groupe consonantique conjoint")
        //            .Phono(_ => new[] { "ə" })
        //            .Rewrite(_ => "e")));
        //}

    }
}
