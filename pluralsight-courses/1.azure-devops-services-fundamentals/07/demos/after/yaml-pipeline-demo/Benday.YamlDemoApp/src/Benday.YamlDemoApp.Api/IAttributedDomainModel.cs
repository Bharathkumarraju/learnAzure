using System.Collections.Generic;
using Benday.YamlDemoApp.Api.DomainModels;

namespace Benday.YamlDemoApp.Api
{
    public interface IAttributedDomainModel
    {
        List<DomainModelBase> GetAttributes();
        string GetAttributeValue(string key);
        void SetAttributeValue(
            string key, string value,
            string status = ApiConstants.DefaultAttributeStatus);
    }
}
