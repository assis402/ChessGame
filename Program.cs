using Chess;
using ChessBoard;
using System;

namespace ChessGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.Finished)
                {
                    try
                    {

                        Console.Clear();
                        Screen.printMatch(match);
                        
                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.writhPosition().toPosition();
                        match.validateOriginPosition(origin);

                        bool[,] possiblePositions = match.Board.GetPieceByPosition(origin).PossibleMoves();

                        Console.Clear();
                        Screen.printBoard(match.Board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = Screen.writhPosition().toPosition();
                        match.validateDestinyPosition(origin, destiny);

                        match.move(origin, destiny);

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
