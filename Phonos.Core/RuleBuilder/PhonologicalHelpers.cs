using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Phonos.Core.RuleBuilder
{
    public static class P
    {
        public static Action<PhonologicalMapBuilder> Erase =>
            pb => pb.Phono(px => new string[0]).Rewrite(gx => string.Empty);
        public static Action<PhonologicalMapBuilder> Consonify =>
            pb => pb.Phono(ConsonifyFunc);

        public static string[] Degeminate(string[] phonemes)
        {
            throw new NotImplementedException();
        }


        static Dictionary<string, string> CONSONIFICATION =
            new Dictionary<string, string>() { { "i", "j" }, { "e", "j" }, { "u", "w" } };
        public static string[] ConsonifyFunc(string[] phonemes)
        {
            if (phonemes.Length > 1)
                throw new RewritingError("Can only consonify a single phoneme.");
            if (!CONSONIFICATION.TryGetValue(phonemes[0], out var glide))
                throw new RewritingError($"Can't consonify phoneme [{phonemes[0]}].");
            else
                return new[] { glide };
        }
    }

    [Serializable]
    class RewritingError : Exception
    {
        public RewritingError(string message) : base(message)
        {
        }
    }
}
