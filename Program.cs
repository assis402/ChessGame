using Chess;
using ChessBoard;
using System;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.finished)
                {
                    Console.Clear();
                    Screen.printBoard(match.board);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.writhPosition().toPosition();
                    Console.Write("Destiny: ");
                    Position destiny = Screen.writhPosition().toPosition();

                    match.execMove(origin, destiny);

                }

                
            }

            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
