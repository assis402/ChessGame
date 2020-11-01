using ChessBoard;
using ChessGame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class ChessMatch
    {
        public Board board { get; set; }
        public int turn { get; set; }
        public Color currentPlayer { get; set; }
        public bool finished { get; set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            putPieces();
            finished = false;
        }

        public void Move(Position origin, Position destiny)
        {
            execMove(origin, destiny);
            turn++;
            changePlayer();
        }

        public void validateOriginPosition(Position pos)
        {
            if (board.piece(pos) == null)
            {
                throw new BoardException("There is no piece in the chosen position.");
            }

            else if(currentPlayer != board.piece(pos).color)
            {
                throw new BoardException("The piece chosen isn't yours.");
            }

            else if (!board.piece(pos).isTherePossibleMove())
            {
                throw new BoardException("There are no possible moves for this piece.");
            }
        }

        public void validateDestinyPosition(Position origin, Position destiny) 
        {
            if (!board.piece(origin).canMoveTo(destiny))
            {
                throw new BoardException("Invalid destination position.");
            }
        }

        public void changePlayer()
        {
            if (currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            }

            else
            {
                currentPlayer = Color.White;
            }
        }

        public void execMove(Position origin, Position destiny)
        {
            Piece p = board.removePiece(origin);
            p.increaseQtsMoves();
            Piece capturedPiece = board.removePiece(destiny);
            board.putPiece(p, destiny);
        }

        private void putPieces()
        {
            board.putPiece(new Tower(board, Color.White), new ChessPosition('c', 1).toPosition());
            board.putPiece(new Tower(board, Color.White), new ChessPosition('c', 2).toPosition());
            board.putPiece(new Tower(board, Color.White), new ChessPosition('d', 2).toPosition());
            board.putPiece(new Tower(board, Color.White), new ChessPosition('e', 2).toPosition());
            board.putPiece(new Tower(board, Color.White), new ChessPosition('e', 1).toPosition());
            board.putPiece(new King(board, Color.White), new ChessPosition('d', 1).toPosition());

            board.putPiece(new Tower(board, Color.Black), new ChessPosition('c', 7).toPosition());
            board.putPiece(new Tower(board, Color.Black), new ChessPosition('c', 8).toPosition());
            board.putPiece(new Tower(board, Color.Black), new ChessPosition('d', 7).toPosition());
            board.putPiece(new Tower(board, Color.Black), new ChessPosition('e', 7).toPosition());
            board.putPiece(new Tower(board, Color.Black), new ChessPosition('e', 8).toPosition());
            board.putPiece(new King(board, Color.Black), new ChessPosition('d', 8).toPosition());



        }

    }
}
