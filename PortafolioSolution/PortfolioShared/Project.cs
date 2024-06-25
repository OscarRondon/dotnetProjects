namespace PortfolioShared
{
    public class Project
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Tech> Technologies { get; set; }
    }
}
