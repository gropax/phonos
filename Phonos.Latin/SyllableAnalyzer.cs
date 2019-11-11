using Intervals;
using Phonos.Core;
using Phonos.Core.Analyzers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Latin
{
    public enum Stress
    {
        UNSTRESSED,
        PRIMARY_STRESS,
        SECONDARY_STRESS,
    }

    public static class Accent
    {
        public const string TONIC = "tonic";
        public const string INITIAL = "initial";
        public const string FINAL = "final";
        public const string PRETONIC = "pre-tonic";
        public const string POSTTONIC = "post-tonic";
    }

    public class Syllable : IInterval<Syllable>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public int End { get => Start + Length; }
        public Stress Stress { get; private set; }

        public Syllable(int start, int length, Stress stress)
        {
            Start = start;
            Length = length;
            Stress = stress;
        }

        public Interval<Syllable> ToInterval()
        {
            return new Interval<Syllable>(Start, Length, this);
        }

        Interval IInterval.ToInterval()
        {
            return new Interval(Start, Length);
        }
    }


    public class SyllableAnalyzer : IAnalyzer
    {
        public enum SyllabicPosition
        {
            ONSET,
            NUCLEUS,
            CODA,
        }

        public void Analyze(Word word)
        {
            var syllables = GetSyllables(word.Phonemes);

            var accents = new List<Interval<string>>();

            for (int i = 0; i < syllables.Length; i++)
            {
                var syllable = syllables[i];

                string accent;
                if (syllable.Stress == Stress.PRIMARY_STRESS || syllable.Stress == Stress.SECONDARY_STRESS)
                    accent = Accent.TONIC;
                else
                {
                    if (i == 0)
                        accent = Accent.INITIAL;
                    else if (i == syllables.Length - 1)
                        accent = Accent.FINAL;
                    else
                    {
                        var previous = syllables[i - 1];
                        if (previous.Stress == Stress.UNSTRESSED)
                            accent = Accent.PRETONIC;
                        else
                            accent = Accent.POSTTONIC;
                    }
                }

                accents.Add(new Interval<string>(syllable.ToInterval(), accent));
            }

            word.SetField("accent", new Core.Alignment<string>(accents));
        }

        public Syllable[] GetSyllables(string[] phonemicWord)
        {
            var syllablesPhonemes = new List<string[]>();
            var syllablePhonemes = new List<string>();

            var lastPosition = SyllabicPosition.CODA;

            string current = null;

            for (int i = 0; i < phonemicWord.Length; i++)
            {
                current = phonemicWord[i];

                if (i == 0)
                {
                    if (IsVocalic(current))
                    {
                        syllablePhonemes.Add(current);
                        lastPosition = SyllabicPosition.NUCLEUS;
                    }
                    else
                    {
                        syllablePhonemes.Add(current);
                        lastPosition = SyllabicPosition.ONSET;
                    }
                }
                else
                {
                    var last = phonemicWord[i - 1];
                    var next = i < phonemicWord.Length - 1 ? phonemicWord[i + 1] : null;

                    if (next == null)
                    {  // Last phoneme of the word
                        if (IsVocalic(current) && IsVocalic(last))
                        {
                            syllablesPhonemes.Add(syllablePhonemes.ToArray());
                            syllablePhonemes.Clear();
                        }
                        syllablePhonemes.Add(current);
                        syllablesPhonemes.Add(syllablePhonemes.ToArray());
                    }
                    else if (IsVocalic(current))
                    {
                        if (IsVocalic(last))
                        {
                            syllablesPhonemes.Add(syllablePhonemes.ToArray());
                            syllablePhonemes.Clear();
                        }

                        syllablePhonemes.Add(current);
                        lastPosition = SyllabicPosition.NUCLEUS;
                    }
                    else
                    {
                        if (IsVocalic(last))
                        {
                            if (IsVocalic(next))
                            {
                                syllablesPhonemes.Add(syllablePhonemes.ToArray());
                                syllablePhonemes.Clear();

                                syllablePhonemes.Add(current);
                                lastPosition = SyllabicPosition.ONSET;
                            }
                            else
                            {
                                syllablePhonemes.Add(current);
                                lastPosition = SyllabicPosition.CODA;
                            }
                        }
                        else
                        {
                            if (IsVocalic(next))
                            {
                                if (lastPosition == SyllabicPosition.CODA)
                                {
                                    syllablesPhonemes.Add(syllablePhonemes.ToArray());
                                    syllablePhonemes.Clear();
                                }

                                syllablePhonemes.Add(current);
                                lastPosition = SyllabicPosition.ONSET;
                            }
                            else
                            {
                                if (current == Phonemes.s)
                                {
                                    syllablePhonemes.Add(current);
                                    lastPosition = SyllabicPosition.CODA;
                                }
                                else
                                {
                                    syllablesPhonemes.Add(syllablePhonemes.ToArray());
                                    syllablePhonemes.Clear();
                                    syllablePhonemes.Add(current);
                                    lastPosition = SyllabicPosition.ONSET;
                                }
                            }
                        }
                    }
                }
            }

            var syllables = new List<Syllable>();

            int start = phonemicWord.Length;
            int distanceToAccent = 0;
            bool isAccentuated = false;
            bool isPrimary = true;

            syllablesPhonemes.Reverse();

            int syllableNumber = syllablesPhonemes.Count;
            int syllableNo = 0;

            foreach (var phonemes in syllablesPhonemes)
            {
                syllableNo++;
                start -= phonemes.Length;
                var nucleus = phonemes.Where(p => IsVocalic(p)).First();
                var hasCoda = !IsVocalic(phonemes.Last());

                if (distanceToAccent == 0)
                {
                    isAccentuated = false;
                    distanceToAccent++;
                }
                else if (distanceToAccent == 1)
                {
                    if (IsLong(nucleus) || hasCoda || syllableNo == syllableNumber)
                    {
                        isAccentuated = true;
                        distanceToAccent = 0;
                    }
                    else
                    {
                        isAccentuated = false;
                        distanceToAccent++;
                    }
                }
                else
                {
                    isAccentuated = true;
                    distanceToAccent = 0;
                }

                Stress accentuation;
                if (isAccentuated && isPrimary)
                {
                    accentuation = Stress.PRIMARY_STRESS;
                    isPrimary = false;
                }
                else if (isAccentuated)
                    accentuation = Stress.SECONDARY_STRESS;
                else
                    accentuation = Stress.UNSTRESSED;

                syllables.Add(new Syllable(start, phonemes.Length, accentuation));
            }

            syllables.Reverse();

            return syllables.ToArray();
        }

        private HashSet<char> VOWELS = new HashSet<char>(
            new[] { 'a', 'e', 'i', 'o', 'u', 'y' });
        private bool IsVocalic(string phoneme)
        {
            return VOWELS.Contains(phoneme[0]);
        }

        private HashSet<char> QUANTITY_MARKS = new HashSet<char>(
            new[] { 'ː', '\u032f' });
        private bool IsLong(string phoneme)
        {
            return QUANTITY_MARKS.Contains(phoneme.Last());
        }
    }
}
