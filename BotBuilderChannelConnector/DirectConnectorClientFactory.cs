﻿using Microsoft.Bot.Builder.Dialogs.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Connector;
using Bot.Builder.ChannelConnector.Facebook;

namespace Bot.Builder.ChannelConnector
{
    public class DirectConnectorClientFactory : IConnectorClientFactory
    {
        readonly FacebookConfig config;

        public DirectConnectorClientFactory(FacebookConfig config)
        {
            this.config = config;
        }

        public IConnectorClient MakeConnectorClient()
        {
            return new DirectConnectorClient(new FacebookClient(config.PageAccessToken));
        }

        public IStateClient MakeStateClient()
        {
            throw new NotImplementedException();
        }
    }
}