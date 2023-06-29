using Benday.EfCore.SqlServer;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Benday.Common;

namespace Benday.YamlDemoApp.Api.DataAccess.Entities
{
    [Table("User", Schema = "dbo")]
    public partial class UserEntity : CoreFieldsEntityBase
    {
        [Column(Order = 1)]
        [StringLength(100)]
        [Required]
        public string Username { get; set; }
        
        [Column(Order = 2)]
        [StringLength(100)]
        [Required]
        public string Source { get; set; }
        
        [Column(Order = 3)]
        [StringLength(100)]
        [Required]
        public string EmailAddress { get; set; }
        
        [Column(Order = 4)]
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }
        
        [Column(Order = 5)]
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }
        
        [Column(Order = 6)]
        [StringLength(50)]
        [Required]
        public string PhoneNumber { get; set; }
        
        private IList<UserClaimEntity> _Claims;
        public IList<UserClaimEntity> Claims
        {
            get
            {
                if (_Claims == null)
                {
                    _Claims = new List<UserClaimEntity>();
                }
                
                return _Claims;
            }
            set
            {
                _Claims = value;
            }
        }
        
        
        public override IList<IDependentEntityCollection> GetDependentEntities()
        {
            var entities = new List<IDependentEntityCollection>();
            entities.Add(new DependentEntityCollection<UserClaimEntity>(Claims));
            return entities;
        }
        
    }
}