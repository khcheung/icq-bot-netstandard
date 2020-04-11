using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICQBot.Models
{
    public class ResponseBase
    {
        [JsonProperty("ok")]
        public Boolean Ok { get; set; }
    }
    public class Events : ResponseBase
    {
        [JsonProperty("events")]
        public EventItem[] Items { get; set; }
    }

    public class EventItem
    {
        [JsonProperty("eventId")]
        public Int64 EventId { get; set; }

        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public EventItemType EventType { get; set; }

        [JsonProperty("payload")]
        public JRaw Payload { get; set; }
    }

    public enum EventItemType
    {
        newMessage,
        editedMessage,
        deletedMessage,
        pinnedMessage,
        unpinnedMessage,
        newChatMembers,
        leftChatMembers,
        callbackQuery
    }

    public class NewMessageEvent
    {
        [JsonProperty("msgId")]
        public String MessageId { get; set; }

        [JsonProperty("chat")]
        public ChatInfo Chat { get; set; }

        [JsonProperty("text")]
        public String Text { get; set; }
    }

    public class ChatInfo
    {
        [JsonProperty("chatId")]
        public String ChatId { get; set; }

        [JsonProperty("type")]
        public String Type { get; set; }
        [JsonProperty("title")]
        public String Title { get; set; }

    }
}
