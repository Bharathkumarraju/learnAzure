using System;
using System.ComponentModel.DataAnnotations;

namespace Benday.YamlDemoApp.Api.DomainModels
{
    public abstract class CoreFieldsDomainModelBase : DomainModelBase
    {
        private readonly DomainModelField<string> _status = new(ApiConstants.DefaultStatus);
        [Required]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string Status
        {
            get => _status.Value;
            set => _status.Value = value;
        }

        private readonly DomainModelField<string> _createdBy = new(default);
        [Display(Name = "created by")]
        public string CreatedBy
        {
            get => _createdBy.Value;
            set => _createdBy.Value = value;
        }

        private readonly DomainModelField<DateTime> _createdDate = new(default);
        [Display(Name = "created date")]
        public DateTime CreatedDate
        {
            get => _createdDate.Value;
            set => _createdDate.Value = value;
        }

        private readonly DomainModelField<string> _lastModifiedBy = new(default);
        [Display(Name = "last modified by")]
        public string LastModifiedBy
        {
            get => _lastModifiedBy.Value;
            set => _lastModifiedBy.Value = value;
        }

        private readonly DomainModelField<DateTime> _lastModifiedDate = new(default);
        [Display(Name = "last modified date")]
        public DateTime LastModifiedDate
        {
            get => _lastModifiedDate.Value;
            set => _lastModifiedDate.Value = value;
        }

        private readonly DomainModelField<byte[]> _timestamp = new(default);
        [Display(Name = "timestamp")]
        public byte[] Timestamp
        {
            get => _timestamp.Value;
            set => _timestamp.Value = value;
        }

        public override bool HasChanges()
        {
            if (base.HasChanges() == true)
            {
                return true;
            }

            if (_status.HasChanges() == true)
            {
                return true;
            }

            if (_createdBy.HasChanges() == true)
            {
                return true;
            }

            if (_createdDate.HasChanges() == true)
            {
                return true;
            }

            if (_lastModifiedBy.HasChanges() == true)
            {
                return true;
            }

            if (_lastModifiedDate.HasChanges() == true)
            {
                return true;
            }

            if (_timestamp.HasChanges() == true)
            {
                return true;
            }

            return false;
        }

        public override void AcceptChanges()
        {
            _status.AcceptChanges();
            _createdBy.AcceptChanges();
            _createdDate.AcceptChanges();
            _lastModifiedBy.AcceptChanges();
            _lastModifiedDate.AcceptChanges();
            _timestamp.AcceptChanges();
            base.AcceptChanges();
        }

        public override void UndoChanges()
        {
            _status.UndoChanges();
            _createdBy.UndoChanges();
            _createdDate.UndoChanges();
            _lastModifiedBy.UndoChanges();
            _lastModifiedDate.UndoChanges();
            _timestamp.UndoChanges();
            base.UndoChanges();
        }
    }
}
