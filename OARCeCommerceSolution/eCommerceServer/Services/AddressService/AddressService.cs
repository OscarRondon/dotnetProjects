
namespace eCommerceServer.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public AddressService(
            DataContext context,
            IAuthService authService
            ) 
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<Address>> AddOrUpdateAddressAsync(Address address)
        {
            var response = new ServiceResponse<Address>
            {
                Success = true,
                Message = string.Empty
            };

            var currentAddress = (await GetAddressAsync()).Data;
            address.UserId = _authService.GetUserId();

            if(currentAddress != null )
            {
                currentAddress.FirstName = address.FirstName;
                currentAddress.LastName = address.LastName;
                currentAddress.Street = address.Street;
                currentAddress.City = address.City;
                currentAddress.State = address.State;
                currentAddress.Zip = address.Zip;
                currentAddress.Country = address.Country;
                response.Data = currentAddress;

            }
            else
            {
                response.Data = address;
                _context.Addresses.Add( address );
            }
            await _context.SaveChangesAsync();


            return response;
        }

        public async Task<ServiceResponse<Address>> GetAddressAsync()
        {
            var response = new ServiceResponse<Address>();

            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.UserId == _authService.GetUserId());

            if ( address == null )
            {
                response.Success = false;
                response.Message = "Address not found";
                response.Data = null;
            }
            else
            {
                response.Success = true;
                response.Message = string.Empty;
                response.Data = address;
            }

            return response;
        }
    }
}
