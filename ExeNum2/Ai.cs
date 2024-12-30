using System;
using System.Collections.Generic;

namespace CheckersGame
{
    public class AI
    {
        private Random m_Random;

        public AI()
        {
            m_Random = new Random();
        }

        public Move SelectMove(List<Move> legalMoves)
        {
            if (legalMoves == null || legalMoves.Count == 0)
            {
                throw new InvalidOperationException("No legal moves available.");
            }

            int index = m_Random.Next(legalMoves.Count);
            return legalMoves[index];
        }

        public List<Move> GenerateLegalMoves(Board board, Player player)
        {
            List<Move> legalMoves = new List<Move>();

            foreach (Piece piece in player.Pieces)
            {
                // בדיקת מהלכים חוקיים עבור כל חייל
                // לדוגמה: תנועה קדימה, אכילה וכו'.
                // יש להוסיף את המימוש המלא לבדיקת חוקיות לפי מצב הלוח.
            }

            return legalMoves;
        }
    }
}