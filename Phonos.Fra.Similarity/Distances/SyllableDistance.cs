using System;
using System.Collections.Generic;
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

        public double GetDistance(Syllable fst, Syllable snd)
        {
            throw new NotImplementedException();
        }
    }
}
