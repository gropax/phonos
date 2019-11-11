using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.RuleBuilder
{
    public class PhonologicalMapBuilder
    {
        private Func<string[], string[]> _phono;
        private GraphicalMap[] _graph = new GraphicalMap[0];

        public PhonologicalMap Build()
        {
            if (_phono == null)
                throw new QueryBuilderException("Phonological map must be set before building.");

            return new PhonologicalMap(_phono, _graph);
        }

        public PhonologicalMapBuilder Phono(Func<string[], string[]> map)
        {
            _phono = map;
            return this;
        }

        public PhonologicalMapBuilder Rewrite(params Func<string[], string[]>[] graphicalMaps)
        {
            _graph = graphicalMaps.Select(gm => new GraphicalMap(gm)).ToArray();
            return this;
        }
    }
}
