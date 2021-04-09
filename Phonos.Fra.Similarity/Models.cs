using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Phonos.Fra.Similarity
{
    public class Lemma
    {
        public string Representative { get; }
    }

    public class WordForm
    {
        public string GraphicForm { get; }
        public Syllable[] Syllables { get; }
        public IEnumerable<Phoneme> Phonemes => Syllables.SelectMany(p => p.Phonemes);
    }

    public class Realization
    {
        /// <summary>
        /// Découpage du mot phonétique en syllabes.
        /// </summary>
        public Syllable[] Syllables { get; }

        /// <summary>
        /// Score quantifiant la déformation du mot-forme ayant abouti à la réalisation.
        /// </summary>
        public double Distortion { get; }
    }

    public class Syllable
    {
        public Phoneme[] Onset { get; }
        public Phoneme Nucleus { get; }
        public Phoneme[] Coda { get; }
        public IEnumerable<Phoneme> Phonemes => Onset.Append(Nucleus).Concat(Coda);

        public Syllable(Phoneme[] onset, Phoneme nucleus, Phoneme[] coda)
        {
            Onset = onset;
            Nucleus = nucleus;
            Coda = coda;
        }

        public override string ToString()
        {
            var symbols = string.Join("", Phonemes.Select(p => p.Symbol));
            return $"/{symbols}/";
        }
    }

}
