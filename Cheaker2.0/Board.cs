using System;


namespace Ex02
{
    public class Board
    {
        public int Size { get; private set; }
        public Piece[,] Grid { get; }

        public Board(int i_Size)
        {
            Size = i_Size;
            Grid = new Piece[i_Size, i_Size];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if ((i + j) % 2 != 0)
                    {
                        if (i < Size / 2 - 1)
                        {
                            Grid[i, j] = new Piece('O', "Player2");
                        }
                        else if (i > Size / 2)
                        {
                            Grid[i, j] = new Piece('X', "Player1");
                        }
                    }
                }
            }
        }
        public bool IsMoveValid(int i_StartRow, int i_StartCol, int i_EndRow, int i_EndCol, Player i_CurrentPlayer)
        {
            if (i_StartRow < 0 || i_StartRow >= Size || i_StartCol < 0 || i_StartCol >= Size ||
                i_EndRow < 0 || i_EndRow >= Size || i_EndCol < 0 || i_EndCol >= Size)
            {
                return false;
            }

            if (i_CurrentPlayer.Symbol == 'X')
            {
                if (!(Grid[i_StartRow, i_StartCol].IsKing) && i_EndRow > i_StartRow)
                {
                    return false;
                }
            }
            else if (i_CurrentPlayer.Symbol == 'O')
            {
                if (!(Grid[i_StartRow, i_StartCol].IsKing) && i_EndRow < i_StartRow)
                {
                    return false;
                }
            }

            if (Grid[i_StartRow, i_StartCol] == null || Grid[i_StartRow, i_StartCol].Owner != i_CurrentPlayer.Id)
            {
                return false;
            }

            if (Grid[i_EndRow, i_EndCol] != null)
            {
                return false;
            }

            int rowDiff = i_EndRow - i_StartRow;
            int colDiff = i_EndCol - i_StartCol;

            if (Math.Abs(rowDiff) == 1 && Math.Abs(colDiff) == 1)
            {
                if (!Grid[i_StartRow, i_StartCol].IsKing)
                {
                    if ((i_CurrentPlayer.Symbol == 'X' && rowDiff >= 0) || (i_CurrentPlayer.Symbol == 'O' && rowDiff <= 0))
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (Math.Abs(rowDiff) == 2 && Math.Abs(colDiff) == 2)
            {
                int middleRow = (i_StartRow + i_EndRow) / 2;
                int middleCol = (i_StartCol + i_EndCol) / 2;

                if (Grid[middleRow, middleCol] != null && Grid[middleRow, middleCol].Symbol != i_CurrentPlayer.Symbol)
                {
                    Grid[middleRow, middleCol] = null;
                    return true;
                }
            }
            return false;
        }
        public void UpdateBoard(int i_StartRow, int i_StartCol, int i_EndRow, int i_EndCol)
        {
            Grid[i_EndRow, i_EndCol] = Grid[i_StartRow, i_StartCol];
            Grid[i_StartRow, i_StartCol] = null;

            if (Math.Abs(i_EndRow - i_StartRow) == 2 && Math.Abs(i_EndCol - i_StartCol) == 2)
            {
                int middleRow = (i_StartRow + i_EndRow) / 2;
                int middleCol = (i_StartCol + i_EndCol) / 2;
                Grid[middleRow, middleCol] = null;
            }
            PromoteToKingIfApplicable(i_EndRow, i_EndCol);
        }

        private void PromoteToKingIfApplicable(int i_Row, int i_Col)
        {
            if (Grid[i_Row, i_Col] == null)
            {
                return;
            }

            if ((Grid[i_Row, i_Col].Symbol == 'X' && i_Row == 0) || (Grid[i_Row, i_Col].Symbol == 'O' && i_Row == Grid.GetLength(0) - 1))
            {
                Grid[i_Row, i_Col].PromoteToKing();
            }
        }
        public bool HasMandatoryCapture(Player i_Player)
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {

                    if (Grid[row, col] != null && Grid[row, col].Owner == i_Player.Id)
                    {
                        if (CanCapture(row, col))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool CanCapture(int i_Row, int i_Col)
        {
            if (Grid[i_Row, i_Col] == null)
            {
                return false;
            }
            int[,] directions;

            if (Grid[i_Row, i_Col].IsKing)
            {
                directions = new int[,] { { -2, -2 }, { -2, 2 }, { 2, -2 }, { 2, 2 } };
            }
            else
            {
                directions = Grid[i_Row, i_Col].Symbol == 'X'
                    ? new int[,] { { -2, -2 }, { -2, 2 } }
                    : new int[,] { { 2, -2 }, { 2, 2 } };
            }

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int targetRow = i_Row + directions[i, 0];
                int targetCol = i_Col + directions[i, 1];
                int middleRow = i_Row + directions[i, 0] / 2;
                int middleCol = i_Col + directions[i, 1] / 2;

                if (IsWithinBounds(targetRow, targetCol) && IsWithinBounds(middleRow, middleCol) &&
                    Grid[middleRow, middleCol] != null &&
                    Grid[middleRow, middleCol].Owner != Grid[i_Row, i_Col].Owner &&
                    Grid[targetRow, targetCol] == null)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsWithinBounds(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < Size && i_Col >= 0 && i_Col < Size;
        }

        public bool IsCaptureMove(int i_StartRow, int i_StartCol, int i_EndRow, int i_EndCol, Player i_Player)
        {
            int middleRow = (i_StartRow + i_EndRow) / 2;
            int middleCol = (i_StartCol + i_EndCol) / 2;
            return Math.Abs(i_EndRow - i_StartRow) == 2 &&
                   Math.Abs(i_EndCol - i_StartCol) == 2 &&
                   Grid[middleRow, middleCol] != null &&
                   Grid[middleRow, middleCol].Symbol != i_Player.Symbol &&
                   Grid[i_EndRow, i_EndCol] == null;
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

            return null;
        }

        public string[] GetAvailableMoves(string i_Player)
        {
            string[] moves = new string[Size * Size * 4];
            int moveIndex = 0;

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (Grid[row, col] != null && Grid[row, col].Owner == i_Player)
                    {
                        AddCaptureMoves(row, col, moves, ref moveIndex);
                    }
                }
            }

            string[] CapturevalidMoves = new string[moveIndex];
            Array.Copy(moves, CapturevalidMoves, moveIndex);
            if (CapturevalidMoves.Length > 0)
            {
                return CapturevalidMoves;
            }

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Piece piece = Grid[row, col];
                    if (piece != null && piece.Owner == i_Player)
                    {
                        AddRegularMoves(row, col, moves, ref moveIndex);
                    }
                }
            }
            string[] RegularvalidMoves = new string[moveIndex];
            Array.Copy(moves, RegularvalidMoves, moveIndex);
            return RegularvalidMoves;
        }
        private void AddRegularMoves(int i_Row, int i_Col, string[] i_Moves, ref int i_MoveIndex)
        {
            int[] directions = { -1, 1 };
            foreach (int dr in directions)
            {
                foreach (int dc in directions)
                {
                    int targetRow = i_Row + dr;
                    int targetCol = i_Col + dc;
                   
                    if (Grid[i_Row, i_Col].Symbol == 'O')
                    {
                        if (!(Grid[i_Row, i_Col].IsKing) && targetRow < i_Row)
                        {
                            break;
                        }
                    }

                    if (IsWithinBounds(targetRow, targetCol) && Grid[targetRow, targetCol] == null)
                    {
                        i_Moves[i_MoveIndex++] = ConvertToMoveString(i_Row, i_Col, targetRow, targetCol);
                    }
                }
            }
        }
        private void AddCaptureMoves(int i_Row, int i_Col, string[] i_Moves, ref int i_MoveIndex)
        {

            int[,] directions;

            if (Grid[i_Row, i_Col].IsKing)
            {
                directions = new int[,] { { -2, -2 }, { -2, 2 }, { 2, -2 }, { 2, 2 } };
            }
            else
            {
                directions = new int[,] { { 2, -2 }, { 2, 2 } };
            }

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int targetRow = i_Row + directions[i, 0];
                int targetCol = i_Col + directions[i, 1];
                int middleRow = i_Row + directions[i, 0] / 2;
                int middleCol = i_Col + directions[i, 1] / 2;

                if (IsWithinBounds(targetRow, targetCol) && IsWithinBounds(middleRow, middleCol) &&
                    Grid[middleRow, middleCol] != null &&
                    Grid[middleRow, middleCol].Symbol != Grid[i_Row, i_Col].Symbol &&
                    Grid[targetRow, targetCol] == null)
                {
                    i_Moves[i_MoveIndex++] = ConvertToMoveString(i_Row, i_Col, targetRow, targetCol);
                }
            }
        }
        private string ConvertToMoveString(int i_StartRow, int i_StartCol, int i_EndRow, int i_EndCol)
        {
            return $"{(char)('A' + i_StartRow)}{(char)('a' + i_StartCol)}>{(char)('A' + i_EndRow)}{(char)('a' + i_EndCol)}";
        }
    }
}