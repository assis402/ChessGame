﻿using ChessBoard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
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

            // NW
            pos.defineValues(position.line - 1, position.row - 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.line - 1, pos.row - 1);
            }

            // NE
            pos.defineValues(position.line - 1, position.row + 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.line - 1, pos.row + 1);
            }

            // SE
            pos.defineValues(position.line + 1, position.row + 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.line + 1, pos.row + 1);
            }

            // SW
            pos.defineValues(position.line + 1, position.row - 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.row] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.line + 1, pos.row - 1);
            }


            return mat;

        }

        public override string ToString()
        {
            return "B";
        }

    }
}
