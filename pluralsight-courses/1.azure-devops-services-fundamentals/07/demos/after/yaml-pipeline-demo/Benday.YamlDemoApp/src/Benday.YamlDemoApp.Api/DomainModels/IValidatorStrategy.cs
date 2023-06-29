namespace Benday.YamlDemoApp.Api.DomainModels
{
    public interface IValidatorStrategy<T>
    {
        bool IsValid(T validateThis);
    }
}
