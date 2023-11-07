using ChessBoard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    internal class ChessPosition
    {
        public char Row { get; set; }
        public int Line { get; set; }

        public ChessPosition(char row, int line)
        {
            Row = row;
            Line = line;
        }

        public Position toPosition()
        {
            return new Position(8 - Line, Row - 'a');
        }
        public override string ToString()
        {
            return "" + Row + Line;
        }
    }
}
