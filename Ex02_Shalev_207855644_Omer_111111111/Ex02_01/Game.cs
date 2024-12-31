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
            Players previousPlayer=Player2;
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
                    ConsoleUI.DisplayPreTurnMessage(currentPlayer,previousPlayer);
                }
                currentPlayer.GetMove(Board);





                if(currentPlayer==Player1)
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
        }
    }
}
