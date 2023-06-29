namespace Benday.YamlDemoApp.Api.DomainModels
{
    public class DomainModelAttributeBase : CoreFieldsDomainModelBase, IDomainModelAttribute
    {
        private readonly DomainModelField<string> _attributeKey = new(default);
        public string AttributeKey
        {
            get => _attributeKey.Value;
            set => _attributeKey.Value = value;
        }

        private readonly DomainModelField<string> _attributeValue = new(default);
        public string AttributeValue
        {
            get => _attributeValue.Value;
            set => _attributeValue.Value = value;
        }

        public override bool HasChanges()
        {
            if (base.HasChanges() == true)
            {
                return true;
            }

            if (_attributeKey.HasChanges() == true)
            {
                return true;
            }
            if (_attributeValue.HasChanges() == true)
            {
                return true;
            }


            return false;
        }

        public override void AcceptChanges()
        {
            base.AcceptChanges();

            _attributeKey.AcceptChanges();
            _attributeValue.AcceptChanges();

        }

        public override void UndoChanges()
        {
            base.UndoChanges();

            _attributeKey.UndoChanges();
            _attributeValue.UndoChanges();
        }
    }
}
