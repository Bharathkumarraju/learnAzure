using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Benday.YamlDemoApp.Api.DomainModels
{
    public class DefaultValidatorStrategy<T> : IValidatorStrategy<T>
    {
        public bool IsValid(T validateThis)
        {
            var results = Validate(validateThis);

            if (results.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [SuppressMessage("csharp", "IDE0060")]
        [SuppressMessage("csharp", "CA1822")]
        private IList<ValidationResult> Validate(T model)
        {
            var results = new List<ValidationResult>();

            var context = new ValidationContext(model);

            Validator.TryValidateObject(
            model, context, results, true);

            return results;
        }
    }
}
