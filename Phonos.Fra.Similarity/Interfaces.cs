using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Fra.Similarity
{
    public interface IWordRealizer
    {
        Realization[] GetRealizations(WordForm wordForm);
    }

    public interface IPhoneticDistance
    {
        double GetDistance(Realization fst, Realization snd);
    }

    public interface ISyllableDistance
    {
        double GetDistance(Syllable fst, Syllable snd);
    }

    public interface IPhonemeDistance
    {
        double GetDistance(Phoneme fst, Phoneme snd);
    }

}
