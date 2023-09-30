using eCommerceTickets.Data.Base;
using eCommerceTickets.Models;

namespace eCommerceTickets.Data.Services
{
    public class ActorsService: EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext context) : base(context)
        {
        }
    }
}
