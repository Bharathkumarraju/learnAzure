using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.EfCore.SqlServer;

namespace Benday.YamlDemoApp.Api.DataAccess.SqlServer
{
    public partial interface IUserRepository :
        ISearchableRepository<UserEntity>
    {
}
}