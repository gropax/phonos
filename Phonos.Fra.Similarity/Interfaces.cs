using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Fra.Similarity
{
    public interface IWordRealizer
    {
        Realization[] GetRealizations(WordForm wordForm);
    }

    public interface IDistance<T>
    {
        double GetDistance(T fst, T snd);
    }
}
