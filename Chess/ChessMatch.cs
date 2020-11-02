using ChessBoard;
using System.Collections.Generic;

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

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            check = false;
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
                            undoMove(x.position, destiny, capturedPiece);

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
