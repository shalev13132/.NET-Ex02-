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


        public bool IsMoveValid(string i_PlayerMove, Players currentPlayer)
        {
            int startRow = i_PlayerMove[0] - 'A';
            int startCol = i_PlayerMove[1] - 'a';
            int endRow = i_PlayerMove[3] - 'A';
            int endCol = i_PlayerMove[4] - 'a';

            // בדיקות גבול
            if (startRow < 0 || startRow >= Size || startCol < 0 || startCol >= Size ||
                endRow < 0 || endRow >= Size || endCol < 0 || endCol >= Size)
            {
                return false;
            }

            // הבדיקה שהמשבצת ההתחלתית שייכת לשחקן הנוכחי
            Pieces movingPiece = Grid[startRow][startCol];
            if (movingPiece == null || movingPiece.Symbol != currentPlayer.GameSign)
            {
                return false;
            }

            // בדיקה אם זו תנועה רגילה או מהלך אכילה
            if ((endRow - startRow) == 1 && (endCol - startCol) == 1)
            {
                // תנועה רגילה
                UpdateBoardBuilder(startRow, startCol, endRow, endCol);
                return Grid[endRow][endCol] == null;
            }
            else if (endRow - startRow == 2 && (endCol - startCol) == 2)
            {
                // מהלך אכילה
                int middleRow = (startRow + endRow) / 2;
                int middleCol = (startCol + endCol) / 2;

                Pieces middlePiece = Grid[middleRow][middleCol];

                // בדיקה אם יש כלי יריב באמצע
                if (middlePiece != null && middlePiece.Symbol != currentPlayer.GameSign &&
                    Grid[endRow][endCol] == null)
                {
                    // הסרת כלי היריב
                    Grid[middleRow][middleCol] = null;
                    UpdateBoardBuilder(startRow, startCol, endRow, endCol);
                    return true;
                }

            }

            return false;  // אם אף תנאי לא מתקיים
        }



        /* public bool IsMoveValid(string i_PlayerMove, Players currentPlayer)
         {
             int startRow;
             int startCol;
             int endRow;
             int endCol;



             if (string.IsNullOrWhiteSpace(i_PlayerMove) || i_PlayerMove.Length != 5 || i_PlayerMove[2] != '>')
             {
                 return false;
             }
             startRow = i_PlayerMove[0] - 'A';
             startCol = i_PlayerMove[1] - 'a';
             endRow = i_PlayerMove[3] - 'A';
             endCol = i_PlayerMove[4] - 'a';
             // Basic boundary checks

             if(currentPlayer.GameSign == 'X')
             {
                 if (Math.Abs(i_PlayerMove[3] - i_PlayerMove[0]) != 1 )
                 {
                     return false;
                 }
             }


             if (startRow < 0 || startRow >= Size || startCol < 0 || startCol >= Size ||
                 endRow < 0 || endRow >= Size || endCol < 0 || endCol >= Size)
                 return false;


             if (Grid[startRow][startCol] == null)
                 return false;


             if (Grid[endRow][endCol] != null)
                 return false;

             UpdateBoardBuilder(startRow, startCol, endRow, endCol);
             return true;
         }*/
        public bool CanCaptureMore(int currentRow, int currentCol)
        {
            int[][] directions = new int[][]
            {
        new int[] { -2, -2 }, new int[] { -2, 2 },
        new int[] { 2, -2 }, new int[] { 2, 2 }
            };

            foreach (var direction in directions)
            {
                int newRow = currentRow + direction[0];
                int newCol = currentCol + direction[1];
                int middleRow = currentRow + direction[0] / 2;
                int middleCol = currentCol + direction[1] / 2;

                if (newRow >= 0 && newRow < Size && newCol >= 0 && newCol < Size &&
                    Grid[newRow][newCol] == null &&
                    Grid[middleRow][middleCol] != null &&
                    Grid[middleRow][middleCol].Symbol != Grid[currentRow][currentCol].Symbol)
                {
                    return true;
                }
            }

            return false;
        }


        private void UpdateBoardBuilder(int startRow, int startCol, int endRow, int endCol)
        {
            int middleRow = (startRow + endRow) / 2;
            int middleCol = (startCol + endCol) / 2;

            // אם זה מהלך אכילה, הסר את כלי היריב
            if (Math.Abs(endRow - startRow) == 2 && Math.Abs(endCol - startCol) == 2)
            {
                Grid[middleRow][middleCol] = null;
            }

            // העברת הכלי למשבצת החדשה
            Grid[endRow][endCol] = Grid[startRow][startCol];
            Grid[startRow][startCol] = null;

            // קידום הכלי למלך אם הגיע לקצה הלוח
            if (endRow == 0 || endRow == Size - 1)
            {
                Grid[endRow][endCol].PromoteToKing();
                Console.WriteLine($"{Grid[endRow][endCol].Owner}'s piece has been promoted to a King!");
            }
        }


    }
}