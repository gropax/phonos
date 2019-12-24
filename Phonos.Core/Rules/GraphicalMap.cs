using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.Rules
{
    public class GraphicalMap
    {
        public static GraphicalMap Identity => new GraphicalMap((b, m, a) => m);

        public Func<string, string, string, string> Map { get; }
        public GraphicalMap(Func<string, string, string, string> map)
        {
            Map = map;
        }
    }
}
