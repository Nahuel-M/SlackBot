using SlackNet;
using SlackNet.Events;
using SlackNet.WebApi;
using WebSocket4Net.Command;

class FoodManagement : IEventHandler<MessageEvent>
{
    private readonly ISlackApiClient _slack;
    public FoodManagement(ISlackApiClient slack) => _slack = slack;

    public async Task Handle(MessageEvent slackEvent)
    {
        if ((await _slack.Conversations.Info(slackEvent.Channel)).Name != "foodbottest")
            return;
        if (slackEvent.Text == null)
            return;
        string text = slackEvent.Text;

        if (slackEvent.Text?.Contains("ping", StringComparison.OrdinalIgnoreCase) == true)
        {
            Console.WriteLine($"Received ping from {(await _slack.Users.Info(slackEvent.User)).Name} in the {(await _slack.Conversations.Info(slackEvent.Channel)).Name} channel");
            
            await _slack.Chat.PostMessage(new Message
                {
                    Text = "pong",
                    Channel = slackEvent.Channel
                });
        }
    }
}