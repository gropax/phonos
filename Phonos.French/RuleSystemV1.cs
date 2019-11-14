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

        /// <summary>
        /// Affaiblissement des voyelles post-tonique suivies d'un /a/ en finale.
        /// [G. Zink, Phonétique historique du français, p. 39]
        /// </summary>
        /// <remarks>
        /// @fixme
        ///     Cette règle s'applique-t-elle à toutes les voyelle ? (je n'ai
        ///     que des exemples de syncope du /i/).
        /// </remarks>
        public Rule Rule3()
        {
            return R.Rule(r => r
                .Named("Syncope des voyelles post-toniques suivies d'un /a/ final")
                .From(0).To(200)
                .Match(q => q.Phon(Q.VOWEL).With("accent", "post-tonic"))
                .After(q => q.Seq(_ => _.Phon(Q.CONSONANT),
                    q2 => q2.Maybe(_ => _.Phon(Q.CONSONANT)),
                    q3 => q3.Phon("a", "a").With("accent", "final")))
                .Map(P.Erase));
        }

        /// <summary>
        /// Consonification des post-toniques brèves en hiatus
        /// [G. Zink, Phonétique historique du français, p. 40]
        /// </summary>
        public Rule Rule4()
        {
            return R.Rule(r => r
                .Named("Consonification des post-toniques brèves en hiatus")
                .From(0).To(100)
                .Match(q => q.Phon(Q.VOWEL).With("accent", "post-tonic"))
                .After(q => q.Phon(Q.VOWEL))
                .Map(P.Consonify));
        }

        /// <summary>
        /// Affaiblissement de toutes les voyelles post-toniques restantes.
        /// [G. Zink, Phonétique historique du français, pp. 39-40]
        /// </summary>
        /// <remarks>
        /// @fixme
        ///    - Ici ont lieu des problèmes de séquençage.
        ///    - Il faut préciser la datation, voire scinder la règle
        /// </remarks>
        public Rule Rule5()
        {
            return R.Rule(r => r
                .Named("Syncope des voyelles post-toniques restantes")
                .From(200).To(500)
                .Match(q => q.Phon(Q.VOWEL).With("accent", "post-tonic"))
                .Map(P.Erase));
        }


        /// <summary>
        /// Centralisation de /a/ pré-tonique en syllabe ouverte.
        /// [G. Zink, Phonétique historique du français, p. 41]
        /// </summary>
        public Rule Rule6()
        {
            return R.Rule(r => r
                .Named("Centralisation de /a/ pré-tonique en syllabe ouverte")
                .From(600).To(700)
                .Scope("syllable")
                .Match(q => q.Phon("a").With("accent", "pre-tonic"))
                .After(Q.End)
                .Map(p => p.Phono(_ => new[] { "ə" })));
        }
    }
}
