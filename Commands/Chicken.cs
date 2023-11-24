using SlackNet.Interaction;
using SlackNet.WebApi;

public class Chicken : ISlashCommandHandler
{
    FoodManager parent;

    public Chicken(FoodManager parent)
    {
        this.parent = parent;
    }

    public Task<SlashCommandResponse> Handle(SlashCommand command)
    {
        this.parent.todaysChickens.Add(command.UserName);
        return Task.FromResult(new SlashCommandResponse
            {
                Message = new Message
                    {
                        Text = "You have chickened out of ordering today"
                    }
            });
    }
}