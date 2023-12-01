namespace BlazorServerApp.Configurations
{
    public class PagingConf
    {
        public bool Enabled { get; set; }
        public int PageSize { get; set; } = 1;

        public int ItemsToSkip(int pageNumber)
        {
            if (Enabled)
            {
                return (pageNumber - 1)*PageSize;
            }
            return 0;
        }

        public int ItemsToTake(int totalItemsCount)
        {
            if (Enabled)
            {
                return PageSize;
            }
            return totalItemsCount;
        }

        public int PrevPageNumber(int currentPage)
        {
            if (Enabled)
            {
                if(currentPage > 0)
                    return (currentPage - 1);
                return 1;

            }
            return 1;
        }

        public int NextPageNumber(int currentPage, int totalItemsCount)
        {
            if (Enabled)
            {
                if (currentPage < MaxPageNumber(totalItemsCount))
                    return (currentPage + 1);
                return currentPage;

            }
            return  currentPage;
        }

        public int MaxPageNumber(int totalItemsCount)
        {
            double totalPages = totalItemsCount / PageSize;
            if (Enabled)
            {
                return (int) Math.Ceiling(totalPages);

            }
            return 1;
        }
    }
}
