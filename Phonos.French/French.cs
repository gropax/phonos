﻿using Phonos.Core;
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

    public static class French2
    {
        public static RuleContext[] Rules()
        {
            return Part1Chapter9.Rules();
        }
    }
}
