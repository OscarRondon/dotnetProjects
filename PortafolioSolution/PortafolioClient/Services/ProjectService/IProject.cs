namespace PortafolioClient.Services.ProjectService
{
    public interface IProjectService
    {
        public List<Project> Projects { get; set; }

        public Task GetProjectsAsync();
    }
}
