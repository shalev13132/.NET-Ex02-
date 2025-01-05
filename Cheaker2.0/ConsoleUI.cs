using System;


namespace Cheaker2
{
    public static class ConsoleUI
    {
        public static void DisplayBoard(Board board)
        {
            Console.Clear();
            Console.WriteLine("    " + string.Join("   ", GetColumnHeaders(board.Size)));
            Console.WriteLine("  " + new string('=', (board.Size * 4) + 1));

            for (int row = 0; row < board.Size; row++)
            {
                Console.Write((char)('A' + row) + " |");
                for (int col = 0; col < board.Size; col++)
                {
                    if (board.Grid[row, col] == null)
                    {
                        Console.Write("   |");
                    }
                    else
                    {
                        Console.Write($" {board.Grid[row, col].Symbol} |");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("  " + new string('=', (board.Size * 4) + 1));
            }
        }

        private static string[] GetColumnHeaders(int size)
        {
            string[] headers = new string[size];
            for (int i = 0; i < size; i++)
            {
                headers[i] = ((char)('a' + i)).ToString();
            }
            return headers;
        }


        public static void DisplayFirstTurnMessage(Player CurrentPlayer)
        {
            Console.WriteLine("{0}'s Turn ({1}) Please enter your move (ROWcol>ROWcol):", CurrentPlayer.Name, CurrentPlayer.Symbol);
        }

        public static void DisplayPreTurnMessage(Player CurrentPlayer, Player previusPlayer)
        {
            Console.WriteLine("{0}'s move was ({1}) : {2}", previusPlayer.Name, previusPlayer.Symbol, previusPlayer.Move);
            Console.WriteLine("{0}'s Turn ({1}) Please enter your move (ROWcol>ROWcol):", CurrentPlayer.Name, CurrentPlayer.Symbol);
        }
        public static void DisplayFormatErrorMessage()
        {
            Console.WriteLine("Invalid move format. Please press Enter and try again.");
            Console.ReadLine();
        }

        public static void DisplayCaptureErrorMessage()
        {
            Console.WriteLine("You must capture an opponent's piece if possible. Please press Enter and try again.");
            Console.ReadLine();
        }

        public static void DisplayResignMessage(Player CurrentPlayer, Player previusPlayer)
        {
            Console.WriteLine("{0} resign the game ,{1} is the winner", CurrentPlayer.Name, previusPlayer.Name);
            Console.ReadLine();
        }

        public static void DisplayNewGameMessage()
        {
            Console.WriteLine("For new game please press Y, to end press any key ");
        }
    }
}