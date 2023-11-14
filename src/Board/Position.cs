﻿namespace ChessGame.Board;

public class Position
{
    public int Line { get; set; }
    public int Row { get; set; }

    public Position(int line, int row)
    {
        Line = line;
        Row = row;
    }
        
    public void DefineValues(int line, int row)
    {
        Line = line;
        Row = row;
    }

    public override string ToString()
    {
        return Line + ", "  + Row;
    }
}