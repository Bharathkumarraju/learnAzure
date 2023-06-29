using Benday.EfCore.SqlServer;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Benday.Common;

namespace Benday.YamlDemoApp.Api.DomainModels
{
    public partial class ConfigurationItem :
        CoreFieldsDomainModelBase
    {
        private DomainModelField<string> _Category = new DomainModelField<string>(default(string));
        [Display(Name = "category")]
        [StringLength(50)]
        public string Category
        {
            get
            {
                return _Category.Value;
            }
            set
            {
                _Category.Value = value;
            }
        }
        
        private DomainModelField<string> _ConfigurationKey = new DomainModelField<string>(default(string));
        [Display(Name = "configuration key")]
        [StringLength(50)]
        public string ConfigurationKey
        {
            get
            {
                return _ConfigurationKey.Value;
            }
            set
            {
                _ConfigurationKey.Value = value;
            }
        }
        
        private DomainModelField<string> _Description = new DomainModelField<string>(default(string));
        [Display(Name = "description")]
        [StringLength(512)]
        public string Description
        {
            get
            {
                return _Description.Value;
            }
            set
            {
                _Description.Value = value;
            }
        }
        
        private DomainModelField<string> _ConfigurationValue = new DomainModelField<string>(default(string));
        [Display(Name = "configuration value")]
        public string ConfigurationValue
        {
            get
            {
                return _ConfigurationValue.Value;
            }
            set
            {
                _ConfigurationValue.Value = value;
            }
        }
        
        
        
        
        public override bool HasChanges()
        {
            if (base.HasChanges() == true)
            {
                return true;
            }
            
            if (_Category.HasChanges() == true)
            {
                return true;
            }
            
            if (_ConfigurationKey.HasChanges() == true)
            {
                return true;
            }
            
            if (_Description.HasChanges() == true)
            {
                return true;
            }
            
            if (_ConfigurationValue.HasChanges() == true)
            {
                return true;
            }
            
            
            
            return false;
        }
        
        public override void AcceptChanges()
        {
            base.AcceptChanges();
            
            _Category.AcceptChanges();
            
            _ConfigurationKey.AcceptChanges();
            
            _Description.AcceptChanges();
            
            _ConfigurationValue.AcceptChanges();
            
            
        }
        
        public override void UndoChanges()
        {
            base.UndoChanges();
            
            _Category.UndoChanges();
            
            _ConfigurationKey.UndoChanges();
            
            _Description.UndoChanges();
            
            _ConfigurationValue.UndoChanges();
            
            
        }
    }
}