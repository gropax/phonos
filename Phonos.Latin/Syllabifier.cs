using Intervals;
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


    public class Syllabifier
    {
        public enum SyllabicPosition
        {
            ONSET,
            NUCLEUS,
            CODA,
        }

        public Syllable[] GetSyllables(Phoneme[] phonemicWord)
        {
            var syllablesPhonemes = new List<Phoneme[]>();
            var syllablePhonemes = new List<Phoneme>();

            var lastPosition = SyllabicPosition.CODA;

            Phoneme current = null;

            for (int i = 0; i < phonemicWord.Length; i++)
            {
                current = phonemicWord[i];

                if (i == 0)
                {
                    if (current.Type == PhonemeType.VOCALIC)
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
                        if (current.Type == PhonemeType.VOCALIC && last.Type == PhonemeType.VOCALIC)
                        {
                            syllablesPhonemes.Add(syllablePhonemes.ToArray());
                            syllablePhonemes.Clear();
                        }
                        syllablePhonemes.Add(current);
                        syllablesPhonemes.Add(syllablePhonemes.ToArray());
                    }
                    else if (current.Type == PhonemeType.VOCALIC)
                    {
                        if (last.Type == PhonemeType.VOCALIC)
                        {
                            syllablesPhonemes.Add(syllablePhonemes.ToArray());
                            syllablePhonemes.Clear();
                        }

                        syllablePhonemes.Add(current);
                        lastPosition = SyllabicPosition.NUCLEUS;
                    }
                    else
                    {
                        if (last.Type == PhonemeType.VOCALIC)
                        {
                            if (next.Type == PhonemeType.VOCALIC)
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
                            if (next.Type == PhonemeType.VOCALIC)
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
                var nucleus = phonemes.Where(p => p.Type == PhonemeType.VOCALIC).First();
                var hasCoda = phonemes.Last().Type != PhonemeType.VOCALIC;

                if (distanceToAccent == 0)
                {
                    isAccentuated = false;
                    distanceToAccent++;
                }
                else if (distanceToAccent == 1)
                {
                    if (nucleus.Quantity == PhonemeQuantity.LONG || hasCoda || syllableNo == syllableNumber)
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
    }
}
