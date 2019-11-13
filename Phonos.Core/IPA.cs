using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core
{
    public static class IPA
    {
        private static HashSet<char> VOWELS = new HashSet<char>(
            new[] { 'a', 'e', 'i', 'o', 'u', 'y' });
        public static bool IsVowel(string phoneme)
        {
            return VOWELS.Contains(phoneme[0]);
        }

        private static HashSet<char> OCCLUSIVES = new HashSet<char>(
            new[] { 'p', 'b', 't', 'd', 'k', 'g' });
        public static bool IsOcclusive(string phoneme)
        {
            return OCCLUSIVES.Contains(phoneme[0]);
        }

        private static HashSet<char> FRICATIVES = new HashSet<char>(
            new[] { 'f', 'v', 's', 'z', 'ʃ', 'ʒ' });
        public static bool IsFricative(string phoneme)
        {
            return FRICATIVES.Contains(phoneme[0]);
        }

        private static HashSet<char> LIQUIDES = new HashSet<char>(
            new[] { 'l', 'r', 'ʁ' });
        public static bool IsLiquide(string phoneme)
        {
            return LIQUIDES.Contains(phoneme[0]);
        }

        //private HashSet<char> CONSONANTS = new HashSet<char>(
        //    new[] { 'j', 'w', 'ɥ' });
        //private bool IsConsonant(string phoneme)
        //{
        //    return CONSONANTS.Contains(phoneme[0]);
        //}

        private static HashSet<char> SEMI_VOWELS = new HashSet<char>(
            new[] { 'j', 'w', 'ɥ' });
        public static bool IsSemiVowel(string phoneme)
        {
            return SEMI_VOWELS.Contains(phoneme[0]);
        }


        private static HashSet<char> QUANTITY_MARKS = new HashSet<char>(
            new[] { 'ː', '\u032f' });
        public static bool IsLong(string phoneme)
        {
            return QUANTITY_MARKS.Contains(phoneme.Last());
        }
    }
}
