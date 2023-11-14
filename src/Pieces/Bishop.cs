using ChessGame.Board;

namespace ChessGame.Pieces;

internal class Bishop : Piece
{
    internal Bishop(ChessBoard chessBoard, Color color) : base(chessBoard, color)
    {
    }

    public override string ToString() => "B";
    
    internal override bool[,] PossibleMoves()
    {
        var match = new bool[ChessBoard.Rows, ChessBoard.Columns];
        var position = new Position(0, 0);

        // NW
        position.SetValues(Position.Row - 1, Position.Column - 1);
        while (ChessBoard.IsValidPosition(position) && CanMove(position))
        {
            match[position.Row, position.Column] = true;

            if (ChessBoard.GetPieceByPosition(position) != null && ChessBoard.GetPieceByPosition(position).Color != Color)
                break;

            position.SetValues(position.Row - 1, position.Column - 1);
        }

        // NE
        position.SetValues(Position.Row - 1, Position.Column + 1);
        while (ChessBoard.IsValidPosition(position) && CanMove(position))
        {
            match[position.Row, position.Column] = true;
            
            if (IsEnemy(position)) break;
            
            position.SetValues(position.Row - 1, position.Column + 1);
        }

        // SE
        position.SetValues(Position.Row + 1, Position.Column + 1);
        while (ChessBoard.IsValidPosition(position) && CanMove(position))
        {
            match[position.Row, position.Column] = true;
            
            if (IsEnemy(position)) break;

            position.SetValues(position.Row + 1, position.Column + 1);
        }

        // SW
        position.SetValues(Position.Row + 1, Position.Column - 1);
        while (ChessBoard.IsValidPosition(position) && CanMove(position))
        {
            match[position.Row, position.Column] = true;
            
            if (IsEnemy(position)) break;

            position.SetValues(position.Row + 1, position.Column - 1);
        }

        return match;
    }
}