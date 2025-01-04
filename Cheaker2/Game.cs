using System;

namespace Cheaker2
{
    public class Game
    {
        private readonly Board board;
        private readonly Player player1;
        private readonly Player player2;

        public Game(int boardSize, string player1Name, string player2Name)
        {
            board = new Board(boardSize);
            player1 = new Player(player1Name, "Player1", 'X');
            player2 = new Player(player2Name, "Player2", 'O');
        }
        

        public void Start()
        {
            Player m_currentPlayer = player1;
            Player m_previusPlayer = player2;
            bool m_FirstMoveFlag = true;

            while (true)
            {
                string move;
                Console.Clear();
                ConsoleUI.DisplayBoard(board);
                if(m_FirstMoveFlag)
                {
                    ConsoleUI.DisplayFirstTurnMessage(m_currentPlayer);
                    m_FirstMoveFlag = false;
                }
                else
                {
                    ConsoleUI.DisplayPreTurnMessage(m_currentPlayer, m_previusPlayer);
                }
                string gameState = board.CheckGameOver(); // Check if the game is over
                bool hasMandatoryCapture = board.HasMandatoryCapture(m_currentPlayer);
                
                if(m_currentPlayer.Id == "Player2" && m_currentPlayer.Name=="Computer")
                {
                    string[] availableMoves = board.GetAvailableMoves(m_currentPlayer.Id);
                    Random random = new Random();
                    int randomIndex = random.Next(availableMoves.Length);

                   move = availableMoves[randomIndex];

                }
                else
                {
                    move = Console.ReadLine();
                }

                m_currentPlayer.Move = move;
                
                
                if (gameState != null)
                {

                    break;
                }
                if (move == "Q")
                {
                    ConsoleUI.DisplayResignMessage(m_currentPlayer, m_previusPlayer);
                    break;
                }
                


                if (!ParseMove(move, out int startRow, out int startCol, out int endRow, out int endCol))
                {
                    ConsoleUI.DisplayFormatErrorMessage();
                    continue;
                }

                // אם קיימת חובה לבצע אכילה, ודא שהמהלך הוא מהלך אכילה
                if (board.HasMandatoryCapture(m_currentPlayer) && !board.IsCaptureMove(startRow, startCol, endRow, endCol, m_currentPlayer))
                {
                    ConsoleUI.DisplayCaptureErrorMessage();
                    continue;
                }

                // בדוק אם המהלך חוקי
                if (board.IsMoveValid(startRow, startCol, endRow, endCol, m_currentPlayer))
                {
                    board.UpdateBoard(startRow, startCol, endRow, endCol);

                    // קידום למלך אם נדרש
                    if (endRow == 0 || endRow == board.Size - 1)
                    {
                        board.Grid[endRow, endCol].PromoteToKing();
                    }

                    while (board.CanCapture(endRow, endCol))
                    {
                        if (Math.Abs(endRow - startRow) != 2 && Math.Abs(endCol - startCol) != 2)
                        {
                            break;
                        }

                        Console.Clear();
                        ConsoleUI.DisplayBoard(board);
                        ConsoleUI.DisplayPreTurnMessage(m_currentPlayer, m_currentPlayer);
                        move = Console.ReadLine();
                        m_currentPlayer.Move = move;
                        if (!ParseMove(move, out int nextStartRow, out int nextStartCol, out int nextEndRow, out int nextEndCol) ||
                            nextStartRow != endRow || nextStartCol != endCol)
                        {
                            ConsoleUI.DisplayFormatErrorMessage();
                            continue;
                        }

                        if (!board.IsCaptureMove(nextStartRow, nextStartCol, nextEndRow, nextEndCol, m_currentPlayer))
                        {
                            ConsoleUI.DisplayCaptureErrorMessage();
                            continue;
                        }

                        board.UpdateBoard(nextStartRow, nextStartCol, nextEndRow, nextEndCol);

                        // עדכון למהלך הבא
                        endRow = nextEndRow;
                        endCol = nextEndCol;
                    }

                    if (m_currentPlayer == player1)
                    {
                        m_currentPlayer = player2;
                        m_previusPlayer = player1;
                    }
                    else
                    {
                        m_currentPlayer = player1;
                        m_previusPlayer = player2;
                    }
                }

                else
                {
                    Console.WriteLine("Invalid move. Please try again.");
                }

            }
            ConsoleUI.DisplayNewGameMessage();
        }

        private bool ParseMove(string i_move, out int startRow, out int startCol, out int endRow, out int endCol)
        {
            startRow = startCol = endRow = endCol = -1;

            if (string.IsNullOrWhiteSpace(i_move) || i_move.Length != 5 || i_move[2] != '>')
            {
                return false;
            }

            startRow = i_move[0] - 'A';
            startCol = i_move[1] - 'a';
            endRow = i_move[3] - 'A';
            endCol = i_move[4] - 'a';
            return true;
        }

        
    }
}
