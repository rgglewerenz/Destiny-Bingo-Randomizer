
namespace Destiny_Bingo_Randomizer.Interfaces
{
    public interface IDiscordBotService
    {
        Task<List<string>> GetAllMessagesInChannel(string channelID);
        public Task Test();
    }
}
