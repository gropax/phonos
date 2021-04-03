using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Fra.Similarity.Distances
{
    public class SyllableDistance : ISyllableDistance
    {
        private IPhonemeDistance _vowelDistance { get; }
        private IPhonemeDistance _consonantDistance { get; }

        public SyllableDistance(IPhonemeDistance vowelDistance, IPhonemeDistance consonantDistance)
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
