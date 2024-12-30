using System.ComponentModel.Design;

namespace CheckersGame
{
    public class Board
    {
        private char[,] m_Board;
        private int m_Size;

        public Board(int size)
        {
            m_Size = size;
            m_Board = new char[size, size];
        }

        public void InitializeBoard(Player player1, Player player2)
        {
            // הצבת חיילים של Player 1 (למשל, בשורות העליונות)
            for (int i = 0; i < 3; i++)
            {
                for (int j = (i % 2 == 0) ? 0 : 1; j < m_Size; j += 2)
                {
                    m_Board[i, j] = 'O'; // סימן לחייל של Player 1
                    player1.Pieces.Add(new Piece(i, j, PieceType.Regular, 'O'));
                }
            }

            // הצבת חיילים של Player 2 (למשל, בשורות התחתונות)
            for (int i = m_Size - 3; i < m_Size; i++)
            {
                for (int j = (i % 2 == 0) ? 0 : 1; j < m_Size; j += 2)
                {
                    m_Board[i, j] = 'X'; // סימן לחייל של Player 2
                    player2.Pieces.Add(new Piece(i, j, PieceType.Regular, 'X'));
                }
            }
        }

        public void PrintBoard(int boardSize)
        {
            if (boardSize == 6)
            {

                Console.WriteLine("    a   b   c   d   e   f   ");
            } // כותרת לעמודות
            else if (boardSize == 8)
            {
                 Console.WriteLine("    a   b   c   d   e   f   g   h  ");
            }
            else {

                Console.WriteLine("    a   b   c   d   e   f   g   h   i   j  ");
            }


            Console.WriteLine(new string('=', boardSize * 4));

            for (int i = 0; i < boardSize; i++)
            {
                Console.Write((char)('A' + i) + " ");
                for (int j = 0; j < boardSize; j++)
                {
                    char piece = m_Board[i, j];
                    Console.Write("|" + (piece == '\0' ? ' ' : piece) + "|");
                }
                Console.WriteLine("");
                Console.WriteLine(new string('=', boardSize * 4));
            }
        }

        public bool IsWithinBounds(int row, int column)
        {
            return row >= 0 && row < m_Size && column >= 0 && column < m_Size;
        }

        public bool IsCellEmpty(int row, int column)
        {
            return IsWithinBounds(row, column) && m_Board[row, column] == ' ';
        }

        public void UpdateBoard(int sourceRow, int sourceColumn, int destRow, int destColumn)
        {
            // עדכון המהלך על הלוח
            m_Board[destRow, destColumn] = m_Board[sourceRow, sourceColumn];
            m_Board[sourceRow, sourceColumn] = ' '; // הפיכת התא המקורי לריק
        }
    }
}