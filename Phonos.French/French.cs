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
                .Concat(Part1Chapter08.Rules())
                .ToArray();
        }
    }

    public static class French2
    {
        public static IRule[] Rules()
        {
            return new IRule[0]
                .Concat(Part1Chapter06.Rules())
                .Concat(Part1Chapter07.Rules())
                .Concat(Part1Chapter08.Rules())
                .Concat(Part1Chapter09.Rules())
                .Concat(Part1Chapter10.Rules())
                .Concat(Part1Chapter11.Rules())
                .Concat(Part1Chapter13.Rules())
                .Concat(Part1Chapter14.Rules())
                .Concat(Part1Chapter15.Rules())
                .Concat(Part1Chapter17.Rules())
                .Concat(Part1Chapter18.Rules())
                .Concat(Part1Chapter19.Rules())
                .Concat(Part1Chapter23.Rules())
                .Concat(Part1Chapter24.Rules())
                .Concat(Part1Chapter27.Rules())
                .ToArray();
        }
    }
}
