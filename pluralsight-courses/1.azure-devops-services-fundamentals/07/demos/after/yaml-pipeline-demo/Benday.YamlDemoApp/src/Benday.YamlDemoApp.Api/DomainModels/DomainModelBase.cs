using Benday.Common;

namespace Benday.YamlDemoApp.Api.DomainModels
{
    public abstract class DomainModelBase : IInt32Identity
    {
        private readonly DomainModelField<int> _id = new(default);
        public int Id
        {
            get => _id.Value;
            set => _id.Value = value;
        }

        public virtual bool HasChanges()
        {
            if (_id.HasChanges() == true)
            {
                return true;
            }

            return false;
        }

        public virtual void AcceptChanges()
        {
            _id.AcceptChanges();
        }

        public virtual void UndoChanges()
        {
            _id.UndoChanges();
        }
    }
}
