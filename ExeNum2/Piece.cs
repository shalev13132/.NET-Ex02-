using System;

namespace CheckersGame
{
    public class Piece
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public PieceType Type { get; set; }
        public char Symbol { get; private set; }

        public Piece(int row, int column, PieceType type, char symbol)
        {
            Row = row;
            Column = column;
            Type = type;
            Symbol = symbol;
        }

        public void PromoteToKing()
        {
            if (Type == PieceType.Regular)
            {
                Type = PieceType.King;
                Symbol = char.ToUpper(Symbol);
            }
        }
    }
}