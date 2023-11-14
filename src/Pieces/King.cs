using ChessGame.Board;

namespace ChessGame.Pieces;

internal class King : Piece
{
    private readonly ChessMatch _match;

    internal King(ChessBoard chessBoard, Color color, ChessMatch match) : base(chessBoard, color) 
        => _match = match;

    public override string ToString() => "K";

    private bool TestRookToRoque(Position pos)
    {
        var piece = ChessBoard.GetPieceByPosition(pos);
        return piece != null && piece is Rook && piece.Color == Color && piece.TotalMoves == 0;
    }

    internal override bool[,] PossibleMoves()
    {
        var match = new bool[ChessBoard.Rows, ChessBoard.Columns];
        var position = new Position(0, 0);

        // N
        position.SetValues(Position.Row - 1, Position.Column);
        if (ChessBoard.IsValidPosition(position) && CanMove(position)) 
            match[position.Row, position.Column] = true;

        // NE
        position.SetValues(Position.Row - 1, Position.Column + 1);
        if (ChessBoard.IsValidPosition(position) && CanMove(position)) 
            match[position.Row, position.Column] = true;

        // E
        position.SetValues(Position.Row, Position.Column + 1);
        if (ChessBoard.IsValidPosition(position) && CanMove(position)) 
            match[position.Row, position.Column] = true;

        // SE
        position.SetValues(Position.Row + 1, Position.Column + 1);
        if (ChessBoard.IsValidPosition(position) && CanMove(position))
            match[position.Row, position.Column] = true;

        // S
        position.SetValues(Position.Row + 1, Position.Column);
        if (ChessBoard.IsValidPosition(position) && CanMove(position)) 
            match[position.Row, position.Column] = true;

        // SW
        position.SetValues(Position.Row + 1, Position.Column - 1);
        if (ChessBoard.IsValidPosition(position) && CanMove(position)) 
            match[position.Row, position.Column] = true;

        // W
        position.SetValues(Position.Row, Position.Column - 1);
        if (ChessBoard.IsValidPosition(position) && CanMove(position)) 
            match[position.Row, position.Column] = true;

        // NW
        position.SetValues(Position.Row - 1, Position.Column - 1);
        if (ChessBoard.IsValidPosition(position) && CanMove(position)) 
            match[position.Row, position.Column] = true;

        // ROQUE

        if (TotalMoves == 0 && !_match.Check)
        {
            var posT1 = new Position(Position.Row, Position.Column+3);
            if (TestRookToRoque(posT1))
            {
                var p1 = new Position(Position.Row, Position.Column + 1);
                var p2 = new Position(Position.Row, Position.Column + 2);
                if (ChessBoard.GetPieceByPosition(p1) == null && ChessBoard.GetPieceByPosition(p2) == null)
                {
                    match[Position.Row, Position.Column + 2] = true;
                }

            }

            var posT2 = new Position(Position.Row, Position.Column - 4);
            if (TestRookToRoque(posT2))
            {
                var p1 = new Position(Position.Row, Position.Column - 1);
                var p2 = new Position(Position.Row, Position.Column - 2);
                var p3 = new Position(Position.Row, Position.Column - 3);
                if (ChessBoard.GetPieceByPosition(p1) == null && ChessBoard.GetPieceByPosition(p2) == null && ChessBoard.GetPieceByPosition(p3) == null)
                {
                    match[Position.Row, Position.Column - 2] = true;
                }

            }
        }

        return match;
    }
}