namespace eCommerceClient.Services.ProductTypeService
{
    public interface IProductTypeService
    {
        public event Action OnChange;
        public List<ProductType> ProductTypes { get; set; }
        public Task GetProductTypesAsync();
        public Task AddProductTypesAsync(ProductType productType);
        public Task UpdateProductTypesAsync(ProductType productType);
        ProductType CreateNewProductType();

    }
}
