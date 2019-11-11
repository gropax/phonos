using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.RuleBuilder
{
    public static class Q
    {
        public static Action<ContextQueryBuilder> Start => qb => qb.Start();
        public static Action<ContextQueryBuilder> End => qb => qb.End();

        public static Action<MatchQueryBuilder> PostTonicVowel =>
            qd => qd.Phon(VOWELS).With("accent", "post-tonic");

        public static string[] VOWELS = new[]
        {
            "a", "e", "i", "o", "u",
        };
    }
}
