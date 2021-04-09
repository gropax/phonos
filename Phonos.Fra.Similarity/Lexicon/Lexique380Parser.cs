using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phonos.Fra.Similarity.Lexicon
{
    public static class LexiqueParser
    {
        public static IEnumerable<LexiqueEntry> ParseLexique(string file)
        {
            using (StreamReader reader = new StreamReader(file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    yield return ParseEntry(line);
            }
        }

        public static LexiqueEntry ParseEntry(string line)
        {
            var parts = line.Split("\t");
            return new LexiqueEntry(
                wordForm: parts[0],
                lemma: parts[2],
                pos: parts[3],
                phonemes: ParsePhonemes(parts[1]));
        }

        public static ContextualPhoneme[] ParsePhonemes(string phonemeStr)
        {
            var phonemes = new List<ContextualPhoneme>();

            foreach (var c in phonemeStr.ToCharArray())
            {
                var phoneme = _phonemeByChar[c];
                bool isElidable = c == '°';
                phonemes.Add(new ContextualPhoneme(phoneme, isElidable));
            }

            return phonemes.ToArray();
        }

        private static Dictionary<char, Phoneme> _phonemeByChar = new Dictionary<char, Phoneme>()
        {
            { 'a', Phonemes.a }, { 'i', Phonemes.i }, { 'y', Phonemes.y }, { 'u', Phonemes.u },
            { 'o', Phonemes.o }, { 'O', Phonemes.O }, { 'e', Phonemes.e }, { 'E', Phonemes.E },
            { '°', Phonemes._e }, { '2', Phonemes.eu }, { '9', Phonemes.oe }, { '5', Phonemes.@in },
            { '1', Phonemes.un }, { '@', Phonemes.an }, { '§', Phonemes.on }, { '3', Phonemes._e },
            { 'j', Phonemes.j }, { '8', Phonemes.Y }, { 'w', Phonemes.w }, { 'p', Phonemes.p },
            { 'b', Phonemes.b }, { 't', Phonemes.t }, { 'd', Phonemes.d }, { 'k', Phonemes.k },
            { 'g', Phonemes.g }, { 'f', Phonemes.f }, { 'v', Phonemes.v }, { 's', Phonemes.s },
            { 'z', Phonemes.z }, { 'S', Phonemes.S }, { 'Z', Phonemes.Z }, { 'm', Phonemes.m },
            { 'n', Phonemes.n }, { 'N', Phonemes.nj }, { 'l', Phonemes.l }, { 'R', Phonemes.R },
            { 'x', Phonemes.X }, { 'G', Phonemes.ng },
        };
    }

    public class LexiqueEntry
    {
        public string WordForm { get; }
        public string Lemma { get; }
        public string POS { get; }
        public ContextualPhoneme[] Phonemes { get; }

        public LexiqueEntry(string wordForm, string lemma, string pos, ContextualPhoneme[] phonemes)
        {
            WordForm = wordForm;
            Lemma = lemma;
            POS = pos;
            Phonemes = phonemes;
        }

        public override bool Equals(object obj)
        {
            return obj is LexiqueEntry entry &&
                   WordForm == entry.WordForm &&
                   Lemma == entry.Lemma &&
                   POS == entry.POS &&
                   Enumerable.SequenceEqual(Phonemes, entry.Phonemes);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(WordForm, Lemma, POS, Phonemes);
        }
    }

    public class ContextualPhoneme
    {
        public Phoneme  Phoneme { get; }
        public bool IsElidable { get; }
        public bool IsLiaison { get; }

        public ContextualPhoneme(Phoneme phoneme, bool isElidable = false, bool isLiaison = false)
        {
            Phoneme = phoneme;
            IsElidable = isElidable;
            IsLiaison = isLiaison;
        }

        public override bool Equals(object obj)
        {
            return obj is ContextualPhoneme phoneme &&
                   EqualityComparer<Phoneme>.Default.Equals(Phoneme, phoneme.Phoneme) &&
                   IsElidable == phoneme.IsElidable &&
                   IsLiaison == phoneme.IsLiaison;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Phoneme, IsElidable, IsLiaison);
        }
    }
}
