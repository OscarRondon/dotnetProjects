using eCommerceTickets.Data.Base;
using eCommerceTickets.Models;

namespace eCommerceTickets.Data.Services
{
    public class ProducersService: EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext context) : base(context)
        {
        }
    }
}
