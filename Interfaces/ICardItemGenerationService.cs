using Destiny_Bingo_Randomizer.DataStructures;

namespace Destiny_Bingo_Randomizer.Services
{
    public interface ICardItemGenerationService
    {
        public List<string> GenerateCardOptions(int width, int height, Deck<string> deck = null);
    }
}
