using System;

namespace Ex02
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to Checkers!");
            string m_Player1Name = GetValidPlayerName("Player 1"); 
            int m_BoardSize = GetBoardSize(); 
            string m_Player2Name = GameModeSelect(); 
            int m_Player1Points = 0; 
            int m_Player2Points = 0; 

            do
            {
                Game m_Game = new Game(m_BoardSize, m_Player1Name, m_Player2Name, m_Player1Points, m_Player2Points); 
                m_Game.Start();
                m_Player1Points = m_Game.m_Player1.Points; 
                m_Player2Points = m_Game.m_Player2.Points; 
            }
            while (Console.ReadLine()?.ToUpper() == "Y");

            Console.WriteLine("Thanks for playing! Final Scores:");
            ConsoleUI.PointsPrint(m_Player1Name, m_Player1Points, m_Player2Name, m_Player2Points); 
            Console.ReadLine();
        }

        static string GetValidPlayerName(string i_PlayerLabel) 
        {
            string playerName;
            do
            {
                Console.Write("Enter {0} Name (up to 20 characters, no spaces): ", i_PlayerLabel); 
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
                    break;
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

        static private string GameModeSelect() 
        {
            string input;
            string playerName;

            while (true)
            {
                Console.WriteLine("For 2 players game please enter 1, to play against the computer please enter 2:");
                input = Console.ReadLine();

                if (input.Equals("1"))
                {
                    playerName = GetValidPlayerName("Player 2"); 
                    break;
                }
                else if (input.Equals("2"))
                {
                    playerName = "Computer "; 
                    break;
                }
                Console.WriteLine("Invalid input.");
            }
            return playerName;
        }
    }
}
