namespace eCommerceServer.Services.ProductTypeService
{
    public interface IProductTypeService
    {
        public Task<ServiceResponse<List<ProductType>>> GetProductTypesAsync();
    }
}
