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

        public async Task<Actor> GetById(int id) 
        {
            return await _context.Actors.Where(a => a.Id == id).FirstOrDefaultAsync();
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
