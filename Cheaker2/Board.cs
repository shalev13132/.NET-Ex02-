using System;


namespace Cheaker2
{
    public class Board
    {
        public int Size { get; private set; }
        public Piece[,] Grid { get; }

        public Board(int size)
        {
            Size = size;
            Grid = new Piece[size, size];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if ((i + j) % 2 != 0) // משבצות שחורות בלבד
                    {
                        if (i < Size / 2 - 1)
                        {
                            Grid[i, j] = new Piece('O', "Player2"); // שחקן 2
                        }
                        else if (i > Size / 2)
                        {
                            Grid[i, j] = new Piece('X', "Player1"); // שחקן 1
                        }
                    }
                }
            }
        }
        public bool IsMoveValid(int i_startRow, int i_startCol, int i_endRow, int i_endCol, Player i_currentPlayer)
        {
            // בדיקות גבול
            if (i_startRow < 0 || i_startRow >= Size || i_startCol < 0 || i_startCol >= Size ||
                i_endRow < 0 || i_endRow >= Size || i_endCol < 0 || i_endCol >= Size)
            {

                return false;
            }

            if (i_currentPlayer.Symbol == 'X')
            {
                if (!(Grid[i_startRow, i_startCol].IsKing) && i_endRow > i_startRow)
                {
                    return false;
                }

            }
            else if (i_currentPlayer.Symbol == 'O')
            {
                if (!(Grid[i_startRow, i_startCol].IsKing) && i_endRow < i_startRow)
                {
                    return false;
                }

            }

            // בדיקה אם המשבצת ההתחלתית שייכת לשחקן הנוכחי
            if (Grid[i_startRow, i_startCol] == null || Grid[i_startRow, i_startCol].Owner != i_currentPlayer.Id)
            {
                return false;
            }

            // בדיקה אם המשבצת הסופית ריקה
            if (Grid[i_endRow, i_endCol] != null)
            {
                return false;
            }

            // הבדיקה אם מדובר בתנועה רגילה או במהלך אכילה
            int rowDiff = i_endRow - i_startRow;
            int colDiff = i_endCol - i_startCol;

            if (Math.Abs(rowDiff) == 1 && Math.Abs(colDiff) == 1)
            {
                // תנועה רגילה: צעד אחד באלכסון
                if (!Grid[i_startRow, i_startCol].IsKing)
                {
                    // חייל רגיל: יכול לנוע רק קדימה
                    if ((i_currentPlayer.Symbol == 'X' && rowDiff >= 0) || (i_currentPlayer.Symbol == 'O' && rowDiff <= 0))
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (Math.Abs(rowDiff) == 2 && Math.Abs(colDiff) == 2)
            {
                // מהלך אכילה: שתי משבצות באלכסון
                int middleRow = (i_startRow + i_endRow) / 2;
                int middleCol = (i_startCol + i_endCol) / 2;

                if (Grid[middleRow, middleCol] != null && Grid[middleRow, middleCol].Symbol != i_currentPlayer.Symbol)
                {
                    // אכילה חוקית
                    Grid[middleRow, middleCol] = null;// הסרת כלי היריב
                    return true;
                }
            }
            return false; // אם אף תנאי לא מתקיים, המהלך אינו חוקי
        }

        public void UpdateBoard(int i_startRow, int i_startCol, int i_endRow, int i_endCol)
        {
            // העברת הכלי למשבצת הסופית
            Grid[i_endRow, i_endCol] = Grid[i_startRow, i_startCol];
            Grid[i_startRow, i_startCol] = null;

            // בדיקה אם המהלך הוא מהלך אכילה
            if (Math.Abs(i_endRow - i_startRow) == 2 && Math.Abs(i_endCol - i_startCol) == 2)
            {
                // חישוב מיקום הכלי היריב שנמצא באמצע
                int middleRow = (i_startRow + i_endRow) / 2;
                int middleCol = (i_startCol + i_endCol) / 2;

                // הסרת הכלי היריב
                Grid[middleRow, middleCol] = null;
            }

            // בדיקה אם יש צורך לקדם את הכלי למלך
            PromoteToKingIfApplicable(i_endRow, i_endCol);
        }

        private void PromoteToKingIfApplicable(int row, int col)
        {
            if (Grid[row, col] == null) return;

            // קידום למלך אם הכלי הגיע לקצה הלוח
            if ((Grid[row, col].Symbol == 'X' && row == 0) || (Grid[row, col].Symbol == 'O' && row == Grid.GetLength(0) - 1))
            {
                Grid[row, col].PromoteToKing();
            }
        }

        public bool HasMandatoryCapture(Player i_player)
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {

                    if (Grid[row, col] != null && Grid[row, col].Owner == i_player.Id)
                    {
                        // בדוק אם יש מהלך אכילה אפשרי עבור הכלי
                        if (CanCapture(row, col))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool CanCapture(int i_row, int i_col)
        {
            if (Grid[i_row, i_col] == null)
            {
                return false; // אין כלי במשבצת הנוכחית
            }
            int[,] directions;

            if (Grid[i_row, i_col].IsKing)
            {
                directions = new int[,] { { -2, -2 }, { -2, 2 }, { 2, -2 }, { 2, 2 } };
            }
            else
            {
                // חייל רגיל יכול לנוע רק קדימה
                directions = Grid[i_row, i_col].Symbol == 'X'
                    ? new int[,] { { -2, -2 }, { -2, 2 } } // חייל "X" נע כלפי מעלה
                    : new int[,] { { 2, -2 }, { 2, 2 } };  // חייל "O" נע כלפי מטה
            }

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int targetRow = i_row + directions[i, 0];
                int targetCol = i_col + directions[i, 1];
                int middleRow = i_row + directions[i, 0] / 2;
                int middleCol = i_col + directions[i, 1] / 2;

                // בדיקה אם הכלי יכול לאכול כלי יריב
                if (IsWithinBounds(targetRow, targetCol) && IsWithinBounds(middleRow, middleCol) &&
                    Grid[middleRow, middleCol] != null &&
                    Grid[middleRow, middleCol].Symbol != Grid[i_row, i_col].Symbol &&
                    Grid[targetRow, targetCol] == null)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsWithinBounds(int i_row, int i_col)
        {
            return i_row >= 0 && i_row < Size && i_col >= 0 && i_col < Size;
        }

        public bool IsCaptureMove(int i_startRow, int i_startCol, int i_endRow, int i_endCol, Player i_player)
        {
            int middleRow = (i_startRow + i_endRow) / 2;
            int middleCol = (i_startCol + i_endCol) / 2;

            return Math.Abs(i_endRow - i_startRow) == 2 &&
                   Math.Abs(i_endCol - i_startCol) == 2 &&
                   Grid[middleRow, middleCol] != null &&
                   Grid[middleRow, middleCol].Symbol != i_player.Symbol &&
                   Grid[i_endRow, i_endCol] == null;
        }

        public string CheckGameOver() 
        {
            bool player1HasMoves = false;
            bool player2HasMoves = false;

            foreach (string move in GetAvailableMoves("Player1"))
            {
                player1HasMoves = true;
                break;
            }

            foreach (string move in GetAvailableMoves("Player2"))
            {
                player2HasMoves = true;
                break;
            }

            
             if (!player1HasMoves)
            {
                return "Player2";
            }
            else if (!player2HasMoves)
            {
                return "Player1";
            }

            return null; // Game is not over
        }
        public string[] GetAvailableMoves(string player)
        {
            string[] moves = new string[Size * Size * 4]; 
            int moveIndex = 0;

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Piece piece = Grid[row,col];
                    if (piece != null && piece.Owner == player)
                    {
                        AddRegularMoves(row, col, moves, ref moveIndex);
                        AddCaptureMoves(row, col, player, moves, ref moveIndex);
                    }
                }
            }

            // Resize array to fit only valid moves
            string[] validMoves = new string[moveIndex];
            Array.Copy(moves, validMoves, moveIndex);
            return validMoves;
        }

        private void AddRegularMoves(int row, int col, string[] moves, ref int moveIndex)
        {
            int[] directions = { -1, 1 }; // Diagonal directions
            foreach (int dr in directions)
            {
                foreach (int dc in directions)
                {
                    int targetRow = row + dr;
                    int targetCol = col + dc;

                    if (IsInBounds(targetRow, targetCol) && Grid[targetRow,targetCol] == null)
                    {
                        moves[moveIndex++] = ConvertToMoveString(row, col, targetRow, targetCol);
                    }
                }
            }
        }

        private void AddCaptureMoves(int row, int col, string player, string[] moves, ref int moveIndex)
        {
            int[] directions = { -1, 1 }; // Diagonal directions
            foreach (int dr in directions)
            {
                foreach (int dc in directions)
                {
                    int midRow = row + dr;
                    int midCol = col + dc;
                    int targetRow = row + 2 * dr;
                    int targetCol = col + 2 * dc;

                    if (IsInBounds(midRow, midCol) && IsInBounds(targetRow, targetCol))
                    {
                        Piece midPiece = Grid[midRow,midCol];
                        if (midPiece != null && midPiece.Owner != player && Grid[targetRow,targetCol] == null)
                        {
                            moves[moveIndex++] = ConvertToMoveString(row, col, targetRow, targetCol);
                        }
                    }
                }
            }
        }
        private string ConvertToMoveString(int startRow, int startCol, int endRow, int endCol)
        {
            return $"{(char)('A' + startRow)}{(char)('a' + startCol)}>{(char)('A' + endRow)}{(char)('a' + endCol)}";
        }

        private bool IsInBounds(int row, int col)
        {
            return row >= 0 && row < Size && col >= 0 && col < Size;
        }
    }
}
