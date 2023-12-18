
namespace eCommerceServer.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _contex;

        public ProductService(DataContext contex)
        {
            _contex = contex;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int Id)
        {
            var response = new ServiceResponse<Product>();
            var product = await _contex.Products.FindAsync(Id);
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
            var products = await _contex.Products.ToListAsync();
            var response = new ServiceResponse<List<Product>>() { Data = products};
            return response;
        }
    }
}
