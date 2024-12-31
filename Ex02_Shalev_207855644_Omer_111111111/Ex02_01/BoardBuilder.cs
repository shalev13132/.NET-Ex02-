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


        public bool IsMoveValid(string i_PlayerMove,char i_PlayerSign)
        {
            int startRow;
            int startCol;
            int endRow;
            int endCol;
            bool inValid=false;

            if (string.IsNullOrWhiteSpace(i_PlayerMove) || i_PlayerMove.Length != 5 || i_PlayerMove[2] != '>')
            {
                return false;
            }
            
            startRow = i_PlayerMove[0] - 'A';
            startCol = i_PlayerMove[1] - 'a';
            endRow = i_PlayerMove[3] - 'A';
            endCol = i_PlayerMove[4] - 'a';
               
            // Basic boundary checks
            
            if (startRow < 0 || startRow >= Size || startCol < 0 || startCol >= Size ||
                endRow < 0 || endRow >= Size || endCol < 0 || endCol >= Size)
                return false;


            if (Grid[startRow][startCol] == null || Grid[endRow][endCol] != null)
                return false;

            
            
            if (startRow == endRow || startCol == endCol || Grid[startRow][startCol].Sign != i_PlayerSign)
            {
                return false;
            }
            if (i_PlayerSign == 'X')
            {
                if (!(Grid[startRow][startCol].IsKing) && endRow>startRow)
                {
                    return false;
                }
                if (endRow+1 != startRow )
                    return false;
                       
            }
            else
            {
                if (!(Grid[startRow][startCol].IsKing) && endRow < startRow)
                {
                    return false;
                }
                if (endRow!= startRow+1)
                    return false;
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

        public bool IsEatingMove()
        {


            return true;
        }

    }
}