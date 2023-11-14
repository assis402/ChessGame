using ChessGame.Pieces;

namespace ChessGame.Board;

public class ChessBoard
{
    public int Lines { get; }
        
    public int Rows { get; }
        
    private readonly Piece[,] _pieces;

    public ChessBoard(int lines, int rows)
    {
        Lines = lines;
        Rows = rows;
        _pieces = new Piece[lines, rows];
    }

    public Piece GetPieceByPosition(int line, int row) => _pieces[line, row];

    public Piece GetPieceByPosition(Position pos) => _pieces[pos.Line, pos.Row];

    private bool IsTherePieceInThisPosition(Position position)
    {
        if (!ValidatePosition(position))
            throw new BoardException("Invalid Position!");
            
        return GetPieceByPosition(position) is not null;
    }

    public void PutPiece(Piece piece, Position position)
    {
        if (IsTherePieceInThisPosition(position))
            throw new BoardException("There is already a piece in that position!");

        _pieces[position.Line, position.Row] = piece;
        piece.SetPosition(position);
    }

    public Piece RemovePiece(Position pos)
    {
        if (GetPieceByPosition(pos) is null)
            return null;
            
        var piece = GetPieceByPosition(pos);
        piece.SetPosition(null);
        _pieces[pos.Line, pos.Row] = null;
            
        return piece;
    }

    public bool ValidatePosition(Position pos) 
        => pos.Line >= 0 && pos.Line < Lines && pos.Row >= 0 && pos.Row < Rows;
}