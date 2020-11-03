using ChessBoard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {

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

            pos.defineValues(position.line - 1, position.row - 2);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }
            pos.defineValues(position.line - 2, position.row - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }
            pos.defineValues(position.line - 2, position.row + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }
            pos.defineValues(position.line - 1, position.row + 2);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }
            pos.defineValues(position.line + 1, position.row + 2);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }
            pos.defineValues(position.line + 2, position.row + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }
            pos.defineValues(position.line + 2, position.row - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }
            pos.defineValues(position.line + 1, position.row - 2);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
            }

            return mat;

        }

        public override string ToString()
        {
            return "N";
        }

    }
}
