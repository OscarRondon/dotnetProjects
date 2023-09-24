using eCommerceTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTickets.Data.Services
{
    public class ActorsService: IActorsService
    {
        private readonly AppDbContext _context;

        public ActorsService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Actor>> GetAll()
        {
            return await _context.Actors.ToListAsync();
        }

        public Actor GetById(int id) 
        {
            return null;
        }

        public void Add(Actor actor)
        {
            _context.Actors.Add(actor);
            _context.SaveChanges();
        }

        public Actor Update(int id, Actor newActorData)
        {
            return null;
        }

        public void Delete(int id) 
        { 
        }
    }
}
