using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBoard
{
    class Board
    {
        public int lines { get; set; }
        public int rows { get; set; }
        private Piece[,] pieces;

        public Board(int lines, int rows)
        {
            this.lines = lines;
            this.rows = rows;
            pieces = new Piece[lines, rows];

        }

        public Piece piece(int line, int row)
        {
            return pieces[line, row];
        }
    }
}
