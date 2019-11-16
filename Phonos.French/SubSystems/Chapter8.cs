using Phonos.Core;
using Phonos.Core.RuleBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    /// <summary>
    /// Évolutions des voyelles du latin classique au latin vulgaire.
    /// [G. Zink, Phonétique historique du français, ch. VIII]
    /// </summary>
    public static class Chapter8
    {
        public static Rule[] Rules()
        {
            return new[]
            {
                Rule7(), Rule8(), Rule9(), Rule10(),
                Rule11(), Rule12(), Rule13(), Rule14(),
            };
        }

        /// <summary>
        /// Évolution de /ē/, /ō/, /ě/ et /ǒ/ en /e/, /o/, /ɛ/, /ɔ/ en latin vulgaire.
        /// [G. Zink, Phonétique historique du français, p. 50]
        /// @interactions [OK] Pas d'evolution de /e/ issu de /oe/
        /// </summary>
        public static Rule Rule7()
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
        /// @interactions [OK] Don't affect /i/ from /iː/
        /// </summary>
        public static Rule Rule8()
        {
            return R.Rule(r => r
                .Named("Évolution de /ǐ/ en latin vulgaire")
                .From(200).To(300)
                .Match(q => q.Phon("i")
                    .Without("classical_latin", "iː"))
                .Map(p => p.Phono(px => new [] { "e" })));
        }

        /// <summary>
        /// Évolution de /ǔ/ en /o/ à l'intérieur d'un mot, en latin vulgaire.
        /// [G. Zink, Phonétique historique du français, p. 50]
        /// @interactions [OK] Don't affect /u/ from /uː/
        /// </summary>
        public static Rule Rule9()
        {
            return R.Rule(r => r
                .Named("Évolution de /ǔ/ en position intérieure en latin vulgaire")
                .From(300).To(400)
                .Match(q => q.Phon("u")
                    .Without("accent", "final")
                    .Without("classical_latin", "uː"))  // @interaction
                .Map(p => p.Phono(px => new [] { "o" })));
        }

        /// <summary>
        /// Évolution de /ǔ/ en /o/ en finale de mot, en latin vulgaire.
        /// [G. Zink, Phonétique historique du français, p. 50]
        /// </summary>
        public static Rule Rule10()
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
        public static Rule Rule11()
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
        public static Rule Rule12()
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
        public static Rule Rule13()
        {
            return R.Rule(r => r
                .Named("Évolution de /au̯/ en latin vulgaire")
                .From(100).To(200)
                .Match(q => q.Phon("au̯"))
                .Map(p => p.Phono(px => new [] { "ɔ" })));
        }

        /// <summary>
        /// Disparition de l'opposition entre voyelles longues et brèves
        /// [G. Zink, Phonétique historique du français, p. 49]
        /// </summary>
        public static Rule Rule14()
        {
            return R.Rule(r => r
                .Named("Disparition de l'opposition entre voyelles longues et brèves")
                .From(100).To(200)
                .Match(q => q.Phon("aː", "iː", "uː"))  // Les cas /ē/ et /ō/ sont gérés en 7
                .Map(P.Shorten));
        }
    }
}
