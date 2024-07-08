using PortafolioClient.Pages;
using System.Net.Http.Json;
using System.Runtime;

namespace PortafolioClient.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient _httpClient;

        public List<Project> Projects { get; set; } = new List<Project>();

        public ProjectService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task GetProjectsAsync()
        {
            Projects  = await _httpClient.GetFromJsonAsync<List<Project>>("data/projectsData.json");
        }
    }
}
