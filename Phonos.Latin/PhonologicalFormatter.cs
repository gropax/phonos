using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Latin
{
    public class PhonologicalFormatter
    {
        public const string DEFAULT_SYLLABLE_SEPARATOR = ".";
        public const string DEFAULT_PRIMARY_STRESS_SEPARATOR = "ˈ";
        public const string DEFAULT_SECONDARY_STRESS_SEPARATOR = "ˌ";

        public string SyllableSeparator { get; private set; }
        public string PrimaryStressSymbol { get; private set; }
        public string SecondaryStressSymbol { get; private set; }

        public PhonologicalFormatter(string syllableSeparator = DEFAULT_SYLLABLE_SEPARATOR,
            string primaryStressSymbol = DEFAULT_PRIMARY_STRESS_SEPARATOR,
            string secondaryStressSymbol = DEFAULT_SECONDARY_STRESS_SEPARATOR)
        {
            SyllableSeparator = syllableSeparator;
            PrimaryStressSymbol = primaryStressSymbol;
            SecondaryStressSymbol = secondaryStressSymbol;
        }

        public string Format(IEnumerable<Phoneme> phonemes)
        {
            return string.Join("", phonemes.Select(p => p.Quality));
        }

        //public string Format(Phoneme[] phonemes, Syllable[] syllables)
        //{
        //    return string.Join(SyllableSeparator,
        //        syllables.Select(s => FormatStress(s.Stress) + Format(s.Slice(phonemes))));
        //}

        public string FormatStress(Stress stress)
        {
            switch (stress)
            {
                case Stress.PRIMARY_STRESS:
                    return PrimaryStressSymbol;
                case Stress.SECONDARY_STRESS:
                    return SecondaryStressSymbol;
                default:
                    return string.Empty;
            }
        }
    }
}
