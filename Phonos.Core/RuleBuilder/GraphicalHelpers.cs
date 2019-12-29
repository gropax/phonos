using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.RuleBuilder
{
    public static class G
    {
        public static string Erase(string graphemes)
        {
            return string.Empty;
        }

        public static Dictionary<string, string> UNVOICE = new Dictionary<string, string>()
        {
            { "g", "g" }, { "b", "p" }, { "v", "f" },
        };
        public static string Unvoice(string graphemes)
        {
            return UNVOICE[graphemes];
        }
    }
}
