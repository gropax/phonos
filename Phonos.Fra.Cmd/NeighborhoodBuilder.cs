using Phonos.Fra.Similarity;
using Phonos.Fra.Similarity.Distances;
using Phonos.Fra.Similarity.Lexicon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Fra.Cmd
{
    public class NeighborhoodBuilder
    {
        private WordForm[] _nouns;
        private ILookup<string, WordForm> _nounsByForm;

        private static RealizationComputer _realizationComputer = new RealizationComputer();
        private static IDistance<WordForm> _distance = Distances.WordFormDistance;

        public NeighborhoodBuilder(WordForm[] nouns, ILookup<string, WordForm> nounsByForm)
        {
            _nouns = nouns;
            _nounsByForm = nounsByForm;
        }

        public static NeighborhoodBuilder Init(string lexiquePath)
        {
            var nouns = LexiqueParser.ParseLexique(lexiquePath)
                .Where(e => e.POS == "NOM")
                .Select(e => new WordForm(e.WordForm, new[] { _realizationComputer.Compute(e).Single() }))
                .ToArray();

            var nounsByForm = nouns.ToLookup(e => e.GraphicForm);

            return new NeighborhoodBuilder(nouns, nounsByForm);
        }

        public Tuple<double, double, WordForm>[] GetNeighbors(string form, int take = 10)
        {
            var wordForm = _nounsByForm[form].First();

            var scoredNeighbors = _nouns
                .Select(wf =>
                {
                    double distance = _distance.GetDistance(wordForm, wf);
                    double similarity = 1 / (1 + distance);
                    return Tuple.Create(similarity, distance, wf);
                })
                .OrderByDescending(t => t.Item1)
                .Take(take);

            return scoredNeighbors.ToArray();
        }
    }


}
