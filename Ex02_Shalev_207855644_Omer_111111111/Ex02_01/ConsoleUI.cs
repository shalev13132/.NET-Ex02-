using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02_01
{
    public static class ConsoleUI
    {
        public static void DisplayBoard(BoardBuilder board)
        {
            Console.Clear();
            Console.WriteLine("    " + string.Join("   ", GetColumnHeaders(board.Size)));
            Console.WriteLine("  " + new string('=', (board.Size * 4)+1));

            for (int row = 0; row < board.Size; row++)
            {
                Console.Write((char)('A' + row) + " |");
                for (int col = 0; col < board.Size; col++)
                {
                    if (board.Grid[row][col] == null)
                    {
                        Console.Write("   |");
                    }
                    else
                    {
                        Console.Write($" {board.Grid[row][col].Sign} |");
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

        
        public static void DisplayFirstTurnMessage(Players CurrentPlayer)
        {
            Console.WriteLine("{0}'s Turn ({1}) Please enter your move (ROWcol>ROWcol):",CurrentPlayer.PlayerName,CurrentPlayer.GameSign);
        }
        public static void DisplayPreTurnMessage(Players CurrentPlayer, Players previusPlayer)
        {
            Console.WriteLine("{0}'s move was ({1}) : {2}",previusPlayer.PlayerName,previusPlayer.GameSign,previusPlayer.PlayerMove);
            Console.WriteLine("{0}'s Turn ({1}) Please enter your move (ROWcol>ROWcol):", CurrentPlayer.PlayerName, CurrentPlayer.GameSign);
        }
        

        public static void WaitForKeyPress()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
