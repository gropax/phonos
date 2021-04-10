using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Fra.Similarity.Lexicon
{
    public class RealizationComputer
    {
        private Syllabifier _syllabifier = new Syllabifier();

        public Realization[] Compute(LexiqueEntry entry)
        {
            var realizations = new List<Realization>();

            var phonemes = entry.Phonemes.Select(cp => cp.Phoneme).ToArray();

            var syllables = _syllabifier.Compute(phonemes).ToArray();
            realizations.Add(new Realization(entry.WordForm, syllables));

            if (entry.POS == "NOM" || entry.POS == "ADJ")
            {
                Phoneme[] sequence;
                string graphic;

                if (entry.Number == "s")
                {
                    if (phonemes[0].IsVowel)
                    {
                        sequence = new[] { Phonemes.l }.Concat(phonemes).ToArray();
                        graphic = "l'" + entry.WordForm;
                    }
                    else if (entry.Gender == "m")
                    {
                        sequence = new[] { Phonemes.l, Phonemes._e }.Concat(phonemes).ToArray();
                        graphic = "le " + entry.WordForm;
                    }
                    else
                    {
                        sequence = new[] { Phonemes.l, Phonemes.a }.Concat(phonemes).ToArray();
                        graphic = "la " + entry.WordForm;
                    }
                }
                else
                {
                    graphic = "les " + entry.WordForm;

                    if (phonemes[0].IsVowel)
                        sequence = new[] { Phonemes.l, Phonemes.e, Phonemes.z }.Concat(phonemes).ToArray();
                    else
                        sequence = new[] { Phonemes.l, Phonemes.e }.Concat(phonemes).ToArray();
                }

                var syllables2 = _syllabifier.Compute(sequence).ToArray();
                realizations.Add(new Realization(graphic, syllables2));
            }

            return realizations.ToArray();
        }  
    }
}
