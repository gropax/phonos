﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Phonos.Core.RuleBuilder
{
    public static class P
    {
        //public static Action<PhonologicalMapBuilder> Erase =>
        //    pb => pb.Phono(px => new string[0]).Rewrite(gx => string.Empty);
        //public static Action<PhonologicalMapBuilder> Consonify =>
        //    pb => pb.Phono(ConsonifyFunc);
        //public static Action<PhonologicalMapBuilder> Shorten =>
        //    pb => pb.Phono(ShortenFunc);

        public static string[] Erase(string[] phonemes)
        {
            return new string[0];
        }

        public static string[] Nothing(string[] phonemes)
        {
            return phonemes;
        }

        public static string[] Degeminate(string[] phonemes)
        {
            throw new NotImplementedException();
        }

        public static string[] Shorten(string[] phonemes)
        {
            return phonemes.Select(p => p.Replace("ː", string.Empty)).ToArray();
        }

        static Dictionary<string, string> CONSONIFICATION =
            new Dictionary<string, string>() { { "i", "j" }, { "e", "j" }, { "u", "w" } };
        public static string[] Consonify(string[] phonemes)
        {
            if (phonemes.Length > 1)
                throw new RewritingError("Can only consonify a single phoneme.");
            if (!CONSONIFICATION.TryGetValue(phonemes[0], out var glide))
                throw new RewritingError($"Can't consonify phoneme [{phonemes[0]}].");
            else
                return new[] { glide };
        }

        public static string[] NasalizeDiphtongue(string[] phonemes)
        {
            string d = phonemes[0];
            return new[]
            {
                d.Substring(0, 1) + "\u0303" + d.Substring(1),
            };
        }

        public static string[] NasalizeDiphtongue2(string[] phonemes)
        {
            string d = phonemes[0];
            return new[]
            {
                d.Substring(0, d.Length - 1) + "\u0303" + d.Last().ToString(),
            };
        }

        public static Dictionary<string, string> UNVOICE = new Dictionary<string, string>()
        {
            { "g", "k" }, { "b", "p" }, { "v", "f" },
        };
        public static string[] Unvoice(string[] phonemes)
        {
            return phonemes.Select(p => UNVOICE[p]).ToArray();
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
