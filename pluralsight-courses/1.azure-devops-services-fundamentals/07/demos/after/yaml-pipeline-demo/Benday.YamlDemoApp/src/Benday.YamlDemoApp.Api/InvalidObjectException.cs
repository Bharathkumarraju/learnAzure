using System;

namespace Benday.YamlDemoApp.Api
{
    /// <summary>
    /// Validation on an object instance failed.
    /// </summary>
    public class InvalidObjectException : Exception
    {
        public InvalidObjectException() { }
        public InvalidObjectException(string message) : base(message) { }
    }
}
