﻿using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create two boards - with starting and with current fields
            char[,] startBoard = {
                { '1', '2', '3' },
                { '4', '5', '6' },
                { '7', '8', '9' }
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
                // TODO: draw the board
                if (player1Move)
                {
                    // TODO: player 1 move
                    player1Move = false;
                }
                else
                {
                    // TODO: player 2 move
                    player1Move = true;
                }
                if (gameEnded)
                    break;
            }
            ////////////////////////////////////////////////////////////////////////////////

            // End the game
            Console.Clear();
            // TODO: draw the board
            Console.Write("Game ended! ");
            // TODO: print who won
        }
    }
}