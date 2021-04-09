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
            var phonemes = entry.Phonemes.Select(cp => cp.Phoneme);
            var syllables = _syllabifier.Compute(phonemes).ToArray();
            return new[] { new Realization(syllables) };
        }  
    }
}
