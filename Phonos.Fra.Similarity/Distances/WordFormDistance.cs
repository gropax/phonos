using Phonos.Fra.Similarity.Lexicon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Fra.Similarity.Distances
{
    public class WordFormDistance : IDistance<WordForm>
    {
        private IDistance<Realization> _realizationDistance;

        public WordFormDistance(IDistance<Realization> realizationDistance)
        {
            _realizationDistance = realizationDistance;
        }

        public double GetDistance(WordForm fst, WordForm snd)
        {
            int length = Math.Max(fst.Realizations.Length, snd.Realizations.Length);
            if (length == 0)
                return 0;

            var distances = 
                from s1 in fst.Realizations
                from s2 in snd.Realizations
                select _realizationDistance.GetDistance(s1, s2);

            return distances.Min(d => d);
        }
    }
}
