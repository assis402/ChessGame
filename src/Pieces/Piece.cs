using ChessGame.Board;

namespace ChessGame.Pieces;

public abstract class Piece
{
    public Position Position { get; protected set; }
        
    public Color Color { get; }

    public int TotalMoves { get; private set; }

    protected ChessBoard ChessBoard { get; }

    protected Piece(ChessBoard chessBoard, Color color)
    {
        Position = null;
        Color = color;
        TotalMoves = 0;
        ChessBoard = chessBoard;
    }
    
    protected bool CanMove(Position position)
    {
        var piece = ChessBoard.GetPieceByPosition(position);
        return piece == null || piece.Color != Color;
    }
    
    public void SetPosition(Position position) => Position = position;
        
    public void IncreaseMove() => TotalMoves++;
        
    public void DecreaseMove() => TotalMoves--;

    public bool IsTherePossibleMove()
    {
        var mat = PossibleMoves();
        for (var i = 0; i < ChessBoard.Lines; i++)
        {
            for (var j = 0; j < ChessBoard.Rows; j++)
            {
                if (mat[i, j]) return true;
            }
        }

        return false;
    }

    public bool IsPossibleMoveToPosition(Position position) => PossibleMoves()[position.Line, position.Row];

    public abstract bool[,] PossibleMoves();
}