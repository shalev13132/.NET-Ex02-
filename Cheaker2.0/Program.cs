using System;

namespace Cheaker2
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to Checkers!");

            string m_player1Name = GetValidPlayerName("Player 1");
            int m_boardSize = GetBoardSize();
            string m_player2Name = GameModeSelect();

            int player1Points = 0;
            int player2Points = 0;

            do
            {
                Game game = new Game(m_boardSize, m_player1Name, m_player2Name, player1Points, player2Points);
                game.Start();

                // עדכון ניקוד מצטבר
                player1Points = game.player1.Points;
                player2Points = game.player2.Points;

                Console.WriteLine($"Current Scores: {m_player1Name}: {player1Points}, {m_player2Name}: {player2Points}");

                //Console.WriteLine("Would you like to play another game? (Y/N): ");
            }
            while (Console.ReadLine()?.ToUpper() == "Y");

            Console.WriteLine("Thanks for playing! Final Scores:");
            Console.WriteLine($"{m_player1Name}: {player1Points}, {m_player2Name}: {player2Points}");
        }

         
        

        static string GetValidPlayerName(string i_playerLabel)
        {
            string playerName;
            do
            {
                Console.Write($"Enter {i_playerLabel} Name (up to 20 characters, no spaces): ");
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
                Console.WriteLine("for 2 players game please enter 1, to play against the computer please enter 2 ");
                input = Console.ReadLine();

                if (input.Equals("1"))
                {
                    playerName = GetValidPlayerName("Player 2");
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