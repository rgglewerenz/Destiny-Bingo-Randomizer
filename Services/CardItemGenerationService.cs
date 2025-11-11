
using Destiny_Bingo_Randomizer.DataStructures;
using System.Reflection;

namespace Destiny_Bingo_Randomizer.Services
{
    public class CardItemGenerationService : ICardItemGenerationService
    {
        private readonly Random random;
        private const string PATH_TO_CARD_ITEMS = "wwwroot\\Data\\carditems.csv";
        private const string FREE_SPACE_IDENTIFIER = "{Free Space}";

        public CardItemGenerationService() {
            random = new Random();

        }

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

            var freeSpaces = GetFreeSpaceOptions(deck);
            deck = RemoveDuplicates(deck);

            deck.Shuffle();

            if(freeSpaces.Count == 0)
            {
                return deck.Take(width * height).ToList();
            }


            var freeSpace = freeSpaces[random.Next(0, freeSpaces.Count)];
            deck.RemoveAll(x => x.Contains(freeSpace));
            var otheritems = deck.Take((width * height) - 1).ToList();
            otheritems.Insert(otheritems.Count / 2, freeSpace);

            return otheritems;
        }


        private Deck<string> GetFreeSpaceOptions(Deck<string> items)
        {
            var freeSpaces = new Deck<string>();
            foreach (var item in items)
            {
                if (item.Contains(FREE_SPACE_IDENTIFIER))
                {
                    freeSpaces.Add(item.Replace(FREE_SPACE_IDENTIFIER, ""));
                }
            }

            return freeSpaces;
        }

        private Deck<string> RemoveDuplicates(Deck<string> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                for (int j = 0; j < items.Count; j++)
                {
                    if (i == j)
                        continue;
                    if (items[i].Contains(items[j]))
                    {
                        items.RemoveAt(i);
                        i--;
                        j--;
                    }
                }
            }
            return items;
        }
    }
}
