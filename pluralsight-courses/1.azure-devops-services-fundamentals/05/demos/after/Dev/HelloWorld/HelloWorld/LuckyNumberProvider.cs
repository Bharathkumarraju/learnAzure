using System;

namespace HelloWorld
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
