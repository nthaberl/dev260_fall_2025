using System;
using System.Linq;
using static System.Console;

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
        // TODO: Choose your game and declare the appropriate board
        // Option 1: Tic-Tac-Toe
        private string[,] board = new string[3, 3];

        // Option 2: Connect Four  
        // private char[,] board = new char[6, 7]; // 6 rows, 7 columns

        // Option 3: Your own game
        // private char[,] board = new char[ROWS, COLUMNS]; // Define your own size

        // TODO: Add fields for game state
        private string currentPlayer = "❌";
        private bool gameOver = false;
        private string winner = "";

        /// <summary>
        /// Constructor - Initialize the board game
        /// TODO: Set up your chosen game
        /// </summary>
        public BoardGame()
        {
            // TODO: Initialize your board array
            // For Tic-Tac-Toe or Connect Four, fill with empty spaces or dots
            // ❌ ⭕ -> use for Tic-Tac-Toe if you'd like for each square/player and the white box ⬜ from array example

            // Console.WriteLine("BoardGame constructor - TODO: Initialize your chosen game");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = "⬜";
                }
            }
        }

        /// <summary>
        /// Main game loop - handles the complete game session
        /// TODO: Implement the full game experience
        /// </summary>
        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("=== BOARD GAME (Part A) ===");
            WriteLine("  ______   __     ______        ______   ______     ______        ______   ______     ______    ");
            WriteLine(@"/\__  _\ /\ \   /\  ___\      /\__  _\ /\  __ \   /\  ___\      /\__  _\ /\  __ \   /\  ___\   ");
            WriteLine(@"\/_/\ \/ \ \ \  \ \ \____     \/_/\ \/ \ \  __ \  \ \ \____     \/_/\ \/ \ \ \/\ \  \ \  __\   ");
            WriteLine(@"   \ \_\  \ \_\  \ \_____\       \ \_\  \ \_\ \_\  \ \_____\       \ \_\  \ \_____\  \ \_____\ ");
            WriteLine(@"    \/_/   \/_/   \/_____/        \/_/   \/_/\/_/   \/_____/        \/_/   \/_____/   \/_____/ ");

            Console.WriteLine();

            // TODO: Display game instructions
            DisplayInstructions();

            // TODO: Implement main game loop
            bool playAgain = true;

            while (playAgain)
            {
                // TODO: Reset game state for new game
                InitializeNewGame();

                // TODO: Play one complete game
                PlayOneGame();

                // TODO: Ask if player wants to play again
                playAgain = AskPlayAgain();
            }

            Console.WriteLine("Thanks for playing!");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }

        /// <summary>
        /// Display game instructions and controls
        /// TODO: Customize for your chosen game
        /// </summary>
        private void DisplayInstructions()
        {
            //Console.WriteLine("TODO: Add instructions for your chosen game");

            // Example for Tic-Tac-Toe:
            Console.WriteLine("TIC-TAC-TOE RULES:");
            Console.WriteLine("- Players take turns placing X and O");
            Console.WriteLine("- Enter row and column (0-2) when prompted");
            Console.WriteLine("- First to get 3 in a row wins!");

            Console.WriteLine();
        }

        /// <summary>
        /// Initialize/reset the game for a new round
        /// TODO: Reset board and game state
        /// </summary>
        private void InitializeNewGame()
        {
            // TODO: reset board
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = "⬜";
                }
            }

            // TODO: Reset current player to 'X' x
            currentPlayer = "❌";

            // TODO: Reset game over flag x
            gameOver = false;

            // TODO: Clear winner x
            winner = "";

            WriteLine("A new game begins!");
            WriteLine();
            WriteLine("❌ goes first");
            WriteLine();
        }

        /// <summary>
        /// Play one complete game until win/draw/quit
        /// TODO: Implement the core game loop
        /// </summary>
        private void PlayOneGame()
        {
            // TODO: Game loop structure:
            // Console.WriteLine("TODO: Implement main game loop");
            // Console.WriteLine("This should include:");
            // Console.WriteLine("1. Render board to console");
            // Console.WriteLine("2. Get and validate player input");
            // Console.WriteLine("3. Update board with move");
            // Console.WriteLine("4. Check for win/draw conditions");
            // Console.WriteLine("5. Switch to next player");
            while (!gameOver)
            {
                RenderBoard();
                var (row, col) = GetPlayerMove();
                UpdateBoard(row, col);
                CheckWinCondition();
                if (!gameOver)
                {
                    SwitchPlayer();
                }
            }

            DisplayWinMessage();
        }

        /// <summary>
        /// Render the current board state to console
        /// TODO: Create clear, readable board display
        /// </summary>
        private void RenderBoard()
        {
            // TODO: Display your multi-dimensional array as a visual board
            // Requirements:
            // - Clear, human-readable format
            // - Show current board state
            // - Include row/column labels for easy reference

            // Print column headers
            Write("   "); // padding for labels
            for (int j = 0; j < board.GetLength(1); j++)
            {
                Write($"{j + 1}  ");
            }
            WriteLine();

            //print row labels
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Write($"{i + 1} ");

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Write(board[i, j] + " ");
                }

                WriteLine();
            }
        }

        /// <summary>
        /// Get and validate player move input
        /// TODO: Handle user input with validation
        /// </summary>
        private (int, int) GetPlayerMove()
        {
            // TODO: Prompt current player for their move
            // TODO: Validate input (in bounds, empty cell, etc.)
            // TODO: Keep asking until valid move is entered

            int row = 0, col = 0;
            bool validMove = false;

            while (!validMove)
            {

                while (true)
                {
                    //get valid row
                    WriteLine("Please enter a row (1-3): ");
                    string input = ReadLine();
                    if (int.TryParse(input, out row) && row >= 1 && row <= 3)
                    {
                        break;
                    }
                    else
                    {
                        WriteLine("Invalid input, please enter a number between 1 and 3");
                    }
                }

                //get valid column
                while (true)
                {
                    //get row
                    WriteLine("Please enter a column (1-3): ");
                    string input = ReadLine();
                    if (int.TryParse(input, out col) && col >= 1 && col <= 3)
                    {
                        break;
                    }
                    else
                    {
                        WriteLine("Invalid input, please enter a number between 1 and 3");
                    }
                }

                //validate move
                if (board[row - 1, col - 1] == "⬜")
                {
                    validMove = true;
                }
                else
                {
                    WriteLine("Space is already taken! Please choose another space");
                }
            }

            //subtracting 1 since array is 0-indexed
            return (row - 1, col - 1);
        }

        /// <summary>
        /// update board with the current player's move
        /// </summary>
        private void UpdateBoard(int row, int col)
        {
            board[row, col] = currentPlayer;
        }

        /// <summary>
        /// Check if current board state has a winner or draw
        /// TODO: Implement win detection logic
        /// </summary>
        private void CheckWinCondition()
        {
            // TODO: Check for win conditions specific to your game
            // For Tic-Tac-Toe:
            // - Check all rows, columns, and diagonals for 3 in a row
            // - Check for draw (board full, no winner)

            //check rows
            for (int i = 0; i < board.GetLength(0); i++)
            {
                //check rows
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != "⬜")
                {
                    winner = currentPlayer;
                    gameOver = true;
                    return;
                }
            }

            //check columns
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[0, j] == board[1, j] && board[1, j] == board[2, j] && board[0, j] != "⬜")
                {
                    winner = currentPlayer;
                    gameOver = true;
                    return;
                }
            }

            //check diagonals
            if ((board[0, 0] == board[1, 1] && board[2, 2] == board[1, 1] && board[0, 0] != "⬜")
            || (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != "⬜"))
            {
                winner = currentPlayer;
                gameOver = true;
                return;
            }

            //check draw
            bool isDraw = true;
            foreach (var cell in board)
            {
                if (cell == "⬜")
                {
                    isDraw = false;
                }
            }

            if (isDraw)
            {
                winner = "draw";
                gameOver = true;
            }
        }

        /// <summary>
        /// Ask player if they want to play another game
        /// TODO: Simple yes/no prompt with validation
        /// </summary>
        private bool AskPlayAgain()
        {
            // TODO: Ask user if they want to play again
            WriteLine("Would you like to play again?");

            while (true)
            {
                //valid inputs
                string[] continueGame = new string[] { "YES", "Y", "SURE", "OK", "YES PLEASE" };
                string[] quit = new string[] { "NO", "N", "NAH", "NO THANKS" };

                // TODO: Validate input (y/n, yes/no, etc.)
                // TODO: Return true for play again, false to return to main menu
                string response = ReadLine().Trim().ToUpper();
                if (continueGame.Contains(response))
                {
                    return true;
                }
                else if (quit.Contains(response))
                {
                    return false;
                }
                else
                {
                    WriteLine("Please enter a valid response: yes or no");
                }
            }
        }

        /// <summary>
        /// Switch to the next player's turn
        /// TODO: Toggle between X and O
        /// </summary>
        private void SwitchPlayer()
        {
            // TODO: Switch currentPlayer between 'X' and 'O'
            currentPlayer = (currentPlayer == "❌") ? "⭕" : "❌";
            WriteLine();
            WriteLine($"{currentPlayer} it's your turn!");
            WriteLine();
        }

        private void DisplayWinMessage()
        {
            if (winner == "draw")
            {
                WriteLine("The game ended in a draw...");
                WriteLine();
            }
            else
            {
                WriteLine();
                WriteLine();
                WriteLine("   ______     ______     __   __     ______     ______     ______     ______   ______    ");
                WriteLine(@" /\  ___\   /\  __ \   /\ ' -.\ \   /\  ___\   /\  == \   /\  __ \   /\__  _\ /\  ___\   ");
                WriteLine(@" \ \ \____  \ \ \/\ \  \ \ \-.,  \  \ \ \__ \  \ \  __<   \ \  __ \  \/_/\ \/ \ \___  \  ");
                WriteLine(@"  \ \_____\  \ \_____\  \ \_\ \'\_\  \ \_____\  \ \_\ \_\  \ \_\ \_\    \ \_\  \/\_____\ ");
                WriteLine(@"   \/_____/   \/_____/   \/_/  \/_/   \/_____/   \/_/ /_/   \/_/\/_/     \/_/   \/_____/ ");
                WriteLine();
                WriteLine($"{winner} wins!");
                WriteLine();
            }
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