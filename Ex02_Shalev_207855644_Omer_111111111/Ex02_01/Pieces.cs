using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02_01
{
    public class Pieces
    {
        public char Sign { get; private set; } // 'X', 'O', or ' ' (empty)
        public bool IsKing { get; private set; }
        public string Owner { get; private set; } // "Player1" or "Player2" or "Computer"

        public Pieces(char i_Sign, string owner)
        {
            Sign = i_Sign;
            Owner = owner;
            IsKing = false;
        }

        public void PromoteToKing()
        {
            IsKing = true;
            Sign = char.ToUpper(Sign); // Converts 'x'/'o' to 'X'/'O'
        }


    }
}
