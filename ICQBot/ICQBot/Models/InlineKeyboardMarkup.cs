using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICQBot.Models
{  

    public class InlineKeyboard
    {
        [JsonProperty("text")]
        public String Text { get; set; }
        [JsonProperty("url", NullValueHandling =  NullValueHandling.Ignore)]
        public String Url { get; set; }
        [JsonProperty("callbackData", NullValueHandling = NullValueHandling.Ignore)]
        public String CallbackData { get; set; }
    }
}
