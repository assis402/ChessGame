using ChessBoard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color)
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

            // N
            pos.defineValues(position.line - 1, position.row);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.line -= 1;

            }

            // S
            pos.defineValues(position.line + 1, position.row);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.line += 1;

            }

            // E
            pos.defineValues(position.line, position.row + 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.row += 1;

            }

            // W
            pos.defineValues(position.line, position.row - 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.row -= 1;

            }

            return mat;

        }

        public override string ToString()
        {
            return "R";
        }

    }
}
