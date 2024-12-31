using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02_01
{
    public class Players
    {
        public string PlayerName { get; }
        //int points;
        public char GameSign { get; }
        public string PlayerMove { get; private set; }



        public Players(string i_name, char i_GameSign)
        {
            PlayerName = i_name;
            //points = 0;
            GameSign = i_GameSign;
        }


        public void GetMove(BoardBuilder i_Board)
        {

            PlayerMove = Console.ReadLine();
            while (!(i_Board.IsMoveValid(PlayerMove, this)))
            {
                Console.WriteLine("invalid move, please rewrite your move :");
                PlayerMove = Console.ReadLine();
            }
        }
    }
}