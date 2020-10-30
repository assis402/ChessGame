using ChessBoard;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame
{
    class Screen
    {
        public static void printBoard(Board board)
        {
            for (int i= 0; i< board.lines; i++)
            {
                for (int j= 0; j< board.rows; j++)
                {
                    if (board.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(board.piece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
