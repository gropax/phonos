using Phonos.Fra.Similarity.Lexicon;
using Phonos.Fra.Similarity.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.Fra.Similarity.Distances.Tests
{
    public class SyllableDistanceTests : SyllableDistanceTestsBase
    {
        protected override IDistance<Syllable> _distance =>
            new SyllableDistance(Distances.NeighborhoodVowelDistance, Distances.NeighborhoodConsonantDistance);
    }

    public abstract class SyllableDistanceTestsBase : DistanceTests<Syllable>
    {
        protected override IEnumerable<Syllable> _points => AllSyllables();

        private IEnumerable<Syllable> AllSyllables()
        {
            return
                from onset in AllOnsets()
                from nucleus in Phonemes.Vowels
                from coda in AllCoda()
                select new Syllable(onset, nucleus, coda);
        }

        private IEnumerable<Phoneme[]> AllOnsets()
        {
            yield return new Phoneme[0];

            foreach (var c in Phonemes.Consonants)
                yield return new[] { c };

            var consonants = Phonemes.Consonants.ToList();
            Shuffle(consonants);

            foreach ((var c1, var c2) in consonants.Pairs())
            {
                yield return new[] { c1, c2 };
                yield return new[] { Phonemes.s, c1, c2 };
            }
        }

        private IEnumerable<Phoneme[]> AllCoda()
        {
            yield return new Phoneme[0];

            foreach (var c in Phonemes.Consonants)
                yield return new[] { c };

            var consonants = Phonemes.Consonants.ToList();
            Shuffle(consonants);

            foreach ((var c1, var c2) in consonants.Pairs())
            {
                yield return new[] { c1, c2 };

                var final = Phonemes.ContinuousConsonants[rng.Next(Phonemes.ContinuousConsonants.Length)];
                yield return new[] { c1, c2, final };
            }
        }

        private Random rng = new Random();  
        public void Shuffle<T>(IList<T> list)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = rng.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }
    }
}
