﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.RuleBuilder
{
    public static class Q
    {
        public static Action<ContextQueryBuilder> Start => qb => qb.Start();
        public static Action<ContextQueryBuilder> End => qb => qb.End();

        public static Action<MatchQueryBuilder> PostTonicVowel =>
            qd => qd.Phon(IPA.VOWELS).With("accent", "post-tonic");

        public static Action<ContextQueryBuilder> ConjointCluster =>
            m => m.Or(
                o => o.Seq(s => s.Phon(IPA.CONSONANTS), s => s.Phon(IPA.GLIDES)),
                o => o.Seq(s =>
                    s.Or(oo => oo.Phon(IPA.OCCLUSIVES), oo => oo.Phon(IPA.FRICATIVES)),
                    s => s.Phon(IPA.LIQUIDES)));
    }
}
