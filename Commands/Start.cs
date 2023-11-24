using Layouts;
using SlackNet;
using SlackNet.Blocks;
using SlackNet.Interaction;
using SlackNet.WebApi;
using Button = SlackNet.Blocks.Button;

public class Start : ISlashCommandHandler
{
    FoodManager parent;
    ISlackApiClient slack;

    public Start(FoodManager parent, ISlackApiClient slack)
    {
        this.parent = parent;
        this.slack = slack;
    }

    public async Task<SlashCommandResponse> Handle(SlashCommand command)
    {
        var response = await this.slack.Chat.PostMessage(PollLayout.Generate(parent.todaysVotes, command.ChannelName));
        parent.votingMessageTimeStamp = response.Ts;
        return new SlashCommandResponse
        {
            Message = new Message
            {
                Text = "Starting ordering process"
            }
        };
    }
}
