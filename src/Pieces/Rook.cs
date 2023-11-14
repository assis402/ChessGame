using ChessGame.Board;

namespace ChessGame.Pieces;

internal class Rook : Piece
{
    internal Rook(ChessBoard chessBoard, Color color) : base(chessBoard, color)
    {
    }

    public override string ToString() => "R";

    internal override bool[,] PossibleMoves()
    {
        var match = new bool[ChessBoard.Rows, ChessBoard.Columns];
        var position = new Position(0, 0);

        // N
        position.SetValues(Position.Row - 1, Position.Column);
        while (ChessBoard.IsValidPosition(position) && CanMove(position))
        {
            match[position.Row, position.Column] = true;
            
            if (IsEnemy(position)) break;
            
            position.Row -= 1;
        }

        // S
        position.SetValues(Position.Row + 1, Position.Column);
        while (ChessBoard.IsValidPosition(position) && CanMove(position))
        {
            match[position.Row, position.Column] = true;
            
            if (IsEnemy(position)) break;
            
            position.Row += 1;
        }

        // E
        position.SetValues(Position.Row, Position.Column + 1);
        while (ChessBoard.IsValidPosition(position) && CanMove(position))
        {
            match[position.Row, position.Column] = true;
            
            if (IsEnemy(position)) break;
            
            position.Column += 1;
        }

        // W
        position.SetValues(Position.Row, Position.Column - 1);
        while (ChessBoard.IsValidPosition(position) && CanMove(position))
        {
            match[position.Row, position.Column] = true;
            
            if (IsEnemy(position)) break;
            
            position.Column -= 1;
        }

        return match;
    }
}