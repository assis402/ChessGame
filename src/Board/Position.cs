namespace ChessGame.Board;

internal class Position
{
    internal Position(int row, int column)
    {
        Row = row;
        Column = column;
    }
    
    internal int Row { get; set; }
    
    internal int Column { get; set; }
        
    internal void SetValues(int row, int column)
    {
        Row = row;
        Column = column;
    }
    
    public override string ToString() => Row + ", "  + Column;
}