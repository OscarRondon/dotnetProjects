using eCommerceTickets.Data.Base;
using eCommerceTickets.Models;

namespace eCommerceTickets.Data.Services
{
    public class CinemasServices : EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasServices(AppDbContext context): base(context)
        {

        }
    }
}
