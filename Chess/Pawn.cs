using ChessBoard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Pawn : Piece
    {
        private ChessMatch match;
        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.match = match;
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
                Position p2 = new Position(position.line - 1, position.row);
                if (board.validPosition(p2) && free(p2) && board.validPosition(pos) && free(pos) && qtsMoves == 0)
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

                // EN PASSANT

                if (position.line == 3)
                {
                    Position left = new Position(position.line, position.row - 1);
                    if (board.validPosition(left) && enemy(left) && board.piece(left) == match.enPassant)
                    {
                        mat[left.line - 1, left.row] = true;
                    }
                    Position right = new Position(position.line, position.row + 1);
                    if (board.validPosition(right) && enemy(right) && board.piece(right) == match.enPassant)
                    {
                        mat[right.line - 1, right.row] = true;
                    }
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
                Position p2 = new Position(position.line + 1, position.row);
                if (board.validPosition(p2) && free(p2) && board.validPosition(pos) && free(pos) && qtsMoves == 0)
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

                // EN PASSANT

                if (position.line == 4)
                {
                    Position left = new Position(position.line, position.row - 1);
                    if (board.validPosition(left) && enemy(left) && board.piece(left) == match.enPassant)
                    {
                        mat[left.line + 1, left.row] = true;
                    }
                    Position right = new Position(position.line, position.row + 1);
                    if (board.validPosition(right) && enemy(right) && board.piece(right) == match.enPassant)
                    {
                        mat[right.line + 1, right.row] = true;
                    }
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
