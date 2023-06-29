using System;

namespace Benday.HelloWorld
{
    public class TimeDateProvider
    {
        public string GetDateTime()
        {
            return DateTime.UtcNow.ToString();
        }
    }
}
