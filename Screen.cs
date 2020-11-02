using Chess;
using ChessBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;

namespace ChessGame
{
    class Screen
    {
        public static void printMatch(ChessMatch match)
        {
            printBoard(match.board);
            Console.WriteLine();
            printCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turn: " + match.turn + "º");
            Console.WriteLine("Next move: " + match.currentPlayer);
            if (match.checkMate)
            {
                Console.WriteLine("XEQUE");
            }
        }

        public static void printCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured pieces: ");
            Console.Write("White: ");
            printGroup(match.capturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            printGroup(match.capturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();

        }

        public static void printGroup(HashSet<Piece> group)
        {
            Console.Write("[");

            if (group.Count == 1)
            {
                Console.Write(group.ElementAt(0));
            }

            else if (group.Count > 1)
            {
                Console.Write(group.ElementAt(0));
                for (int i = 1; i < group.Count; i++)
                {
                    Console.Write(" " + group.ElementAt(i));
                }
            }

            Console.Write("]");
        }

        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(" " + (8 - i) + " ");
                for (int j = 0; j < board.Rows; j++)
                {
                    Screen.printPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }

            Console.WriteLine("   a b c d e f g h ");
        }

        public static void printBoard(Board board, bool[,] possiblePositions)
        {


            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(" " + (8 - i) + " ");
                ConsoleColor originalBackground = Console.BackgroundColor;
                ConsoleColor alteredBackground = ConsoleColor.DarkRed;

                for (int j = 0; j < board.Rows; j++)
                {


                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = alteredBackground;
                    }

                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }

                    Screen.printPiece(board.piece(i, j));
                }
                Console.BackgroundColor = originalBackground;
                Console.WriteLine();
            }

            Console.WriteLine("   a b c d e f g h ");

        }

        public static void printPiece(Piece piece)
        {

            if (piece == null)
            {
                Console.Write("- ");
            }

            else
            {
                if (piece.color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }


                Console.Write(" ");
            }
        }

        public static ChessPosition writhPosition()
        {
            string s = Console.ReadLine();
            char row = s[0];
            int line = int.Parse(s[1] + " ");
            return new ChessPosition(row, line);

        }
    }
}
