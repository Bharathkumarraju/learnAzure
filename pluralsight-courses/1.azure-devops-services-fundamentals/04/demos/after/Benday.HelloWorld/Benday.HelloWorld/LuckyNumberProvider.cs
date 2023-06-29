using System;

namespace Benday.HelloWorld
{
    public class LuckyNumberProvider
    {

        public int GetLuckyNumber()
        {
            var rnd = new Random();

            var returnValue = rnd.Next();

            return returnValue;
        }

    }
}
