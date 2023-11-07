using ChessBoard;

namespace Chess
{
    internal class King : Piece
    {
        private ChessMatch _match;

        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            _match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool testRookToRoque(Position pos)
        {
            Piece p = Board.GetPieceByPosition(pos);
            return p != null && p is Rook && p.Color == Color && p.TotalMoves == 0;

        }

        private bool canMove(Position pos)
        {
            Piece p = Board.GetPieceByPosition(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Rows];
            Position pos = new Position(0, 0);

            // N
            pos.DefineValues(Position.Line - 1, Position.Row);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            // NE
            pos.DefineValues(Position.Line - 1, Position.Row + 1);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            // E
            pos.DefineValues(Position.Line, Position.Row + 1);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            // SE
            pos.DefineValues(Position.Line + 1, Position.Row + 1);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            // S
            pos.DefineValues(Position.Line + 1, Position.Row);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            // SW
            pos.DefineValues(Position.Line + 1, Position.Row - 1);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            // W
            pos.DefineValues(Position.Line, Position.Row - 1);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            // NW
            pos.DefineValues(Position.Line - 1, Position.Row - 1);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            // ROQUE

            if (TotalMoves == 0 && !_match.Check)
            {
                Position posT1 = new Position(Position.Line, Position.Row+3);
                if (testRookToRoque(posT1))
                {
                    Position p1 = new Position(Position.Line, Position.Row + 1);
                    Position p2 = new Position(Position.Line, Position.Row + 2);
                    if (Board.GetPieceByPosition(p1) == null && Board.GetPieceByPosition(p2) == null)
                    {
                        mat[Position.Line, Position.Row + 2] = true;
                    }

                }

                Position posT2 = new Position(Position.Line, Position.Row - 4);
                if (testRookToRoque(posT2))
                {
                    Position p1 = new Position(Position.Line, Position.Row - 1);
                    Position p2 = new Position(Position.Line, Position.Row - 2);
                    Position p3 = new Position(Position.Line, Position.Row - 3);
                    if (Board.GetPieceByPosition(p1) == null && Board.GetPieceByPosition(p2) == null && Board.GetPieceByPosition(p3) == null)
                    {
                        mat[Position.Line, Position.Row - 2] = true;
                    }

                }
            }

            return mat;

        }
    }
}
