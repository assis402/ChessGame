using ChessBoard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {

        }

        private bool enemy(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p.color != color;
        }

        private bool free(Position pos)
        {
            return board.piece(pos) == null;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.Lines, board.Rows];
            Position pos = new Position(0, 0);

            if (color == Color.White)
            {
                pos.defineValues(position.line - 1, position.row);
                if (board.validPosition(pos) && free(pos))
                {
                    mat[pos.line, pos.row] = true;
                }
                pos.defineValues(position.line - 2, position.row);
                if (board.validPosition(pos) && free(pos) && qtsMoves == 0)
                {
                    mat[pos.line, pos.row] = true;
                }
                pos.defineValues(position.line - 1, position.row - 1);
                if (board.validPosition(pos) && enemy(pos))
                {
                    mat[pos.line, pos.row] = true;
                }
                pos.defineValues(position.line - 1, position.row + 1);
                if (board.validPosition(pos) && enemy(pos))
                {
                    mat[pos.line, pos.row] = true;
                }
            }

            else
            {
                pos.defineValues(position.line + 1, position.row);
                if (board.validPosition(pos) && free(pos))
                {
                    mat[pos.line, pos.row] = true;
                }
                pos.defineValues(position.line + 2, position.row);
                if (board.validPosition(pos) && free(pos) && qtsMoves == 0)
                {
                    mat[pos.line, pos.row] = true;
                }
                pos.defineValues(position.line + 1, position.row + 1);
                if (board.validPosition(pos) && enemy(pos))
                {
                    mat[pos.line, pos.row] = true;
                }
                pos.defineValues(position.line + 1, position.row - 1);
                if (board.validPosition(pos) && enemy(pos))
                {
                    mat[pos.line, pos.row] = true;
                }
            }

            return mat;

        }

        public override string ToString()
        {
            return "P";
        }

    }
}
