using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Fra.Similarity.Distances
{
    /// <summary>
    /// Lexicographic order of phoneme symbols: abdefgijklmnopstuvwyzøŋœœ̃ɑɑ̃ɔɔ̃əɛɛ̃ɥɲʁʃʒ
    /// </summary>
    public class ConsonantDistance : IDistance<Phoneme>
    {
        private const double D0 = 1;
        private const double D1 = 2;
        private const double D2 = 3;
        private const double D3 = 4;

        private string[] _central1 = new[] { "ɛ", "ɔ" };
        private string[] _central2 = new[] { "e", "a", "o" };

        public double GetDistance(Phoneme fst, Phoneme snd)
        {
            return Math.Log10(1 + GetDistanceBase(fst, snd));
        }

        public double GetDistanceBase(Phoneme fst, Phoneme snd)
        {
            if (fst == snd)
                return 0;

            (var s1, var s2) = fst.Symbol.CompareTo(snd.Symbol) <= 0 ? (fst.Symbol, snd.Symbol) : (snd.Symbol, fst.Symbol);

            bool schwa1 = s1 == "ə";
            bool schwa2 = s2 == "ə";

            if ((s1 == "a" && s2 == "ɑ") ||
                (schwa1 && s2 == "ø") ||
                (schwa1 && s2 == "œ") ||
                (s1 == "ɛ̃" && s2 == "œ̃"))
                return D0;
            else if ((s1 == "ɑ" && s2 == "ɔ") ||
                (s1 == "ɑ̃" && s2 == "ɔ̃"))
                return D2;

            if (schwa1 || schwa2)
            {
                var other = schwa1 ? s2 : s1;
                if (_central1.Contains(other))
                    return D1;
                else if (_central2.Contains(other))
                    return D2;
            }

            double dist = 0;

            if (fst.IsTight != snd.IsTight)
                dist += D2;

            if (fst.IsRounded != snd.IsRounded)
                dist += D3;

            if (fst.IsHigh != snd.IsHigh)
                dist += D3;

            if (fst.IsLow != snd.IsLow)
                dist += D3;

            bool antDiff = fst.IsAnterior != snd.IsAnterior;
            bool postDiff = fst.IsPosterior != snd.IsPosterior;
            if (antDiff && postDiff)
                dist += D3;
            else if (antDiff || postDiff)
                dist += D1;

            if (fst.IsNasal != snd.IsNasal)
                dist += D3;

            return dist;
        }
    }
}
