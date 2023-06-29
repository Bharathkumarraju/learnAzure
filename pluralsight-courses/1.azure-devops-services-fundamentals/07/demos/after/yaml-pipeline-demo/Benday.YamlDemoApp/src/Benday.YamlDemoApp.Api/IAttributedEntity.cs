using System.Collections.Generic;
using Benday.YamlDemoApp.Api.DataAccess.Entities;

namespace Benday.YamlDemoApp.Api
{
    public interface IAttributedEntity
    {
        List<EntityBase> GetAttributes();
    }
}
