using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    /// <summary>
    /// [G. Zink, Phonétique historique du français, ch. XXIV p. 158]
    /// </summary>
    public static class Part1Chapter24
    {
        public static IRule[] Rules()
        {
            return new IRule[]
            {
                Rule1(),
            };
        }

        public static IRule[] RuleComponents()
        {
            return new[]
            {
                Rule1(),
            };
        }

        public static Rule Rule1()
        {
            return R.Rule(c => c
                .Id("p1c24r1")
                .Group("")
                .From(1600).To(1700)
                .Query(q => q.Match(m => m.Phon("r")))
                .Rules(r => r
                    .Named("Changement d'articulation de /r/")
                    .Phono(_ => new[] { "ʁ" })));
        }
    }
}
