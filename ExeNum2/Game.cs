using System;
using System.Collections.Generic;

namespace CheckersGame
{
    public class Game
    {
        private Board m_Board;
        private Player m_Player1;
        private Player m_Player2;
        private Player m_CurrentPlayer;
        private bool m_IsVsComputer;
        private LegalMovesManager m_MovesManager;

        public Game(int boardSize, string player1Name, string player2Name = null)
        {
            m_Board = new Board(boardSize);
            m_Player1 = new Player(player1Name, PlayerType.Human);
            m_Player2 = player2Name == null
                ? new Player("Computer", PlayerType.Computer)
                : new Player(player2Name, PlayerType.Human);
            m_CurrentPlayer = m_Player1;
            m_IsVsComputer = player2Name == null;
            m_MovesManager = new LegalMovesManager();
        }

        public void StartGame(int boardSize)    // ניסיון
        {

            m_Board.PrintBoard(boardSize);
            Move move = GetPlayerMove(m_CurrentPlayer);
            MakeMove(move, m_CurrentPlayer);
            SwitchTurn();


            if (!m_MovesManager.IsGameOver(m_Board, m_CurrentPlayer, GetOpponent()))
            {
                
                    Console.WriteLine("out");
                    EndGame();
            }
              
        }

     
        /*public void StartGame()
        {
            while (!m_MovesManager.IsGameOver(m_Board, m_CurrentPlayer, GetOpponent()))
            {
                try
                {
                    m_Board.PrintBoard();
                    Move move = GetPlayerMove(m_CurrentPlayer);
                    MakeMove(move, m_CurrentPlayer);
                    SwitchTurn();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            EndGame();
        }*/

        private Player GetOpponent()
        {
            return m_CurrentPlayer == m_Player1 ? m_Player2 : m_Player1;
        }

        private Move GetPlayerMove(Player player)
        {
            if (player.Type == PlayerType.Computer)
            {
                AI ai = new AI();
                List<Move> legalMoves = m_MovesManager.GenerateLegalMoves(m_Board, player);
                return ai.SelectMove(legalMoves);
            }
            else
            {
                Console.WriteLine($"{player.Name}'s turn. Enter your move (e.g., A2>B3): ");
                string input = Console.ReadLine();
                return ParseMove(input);
            }
        }

        private Move ParseMove(string input)
        {
            // Parse the input string into a Move object.
            // Example: "A2>B3" -> (SourceRow: 1, SourceColumn: 0, DestRow: 2, DestColumn: 1)
            throw new NotImplementedException();
        }

        private void MakeMove(Move move, Player currentPlayer)
        {
            if (m_MovesManager.GenerateLegalMoves(m_Board, currentPlayer).Contains(move))
            {
                m_Board.UpdateBoard(move.SourceRow, move.SourceColumn, move.DestRow, move.DestColumn);

                if (move.IsCapture)
                {
                    HandleCapture(move);
                }

                PromoteToKingIfNeeded(move, currentPlayer);
            }
            else
            {
                throw new InvalidOperationException("Illegal move.");
            }
        }

        private void HandleCapture(Move move)
        {
            // Implement logic to handle capture
            throw new NotImplementedException();
        }

        private void PromoteToKingIfNeeded(Move move, Player currentPlayer)
        {
            // Implement logic to promote a piece to King
            throw new NotImplementedException();
        }

        private void SwitchTurn()
        {
            m_CurrentPlayer = m_CurrentPlayer == m_Player1 ? m_Player2 : m_Player1;
        }

        private void EndGame()
        {
            Console.WriteLine("Game Over!");
            // Implement additional end game logic, like declaring the winner
        }
    }
}