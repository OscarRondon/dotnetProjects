using PortafolioClient.Pages;
using System.Net.Http.Json;
using System.Runtime;

namespace PortafolioClient.Services.ProjectService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public List<Project> Projects { get; set; } = new List<Project>();

        public ProductService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task GetProjectsAsync()
        {
            var data  = await _httpClient.GetFromJsonAsync<ServiceResponse<Project>>("");
        }
    }
}
