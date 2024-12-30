using System;

namespace CheckersGame
{
    public class UserInterface
    {
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string GetInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public void DisplayBoard(Board board, int boardSize)
        {
            board.PrintBoard(boardSize);
        }

        public void ClearScreen()
        {
            Console.Clear();
        }

        public void DisplayError(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ResetColor();
        }
    }
}