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
    public static class Chapter6
    {
        public static Rule[] Rules()
        {
            return new[]
            {
                Rule1(), Rule2(), Rule3(), Rule4(), Rule5(), Rule6(),
                Rule7(), Rule8(), Rule9(),
            };
        }

        /// <summary>
        /// Affaiblissement des voyelles post-tonique entre une "occlusive orale" à l'avant,
        /// et une consonne liquide à l'arrière (r, l).
        /// [G. Zink, Phonétique historique du français, p. 39]
        /// </summary>
        public static Rule Rule1()
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
        public static Rule Rule2()
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
        public static Rule Rule3()
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
        public static Rule Rule4()
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
        public static Rule Rule5()
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
        public static Rule Rule6()
        {
            return R.Rule(r => r
                .Named("Centralisation de /a/ pré-tonique en syllabe ouverte")
                .From(400).To(600)
                .Scope("syllable")
                .Match(q => q.Phon("a").With("accent", "pre-tonic"))
                .After(Q.End)
                .Map(p => p.Phono(_ => new[] { "ə" })));
        }

        /// <summary>
        /// Centralisation des voyelles pré-toniques autres que /a/ en syllabe ouverte,
        /// précédées d'un groupe consonantique (explosif)
        /// [G. Zink, Phonétique historique du français, p. 41]
        /// @interaction [?]
        /// </summary>
        public static Rule Rule7()
        {
            return R.Rule(r => r
                .Named("Centralisation des voyelles pré-toniques autres que /a/ en syllable ouverte, précédées d'un groupe consonantique")
                .From(400).To(600)
                .Scope("syllable")
                .Match(q => q.Phon(p => Q.Vowel(p) && p[0] != 'a')
                    .With("accent", "pre-tonic"))
                .Before(q => q.Seq(q1 => q1.Phon(Q.Consonant), q2 => q2.Phon(Q.Consonant)))
                .After(Q.End)
                .Map(p => p.Phono(_ => new[] { "ə" })));
        }

        /// <summary>
        /// Syncope des voyelles pré-toniques autres que /a/ en syllabe ouvert,
        /// non supportées par un groupe consonantique en précession
        /// [G. Zink, Phonétique historique du français, p. 41]
        /// @interaction [?]
        /// </summary>
        public static Rule Rule8()
        {
            return R.Rule(r => r
                .Named("Syncope des voyelles pré-toniques autres que /a/ en syllable ouverte")
                .From(400).To(600)
                .Scope("syllable")
                .Match(q => q.Phon(p => Q.Vowel(p) && p[0] != 'a')
                    .With("accent", "pre-tonic"))
                .Before(q => q.Seq(Q.Start, s2 => s2.Maybe(m => m.Phon(Q.Consonant))))
                .After(Q.End)
                .Map(P.Erase));
        }

        /// <summary>
        /// Évolution des pré-toniques autres que /a/ en syllabe fermée
        /// [G. Zink, Phonétique historique du français, p. 41]
        /// @interaction [TODO] Contralisation en cas de libération d'entrave
        /// @problem [TODO] Syllabation de calumnǐāre problématique : pour le moment
        /// /u/ est contr-tonique et /ǐ/ pré-tonique, mais l'exemple du livre donne
        /// /u/ comme pré-tonique (/ǐ/ est donc à considéré comme /j/ ?).
        /// </summary>
        public static Rule Rule9()
        {
            return R.Rule(r => r
                .Named("Antériorisation des pré-toniques autres que /a/ en syllabe formée")
                .From(400).To(600)
                .Scope("syllable")
                .Match(q => q.Phon(Q.Vowel)
                    .With("accent", "pre-tonic"))
                .After(q => q.Phon(Q.Consonant))
                .Map(p => p.Phono(_ => new[] { "e" }).Rewrite(_ => "e")));
        }
    }
}
