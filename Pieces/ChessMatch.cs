using ChessBoard;
using ChessGame;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _captured;
        public bool Check { get; private set; }
        public Piece EnPassant { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            EnPassant = null;
            _pieces = new HashSet<Piece>();
            _captured = new HashSet<Piece>();

            putPieces();
        }

        public Piece execMove(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMove();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.PutPiece(p, destiny);
            if (capturedPiece != null)
            {
                _captured.Add(capturedPiece);
            }

            // EN PASSANT

            if (p is Pawn)
            {
                if(origin.Row != destiny.Row && capturedPiece == null)
                {
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(destiny.Line + 1, destiny.Row);
                    }

                    else
                    {
                        posP = new Position(destiny.Line - 1, destiny.Row);
                    }
                    capturedPiece = Board.RemovePiece(posP);
                    _captured.Add(capturedPiece);
                }
            }

            // ROQUE

            if (p is King && destiny.Row == origin.Row + 2)
            {
                Position originR = new Position(origin.Line, origin.Row + 3);
                Position destinyR = new Position(origin.Line, origin.Row + 1);
                Piece r = Board.RemovePiece(originR);
                r.IncreaseMove();
                Board.PutPiece(r, destinyR);
            }

            if (p is King && destiny.Row == origin.Row - 2)
            {
                Position originR = new Position(origin.Line, origin.Row - 4);
                Position destinyR = new Position(origin.Line, origin.Row - 1);
                Piece r = Board.RemovePiece(originR);
                r.IncreaseMove();
                Board.PutPiece(r, destinyR);
            }

            return capturedPiece;

        }

        public void move(Position origin, Position destiny)
        {
            Piece capturedPiece = execMove(origin, destiny);

            if (isInCheck(CurrentPlayer))
            {
                undoMove(origin, destiny, capturedPiece);
                throw new BoardException("You can't put yourself in checkmate.");
            }

            Piece p = Board.GetPieceByPosition(destiny);

            // PROMOTION SPECIAL MOVE

            if(p is Pawn)
            {
                if (p.Color == Color.White && destiny.Line == 0 || p.Color == Color.Black && destiny.Line == 7)
                {
                    p = Board.RemovePiece(destiny);
                    _pieces.Remove(p);
                    Piece queen = new Queen(Board, p.Color);
                    Board.PutPiece(queen, destiny);
                    _pieces.Add(queen);

                }
            }

            if (isInCheck(opponent(CurrentPlayer)))
            {
                Check = true;
            }

            else
            {
                Check = false;
            }

            if (testCheckMate(opponent(CurrentPlayer)))
            {
                Finished = true;
            }

            else
            {
                Turn++;
                changePlayer();
            }

            // EN PASSANT

            if (p is Pawn && (destiny.Line == origin.Line - 2 || destiny.Line == origin.Line + 2))
            {
                EnPassant = p;
            }

            else
            {
                EnPassant = null;
            }
        }

        public void undoMove(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destiny);
            p.DecreaseMove();
            if (capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, destiny);
                _captured.Remove(capturedPiece);
            }
            Board.PutPiece(p, origin);

            // EN PASSANT

            if (p is Pawn)
            {
                if(origin.Row != destiny.Row && capturedPiece == EnPassant)
                {
                    Piece pawn = Board.RemovePiece(destiny);
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(3, destiny.Row);
                    }
                    else
                    {
                        posP = new Position(4, destiny.Row);
                    }
                    Board.PutPiece(pawn, posP);
                }
            }

            // ROQUE

            if (p is King && destiny.Row == origin.Row + 2)
            {
                Position originR = new Position(origin.Line, origin.Row + 3);
                Position destinyR = new Position(origin.Line, origin.Row + 1);
                Piece r = Board.RemovePiece(destinyR);
                r.DecreaseMove();
                Board.PutPiece(r, originR);
            }

            if (p is King && destiny.Row == origin.Row - 2)
            {
                Position originR = new Position(origin.Line, origin.Row - 4);
                Position destinyR = new Position(origin.Line, origin.Row - 1);
                Piece r = Board.RemovePiece(destinyR);
                r.DecreaseMove();
                Board.PutPiece(r, originR);
            }
        }

        public void validateOriginPosition(Position pos)
        {
            if (Board.GetPieceByPosition(pos) == null)
            {
                throw new BoardException("There is no piece in the chosen position.");
            }

            else if (CurrentPlayer != Board.GetPieceByPosition(pos).Color)
            {
                throw new BoardException("The piece chosen isn't yours.");
            }

            else if (!Board.GetPieceByPosition(pos).IsTherePossibleMove())
            {
                throw new BoardException("There are no possible moves for this piece.");
            }
        }

        public void validateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.GetPieceByPosition(origin).IsPossibleMoveToPosition(destiny))
            {
                throw new BoardException("Invalid destination position.");
            }
        }

        public void changePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }

            else
            {
                CurrentPlayer = Color.White;
            }
        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in _captured)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> pieceOnTheBoard(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in _pieces)
            {
                if (x.Color == color)
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
            Piece k = king(color);
            if (k == null)
            {
                throw new BoardException("There is no king of " + color + " color on the board.");
            }
            foreach (Piece x in pieceOnTheBoard(opponent(color)))
            {
                bool[,] mat = x.PossibleMoves();
                if (mat[k.Position.Line, k.Position.Row])
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
                bool[,] mat = x.PossibleMoves();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Rows; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
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
            Board.PutPiece(piece, new ChessPosition(row, line).toPosition());
            _pieces.Add(piece);
        }

        private void putPieces()
        {
            putNewPiece('a', 1, new Rook(Board, Color.White));
            putNewPiece('b', 1, new Knight(Board, Color.White));
            putNewPiece('c', 1, new Bishop(Board, Color.White));
            putNewPiece('d', 1, new Queen(Board, Color.White));
            putNewPiece('e', 1, new King(Board, Color.White, this));
            putNewPiece('f', 1, new Bishop(Board, Color.White));
            putNewPiece('g', 1, new Knight(Board, Color.White));
            putNewPiece('h', 1, new Rook(Board, Color.White));
            putNewPiece('a', 2, new Pawn(Board, Color.White, this));
            putNewPiece('b', 2, new Pawn(Board, Color.White, this));
            putNewPiece('c', 2, new Pawn(Board, Color.White, this));
            putNewPiece('d', 2, new Pawn(Board, Color.White, this));
            putNewPiece('e', 2, new Pawn(Board, Color.White, this));
            putNewPiece('f', 2, new Pawn(Board, Color.White, this));
            putNewPiece('g', 2, new Pawn(Board, Color.White, this));
            putNewPiece('h', 2, new Pawn(Board, Color.White, this));

            putNewPiece('a', 8, new Rook(Board, Color.Black));
            putNewPiece('b', 8, new Knight(Board, Color.Black));
            putNewPiece('c', 8, new Bishop(Board, Color.Black));
            putNewPiece('d', 8, new Queen(Board, Color.Black));
            putNewPiece('e', 8, new King(Board, Color.Black, this));
            putNewPiece('f', 8, new Bishop(Board, Color.Black));
            putNewPiece('g', 8, new Knight(Board, Color.Black));
            putNewPiece('h', 8, new Rook(Board, Color.Black));
            putNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }

    }
}
