using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02_01
{
    class Program
    {
        static void Main()
        {
            int m_BoardSize;
            //bool m_GameStatus = true; // true= game on/false=game ended
            string player1 = PlayerNameValidation();
            m_BoardSize = BoardSizeValidation();
            //BoardBuilder MainBoard = new BoardBuilder(m_BoardSize);
            string player2 = GameModeSelect();
            Game m_NewGame = new Game(m_BoardSize, player1, player2);
            m_NewGame.Start();
            Console.ReadLine();
        }

        static public string PlayerNameValidation()
        {
            Console.WriteLine("Please enter your name, no spaces, max 20 letters long");
            string playerName = Console.ReadLine();

            while (playerName.Length > 20 || playerName.Contains(' '))
            {
                Console.WriteLine("Invalid input, please enter a name without spaces and 20 letters max!");
                playerName = Console.ReadLine();
            }
            return playerName;
        }

        static private int BoardSizeValidation()
        {
            int BoardSize;
            while (true)
            {
                Console.Write("Enter board size (6, 8, or 10): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out BoardSize))
                {
                    switch (BoardSize)
                    {
                        case 6:
                        case 8:
                        case 10:
                            return BoardSize; // Valid size entered
                        default:
                            Console.WriteLine("Invalid size. Please enter 6, 8, or 10.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        static private string GameModeSelect()
        {
            string input;
            string playerName;

            while (true)
            {
                Console.WriteLine("for 2 players game please enter 1, to play against the computer please enter 2 ");
                input = Console.ReadLine();


                if (input.Equals("1"))
                {
                    playerName = PlayerNameValidation();
                    break;
                }
                else if (input.Equals("2"))
                {
                    playerName = "Computer";
                    break;
                }
                Console.WriteLine("Invalid input.");

            }
            return playerName;

        }
    }

}