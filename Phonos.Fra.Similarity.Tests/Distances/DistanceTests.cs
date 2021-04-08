using Phonos.Fra.Similarity.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.Fra.Similarity.Distances.Tests
{
    public abstract class DistanceTests<T>
    {
        protected abstract IDistance<T> _distance { get; }
        protected abstract IEnumerable<T> _points { get; }

        [Fact]
        public void TestSeparation()
        {
            foreach ((var p1, var p2) in _points.UnorderedPairs())
            {
                if (p1.Equals(p2))
                    Assert.Equal(0, _distance.GetDistance(p1, p2));
                else
                    Assert.True(_distance.GetDistance(p1, p2) > 0);
            }
        }

        [Fact]
        public void TestSymetry()
        {
            foreach ((var p1, var p2) in _points.UnorderedPairs())
            {
                var d1 = _distance.GetDistance(p1, p2);
                var d2 = _distance.GetDistance(p2, p1);

                Assert.Equal(d1, d2);
            }
        }

        [Fact]
        public void TestTriangleInequality()
        {
            var triplets = _points.Triplets().ToArray();

            int total = triplets.Length;
            double count = 0;

            foreach ((var p1, var p2, var p3) in triplets)
            {
                var d1 = _distance.GetDistance(p1, p2);
                var d2 = _distance.GetDistance(p2, p3);
                var d3 = _distance.GetDistance(p1, p3);

                Assert.True(d3 <= d1 + d2, $"[{count/total:P2}]  d({p1}, {p3}) = {d3} ≰ {d1 + d2} = {d1} + {d2} = d({p1}, {p2}) + d({p2}, {p3})");

                count++;
            }
        }
    }
}
