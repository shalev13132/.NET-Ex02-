using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheaker2
{
    public class Board
    {
        //private readonly int size;
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


        public void DisplayBoard()
        {
            // הדפסת כותרת העמודות (a, b, c ...)
            Console.Write("  ");
            for (int col = 0; col < Size; col++)
            {
                Console.Write($"  {char.ToLower((char)('A' + col))} ");
            }
            Console.WriteLine();

            // הדפסת קו עליון
            //Console.WriteLine("  +" + string.Join("+", Enumerable.Repeat("---", Size)) + "+");
            Console.WriteLine("  =" + String.Concat(Enumerable.Repeat("====", Size)));
            // הדפסת שורות הלוח
            for (int row = 0; row < Size; row++)
            {
                // הדפסת מספר השורה
                Console.Write($"{(char)('A' + row)} |");

                for (int col = 0; col < Size; col++)
                {
                    // הצגת החייל או משבצת ריקה
                    char symbol = Grid[row, col]?.Symbol ?? ' ';
                    Console.Write($" {symbol} |");
                }

                // הדפסת קו תחתון
                Console.WriteLine();
                Console.WriteLine("  =" + String.Concat(Enumerable.Repeat("====", Size)));
            }
        }

        public bool IsMoveValid(int startRow, int startCol, int endRow, int endCol, Player currentPlayer)
        {
            // בדיקות גבול
            if (startRow < 0 || startRow >= Size || startCol < 0 || startCol >= Size ||
                endRow < 0 || endRow >= Size || endCol < 0 || endCol >= Size)
            {
              
                return false;
            }

            // בדיקה אם המשבצת ההתחלתית שייכת לשחקן הנוכחי
            Piece movingPiece = Grid[startRow, startCol];
            if (movingPiece == null || movingPiece.Symbol != currentPlayer.Symbol)
            {
                
                return false;
            }

            // בדיקה אם המשבצת הסופית ריקה
            if (Grid[endRow, endCol] != null)
            {
               
                return false;
            }

            // הבדיקה אם מדובר בתנועה רגילה או במהלך אכילה
            int rowDiff = endRow - startRow;
            int colDiff = endCol - startCol;

            if (Math.Abs(rowDiff) == 1 && Math.Abs(colDiff) == 1)
            {
                // תנועה רגילה: צעד אחד באלכסון
                if (!movingPiece.IsKing)
                {
                    // חייל רגיל: יכול לנוע רק קדימה
                    if ((currentPlayer.Symbol == 'X' && rowDiff >= 0) || (currentPlayer.Symbol == 'O' && rowDiff <= 0))
                    {
                        return false;
                    }
                }
                
                return true;
            }
            else if (Math.Abs(rowDiff) == 2 && Math.Abs(colDiff) == 2)
            {
                // מהלך אכילה: שתי משבצות באלכסון
                int middleRow = (startRow + endRow) / 2;
                int middleCol = (startCol + endCol) / 2;
                Piece middlePiece = Grid[middleRow, middleCol];

                if (middlePiece != null && middlePiece.Symbol != currentPlayer.Symbol)
                {
                    // אכילה חוקית
                    Grid[middleRow, middleCol] = null; // הסרת כלי היריב
                    return true;
                }
            }

            
            return false; // אם אף תנאי לא מתקיים, המהלך אינו חוקי
        }

        public void UpdateBoard(int startRow, int startCol, int endRow, int endCol)
        {
            // העברת הכלי למשבצת הסופית
            Grid[endRow, endCol] = Grid[startRow, startCol];
            Grid[startRow, startCol] = null;

            // בדיקה אם המהלך הוא מהלך אכילה
            if (Math.Abs(endRow - startRow) == 2 && Math.Abs(endCol - startCol) == 2)
            {
                // חישוב מיקום הכלי היריב שנמצא באמצע
                int middleRow = (startRow + endRow) / 2;
                int middleCol = (startCol + endCol) / 2;

                // הסרת הכלי היריב
                Grid[middleRow, middleCol] = null;
            }

            // בדיקה אם יש צורך לקדם את הכלי למלך
            PromoteToKingIfApplicable(endRow, endCol);
        }

        private void PromoteToKingIfApplicable(int row, int col)
        {
            Piece piece = Grid[row, col];
            if (piece == null) return;

            // קידום למלך אם הכלי הגיע לקצה הלוח
            if ((piece.Symbol == 'X' && row == 0) || (piece.Symbol == 'O' && row == Grid.GetLength(0) - 1))
            {
                piece.PromoteToKing();
            }
        }

        public bool HasMandatoryCapture(Player player)
        {
            char playerSymbol = player.Symbol;

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Piece piece = Grid[row, col];
                    if (piece != null && piece.Symbol == playerSymbol)
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

        public bool CanCapture(int row, int col)
        {
            Piece piece = Grid[row, col];
            if (piece == null)
            {
                return false; // אין כלי במשבצת הנוכחית
            }

            // קבע את הכיוונים המותרים בהתאם לסוג הכלי
            int[,] directions;

            if (piece.IsKing)
            {
                // מלך יכול לנוע בכל ארבעת הכיוונים
                directions = new int[,] { { -2, -2 }, { -2, 2 }, { 2, -2 }, { 2, 2 } };
            }
            else
            {
                // חייל רגיל יכול לנוע רק קדימה
                directions = piece.Symbol == 'X'
                    ? new int[,] { { -2, -2 }, { -2, 2 } } // חייל "X" נע כלפי מעלה
                    : new int[,] { { 2, -2 }, { 2, 2 } };  // חייל "O" נע כלפי מטה
            }

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int targetRow = row + directions[i, 0];
                int targetCol = col + directions[i, 1];
                int middleRow = row + directions[i, 0] / 2;
                int middleCol = col + directions[i, 1] / 2;

                // בדיקה אם הכלי יכול לאכול כלי יריב
                if (IsWithinBounds(targetRow, targetCol) &&
                    IsWithinBounds(middleRow, middleCol) &&
                    Grid[middleRow, middleCol] != null &&
                    Grid[middleRow, middleCol].Symbol != piece.Symbol &&
                    Grid[targetRow, targetCol] == null)
                {
                    return true;
                }
            }

            return false;
        }


       

        private bool IsWithinBounds(int row, int col)
        {
            return row >= 0 && row < Size && col >= 0 && col < Size;
        }

        public bool IsCaptureMove(int startRow, int startCol, int endRow, int endCol, Player player)
        {
            int middleRow = (startRow + endRow) / 2;
            int middleCol = (startCol + endCol) / 2;

            return Math.Abs(endRow - startRow) == 2 &&
                   Math.Abs(endCol - startCol) == 2 &&
                   Grid[middleRow, middleCol] != null &&
                   Grid[middleRow, middleCol].Symbol != player.Symbol &&
                   Grid[endRow, endCol] == null;
        }

    }


}
