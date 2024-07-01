namespace PortafolioClient.Services.ProjectService
{
    public interface IProductService
    {
        public List<Project> Projects { get; set; }

        public Task GetProjectsAsync();
    }
}
