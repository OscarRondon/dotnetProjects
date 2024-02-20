
using System.Net.Http.Json;

namespace eCommerceClient.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient _httpClient;
        private readonly ClientAppSettings _settings;

        public AddressService(
            HttpClient httpClient,
            ClientAppSettings settings
            ) 
        {
            _httpClient = httpClient;
            _settings = settings;
        }
        public async Task<Address> AddOrUpdateAddressAsync(Address address)
        {
            var response = await _httpClient.PostAsJsonAsync(_settings.BackendApiURL + "Address", address);
            return  response.Content.ReadFromJsonAsync<ServiceResponse<Address>>().Result.Data;
        }

        public async Task<Address> GetAddressAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<Address>>(_settings.BackendApiURL + "Address");
            return response.Data;
        }
    }
}
