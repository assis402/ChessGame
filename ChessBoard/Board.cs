using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ChessBoard
{
    class Board
    {
        public int Lines { get; set; }
        public int Rows { get; set; }
        private Piece[,] pieces;

        public Board(int lines, int rows)
        {
            this.Lines = lines;
            this.Rows = rows;
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

        public Piece removePiece(Position pos)
        {
            if (piece(pos) == null)
            {
                return null;
            }
            Piece aux = piece(pos);
            aux.position = null;
            pieces[pos.line, pos.row] = null;
            return aux;
        }

        public bool validPosition(Position pos)
        {
            if (pos.line < 0 || pos.line >= Lines || pos.row < 0 || pos.row >= Rows)
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
