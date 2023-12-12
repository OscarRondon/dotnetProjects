namespace eCommerceClient.Services.AppService
{
    public interface IAppService
    {
        public string ApiUrl { get; set; }

        public Task GetEndpointUrls();
    }
}
