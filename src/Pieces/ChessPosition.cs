using ChessGame.Board;

namespace ChessGame.Pieces;

internal class ChessPosition
{
    private char Row { get; set; }

    private int Line { get; set; }

    public ChessPosition(char row, int line)
    {
        Row = row;
        Line = line;
    }

    public Position ToPosition() => new(8 - Line, Row - 'a');

    public override string ToString() => "" + Row + Line;
}