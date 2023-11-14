using ChessGame.Board;

namespace ChessGame.Pieces;

internal class Pawn : Piece
{
    private readonly ChessMatch _match;
    
    internal Pawn(ChessBoard chessBoard, Color color, ChessMatch match) : base(chessBoard, color) 
        => _match = match;
    
    public override string ToString() => "P";
    
    internal override bool[,] PossibleMoves()
    {
        var match = new bool[ChessBoard.Rows, ChessBoard.Columns];
        var position = new Position(0, 0);

        if (Color == Color.White)
        {
            position.SetValues(Position.Row - 1, Position.Column);
            
            if (ChessBoard.IsValidPosition(position) && IsFreePosition(position)) 
                match[position.Row, position.Column] = true;
            
            position.SetValues(Position.Row - 2, Position.Column);
            var p2 = new Position(Position.Row - 1, Position.Column);
            if (ChessBoard.IsValidPosition(p2) && IsFreePosition(p2) && ChessBoard.IsValidPosition(position) && IsFreePosition(position) && TotalMoves == 0)
            {
                match[position.Row, position.Column] = true;
            }
            position.SetValues(Position.Row - 1, Position.Column - 1);
            if (ChessBoard.IsValidPosition(position) && IsEnemy(position))
            {
                match[position.Row, position.Column] = true;
            }
            position.SetValues(Position.Row - 1, Position.Column + 1);
            if (ChessBoard.IsValidPosition(position) && IsEnemy(position))
            {
                match[position.Row, position.Column] = true;
            }

            // EN PASSANT

            if (Position.Row == 3)
            {
                var left = new Position(Position.Row, Position.Column - 1);
                if (ChessBoard.IsValidPosition(left) && IsEnemy(left) && ChessBoard.GetPieceByPosition(left) == _match.EnPassant)
                {
                    match[left.Row - 1, left.Column] = true;
                }
                var right = new Position(Position.Row, Position.Column + 1);
                if (ChessBoard.IsValidPosition(right) && IsEnemy(right) && ChessBoard.GetPieceByPosition(right) == _match.EnPassant)
                {
                    match[right.Row - 1, right.Column] = true;
                }
            }
        }

        else
        {
            position.SetValues(Position.Row + 1, Position.Column);
            if (ChessBoard.IsValidPosition(position) && IsFreePosition(position))
            {
                match[position.Row, position.Column] = true;
            }
            position.SetValues(Position.Row + 2, Position.Column);
            var p2 = new Position(Position.Row + 1, Position.Column);
            if (ChessBoard.IsValidPosition(p2) && IsFreePosition(p2) && ChessBoard.IsValidPosition(position) && IsFreePosition(position) && TotalMoves == 0)
            {
                match[position.Row, position.Column] = true;
            }
            position.SetValues(Position.Row + 1, Position.Column + 1);
            if (ChessBoard.IsValidPosition(position) && IsEnemy(position))
            {
                match[position.Row, position.Column] = true;
            }
            position.SetValues(Position.Row + 1, Position.Column - 1);
            if (ChessBoard.IsValidPosition(position) && IsEnemy(position))
            {
                match[position.Row, position.Column] = true;
            }

            // EN PASSANT

            if (Position.Row == 4)
            {
                var left = new Position(Position.Row, Position.Column - 1);
                if (ChessBoard.IsValidPosition(left) && IsEnemy(left) && ChessBoard.GetPieceByPosition(left) == _match.EnPassant)
                {
                    match[left.Row + 1, left.Column] = true;
                }
                var right = new Position(Position.Row, Position.Column + 1);
                if (ChessBoard.IsValidPosition(right) && IsEnemy(right) && ChessBoard.GetPieceByPosition(right) == _match.EnPassant)
                {
                    match[right.Row + 1, right.Column] = true;
                }
            }

        }

        return match;

    }
}