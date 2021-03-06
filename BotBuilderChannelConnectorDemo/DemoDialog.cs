﻿using Microsoft.Bot.Builder.ConnectorEx;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Builder.ChannelConnector.Demo
{
    [Serializable]
    public class DemoDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var text = message.Text?.ToLower();

            if ("keyboard".Equals(text))
            {
                var reply = context.MakeMessage();
                reply.AddKeyboardCard("Keyboard Card example", new[] { "Option 1", "Option 2" });
                await context.PostAsync(reply);
            }
            else if ("typing".Equals(text))
            {
                var typing = context.MakeMessage();
                typing.Type = "typing";
                await context.PostAsync(typing);
            }
            else if ("location".Equals(text))
            {
                var reply = context.MakeMessage();

                reply.ChannelData = new FacebookMessage
                (
                    "Please submit your location",
                    new List<FacebookQuickReply>
                    {
                        // If content_type is location, title and payload are not used
                        // see https://developers.facebook.com/docs/messenger-platform/send-api-reference/quick-replies#fields
                        // for more information.
                        new FacebookQuickReply
                        (
                            FacebookQuickReply.ContentTypes.Location,
                            default(string),
                            default(string)
                        )
                    }
                );

                await context.PostAsync(reply);
            }
            else if ("hero".Equals(text))
            {
                var reply = context.MakeMessage();

                var heroCard = new HeroCard
                {
                    Title = "Deco!",
                    Subtitle = "I am Brasilian",
                    Images = new List<CardImage>
                    {
                        new CardImage(url: "https://upload.wikimedia.org/wikipedia/commons/5/5c/Chelsea_Deco.jpg")
                    },
                    Buttons = new List<CardAction>
                    {
                        new CardAction
                        {
                            Value = "https://de.wikipedia.org/wiki/Deco",
                            Type = "openUrl",
                            Title = "Get me to Deco"
                        },
                        new CardAction
                        {
                            Type = ActionTypes.PostBack,
                            Title = "Make me a postback",
                            Value = "Postback value"
                        }
                    }
                };

                reply.Attachments = new List<Attachment>
                {
                    heroCard.ToAttachment(),
                    heroCard.ToAttachment()
                };

                await context.PostAsync(reply);
            }
            else if ("attachment".Equals(text))
            {
                var reply = context.MakeMessage();
                reply.Attachments.Add(new Attachment
                {
                    ContentUrl = "http://aihelpwebsite.com/portals/0/Images/AIHelpWebsiteLogo_Large.png",
                    ContentType = "image/png",
                    Name = "AIHelpWebsiteLogo_Large.png"
                });
                await context.PostAsync(reply);
            }
            else
            {
                await context.PostAsync($"Sorry, I dont understand {message.Text}");
            }

            context.Wait(MessageReceivedAsync);
        }
    }
}
