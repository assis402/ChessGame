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
                    try
                    {

                        Console.Clear();
                        Screen.printMatch(match);
                        
                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.writhPosition().toPosition();
                        match.validateOriginPosition(origin);

                        bool[,] possiblePositions = match.board.piece(origin).possibleMoves();

                        Console.Clear();
                        Screen.printBoard(match.board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = Screen.writhPosition().toPosition();
                        match.validateDestinyPosition(origin, destiny);

                        match.Move(origin, destiny);

                    }

                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }

                }
                Console.Clear();
                Screen.printMatch(match);

            }

            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
