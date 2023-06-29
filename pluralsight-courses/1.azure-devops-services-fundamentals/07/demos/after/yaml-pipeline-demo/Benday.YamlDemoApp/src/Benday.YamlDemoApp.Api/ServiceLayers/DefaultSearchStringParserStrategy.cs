using System;

namespace Benday.YamlDemoApp.Api.ServiceLayers
{
    public class DefaultSearchStringParserStrategy : ISearchStringParserStrategy
    {
        private readonly string _semiColonDelimiter = ";";
        private readonly string _commaDelimiter = ",";

        public string[] Parse(string parseThis)
        {
            if (parseThis == null)
            {
                return Array.Empty<string>();
            }
            else
            {
                parseThis = parseThis.Trim();

                if (parseThis.Length == 0 ||
                parseThis.Replace(_semiColonDelimiter, string.Empty)
                .Replace(_commaDelimiter, string.Empty).Length == 0)
                {
                    return Array.Empty<string>();
                }
                else
                {
                    return ParseNonEmptySearch(parseThis);
                }
            }
        }

        private string[] ParseNonEmptySearch(string parseThis)
        {
            var tokens = parseThis.Split(
            new string[] { _semiColonDelimiter, _commaDelimiter },
            StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Trim();
            }

            return tokens;
        }
    }
}
