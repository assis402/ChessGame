using ChessBoard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class ChessPosition
    {
        public char row { get; set; }
        public int line { get; set; }

        public ChessPosition(char row, int line)
        {
            this.row = row;
            this.line = line;
        }

        public Position toPosition()
        {
            return new Position(8 - line, row - 'a');
        }
        public override string ToString()
        {
            return "" + row + line;
        }
    }
}
