using Destiny_Bingo_Randomizer.Interfaces;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Destiny_Bingo_Randomizer.Services
{
    public class DiscordBotService : IDiscordBotService
    {
        DiscordSocketClient _client;
        bool hasLoggedIn = false;
        private string token { get; init; }
        public DiscordBotService(IConfiguration config)
        {
            _client = new DiscordSocketClient();


            _client.Log += Log;

            token = config.GetValue<string>("DiscordApiKey") ?? "n/a";


        }

        public async Task<List<string>> GetAllMessagesInChannel(string channelID)
        {
            List<string> items = new List<string>();


            await Login();

            try
            {
                var channel = await _client.GetChannelAsync(Convert.ToUInt64(channelID)) as ITextChannel;

                var messages = await channel.GetMessagesAsync(limit: 100).FlattenAsync();

                foreach (var message in messages)
                {
                    items.Add(message.Content);
                    Console.WriteLine(message.Content);
                }
                return items;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public async Task Test()
        {
            await Login();
        }

        
        #region Private Methods

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task Login()
        {
            if (hasLoggedIn)
            {
                return;
            }
            if (token == "n/a")
            {
                return;
            }
            await _client.LoginAsync(TokenType.Bot, token);

            await _client.StartAsync();

            hasLoggedIn = true;
        }

        #endregion
    }
}
