using Microsoft.AspNetCore;

namespace ExpenseTracker.Configuration
{
    public class Configuration
    {
        public string DBUser { get; }
        public string DBPassword { get; }
        public string DBName { get; }

        public Configuration()  { 
        }
    }
}
