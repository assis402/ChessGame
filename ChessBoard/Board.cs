using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
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

        public Piece piece(Position pos)
        { 
            return pieces[pos.line, pos.row];
        }

        public bool isTherePiece(Position pos)
        {
            positionValidate(pos);
            return piece(pos) != null;
        }

        public void putPiece(Piece p, Position pos)
        {
            if (isTherePiece(pos))
            {
                throw new BoardException("There is already a piece in that position!");
            }
            pieces[pos.line, pos.row] = p;
            p.position = pos;

        }

        public bool validPosition(Position pos)
        {
            if (pos.line < 0 || pos.line >= lines || pos.row < 0 || pos.row >= rows)
            {
                return false;
            }
            return true;
        }

        public void positionValidate(Position pos)
        {
            if (!validPosition(pos))
            {
                throw new BoardException("Invalid Position!");
            }
        }


        
    }

}
