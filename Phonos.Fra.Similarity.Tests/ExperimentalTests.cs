using Phonos.Fra.Similarity.Distances;
using Phonos.Fra.Similarity.Lexicon;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.Fra.Similarity.Tests
{
    public class ExperimentalTests
    {
        [Fact]
        public void TestSortBySimilarity()
        {
            string lexiquePath = @"C:\Users\VR1\source\repos\phonos\data\Lexique383.tsv";
            var nouns = LexiqueParser.ParseLexique(lexiquePath)
                .Where(e => e.POS == "NOM")
                .ToArray();

            var phoneticDistance = new PhoneticDistance(new SyllableDistance(Distances.Distances.NeighborhoodVowelDistance, Distances.Distances.NeighborhoodConsonantDistance));
            var realizationComputer = new RealizationComputer();
            var rng = new Random();  

            var reference = realizationComputer.Compute(nouns[rng.Next(nouns.Length)]).Single();

            var res = new List<Tuple<double, Realization>>();

            foreach (var noun in nouns)
            {
                var realization = realizationComputer.Compute(noun).Single();
                var distance = phoneticDistance.GetDistance(reference, realization);
                var similarity = 1 / (1 + distance);
                res.Add(Tuple.Create(similarity, realization));
            }

            var sorted = res.OrderByDescending(t => t.Item1).ToArray();
        }


        [Fact]
        public void TestComputeAllNounDistances()
        {
            string lexiquePath = @"C:\Users\VR1\source\repos\phonos\data\Lexique383.tsv";
            var nouns = LexiqueParser.ParseLexique(lexiquePath)
                .Where(e => e.POS == "NOM")
                .ToArray();

            var phoneticDistance = new PhoneticDistance(new SyllableDistance(Distances.Distances.NeighborhoodVowelDistance, Distances.Distances.NeighborhoodConsonantDistance));
            var realizationComputer = new RealizationComputer();

            var res = new List<Tuple<double, Realization, Realization>>();

            foreach ((var n1, var n2) in nouns.UnorderedPairs())
            {
                var r1 = realizationComputer.Compute(n1).Single();
                var r2 = realizationComputer.Compute(n2).Single();
                var distance = phoneticDistance.GetDistance(r1, r2);
                var similarity = 1 / (1 + distance);
                res.Add(Tuple.Create(similarity, r1, r2));
            }

            var sorted = res.OrderByDescending(t => t.Item1).ToArray();
        }
    }
}
