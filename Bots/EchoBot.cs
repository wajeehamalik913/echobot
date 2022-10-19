// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;

namespace Microsoft.BotBuilderSamples.Bots
{
    public class EchoBot : ActivityHandler
    {

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var teamConversationData = turnContext.Activity.GetChannelData<TeamsChannelData>();
            await turnContext.SendActivityAsync(MessageFactory.Text("Your ID is " + turnContext.Activity.From.Id), cancellationToken);
            await turnContext.SendActivityAsync(MessageFactory.Text("My ID is " + turnContext.Activity.Recipient.Id), cancellationToken);
            await turnContext.SendActivityAsync(MessageFactory.Text("Tenant ID is " + teamConversationData.Tenant.Id));
            await turnContext.SendActivityAsync(MessageFactory.Text("Service URL is " + turnContext.Activity.ServiceUrl));
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
