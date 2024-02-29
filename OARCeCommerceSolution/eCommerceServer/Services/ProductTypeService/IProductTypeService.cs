namespace eCommerceServer.Services.ProductTypeService
{
    public interface IProductTypeService
    {
        public Task<ServiceResponse<List<ProductType>>> GetProductTypesAsync();
        public Task<ServiceResponse<List<ProductType>>> AddProductTypesAsync(ProductType productType);
        public Task<ServiceResponse<List<ProductType>>> UpdateProductTypesAsync(ProductType productType);

    }
}
