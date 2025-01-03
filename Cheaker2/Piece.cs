using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheaker2
{
    public class Piece
    {
        public char Symbol { get; private set; } // X או O
        public bool IsKing { get; private set; }
        public string Owner { get; private set; } // שם הבעלים של הכלי

        public Piece(char symbol, string owner)
        {
            Symbol = symbol;
            IsKing = false;
            Owner = owner;
        }

        public void PromoteToKing()
        {
            IsKing = true;
            Symbol = char.ToUpper(Symbol); // X או O הופך ל-X או O באות גדולה
        }
    }

}
