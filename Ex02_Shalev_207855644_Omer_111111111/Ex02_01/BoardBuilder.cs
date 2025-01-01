using System;

namespace Ex02_01
{
    public class BoardBuilder
    {
        public int Size { get; private set; }
        public Pieces[][] Grid { get; private set; }

        public BoardBuilder(int size)
        {
            Size = size;
            Grid = new Pieces[Size][];
            InitializeBoardBuilder();
        }

        private void InitializeBoardBuilder()
        {
            for (int row = 0; row < Size; row++)
            {
                Grid[row] = new Pieces[Size];
                for (int col = 0; col < Size; col++)
                {
                    if ((row + col) % 2 != 0) // Only place Piecess on black squares
                    {
                        if (row < Size / 2 - 1)
                            Grid[row][col] = new Pieces('O', "Player2");
                        else if (row > Size / 2)
                            Grid[row][col] = new Pieces('X', "Player1");
                        else
                            Grid[row][col] = null; // Empty square
                    }
                    else
                    {
                        Grid[row][col] = null; // White square
                    }
                }
            }
        }


        public bool IsMoveValid(string i_PlayerMove, char i_PlayerSign)
        {
            if (string.IsNullOrWhiteSpace(i_PlayerMove) || i_PlayerMove.Length != 5 || i_PlayerMove[2] != '>')
            {
                return false;
            }
            int startRow = i_PlayerMove[0] - 'A';
            int startCol = i_PlayerMove[1] - 'a';
            int endRow = i_PlayerMove[3] - 'A';
            int endCol = i_PlayerMove[4] - 'a';
            string[] EatingMoves= GetAvailableMoves(i_PlayerSign);
            
            if (EatingMoves.Length != 0)
            {
                foreach (string move in EatingMoves)
                {
                    if (move == i_PlayerMove)
                    {
                        RemoveEatedPiece(startRow, startCol, endRow, endCol);
                        UpdateBoardBuilder(startRow, startCol, endRow, endCol);
                        return true;
                    }
                }
                return false;
            }


            if (startRow < 0 || startRow >= Size || startCol < 0 || startCol >= Size || endRow < 0 || endRow >= Size || endCol < 0 || endCol >= Size)
            {
                return false;
            }


            if (startRow == endRow || startCol == endCol || Grid[startRow][startCol].Sign != i_PlayerSign || Grid[startRow][startCol] == null || Grid[endRow][endCol] != null || (endCol != startCol + 1 && endCol != startCol - 1))// צריך לעבור למקרה של אכילה 
            {
                return false;
            }

            if (i_PlayerSign == 'X')
            {
                if (!(Grid[startRow][startCol].IsKing) && endRow > startRow)
                {
                    return false;
                }
                if (endRow + 1 != startRow)
                {
                    return false;
                }
            }
            else
            {
                if (!(Grid[startRow][startCol].IsKing) && endRow < startRow)
                {
                    return false;
                }
                if (endRow != startRow + 1)
                {
                    return false;
                }
            }
           

            UpdateBoardBuilder(startRow, startCol, endRow, endCol);
            return true;
        }


        private void UpdateBoardBuilder(int startRow, int startCol, int endRow, int endCol)
        {
            // Move the Pieces
            Grid[endRow][endCol] = Grid[startRow][startCol];
            Grid[startRow][startCol] = null;
        }

        public string[] GetAvailableMoves(char player)
        {
            string[] moves = new string[Size * Size * 4]; 
            int moveIndex = 0;

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Pieces piece = Grid[row][col];
                    if (piece != null && piece.Sign == player)
                    {
                        //AddRegularMoves(row, col, moves, ref moveIndex);
                        AddCaptureMoves(row, col, player, moves, ref moveIndex);
                    }
                }
            }
            string[] validMoves = new string[moveIndex];
            Array.Copy(moves, validMoves, moveIndex);
            return validMoves;
        }

        private void RemoveEatedPiece(int startRow,int startCol,int endRow,int endCol)
        {
            int midRow = (startRow + endRow) / 2;
            int midCol = (startCol + endCol) / 2;

            if (Math.Abs(startRow - endRow) == 2 && Math.Abs(startCol - endCol) == 2)
            {
                Grid[midRow][midCol] = null;
            }
        }

        private void AddCaptureMoves(int row, int col, char player, string[] moves, ref int moveIndex)
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
                        Pieces midPiece = Grid[midRow][midCol];
                        if (midPiece != null && midPiece.Sign != player && Grid[targetRow][targetCol] == null)
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