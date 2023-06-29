using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Benday.EfCore.SqlServer;

namespace Benday.YamlDemoApp.Api.DataAccess.Entities
{
    public class EntityBase : IEntityBase
    {
        [Column(Order = 0)]
        public int Id { get; set; }

        [NotMapped]
        public bool IsMarkedForDelete
        {
            get; set;
        }

        public virtual IList<IDependentEntityCollection> GetDependentEntities()
        {
            return null;
        }
    }
}
