using ChessBoard;
using ChessGame;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Chess
{
    class ChessMatch
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool check { get; private set; }
        public Piece enPassant { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            check = false;
            enPassant = null;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();

            putPieces();
        }

        public Piece execMove(Position origin, Position destiny)
        {
            Piece p = board.removePiece(origin);
            p.increaseQtsMoves();
            Piece capturedPiece = board.removePiece(destiny);
            board.putPiece(p, destiny);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }

            // EN PASSANT

            if (p is Pawn)
            {
                if(origin.row != destiny.row && capturedPiece == null)
                {
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(destiny.line + 1, destiny.row);
                    }

                    else
                    {
                        posP = new Position(destiny.line - 1, destiny.row);
                    }
                    capturedPiece = board.removePiece(posP);
                    captured.Add(capturedPiece);
                }
            }

            // ROQUE

            if (p is King && destiny.row == origin.row + 2)
            {
                Position originR = new Position(origin.line, origin.row + 3);
                Position destinyR = new Position(origin.line, origin.row + 1);
                Piece R = board.removePiece(originR);
                R.increaseQtsMoves();
                board.putPiece(R, destinyR);
            }

            if (p is King && destiny.row == origin.row - 2)
            {
                Position originR = new Position(origin.line, origin.row - 4);
                Position destinyR = new Position(origin.line, origin.row - 1);
                Piece R = board.removePiece(originR);
                R.increaseQtsMoves();
                board.putPiece(R, destinyR);
            }

            return capturedPiece;

        }

        public void Move(Position origin, Position destiny)
        {
            Piece capturedPiece = execMove(origin, destiny);

            if (isInCheck(currentPlayer))
            {
                undoMove(origin, destiny, capturedPiece);
                throw new BoardException("You can't put yourself in checkmate.");
            }

            Piece p = board.piece(destiny);

            // PROMOTION SPECIAL MOVE

            if(p is Pawn)
            {
                if (p.color == Color.White && destiny.line == 0 || p.color == Color.Black && destiny.line == 7)
                {
                    p = board.removePiece(destiny);
                    pieces.Remove(p);
                    Piece queen = new Queen(board, p.color);
                    board.putPiece(queen, destiny);
                    pieces.Add(queen);

                }
            }

            if (isInCheck(opponent(currentPlayer)))
            {
                check = true;
            }

            else
            {
                check = false;
            }

            if (testCheckMate(opponent(currentPlayer)))
            {
                finished = true;
            }

            else
            {
                turn++;
                changePlayer();
            }

            // EN PASSANT

            if (p is Pawn && (destiny.line == origin.line - 2 || destiny.line == origin.line + 2))
            {
                enPassant = p;
            }

            else
            {
                enPassant = null;
            }
        }

        public void undoMove(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = board.removePiece(destiny);
            p.retireQtsMoves();
            if (capturedPiece != null)
            {
                board.putPiece(capturedPiece, destiny);
                captured.Remove(capturedPiece);
            }
            board.putPiece(p, origin);

            // EN PASSANT

            if (p is Pawn)
            {
                if(origin.row != destiny.row && capturedPiece == enPassant)
                {
                    Piece pawn = board.removePiece(destiny);
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(3, destiny.row);
                    }
                    else
                    {
                        posP = new Position(4, destiny.row);
                    }
                    board.putPiece(pawn, posP);
                }
            }

            // ROQUE

            if (p is King && destiny.row == origin.row + 2)
            {
                Position originR = new Position(origin.line, origin.row + 3);
                Position destinyR = new Position(origin.line, origin.row + 1);
                Piece R = board.removePiece(destinyR);
                R.retireQtsMoves();
                board.putPiece(R, originR);
            }

            if (p is King && destiny.row == origin.row - 2)
            {
                Position originR = new Position(origin.line, origin.row - 4);
                Position destinyR = new Position(origin.line, origin.row - 1);
                Piece R = board.removePiece(destinyR);
                R.retireQtsMoves();
                board.putPiece(R, originR);
            }
        }

        public void validateOriginPosition(Position pos)
        {
            if (board.piece(pos) == null)
            {
                throw new BoardException("There is no piece in the chosen position.");
            }

            else if (currentPlayer != board.piece(pos).color)
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
            if (!board.piece(origin).possibleMove(destiny))
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
            foreach (Piece x in captured)
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
            foreach (Piece x in pieces)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        private Color opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece x in pieceOnTheBoard(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool isInCheck(Color color)
        {
            Piece K = king(color);
            if (K == null)
            {
                throw new BoardException("There is no king of " + color + " color on the board.");
            }
            foreach (Piece x in pieceOnTheBoard(opponent(color)))
            {
                bool[,] mat = x.possibleMoves();
                if (mat[K.position.line, K.position.row])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testCheckMate(Color color)
        {
            if (!isInCheck(color))
            {
                return false;
            }
            foreach (Piece x in pieceOnTheBoard(color))
            {
                bool[,] mat = x.possibleMoves();
                for (int i = 0; i < board.Lines; i++)
                {
                    for (int j = 0; j < board.Rows; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = execMove(origin, destiny);
                            bool testCheck = isInCheck(color);
                            undoMove(origin, destiny, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void putNewPiece(char row, int line, Piece piece)
        {
            board.putPiece(piece, new ChessPosition(row, line).toPosition());
            pieces.Add(piece);
        }

        private void putPieces()
        {
            putNewPiece('a', 1, new Rook(board, Color.White));
            putNewPiece('b', 1, new Knight(board, Color.White));
            putNewPiece('c', 1, new Bishop(board, Color.White));
            putNewPiece('d', 1, new Queen(board, Color.White));
            putNewPiece('e', 1, new King(board, Color.White, this));
            putNewPiece('f', 1, new Bishop(board, Color.White));
            putNewPiece('g', 1, new Knight(board, Color.White));
            putNewPiece('h', 1, new Rook(board, Color.White));
            putNewPiece('a', 2, new Pawn(board, Color.White, this));
            putNewPiece('b', 2, new Pawn(board, Color.White, this));
            putNewPiece('c', 2, new Pawn(board, Color.White, this));
            putNewPiece('d', 2, new Pawn(board, Color.White, this));
            putNewPiece('e', 2, new Pawn(board, Color.White, this));
            putNewPiece('f', 2, new Pawn(board, Color.White, this));
            putNewPiece('g', 2, new Pawn(board, Color.White, this));
            putNewPiece('h', 2, new Pawn(board, Color.White, this));

            putNewPiece('a', 8, new Rook(board, Color.Black));
            putNewPiece('b', 8, new Knight(board, Color.Black));
            putNewPiece('c', 8, new Bishop(board, Color.Black));
            putNewPiece('d', 8, new Queen(board, Color.Black));
            putNewPiece('e', 8, new King(board, Color.Black, this));
            putNewPiece('f', 8, new Bishop(board, Color.Black));
            putNewPiece('g', 8, new Knight(board, Color.Black));
            putNewPiece('h', 8, new Rook(board, Color.Black));
            putNewPiece('a', 7, new Pawn(board, Color.Black, this));
            putNewPiece('b', 7, new Pawn(board, Color.Black, this));
            putNewPiece('c', 7, new Pawn(board, Color.Black, this));
            putNewPiece('d', 7, new Pawn(board, Color.Black, this));
            putNewPiece('e', 7, new Pawn(board, Color.Black, this));
            putNewPiece('f', 7, new Pawn(board, Color.Black, this));
            putNewPiece('g', 7, new Pawn(board, Color.Black, this));
            putNewPiece('h', 7, new Pawn(board, Color.Black, this));
        }

    }
}
