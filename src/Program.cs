using System;
using ChessGame.Board;
using ChessGame.Pieces;

namespace ChessGame;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            var match = new ChessMatch();

            while (!match.Finished)
            {
                try
                {
                    Console.Clear();
                    Screen.PrintMatch(match);

                    Console.WriteLine();
                    Console.Write("Origin: ");

                    var origin = Screen.WrithPosition().ToPosition();
                    match.ValidateOriginPosition(origin);

                    var possiblePositions = match.ChessBoard.GetPieceByPosition(origin).PossibleMoves();

                    Console.Clear();
                    Screen.PrintBoard(match.ChessBoard, possiblePositions);

                    Console.WriteLine();
                    Console.Write("Destiny: ");

                    var destiny = Screen.WrithPosition().ToPosition();
                    match.ValidateDestinyPosition(origin, destiny);

                    match.TryMove(origin, destiny);
                }
                catch (BoardException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }

            Console.Clear();
            Screen.PrintMatch(match);
        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }

        Console.ReadLine();
    }
}