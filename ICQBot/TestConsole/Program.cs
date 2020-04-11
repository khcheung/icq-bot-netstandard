using System;

namespace TestConsole
{
    class Program
    {
        private static ICQBot.Bot bot;
        static void Main(string[] args)
        {
            bot = new ICQBot.Bot("");
            bot.NewMessage += Bot_NewMessage;
            bot.StartPolling();
            Console.ReadKey();
        }

        private static async void Bot_NewMessage(object sender, ICQBot.Models.ICQEventArgs<ICQBot.Models.NewMessageEvent> e)
        {

            await bot.SendMessageText(e.Event.Chat.ChatId, $"Re: {e.Event.Text}");

            //await bot.SendMessageText(e.Event.Chat.ChatId, $"Re: {e.Event.Text}", new ICQBot.Models.InlineKeyboard[] { 
            //    new ICQBot.Models.InlineKeyboard() { Text = "Web", Url = "http://www.google.com" },
            //    new ICQBot.Models.InlineKeyboard() { Text = "Ok", CallbackData = "1" },
            //    new ICQBot.Models.InlineKeyboard() { Text = "Cancel", CallbackData = "2" }
            //});
        }


    }
}
