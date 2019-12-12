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
                Rule1a(), Rule1b(),
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
    }
}
