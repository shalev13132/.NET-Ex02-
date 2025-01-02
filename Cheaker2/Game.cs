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
                Console.Write("Enter your move (e.g., A1>B2): ");
                string move = Console.ReadLine();

                if (!ParseMove(move, out int startRow, out int startCol, out int endRow, out int endCol))
                {
                    
                    Console.WriteLine("Invalid move format. Please try again.");
                    Console.ReadLine();
                    continue;
                }

                
                if (board.IsMoveValid(startRow, startCol, endRow, endCol, currentPlayer))
                {
                    board.UpdateBoard(startRow, startCol, endRow, endCol);

                    // קידום למלך אם נדרש
                    if (endRow == 0 || endRow == board.Size - 1)
                    {
                        board.Grid[endRow, endCol].PromoteToKing();
                        Console.WriteLine($"{currentPlayer.Name}'s piece has been promoted to a King!");
                    }

                    // החלפת תור
                    currentPlayer = currentPlayer == player1 ? player2 : player1;
                }
                else
                {
                    
                    Console.WriteLine("Invalid move. Please try again.");
                    Console.ReadLine();
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
