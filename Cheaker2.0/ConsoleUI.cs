using System;

namespace Ex02
{
    public static class ConsoleUI
    {
        public static void DisplayBoard(Board i_board)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("    " + string.Join("   ", GetColumnHeaders(i_board.Size)));
            Console.WriteLine("  " + new string('=', (i_board.Size * 4) + 1));

            for (int row = 0; row < i_board.Size; row++)
            {
                Console.Write((char)('A' + row) + " |");
                for (int col = 0; col < i_board.Size; col++)
                {
                    if (i_board.Grid[row, col] == null)
                    {
                        Console.Write("   |");
                    }
                    else
                    {
                        Console.Write($" {i_board.Grid[row, col].Symbol} |");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("  " + new string('=', (i_board.Size * 4) + 1));
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

        public static void PointsPrint(string i_player1Name, int i_player1Points, string i_player2Name, int i_player2Points)
        {
            Console.WriteLine("Current Scores: {0}: {1}, {2}: {3}", i_player1Name, i_player1Points, i_player2Name, i_player2Points);

        }
    }
}