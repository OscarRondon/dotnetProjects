
namespace eCommerceServer.Services.ProductTypeService
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly DataContext _context;

        public ProductTypeService(DataContext context) 
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<ProductType>>> GetProductTypesAsync()
        {
            var response = new ServiceResponse<List<ProductType>>
            {
                Success = true,
                Message = "List of product Types",
                Data = await _context.ProductTypes.ToListAsync()
            };
            return response;
        }
    }
}
