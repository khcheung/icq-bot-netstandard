using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ICQBot.Bot bot = new ICQBot.Bot("");
            bot.StartPolling();
            Console.ReadKey();
        }
    }
}
