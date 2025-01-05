namespace Cheaker2
{
    public class Player
    {
        public string Name { get; }
        public char Symbol { get; }
        public string Id { get; }
        public string Move { get; internal set; }
        public int Points { get; internal set; }


        public Player(string name, string id, char symbol, int initialPoints = 0)
        {
            Name = name;
            Id = id;
            Symbol = symbol;
            Points = initialPoints;
        }
    }

}