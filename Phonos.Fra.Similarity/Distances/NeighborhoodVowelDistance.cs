using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Fra.Similarity.Distances
{
    public class NeighborhoodVowelDistance : IDistance<Phoneme>
    {
        private Dictionary<string, Neighborhood> _neighborhoods = Neighborhoods(
            Of("i", Circle(), Circle(), Circle("e", "y"), Circle("ɛ", "ø", "ə")),
            Of("e", Circle(), Circle("ɛ"), Circle("i", "ø", "ə", "œ"), Circle("a", "y")),
            Of("ɛ", Circle(), Circle("e", "ə"), Circle("œ", "ø", "a"), Circle("i", "ɛ̃")),
            Of("a", Circle("ɑ"), Circle("ɔ"), Circle("ɛ", "ə"), Circle("e")),
            Of("y", Circle(), Circle(), Circle("i", "ø"), Circle("œ", "e", "ə")),
            Of("ø", Circle("ə"), Circle("œ"), Circle("y", "e", "ɛ"), Circle("i")),
            Of("œ", Circle("ə"), Circle("ø"), Circle("ɛ", "e"), Circle("y", "œ̃")),
            Of("ə", Circle("ø", "œ"), Circle("ɛ", "ɔ"), Circle("e", "o", "a", "ɑ"), Circle("i", "y", "u")),
            Of("u", Circle(), Circle(), Circle("o"), Circle("ɔ", "ə")),
            Of("o", Circle(), Circle("ɔ"), Circle("u", "ə"), Circle("ɑ")),
            Of("ɔ", Circle(), Circle("o", "ɑ", "a", "ə"), Circle(), Circle("u", "ɔ̃")),
            Of("ɑ", Circle("a"), Circle("ɔ"), Circle("ə"), Circle("o", "ɑ̃")),
            Of("ɛ̃", Circle("œ̃"), Circle(), Circle(), Circle("ɛ")),
            Of("ɑ̃", Circle(), Circle("ɔ̃"), Circle(), Circle("ɑ")),
            Of("œ̃", Circle("ɛ̃"), Circle(), Circle(), Circle("œ")),
            Of("ɔ̃", Circle(), Circle("ɑ̃"), Circle(), Circle("ɔ")));

        private static Dictionary<string, Neighborhood> Neighborhoods(params Neighborhood[] neighborhoods) => neighborhoods.ToDictionary(n => n.Center);
        private static Neighborhood Of(string center, params string[][] circles) => new Neighborhood(center, circles);
        private static string[] Circle(params string[] symbols) => symbols;

        private double[] _circleDistances = new double[] { 1, 2, 3, 4 };
        private double _maxDistances = 4;

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
