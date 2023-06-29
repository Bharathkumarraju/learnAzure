using Benday.YamlDemoApp.Api;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.EfCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using Benday.Common;
using Benday.YamlDemoApp.Api.DataAccess.SqlServer;

namespace Benday.YamlDemoApp.UnitTests.Fakes.Repositories
{
    public partial class InMemoryUserClaimRepository :
        InMemoryRepository<UserClaimEntity>, IUserClaimRepository
    {
}
}