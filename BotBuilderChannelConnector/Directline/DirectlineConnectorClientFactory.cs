﻿using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Builder.ChannelConnector.Directline
{
    public class DirectlineConnectorClientFactory : IConnectorClientFactory
    {
        readonly string conversationId;
        readonly IChatLog chatLog;
	    readonly string botName;

        public DirectlineConnectorClientFactory(string botName, string conversationId, IChatLog chatLog)
        {
            this.chatLog = chatLog;
            this.conversationId = conversationId;
        }

        public IConnectorClient MakeConnectorClient()
        {
            return new DirectlineConnectorClient(new DirectlineChat(botName, conversationId, chatLog));
        }

        public IStateClient MakeStateClient()
        {
            throw new NotImplementedException();
        }
    }
}
