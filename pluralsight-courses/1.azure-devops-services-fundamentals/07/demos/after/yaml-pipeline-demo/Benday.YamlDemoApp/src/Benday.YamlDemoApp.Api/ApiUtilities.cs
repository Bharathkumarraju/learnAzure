namespace Benday.YamlDemoApp.Api
{
    public static partial class ApiUtilities
    {
        public static void ThrowValidationException(string message)
        {
            throw new InvalidObjectException(message);
        }

        public static void ThrowUnknownObjectException(string unknownItemType, int unknownId)
        {
            throw new UnknownObjectException(
            $"Could not locate an '{unknownItemType}' item with an id of '{unknownId}'."
            );
        }

        internal static string SafeToString(string value, string returnThisIfNull)
        {
            if (string.IsNullOrWhiteSpace(value) == false)
            {
                return value;
            }
            else
            {
                return returnThisIfNull;
            }
        }
    }
}
