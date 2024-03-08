
namespace eCommerceServer.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int Id)
        {
            var response = new ServiceResponse<Product>();

            Product product = null;

            if(_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                product = await _context.Products
                .Include(pv => pv.Variants.Where(pv => !pv.Deleted))
                .ThenInclude(pt => pt.ProductType)
                .Include(i => i.Images)
                .FirstOrDefaultAsync(p => p.Id == Id &&  !p.Deleted);
            }
            else
            {
                product = await _context.Products
                .Include(pv => pv.Variants.Where(pv => pv.Visible && !pv.Deleted))
                .ThenInclude(pt => pt.ProductType)
                .Include(i => i.Images)
                .FirstOrDefaultAsync(p => p.Id == Id && p.Visible && !p.Deleted);
            }

            
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
                .Where(p => p.Visible && !p.Deleted)
                .Include(pv => pv.Variants.Where(pv => pv.Visible && !pv.Deleted))
                .ThenInclude(pt => pt.ProductType)
                .Include(i => i.Images)
                .ToListAsync();
            var response = new ServiceResponse<List<Product>>() { Data = products };
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl)
        {
            var products = await _context.Products.Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()) && p.Visible && !p.Deleted)
                .Include(pv => pv.Variants.Where(pv => pv.Visible && !pv.Deleted))
                .ThenInclude(pt => pt.ProductType)
                .Include(i => i.Images)
                .ToListAsync();
            var response = new ServiceResponse<List<Product>>() { Data = products };
            return response;
        }

        public async Task<ServiceResponse<ProductSearchResult>> SearchProductsAsync(string searchText, int page)
        {

            var pageResults = 2f;
            List<Product> products = await FindProductBySearchString(searchText);
            var pageCount = Math.Ceiling(products.Count / pageResults);

            products = await FindProductBySearchString(searchText, (page - 1) * (int)pageResults, (int)pageResults);

            var response = new ServiceResponse<ProductSearchResult>()
            {
                Data = new ProductSearchResult
                {
                    CurrentPage = page,
                    Pages = (int)pageCount,
                    Products = products
                }
            };

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

                if (!string.IsNullOrEmpty(product.Description))
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

        private async Task<List<Product>> FindProductBySearchString(string searchText, int skip = 0, int take = 10)
        {
            return await _context.Products.Where(p => p.Title.ToLower().Contains(searchText.ToLower()) || p.Description.ToLower().Contains(searchText.ToLower()) && p.Visible && !p.Deleted)
                .Include(pv => pv.Variants.Where(pv => pv.Visible && !pv.Deleted))
                .ThenInclude(pt => pt.ProductType)
                .Include(i => i.Images)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<ServiceResponse<List<Product>>> GetFeaturedProductsAsync()
        {
            var products = await _context.Products
                .Where(p => p.Featured && p.Visible && !p.Deleted)
                .Include(pv => pv.Variants.Where(pv => pv.Visible && !pv.Deleted))
                .ThenInclude(pt => pt.ProductType)
                .Include(i => i.Images)
                .ToListAsync();
            var response = new ServiceResponse<List<Product>>() { Data = products };
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetAdminProductsAsync()
        {
            var products = await _context.Products
                .Where(p => !p.Deleted)
                .Include(pv => pv.Variants.Where(pv => !pv.Deleted))
                .ThenInclude(pt => pt.ProductType)
                .Include(i => i.Images)
                .ToListAsync();
            var response = new ServiceResponse<List<Product>>() { Data = products };
            return response;
        }

        public async Task<ServiceResponse<Product>> CreateProductAsync(Product product)
        {
            foreach(var variant in product.Variants)
            {
                variant.ProductType = null;
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Product>
            {
                Success = true,
                Message = "Product created successfully",
                Data = product
            };
        }

        public async Task<ServiceResponse<Product>> UpdateProductAsync(Product product)
        {
            var dbProduct = await _context.Products.FindAsync(product.Id);
            if (dbProduct != null)
            {
                dbProduct.Title = product.Title;
                dbProduct.Description = product.Description;
                dbProduct.ImageUrl = product.ImageUrl;
                dbProduct.CategoryId = product.CategoryId;
                dbProduct.Category = product.Category;
                dbProduct.Featured = product.Featured;
                dbProduct.Visible = product.Visible;
                dbProduct.Images = product.Images;
                dbProduct.Deleted = false;

                foreach (var variant in product.Variants)
                {
                    var dbVariant = await _context.ProductVariants.SingleOrDefaultAsync(v => v.ProductId == variant.ProductId && v.ProductTypeId == variant.ProductTypeId);
                    if (dbVariant != null)
                    {
                        dbVariant.ProductId = variant.ProductId;
                        dbVariant.ProductTypeId = variant.ProductTypeId;
                        dbVariant.Price = variant.Price;
                        dbVariant.OriginalPrice = variant.OriginalPrice;
                        dbVariant.Visible = variant.Visible;
                        dbVariant.Deleted = false;
                    }
                    else
                    {
                        variant.ProductType = null;
                        _context.ProductVariants.Add(variant);
                    }
                }
                await _context.SaveChangesAsync();
                return new ServiceResponse<Product>
                {
                    Success = true,
                    Message = "Product deleted",
                    Data = dbProduct
                };
            }
            else
            {
                return new ServiceResponse<Product>
                {
                    Success = false,
                    Message = "Product not found",
                    Data = null
                };
            }
        }

        public async Task<ServiceResponse<bool>> DeleteProductAsync(int Id)
        {
            var dbProduct = await _context.Products.FindAsync(Id);
            if(dbProduct != null)
            {
                dbProduct.Deleted = true;
                await _context.SaveChangesAsync();
                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = "Product deleted",
                    Data = true
                };
            }
            else
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Product not found",
                    Data = false
                };
            }
        }
    }
}
