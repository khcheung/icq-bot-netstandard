using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICQBot.Models
{
    public class SendMessageTextResponse : ResponseBase
    {
        [JsonProperty("msgId")]
        public String MessageId { get; set; }
    }
}
