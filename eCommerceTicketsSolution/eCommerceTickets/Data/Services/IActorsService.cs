using eCommerceTickets.Models;

namespace eCommerceTickets.Data.Services
{
    public interface IActorsService
    {
        public Task<IEnumerable<Actor>> GetAll();

        public Task<Actor> GetById( int id);

        public void Add(Actor actor);

        public Actor Update(int id, Actor newActorData);

        public void Delete(int id);
    }
}
