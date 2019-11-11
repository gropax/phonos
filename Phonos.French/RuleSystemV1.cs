using Phonos.Core;
using Phonos.Core.RuleBuilder;
using System;

namespace Phonos.French
{
    public class RuleSystemV1
    {
        public Rule[] Build()
        {
            return R.System(s => s
                .Rule(Rule1())
            );
        }

        /// <summary>
        /// Affaiblissement des voyelles post-tonique entre une "occlusive orale" à l'avant,
        /// et une consonne liquide à l'arrière (r, l).
        /// [G. Zink, Phonétique historique du français, p. 39]
        /// </summary>
        public Rule Rule1()
        {
            return R.Rule(r => r
                .Named("Syncope des voyelles post-toniques entre une occlusive orale et une consonne liquide")
                .From(0).To(200)
                .Match(Q.PostTonicVowel)
                .Before(q => q.Phon("b", "k"))
                .After(q => q.Phon("l", "r"))
                .Map(P.Erase));
        }

        /// <summary>
        /// Affaiblissement des voyelles post-tonique entre une consonne homorganique
        /// à l'avant (r, l, n, s) et une dentale à l'arrière.
        /// [G. Zink, Phonétique historique du français, p. 39]
        /// </summary>
        public Rule Rule2()
        {
            return R.Rule(r => r
                .Named("Syncope des voyelles post-toniques entre une consonne homorganique et une dentale")
                .From(0).To(200)
                .Match(Q.PostTonicVowel)
                .Before(q => q.Phon("r", "l", "n", "s"))
                .After(q => q.Phon("t", "d"))
                .Map(P.Erase));
        }
    }
}
