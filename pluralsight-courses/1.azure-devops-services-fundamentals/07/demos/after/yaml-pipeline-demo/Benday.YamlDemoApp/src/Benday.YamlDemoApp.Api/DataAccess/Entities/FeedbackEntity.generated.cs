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
    [Table("Feedback", Schema = "dbo")]
    public partial class FeedbackEntity : CoreFieldsEntityBase
    {
        [Column(Order = 1)]
        [StringLength(50)]
        [Required]
        public string FeedbackType { get; set; }
        
        [Column(Order = 2)]
        [StringLength(50)]
        [Required]
        public string Sentiment { get; set; }
        
        [Column(Order = 3)]
        [StringLength(1024)]
        [Required]
        public string Subject { get; set; }
        
        [Column(Order = 4)]
        [StringLength(2048)]
        [Required]
        public string FeedbackText { get; set; }
        
        [Column(Order = 5)]
        [StringLength(50)]
        [Required]
        public string Username { get; set; }
        
        [Column(Order = 6)]
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }
        
        [Column(Order = 7)]
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }
        
        [Column(Order = 8)]
        [StringLength(1000)]
        [Required]
        public string Referer { get; set; }
        
        [Column(Order = 9)]
        [StringLength(1000)]
        [Required]
        public string UserAgent { get; set; }
        
        [Column(Order = 10)]
        [StringLength(50)]
        [Required]
        public string IpAddress { get; set; }
        
        [Column(Order = 11)]
        [Required]
        public bool IsContactRequest { get; set; }
        
        
        
        
    }
}