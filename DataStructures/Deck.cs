namespace Destiny_Bingo_Randomizer.DataStructures
{
    public class Deck<T> : List<T>
    {
        private readonly Random random = new Random();

        public Deck() { }

        public Deck(List<T> list) : base(list) { }

        public void Shuffle()
        {
            int n = this.Count;
            while (n > 1)
            {
                int k = random.Next(n--);
                T temp = this[n];
                this[n] = this[k];
                this[k] = temp;
            }
        }
    }
}
