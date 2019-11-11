﻿using Phonos.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.RuleBuilder
{
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
}