using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheaker2
{
    using System.Collections.Generic;

    public class Player
    {
        public string Name { get; }
        public char Symbol { get; }
        public List<Piece> Pieces { get; }

        public Player(string name, char symbol)
        {
            Name = name;
            Symbol = symbol;
            Pieces = new List<Piece>();
        }
    }

}
