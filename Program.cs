// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema.Teams;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Microsoft.BotBuilderSamples
{
    public class Program
    {
        [Obsolete]
        async static Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            const string url = " https://smba.trafficmanager.net/emea/";
            const string appId = "10f2c6ee-b4a8-4bc4-a871-c39f7c652577";
            const string appPassword = "qJI8Q~gU.~~A5fb~NQECptgWAU5hQhYCI0aLRaDW";
            MicrosoftAppCredentials.TrustServiceUrl(url);
            var client = new ConnectorClient(new Uri(url), appId, appPassword);
            // Create or get existing chat conversation with user
            var parameters = new ConversationParameters
            {
                Bot = new ChannelAccount("28:10f2c6ee-b4a8-4bc4-a871-c39f7c652577"),
                Members = new[] { new ChannelAccount("29:1VCZRJNwIlly9hfb5dS3wjigxETakM7j7eh-EcZhoaTMPxeGD5T1YLEU_hBmvZUZF4bEw_3a8gjWmDGWIPsf82w") },
                ChannelData = new TeamsChannelData
                {
                    Tenant = new TenantInfo("603439c3-58ad-4a91-8ed3-b53e9a8677b3"),
                },
            };
            var response = await client.Conversations.CreateConversationAsync(parameters);
            // Construct the message to post to conversation
            var newActivity = new Activity
            {
                Text = "Hello",
                Type = ActivityTypes.Message,
                Conversation = new ConversationAccount
                {
                    Id = response.Id
                },
            };
            // Post the message to chat conversation with user
            await client.Conversations.SendToConversationAsync(response.Id, newActivity);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging((logging) =>
                    {
                        logging.AddDebug();
                        logging.AddConsole();
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
