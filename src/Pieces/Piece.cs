using ChessGame.Board;

namespace ChessGame.Pieces;

internal abstract class Piece
{
    protected Piece(ChessBoard chessBoard, Color color)
    {
        Position = null;
        Color = color;
        TotalMoves = 0;
        ChessBoard = chessBoard;
    }
    
    internal Position Position { get; private set; }
        
    internal Color Color { get; }

    internal int TotalMoves { get; private set; }

    protected ChessBoard ChessBoard { get; }
    
    protected bool IsEnemy(Position position)
    {
        var piece = ChessBoard.GetPieceByPosition(position);
        return piece != null && piece.Color != Color;
    }
    
    protected bool IsFreePosition(Position position) => ChessBoard.GetPieceByPosition(position) == null;
    
    protected bool CanMove(Position position)
    {
        var piece = ChessBoard.GetPieceByPosition(position);
        return piece == null || piece.Color != Color;
    }
    
    internal void SetPosition(Position position) => Position = position;
        
    internal void IncreaseMove() => TotalMoves++;
        
    internal void DecreaseMove() => TotalMoves--;

    internal bool IsThereAnyPossibleMove()
    {
        var possibleMoves = PossibleMoves();
        
        for (var i = 0; i < ChessBoard.Rows; i++)
        {
            for (var j = 0; j < ChessBoard.Columns; j++)
                if (possibleMoves[i, j])
                    return true;
        }

        return false;
    }

    internal bool IsPossibleMoveToPosition(Position position) => PossibleMoves()[position.Row, position.Column];

    internal abstract bool[,] PossibleMoves();
}