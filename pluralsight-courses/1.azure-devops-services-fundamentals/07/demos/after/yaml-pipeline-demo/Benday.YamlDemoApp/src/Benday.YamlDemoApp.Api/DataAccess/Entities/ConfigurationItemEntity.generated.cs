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
    [Table("ConfigurationItem", Schema = "dbo")]
    public partial class ConfigurationItemEntity : CoreFieldsEntityBase
    {
        [Column(Order = 1)]
        [StringLength(50)]
        [Required]
        public string Category { get; set; }
        
        [Column(Order = 2)]
        [StringLength(50)]
        [Required]
        public string ConfigurationKey { get; set; }
        
        [Column(Order = 3)]
        [StringLength(512)]
        [Required]
        public string Description { get; set; }
        
        [Column(Order = 4)]
        
        [Required]
        public string ConfigurationValue { get; set; }
        
        
        
        
    }
}