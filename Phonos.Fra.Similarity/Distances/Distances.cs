using Phonos.Fra.Similarity.Lexicon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Fra.Similarity.Distances
{
    public static class Distances
    {
        public static IDistance<WordForm> WordFormDistance => new WordFormDistance(RealizationDistance);

        public static IDistance<Realization> RealizationDistance => new PhoneticDistance(SyllableDistance);

        public static IDistance<Syllable> SyllableDistance => new SyllableDistance(NeighborhoodVowelDistance, NeighborhoodConsonantDistance);

        public static IDistance<Phoneme> NeighborhoodVowelDistance => new NeighborhoodDistance<Phoneme>(
            circleDistances: new double[] { 1, 2, 3, 4 },
            maxDistances: 5,
            neighborhoods: Neighborhoods(
                Of(""),
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
                Of("ɛ̃", C("œ̃"), C(), C(), C("ɛ", "ɑ̃", "ɔ̃")),
                Of("ɑ̃", C(), C("ɔ̃"), C(), C("ɑ", "ɛ̃", "œ̃")),
                Of("œ̃", C("ɛ̃"), C(), C("ɔ̃"), C("œ", "ɑ̃")),
                Of("ɔ̃", C(), C("ɑ̃"), C("œ̃"), C("ɔ", "ɛ̃"))));

        public static IDistance<Phoneme> NeighborhoodConsonantDistance => new NeighborhoodDistance<Phoneme>(
            circleDistances: new double[] { 1, 2, 3, 4 },
            maxDistances: 5,
            neighborhoods: Neighborhoods(
                Of(""),
                Of("p", C("b")),
                Of("t", C("d")),
                Of("k", C("g")),
                Of("b", C("p")),
                Of("d", C("t")),
                Of("g", C("k")),
                Of("f", C("v")),
                Of("s", C("z")),
                Of("ʃ", C("ʒ")),
                Of("v", C("f")),
                Of("z", C("s")),
                Of("ʒ", C("ʃ")),
                Of("m", C()),
                Of("n", C()),
                Of("ɲ", C()),
                Of("ŋ", C()),
                Of("l", C()),
                Of("ʁ", C("χ")),
                Of("χ", C("ʁ")),
                Of("j", C()),
                Of("w", C()),
                Of("ɥ", C())));

        private static Dictionary<Phoneme, Neighborhood<Phoneme>> Neighborhoods(params Neighborhood<Phoneme>[] neighborhoods) =>
            neighborhoods.ToDictionary(n => n.Center);
        private static Neighborhood<Phoneme> Of(string center, params string[][] circles) =>
            new Neighborhood<Phoneme>(
                Phonemes.BySymbol(center),
                circles.Select(ps => ps.Select(p => Phonemes.BySymbol(p)).ToArray()).ToArray());
        private static string[] C(params string[] symbols) => symbols;
    }
}
