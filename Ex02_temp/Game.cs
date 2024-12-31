using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02_01
{
    public class Game
    {
        BoardBuilder Board;
        Players Player1;
        Players Player2;




        public Game(int boardSize, string player1Name, string player2Name)
        {



            Board = new BoardBuilder(boardSize);
            Player1 = new Players(player1Name, 'X');
            Player2 = new Players(player2Name, 'O');
        }

        public void Start()
        {
            Players currentPlayer = Player1;
            Players previousPlayer = Player2;
            bool isFirstMove = true;

            while (true)
            {
                ConsoleUI.DisplayBoard(Board);

                if (isFirstMove)
                {
                    ConsoleUI.DisplayFirstTurnMessage(currentPlayer);
                    isFirstMove = false;
                }
                else
                {
                    ConsoleUI.DisplayPreTurnMessage(currentPlayer, previousPlayer);
                }

                currentPlayer.GetMove(Board);

                // בדיקת חוקי המשחק
                int startRow = currentPlayer.PlayerMove[0] - 'A';
                int startCol = currentPlayer.PlayerMove[1] - 'a';
                int endRow = currentPlayer.PlayerMove[3] - 'A';
                int endCol = currentPlayer.PlayerMove[4] - 'a';

                if (Math.Abs(endRow - startRow) == 2) // אם זה מהלך אכילה
                {
                    if (Board.CanCaptureMore(endRow, endCol))
                    {
                        Console.WriteLine($"{currentPlayer.PlayerName}, you can capture more! Continue your move.");
                        continue;
                    }
                }

                // החלפת תור
                previousPlayer = currentPlayer;
                currentPlayer = currentPlayer == Player1 ? Player2 : Player1;
            }
        }



        /*public void Start()
        {
           


            Players currentPlayer = Player1;
            Players previousPlayer = Player2;
            bool FirstMoveFlag = true;

            while (true)
            {
                ConsoleUI.DisplayBoard(Board);
                if (FirstMoveFlag)
                {
                    ConsoleUI.DisplayFirstTurnMessage(currentPlayer);
                    FirstMoveFlag = false;
                }
                else
                {
                    ConsoleUI.DisplayPreTurnMessage(currentPlayer, previousPlayer);
                }
                if (currentPlayer == Player1) {
                    currentPlayer.GetMove(Board);
                }
                else { currentPlayer.GetMove(Board); }

            }
             


                if (currentPlayer == Player1)
                {
                    currentPlayer = Player2;
                    previousPlayer = Player1;
                }
                else
                {
                    currentPlayer = Player1;
                    previousPlayer = Player2;
                }
                Console.ReadLine();
            
        }
    }*/
    }
}