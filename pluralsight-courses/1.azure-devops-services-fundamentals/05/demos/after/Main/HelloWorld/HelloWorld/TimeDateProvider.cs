using System;

namespace HelloWorld
{
    public class TimeDateProvider
    {
        public string GetDateTime()
        {
            return DateTime.UtcNow.ToString();
        }
    }
}
