using Intervals;
using Phonos.Core.Queries;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.RuleBuilder
{

    public class RuleBuilder
    {
        private string _name = null;
        private string[] _metas = new string[0];
        private Func<string[], string[]> _phono;
        private GraphicalMap[] _graph;

        public Operation Build()
        {
            return new Operation(_name, _phono ?? Operation.Identity,
                _graph ?? new[] { GraphicalMap.Identity },
                _metas);
        }

        public RuleBuilder Named(string name)
        {
            _name = name;
            return this;
        }

        public RuleBuilder Meta(params string[] metas)
        {
            _metas = metas;
            return this;
        }

        public RuleBuilder Phono(Func<string[], string[]> map)
        {
            _phono = map;
            return this;
        }

        public RuleBuilder Rewrite(params Func<string, string, string, string>[] graphicalMaps)
        {
            _graph = graphicalMaps.Select(gm =>
                new GraphicalMap((b, m, a) =>
                    ParseIntervals(gm(b, m, a)).ToArray())).ToArray();
            return this;
        }

        public RuleBuilder Rewrite(params Func<string, string>[] graphicalMaps)
        {
            _graph = graphicalMaps.Select(gm =>
                new GraphicalMap((b, m, a) =>
                    ParseIntervals(gm(m)).ToArray())).ToArray();
            return this;
        }

        private IEnumerable<Interval<string>> ParseIntervals(string s)
        {
            int shift = 0;
            foreach (var i in s.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                var parts = i.Trim().Split(':');
                if (parts.Length > 2)
                    throw new Exception($"Invalid interval descriptor: [{s}].");

                int length = parts.Length == 2 ? int.Parse(parts[1]) : 1;

                yield return new Interval<string>(shift, length, parts[0]);

                shift += length;
            }
        }
    }
}
