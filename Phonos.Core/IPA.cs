using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core
{
    public static class IPA
    {
        public static string[] VOWELS = new []
        {
            "i", "y", "ɨ", "ʉ", "ɯ", "u", "ɪ", "ʏ", "ʊ", "e", "ø",
            "ɘ", "ɵ", "ɤ", "o", "ə", "ɛ", "œ", "ɜ", "ɞ",
            "ʌ", "ɔ", "æ", "ɐ", "a", "ɶ", "ä", "ɑ", "ɒ",
        };

        public static bool IsVowel(string phoneme)
        {
            return VOWELS.Contains(phoneme[0].ToString());
        }

        public static bool IsLongVowel(string phoneme)
        {
            return VOWELS.Contains(phoneme[0].ToString())
                && phoneme[phoneme.Length - 1] == 'ː';
        }


        public static string[] OCCLUSIVES = new []
        {
            "p", "b", "p̪", "b̪", "t̼", "d̼", "t", "d", "ʈ", "ɖ",
            "c", "ɟ", "k", "ɡ", "q", "ɢ", "ʡ", "ʔ",
        };
        public static bool IsOcclusive(string phoneme)
        {
            return OCCLUSIVES.Contains(phoneme[0].ToString());
        }

        public static string[] FRICATIVES = new []
        {
            "f", "v", "s", "z", "ʃ", "ʒ", "ɸ", "β", "f", "v",
            "θ", "ð", "x", "ɣ", "χ", "ʁ", "h",
        };
        public static bool IsFricative(string phoneme)
        {
            return FRICATIVES.Contains(phoneme[0].ToString());
        }

        public static string[] LIQUIDES = new []
        {
            "l", "r", "ʁ"
        };
        public static bool IsLiquide(string phoneme)
        {
            return LIQUIDES.Contains(phoneme[0].ToString());
        }

        public static string[] GLIDES = new []
        {
            "j", "w", "ɥ",
        };
        public static bool IsGlide(string phoneme)
        {
            return GLIDES.Contains(phoneme[0].ToString());
        }

        public static string[] OTHER_CONSONANTS = new[]
        {
            "pʰ", "tʰ", "kʰ", "kʷ", "gʷ",
        };

        public static HashSet<char> QUANTITY_MARKS = new HashSet<char>(
            new[] { 'ː', '\u032f' });
        public static bool IsLong(string phoneme)
        {
            return QUANTITY_MARKS.Contains(phoneme.Last());
        }

        public static string[] NASAL_CONSONANTS = new []
        {
            "m", "n", "ŋ", "ɲ",
        };
        public static bool IsNasalConsonant(string phoneme)
        {
            return NASAL_CONSONANTS.Contains(phoneme[0].ToString());
        }

        public static bool IsNasalVowel(string phoneme)
        {
            return IsVowel(phoneme.Substring(0, 1)) && phoneme.Contains('\u0303');
        }

        public static string[] CONSONANTS = OCCLUSIVES.Concat(FRICATIVES).Concat(LIQUIDES).Concat(NASAL_CONSONANTS).Concat(OTHER_CONSONANTS).ToArray();
        public static bool IsConsonant(string phoneme)
        {
            return CONSONANTS.Contains(phoneme[0].ToString());
        }

        public static string[] NON_VOWELS = CONSONANTS.Concat(GLIDES).ToArray();
        public static bool NonVowel(string phoneme)
        {
            return NON_NASAL_CONSONANTS.Contains(phoneme[0].ToString());
        }

        public static string[] NON_CONSONANTS = VOWELS.Concat(GLIDES).ToArray();
        public static bool NonConsonant(string phoneme)
        {
            return NON_NASAL_CONSONANTS.Contains(phoneme[0].ToString());
        }

        public static string[] NON_NASAL_CONSONANTS = CONSONANTS.Except(NASAL_CONSONANTS).ToArray();
        public static bool NonNasalConsonant(string phoneme)
        {
            return NON_NASAL_CONSONANTS.Contains(phoneme[0].ToString());
        }
    }
}
