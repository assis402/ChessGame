using ChessBoard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    internal class Pawn : Piece
    {
        private ChessMatch _match;
        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            _match = match;
        }

        private bool enemy(Position pos)
        {
            Piece p = Board.GetPieceByPosition(pos);
            return p != null && p.Color != Color;
        }

        private bool free(Position pos)
        {
            return Board.GetPieceByPosition(pos) == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Rows];
            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.DefineValues(Position.Line - 1, Position.Row);
                if (Board.ValidatePosition(pos) && free(pos))
                {
                    mat[pos.Line, pos.Row] = true;
                }
                pos.DefineValues(Position.Line - 2, Position.Row);
                Position p2 = new Position(Position.Line - 1, Position.Row);
                if (Board.ValidatePosition(p2) && free(p2) && Board.ValidatePosition(pos) && free(pos) && TotalMoves == 0)
                {
                    mat[pos.Line, pos.Row] = true;
                }
                pos.DefineValues(Position.Line - 1, Position.Row - 1);
                if (Board.ValidatePosition(pos) && enemy(pos))
                {
                    mat[pos.Line, pos.Row] = true;
                }
                pos.DefineValues(Position.Line - 1, Position.Row + 1);
                if (Board.ValidatePosition(pos) && enemy(pos))
                {
                    mat[pos.Line, pos.Row] = true;
                }

                // EN PASSANT

                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Row - 1);
                    if (Board.ValidatePosition(left) && enemy(left) && Board.GetPieceByPosition(left) == _match.EnPassant)
                    {
                        mat[left.Line - 1, left.Row] = true;
                    }
                    Position right = new Position(Position.Line, Position.Row + 1);
                    if (Board.ValidatePosition(right) && enemy(right) && Board.GetPieceByPosition(right) == _match.EnPassant)
                    {
                        mat[right.Line - 1, right.Row] = true;
                    }
                }
            }

            else
            {
                pos.DefineValues(Position.Line + 1, Position.Row);
                if (Board.ValidatePosition(pos) && free(pos))
                {
                    mat[pos.Line, pos.Row] = true;
                }
                pos.DefineValues(Position.Line + 2, Position.Row);
                Position p2 = new Position(Position.Line + 1, Position.Row);
                if (Board.ValidatePosition(p2) && free(p2) && Board.ValidatePosition(pos) && free(pos) && TotalMoves == 0)
                {
                    mat[pos.Line, pos.Row] = true;
                }
                pos.DefineValues(Position.Line + 1, Position.Row + 1);
                if (Board.ValidatePosition(pos) && enemy(pos))
                {
                    mat[pos.Line, pos.Row] = true;
                }
                pos.DefineValues(Position.Line + 1, Position.Row - 1);
                if (Board.ValidatePosition(pos) && enemy(pos))
                {
                    mat[pos.Line, pos.Row] = true;
                }

                // EN PASSANT

                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Row - 1);
                    if (Board.ValidatePosition(left) && enemy(left) && Board.GetPieceByPosition(left) == _match.EnPassant)
                    {
                        mat[left.Line + 1, left.Row] = true;
                    }
                    Position right = new Position(Position.Line, Position.Row + 1);
                    if (Board.ValidatePosition(right) && enemy(right) && Board.GetPieceByPosition(right) == _match.EnPassant)
                    {
                        mat[right.Line + 1, right.Row] = true;
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
