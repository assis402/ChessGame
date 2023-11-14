using ChessGame.Board;

namespace ChessGame.Pieces;

internal class Bishop : Piece
{
    public Bishop(ChessBoard chessBoard, Color color) : base(chessBoard, color)
    {
    }

    public override bool[,] PossibleMoves()
    {
        var mat = new bool[ChessBoard.Lines, ChessBoard.Rows];
        var position = new Position(0, 0);

        // NW
        position.DefineValues(Position.Line - 1, Position.Row - 1);
        while (ChessBoard.ValidatePosition(position) && CanMove(position))
        {
            mat[position.Line, position.Row] = true;

            if (ChessBoard.GetPieceByPosition(position) != null && ChessBoard.GetPieceByPosition(position).Color != Color)
                break;

            position.DefineValues(position.Line - 1, position.Row - 1);
        }

        // NE
        position.DefineValues(Position.Line - 1, Position.Row + 1);
        while (ChessBoard.ValidatePosition(position) && CanMove(position))
        {
            mat[position.Line, position.Row] = true;

            if (ChessBoard.GetPieceByPosition(position) != null && ChessBoard.GetPieceByPosition(position).Color != Color)
                break;

            position.DefineValues(position.Line - 1, position.Row + 1);
        }

        // SE
        position.DefineValues(Position.Line + 1, Position.Row + 1);
        while (ChessBoard.ValidatePosition(position) && CanMove(position))
        {
            mat[position.Line, position.Row] = true;
            if (ChessBoard.GetPieceByPosition(position) != null && ChessBoard.GetPieceByPosition(position).Color != Color)
            {
                break;
            }

            position.DefineValues(position.Line + 1, position.Row + 1);
        }

        // SW
        position.DefineValues(Position.Line + 1, Position.Row - 1);
        while (ChessBoard.ValidatePosition(position) && CanMove(position))
        {
            mat[position.Line, position.Row] = true;
            if (ChessBoard.GetPieceByPosition(position) != null && ChessBoard.GetPieceByPosition(position).Color != Color)
            {
                break;
            }

            position.DefineValues(position.Line + 1, position.Row - 1);
        }

        return mat;
    }

    public override string ToString() => "B";
}