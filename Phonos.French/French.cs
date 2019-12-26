using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using Phonos.French.SubSystems;
using System;
using System.Linq;

namespace Phonos.French
{
    public static class French
    {
        public static Rule[] Rules()
        {
            return Chapter6.Rules()
                .Concat(Part1Chapter8.Rules())
                .ToArray();
        }
    }

    public static class French2
    {
        public static IRule[] Rules()
        {
            return new IRule[0]
                .Concat(Part1Chapter6.Rules())
                .Concat(Part1Chapter7.Rules())
                .Concat(Part1Chapter8.Rules())
                .Concat(Part1Chapter9.Rules())
                .Concat(Part1Chapter13.Rules())
                .Concat(Part1Chapter14.Rules())
                .Concat(Part1Chapter15.Rules())
                .Concat(Part1Chapter18.Rules())
                .Concat(Part1Chapter19.Rules())
                .Concat(Part1Chapter24.Rules())
                .Concat(Part1Chapter27.Rules())
                .ToArray();
        }
    }
}
