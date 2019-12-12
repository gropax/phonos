using Intervals;
using Phonos.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.RuleBuilder
{

    public class RuleBuilder
    {
        private string _name = "Unnamed";
        private Func<string[], string[]> _phono;
        private GraphicalMap[] _graph;

        public Rule Build()
        {
            if (_phono == null)
                throw new QueryBuilderException("Phonological map must be set before building.");

            return new Rule(_name, _phono,
                _graph ?? new[] { GraphicalMap.Identity });
        }

        public RuleBuilder Named(string name)
        {
            _name = name;
            return this;
        }

        public RuleBuilder Phono(Func<string[], string[]> map)
        {
            _phono = map;
            return this;
        }

        public RuleBuilder Rewrite(params Func<string, string>[] graphicalMaps)
        {
            _graph = graphicalMaps.Select(gm => new GraphicalMap(gm)).ToArray();
            return this;
        }
    }
}
