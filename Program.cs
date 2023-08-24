
using SlackNet;
using SlackNet.Events;

const string FOOD_CHANNEL = "foodbottest";
const string oauthToken = "xoxb-327499485233-5762082055410-KCU8jPIjSrwQugG6L0nd7KdO";
const string socketToken = "xapp-1-A05LZNLBWET-5759319122693-2f5e68c6adcbcf5aca6460008ad0c4cb133c9a84f892e4656c59f2f561ae3232";


Console.WriteLine("Configuring...");

var options = new List<string>(){
    "Taza",
    "Broodbode",
    "Domino's"
};


FoodManager foodManager = new(options, oauthToken, socketToken);


Console.WriteLine("Connected. Press any key to exit...");
await Task.Run(Console.ReadKey);
