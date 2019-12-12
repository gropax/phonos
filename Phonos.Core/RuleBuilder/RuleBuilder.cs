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
        private string _id = null;
        private string _group = "Unnamed";
        private string _name = "Unnamed";
        private int _start = 0;
        private int _end = 0;
        private ContextualQuery[] _queries;
        private PhonologicalMap[] _maps;

        public Rule Build()
        {
            //if (_id == null)
            //    throw new QueryBuilderException("Rule must have an ID.");
            //else
            if (_queries.Length == 0)
                throw new QueryBuilderException("Rule must have at least one contextual query.");
            else if (_maps == null)
                throw new QueryBuilderException("Phonological map must be set before building.");

            return new Rule(_id, _group, _name, new Interval(_start, _end - _start),
                _queries, _maps);
        }

        public RuleBuilder Id(string id)
        {
            _id = id;
            return this;
        }

        public RuleBuilder Group(string group)
        {
            _group = group;
            return this;
        }

        public RuleBuilder Named(string name)
        {
            _name = name;
            return this;
        }

        public RuleBuilder From(int start)
        {
            _start = start;
            return this;
        }

        public RuleBuilder To(int end)
        {
            _end = end;
            return this;
        }

        public RuleBuilder Query(params Action<ContextualQueryBuilder>[] queryDefinitions)
        {
            var queries = queryDefinitions.Select(md =>
            {
                var builder = new ContextualQueryBuilder();
                md(builder);
                return builder.Build();
            });

            _queries = queries.ToArray();
            return this;
        }

        public RuleBuilder Map(params Action<PhonologicalMapBuilder>[] mapDefinitions)
        {
            var maps = mapDefinitions.Select(md =>
            {
                var builder = new PhonologicalMapBuilder();
                md(builder);
                return builder.Build();
            });

            _maps = maps.ToArray();
            return this;
        }
    }





    public class QueryBuilderException : Exception
    {
        public QueryBuilderException(string message) : base(message)
        {
        }
    }
}
