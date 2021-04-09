using Phonos.Fra.Lexicon;
using System;
using System.Linq;

namespace Phonos.Fra.Similarity.Distances
{
    public class SyllabicDistance : IDistance<Realization>
    {
        private IDistance<Syllable> _syllableDistance;

        public SyllabicDistance(IDistance<Syllable> syllableDistance)
        {
            _syllableDistance = syllableDistance;
        }

        public double GetDistance(Realization fst, Realization snd)
        {
            if (fst.Syllables.Length != snd.Syllables.Length)
                return double.MaxValue;
            else if (fst.Syllables.Length == 0)
                return 0;
            else
                return fst.Syllables.Zip(snd.Syllables, (s1, s2) => _syllableDistance.GetDistance(s1, s2)).Sum();
        }
    }
}
