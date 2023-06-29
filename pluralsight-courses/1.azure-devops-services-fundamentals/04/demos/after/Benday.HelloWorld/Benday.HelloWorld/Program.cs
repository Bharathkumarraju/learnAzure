using System;

namespace Benday.HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var provider = new TimeDateProvider();

            Console.WriteLine("The current time is '{0}' UTC.", 
                provider.GetDateTime());

            var luckyNumberProvider =
                new LuckyNumberProvider();

            Console.WriteLine("Here's your lucky number: {0}", 
                luckyNumberProvider.GetLuckyNumber());
        }
    }
}
