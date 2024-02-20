namespace eCommerceClient.Services.AddressService
{
    public interface IAddressService
    {
        public Task<Address> GetAddressAsync();
        public Task<Address> AddOrUpdateAddressAsync(Address address);
    }
}
