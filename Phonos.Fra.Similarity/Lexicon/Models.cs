using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Phonos.Fra.Similarity.Lexicon
{
    public class Lemma
    {
        public string Representative { get; }
    }

    public class WordForm
    {
        public string GraphicForm { get; }
        public Realization[] Realizations { get; }

        public WordForm(string graphicForm, Realization[] realizations)
        {
            GraphicForm = graphicForm;
            Realizations = realizations;
        }

        public override string ToString()
        {
            return GraphicForm;
        }
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

        public Realization(Syllable[] syllables, double distortion = 0)
        {
            Syllables = syllables;
            Distortion = distortion;
        }

        public override string ToString()
        {
            var symbols = string.Join(".", Syllables.Select(s => string.Join("", s.Phonemes.Select(p => p.Symbol))));
            return $"/{symbols}/";
        }
    }

    public class Syllable
    {
        public static Syllable Null { get; } = new Syllable(new Phoneme[0], Lexicon.Phonemes._, new Phoneme[0]);

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

        public override bool Equals(object obj)
        {
            return obj is Syllable syllable &&
                   Enumerable.SequenceEqual(Onset, syllable.Onset) &&
                   EqualityComparer<Phoneme>.Default.Equals(Nucleus, syllable.Nucleus) &&
                   Enumerable.SequenceEqual(Coda, syllable.Coda);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Onset, Nucleus, Coda, Phonemes);
        }
    }

}
