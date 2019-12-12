using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.French.SubSystems;
using System;
using System.Linq;

namespace Phonos.French
{
    public static class French
    {
        public static RuleContext[] Rules()
        {
            return Chapter6.Rules()
                .Concat(Chapter8.Rules())
                .ToArray();
        }
    }
}
