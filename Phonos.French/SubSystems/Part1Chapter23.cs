using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    public static class Part1Chapter23
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
                Rule1a(), Rule1b(), Rule1c(), Rule1d(), Rule1e(), Rule1f(),
            };
        }

        public static Rule Rule1a()
        {
            return R.Rule(c => c
                .Id("p1c23r1a")
                .From(300).To(400)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsLongVowel))
                    .Match(m => m.Seq(s => s.Phon("l"), s => s.Phon("l"))))
                .Rules(r => r
                    .Named("Dégémination de /ll/ devant voyelle longue")
                    .Phono(px => new[] { "l" })
                    .Rewrite(_ => "l")));
        }

        public static Rule Rule1b()
        {
            return R.Rule(c => c
                .Id("p1c23r1b")
                .From(600).To(700)
                .Query(q => q.Match(m => m.Twice(t => t
                    .Phon(p => IPA.IsConsonant(p) && p != "r"))))
                .Rules(r => r
                    .Named("Dégémination en intervocalique (excepté /rr/)")
                    .Phono(px => new[] { px[0] })));
        }

        public static Rule Rule1c()
        {
            return R.Rule(c => c
                .Id("p1c23r1c")
                .From(1600).To(1700)
                .Query(q => q
                    .Match(m => m.Seq(s => s.Phon("r"), s => s.Phon("r"))))
                .Rules(r => r
                    .Named("Dégémination de /rr/")
                    .Phono(px => new[] { "r" })));
        }

        public static Rule Rule1d()
        {
            return R.Rule(c => c
                .Id("p1c23r1d")
                .From(900).To(1000)
                .Query(q => q
                    .Match(m => m.Seq(s => s.Phon("m"), s => s.Phon("n"))))
                .Rules(
                    r => r
                        .Named("Assimilation de /mn/ en /mm/")
                        .Phono(px => new[] { "m", "m" })
                        .Rewrite(_ => "m m"),
                    r => r
                        .Named("Assimilation de /mn/ en /mm/")
                        .Phono(px => new[] { "n", "n" })
                        .Rewrite(_ => "n n")));
        }

        public static Rule Rule1e()
        {
            return R.Rule(c => c
                .Id("p1c23r1e")
                .From(900).To(1000)
                .Query(q => q
                    .Match(m => m.Seq(s => s.Phon("n"), s => s.Phon("m"))))
                .Rules(r => r
                    .Named("Assimilation de /nm/")
                    .Phono(px => new[] { "m", "m" })
                    .Rewrite(_ => "m m")));
        }

        public static Rule Rule1f()
        {
            return R.Rule(c => c
                .Id("p1c23r1f")
                .From(1100).To(1200)
                .Query(q => q.Match(m => m.Twice(t => t
                    .Phon(p => IPA.IsConsonant(p)))))
                .Rules(r => r
                    .Named("Dégémination")
                    .Phono(px => new[] { px[0] })));
        }
    }
}
