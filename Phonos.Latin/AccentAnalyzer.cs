using Intervals;
using Phonos.Core;
using Phonos.Core.Analyzers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Latin
{
    public static class Accent
    {
        public const string TONIC = "tonic";
        public const string INITIAL = "initial";
        public const string FINAL = "final";
        public const string PRETONIC = "pre-tonic";
        public const string POSTTONIC = "post-tonic";
    }

    public class AccentAnalyzer : IAnalyzer
    {
        public void Analyze(Word word)
        {
            var syllables = word.GetField("syllable").Intervals
                .Reverse().ToArray();
            var accents = ComputeAccent(syllables);
            word.SetField("accent", new Core.Alignment<string>(accents));
        }

        private Interval<string>[] ComputeAccent(Interval<string>[] syllables)
        {
            if (syllables.Length == 1)
                return syllables.Map((string _) => "tonic").ToArray();

            var accents = new List<Interval<string>>();

            int distanceToAccent = 0;
            int syllableNumber = syllables.Length;
            int syllableNo = 0;

            foreach (var syllable in syllables)
            {
                syllableNo++;

                if (distanceToAccent == 0)
                {
                    var val = syllableNo == 1 ? "final"
                        : syllableNo == syllables.Length ? "initial"
                        : "pre-tonic";
                    accents.Add(syllable.Map(_ => val));
                    distanceToAccent++;
                }
                else if (distanceToAccent == 1)
                {
                    if (syllable.Value == "long" || syllableNo == syllableNumber)
                    {
                        accents.Add(syllable.Map(_ => "tonic"));
                        distanceToAccent = 0;
                    }
                    else
                    {
                        var val = syllableNo == 2 ? "post-tonic" : "pre-tonic";
                        accents.Add(syllable.Map(_ => val));
                        distanceToAccent++;
                    }
                }
                else
                {
                    accents.Add(syllable.Map(_ => "tonic"));
                    distanceToAccent = 0;
                }
            }

            accents.Reverse();
            return accents.ToArray();
        }
    }
}
