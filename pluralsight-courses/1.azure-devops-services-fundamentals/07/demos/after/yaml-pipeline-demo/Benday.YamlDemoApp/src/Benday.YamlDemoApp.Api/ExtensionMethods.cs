using System.Globalization;

namespace Benday.YamlDemoApp.Api
{
    public static partial class ExtensionMethods
    {
        public static string SafeToString(this string value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else
            {
                return value;
            }
        }

        public static string SafeToString(this string value, string defaultValue)
        {
            if (value == null)
            {
                return defaultValue;
            }
            else
            {
                return value;
            }
        }

        public static int SafeToInt32(this string value, int defaultValue)
        {
            var valueAsString = value.SafeToString();

            if (value == string.Empty)
            {
                return defaultValue;
            }
            else
            {
                if (int.TryParse(valueAsString, out var returnValue) == true)
                {
                    return returnValue;
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        public static bool IsNullOrWhitespace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string ToStringUsdCurrency(this float value)
        {
            return string.Format(new CultureInfo("en-US"), "{0:C}", value);
        }
    }
}
