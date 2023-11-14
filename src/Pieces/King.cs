using ChessGame.Board;

namespace ChessGame.Pieces;

internal class King : Piece
{
    private readonly ChessMatch _match;

    public King(ChessBoard chessBoard, Color color, ChessMatch match) : base(chessBoard, color) 
        => _match = match;

    public override string ToString() => "K";

    private bool TestRookToRoque(Position pos)
    {
        var piece = ChessBoard.GetPieceByPosition(pos);
        return piece != null && piece is Rook && piece.Color == Color && piece.TotalMoves == 0;
    }

    public override bool[,] PossibleMoves()
    {
        var match = new bool[ChessBoard.Lines, ChessBoard.Rows];
        var position = new Position(0, 0);

        // N
        position.DefineValues(Position.Line - 1, Position.Row);
        if (ChessBoard.ValidatePosition(position) && CanMove(position)) 
            match[position.Line, position.Row] = true;

        // NE
        position.DefineValues(Position.Line - 1, Position.Row + 1);
        if (ChessBoard.ValidatePosition(position) && CanMove(position)) 
            match[position.Line, position.Row] = true;

        // E
        position.DefineValues(Position.Line, Position.Row + 1);
        if (ChessBoard.ValidatePosition(position) && CanMove(position)) 
            match[position.Line, position.Row] = true;

        // SE
        position.DefineValues(Position.Line + 1, Position.Row + 1);
        if (ChessBoard.ValidatePosition(position) && CanMove(position))
            match[position.Line, position.Row] = true;

        // S
        position.DefineValues(Position.Line + 1, Position.Row);
        if (ChessBoard.ValidatePosition(position) && CanMove(position)) 
            match[position.Line, position.Row] = true;

        // SW
        position.DefineValues(Position.Line + 1, Position.Row - 1);
        if (ChessBoard.ValidatePosition(position) && CanMove(position)) 
            match[position.Line, position.Row] = true;

        // W
        position.DefineValues(Position.Line, Position.Row - 1);
        if (ChessBoard.ValidatePosition(position) && CanMove(position)) 
            match[position.Line, position.Row] = true;

        // NW
        position.DefineValues(Position.Line - 1, Position.Row - 1);
        if (ChessBoard.ValidatePosition(position) && CanMove(position)) 
            match[position.Line, position.Row] = true;

        // ROQUE

        if (TotalMoves == 0 && !_match.Check)
        {
            var posT1 = new Position(Position.Line, Position.Row+3);
            if (TestRookToRoque(posT1))
            {
                var p1 = new Position(Position.Line, Position.Row + 1);
                var p2 = new Position(Position.Line, Position.Row + 2);
                if (ChessBoard.GetPieceByPosition(p1) == null && ChessBoard.GetPieceByPosition(p2) == null)
                {
                    match[Position.Line, Position.Row + 2] = true;
                }

            }

            var posT2 = new Position(Position.Line, Position.Row - 4);
            if (TestRookToRoque(posT2))
            {
                var p1 = new Position(Position.Line, Position.Row - 1);
                var p2 = new Position(Position.Line, Position.Row - 2);
                var p3 = new Position(Position.Line, Position.Row - 3);
                if (ChessBoard.GetPieceByPosition(p1) == null && ChessBoard.GetPieceByPosition(p2) == null && ChessBoard.GetPieceByPosition(p3) == null)
                {
                    match[Position.Line, Position.Row - 2] = true;
                }

            }
        }

        return match;
    }
}