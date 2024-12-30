using System;
using System.Collections.Generic;

namespace CheckersGame
{
    public class LegalMovesManager
    {
        public bool IsGameOver(Board board, Player currentPlayer, Player opponent)
        {
            // אם לאחד השחקנים אין חיילים, המשחק נגמר
            if (currentPlayer.Pieces.Count == 0 || opponent.Pieces.Count == 0)
            {
                return true;
            }

            // אם אין מהלכים חוקיים לשחקן הנוכחי, המשחק נגמר
            List<Move> legalMoves = GenerateLegalMoves(board, currentPlayer);
            return legalMoves.Count == 0;
        }

        public List<Move> GenerateLegalMoves(Board board, Player player)
        {
            List<Move> legalMoves = new List<Move>();

            foreach (Piece piece in player.Pieces)
            {
                // בדיקת מהלכים חוקיים עבור כל חייל על הלוח
                legalMoves.AddRange(GenerateMovesForPiece(board, piece));
            }

            return legalMoves;
        }

        private List<Move> GenerateMovesForPiece(Board board, Piece piece)
        {
            List<Move> moves = new List<Move>();

            // חישוב מהלכים אפשריים (למשל, אלכסון אחד קדימה)
            AddMoveIfValid(board, piece, piece.Row + 1, piece.Column + 1, moves);
            AddMoveIfValid(board, piece, piece.Row + 1, piece.Column - 1, moves);

            // אם החייל הוא מלך, ניתן גם לבדוק תנועה אחורה
            if (piece.Type == PieceType.King)
            {
                AddMoveIfValid(board, piece, piece.Row - 1, piece.Column + 1, moves);
                AddMoveIfValid(board, piece, piece.Row - 1, piece.Column - 1, moves);
            }

            return moves;
        }

        private void AddMoveIfValid(Board board, Piece piece, int destRow, int destColumn, List<Move> moves)
        {
            // בדיקה אם המהלך בטווח הלוח ואם המשבצת ריקה
            if (board.IsWithinBounds(destRow, destColumn) && board.IsCellEmpty(destRow, destColumn))
            {
                moves.Add(new Move(piece.Row, piece.Column, destRow, destColumn));
            }
        }
    }
}