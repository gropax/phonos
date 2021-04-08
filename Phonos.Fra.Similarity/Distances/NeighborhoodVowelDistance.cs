using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Fra.Similarity.Distances
{
    public class NeighborhoodVowelDistance : IDistance<Phoneme>
    {
        private Dictionary<string, Neighborhood> _neighborhoods = Neighborhoods(
            Of("i", C(), C(), C("e", "y"), C("ɛ", "ø", "ə")),
            Of("e", C(), C("ɛ"), C("i", "ø", "ə", "œ"), C("a", "y")),
            Of("ɛ", C(), C("e", "ə"), C("œ", "ø", "a"), C("i", "ɛ̃", "ɑ", "ɔ")),
            Of("a", C("ɑ"), C("ɔ"), C("ɛ", "ə"), C("e", "ø", "œ", "o")),
            Of("y", C(), C(), C("i", "ø"), C("œ", "e", "ə")),
            Of("ø", C("ə"), C("œ"), C("y", "e", "ɛ", "ɔ"), C("i", "a", "o", "ɑ")),
            Of("œ", C("ə"), C("ø"), C("ɛ", "e", "ɔ"), C("y", "œ̃", "a", "o", "ɑ")),
            Of("ə", C("ø", "œ"), C("ɛ", "ɔ"), C("e", "o", "a", "ɑ"), C("i", "y", "u")),
            Of("u", C(), C(), C("o"), C("ɔ", "ə")),
            Of("o", C(), C("ɔ"), C("u", "ə"), C("ɑ", "a", "ø", "œ")),
            Of("ɔ", C(), C("o", "ɑ", "a", "ə"), C("ø", "œ"), C("u", "ɔ̃", "ɛ")),
            Of("ɑ", C("a"), C("ɔ"), C("ə"), C("o", "ɑ̃", "ɛ", "ø", "œ")),
            Of("ɛ̃", C("œ̃"), C(), C(), C("ɛ")),
            Of("ɑ̃", C(), C("ɔ̃"), C(), C("ɑ")),
            Of("œ̃", C("ɛ̃"), C(), C(), C("œ")),
            Of("ɔ̃", C(), C("ɑ̃"), C(), C("ɔ")));

        private static Dictionary<string, Neighborhood> Neighborhoods(params Neighborhood[] neighborhoods) => neighborhoods.ToDictionary(n => n.Center);
        private static Neighborhood Of(string center, params string[][] circles) => new Neighborhood(center, circles);
        private static string[] C(params string[] symbols) => symbols;

        private double[] _circleDistances = new double[] { 1, 2, 3, 4 };
        private double _maxDistances = 5;

        public double GetDistance(Phoneme fst, Phoneme snd)
        {
            if (fst == snd)
                return 0;

            var neighborhood = _neighborhoods[fst.Symbol];
            if (neighborhood.TryGetCircle(snd.Symbol, out int circleIndex))
                return _circleDistances[circleIndex];
            else
                return _maxDistances;
        }
    }

    public class Neighborhood
    {
        public string Center { get; }
        public string[][] Circles { get; }

        public Neighborhood(string center, string[][] circles)
        {
            Center = center;
            Circles = circles;
        }

        public bool TryGetCircle(string symbol, out int index)
        {
            for (int i = 0; i < Circles.Length; i++)
            {
                var circle = Circles[i];
                if (circle.Contains(symbol))
                {
                    index = i;
                    return true;
                }
            }

            index = -1;
            return false;
        }
    }
}
