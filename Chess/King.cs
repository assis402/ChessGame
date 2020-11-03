using ChessBoard;

namespace Chess
{
    class King : Piece
    {
        private ChessMatch match;

        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool testRookToRoque(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p is Rook && p.color == color && p.qtsMoves == 0;

        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.Lines, board.Rows];
            Position pos = new Position(0, 0);

            // N
            pos.defineValues(position.line - 1, position.row);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            // NE
            pos.defineValues(position.line - 1, position.row + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            // E
            pos.defineValues(position.line, position.row + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            // SE
            pos.defineValues(position.line + 1, position.row + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            // S
            pos.defineValues(position.line + 1, position.row);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            // SW
            pos.defineValues(position.line + 1, position.row - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            // W
            pos.defineValues(position.line, position.row - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            // NW
            pos.defineValues(position.line - 1, position.row - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            // ROQUE

            if (qtsMoves == 0 && !match.check)
            {
                Position posT1 = new Position(position.line, position.row+3);
                if (testRookToRoque(posT1))
                {
                    Position p1 = new Position(position.line, position.row + 1);
                    Position p2 = new Position(position.line, position.row + 2);
                    if (board.piece(p1) == null && board.piece(p2) == null)
                    {
                        mat[position.line, position.row + 2] = true;
                    }

                }

                Position posT2 = new Position(position.line, position.row - 4);
                if (testRookToRoque(posT2))
                {
                    Position p1 = new Position(position.line, position.row - 1);
                    Position p2 = new Position(position.line, position.row - 2);
                    Position p3 = new Position(position.line, position.row - 3);
                    if (board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null)
                    {
                        mat[position.line, position.row - 2] = true;
                    }

                }
            }

            return mat;

        }
    }
}
