using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceTicketsTest.InMemory.Utilities
{
    [CollectionDefinition("AppDbContextFixture")]
    public class AppDbContextFixtureCollection: ICollectionFixture<AppDbContextFixture>
    {
    }
}
