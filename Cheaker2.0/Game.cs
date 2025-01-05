using System;

namespace Ex02
{
    public class Game
    {
        private readonly Board m_Board;
        public readonly Player m_Player1;
        public readonly Player m_Player2;

        public Game(int i_BoardSize, string i_Player1Name, string i_Player2Name, int i_Player1Points = 0, int i_Player2Points = 0)
        {
            m_Board = new Board(i_BoardSize);
            m_Player1 = new Player(i_Player1Name, "Player1", 'X', i_Player1Points);
            m_Player2 = new Player(i_Player2Name, "Player2", 'O', i_Player2Points);
        }

        public void Start()
        {
            Player m_CurrentPlayer = m_Player1;
            Player m_PreviousPlayer = m_Player2;
            bool m_FirstMoveFlag = true;

            while (true)
            {
                string m_Move;
                Ex02.ConsoleUtils.Screen.Clear();
                ConsoleUI.DisplayBoard(m_Board);
                if (m_FirstMoveFlag)
                {
                    ConsoleUI.DisplayFirstTurnMessage(m_CurrentPlayer);
                    m_FirstMoveFlag = false;
                }
                else
                {
                    ConsoleUI.DisplayPreTurnMessage(m_CurrentPlayer, m_PreviousPlayer);
                }

                string gameOverResult = m_Board.CheckGameOver();
                if (gameOverResult != null)
                {
                    HandleGameOver(gameOverResult);
                    break;
                }

                if (m_CurrentPlayer.Id == "Player2" && m_CurrentPlayer.Name == "Computer ")
                {
                    string[] availableMoves = m_Board.GetAvailableMoves(m_CurrentPlayer.Id);
                    if (availableMoves == null)
                    {
                        HandleGameOver("Player1");
                        break;
                    }
                    Random random = new Random();
                    int randomIndex = random.Next(availableMoves.Length);
                    m_Move = availableMoves[randomIndex];
                }
                else
                {
                    m_Move = Console.ReadLine();
                }
                m_CurrentPlayer.Move = m_Move;

                if (m_Move == "Q")
                {
                    ConsoleUI.DisplayResignMessage(m_CurrentPlayer, m_PreviousPlayer);
                    m_PreviousPlayer.Points = 10;
                    break;
                }

                if (!ParseMove(m_Move, out int i_StartRow, out int i_StartCol, out int i_EndRow, out int i_EndCol))
                {
                    ConsoleUI.DisplayFormatErrorMessage();
                    continue;
                }

                if (m_Board.HasMandatoryCapture(m_CurrentPlayer) && !m_Board.IsCaptureMove(i_StartRow, i_StartCol, i_EndRow, i_EndCol, m_CurrentPlayer))
                {
                    ConsoleUI.DisplayCaptureErrorMessage();
                    continue;
                }

                if (m_Board.IsMoveValid(i_StartRow, i_StartCol, i_EndRow, i_EndCol, m_CurrentPlayer))
                {
                    m_Board.UpdateBoard(i_StartRow, i_StartCol, i_EndRow, i_EndCol);

                    if (i_EndRow == 0 || i_EndRow == m_Board.Size - 1)
                    {
                        m_Board.Grid[i_EndRow, i_EndCol].PromoteToKing();
                    }

                    while (m_Board.CanCapture(i_EndRow, i_EndCol))
                    {
                        if (Math.Abs(i_EndRow - i_StartRow) != 2 && Math.Abs(i_EndCol - i_StartCol) != 2)
                        {
                            break;
                        }

                        Ex02.ConsoleUtils.Screen.Clear();
                        ConsoleUI.DisplayBoard(m_Board);
                        ConsoleUI.DisplayPreTurnMessage(m_CurrentPlayer, m_CurrentPlayer);
                        if (m_CurrentPlayer.Id == "Player2" && m_CurrentPlayer.Name == "Computer ")
                        {
                            string[] availableMoves = m_Board.GetAvailableMoves(m_CurrentPlayer.Id);
                            if (availableMoves == null)
                            {
                                HandleGameOver("Player1");
                                break;
                            }
                            Random random = new Random();
                            int randomIndex = random.Next(availableMoves.Length);
                            m_Move = availableMoves[randomIndex];
                        }
                        else
                        {
                            m_Move = Console.ReadLine();
                        }
                        m_CurrentPlayer.Move = m_Move;
                        if (!ParseMove(m_Move, out int i_NextStartRow, out int i_NextStartCol, out int i_NextEndRow, out int i_NextEndCol) ||
                            i_NextStartRow != i_EndRow || i_NextStartCol != i_EndCol)
                        {
                            ConsoleUI.DisplayFormatErrorMessage();
                            continue;
                        }

                        if (!m_Board.IsCaptureMove(i_NextStartRow, i_NextStartCol, i_NextEndRow, i_NextEndCol, m_CurrentPlayer))
                        {
                            ConsoleUI.DisplayCaptureErrorMessage();
                            continue;
                        }

                        m_Board.UpdateBoard(i_NextStartRow, i_NextStartCol, i_NextEndRow, i_NextEndCol);

                        i_EndRow = i_NextEndRow;
                        i_EndCol = i_NextEndCol;
                    }

                    if (m_CurrentPlayer == m_Player1)
                    {
                        m_CurrentPlayer = m_Player2;
                        m_PreviousPlayer = m_Player1;
                    }
                    else
                    {
                        m_CurrentPlayer = m_Player1;
                        m_PreviousPlayer = m_Player2;
                    }
                }

                else
                {
                    Console.WriteLine("Invalid move. Please try again.");
                    Console.ReadLine();
                }
            }
            ConsoleUI.DisplayNewGameMessage();
        }

        private bool ParseMove(string i_Move, out int io_StartRow, out int io_StartCol, out int io_EndRow, out int io_EndCol)
        {
            io_StartRow = io_StartCol = io_EndRow = io_EndCol = -1;

            if (string.IsNullOrWhiteSpace(i_Move) || i_Move.Length != 5 || i_Move[2] != '>')
            {
                return false;
            }

            io_StartRow = i_Move[0] - 'A';
            io_StartCol = i_Move[1] - 'a';
            io_EndRow = i_Move[3] - 'A';
            io_EndCol = i_Move[4] - 'a';
            return true;
        }

        private void HandleGameOver(string i_Winner)
        {
            if (i_Winner == "Player1")
            {
                m_Player1.Points += CalculateScore(m_Player1);
                Console.WriteLine($"{m_Player1.Name} wins!");
            }
            else if (i_Winner == "Player2")
            {
                m_Player2.Points += CalculateScore(m_Player2);
                Console.WriteLine($"{m_Player2.Name} wins!");
            }
            else
            {
                Console.WriteLine("It's a draw!");
            }
            Console.WriteLine($"Current Scores: {m_Player1.Name}: {m_Player1.Points}, {m_Player2.Name}: {m_Player2.Points}");
        }

        private int CalculateScore(Player i_Player)
        {
            int score = 0;

            for (int row = 0; row < m_Board.Size; row++)
            {
                for (int col = 0; col < m_Board.Size; col++)
                {
                    Piece piece = m_Board.Grid[row, col];
                    if (piece != null && piece.Owner == i_Player.Id)
                    {
                        score += piece.IsKing ? 4 : 1;
                    }
                }
            }
            return score;
        }
    }
}
