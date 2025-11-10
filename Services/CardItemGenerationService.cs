
using Destiny_Bingo_Randomizer.DataStructures;
using System.Reflection;

namespace Destiny_Bingo_Randomizer.Services
{
    public class CardItemGenerationService : ICardItemGenerationService
    {
        private readonly Random random;
        private const string PATH_TO_CARD_ITEMS = "wwwroot\\Data\\carditems.csv";

        public CardItemGenerationService() { }

        public List<string> GenerateCardOptions(int width, int height, Deck<string> deck = null)
        {
            if(deck == null)
            {
                var items = File.ReadAllText(PATH_TO_CARD_ITEMS);

                deck = new Deck<string>(items.Split(',').ToList());

                if (string.IsNullOrEmpty(deck[deck.Count - 1]))
                {
                    deck.RemoveAt(deck.Count - 1);
                }
            }
            

            deck.Shuffle();

            for (int i = 0; i < deck.Count; i++)
            {
                for (int j = 0; j < deck.Count; j++)
                {
                    
                    if (i == j)
                        continue;
                    if (deck[i].Contains(deck[j]))
                    {
                        deck.RemoveAt(i);
                        i--;
                        j--;
                        Console.WriteLine(deck[i] + " = " + deck[j]);
                    }
                    
                }
            }

            return deck.Take(width * height).ToList();
        }
    }
}
