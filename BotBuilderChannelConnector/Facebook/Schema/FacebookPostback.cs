﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Builder.ChannelConnector.Facebook.Schema
{
    public class FacebookPostback
    {
        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}
