using System;
using System.Collections.Generic;
using System.Linq;
using ChessGame.Board;
using ChessGame.Pieces;

namespace ChessGame;

internal static class Screen
{
    public static void PrintMatch(ChessMatch match)
    {
        var originalBackground = Console.BackgroundColor;
        var redBackground = ConsoleColor.DarkRed;
        var yellowBackground = ConsoleColor.DarkYellow;

        PrintBoard(match.ChessBoard);
        Console.WriteLine();
        
        PrintCapturedPieces(match);
        Console.WriteLine();
        Console.WriteLine("Turn: " + match.Turn + "º");
        
        if (!match.Finished)
        {
            Console.WriteLine("Next move: " + match.CurrentPlayer);
            
            if (match.Check)
            {
                Console.BackgroundColor = yellowBackground;
                Console.WriteLine("CHECK!");
                Console.BackgroundColor = originalBackground;
            }
        }
        else
        {
            Console.BackgroundColor = redBackground;
            Console.WriteLine("CHECKMATE!");
            Console.BackgroundColor = originalBackground;
            Console.WriteLine("Winner: " + match.CurrentPlayer);
        }
    }

    private static void PrintCapturedPieces(ChessMatch match)
    {
        Console.WriteLine("Captured pieces: ");
        Console.Write("White: ");
        PrintGroup(match.GetCapturedPieces(Color.White));
        Console.WriteLine();
        Console.Write("Black: ");
        var aux = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        PrintGroup(match.GetCapturedPieces(Color.Black));
        Console.ForegroundColor = aux;
        Console.WriteLine();
    }

    private static void PrintGroup(HashSet<Piece> group)
    {
        Console.Write("[");
        
        if (group.Count == 1)
        {
            Console.Write(group.ElementAt(0));
        }
        else if (group.Count > 1)
        {
            Console.Write(group.ElementAt(0));
            
            for (var i = 1; i < group.Count; i++) 
                Console.Write(" " + group.ElementAt(i));
        }

        Console.Write("]");
    }

    private static void PrintBoard(ChessBoard chessBoard)
    {
        for (var i = 0; i < chessBoard.Lines; i++)
        {
            Console.Write(" " + (8 - i) + " ");
            
            for (var j = 0; j < chessBoard.Rows; j++) 
                PrintPiece(chessBoard.GetPieceByPosition(i, j));

            Console.WriteLine();
        }

        Console.WriteLine("   a b c d e f g h ");
    }

    public static void PrintBoard(ChessBoard chessBoard, bool[,] possiblePositions)
    {
        for (var i = 0; i < chessBoard.Lines; i++)
        {
            Console.Write(" " + (8 - i) + " ");
            var originalBackground = Console.BackgroundColor;
            var redBackground = ConsoleColor.DarkRed;

            for (var j = 0; j < chessBoard.Rows; j++)
            {
                if (possiblePositions[i, j])
                    Console.BackgroundColor = redBackground;
                else
                    Console.BackgroundColor = originalBackground;

                PrintPiece(chessBoard.GetPieceByPosition(i, j));
            }
            
            Console.BackgroundColor = originalBackground;
            Console.WriteLine();
        }

        Console.WriteLine("   a b c d e f g h ");
    }

    private static void PrintPiece(Piece piece)
    {
        if (piece == null)
        {
            Console.Write("- ");
        }
        else
        {
            if (piece.Color == Color.White)
            {
                Console.Write(piece);
            }
            else
            {
                var aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }

            Console.Write(" ");
        }
    }

    public static ChessPosition WrithPosition()
    {
        var s = Console.ReadLine();
        var row = s[0];
        var line = int.Parse(s[1] + "");
        
        return new ChessPosition(row, line);
    }
}