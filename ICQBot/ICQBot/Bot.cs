using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ICQBot
{
    public class Bot
    {
        private String mBotToken = null;
        private String mAPIUrl = "https://api.icq.net/bot/v1";
        public Bot(String botToken)
        {
            this.mBotToken = botToken;
        }

        public void StartPolling()
        {
            String url = $"{mAPIUrl}/events/get";
            _ = Task.Run(async () =>
            {
                Int64 lastEventId = 0;
                Dictionary<String, String> parameters = new Dictionary<string, string>();
                parameters.Add("token", mBotToken);
                parameters.Add("lastEventId", lastEventId.ToString());
                parameters.Add("pollTime", "30");

                HttpClient client = new HttpClient();
                while (true)
                {
                    var query = String.Join("&", parameters.Where(kv => !String.IsNullOrEmpty(kv.Value)).Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}").ToArray());
                    var response = await client.GetAsync($"{url}?{query}");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine(responseString);
                        var responseObject = JsonConvert.DeserializeObject<Models.Events>(responseString);
                        if (responseObject.Ok)
                        {
                            if (responseObject.Items.Length > 0)
                            {
                                lastEventId = responseObject.Items.Max(i => i.EventId);
                                parameters["lastEventId"] = lastEventId.ToString();

                                foreach (var e in responseObject.Items)
                                {
                                    switch(e.EventType)
                                    {
                                        case Models.EventItemType.newMessage:
                                            var newMessage = JsonConvert.DeserializeObject<Models.NewMessageEvent>(e.Payload.Value.ToString());
                                            await this.SendMessageText(newMessage.Chat.ChatId, $"Re: {newMessage.Text}");
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            });
        }


        public async Task SendMessageText(String chatId, String text)
        {
            HttpClient client = new HttpClient();

            String url = $"{mAPIUrl}/messages/sendText";

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            parameters.Add("token", mBotToken);
            parameters.Add("chatId", chatId);
            parameters.Add("text", text);

            var query = String.Join("&", parameters.Where(kv => !String.IsNullOrEmpty(kv.Value)).Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}").ToArray());
            var response = await client.GetAsync($"{url}?{query}");
            var responseString = await response.Content.ReadAsStringAsync();            
            var responseObject = JsonConvert.DeserializeObject<Models.SendMessageTextResponse>(responseString);


        }

    }
}
