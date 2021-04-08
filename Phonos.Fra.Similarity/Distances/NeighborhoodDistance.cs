using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Fra.Similarity.Distances
{
    public class NeighborhoodDistance<T> : IDistance<T>
    {
        protected Dictionary<T, Neighborhood<T>> _neighborhoods { get; }
        protected double[] _circleDistances { get; }
        protected double _maxDistances { get; }

        public NeighborhoodDistance(Dictionary<T, Neighborhood<T>> neighborhoods, double[] circleDistances, double maxDistances)
        {
            _neighborhoods = neighborhoods;
            _circleDistances = circleDistances;
            _maxDistances = maxDistances;
        }

        public double GetDistance(T fst, T snd)
        {
            if (fst.Equals(snd))
                return 0;

            var neighborhood = _neighborhoods[fst];
            if (neighborhood.TryGetCircle(snd, out int circleIndex))
                return _circleDistances[circleIndex];
            else
                return _maxDistances;
        }
    }

    public class Neighborhood<T>
    {
        public T Center { get; }
        public T[][] Circles { get; }

        public Neighborhood(T center, T[][] circles)
        {
            Center = center;
            Circles = circles;
        }

        public bool TryGetCircle(T symbol, out int index)
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
