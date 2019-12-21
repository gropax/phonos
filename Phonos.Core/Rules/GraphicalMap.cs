using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.Rules
{
    public class GraphicalMap
    {
        public static GraphicalMap Identity => new GraphicalMap(_ => _);

        public Func<string, string> Map { get; }
        public GraphicalMap(Func<string, string> map)
        {
            Map = map;
        }
    }
}
