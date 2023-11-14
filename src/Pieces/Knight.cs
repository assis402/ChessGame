using ChessGame.Board;

namespace ChessGame.Pieces;

internal class Knight : Piece
{
    public Knight(ChessBoard chessBoard, Color color) : base(chessBoard, color)
    {
    }

    public override bool[,] PossibleMoves()
    {
        var mat = new bool[ChessBoard.Lines, ChessBoard.Rows];
        var pos = new Position(0, 0);

        pos.DefineValues(Position.Line - 1, Position.Row - 2);
        if (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
        }
        pos.DefineValues(Position.Line - 2, Position.Row - 1);
        if (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
        }
        pos.DefineValues(Position.Line - 2, Position.Row + 1);
        if (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
        }
        pos.DefineValues(Position.Line - 1, Position.Row + 2);
        if (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
        }
        pos.DefineValues(Position.Line + 1, Position.Row + 2);
        if (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
        }
        pos.DefineValues(Position.Line + 2, Position.Row + 1);
        if (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
        }
        pos.DefineValues(Position.Line + 2, Position.Row - 1);
        if (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
        }
        pos.DefineValues(Position.Line + 1, Position.Row - 2);
        if (ChessBoard.ValidatePosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Row] = true;
        }

        return mat;

    }

    public override string ToString()
    {
        return "N";
    }

}