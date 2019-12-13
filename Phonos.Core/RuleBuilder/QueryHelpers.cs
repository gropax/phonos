using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.RuleBuilder
{
    public static class Q
    {
        public static Action<ContextQueryBuilder> Start => qb => qb.Start();
        public static Action<ContextQueryBuilder> End => qb => qb.End();

        //public static Action<MatchQueryBuilder> Consonant =>
        //    qd => qd.Phon(CONSONANT);
        public static Action<MatchQueryBuilder> PostTonicVowel =>
            qd => qd.Phon(VOWEL).With("accent", "post-tonic");

        public static string[] VOWELS = new[]
        {
            "i", "y", "ɨ", "ʉ", "ɯ", "u", "ɪ", "ʏ", "ʊ", "e", "ø",
            "ɘ", "ɵ", "ɤ", "o", "ə", "ɛ", "œ", "ɜ", "ɞ",
            "ʌ", "ɔ", "æ", "ɐ", "a", "ɶ", "ä", "ɑ", "ɒ",
        };

        public static bool Vowel(string phoneme)
        {
            return VOWELS.Contains(phoneme[0].ToString());
        }

        public static bool Consonant(string phoneme)
        {
            return CONSONANT.Contains(phoneme[0].ToString());
        }

        public static bool NasalConsonant(string phoneme)
        {
            return NASAL_CONSONANT.Contains(phoneme[0].ToString());
        }

        public static bool NonNasalConsonant(string phoneme)
        {
            return NON_NASAL_CONSONANT.Contains(phoneme[0].ToString());
        }

        public static string[] VOWEL = new[]
        {
            "a", "aː", "e", "eː", "i", "iː", "o", "oː", "u", "uː", "y", "yː" 
        };

        public static string[] CONSONANT = new[]
        {
            "p", "pʰ", "b", "m", "f", "v", "t", "tʰ", "d", "n", "s", "z",
            "r", "ʁ", "l", "k", "g", "ŋ", "kʰ", "kʷ", "gʷ", "h",
        };

        public static string[] GLIDE = new[]
        {
            "j", "w", "ɥ",
        };

        public static string[] NASAL_CONSONANT = new[]
        {
            "m", "n", "ŋ", "ɲ",
        };

        public static string[] NON_VOWEL = CONSONANT.Concat(GLIDE).ToArray();
        public static string[] NON_CONSONANT = VOWEL.Concat(GLIDE).ToArray();
        public static string[] NON_NASAL_CONSONANT = CONSONANT.Except(NASAL_CONSONANT).ToArray();
    }
}
