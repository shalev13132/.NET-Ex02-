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
        public bool HaveEatingMove { get; internal set; }


        public Players(string i_name, char i_GameSign)
        {
            PlayerName = i_name;
            //points = 0;
            GameSign = i_GameSign;
            HaveEatingMove = false;
        }


        public void GetMove(BoardBuilder i_Board,Players CurrentPlayer)
        {
            PlayerMove = Console.ReadLine();
            
            while (!(i_Board.IsMoveValid(CurrentPlayer)))
            {
                Console.WriteLine("invalid move, please rewrite your move :");
                PlayerMove = Console.ReadLine();
            }
        }
    }
}
