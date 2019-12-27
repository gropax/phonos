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
                //Rule2a(), Rule2b(), Rule2c(), Rule2d(), Rule2e(),
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
    }
}
