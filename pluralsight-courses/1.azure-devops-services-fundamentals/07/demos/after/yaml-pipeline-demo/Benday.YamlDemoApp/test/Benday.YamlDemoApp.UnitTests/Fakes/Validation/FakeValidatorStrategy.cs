using Benday.YamlDemoApp.Api.DomainModels;

namespace Benday.YamlDemoApp.UnitTests.Fakes.Validation
{
    public class FakeValidatorStrategy<T> : IValidatorStrategy<T>
    {
        public bool IsValidReturnValue { get; set; }

        public bool IsValid(T validateThis)
        {
            return IsValidReturnValue;
        }
    }
}
