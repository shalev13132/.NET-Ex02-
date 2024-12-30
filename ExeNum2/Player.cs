using System;
using System.Collections.Generic;

namespace CheckersGame
{
    public class Player
    {
        public string Name { get; set; }
        public PlayerType Type { get; private set; }
        public List<Piece> Pieces { get; private set; }

        public Player(string name, PlayerType type)
        {
            Name = name;
            Type = type;
            Pieces = new List<Piece>();
        }

        public void AddPiece(Piece piece)
        {
            Pieces.Add(piece);
        }

        public void RemovePiece(Piece piece)
        {
            Pieces.Remove(piece);
        }

        public bool HasPiecesLeft()
        {
            return Pieces.Count > 0;
        }
    }
}