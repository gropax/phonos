using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    /// <summary>
    /// Évolutions des voyelles du latin classique au latin vulgaire.
    /// [G. Zink, Phonétique historique du français, ch. VIII]
    /// </summary>
    public static class Part1Chapter08
    {
        public static Rule[] Rules()
        {
            return new[]
            {
                Rule1a(), Rule1b(), Rule1c(), Rule1d(), Rule1e(),
                Rule2a(), Rule2b(), Rule2c(), 
            };
        }

        public static Rule Rule1a()
        {
            return R.Rule(c => c
                .Id("p1c8r1a")
                .Group("Redistribution des quantités, différentiation des timbres")
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

        public static Rule Rule1b()
        {
            return R.Rule(c => c
                .Id("p1c8r1b")
                .Group("Redistribution des quantités, différentiation des timbres")
                .From(100).To(200)
                .Query(q => q
                    .Match(m => m.Phon("aː", "iː", "uː")))  // Les cas /ē/ et /ō/ sont gérés en 7
                .Rules(r => r
                    .Named("Disparition de l'opposition entre voyelles longues et brèves")
                    .Phono(P.Shorten)));
        }

        // @interactions [OK] Don't affect /i/ from /iː/
        public static Rule Rule1c()
        {
            return R.Rule(c => c
                .Id("p1c8r1c")
                .Group("Redistribution des quantités, différentiation des timbres")
                .From(200).To(300)
                .Query(q => q
                    .Match(m => m.Phon("i")
                        .Without("classical_latin", "iː")))
                .Rules(p => p
                    .Named("Ouverture de /ǐ/")
                    .Phono(px => new [] { "e" })
                    .Rewrite(_ => "e")));
        }

        // @interactions [OK] Don't affect /u/ from /uː/
        public static Rule Rule1d()
        {
            return R.Rule(c => c
                .Id("p1c8r1d")
                .Group("Redistribution des quantités, différentiation des timbres")
                .From(300).To(400)
                .Query(q => q
                    .Match(m => m.Phon("u")
                        .Without("accent", "final")
                        .Without("classical_latin", "uː")))  // @interaction
                .Rules(p => p
                    .Named("Évolution de /ǔ/ en position intérieure en latin vulgaire")
                    .Phono(px => new [] { "o" })
                    .Rewrite(_ => "o")));
        }

        public static Rule Rule1e()
        {
            return R.Rule(c => c
                .Id("p1c8r1e")
                .Group("Redistribution des quantités, différentiation des timbres")
                .From(400).To(500)
                .Query(q => q
                    .Match(m => m.Phon("u").With("accent", "final")))
                .Rules(p => p
                    .Named("Évolution de /ǔ/ en finale en latin vulgaire")
                    .Phono(px => new [] { "o" })
                    .Rewrite(_ => "o")));
        }


        public static Rule Rule2a()
        {
            return R.Rule(c => c
                .Id("p1c8r2a")
                .Group("Monophtongaison des diphtongues latines")
                .From(0).To(100)
                .Query(q => q
                    .Match(m => m.Phon("oi̯")))
                .Rules(p => p
                    .Named("Évolution de /oi̯/ en latin vulgaire")
                    .Phono(px => new [] { "e" })));
        }

        // @subrules
        public static Rule Rule2b()
        {
            return R.Rule(c => c
                .Id("p1c8r2b")
                .Group("Monophtongaison des diphtongues latines")
                .From(100).To(200)
                .Query(q => q
                    .Match(m => m.Phon("ai̯")))
                .Rules(p => p
                    .Named("Évolution de /ai̯/ en latin vulgaire")
                    .Phono(px => new [] { "ɛ" })));
        }

        // @subrules
        // @interactions Pas de diphtongaisons ultérieure du /ɔ/ (p. 51)
        // @evolution (p. 52)
        public static Rule Rule2c()
        {
            return R.Rule(c => c
                .Id("p1c8r2c")
                .Group("Monophtongaison des diphtongues latines")
                .From(100).To(200)
                .Query(q => q
                    .Match(m => m.Phon("au̯")))
                .Rules(p => p
                    .Named("Évolution de /au̯/ en latin vulgaire")
                    .Phono(px => new [] { "ɔ" })));
        }
    }
}
