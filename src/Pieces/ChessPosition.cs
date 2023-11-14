using ChessGame.Board;

namespace ChessGame.Pieces;

internal class ChessPosition
{
    internal ChessPosition(char column, int row)
    {
        Column = column;
        Row = row;
    }
    
    private char Column { get; set; }

    private int Row { get; set; }

    internal Position ConvertToNumericPosition() => new(8 - Row, Column - 'a');

    public override string ToString() => "" + Column + Row;
}