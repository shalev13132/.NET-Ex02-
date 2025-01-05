namespace Ex02
{
    public class Piece
    {
        public char Symbol { get; private set; } 
        public bool IsKing { get; private set; }
        public string Owner { get; private set; } 

        public Piece(char i_Symbol, string i_Owner)
        {
            Symbol = i_Symbol;
            IsKing = false;
            Owner = i_Owner;
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