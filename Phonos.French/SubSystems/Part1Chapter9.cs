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
                Rule1a(),
            };
        }

        /// <summary>
        /// Affaiblissement des voyelles post-tonique entre une "occlusive orale" à l'avant,
        /// et une consonne liquide à l'arrière (r, l).
        /// [G. Zink, Phonétique historique du français, p. 39]
        /// </summary>
        public static Rule Rule1a()
        {
            return R.Rule(r => r
                .Id("p1c9r1a")
                .Group("Diphtongaison de /ɛ/ tonique libre ou monosyllabique")
                .Named("Segmentation de /ɛ/ sous l'accent tonique")
                .From(100).To(200)
                .Match(q => q.Phon("ɛ").With("accent", "tonic"))
                .Before(q => q.Phon("b", "k"))
                .After(q => q.Phon("l", "r"))
                .Map(P.Erase));
        }
    }
}
