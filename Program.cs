using ChessBoard;
using ChessGame.Chess;
using System;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessPosition pos = new ChessPosition('a', 1);

            Console.WriteLine(pos);

            Console.WriteLine(pos.toPosition());

            Console.ReadLine();
        }
    }
}
