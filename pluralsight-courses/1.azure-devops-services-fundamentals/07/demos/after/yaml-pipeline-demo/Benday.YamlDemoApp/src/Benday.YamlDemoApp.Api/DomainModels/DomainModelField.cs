namespace Benday.YamlDemoApp.Api.DomainModels
{
    public class DomainModelField<T>
    {
        private T _originalValue;

        public DomainModelField(T value)
        {
            Value = value;
            AcceptChanges();
        }

        public bool HasChanges()
        {
            if (Value == null && _originalValue != null)
            {
                return true;
            }
            else if (Value != null && _originalValue == null)
            {
                return true;
            }
            else if (Value == null && _originalValue == null)
            {
                return false;
            }
            else if (Value.Equals(_originalValue) == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AcceptChanges()
        {
            _originalValue = Value;
        }

        public void UndoChanges()
        {
            Value = _originalValue;
        }

        public T Value { get; set; }
    }
}
