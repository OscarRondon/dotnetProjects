namespace eCommerceTickets.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        public Task<IEnumerable<T>> GetAll();

        public Task<T> GetById(int id);

        public Task Add(T entity);

        public Task<T> Update(int id, T newEntity);

        public Task Delete(int id);
    }
}
