using Phonos.Fra.Similarity.Lexicon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Fra.Similarity.Distances
{
    public class PhoneticDistance : IDistance<Realization>
    {
        private IDistance<Syllable> _syllableDistance;

        public PhoneticDistance(IDistance<Syllable> syllableDistance)
        {
            _syllableDistance = syllableDistance;
        }

        public double GetDistance(Realization fst, Realization snd)
        {
            int length = Math.Max(fst.Syllables.Length, snd.Syllables.Length);
            if (length == 0)
                return 0;

            var candidates = 
                from ss1 in fst.Syllables.Pad(Syllable.Null, length)
                from ss2 in snd.Syllables.Pad(Syllable.Null, length)
                select ss1.Zip(ss2, (s1, s2) => _syllableDistance.GetDistance(s1, s2)).Sum(d => d);

            return candidates.Min(d => d) / length;
        }
    }
}
