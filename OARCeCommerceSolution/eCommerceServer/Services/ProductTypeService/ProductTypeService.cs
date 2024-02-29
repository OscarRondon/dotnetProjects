
namespace eCommerceServer.Services.ProductTypeService
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly DataContext _context;

        public ProductTypeService(DataContext context) 
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<ProductType>>> AddProductTypesAsync(ProductType productType)
        {
            _context.ProductTypes.Add(productType);
            await _context.SaveChangesAsync();
            return await GetProductTypesAsync();
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

        public async Task<ServiceResponse<List<ProductType>>> UpdateProductTypesAsync(ProductType productType)
        {
            var dbProductType = await _context.ProductTypes.FindAsync(productType.Id);
            if (dbProductType == null) 
            {
                return new ServiceResponse<List<ProductType>>
                {
                    Success = false,
                    Message = "Product Type not found.",
                    Data = null
                };
            }

            dbProductType.Name = productType.Name;
            dbProductType.Editing = productType.Editing;
            dbProductType.IsNew = productType.IsNew;

            await _context.SaveChangesAsync();
            return await GetProductTypesAsync();
        }
    }
}
