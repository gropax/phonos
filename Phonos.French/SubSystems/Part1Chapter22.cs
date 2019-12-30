﻿using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    public static class Part1Chapter22
    {
        public static IRule[] Rules()
        {
            return RuleComponents();
            //return new IRule[] { };
        }

        public static IRule[] RuleComponents()
        {
            return new[]
            {
                Rule1a(), Rule1b(),
            };
        }

        public static Rule Rule1a()
        {
            return R.Rule(c => c
                .Id("p1c22r1a")
                .From(0).To(100)
                .Query(q => q.Match(m => m.Phon("h")))
                .Rules(r => r
                    .Named("Amuïssement de /h/ (latin)")
                    .Phono(P.Erase)
                    .Rewrite(G.Erase)));
        }

        public static Rule Rule1b()
        {
            return R.Rule(c => c
                .Id("p1c22r1b")
                .From(1600).To(1700)
                .Query(q => q.Match(m => m.Phon("h")))
                .Rules(r => r
                    .Named("Amuïssement de /h/ (germanique)")
                    .Phono(P.Erase)));
        }
    }
}
