using ChessBoard;
using ChessGame.Chess;
using System;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);

                board.putPiece(new Tower(board, Color.Black), new Position(0, 0));
                board.putPiece(new Tower(board, Color.Black), new Position(1, 9));
                board.putPiece(new King(board, Color.Black), new Position(0, 2));

                Screen.printBoard(board);
            }

            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
