
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
            var response = new ServiceResponse<List<Product>>() { Data = products };
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

        public async Task<ServiceResponse<List<Product>>> SearchProductsAsync(string searchText)
        {
            List<Product> products = await FindProductBySearchString(searchText);
            var response = new ServiceResponse<List<Product>>() { Data = products };
            return response;
        }

        public async Task<ServiceResponse<List<string>>> GetProdctsSearchSuggestionsAsync(string searchText)
        {
            List<Product> products = await FindProductBySearchString(searchText);
            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    result.Add(product.Title);

                if(!string.IsNullOrEmpty(product.Description))
                {
                    var punctuation = product.Description.Where(char.IsPunctuation)
                        .Distinct().ToArray();
                    var words = product.Description.Split()
                        .Select(d => d.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase) && !result.Contains(word))
                            result.Add(word);
                    }
                }
            }

            var response = new ServiceResponse<List<string>>() { Data = result };
            return response;
        }

        private async Task<List<Product>> FindProductBySearchString(string searchText)
        {
            return await _context.Products.Where(p => p.Title.ToLower().Contains(searchText.ToLower()) || p.Description.ToLower().Contains(searchText.ToLower()))
                .Include(pv => pv.Variants)
                .ThenInclude(pt => pt.ProductType)
                .ToListAsync();
        }

        public async Task<ServiceResponse<List<Product>>> GetFeaturedProductsAsync()
        {
            var products = await _context.Products
                .Where(p => p.Featured == true)
                .Include(pv => pv.Variants)
                .ThenInclude(pt => pt.ProductType)
                .ToListAsync();
            var response = new ServiceResponse<List<Product>>() { Data = products };
            return response;
        }
    }
}
