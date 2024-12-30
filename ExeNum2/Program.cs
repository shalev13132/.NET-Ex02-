using System;

namespace CheckersGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Checkers Game!");
            Console.Write("Enter Player 1 Name: ");
            string player1Name = Console.ReadLine();

            Console.Write("Enter Player 2 Name (or press Enter to play against the computer): ");
            string player2Name = Console.ReadLine();

            Console.Write("Enter Board Size (6, 8, or 10): ");
            int boardSize;
            while (!int.TryParse(Console.ReadLine(), out boardSize) || (boardSize != 6 && boardSize != 8 && boardSize != 10))
            {
                Console.Write("Invalid input. Please enter 6, 8, or 10: ");
            }

            Game game = new Game(boardSize, player1Name, string.IsNullOrEmpty(player2Name) ? null : player2Name);
            game.StartGame(boardSize);

            Console.WriteLine("Thanks for playing!");
        }
    }
}