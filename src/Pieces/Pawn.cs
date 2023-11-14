using ChessGame.Board;

namespace ChessGame.Pieces;

internal class Pawn : Piece
{
    private readonly ChessMatch _match;
    
    public Pawn(ChessBoard chessBoard, Color color, ChessMatch match) : base(chessBoard, color) 
        => _match = match;

    private bool Enemy(Position pos)
    {
        var p = ChessBoard.GetPieceByPosition(pos);
        return p != null && p.Color != Color;
    }

    private bool Free(Position pos)
    {
        return ChessBoard.GetPieceByPosition(pos) == null;
    }

    public override bool[,] PossibleMoves()
    {
        var mat = new bool[ChessBoard.Lines, ChessBoard.Rows];
        var pos = new Position(0, 0);

        if (Color == Color.White)
        {
            pos.DefineValues(Position.Line - 1, Position.Row);
            if (ChessBoard.ValidatePosition(pos) && Free(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            pos.DefineValues(Position.Line - 2, Position.Row);
            var p2 = new Position(Position.Line - 1, Position.Row);
            if (ChessBoard.ValidatePosition(p2) && Free(p2) && ChessBoard.ValidatePosition(pos) && Free(pos) && TotalMoves == 0)
            {
                mat[pos.Line, pos.Row] = true;
            }
            pos.DefineValues(Position.Line - 1, Position.Row - 1);
            if (ChessBoard.ValidatePosition(pos) && Enemy(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            pos.DefineValues(Position.Line - 1, Position.Row + 1);
            if (ChessBoard.ValidatePosition(pos) && Enemy(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            // EN PASSANT

            if (Position.Line == 3)
            {
                var left = new Position(Position.Line, Position.Row - 1);
                if (ChessBoard.ValidatePosition(left) && Enemy(left) && ChessBoard.GetPieceByPosition(left) == _match.EnPassant)
                {
                    mat[left.Line - 1, left.Row] = true;
                }
                var right = new Position(Position.Line, Position.Row + 1);
                if (ChessBoard.ValidatePosition(right) && Enemy(right) && ChessBoard.GetPieceByPosition(right) == _match.EnPassant)
                {
                    mat[right.Line - 1, right.Row] = true;
                }
            }
        }

        else
        {
            pos.DefineValues(Position.Line + 1, Position.Row);
            if (ChessBoard.ValidatePosition(pos) && Free(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            pos.DefineValues(Position.Line + 2, Position.Row);
            var p2 = new Position(Position.Line + 1, Position.Row);
            if (ChessBoard.ValidatePosition(p2) && Free(p2) && ChessBoard.ValidatePosition(pos) && Free(pos) && TotalMoves == 0)
            {
                mat[pos.Line, pos.Row] = true;
            }
            pos.DefineValues(Position.Line + 1, Position.Row + 1);
            if (ChessBoard.ValidatePosition(pos) && Enemy(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            pos.DefineValues(Position.Line + 1, Position.Row - 1);
            if (ChessBoard.ValidatePosition(pos) && Enemy(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            // EN PASSANT

            if (Position.Line == 4)
            {
                var left = new Position(Position.Line, Position.Row - 1);
                if (ChessBoard.ValidatePosition(left) && Enemy(left) && ChessBoard.GetPieceByPosition(left) == _match.EnPassant)
                {
                    mat[left.Line + 1, left.Row] = true;
                }
                var right = new Position(Position.Line, Position.Row + 1);
                if (ChessBoard.ValidatePosition(right) && Enemy(right) && ChessBoard.GetPieceByPosition(right) == _match.EnPassant)
                {
                    mat[right.Line + 1, right.Row] = true;
                }
            }

        }

        return mat;

    }

    public override string ToString()
    {
        return "P";
    }

}