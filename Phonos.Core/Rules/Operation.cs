﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.Rules
{
    public class Operation
    {
        public static Func<string[], string[]> Identity = (x) => x;

        public string Name { get; }
        public string[] Metas { get; }
        public Func<string[], string[]> Phonological { get; }
        public GraphicalMap[] Graphical { get; }

        public Operation(string name, Func<string[], string[]> phonological, GraphicalMap[] graphical, string[] metas = null)
        {
            Name = name;
            Phonological = phonological;
            Graphical = graphical;
            Metas = metas ?? new string[0];
        }
    }
}
