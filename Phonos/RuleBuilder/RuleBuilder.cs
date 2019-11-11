using Intervals;
using Phonos.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.RuleBuilder
{

    public class RuleBuilder
    {
        private string _name = "Unnamed";
        private int _start = 0;
        private int _end = 0;
        private IQuery _query;
        private IQuery _lookBehind;
        private IQuery _lookAhead;
        private PhonologicalMap[] _maps;

        public Rule Build()
        {
            if (_query == null)
                throw new QueryBuilderException("Match query must be set before building.");
            else if (_maps == null)
                throw new QueryBuilderException("Phonological map must be set before building.");

            return new Rule(_name, new Interval(_start, _end - _start),
                _query, _maps, _lookBehind, _lookAhead);
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

        public RuleBuilder Match(Action<MatchQueryBuilder> queryDefinition)
        {
            var queryBuilder = new MatchQueryBuilder();
            queryDefinition(queryBuilder);
            _query = queryBuilder.Build();
            return this;
        }

        public RuleBuilder Before(Action<ContextQueryBuilder> queryDefinition)
        {
            var queryBuilder = new ContextQueryBuilder();
            queryDefinition(queryBuilder);
            _lookBehind = queryBuilder.Build();
            return this;
        }

        public RuleBuilder After(Action<ContextQueryBuilder> queryDefinition)
        {
            var queryBuilder = new ContextQueryBuilder();
            queryDefinition(queryBuilder);
            _lookAhead = queryBuilder.Build();
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
