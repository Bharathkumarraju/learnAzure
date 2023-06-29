using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benday.YamlDemoApp.Api.DataAccess.Entities
{
    public class CoreFieldsEntityBase : EntityBase
    {
        [Column(Order = 500)]
        [Required]
        [StringLength(30)]
        public string Status { get; set; }

        [Column(Order = 510)]
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Column(Order = 520)]
        [Required]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 530)]
        [Required]
        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        [Column(Order = 540)]
        [Required]
        public DateTime LastModifiedDate { get; set; }

        [Column(Order = 550)]
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
