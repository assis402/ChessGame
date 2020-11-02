﻿using ChessBoard;
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
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;

            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();

            putPieces();
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

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in captured)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> pieceOnTheBoard(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        public void execMove(Position origin, Position destiny)
        {
            Piece p = board.removePiece(origin);
            p.increaseQtsMoves();
            Piece capturedPiece = board.removePiece(destiny);
            board.putPiece(p, destiny);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
        }

        public void putNewPiece(char row, int line, Piece piece)
        {
            board.putPiece(piece, new ChessPosition(row, line).toPosition());
            pieces.Add(piece);
        }

        private void putPieces()
        {
            putNewPiece('c', 1, new Tower(board, Color.White));
            putNewPiece('c', 2, new Tower(board, Color.White));
            putNewPiece('d', 2, new Tower(board, Color.White));
            putNewPiece('e', 2, new Tower(board, Color.White));
            putNewPiece('e', 1, new Tower(board, Color.White));
            putNewPiece('d', 1, new King(board, Color.White));

            putNewPiece('c', 7, new Tower(board, Color.Black));
            putNewPiece('c', 8, new Tower(board, Color.Black));
            putNewPiece('d', 7, new Tower(board, Color.Black));
            putNewPiece('e', 7, new Tower(board, Color.Black));
            putNewPiece('e', 8, new Tower(board, Color.Black));
            putNewPiece('d', 8, new King(board, Color.Black));

        }

    }
}
