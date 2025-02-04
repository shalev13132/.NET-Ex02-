﻿

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
            if (!IsKing)
            {
                IsKing = true;
                if (Symbol == 'X')
                {
                    Symbol = 'K';
                }
                else if (Symbol == 'O')
                {
                    Symbol = 'U';
                }
            }
        }
    }
}
