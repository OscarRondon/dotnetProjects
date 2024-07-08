﻿namespace PortfolioShared
{
    public class Project
    {
        public string Id { get; set; }
        public string[] ImagesUrls { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Tech> Technologies { get; set; }
        public string GitHub {  get; set; }
    }
}
