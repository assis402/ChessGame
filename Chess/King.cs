using ChessBoard;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "K";
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.Lines, board.Rows];
            Position pos = new Position(0, 0);

            // N
            pos.defineValues(position.line - 1, position.row);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            // NE
            pos.defineValues(position.line - 1, position.row + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            // E
            pos.defineValues(position.line, position.row + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            // SE
            pos.defineValues(position.line + 1, position.row + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            // S
            pos.defineValues(position.line + 1, position.row);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            // SW
            pos.defineValues(position.line + 1, position.row - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            // W
            pos.defineValues(position.line, position.row - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            // NW
            pos.defineValues(position.line - 1, position.row - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            return mat;

        }
    }
}
