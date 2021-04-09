using Phonos.Fra.Similarity.Lexicon;
using Phonos.Fra.Similarity.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.Fra.Similarity.Distances.Tests
{
    public class PhoneticDistanceTests : PhoneticDistanceTestsBase
    {
        protected override IDistance<Realization> _distance =>
            new PhoneticDistance(new SyllableDistance(PhonemeDistances.NeighborhoodVowelDistance, PhonemeDistances.NeighborhoodConsonantDistance));
    }

    public abstract class PhoneticDistanceTestsBase : DistanceTests<Realization>
    {
        protected override IEnumerable<Realization> _points => Realizations();

        private IEnumerable<Realization> Realizations()
        {
            yield return new Realization(new[] { Syllable.Null });

            var syllables = AllSyllables().GetEnumerator();
            var q1 = new Queue<Syllable>();
            var q2 = new Queue<Syllable>();
            var q3 = new Queue<Syllable>();

            while (syllables.MoveNext())
            {
                if (q1.Count == 1)
                    q1.Dequeue();

                if (q2.Count == 2)
                    q2.Dequeue();

                if (q3.Count == 3)
                    q3.Dequeue();

                var syllable = syllables.Current;

                q1.Enqueue(syllable);
                q2.Enqueue(syllable);
                q3.Enqueue(syllable);

                yield return new Realization(q1.ToArray());

                if (q2.Count == 2)
                    yield return new Realization(q2.ToArray());

                if (q2.Count == 3)
                    yield return new Realization(q3.ToArray());
            }
        }

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
