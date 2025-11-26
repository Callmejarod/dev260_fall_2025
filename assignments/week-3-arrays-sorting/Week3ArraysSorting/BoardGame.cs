using System;

namespace Week3ArraysSorting
{
    /// <summary>
    /// Board Game implementation for Assignment 2 Part A
    /// Demonstrates multi-dimensional arrays with interactive gameplay
    /// 
    /// Learning Focus: 
    /// - Multi-dimensional array manipulation (char[,])
    /// - Console rendering and user input
    /// - Game state management and win detection
    /// 
    /// Choose ONE game to implement:
    /// - Tic-Tac-Toe (3x3 grid)
    /// - Connect Four (6x7 grid with gravity)
    /// - Or something else creative using a 2D array! (I need to be able to understand the rules from your instructions)
    /// </summary>
    public class BoardGame
    {

        private char[,] board = new char[3, 3];  
        private char currentPlayer = 'X';        
        private bool gameOver = false;          
        private string winner = "";              

        public BoardGame()
        {
            
        }
        
        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("=== BOARD GAME (Part A) ===");
            Console.WriteLine();

            // TODO: Display game instructions
            DisplayInstructions();
            RenderBoard();
            
            bool playAgain = true;
            
            while (playAgain)
            {
                InitializeNewGame();
                PlayOneGame();
                playAgain = AskPlayAgain();
            }
            
            Console.WriteLine("Thanks for playing!");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }
        
        private void DisplayInstructions()
        {
            Console.WriteLine("=== TIC-TAC-TOE RULES ===");
            Console.WriteLine();
            Console.WriteLine("1. Two players take turns: Player X and Player O.");
            Console.WriteLine("2. Enter your move by specifying the row and column numbers separated by a space.");
            Console.WriteLine("3. First player to get 3 in a row (horizontally, vertically, or diagonally) wins.");
            Console.WriteLine("4. If all spots are filled and no player has 3 in a row, the game is a draw.");
            Console.WriteLine();
            Console.WriteLine("Press any key to start the game...");
            Console.ReadKey();
        }
        
        private void InitializeNewGame()
        {

            Console.WriteLine("New game initialized!");

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = ' ';
                }
            }

            currentPlayer = 'X';
            gameOver = false;
            winner = "";
        }
        
        private void PlayOneGame()
        {

            while (!gameOver)
            {
                RenderBoard();       // show the current board
                GetPlayerMove();     // ask the current player for their move
                CheckWinCondition(); // see if that move caused a win or draw

                if (!gameOver)
                    SwitchPlayer();  // toggle X ↔ O
            }

            RenderBoard(); // show final board

            if (winner != "")
                Console.WriteLine($"Player {winner} wins!");
            else
                Console.WriteLine("It's a draw!");

        }
        
        /// <summary>
        /// Render the current board state to console
        /// TODO: Create clear, readable board display
        /// </summary>
        private void RenderBoard()
        {

            Console.Clear();
            Console.WriteLine("Current Board:\n");

            // Column labels
            Console.WriteLine("    0   1   2");

            for (int row = 0; row < 3; row++)
            {
                Console.Write($"{row} "); // Row label
                for (int col = 0; col < 3; col++)
                {
                    Console.Write($" {board[row, col]} ");
                    if (col < 2)
                        Console.Write("|");
                }

                Console.WriteLine();

                if (row < 2)
                    Console.WriteLine("   ---+---+---");
            }

            Console.WriteLine();
        }
        
        /// <summary>
        /// Get and validate player move input
        /// TODO: Handle user input with validation
        /// </summary>
        private void GetPlayerMove()
        {
            bool validMove = false;

            while (!validMove)
            {
                Console.Write($"Player {currentPlayer}, enter your move (row and column): ");
                string? input = Console.ReadLine();

                string[] parts = input?.Split(' ') ?? Array.Empty<string>();
                if (parts.Length != 2 
                    || !int.TryParse(parts[0], out int row) 
                    || !int.TryParse(parts[1], out int col))
                {
                    Console.WriteLine("Invalid input. Enter two numbers separated by a space (e.g., 1 2).");
                    continue;
                }

                if (row < 0 || row > 2 || col < 0 || col > 2)
                {
                    Console.WriteLine("Move out of bounds. Rows and columns are 0-2.");
                    continue;
                }

                if (board[row, col] != ' ')
                {
                    Console.WriteLine("That spot is already taken!");
                    continue;
                }

                board[row, col] = currentPlayer;
                validMove = true;
            }
        }
        
        /// <summary>
        /// Check if current board state has a winner or draw
        /// TODO: Implement win detection logic
        /// </summary>
        private void CheckWinCondition()
        {
            // Check rows
            for (int row = 0; row < 3; row++)
            {
                if (board[row, 0] == currentPlayer &&
                    board[row, 1] == currentPlayer &&
                    board[row, 2] == currentPlayer)
                {
                    winner = currentPlayer.ToString();
                    gameOver = true;
                    return;
                }
            }

            // Check columns
            for (int col = 0; col < 3; col++)
            {
                if (board[0, col] == currentPlayer &&
                    board[1, col] == currentPlayer &&
                    board[2, col] == currentPlayer)
                {
                    winner = currentPlayer.ToString();
                    gameOver = true;
                    return;
                }
            }

            // Check diagonals
            if ((board[0, 0] == currentPlayer &&
                board[1, 1] == currentPlayer &&
                board[2, 2] == currentPlayer) ||
                (board[0, 2] == currentPlayer &&
                board[1, 1] == currentPlayer &&
                board[2, 0] == currentPlayer))
            {
                winner = currentPlayer.ToString();
                gameOver = true;
                return;
            }

            // Check for draw
            bool boardFull = true;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == ' ')
                    {
                        boardFull = false;
                        break;
                    }
                }
                if (!boardFull) break;
            }

            if (boardFull)
            {
                winner = "";  // no winner
                gameOver = true;
            }
        }

        
        private bool AskPlayAgain()
        {
            while (true)
            {
                Console.Write("Do you want to play again? (y/n): ");
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "y" || input == "yes")
                    return true;
                else if (input == "n" || input == "no")
                    return false;
                else
                    Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }
        
        private void SwitchPlayer()
        {
            currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
        }
        
        // TODO: Add helper methods as needed
        // Examples:
        // - IsValidMove(int row, int col)
        // - IsBoardFull()
        // - CheckRow(int row, char player)
        // - CheckColumn(int col, char player)
        // - CheckDiagonals(char player)
        // - DropToken(int column, char player) // For Connect Four
    }
}