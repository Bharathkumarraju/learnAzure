using System.Collections.Generic;
using System.Linq;

namespace Benday.YamlDemoApp.Api.DomainModels
{
    public partial class AttributedDomainModelBase<T> : CoreFieldsDomainModelBase, IAttributedDomainModel
        where T : CoreFieldsDomainModelBase, IDomainModelAttribute, new()
    {
        private List<T> _attributes;
        public List<T> Attributes
        {
            get
            {
                if (_attributes == null)
                {
                    _attributes = new List<T>();
                }

                return _attributes;
            }
            set => _attributes = value;
        }

        public List<DomainModelBase> GetAttributes()
        {
            return new List<DomainModelBase>(Attributes);
        }

        public string GetAttributeValue(string key)
        {
            var match = (from temp in Attributes
                         where temp.AttributeKey == key
                         select temp).FirstOrDefault();

            if (match == null)
            {
                return null;
            }
            else
            {
                return match.AttributeValue;
            }
        }

        public void SetAttributeValue(string key, string value,
            string status = ApiConstants.DefaultAttributeStatus)
        {
            var match = (from temp in Attributes
                         where temp.AttributeKey == key
                         select temp).FirstOrDefault();

            if (match == null)
            {
                match = new T
                {
                    AttributeKey = key,
                    AttributeValue = value
                };

                Attributes.Add(match);
            }
            else
            {
                match.AttributeValue = value;
            }

            match.Status = status;
        }
    }
}
