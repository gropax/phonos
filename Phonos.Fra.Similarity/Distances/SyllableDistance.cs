using Phonos.Fra.Similarity.Lexicon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Fra.Similarity.Distances
{
    public class SyllableDistance : IDistance<Syllable>
    {
        private IDistance<Phoneme> _vowelDistance { get; }
        private IDistance<Phoneme> _consonantDistance { get; }

        public SyllableDistance(IDistance<Phoneme> vowelDistance, IDistance<Phoneme> consonantDistance)
        {
            _vowelDistance = vowelDistance;
            _consonantDistance = consonantDistance;
        }

        private const double ONSET_WEIGHT = 1;
        private const double NUCLEUS_WEIGHT = 1;
        private const double CODA_WEIGHT = 1;

        public double GetDistance(Syllable fst, Syllable snd)
        {
            var onsetDistance = GetConsonantClusterDistance(fst.Onset, snd.Onset);
            var nucleusDistance = _vowelDistance.GetDistance(fst.Nucleus, snd.Nucleus);
            var codaDistance = GetConsonantClusterDistance(fst.Coda, snd.Coda);

            return ONSET_WEIGHT * onsetDistance + NUCLEUS_WEIGHT * nucleusDistance + CODA_WEIGHT * codaDistance;
        }

        public double GetConsonantClusterDistance(Phoneme[] fst, Phoneme[] snd)
        {
            int length = Math.Max(fst.Length, snd.Length);
            if (length == 0)
                return 0;

            var candidates = 
                from ps1 in fst.Pad(Phonemes._, length)
                from ps2 in snd.Pad(Phonemes._, length)
                select ps1.Zip(ps2, (p1, p2) => _consonantDistance.GetDistance(p1, p2)).Sum(d => d);

            return candidates.Min(d => d) / length;
        }
    }
}
