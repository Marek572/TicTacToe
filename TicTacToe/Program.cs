using System;
using System.Threading;                                                             // for Thread.Sleep

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create two players
            HumanPlayer p1 = new HumanPlayer();                                     //or ComputerPlayer p2 = new ComputerPlayer(); to play bot vs bot ;)
            ComputerPlayer p2 = new ComputerPlayer();                               //or HumanPlayer p2 = new HumanPlayer(); to play pvp ;)
            p1.Name = "User";
            p2.Name = "AI";
            p1.Symbol = 'x';
            p2.Symbol = 'o';

            //Create two boards - with starting and with current fields
            char[,] startBoard = {
                { '7', '8', '9' },
                { '4', '5', '6' },
                { '1', '2', '3' }
            };
            char[,] gameBoard = startBoard.Clone() as char[,];

            //Flags
            bool gameEnded = false;
            bool player1Move = true; // true - player 1 move, false - player 2 move

            ////////////////////////////////////////////////////////////////////////////////

            //Loop over rounds
            for (int round = 0; round < gameBoard.Length; round++)
            {
                Console.Clear();
                Draw(gameBoard);

                if (player1Move)
                {
                    Console.WriteLine(p1.Name + " turn");
                    gameEnded = p1.MakeMove(startBoard, gameBoard);
                    player1Move = false;
                }
                else
                {
                    Console.WriteLine(p2.Name + " turn");
                    gameEnded = p2.MakeMove(startBoard, gameBoard);
                    player1Move = true;
                }
                if (gameEnded)
                    break;
            }

            ////////////////////////////////////////////////////////////////////////////////

            // End the game
            Console.Clear();
            Draw(gameBoard);
            Console.WriteLine("Game ended! ");
            if (gameEnded)
            {
                Console.Write("Winner: ");
                if(player1Move)
                    Console.WriteLine(p2.Name);
                else
                    Console.WriteLine(p1.Name);
            }
            else
                Console.WriteLine("A tie.");
        }
        
        /******************************************************************/

        static void Draw(char[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                    Console.Write(board[i, j]);
                Console.WriteLine();
            }
        }
    }

    /**********************************************************************/

    interface IMoving
    {
        bool MakeMove(char[,] startBoard, char[,] gameBoard);
    }

    abstract class Player
    {
        public string Name { get; set; }
        public char Symbol { get; set; }

        public bool CheckIfPlayerWon(char[,] gameBoard)
        {
            int height = gameBoard.GetLength(0);
            int width = gameBoard.GetLength(1);
            if (height != width)
                throw new Exception("The board is not a square!");

            //Check rows
            for(int i = 0; i< height; i++)
            {
                int rowSum = 0;
                for(int j = 0; j < width; j++)
                {
                    if (gameBoard[i,j] == Symbol)
                        rowSum++;
                }
                if (rowSum == width)
                    return true;                                    //ends the method
            }

            //Check column
            for(int i = 0; i< width; i++)
            {
                int colSum = 0;
                for(int j = 0; j < height; j++)
                {
                    if (gameBoard[i,j] == Symbol)
                        colSum++;
                }
                if (colSum == height)
                    return true;                                    //ends the method
            }

            //Check diagonals
            int diagSumA = 0;
            int diagSumB = 0;
            for (int k = 0; k < width; k++)
            {
                if (gameBoard[k, k] == Symbol)
                    diagSumA++;
                if (gameBoard[k, width - 1 - k] == Symbol)
                    diagSumB++;
            }
            if (diagSumA == width || diagSumB == width)
                return true;                                        //ends the method

            //Otherwise, no win yet
            return false;
        }

        public bool PlaceSymbol(char c, char[,] startBoard, char[,] gameBoard)
        {
            int height = gameBoard.GetLength(0);
            int width = gameBoard.GetLength(1);
            if (height != startBoard.GetLength(0) || width != startBoard.GetLength(1))
                throw new Exception("The boards have different sizes!");

            //Try to put player's symbol at a given place, if the place is available
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if ((gameBoard[i, j] == c) && (gameBoard[i, j] == startBoard[i, j]))
                    {
                        gameBoard[i, j] = Symbol;
                        return true;
                    }

            // Otherwise, return withouth success
            return false;
        }
    }

    class HumanPlayer : Player, IMoving
    {
        public bool MakeMove(char[,] startBoard, char[,] gameBoard)
        {
            //Ask human player to enter a place until (s)he picks an available one
            char chosenPlace;
            do
            {
                Console.Write("Choose an empty place: ");
                chosenPlace = Console.ReadKey().KeyChar;
                Console.WriteLine();
            }
            while (!PlaceSymbol(chosenPlace, startBoard, gameBoard));
            return CheckIfPlayerWon(gameBoard);
        }
    }

    class ComputerPlayer : Player, IMoving
    {
        public bool MakeMove(char[,] startBoard, char[,] gameBoard)
        {
            // Draw random numbers until AI player picks an available one
            Random rnd = new Random();
            char chosenPlace;
            do
            {
                int p = rnd.Next(1, gameBoard.Length + 1);                              //random 1-9
                chosenPlace = p.ToString()[0];                                          //convert digit to char
            }
            while (!PlaceSymbol(chosenPlace, startBoard, gameBoard));

            Thread.Sleep(2000);                                                         //wait 2 seconds

            return CheckIfPlayerWon(gameBoard);
        }
    }
}

