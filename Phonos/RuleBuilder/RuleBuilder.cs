using Intervals;
using Phonos.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.RuleBuilder
{
    public static class Dev
    {
        public static void Method()
        {
            var ruleBuilder = new RuleBuilder();
            ruleBuilder
                .Named("My rule")
                .From(0).To(200)
                .Match(q => q.Phon("a").With("accent", "tonic"))
                .Before(q => q.Seq(Q.Start, q2 => q2.Phon("b").With("auie", "nrst")))
                .After(Q.End)
                .Map(m => m.Phono(Ph.Degeminate).Rewrite());
        }
    }

    public static class Q
    {
        public static Action<ContextQueryBuilder> Start => qb => qb.Start();
        public static Action<ContextQueryBuilder> End => qb => qb.End();
    }

    public static class Ph
    {
        public static string[] Degeminate(string[] phonemes)
        {
            throw new NotImplementedException();
        }
    }

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

    public abstract class QueryBuilder
    {
        protected IQuery _query;

        public IQuery Build()
        {
            if (_query == null)
                throw new QueryBuilderException("Query must be set before building.");

            return _query;
        }

        protected void SetQuery(IQuery query)
        {
            if (_query != null)
                throw new QueryBuilderException("Query already defined.");
            _query = query;
        }

        protected void WrapQuery(IQuery query)
        {
            if (_query == null)
                throw new QueryBuilderException("Define a query first before using With().");
            _query = query;
        }
    }

    public class MatchQueryBuilder : QueryBuilder
    {
        public MatchQueryBuilder Phon(params string[] phonemes)
        {
            SetQuery(new PhonemeQuery(phonemes));
            return this;
        }

        public MatchQueryBuilder Seq(params Action<MatchQueryBuilder>[] queriesDefinitions)
        {
            var queries = queriesDefinitions.Select(qd =>
            {
                var queryBuilder = new MatchQueryBuilder();
                qd(queryBuilder);
                return queryBuilder.Build();
            });

            SetQuery(new SequenceQuery(queries));
            return this;
        }

        public MatchQueryBuilder With(string key, params string[] values)
        {
            WrapQuery(new WithQuery(_query, key, values));
            return this;
        }
    }

    public class ContextQueryBuilder : QueryBuilder
    {
        public ContextQueryBuilder Start()
        {
            SetQuery(new StartAnchorQuery());
            return this;
        }

        public ContextQueryBuilder End()
        {
            SetQuery(new EndAnchorQuery());
            return this;
        }

        public ContextQueryBuilder Phon(params string[] phonemes)
        {
            SetQuery(new PhonemeQuery(phonemes));
            return this;
        }

        public ContextQueryBuilder Seq(params Action<ContextQueryBuilder>[] queriesDefinitions)
        {
            var queries = queriesDefinitions.Select(qd =>
            {
                var queryBuilder = new ContextQueryBuilder();
                qd(queryBuilder);
                return queryBuilder.Build();
            });

            SetQuery(new SequenceQuery(queries));
            return this;
        }

        public ContextQueryBuilder With(string key, params string[] values)
        {
            WrapQuery(new WithQuery(_query, key, values));
            return this;
        }
    }

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

    public class QueryBuilderException : Exception
    {
        public QueryBuilderException(string message) : base(message)
        {
        }
    }
}
