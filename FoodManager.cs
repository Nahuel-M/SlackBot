using SlackNet;

public class FoodManager{
    // Client
    ISlackSocketModeClient client;

    // State
    public List<string> todaysChickens;
    public Dictionary<string, List<string>> todaysVotes;

    public FoodManager(List<string> options, string oauthToken, string socketToken){
        this.todaysVotes = options.ToDictionary(option => option, _ => new List<string>());
        this.todaysChickens = new List<string>();

        var slackServices = new SlackServiceBuilder()
            .UseApiToken(oauthToken) // This gets used by the API client
            .UseAppLevelToken(socketToken) // This gets used by the socket mode client
            .RegisterSlashCommandHandler("/chicken", ctx => new Chicken(this))
            .RegisterSlashCommandHandler("/start", ctx => new Start(this, ctx.ServiceProvider.GetApiClient()));
            
        this.client = slackServices.GetSocketModeClient();
        this.client.Connect().Wait();
        Console.WriteLine("Connected to Slack");
    }



}