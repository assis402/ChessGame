using ChessGame.Pieces;

namespace ChessGame.Board;

internal class ChessBoard
{
    internal ChessBoard(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        _pieces = new Piece[rows, columns];
    }
    
    internal int Rows { get; }
        
    internal int Columns { get; }
    
    private readonly Piece[,] _pieces;

    internal Piece GetPieceByPosition(int row, int column) => _pieces[row, column];

    internal Piece GetPieceByPosition(Position pos) => _pieces[pos.Row, pos.Column];

    private bool IsTherePieceInThisPosition(Position position)
    {
        if (!IsValidPosition(position))
            throw new BoardException("Invalid Position!");
            
        return GetPieceByPosition(position) is not null;
    }

    internal void PutPiece(Piece piece, Position position)
    {
        if (IsTherePieceInThisPosition(position))
            throw new BoardException("There is already a piece in that position!");

        _pieces[position.Row, position.Column] = piece;
        piece.SetPosition(position);
    }

    internal Piece RemovePiece(Position position)
    {
        if (GetPieceByPosition(position) is null)
            return null;
            
        var piece = GetPieceByPosition(position);
        piece.SetPosition(null);
        _pieces[position.Row, position.Column] = null;
            
        return piece;
    }

    internal bool IsValidPosition(Position position) 
        => position.Row >= 0 && position.Row < Rows && position.Column >= 0 && position.Column < Columns;
}