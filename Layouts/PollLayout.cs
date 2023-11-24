using System.Diagnostics;
using System.Threading.Channels;
using SlackNet.Blocks;
using SlackNet.WebApi;

namespace Layouts;

public class PollLayout{
    public static Message Generate(Dictionary<string, List<string>> votes, string channel){
         var entries = votes
            .Select(x => new List<Block>() { newButton(x.Key, x.Value.Count), newList(x.Value) })
            .SelectMany(i => i)
            .ToArray();
        
        return new Message
        {
            Channel = channel,
            Blocks = entries
        };
    }


    private static SectionBlock newButton(string text, int count)
    {
        return new SectionBlock{
            Text = new Markdown( "(" + count.ToString() + ")" ),
            Accessory = new Button{
                ActionId = text,
                Text = text
            }
        };
    }
    
    private static SectionBlock newList(List<string> voters)
    {
        return new SectionBlock
        {
            Text = string.Join(", ", voters) + " "
        };
    }

}
