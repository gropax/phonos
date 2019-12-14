using Intervals;
using Phonos.Core;
using Phonos.Core.Analyzers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Latin
{
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
            word.SetField("syllable", new Core.Alignment<string>(syllables));
        }

        public IEnumerable<Interval<string>> GetSyllables(string[] phonemicWord)
        {
            var start = 0;
            var syllablePhonemes = new List<string>();

            var lastPosition = SyllabicPosition.CODA;

            string current = null;

            for (int i = 0; i < phonemicWord.Length; i++)
            {
                current = phonemicWord[i];

                if (i == 0)
                {
                    if (IPA.IsVowel(current))
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
                        if (IPA.IsVowel(current) && IPA.IsVowel(last))
                        {
                            yield return new Interval<string>(start,
                                syllablePhonemes.Count,
                                IPA.IsLong(current) ? "long" : "short");
                            start += syllablePhonemes.Count;
                            syllablePhonemes.Clear();
                        }
                        syllablePhonemes.Add(current);
                        yield return new Interval<string>(start,
                            syllablePhonemes.Count,
                            !IPA.IsVowel(current) || IPA.IsLong(current) ? "long" : "short");
                    }
                    else if (IPA.IsVowel(current))
                    {
                        if (IPA.IsVowel(last))
                        {
                            yield return new Interval<string>(start,
                                syllablePhonemes.Count,
                                IPA.IsLong(last) ? "long" : "short");
                            start += syllablePhonemes.Count;
                            syllablePhonemes.Clear();
                        }

                        syllablePhonemes.Add(current);
                        lastPosition = SyllabicPosition.NUCLEUS;
                    }
                    else
                    {
                        if (IPA.IsVowel(last))
                        {
                            if (IPA.IsVowel(next))
                            {
                                yield return new Interval<string>(start,
                                    syllablePhonemes.Count,
                                    IPA.IsLong(last) ? "long" : "short");
                                start += syllablePhonemes.Count;
                                syllablePhonemes.Clear();

                                syllablePhonemes.Add(current);
                                lastPosition = SyllabicPosition.ONSET;
                            }
                            else if (IsOnsetCluster(current, next))
                            {
                                yield return new Interval<string>(start,
                                    syllablePhonemes.Count,
                                    IPA.IsLong(last) ? "long" : "short");
                                start += syllablePhonemes.Count;
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
                            if (IPA.IsVowel(next))
                            {
                                if (lastPosition == SyllabicPosition.CODA)
                                {
                                    yield return new Interval<string>(start,
                                        syllablePhonemes.Count,
                                        "long");
                                    start += syllablePhonemes.Count;
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
                                    yield return new Interval<string>(start,
                                        syllablePhonemes.Count,
                                        "long");
                                    start += syllablePhonemes.Count;
                                    syllablePhonemes.Clear();
                                    syllablePhonemes.Add(current);
                                    lastPosition = SyllabicPosition.ONSET;
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool IsOnsetCluster(string fst, string snd)
        {
            return (!IPA.IsVowel(fst) && !IPA.IsGlide(fst) && IPA.IsGlide(snd))
                || ((IPA.IsOcclusive(fst) || IPA.IsFricative(fst)) && IPA.IsLiquide(snd));
        }
    }
}
