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
        public static RuleContext[] Rules()
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
        public static RuleContext Rule7()
        {
            return R.Rule(c => c
                .From(100).To(200)
                .Query(q => q
                    .Match(m => m.Phon("eː", "e", "oː", "o")
                        .Without("classical_latin", "oi̯")))  // @interaction
                .Rules(p => p
                    .Named("Évolution des voyelles /e/ et /o/ en latin vulgaire")
                    .Phono(px =>
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
        public static RuleContext Rule8()
        {
            return R.Rule(c => c
                .From(200).To(300)
                .Query(q => q
                    .Match(m => m.Phon("i")
                        .Without("classical_latin", "iː")))
                .Rules(p => p
                    .Named("Évolution de /ǐ/ en latin vulgaire")
                    .Phono(px => new [] { "e" })));
        }

        /// <summary>
        /// Évolution de /ǔ/ en /o/ à l'intérieur d'un mot, en latin vulgaire.
        /// [G. Zink, Phonétique historique du français, p. 50]
        /// @interactions [OK] Don't affect /u/ from /uː/
        /// </summary>
        public static RuleContext Rule9()
        {
            return R.Rule(c => c
                .From(300).To(400)
                .Query(q => q
                    .Match(m => m.Phon("u")
                        .Without("accent", "final")
                        .Without("classical_latin", "uː")))  // @interaction
                .Rules(p => p
                    .Named("Évolution de /ǔ/ en position intérieure en latin vulgaire")
                    .Phono(px => new [] { "o" })));
        }

        /// <summary>
        /// Évolution de /ǔ/ en /o/ en finale de mot, en latin vulgaire.
        /// [G. Zink, Phonétique historique du français, p. 50]
        /// </summary>
        public static RuleContext Rule10()
        {
            return R.Rule(c => c
                .From(400).To(500)
                .Query(q => q
                    .Match(m => m.Phon("u").With("accent", "final")))
                .Rules(p => p
                    .Named("Évolution de /ǔ/ en finale en latin vulgaire")
                    .Phono(px => new [] { "o" })));
        }

        /// <summary>
        /// Évolution de /oi̯/ en /e/ en latin vulgaire.
        /// [G. Zink, Phonétique historique du français, p. 51]
        /// </summary>
        public static RuleContext Rule11()
        {
            return R.Rule(c => c
                .From(0).To(100)
                .Query(q => q
                    .Match(m => m.Phon("oi̯")))
                .Rules(p => p
                    .Named("Évolution de /oi̯/ en latin vulgaire")
                    .Phono(px => new [] { "e" })));
        }

        /// <summary>
        /// Évolution de /ai̯/ en /ɛ/ en latin vulgaire.
        /// [G. Zink, Phonétique historique du français, p. 51]
        /// @subrules
        /// </summary>
        public static RuleContext Rule12()
        {
            return R.Rule(c => c
                .From(100).To(200)
                .Query(q => q
                    .Match(m => m.Phon("ai̯")))
                .Rules(p => p
                    .Named("Évolution de /ai̯/ en latin vulgaire")
                    .Phono(px => new [] { "ɛ" })));
        }

        /// <summary>
        /// Évolution de /au̯/ en /ɔ/ en latin vulgaire.
        /// [G. Zink, Phonétique historique du français, p. 51]
        /// @subrules
        /// @interactions Pas de diphtongaisons ultérieure du /ɔ/ (p. 51)
        /// @evolution (p. 52)
        /// </summary>
        public static RuleContext Rule13()
        {
            return R.Rule(c => c
                .From(100).To(200)
                .Query(q => q
                    .Match(m => m.Phon("au̯")))
                .Rules(p => p
                    .Named("Évolution de /au̯/ en latin vulgaire")
                    .Phono(px => new [] { "ɔ" })));
        }

        /// <summary>
        /// Disparition de l'opposition entre voyelles longues et brèves
        /// [G. Zink, Phonétique historique du français, p. 49]
        /// </summary>
        public static RuleContext Rule14()
        {
            return R.Rule(c => c
                .From(100).To(200)
                .Query(q => q
                    .Match(m => m.Phon("aː", "iː", "uː")))  // Les cas /ē/ et /ō/ sont gérés en 7
                .Rules(r => r
                    .Named("Disparition de l'opposition entre voyelles longues et brèves")
                    .Phono(P.Shorten)));
        }
    }
}
