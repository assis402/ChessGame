using ChessGame.Board;

namespace ChessGame.Pieces;

internal class Queen : Piece
{
    public Queen(ChessBoard chessBoard, Color color) : base(chessBoard, color)
    {
    }

    public override bool[,] PossibleMoves()
    {
        var mat = new bool[ChessBoard.Lines, ChessBoard.Rows];
        var pos = new Position(0, 0);

        // N
        pos.DefineValues(Position.Line - 1, Position.Row);
        while (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
            if (ChessBoard.GetPieceByPosition(pos) != null && ChessBoard.GetPieceByPosition(pos).Color != Color)
            {
                break;
            }
            pos.Line -= 1;

        }

        // S
        pos.DefineValues(Position.Line + 1, Position.Row);
        while (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
            if (ChessBoard.GetPieceByPosition(pos) != null && ChessBoard.GetPieceByPosition(pos).Color != Color)
            {
                break;
            }
            pos.Line += 1;

        }

        // E
        pos.DefineValues(Position.Line, Position.Row + 1);
        while (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
            if (ChessBoard.GetPieceByPosition(pos) != null && ChessBoard.GetPieceByPosition(pos).Color != Color)
            {
                break;
            }
            pos.Row += 1;

        }

        // W
        pos.DefineValues(Position.Line, Position.Row - 1);
        while (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
            if (ChessBoard.GetPieceByPosition(pos) != null && ChessBoard.GetPieceByPosition(pos).Color != Color)
            {
                break;
            }
            pos.Row -= 1;

        }

        // NW
        pos.DefineValues(Position.Line - 1, Position.Row - 1);
        while (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
            if (ChessBoard.GetPieceByPosition(pos) != null && ChessBoard.GetPieceByPosition(pos).Color != Color)
            {
                break;
            }
            pos.DefineValues(pos.Line - 1, pos.Row - 1);

        }

        // NE
        pos.DefineValues(Position.Line - 1, Position.Row + 1);
        while (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
            if (ChessBoard.GetPieceByPosition(pos) != null && ChessBoard.GetPieceByPosition(pos).Color != Color)
            {
                break;
            }
            pos.DefineValues(pos.Line - 1, pos.Row + 1);

        }

        // SW
        pos.DefineValues(Position.Line + 1, Position.Row - 1);
        while (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
            if (ChessBoard.GetPieceByPosition(pos) != null && ChessBoard.GetPieceByPosition(pos).Color != Color)
            {
                break;
            }
            pos.DefineValues(pos.Line + 1, pos.Row - 1);

        }

        // SE
        pos.DefineValues(Position.Line + 1, Position.Row + 1);
        while (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
            if (ChessBoard.GetPieceByPosition(pos) != null && ChessBoard.GetPieceByPosition(pos).Color != Color)
            {
                break;
            }
            pos.DefineValues(pos.Line + 1, pos.Row + 1);

        }

        return mat;

    }

    public override string ToString()
    {
        return "Q";
    }

}