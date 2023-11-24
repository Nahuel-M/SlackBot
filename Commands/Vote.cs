using System.Diagnostics;
using Layouts;
using SlackNet;
using SlackNet.Blocks;
using SlackNet.Interaction;
using SlackNet.WebApi;

public class Vote : IBlockActionHandler<BlockAction>
{
    FoodManager parent;
    ISlackApiClient slack;

    public Vote(FoodManager parent, ISlackApiClient slack)
    {
        this.parent = parent;
        this.slack = slack;
    }
    public async Task Handle(BlockAction action, BlockActionRequest request)
    {
        var user = request.User.Name;
        var actionId = action.ActionId;

        if (parent.todaysVotes[actionId].Contains(user)){
            parent.todaysVotes[actionId].Remove(user);
        } else {
            parent.todaysVotes[actionId].Add(user);
        }

        await slack.Chat.Update(new MessageUpdate
        {
            ChannelId = request.Channel.Id,
            Ts = parent.votingMessageTimeStamp,
            Blocks = PollLayout.Generate(parent.todaysVotes, request.Channel.Id).Blocks
        });
    }

}