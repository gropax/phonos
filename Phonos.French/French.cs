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
                .Concat(Part1Chapter8.Rules())
                .ToArray();
        }
    }

    public static class French2
    {
        public static RuleContext[] Rules()
        {
            return new RuleContext[0]
                .Concat(Part1Chapter8.Rules())
                .Concat(Part1Chapter9.Rules())
                .ToArray();
        }
    }
}
