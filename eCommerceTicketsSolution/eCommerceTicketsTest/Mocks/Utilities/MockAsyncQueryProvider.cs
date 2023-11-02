using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace eCommerceTicketsTest.Mocks.Utilities
{
    internal class MockAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _provider;

        public MockAsyncQueryProvider(IQueryProvider provider)
        {
            _provider = provider;
        }
        public IQueryable CreateQuery(Expression expression)
        {
            return new MockAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new MockAsyncEnumerable<TElement>(expression);
        }

        public object? Execute(Expression expression)
        {
            return _provider.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _provider.Execute<TResult>(expression);
        }

        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return new MockAsyncEnumerable<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        {
            return Execute<TResult>(expression);
        }

        //IAsyncEnumerable<TResult> IAsyncQueryProvider.ExecuteAsync<TResult>(System.Linq.Expressions.Expression expression, System.Threading.CancellationToken cancellationToken)
        //{
        //    return new MockAsyncEnumerable<TResult>(expression);
        //}
    }
}
