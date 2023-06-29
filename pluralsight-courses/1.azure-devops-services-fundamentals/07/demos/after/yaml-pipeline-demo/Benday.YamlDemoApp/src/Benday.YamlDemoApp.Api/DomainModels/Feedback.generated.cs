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
    public partial class Feedback :
        CoreFieldsDomainModelBase
    {
        private DomainModelField<string> _FeedbackType = new DomainModelField<string>(default(string));
        [Display(Name = "feedback type")]
        [StringLength(50)]
        public string FeedbackType
        {
            get
            {
                return _FeedbackType.Value;
            }
            set
            {
                _FeedbackType.Value = value;
            }
        }
        
        private DomainModelField<string> _Sentiment = new DomainModelField<string>(default(string));
        [Display(Name = "sentiment")]
        [StringLength(50)]
        public string Sentiment
        {
            get
            {
                return _Sentiment.Value;
            }
            set
            {
                _Sentiment.Value = value;
            }
        }
        
        private DomainModelField<string> _Subject = new DomainModelField<string>(default(string));
        [Display(Name = "notification subject")]
        [StringLength(1024)]
        public string Subject
        {
            get
            {
                return _Subject.Value;
            }
            set
            {
                _Subject.Value = value;
            }
        }
        
        private DomainModelField<string> _FeedbackText = new DomainModelField<string>(default(string));
        [Display(Name = "message")]
        [StringLength(2048)]
        public string FeedbackText
        {
            get
            {
                return _FeedbackText.Value;
            }
            set
            {
                _FeedbackText.Value = value;
            }
        }
        
        private DomainModelField<string> _Username = new DomainModelField<string>(default(string));
        [Display(Name = "username")]
        [StringLength(50)]
        public string Username
        {
            get
            {
                return _Username.Value;
            }
            set
            {
                _Username.Value = value;
            }
        }
        
        private DomainModelField<string> _FirstName = new DomainModelField<string>(default(string));
        [Display(Name = "first name")]
        [StringLength(50)]
        public string FirstName
        {
            get
            {
                return _FirstName.Value;
            }
            set
            {
                _FirstName.Value = value;
            }
        }
        
        private DomainModelField<string> _LastName = new DomainModelField<string>(default(string));
        [Display(Name = "last name")]
        [StringLength(50)]
        public string LastName
        {
            get
            {
                return _LastName.Value;
            }
            set
            {
                _LastName.Value = value;
            }
        }
        
        private DomainModelField<string> _Referer = new DomainModelField<string>(default(string));
        [Display(Name = "referer")]
        [StringLength(1000)]
        public string Referer
        {
            get
            {
                return _Referer.Value;
            }
            set
            {
                _Referer.Value = value;
            }
        }
        
        private DomainModelField<string> _UserAgent = new DomainModelField<string>(default(string));
        [Display(Name = "user agent")]
        [StringLength(1000)]
        public string UserAgent
        {
            get
            {
                return _UserAgent.Value;
            }
            set
            {
                _UserAgent.Value = value;
            }
        }
        
        private DomainModelField<string> _IpAddress = new DomainModelField<string>(default(string));
        [Display(Name = "ip address")]
        [StringLength(50)]
        public string IpAddress
        {
            get
            {
                return _IpAddress.Value;
            }
            set
            {
                _IpAddress.Value = value;
            }
        }
        
        private DomainModelField<bool> _IsContactRequest = new DomainModelField<bool>(default(bool));
        [Display(Name = "yes, I'd like a reply")]
        public bool IsContactRequest
        {
            get
            {
                return _IsContactRequest.Value;
            }
            set
            {
                _IsContactRequest.Value = value;
            }
        }
        
        
        
        
        public override bool HasChanges()
        {
            if (base.HasChanges() == true)
            {
                return true;
            }
            
            if (_FeedbackType.HasChanges() == true)
            {
                return true;
            }
            
            if (_Sentiment.HasChanges() == true)
            {
                return true;
            }
            
            if (_Subject.HasChanges() == true)
            {
                return true;
            }
            
            if (_FeedbackText.HasChanges() == true)
            {
                return true;
            }
            
            if (_Username.HasChanges() == true)
            {
                return true;
            }
            
            if (_FirstName.HasChanges() == true)
            {
                return true;
            }
            
            if (_LastName.HasChanges() == true)
            {
                return true;
            }
            
            if (_Referer.HasChanges() == true)
            {
                return true;
            }
            
            if (_UserAgent.HasChanges() == true)
            {
                return true;
            }
            
            if (_IpAddress.HasChanges() == true)
            {
                return true;
            }
            
            if (_IsContactRequest.HasChanges() == true)
            {
                return true;
            }
            
            
            
            return false;
        }
        
        public override void AcceptChanges()
        {
            base.AcceptChanges();
            
            _FeedbackType.AcceptChanges();
            
            _Sentiment.AcceptChanges();
            
            _Subject.AcceptChanges();
            
            _FeedbackText.AcceptChanges();
            
            _Username.AcceptChanges();
            
            _FirstName.AcceptChanges();
            
            _LastName.AcceptChanges();
            
            _Referer.AcceptChanges();
            
            _UserAgent.AcceptChanges();
            
            _IpAddress.AcceptChanges();
            
            _IsContactRequest.AcceptChanges();
            
            
        }
        
        public override void UndoChanges()
        {
            base.UndoChanges();
            
            _FeedbackType.UndoChanges();
            
            _Sentiment.UndoChanges();
            
            _Subject.UndoChanges();
            
            _FeedbackText.UndoChanges();
            
            _Username.UndoChanges();
            
            _FirstName.UndoChanges();
            
            _LastName.UndoChanges();
            
            _Referer.UndoChanges();
            
            _UserAgent.UndoChanges();
            
            _IpAddress.UndoChanges();
            
            _IsContactRequest.UndoChanges();
            
            
        }
    }
}