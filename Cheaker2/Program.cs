using System;

namespace Cheaker2
{
    using System;


    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to Checkers!");

            string player1Name = GetValidPlayerName("Player 1");
            string player2Name = GetValidPlayerName("Player 2");

            int boardSize = GetBoardSize();

            Game game = new Game(boardSize, player1Name, player2Name);
            game.Start();
        }

        static string GetValidPlayerName(string playerLabel)
        {
            string playerName;
            do
            {
                Console.Write($"Enter {playerLabel} Name (up to 20 characters, no spaces): ");
                playerName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(playerName))
                {
                    Console.WriteLine("Name cannot be empty. Please try again.");
                }
                else if (playerName.Length > 20)
                {
                    Console.WriteLine("Name is too long. Maximum 20 characters allowed. Please try again.");
                }
                else if (playerName.Contains(" "))
                {
                    Console.WriteLine("Name cannot contain spaces. Please try again.");
                }
                else
                {
                    break; // Name is valid
                }

            } while (true);

            return playerName;
        }

        static int GetBoardSize()
        {
            int boardSize;
            do
            {
                Console.Write("Choose board size (6, 8, or 10): ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out boardSize) || (boardSize != 6 && boardSize != 8 && boardSize != 10))
                {
                    Console.WriteLine("Invalid size. Please choose 6, 8, or 10.");
                }
                else
                {
                    break;
                }
            } while (true);

            return boardSize;
        }
    }


}