using System.Collections.Generic;
using System.Linq;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.YamlDemoApp.Api.DataAccess.SqlServer;

namespace Benday.YamlDemoApp.UnitTests.Fakes.Repositories
{
    public class InMemoryLookupRepository : InMemoryRepository<LookupEntity>, ILookupRepository
    {
        public IList<LookupEntity> GetAllByType(string lookupType)
        {
            return (from temp in Items
                    where temp.LookupType == lookupType
                    select temp).ToList();
        }
    }
}
