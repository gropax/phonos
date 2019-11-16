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

        /// <summary>
        /// Évolution de /ē/, /ō/, /ě/ et /ǒ/ en /e/, /o/, /ɛ/, /ɔ/ en latin vulgaire.
        /// [G. Zink, Phonétique historique du français, p. 50]
        /// @interactions [OK] Pas d'evolution de /e/ issu de /oe/
        /// </summary>
        public Rule Rule7()
        {
            return R.Rule(r => r
                .Named("Évolution des voyelles /e/ et /o/ en latin vulgaire")
                .From(100).To(200)
                .Match(q => q.Phon("eː", "e", "oː", "o")
                    .Without("classical_latin", "oi̯"))  // @interaction
                .Map(p => p.Phono(px =>
                {
                    if (px[0] == "eː")
                        return new[] { "e" };
                    else if (px[0] == "e")
                        return new[] { "ɛ" };
                    else if (px[0] == "oː")
                        return new[] { "o" };
                    else  // 
                        return new[] { "ɔ" };
                })));
        }

        /// <summary>
        /// Évolution de /ǐ/ en /e/ en latin vulgaire.
        /// [G. Zink, Phonétique historique du français, p. 50]
        /// </summary>
        public Rule Rule8()
        {
            return R.Rule(r => r
                .Named("Évolution de /ǐ/ en latin vulgaire")
                .From(200).To(300)
                .Match(q => q.Phon("i"))
                .Map(p => p.Phono(px => new [] { "e" })));
        }

        /// <summary>
        /// Évolution de /ǔ/ en /o/ à l'intérieur d'un mot, en latin vulgaire.
        /// [G. Zink, Phonétique historique du français, p. 50]
        /// </summary>
        public Rule Rule9()
        {
            return R.Rule(r => r
                .Named("Évolution de /ǔ/ en position intérieure en latin vulgaire")
                .From(300).To(400)
                .Match(q => q.Phon("u").Without("accent", "final"))
                .Map(p => p.Phono(px => new [] { "o" })));
        }

        /// <summary>
        /// Évolution de /ǔ/ en /o/ en finale de mot, en latin vulgaire.
        /// [G. Zink, Phonétique historique du français, p. 50]
        /// </summary>
        public Rule Rule10()
        {
            return R.Rule(r => r
                .Named("Évolution de /ǔ/ en finale en latin vulgaire")
                .From(400).To(500)
                .Match(q => q.Phon("u").With("accent", "final"))
                .Map(p => p.Phono(px => new [] { "o" })));
        }

        /// <summary>
        /// Évolution de /oi̯/ en /e/ en latin vulgaire.
        /// [G. Zink, Phonétique historique du français, p. 51]
        /// </summary>
        public Rule Rule11()
        {
            return R.Rule(r => r
                .Named("Évolution de /oi̯/ en latin vulgaire")
                .From(0).To(100)
                .Match(q => q.Phon("oi̯"))
                .Map(p => p.Phono(px => new [] { "e" })));
        }

        /// <summary>
        /// Évolution de /ai̯/ en /ɛ/ en latin vulgaire.
        /// [G. Zink, Phonétique historique du français, p. 51]
        /// @subrules
        /// </summary>
        public Rule Rule12()
        {
            return R.Rule(r => r
                .Named("Évolution de /ai̯/ en latin vulgaire")
                .From(100).To(200)
                .Match(q => q.Phon("ai̯"))
                .Map(p => p.Phono(px => new [] { "ɛ" })));
        }

        /// <summary>
        /// Évolution de /au̯/ en /ɔ/ en latin vulgaire.
        /// [G. Zink, Phonétique historique du français, p. 51]
        /// @subrules
        /// @interactions Pas de diphtongaisons ultérieure du /ɔ/ (p. 51)
        /// @evolution (p. 52)
        /// </summary>
        public Rule Rule13()
        {
            return R.Rule(r => r
                .Named("Évolution de /au̯/ en latin vulgaire")
                .From(100).To(200)
                .Match(q => q.Phon("au̯"))
                .Map(p => p.Phono(px => new [] { "ɔ" })));
        }

        /// <summary>
        /// Évolutions des voyelles du latin classique au latin vulgaire.
        /// </summary>
        public Rule[] RuleSystem1()
        {
            return new[]
            {
                Rule7(), Rule8(), Rule9(), Rule10(),
                Rule11(), Rule12(), Rule13(),
            };
        }

        //public Rule[] RuleSystem1()
        //{
        //    return R.System(s => s
        //        .Rule(r => r
        //            .Named("Changement vocaliques du latin vulgaire")
        //            .From(0).To(200)
        //            .Match(q => q.Phon("a").With("accent", "pre-tonic"))
        //            .After(Q.End)
        //            .Map(p => p.Phono(_ => new[] { "ə" })));
        //            ))
        //}
    }
}
