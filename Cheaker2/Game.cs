using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheaker2
{
    using System;

    public class Game
    {
        private readonly Board board;
        private readonly Player player1;
        private readonly Player player2;
        private Player currentPlayer;

        public Game(int boardSize, string player1Name, string player2Name)
        {
            board = new Board(boardSize);
            player1 = new Player(player1Name, 'X');
            player2 = new Player(player2Name, 'O');
            currentPlayer = player1;
        }

        public void Start()
        {
            Player currentPlayer = player1;

            while (true)
            {
                Console.Clear();
                
                board.DisplayBoard();

                Console.WriteLine($"{currentPlayer.Name}'s turn ({currentPlayer.Symbol}): ");
                
                bool hasMandatoryCapture = board.HasMandatoryCapture(currentPlayer);

                Console.Write("Enter your move (e.g., A1>B2): ");
                string move = Console.ReadLine();

                if (!ParseMove(move, out int startRow, out int startCol, out int endRow, out int endCol))
                {
                    Console.WriteLine("Invalid move format. Please try again.");
                    Console.ReadLine();
                    continue;
                }

                // אם קיימת חובה לבצע אכילה, ודא שהמהלך הוא מהלך אכילה
                if (board.HasMandatoryCapture(currentPlayer) && !board.IsCaptureMove(startRow, startCol, endRow, endCol, currentPlayer))
                {
                    Console.WriteLine("You must capture an opponent's piece if possible.");
                    Console.ReadLine();
                    continue;
                }

                // בדוק אם המהלך חוקי
                if (board.IsMoveValid(startRow, startCol, endRow, endCol, currentPlayer))
                {
                    board.UpdateBoard(startRow, startCol, endRow, endCol);

                    // קידום למלך אם נדרש
                    if (endRow == 0 || endRow == board.Size - 1)
                    {
                        board.Grid[endRow, endCol].PromoteToKing();
                    }


                    
                    while (board.CanCapture(endRow, endCol))
                        {
                            if (Math.Abs(endRow - startRow) != 2 && Math.Abs(endCol - startCol) != 2)
                            {
                            break;

                            }

                            Console.Clear();
                            board.DisplayBoard();

                            Console.WriteLine($"{currentPlayer.Name}, your piece at {(char)('A' + endRow)}{(char)('a' + endCol)} can perform another capture.");
                            Console.Write("Enter your next move (e.g., C3>E5): ");
                            move = Console.ReadLine();

                            if (!ParseMove(move, out int nextStartRow, out int nextStartCol, out int nextEndRow, out int nextEndCol) ||
                                nextStartRow != endRow || nextStartCol != endCol)
                            {
                                Console.WriteLine("Invalid move format or incorrect piece. Please try again.");
                                continue;
                            }

                            if (!board.IsCaptureMove(nextStartRow, nextStartCol, nextEndRow, nextEndCol, currentPlayer))
                            {
                                Console.WriteLine("You must capture an opponent's piece if possible.");
                                continue;
                            }

                            board.UpdateBoard(nextStartRow, nextStartCol, nextEndRow, nextEndCol);

                            // עדכון למהלך הבא
                            endRow = nextEndRow;
                            endCol = nextEndCol;
                    }
                    

                    
                  currentPlayer = currentPlayer == player1 ? player2 : player1;
                }

                else
                {
                    Console.WriteLine("Invalid move. Please try again.");
                }
            }
        }


        private bool ParseMove(string move, out int startRow, out int startCol, out int endRow, out int endCol)
        {
            startRow = startCol = endRow = endCol = -1;

            if (string.IsNullOrWhiteSpace(move) || move.Length != 5 || move[2] != '>')
            {
                return false;
            }

            startRow = move[0] - 'A';
            startCol = move[1] - 'a';
            endRow = move[3] - 'A';
            endCol = move[4] - 'a';

            return true;
        }



       /* public static void DisplayInvalidMoveMessage()
        {
            Console.WriteLine("Invalid move. Please try again.");
            Console.ReadLine();
        }

        public static void DisplayValidMoveMessage(Player currentPlayer)
        {
            Console.WriteLine($"{currentPlayer.Name} made a valid move.");
        }
       */
    }


}
