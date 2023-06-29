using System.Collections.Generic;
using Benday.EfCore.SqlServer;
using Benday.YamlDemoApp.Api.DataAccess.Entities;

namespace Benday.YamlDemoApp.Api.DataAccess.SqlServer
{
    public interface ILookupRepository : ISearchableRepository<LookupEntity>
    {
        IList<LookupEntity> GetAllByType(string lookupType);
    }
}
