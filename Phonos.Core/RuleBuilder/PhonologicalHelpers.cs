using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.RuleBuilder
{
    public static class P
    {
        public static Action<PhonologicalMapBuilder> Erase =>
            pb => pb.Phono(px => new string[0]).Rewrite(gx => new string[0]);

        public static string[] Degeminate(string[] phonemes)
        {
            throw new NotImplementedException();
        }
    }
}
