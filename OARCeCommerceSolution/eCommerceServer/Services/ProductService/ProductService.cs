
namespace eCommerceServer.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _contex;

        public ProductService(DataContext contex)
        {
            _contex = contex;
        }
        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var products = await _contex.Products.ToListAsync();
            var response = new ServiceResponse<List<Product>>() { Data = products};
            return response;
        }
    }
}
