using System.Linq.Expressions;

namespace eCommerceTicketsTest.Mocks.Utilities
{
    internal class MockAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        IQueryProvider IQueryable.Provider  => new MockAsyncQueryProvider<T>(this);

        public MockAsyncEnumerable(Expression expression) : base(expression) 
        {
            
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new MockAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }
    }
}
