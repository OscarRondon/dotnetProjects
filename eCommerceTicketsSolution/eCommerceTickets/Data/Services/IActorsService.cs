using eCommerceTickets.Models;

namespace eCommerceTickets.Data.Services
{
    public interface IActorsService
    {
        public Task<IEnumerable<Actor>> GetAll();

        public Task<Actor> GetById( int id);

        public Task Add(Actor actor);

        public Task<Actor> Update(int id, Actor newActorData);

        public Task Delete(int id);
    }
}
