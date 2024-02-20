namespace eCommerceServer.Services.AddressService
{
    public interface IAddressService
    {
        public Task<ServiceResponse<Address>> GetAddressAsync();
        public Task<ServiceResponse<Address>> AddOrUpdateAddressAsync(Address address);

    }
}
