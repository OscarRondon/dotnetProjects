
namespace eCommerceServer.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int Id)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products
                .Include(pv => pv.Variants)
                .ThenInclude(pt => pt.ProductType)
                .FirstOrDefaultAsync(p => p.Id == Id);
            if (product == null)
            {
                response.Success = false;
                response.Message = "Product not found";
                response.Data = null;
            }
            else
                response.Data = product;
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var products = await _context.Products
                .Include(pv => pv.Variants)
                .ThenInclude(pt => pt.ProductType)
                .ToListAsync();
            var response = new ServiceResponse<List<Product>>() { Data = products};
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl)
        {
            var products = await _context.Products.Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()))
                .Include(pv => pv.Variants)
                .ThenInclude(pt => pt.ProductType)
                .ToListAsync();
            var response = new ServiceResponse<List<Product>>() { Data = products };
            return response;
        }
    }
}
