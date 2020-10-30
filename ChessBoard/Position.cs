using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBoard
{
    class Position
    {
        public int line { get; set; }
        public int row { get; set; }

        public Position(int line, int row)
        {
            this.line = line;
            this.row = row;
        }

        public override string ToString()
        {
            return line
                + ", "
                + row;
        }
    }
}
