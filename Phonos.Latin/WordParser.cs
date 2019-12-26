using Intervals;
using Phonos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Latin
{
    public class WordParser
    {
        public string[] VOWELS = new[] { "a", "ā", "e", "ē", "i", "ī", "o", "ō", "u", "ū", "y", "ȳ" };

        public Dictionary<string, string[]> CLASSICAL_LATIN_MONOGRAMS = new Dictionary<string, string[]>()
        {
            { "a", new [] { Phonemes.a } },
            { "ā", new [] { Phonemes.LONG_a } },
            { "b", new [] { Phonemes.b } },
            { "c", new [] { Phonemes.k } },
            { "d", new [] { Phonemes.d } },
            { "e", new [] { Phonemes.e } },
            { "ē", new [] { Phonemes.LONG_e } },
            { "f", new [] { Phonemes.f } },
            { "g", new [] { Phonemes.g } },
            { "h", new [] { Phonemes.h } },
            { "i", new [] { Phonemes.i } },
            { "ī", new [] { Phonemes.LONG_i } },
            { "j", new [] { Phonemes.j } },
            { "k", new [] { Phonemes.k } },
            { "l", new [] { Phonemes.l } },
            { "m", new [] { Phonemes.m } },
            { "n", new [] { Phonemes.n } },
            { "o", new [] { Phonemes.o } },
            { "ō", new [] { Phonemes.LONG_o } },
            { "p", new [] { Phonemes.p } },
            { "r", new [] { Phonemes.r } },
            { "s", new [] { Phonemes.s } },
            { "t", new [] { Phonemes.t } },
            { "u", new [] { Phonemes.u } },
            { "ū", new [] { Phonemes.LONG_u } },
            { "v", new [] { Phonemes.w } },
            { "x", new [] { Phonemes.k, Phonemes.s } },
            { "y", new [] { Phonemes.y } },
            { "ȳ", new [] { Phonemes.LONG_y } },
            { "z", new [] { Phonemes.z } },
        };

        public Dictionary<string, string[]> CLASSICAL_LATIN_BIGRAMS = new Dictionary<string, string[]>()
        {
            { "ae", new [] { Phonemes.a_i } },
            { "au", new [] { Phonemes.a_u } },
            { "ei", new [] { Phonemes.e_i } },
            { "eu", new [] { Phonemes.e_u } },
            { "oe", new [] { Phonemes.o_i } },
            { "ui", new [] { Phonemes.u_i } },
            { "yi", new [] { Phonemes.y_i } },
            { "ch", new [] { Phonemes.kh } },
            { "gn", new [] { Phonemes.ng, Phonemes.n } },  // /!\ mots grecs
            { "gu", new [] { Phonemes.gw } },  // (!) si pas de voyelle après
            { "ph", new [] { Phonemes.ph } },
            { "qu", new [] { Phonemes.kw } },
            { "rh", new [] { Phonemes.r } },
            { "th", new [] { Phonemes.th } },
        };

        public Word Parse(string word)
        {
            var l = 0;
            var allPhonemes = new List<string>();
            var graphemes = new List<Interval<string>>();

            int length = word.Length;
            for (int i = 0; i < length; i++)
            {
                string monogram = word[i].ToString();
                string bigram = i < length - 1 ? word.Substring(i, 2) : string.Empty;
                string trigram = i < length - 2 ? word.Substring(i, 3) : string.Empty;

                string[] phonemes;

                if (trigram.StartsWith("qu") && !VOWELS.Contains(trigram.Substring(2, 1)))
                {
                    phonemes = new[] { Phonemes.k, Phonemes.u };
                    graphemes.Add(new Interval<string>(l, phonemes.Length, bigram));
                    l += phonemes.Length;
                    allPhonemes.AddRange(phonemes);
                    i++;
                }
                if (trigram.StartsWith("gu") && !VOWELS.Contains(trigram.Substring(2, 1)))
                {
                    phonemes = new[] { Phonemes.g, Phonemes.u };
                    graphemes.Add(new Interval<string>(l, phonemes.Length, bigram));
                    l += phonemes.Length;
                    allPhonemes.AddRange(phonemes);
                    i++;
                }
                else if (CLASSICAL_LATIN_BIGRAMS.TryGetValue(bigram, out phonemes))
                {
                    graphemes.Add(new Interval<string>(l, phonemes.Length, bigram));
                    l += phonemes.Length;
                    allPhonemes.AddRange(phonemes);
                    i++;
                }
                else if (monogram == "j")
                {
                    if (i > 0 && i < length - 1 && IsVowel(word[i - 1]) && IsVowel(word[i + 1]))
                        phonemes = new[] { Phonemes.j, Phonemes.j };
                    else
                        phonemes = new[] { Phonemes.j };

                    graphemes.Add(new Interval<string>(l, phonemes.Length, monogram));
                    l += phonemes.Length;
                    allPhonemes.AddRange(phonemes);
                }
                else if (CLASSICAL_LATIN_MONOGRAMS.TryGetValue(monogram, out phonemes))
                {
                    graphemes.Add(new Interval<string>(l, phonemes.Length, monogram));
                    l += phonemes.Length;
                    allPhonemes.AddRange(phonemes);
                }
                else
                    throw new Exception($"Unsupported character: `{monogram}`.");
            }

            var normalizedGraphemes = graphemes.Map((string g) => NormalizeDiacritics(g));

            return new Word(
                phonemes: allPhonemes.ToArray(),
                graphicalForms: new[] {
                    new Core.Alignment<string>(normalizedGraphemes.ToArray()),
                });
        }

        private bool IsVowel(char v)
        {
            return VOWELS.Contains(v.ToString());
        }

        private Dictionary<char, char> DIACRITICS =
            new Dictionary<char, char>()
            {
                { 'ā', 'a' }, { 'ã', 'a' }, { 'â', 'a' }, { 'ǎ', 'a' },
                { 'ē', 'e' }, { 'ẽ', 'e' }, { 'ê', 'e' }, { 'ě', 'e' },
                { 'ī', 'i' }, { 'ĩ', 'i' }, { 'î', 'i' }, { 'ǐ', 'i' },
                { 'ō', 'o' }, { 'õ', 'o' }, { 'ô', 'o' }, { 'ǒ', 'o' },
                { 'ū', 'u' }, { 'ũ', 'u' }, { 'û', 'u' }, { 'ǔ', 'u' },
                { 'ȳ', 'y' }, { 'ỹ', 'y' }, { 'ŷ', 'y' },
            };
        private string NormalizeDiacritics(string s)
        {
            var clean = s
                .Replace("\u0304", "")  // macron
                .Replace("\u007E", "")  // tilde
                .Replace("\u0302", "")  // circumflex
                .Replace("\u030C", ""); // reversed circumflex

            var builder = new StringBuilder();

            foreach (char c in clean)
            {
                if (DIACRITICS.TryGetValue(c, out var c2))
                    builder.Append(c2);
                else
                    builder.Append(c);
            }

            return builder.ToString();
        }
    }

    public struct LatinWord
    {
        public string Word { get; }
        public string[] Phonemes { get; }
        public Interval<string[]>[] GraphicalForm { get; }

        public LatinWord(string word, string[] phonemes, Interval<string[]>[] graphicalForm)
        {
            Word = word;
            Phonemes = phonemes;
            GraphicalForm = graphicalForm;
        }
    }
}
