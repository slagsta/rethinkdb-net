using RethinkDb.Spec;
using System;
using System.Linq.Expressions;

namespace RethinkDb.QueryTerm
{
    public class JoinQuery<TLeft, TRight> : ISequenceQuery<Tuple<TLeft, TRight>>
    {
        private ISequenceQuery<TLeft> leftQuery;
        private ISequenceQuery<TRight> rightQuery;
        private Expression<Func<TLeft, TRight, bool>> joinPredicate;

        public JoinQuery(ISequenceQuery<TLeft> leftQuery, ISequenceQuery<TRight> rightQuery, Expression<Func<TLeft, TRight, bool>> joinPredicate)
        {
            this.leftQuery = leftQuery;
            this.rightQuery = rightQuery;
            this.joinPredicate = joinPredicate;
        }

        public Term GenerateTerm(IDatumConverterFactory datumConverterFactory)
        {
            var filterTerm = new Term()
            {
                type = Term.TermType.INNER_JOIN,
            };
            filterTerm.args.Add(leftQuery.GenerateTerm(datumConverterFactory));
            filterTerm.args.Add(rightQuery.GenerateTerm(datumConverterFactory));

            if (joinPredicate.NodeType != ExpressionType.Lambda)
                throw new NotSupportedException("Unsupported expression type");

            var body = joinPredicate.Body;
            filterTerm.args.Add(ExpressionUtils.MapLambdaToFunction<TLeft, TRight>(datumConverterFactory, (LambdaExpression)joinPredicate));

            return filterTerm;
        }
    }
}
